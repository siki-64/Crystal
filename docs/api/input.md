# Crystal.Input

Keyboard, mouse and gamepad state. The game loop processes window events before each tick; `GetState` returns a snapshot that only changes between ticks. Reading input state allocates no managed memory. Call it from the thread that runs `Game.Run`.

## Keyboard
`public static class Keyboard`

| Member | Meaning |
|---|---|
| `static KeyboardState GetState()` | The keys held at the start of this tick. |

## KeyboardState
`public struct KeyboardState : IEquatable<KeyboardState>`

| Member | Meaning |
|---|---|
| `const int KeyCount = 512` | Size of the key range. |
| `KeyboardState(params ReadOnlySpan<Keys> pressedKeys)` | A state with these keys held, for tests and replays. |
| `bool IsKeyDown(Keys key)` | `true` while held. `false` for values outside 0 to 511. |
| `bool IsKeyUp(Keys key)` | `!IsKeyDown(key)`. |
| `int GetPressedKeyCount()` | Number of held keys. |
| `int GetPressedKeys(Span<Keys> destination)` | Writes the held keys in ascending order, as many as fit, and returns how many it wrote. |
| `Equals`, `GetHashCode`, `==`, `!=` | Compare the held keys. |

## Keys
`public enum Keys`

Physical key positions with USB HID usage values, so `Keys.W` is the key left of `E` on any layout.

| Group | Members |
|---|---|
| Letters | `A = 4` … `Z = 29` |
| Digits | `D1 = 30` … `D9 = 38`, `D0 = 39` |
| Editing | `Enter = 40`, `Escape = 41`, `Back = 42`, `Tab = 43`, `Space = 44` |
| Punctuation | `OemMinus = 45`, `OemPlus = 46`, `OemOpenBrackets = 47`, `OemCloseBrackets = 48`, `OemPipe = 49`, `OemNonUsHash = 50`, `OemSemicolon = 51`, `OemQuotes = 52`, `OemTilde = 53`, `OemComma = 54`, `OemPeriod = 55`, `OemQuestion = 56`, `OemBackslash = 100` |
| Locks | `CapsLock = 57`, `Scroll = 71`, `NumLock = 83` |
| Function | `F1 = 58` … `F12 = 69`, `F13 = 104` … `F24 = 115` |
| System | `PrintScreen = 70`, `Pause = 72`, `Apps = 101` |
| Navigation | `Insert = 73`, `Home = 74`, `PageUp = 75`, `Delete = 76`, `End = 77`, `PageDown = 78`, `Right = 79`, `Left = 80`, `Down = 81`, `Up = 82` |
| Numeric keypad | `Divide = 84`, `Multiply = 85`, `Subtract = 86`, `Add = 87`, `NumPadEnter = 88`, `NumPad1 = 89` … `NumPad9 = 97`, `NumPad0 = 98`, `Decimal = 99` |
| Media | `VolumeMute = 127`, `VolumeUp = 128`, `VolumeDown = 129`, `MediaNextTrack = 267`, `MediaPreviousTrack = 268`, `MediaStop = 269`, `MediaPlayPause = 271` |
| Modifiers | `LeftControl = 224`, `LeftShift = 225`, `LeftAlt = 226`, `LeftWindows = 227`, `RightControl = 228`, `RightShift = 229`, `RightAlt = 230`, `RightWindows = 231` |

`None = 0` is no key.

## Mouse
`public static class Mouse`

| Member | Meaning |
|---|---|
| `const int WheelDelta = 120` | Scroll value per wheel notch. |
| `static MouseState GetState()` | The mouse at the start of this tick. |
| `static bool IsRelativeMode { get; set; }` | While `true` the cursor is hidden and held in the window, `X` and `Y` stop changing, and only `RelativeX` and `RelativeY` move. Throws `InvalidOperationException` before a `Game` exists or for a hosted window. |
| `static void SetPosition(int x, int y)` | Moves the cursor to a point in the client area and updates the current state. Throws `InvalidOperationException` before a `Game` exists. |

`Game.IsMouseVisible` shows or hides the cursor over the window.

