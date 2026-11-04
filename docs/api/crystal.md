# Crystal

The game loop, the window, displays, and the small value types used everywhere. Vectors, matrices, quaternions and planes are `System.Numerics` types.

## Game
`public abstract class Game : IDisposable`

Owns the window and the graphics device and runs the game loop. Derive from it, override the hooks, and call `Run`.

| Member | Meaning |
|---|---|
| `protected Game(GameOptions? options = null)` | Creates the window (hidden until `Run`) and the `GraphicsDevice`. `null` uses `new GameOptions()`. Hides the mouse cursor. Throws `GraphicsException` when no adapter can be used. |
| `GameOptions Options { get; }` | The options passed to the constructor. |
| `GameWindow Window { get; }` | The window. |
| `GraphicsDevice GraphicsDevice { get; }` | The device that draws into the window. |
| `bool IsActive { get; }` | `true` while the window has keyboard focus. Starts `true`. |
| `bool IsMouseVisible { get; set; }` | Shows or hides the cursor over the window. `false` after construction. |
| `TimeSpan TargetElapsedTime { get; set; }` | The length of one fixed update step. Throws `ArgumentOutOfRangeException` for zero or negative values. |
| `bool IsFixedTimeStep { get; set; }` | `true` runs `Update` in steps of `TargetElapsedTime` and sleeps until the next step is due. `false` runs one `Update` and one `Draw` per tick with the real elapsed time. |
| `void Run()` | Calls `Initialize` and `LoadContent`, shows the window unless `GameOptions.StartHidden`, then ticks until `Exit` or the window closes. Calls `UnloadContent` on the way out, also when a hook throws. Blocks the calling thread, which becomes the render thread. |
| `void Exit()` | Ends the loop. The current tick skips its remaining updates and its draw. Closing the window calls it. |
| `protected virtual void Initialize()` | Runs once before `LoadContent`. Empty by default. |
| `protected virtual void LoadContent()` | Runs once before the first tick. Empty by default. |
| `protected virtual void UnloadContent()` | Runs once when `Run` returns. Empty by default. |
| `protected virtual void Update(GameTime gameTime)` | Runs once per update step. Empty by default. |
| `protected virtual bool BeginDraw()` | Begins the frame and returns `true`. An override returns `false` to skip `Draw` and `EndDraw` for this tick, and must call `base.BeginDraw()` before returning `true`. |
| `protected virtual void Draw(GameTime gameTime)` | Draws the frame. Empty by default. |
| `protected virtual void EndDraw()` | Ends and presents the frame. An override must call `base.EndDraw()`. |
| `void Dispose()` | Disposes the device and the window. |
| `protected virtual void Dispose(bool disposing)` | Override to release your own resources; call the base method. |

**Tick.** Each tick processes window events, sleeps until an update is due (fixed time step), runs the due `Update` steps, then `BeginDraw`, `Draw` and `EndDraw`. At most 500 ms of elapsed time is carried into one tick, so a long stall doesn't cause a burst of updates. While the window is minimized the tick skips drawing and sleeps 10 ms.

**Throws** `InvalidOperationException` from `Run` when an override of `BeginDraw` returns `true` without calling `base.BeginDraw`, returns `false` after calling it, or when an override of `EndDraw` doesn't call `base.EndDraw`.

## GameOptions
`public sealed record GameOptions`

