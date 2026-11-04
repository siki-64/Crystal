# Credits

Crystal is built on the work of these projects. Each keeps its own license.

| Project | Used for | License |
| --- | --- | --- |
| [Dear ImGui](https://github.com/ocornut/imgui), Omar Cornut and contributors | Debug and tool UI in `Crystal.ImGui`, downloaded and compiled at build time | MIT |
| [cimgui](https://github.com/cimgui/cimgui), Stephan Dilly and contributors | C bindings for Dear ImGui, vendored in `src/Crystal.ImGui/Native` | MIT |
| [SDL](https://github.com/libsdl-org/SDL), Sam Lantinga and contributors | Windows, input and audio devices (`SDL3.dll`, downloaded at build time) | zlib |
| [TerraFX.Interop.Windows](https://github.com/terrafx/terrafx.interop.windows), Tanner Gooding and contributors | Win32, DXGI and Direct3D 12 bindings | MIT |
| [DirectX Agility SDK](https://devblogs.microsoft.com/directx/directx12agility/), Microsoft | `D3D12Core.dll` and the debug layers | Microsoft DirectX SDK license |
| [DirectX Shader Compiler](https://github.com/microsoft/DirectXShaderCompiler), Microsoft | Compiling HLSL at build time and at run time | University of Illinois/NCSA |
| [Roslyn](https://github.com/dotnet/roslyn), .NET Foundation | Analyzer support | MIT |
| [MSTest](https://github.com/microsoft/testfx), Microsoft | Tests | MIT |