## MouseState
`public readonly record struct MouseState(int X, int Y, int ScrollWheelValue, ButtonState LeftButton, ButtonState MiddleButton, ButtonState RightButton, ButtonState XButton1, ButtonState XButton2, int HorizontalScrollWheelValue = 0, int RelativeX = 0, int RelativeY = 0)`

| Member | Meaning |
|---|---|
| `int X`, `int Y` | Cursor position in back buffer pixels, relative to the client area. |
| `int ScrollWheelValue` | Accumulated vertical scrolling since the game started, `WheelDelta` per notch, positive away from the user. |
| `int HorizontalScrollWheelValue` | Accumulated horizontal scrolling, `WheelDelta` per notch, positive to the right. |
| `int RelativeX`, `int RelativeY` | Accumulated mouse motion since the game started, in back buffer pixels, rounded from the device's sub-pixel motion. It keeps counting in relative mode and at the window edge. A hosted window counts cursor movement instead. |
| `LeftButton`, `MiddleButton`, `RightButton`, `XButton1`, `XButton2` | Button states. |
| `Point Position { get; }` | (`X`, `Y`). |

## ButtonState
`public enum ButtonState { Released = 0, Pressed = 1 }`

## GamePad
`public static class GamePad`

| Member | Meaning |
|---|---|
| `const int MaximumGamePadCount = 8` | Player indices 0 to 7. |
| `const float LeftThumbDeadZone` | 7849 / 32767 (about 0.24). |
| `const float RightThumbDeadZone` | 8689 / 32767 (about 0.27). |
| `const float TriggerThreshold` | 30 / 255 (about 0.12). |
| `static GamePadState GetState(int playerIndex, GamePadDeadZone deadZone = GamePadDeadZone.IndependentAxes)` | The gamepad at that index. Throws `ArgumentOutOfRangeException` outside 0 to 7. A disconnected index returns a state with `IsConnected` `false`. |

Gamepads are read through SDL's gamepad database. A connected gamepad takes the lowest free index and keeps it until it disconnects. Face buttons use Xbox names, so `A` is the bottom button.

**Dead zones.** Values inside the dead zone become 0 and the rest is rescaled to reach 1 at full deflection. Triggers use `TriggerThreshold` in both stick modes. `Buttons.LeftTrigger` and `RightTrigger` are set while the trigger is above 0 after the dead zone, so with `None` any pressure sets them.

## GamePadDeadZone
`public enum GamePadDeadZone { None = 0, IndependentAxes = 1, Circular = 2 }`

| Member | Meaning |
|---|---|
| `None` | Raw values. |
| `IndependentAxes` | Applies the dead zone to each stick axis on its own. |
| `Circular` | Applies the dead zone to the stick's length and keeps its direction. |

## GamePadState
`public readonly record struct GamePadState(bool IsConnected, Buttons PressedButtons, GamePadThumbSticks ThumbSticks, GamePadTriggers Triggers)`

| Member | Meaning |
|---|---|
| `bool IsConnected` | `false` when no gamepad has this index. |
| `Buttons PressedButtons` | Held buttons as flags. |
| `GamePadThumbSticks ThumbSticks` | Stick positions. |
| `GamePadTriggers Triggers` | Trigger positions. |
| `GamePadButtons Buttons { get; }` | The buttons as `ButtonState` properties. |
| `GamePadDPad DPad { get; }` | The directional pad. |
| `bool IsButtonDown(Buttons buttons)` | `true` when all given buttons are held. `false` for `None`. |
| `bool IsButtonUp(Buttons buttons)` | `true` when none of the given buttons is held. |

## Buttons
`[Flags] public enum Buttons`

`None = 0`, `DPadUp = 1`, `DPadDown = 2`, `DPadLeft = 4`, `DPadRight = 8`, `Start = 16`, `Back = 32`, `LeftStick = 64`, `RightStick = 128`, `LeftShoulder = 256`, `RightShoulder = 512`, `BigButton = 2048`, `A = 4096`, `B = 8192`, `X = 16384`, `Y = 32768`, `RightTrigger = 4194304`, `LeftTrigger = 8388608`.

`LeftStick` and `RightStick` are the stick clicks. `BigButton` is the guide button.

## GamePadButtons
`public readonly record struct GamePadButtons(Buttons Pressed)`

