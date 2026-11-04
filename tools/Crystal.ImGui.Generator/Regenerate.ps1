param(
    [string]$LuaJit = "luajit",
    [string]$CimguiCommit = "125f397e0982fe9466eb0a162de547be5efec849"
)

$ErrorActionPreference = "Stop"

if (-not (Get-Command cl.exe -ErrorAction SilentlyContinue)) {
    throw "cl.exe is not on PATH. Run this script from a Visual Studio developer prompt."
}

$repositoryRoot = Resolve-Path (Join-Path $PSScriptRoot "../..")
$imguiSource = Join-Path $repositoryRoot "artifacts/natives/imgui-1.93.0-docking/imgui"
$nativeDirectory = Join-Path $repositoryRoot "src/Crystal.ImGui/Native"
$patch = Join-Path $PSScriptRoot "cimgui-generator.patch"
$workDirectory = Join-Path ([System.IO.Path]::GetTempPath()) ("cimgui-" + [System.Guid]::NewGuid().ToString("N"))

if (-not (Test-Path (Join-Path $imguiSource "imgui.h"))) {
    throw "Dear ImGui source is missing at $imguiSource. Build src/Crystal.ImGui once to download it."
}

try {
    git clone --quiet --no-checkout https://github.com/cimgui/cimgui.git $workDirectory
    if ($LASTEXITCODE -ne 0) { throw "git clone failed" }
    git -C $workDirectory checkout --quiet $CimguiCommit
    if ($LASTEXITCODE -ne 0) { throw "git checkout $CimguiCommit failed" }
    git -C $workDirectory apply --ignore-whitespace $patch
    if ($LASTEXITCODE -ne 0) { throw "applying cimgui-generator.patch failed" }

    $env:IMGUI_PATH = $imguiSource
    Push-Location (Join-Path $workDirectory "generator")
    try {
        & $LuaJit ./generator.lua cl "noimstrv"
        if ($LASTEXITCODE -ne 0) { throw "the cimgui generator failed" }
    }
    finally {
        Pop-Location
        Remove-Item Env:IMGUI_PATH
    }

    Copy-Item (Join-Path $workDirectory "cimgui.h") $nativeDirectory
    Copy-Item (Join-Path $workDirectory "cimgui.cpp") $nativeDirectory
    foreach ($name in "definitions.json", "structs_and_enums.json", "typedefs_dict.json") {
        Copy-Item (Join-Path $workDirectory "generator/output/$name") $nativeDirectory
    }
}
finally {
    if (Test-Path $workDirectory) { Remove-Item -Recurse -Force $workDirectory }
}

dotnet run --project (Join-Path $PSScriptRoot "Crystal.ImGui.Generator.csproj") -- $repositoryRoot
if ($LASTEXITCODE -ne 0) { throw "Crystal.ImGui.Generator failed with exit code $LASTEXITCODE." }
