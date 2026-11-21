# Credits

Crystal is built on the work of these projects. Each keeps its own license.

| Project | Used for | License |
| --- | --- | --- |
| [Dear ImGui](https://github.com/ocornut/imgui), Omar Cornut and contributors | Debug and tool UI in `Crystal.ImGui`, downloaded and compiled at build time | MIT |
| [cimgui](https://github.com/cimgui/cimgui), Stephan Dilly and contributors | C bindings for Dear ImGui, vendored in `src/Crystal.ImGui/Native` | MIT |
| [SDL](https://github.com/libsdl-org/SDL), Sam Lantinga and contributors | Windows, input and audio devices (`SDL3.dll`, downloaded at build time) | zlib |
| [StbVorbisSharp](https://github.com/StbSharp/StbVorbisSharp), Roman Shapiro and contributors, a C# port of [stb_vorbis](https://github.com/nothings/stb) by Sean Barrett | Decoding Ogg Vorbis in `VorbisAudioSource` | Public domain |
| [NLayer](https://github.com/naudio/NLayer), Mark Heath, Andrew Ward and contributors | Decoding MP3 in `Mp3AudioSource` | MIT |
| [ClangSharp](https://github.com/dotnet/ClangSharp), .NET Foundation and contributors | Generating the Win32, DXGI, Direct3D 12 and DXC bindings in `src/Crystal.Framework/Interop/Generated` (`tools/Crystal.Interop.Generator`) | MIT |
| [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk/), Microsoft | Win32 and DXGI headers the bindings are generated from | Microsoft Windows SDK license |
| [DirectX Agility SDK](https://devblogs.microsoft.com/directx/directx12agility/), Microsoft | `D3D12Core.dll`, the debug layers, and the Direct3D 12 headers the bindings are generated from | Microsoft DirectX SDK license |
| [DirectX Shader Compiler](https://github.com/microsoft/DirectXShaderCompiler), Microsoft | Compiling HLSL at build time and at run time | University of Illinois/NCSA |
| [Roslyn](https://github.com/dotnet/roslyn), .NET Foundation | Analyzer support | MIT |
| [MSTest](https://github.com/microsoft/testfx), Microsoft | Tests | MIT |