`A`, `B`, `X`, `Y`, `Back`, `Start`, `BigButton`, `LeftShoulder`, `RightShoulder`, `LeftStick` and `RightStick` are `ButtonState` properties read from `Pressed`.

## GamePadDPad
`public readonly record struct GamePadDPad(ButtonState Up, ButtonState Down, ButtonState Left, ButtonState Right)`

## GamePadThumbSticks
`public readonly record struct GamePadThumbSticks(Vector2 Left, Vector2 Right)`

Each axis runs from -1 to 1, with up positive.

## GamePadTriggers
`public readonly record struct GamePadTriggers(float Left, float Right)`

Each runs from 0 to 1.

## InputFrame
`public sealed class InputFrame`

This tick's and the previous tick's keyboard, mouse and gamepad state, for edges and movement. Call `Update` once per tick before reading it.

| Member | Meaning |
|---|---|
| `InputFrame(int gamePadIndex = 0)` | Tracks the gamepad at that index. Throws `ArgumentOutOfRangeException` outside 0 to 7. |
| `int GamePadIndex { get; }` | The tracked gamepad. |
| `void Update()` | Reads `Keyboard`, `Mouse` and `GamePad` and makes the old state the previous one. |
| `void Advance(KeyboardState keyboard, MouseState mouse, GamePadState gamePad)` | The same with given states, for tests and replays. |
| `Keyboard`, `PreviousKeyboard`, `Mouse`, `PreviousMouse`, `GamePad`, `PreviousGamePad` | The two snapshots. |
| `Point MouseMovement { get; }` | Cursor movement since the previous tick, in back buffer pixels. |
| `Point RelativeMovement { get; }` | Mouse motion since the previous tick, also in relative mode. |
| `float ScrollNotches { get; }`, `float HorizontalScrollNotches { get; }` | Wheel notches since the previous tick. |
| `bool IsKeyPressed(Keys key)`, `bool IsKeyReleased(Keys key)` | `true` on the tick the key went down or up. |
| `bool IsMouseButtonDown(MouseButton button)`, `IsMouseButtonPressed`, `IsMouseButtonReleased` | Held, and the tick it went down or up. |
| `bool IsButtonPressed(Buttons buttons)`, `bool IsButtonReleased(Buttons buttons)` | The tick all given gamepad buttons became held, or stopped being held. |
| `bool IsDown(InputBinding binding)`, `bool WasDown(InputBinding binding)` | The binding held this tick, or the previous tick. |

## MouseButton
`public enum MouseButton { Left = 0, Middle = 1, Right = 2, XButton1 = 3, XButton2 = 4 }`

## InputBinding
`public readonly record struct InputBinding`

One key, mouse button or gamepad button. `Keys`, `MouseButton` and `Buttons` convert to it implicitly.

| Member | Meaning |
|---|---|
| `InputBindingKind Kind { get; }` | `None`, `Key`, `MouseButton` or `GamePadButton`. `default` is `None`. |
| `Keys Key`, `MouseButton MouseButton`, `Buttons GamePadButton` | The bound input; only the one matching `Kind` is used. |
| `static FromKey(Keys)`, `static FromMouseButton(MouseButton)`, `static FromGamePadButton(Buttons)` | Create a binding. A gamepad binding with several flags needs all of them held. |

## InputMap<TAction>
`public sealed class InputMap<TAction> where TAction : struct, Enum`

Binds game-defined actions to any number of inputs.

| Member | Meaning |
|---|---|
| `void Bind(TAction action, InputBinding binding)` | Adds a binding; binding the same input twice does nothing. Throws `ArgumentException` for a `None` binding. |
| `bool Unbind(TAction action, InputBinding binding)` | Removes it and returns whether it was bound. |
| `void Clear(TAction action)` | Removes every binding of the action. |
| `IReadOnlyList<InputBinding> BindingsOf(TAction action)` | The action's bindings in the order they were added. |
| `bool IsDown(InputFrame frame, TAction action)` | `true` while any binding is held. |
| `bool WasPressed(InputFrame frame, TAction action)`, `bool WasReleased(InputFrame frame, TAction action)` | `true` on the tick the action as a whole went down or up, so switching between two held bindings is no edge. |
