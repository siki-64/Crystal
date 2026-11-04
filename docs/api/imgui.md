# Crystal.ImGui

Dear ImGui 1.93.0 (docking branch) for Crystal: a renderer and input backend, UTF-8 helpers, and bindings generated from cimgui. The package ships the native library `Crystal.ImGui.Native`, built without FreeType.

The hand-written types are documented member by member. The generated `ImGui`, `ImGuiNative`, pointer, struct and enum types follow imgui.h through fixed naming rules, so the Dear ImGui documentation applies to them once the rules below are known.

## ImGuiRenderer
`public sealed unsafe class ImGuiRenderer : IDisposable`

Owns one ImGui context, feeds it Crystal input, and draws its output with the graphics device. Render thread.

| Member | Meaning |
|---|---|
| `const nint ResetRenderStateCallback = -8` | `ImDrawCallback_ResetRenderState`. A draw command with this callback restores the renderer's pipeline, viewport and projection. |
| `ImGuiRenderer(Game game)` | Draws into targets of the back buffer format, without depth, single-sampled. |
| `ImGuiRenderer(Game game, SurfaceFormat renderTargetFormat, SurfaceFormat? depthStencilFormat = null, int sampleCount = 1)` | Draws into targets of these formats. |
| `ImGuiContext* Context { get; }` | The renderer's context. |
| `bool WantCaptureMouse { get; }` | ImGui uses the mouse this frame; the game should ignore it. |
| `bool WantCaptureKeyboard { get; }` | ImGui uses the keyboard this frame. |
| `bool WantTextInput { get; }` | A text field is active. |
| `bool DockingEnabled { get; set; }` | `ImGuiConfigFlags.DockingEnable`. Off by default. |
| `string? IniFilePath { get; set; }` | Where ImGui saves window positions. `null` (the default) saves nothing. |
| `ImTextureRef BindTexture(Texture2D texture)` | Makes a texture drawable by `ImGui.Image` and the draw list. Each call returns a new reference. |
| `void UnbindTexture(ImTextureRef textureRef)` | Forgets a bound texture. References to textures ImGui created itself, such as the font atlas, are ignored. |
| `void BeginFrame(GameTime gameTime)` | Feeds the frame's input and calls `ImGui.NewFrame`. |
| `void EndFrame()` | Calls `ImGui.Render` and draws the result into the bound render target. |
| `void Dispose()` | Destroys the context and the textures and GPU objects the renderer created. Bound textures stay with their owner. |

**Construction** creates and selects a new context, sets `RendererHasVtxOffset` and `RendererHasTextures`, sets no ini file, adds the default font, and subscribes to `GameWindow.TextInput`. The properties select the renderer's context before they read or write it, and so do `BeginFrame` and `EndFrame`.

**BeginFrame** sets `DisplaySize` to the back buffer size, `DeltaTime` to the elapsed game time (1/60 s when 0), focus from `Game.IsActive`, the mouse position, five buttons and the wheel deltas in notches, the four modifier keys, and the keys letters, digits, function keys F1 to F24, navigation, editing, locks, keypad and modifiers. Window text input characters reach ImGui as they arrive; control characters are dropped.

**EndFrame** turns `GameWindow.IsTextInputEnabled` on while `WantTextInput` is true and off when it becomes false. It creates, updates and destroys the textures ImGui requests through the `ImTextureData` protocol, then draws with scissor rectangles from the clip rectangles. It restores the device viewport and scissor rectangle afterwards. Call it between `GraphicsDevice.BeginFrame` and `EndFrame`, with a target bound that matches the formats given to the constructor.

**Callbacks.** Other non-null `UserCallback` values are called as `delegate* unmanaged[Cdecl]<ImDrawList*, ImDrawCmd*, void>` on the render thread.

Using a disposed renderer throws `ObjectDisposedException`. `BeginFrame` throws `ArgumentNullException` for a null `gameTime`.