| Property | Default | Meaning |
|---|---|---|
| `string Title { get; init; }` | `"Crystal"` | Window title. |
| `int Width { get; init; }` | `1280` | Client area width in pixels. |
| `int Height { get; init; }` | `720` | Client area height in pixels. |
| `bool AllowUserResizing { get; init; }` | `true` | Whether the user can resize the window. |
| `bool StartHidden { get; init; }` | `false` | Keeps the window hidden when `Run` starts. |
| `bool IsFixedTimeStep { get; init; }` | `true` | Initial value of `Game.IsFixedTimeStep`. |
| `TimeSpan TargetElapsedTime { get; init; }` | 1/60 s | Initial value of `Game.TargetElapsedTime`. |
| `GraphicsDeviceOptions Graphics { get; init; }` | `new()` | Options for the device; see [GraphicsDeviceOptions](graphics-device.md#graphicsdeviceoptions). |

## GameTime
`public sealed class GameTime`

| Member | Meaning |
|---|---|
| `GameTime()` | A zero time. The game loop passes one instance to every `Update` and another to every `Draw`, and changes them in place. |
| `TimeSpan TotalGameTime { get; }` | Game time since `Run` started, including the current step. |
| `TimeSpan ElapsedGameTime { get; }` | For `Update`: the length of this step, which is `TargetElapsedTime` with a fixed time step. For `Draw`: the game time that this tick's update steps covered. |
| `bool IsRunningSlowly { get; }` | `true` when the tick had to run more than one update step to catch up. |

## GameWindow
`public sealed class GameWindow : IDisposable`

Created by `Game`; there is no public constructor.

| Member | Meaning |
|---|---|
| `nint Handle { get; }` | The native window handle (HWND). |
| `string Title { get; set; }` | The title bar text. |
| `bool AllowUserResizing { get; set; }` | Whether the user can resize the window. |
| `Point ClientSize { get; }` | Client area size in pixels. |
| `Point Position { get; set; }` | Top-left corner of the window in desktop pixels. |
| `Display Display { get; }` | The display that holds the window. |
| `bool IsMinimized { get; }` | `true` while minimized. |
| `bool IsTextInputEnabled { get; set; }` | Turns on text input, which raises `TextInput`. May show an on-screen keyboard or an input method window; leave it off while only reading keys. |
| `event EventHandler? ClientSizeChanged` | Raised after the client area changes size, during event processing at the start of a tick. The back buffer is resized at the next frame. |
| `event EventHandler<TextInputEventArgs>? TextInput` | Raised once per typed UTF-16 code unit while `IsTextInputEnabled` is on. Characters outside the Basic Multilingual Plane arrive as two surrogate events. |
| `void Dispose()` | Destroys the window. `Game.Dispose` calls it. |

## Display
`public sealed record Display(string Name, Rectangle Bounds, Rectangle WorkArea, bool IsPrimary, float ContentScale)`

| Member | Meaning |
|---|---|
| `static IReadOnlyList<Display> GetAll()` | The connected displays. Works before a `Game` exists. |
| `string Name` | The display's name as the system reports it. |
| `Rectangle Bounds` | The display area in desktop pixels. |
| `Rectangle WorkArea` | `Bounds` without the taskbar and other reserved areas. |
| `bool IsPrimary` | `true` for the primary display. |
| `float ContentScale` | The display's scale setting, 1 for 100 %. |

## TextInputEventArgs
`public readonly record struct TextInputEventArgs(char Character)`

One UTF-16 code unit typed into the window.

## Color
`public readonly record struct Color(byte R, byte G, byte B, byte A = 255)`

An 8-bit RGBA color.

| Member | Meaning |
|---|---|
| `Color(float r, float g, float b, float a = 1f)` | Channels from 0 to 1, clamped and rounded to bytes. |
| `Color(Vector4 rgba)` | Same, from a vector. |
| `uint PackedValue { get; }` | `R` in the low byte, then `G`, `B`, `A`. |
| `Vector4 ToVector4()` | Channels divided by 255. |
| `static Color FromPacked(uint packedValue)` | The inverse of `PackedValue`. |
| `static Color Lerp(Color from, Color to, float amount)` | Linear interpolation of the channels. |
| `Transparent`, `Black`, `White`, `Red`, `Green`, `Blue`, `CornflowerBlue` | Static colors. `Transparent` is (0, 0, 0, 0). |

## Point
`public readonly record struct Point(int X, int Y)`

| Member | Meaning |
|---|---|
| `static Point Zero { get; }` | (0, 0). |

## Rectangle
`public readonly record struct Rectangle(int X, int Y, int Width, int Height)`

| Member | Meaning |
|---|---|
| `static Rectangle Empty { get; }` | All zero. |
| `int Left`, `Top`, `Right`, `Bottom` | `X`, `Y`, `X + Width`, `Y + Height`. |
| `Point Location { get; }` | (`X`, `Y`). |
| `Point Size { get; }` | (`Width`, `Height`). |
| `bool IsEmpty { get; }` | `true` when `Width` or `Height` is zero or negative. |
| `bool Contains(Point point)` | `true` when the point is inside; `Right` and `Bottom` are exclusive. |
| `bool Intersects(Rectangle other)` | `true` when the rectangles overlap by at least one pixel. |
| `static Rectangle Intersect(Rectangle first, Rectangle second)` | The overlap, or `Empty` when they don't overlap. |

## Bounding volumes
All are structs and none of their methods allocate. `Contains` returns a `ContainmentType`. `Intersects` with a `Plane` returns a `PlaneIntersectionType`, and `Intersects` with a `Ray` returns the distance along the ray to the hit, or `null`.

### ContainmentType
`public enum ContainmentType { Disjoint = 0, Contains = 1, Intersects = 2 }`

### PlaneIntersectionType
`public enum PlaneIntersectionType { Front = 0, Back = 1, Intersecting = 2 }`

`Front` is the side the plane normal points to.

### BoundingBox
`public readonly record struct BoundingBox(Vector3 Min, Vector3 Max)`

| Member | Meaning |
|---|---|
| `const int CornerCount = 8` | Size of the span `GetCorners` needs. |
| `Vector3 Center { get; }` | Midpoint of `Min` and `Max`. |
| `Vector3 Extents { get; }` | Half the size. |
| `static BoundingBox CreateFromPoints(ReadOnlySpan<Vector3> points)` | The smallest box holding the points. |
| `static BoundingBox CreateFromSphere(BoundingSphere sphere)` | The box around the sphere. |
| `static BoundingBox CreateMerged(BoundingBox first, BoundingBox second)` | The box around both. |
| `void GetCorners(Span<Vector3> corners)` | Writes the 8 corners. |
| `BoundingBox Transform(Matrix4x4 matrix)` | The axis-aligned box around the transformed box. |
| `Contains(Vector3)`, `Contains(BoundingBox)`, `Contains(BoundingSphere)` | Containment test. |
| `Intersects(BoundingBox)`, `Intersects(BoundingSphere)` | Overlap test. |
| `PlaneIntersectionType Intersects(Plane plane)` | Side of the plane. |
| `float? Intersects(Ray ray)` | Distance to the hit, or `null`. |

### BoundingSphere
`public readonly record struct BoundingSphere(Vector3 Center, float Radius)`

| Member | Meaning |
|---|---|
| `static BoundingSphere CreateFromPoints(ReadOnlySpan<Vector3> points)` | A sphere centered on the points' bounding box that reaches the farthest point. Not the smallest possible sphere. |
| `static BoundingSphere CreateFromBoundingBox(BoundingBox box)` | The sphere through the box corners. |
| `static BoundingSphere CreateMerged(BoundingSphere first, BoundingSphere second)` | The sphere around both. |
| `BoundingSphere Transform(Matrix4x4 matrix)` | Moves the center and scales the radius by the largest axis scale. |
| `Contains`, `Intersects` | As for `BoundingBox`. |

### BoundingFrustum
`public readonly struct BoundingFrustum : IEquatable<BoundingFrustum>`

Built from a view-projection matrix with a 0 to 1 depth range, as `Matrix4x4.CreatePerspectiveFieldOfView` produces.

| Member | Meaning |
|---|---|
| `const int PlaneCount = 6`, `const int CornerCount = 8` | Span sizes for `GetPlanes` and `GetCorners`. |
| `BoundingFrustum(Matrix4x4 viewProjection)` | Extracts the planes. |
| `Matrix4x4 Matrix { get; }` | The matrix passed in. |
| `Plane Near`, `Far`, `Left`, `Right`, `Top`, `Bottom` | Normalized planes whose normals point out of the frustum. |
| `void GetPlanes(Span<Plane> planes)` | Writes the six planes in the order above. |
| `void GetCorners(Span<Vector3> corners)` | Writes the eight corners. |
| `Contains(Vector3)`, `Contains(BoundingBox)`, `Contains(BoundingSphere)` | Plane-by-plane test. |
| `Intersects(BoundingBox)`, `Intersects(BoundingSphere)` | Plane-by-plane test. A box near a frustum corner can report an intersection while lying just outside. |
| `==`, `!=`, `Equals`, `GetHashCode` | Compare the matrices. |

### Ray
`public readonly record struct Ray(Vector3 Position, Vector3 Direction)`

| Member | Meaning |
|---|---|
| `float? Intersects(BoundingBox box)` | Distance to the hit, or `null`. |
| `float? Intersects(BoundingSphere sphere)` | Distance to the hit, or `null`. |
| `float? Intersects(Plane plane)` | Distance to the plane, or `null` when the ray is parallel to it or the plane is behind the ray. |
