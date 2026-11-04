# Purpose
- Crystal is an XNA-inspired C# game framework with a Direct3D 12 backend. Keep it game-agnostic: never name a game, its file formats or its constants.
- Borrow XNA's ideas where they fit. There is no XNA compatibility goal, so don't document departures from XNA.

# Branches and Pull Requests
- `main` is the development branch. Work branches start from `main` and PRs target `main`.
- Commit on work branches after each completed milestone.
- Humans open and merge PRs, squashed to one commit each.
- Agent writes title + short summary (max 100 words), and no AI session links.

# Code Style
- No comments. Documentation lives outside of code.
- Names for code and data must be self-explanatory.
- File-scoped namespaces, LF line endings, warnings are errors.

# Layout
- Public API lives in `Crystal`, `Crystal.Graphics`, `Crystal.Input` and `Crystal.ImGui`. It never exposes D3D, DXGI or SDL types.
- D3D12 code is `internal` under `Graphics/D3D12/`; SDL code is `internal` under `Platform/`.
- SDL3 is bound by hand with `[LibraryImport]` in `Platform/Sdl.cs`. Check new constants and struct layouts against the SDL headers of the pinned version.
- Flat global names use the `CRYSTAL_` prefix: environment variables, debug object names, log tags.

# Graphics
- Run GPU changes with `CRYSTAL_DX12_DEBUG=1` and fix every debug layer error they report. Add `CRYSTAL_DX12_GBV=1` to also catch errors that only show while shaders run.
- Every GPU test runs on WARP under the debug layer and fails on any debug layer error.
- No managed allocations per draw call.

# Documentation
- Add each new public feature to README.md.
- Change `docs/api` in the same commit as any public type or member it describes.