## ImGuiUtf8
`public static unsafe class ImGuiUtf8`

| Member | Meaning |
|---|---|
| `static string? ToString(byte* text)` | Decodes a null-terminated UTF-8 string. `null` for a null pointer. |
| `static byte* Literal(ReadOnlySpan<byte> nullTerminatedLiteral)` | The address of a `"...\0"u8` literal. Such literals live in the assembly's data and never move, so the pointer stays valid. |

## ImGuiUtf8String
`public unsafe ref struct ImGuiUtf8String`

A null-terminated UTF-8 copy of a string for one native call.

| Member | Meaning |
|---|---|
| `const int StackCapacity = 256` | A good size for the stack buffer. |
| `ImGuiUtf8String(string? text, Span<byte> stackBuffer)` | Encodes into `stackBuffer` when the text fits, otherwise into native memory. |
| `byte* Pointer { get; }` | The text. `null` when `text` was `null`. |
| `void Dispose()` | Frees native memory, if any. |

```csharp
Span<byte> buffer = stackalloc byte[ImGuiUtf8String.StackCapacity];
using var label = new ImGuiUtf8String(name, buffer);
ImGuiNative.igTextUnformatted(label.Pointer, null);
```

## ImGuiUtf8StringArray
`public unsafe ref struct ImGuiUtf8StringArray`

| Member | Meaning |
|---|---|
| `ImGuiUtf8StringArray(string[] items)` | Encodes the strings into one native block: the pointer array followed by the texts. `null` items become empty strings. |
| `byte** Pointer { get; }` | The pointer array. |
| `void Dispose()` | Frees the block. |

## ImVector&lt;T&gt;
`public unsafe struct ImVector<T> where T : unmanaged`

The layout of `ImVector<T>`, read in place.

| Member | Meaning |
|---|---|
| `int Size` | Number of elements. |
| `int Capacity` | Allocated elements. |
| `T* Data` | The elements. |
| `ref T this[int index]` | An element. Throws `ArgumentOutOfRangeException` when `index` is negative or not below `Size`. |
| `Span<T> AsSpan()` | The `Size` elements. |

## ImPtrVector&lt;T&gt;
`public unsafe struct ImPtrVector<T> where T : unmanaged`

An `ImVector` of pointers, such as `ImVector<ImDrawList*>`.

| Member | Meaning |
|---|---|
| `int Size`, `int Capacity` | As in `ImVector<T>`. |
| `T** Data` | The pointers. |
| `T* this[int index]` | A pointer. Throws `ArgumentOutOfRangeException` when `index` is out of range. |

## ImGuiNative
`public static unsafe partial class ImGuiNative`

| Member | Meaning |
|---|---|
| `const string LibraryName = "Crystal.ImGui.Native"` | The native library every entry point binds to. |

The rest of the class is generated; see below.

## Generated bindings
`tools/Crystal.ImGui.Generator` writes the files in `src/Crystal.ImGui/Generated` from cimgui's JSON definitions. Rerun it with `Regenerate.ps1` after updating Dear ImGui. Generated struct layouts are checked by the layout tests.

### ImGuiNative: the raw entry points
Every cimgui function keeps its exact name and parameters: `igBegin`, `ImDrawList_AddLine(ImDrawList* self, …)`, overloads with cimgui's suffixes such as `_Str` and `_Nil`. Text is `byte*` (null-terminated UTF-8), structs are pointers, and `bool` is marshalled as one byte. Formatted-text functions bind to `CRYSTAL_`-prefixed shims that take the text verbatim, such as `CRYSTAL_igText(byte* text)`.

### ImGui: the managed functions
`ImGui` holds every `ig` function with the prefix removed: `igBegin` is `ImGui.Begin`. Parameters change as follows.

