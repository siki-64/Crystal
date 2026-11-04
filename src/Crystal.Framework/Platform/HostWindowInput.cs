using Crystal.Input;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.Windows.VK;

namespace Crystal.Platform;

internal static unsafe class HostWindowInput
{
    private static readonly (Keys Key, int VirtualKey)[] VirtualKeyByKey = BuildVirtualKeyByKey();
    private static bool _hasCursorPosition;

    public static void Poll(GameWindow window)
    {
        if (!window.IsHostForeground)
        {
            Keyboard.Reset();
            _hasCursorPosition = false;
            Mouse.State = Mouse.State with
            {
                LeftButton = ButtonState.Released,
                MiddleButton = ButtonState.Released,
                RightButton = ButtonState.Released,
                XButton1 = ButtonState.Released,
                XButton2 = ButtonState.Released,
            };
            return;
        }

        foreach (var (key, virtualKey) in VirtualKeyByKey) Keyboard.SetKey(key, IsDown(virtualKey));

        POINT cursor;
        if (!Windows.GetCursorPos(&cursor) || !Windows.ScreenToClient((HWND)window.Handle, &cursor)) return;
        if (_hasCursorPosition) Mouse.Move(cursor.x - Mouse.State.X, cursor.y - Mouse.State.Y);
        _hasCursorPosition = true;
        Mouse.State = Mouse.State with
        {
            X = cursor.x,
            Y = cursor.y,
            LeftButton = ButtonOf(VK_LBUTTON),
            MiddleButton = ButtonOf(VK_MBUTTON),
            RightButton = ButtonOf(VK_RBUTTON),
            XButton1 = ButtonOf(VK_XBUTTON1),
            XButton2 = ButtonOf(VK_XBUTTON2),
        };
    }

    private static bool IsDown(int virtualKey) => (Windows.GetAsyncKeyState(virtualKey) & 0x8000) != 0;

    private static ButtonState ButtonOf(int virtualKey) => IsDown(virtualKey) ? ButtonState.Pressed : ButtonState.Released;

    private static (Keys Key, int VirtualKey)[] BuildVirtualKeyByKey()
    {
        var pairs = new List<(Keys, int)>();
        for (var index = 0; index < 26; index++) pairs.Add((Keys.A + index, 'A' + index));
        for (var index = 0; index < 9; index++) pairs.Add((Keys.D1 + index, '1' + index));
        pairs.Add((Keys.D0, '0'));
        for (var index = 0; index < 12; index++) pairs.Add((Keys.F1 + index, VK_F1 + index));
        for (var index = 0; index < 9; index++) pairs.Add((Keys.NumPad1 + index, VK_NUMPAD1 + index));
        pairs.AddRange(
        [
            (Keys.NumPad0, VK_NUMPAD0), (Keys.Enter, VK_RETURN), (Keys.Escape, VK_ESCAPE), (Keys.Back, VK_BACK),
            (Keys.Tab, VK_TAB), (Keys.Space, VK_SPACE), (Keys.OemMinus, VK_OEM_MINUS), (Keys.OemPlus, VK_OEM_PLUS),
            (Keys.OemOpenBrackets, VK_OEM_4), (Keys.OemCloseBrackets, VK_OEM_6), (Keys.OemPipe, VK_OEM_5),
            (Keys.OemSemicolon, VK_OEM_1), (Keys.OemQuotes, VK_OEM_7), (Keys.OemTilde, VK_OEM_3),
            (Keys.OemComma, VK_OEM_COMMA), (Keys.OemPeriod, VK_OEM_PERIOD), (Keys.OemQuestion, VK_OEM_2),
            (Keys.OemBackslash, VK_OEM_102), (Keys.CapsLock, VK_CAPITAL), (Keys.Insert, VK_INSERT), (Keys.Home, VK_HOME),
            (Keys.PageUp, VK_PRIOR), (Keys.Delete, VK_DELETE), (Keys.End, VK_END), (Keys.PageDown, VK_NEXT),
            (Keys.Right, VK_RIGHT), (Keys.Left, VK_LEFT), (Keys.Down, VK_DOWN), (Keys.Up, VK_UP),
            (Keys.Divide, VK_DIVIDE), (Keys.Multiply, VK_MULTIPLY), (Keys.Subtract, VK_SUBTRACT), (Keys.Add, VK_ADD),
            (Keys.Decimal, VK_DECIMAL), (Keys.LeftControl, VK_LCONTROL), (Keys.RightControl, VK_RCONTROL),
            (Keys.LeftShift, VK_LSHIFT), (Keys.RightShift, VK_RSHIFT), (Keys.LeftAlt, VK_LMENU), (Keys.RightAlt, VK_RMENU),
            (Keys.LeftWindows, VK_LWIN), (Keys.RightWindows, VK_RWIN), (Keys.Apps, VK_APPS),
        ]);
        return [.. pairs];
    }
}
