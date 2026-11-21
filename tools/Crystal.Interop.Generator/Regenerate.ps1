$ErrorActionPreference = 'Stop'
$engine = Resolve-Path "$PSScriptRoot/../.."
$packages = if ($env:NUGET_PACKAGES) { $env:NUGET_PACKAGES } else { Join-Path $env:USERPROFILE '.nuget/packages' }
[xml]$packageProps = Get-Content (Join-Path $engine 'Directory.Packages.props')
$agilityVersion = $packageProps.Project.PropertyGroup.AgilitySdkVersion

function Get-PackageVersion([string]$id) {
    (($packageProps.Project.ItemGroup.PackageVersion | Where-Object Include -eq $id).Version).Replace('$(AgilitySdkVersion)', $agilityVersion)
}

function ConvertTo-ForwardSlashes([string]$path) { $path.Replace([char]92, [char]47) }

$agilityInclude = Join-Path $packages "microsoft.direct3d.d3d12/$(Get-PackageVersion 'Microsoft.Direct3D.D3D12')/build/native/include"
$dxcInclude = Join-Path $packages "microsoft.direct3d.dxc/$(Get-PackageVersion 'Microsoft.Direct3D.DXC')/build/native/include"
$windowsKitRoot = Join-Path ${env:ProgramFiles(x86)} 'Windows Kits/10/Include'
$windowsKit = Get-ChildItem $windowsKitRoot -Directory | Sort-Object { [version]$_.Name } | Select-Object -Last 1
$includeDirectories = @($agilityInclude, $dxcInclude) + (@('shared', 'um', 'ucrt') | ForEach-Object { Join-Path $windowsKit.FullName $_ })
foreach ($directory in $includeDirectories) {
    if (-not (Test-Path $directory)) { throw "Missing include directory $directory. Restore the solution and install the Windows SDK." }
}

function Resolve-Header([string]$name) {
    foreach ($directory in @($PSScriptRoot) + $includeDirectories) {
        $candidate = Join-Path $directory $name
        if (Test-Path $candidate) { return ConvertTo-ForwardSlashes (Resolve-Path $candidate).Path }
    }
    throw "Header $name not found."
}

function Expand-GroupArguments([string]$responseFile) {
    $expanded = @()
    $expandsHeaders = $false
    foreach ($line in Get-Content $responseFile) {
        if ($line -eq '') { continue }
        if ($line.StartsWith('--')) {
            $expandsHeaders = $line -in @('--file', '--traverse')
            $expanded += $line
            continue
        }
        $expanded += if ($expandsHeaders) { Resolve-Header $line } else { $line }
    }
    return $expanded
}

$frameworkOutput = Join-Path $engine 'src/Crystal.Framework/Interop/Generated'
$testOutput = Join-Path $engine 'tests/Crystal.Tests/Interop/Generated'
$outputByGroup = [ordered]@{ D3D12 = $frameworkOutput; DXGI = $frameworkOutput; Dxc = $frameworkOutput; Windows = $frameworkOutput; D3DCompiler = $testOutput }
$includeArguments = $includeDirectories | ForEach-Object { '--include-directory'; ConvertTo-ForwardSlashes $_ }
Push-Location $engine
try {
    foreach ($group in $outputByGroup.Keys) {
        $output = $outputByGroup[$group]
        New-Item -ItemType Directory -Force $output | Out-Null
        $arguments = @("@$PSScriptRoot/Common.rsp") + $includeArguments + (Expand-GroupArguments "$PSScriptRoot/$group.rsp") + @('--output', "$output/$group.g.cs")
        $log = & dotnet tool run ClangSharpPInvokeGenerator -- @arguments 2>&1 | ForEach-Object { "$_" }
        $errors = @($log | Where-Object { $_ -match '^\s*Error|: error:' })
        $unresolved = @($log | Where-Object { $_ -match "Macro definition '(\w+)' could not be resolved" } | ForEach-Object { [regex]::Match($_, "'(\w+)'").Groups[1].Value })
        Write-Host "$group generated; unresolved macros: $($unresolved -join ', ')"
        if ($errors.Count -gt 0) { $errors | Write-Host; throw "ClangSharp failed for $group." }
        $generated = Get-Content "$output/$group.g.cs" | Where-Object { $_ -notmatch '^\s*///' }
        [IO.File]::WriteAllText("$output/$group.g.cs", ((@('#pragma warning disable CS0649') + $generated) -join "`n") + "`n")
    }
}
finally { Pop-Location }