| imgui.h | C# |
|---|---|
| `snake_case` parameter names | `camelCase`: `p_open` is `pOpen`. |
| `const char*` | `string`, encoded through `ImGuiUtf8String`. A trailing `text_end` is dropped and passed as `NULL`. |
| `const char* const items[]` with a count after it | `string[]`; the count is the array length. |
| `char* buf, size_t buf_size` | `Span<byte>` holding null-terminated UTF-8. |
| `float v[2]`, `v[3]`, `v[4]` | `ref Vector2`, `ref Vector3`, `ref Vector4`. |
| Other fixed arrays | `Span<T>`. Throws `ArgumentException` when the span is shorter than the C array. |
| Non-const pointer to a number, enum, `ImVec2` or `ImVec4` | `ref T`. |
| Const pointer to values with a count after it | `ReadOnlySpan<T>`; the count is the span length. |
| Pointer to a struct | `XPtr` (see below). |
| Function pointers and callbacks | `void*`. |
| `ImVec2`, `ImVec4` | `System.Numerics.Vector2`, `Vector4`. |
| `size_t`, `char` | `nuint`, `byte`. |
| Returned `const char*` | `string`, decoded with `ImGuiUtf8.ToString`. |
| Returned struct pointer | `XPtr`. |
| Default arguments | Overloads that drop trailing defaulted parameters one at a time. |
| `fmt, ...` | One `string` taken verbatim; `%` is not interpreted. |

```csharp
ImGui.Begin("Stats", ref isOpen, ImGuiWindowFlags.NoTitleBar);
ImGui.Begin("Stats", ref isOpen);
ImGui.Begin("Stats");
ImGui.Combo("Mode", ref modeIndex, modeNames);
ImGui.Checkbox("Wireframe", ref wireframe);
ImGui.Text($"{frameTime:F2} ms");
```

Not generated: functions that take a `va_list`, cimgui's `...0` variants of formatted functions (the shims replace them), templated helpers, FreeType functions, overloads whose managed signature repeats another, and struct methods whose name collides with a field. The generator lists each skip in its `Skipped` output. These remain reachable through `ImGuiNative` where cimgui exports them.

### XPtr: struct pointers
Every struct except `ImVec2` and `ImVec4` gets a `readonly struct XPtr`, such as `ImGuiIOPtr` for `ImGuiIO*`.

| Member | Meaning |
|---|---|
| `X* Handle` | The pointer. |
| `XPtr(X* handle)` | Wraps a pointer. |
| `implicit operator XPtr(X*)`, `implicit operator X*(XPtr)` | Converts both ways. |
| `bool IsNull { get; }` | `Handle == null`. |
| Fields | `ref` properties: `io.DeltaTime = 1f / 60f;`, `ref var flags = ref io.ConfigFlags;`. |
| Pointer-to-struct fields | `YPtr` get/set properties: `io.Fonts` is an `ImFontAtlasPtr`. |
| Bitfields | `uint` get/set properties. |
| Unions | Their members appear as properties of the struct. |
| Methods | cimgui's `X_Method(X* self, …)` without `self`: `drawList.AddLine(p1, p2, color)`. Constructors are `static XPtr Create(…)` and destructors `void Destroy()`. |

### Structs and enums
Structs are blittable copies of the C layout under their C names. `ImVector_X` fields are `ImVector<X>`, or `ImPtrVector<X>` for pointer elements. Fixed-size arrays are `FixedArrayN<T>` inline arrays. `ImSmallStackImDrawFlags3` is the small-stack type of `ImDrawList`.

Enum members drop the enum's own prefix, or the `ImGui` or `Im` prefix with underscores removed: `ImGuiWindowFlags_NoTitleBar` is `ImGuiWindowFlags.NoTitleBar`, `ImGuiMod_Ctrl` is `ImGuiKey.ModCtrl`. A trailing `_` is trimmed, a leading digit gets a `D` (`ImGuiKey_0` is `ImGuiKey.D0`), and C# keywords get `@`.
