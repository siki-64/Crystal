using System.Runtime.InteropServices;

namespace Crystal.Platform;

internal static unsafe partial class Sdl
{
    private const string Library = "SDL3";

    public const uint InitVideo = 0x20;
    public const uint InitGamepad = 0x2000;

    public const ulong WindowHidden = 0x8;
    public const ulong WindowResizable = 0x20;
    public const ulong WindowMinimized = 0x40;
    public const ulong WindowHighPixelDensity = 0x2000;

    public const string WindowWin32HwndProperty = "SDL.window.win32.hwnd";
    public const string JoystickAllowBackgroundEventsHint = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";

    [LibraryImport(Library, EntryPoint = "SDL_Init")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool Init(uint flags);

    [LibraryImport(Library, EntryPoint = "SDL_Quit")]
    public static partial void Quit();

    [LibraryImport(Library, EntryPoint = "SDL_SetHint", StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SetHint(string name, string value);

    [LibraryImport(Library, EntryPoint = "SDL_GetError")]
    private static partial byte* GetErrorUtf8();

    [LibraryImport(Library, EntryPoint = "SDL_CreateWindow", StringMarshalling = StringMarshalling.Utf8)]
    public static partial SdlWindow CreateWindow(string title, int width, int height, ulong flags);

    [LibraryImport(Library, EntryPoint = "SDL_DestroyWindow")]
    public static partial void DestroyWindow(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_ShowWindow")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ShowWindow(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_SetWindowTitle", StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SetWindowTitle(SdlWindow window, string title);

    [LibraryImport(Library, EntryPoint = "SDL_SetWindowResizable")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SetWindowResizable(SdlWindow window, [MarshalAs(UnmanagedType.U1)] bool resizable);

    [LibraryImport(Library, EntryPoint = "SDL_StartTextInput")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool StartTextInput(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_StopTextInput")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool StopTextInput(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_TextInputActive")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool TextInputActive(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_GetWindowSizeInPixels")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool GetWindowSizeInPixels(SdlWindow window, int* width, int* height);

    [LibraryImport(Library, EntryPoint = "SDL_GetWindowPixelDensity")]
    public static partial float GetWindowPixelDensity(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_GetWindowFlags")]
    public static partial ulong GetWindowFlags(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_GetWindowID")]
    public static partial uint GetWindowId(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_GetWindowProperties")]
    public static partial uint GetWindowProperties(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_GetPointerProperty", StringMarshalling = StringMarshalling.Utf8)]
    public static partial nint GetPointerProperty(uint properties, string name, nint defaultValue);

    [LibraryImport(Library, EntryPoint = "SDL_GetWindowPosition")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool GetWindowPosition(SdlWindow window, int* x, int* y);

    [LibraryImport(Library, EntryPoint = "SDL_SetWindowPosition")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SetWindowPosition(SdlWindow window, int x, int y);

    [LibraryImport(Library, EntryPoint = "SDL_SetWindowSize")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SetWindowSize(SdlWindow window, int width, int height);

    [LibraryImport(Library, EntryPoint = "SDL_SyncWindow")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SyncWindow(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_GetDisplayForWindow")]
    public static partial uint GetDisplayForWindow(SdlWindow window);

    [LibraryImport(Library, EntryPoint = "SDL_GetDisplays")]
    public static partial uint* GetDisplays(int* count);

    [LibraryImport(Library, EntryPoint = "SDL_GetPrimaryDisplay")]
    public static partial uint GetPrimaryDisplay();

    [LibraryImport(Library, EntryPoint = "SDL_GetDisplayName")]
    public static partial byte* GetDisplayName(uint displayId);

    [LibraryImport(Library, EntryPoint = "SDL_GetDisplayBounds")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool GetDisplayBounds(uint displayId, SdlRect* rectangle);

    [LibraryImport(Library, EntryPoint = "SDL_GetDisplayUsableBounds")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool GetDisplayUsableBounds(uint displayId, SdlRect* rectangle);

    [LibraryImport(Library, EntryPoint = "SDL_GetDisplayContentScale")]
    public static partial float GetDisplayContentScale(uint displayId);

    [LibraryImport(Library, EntryPoint = "SDL_free")]
    public static partial void Free(void* memory);

    [LibraryImport(Library, EntryPoint = "SDL_PollEvent")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool PollEvent(SdlEvent* sdlEvent);

    [LibraryImport(Library, EntryPoint = "SDL_PushEvent")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool PushEvent(SdlEvent* sdlEvent);

    [LibraryImport(Library, EntryPoint = "SDL_ShowCursor")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ShowCursor();

    [LibraryImport(Library, EntryPoint = "SDL_HideCursor")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool HideCursor();

    [LibraryImport(Library, EntryPoint = "SDL_SetWindowRelativeMouseMode")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SetWindowRelativeMouseMode(SdlWindow window, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(Library, EntryPoint = "SDL_WarpMouseInWindow")]
    public static partial void WarpMouseInWindow(SdlWindow window, float x, float y);

    [LibraryImport(Library, EntryPoint = "SDL_OpenGamepad")]
    public static partial SdlGamepad OpenGamepad(uint joystickId);

    [LibraryImport(Library, EntryPoint = "SDL_CloseGamepad")]
    public static partial void CloseGamepad(SdlGamepad gamepad);

    [LibraryImport(Library, EntryPoint = "SDL_SetGamepadPlayerIndex")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SetGamepadPlayerIndex(SdlGamepad gamepad, int playerIndex);

    [LibraryImport(Library, EntryPoint = "SDL_GetGamepadButton")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool GetGamepadButton(SdlGamepad gamepad, SdlGamepadButton button);

    [LibraryImport(Library, EntryPoint = "SDL_GetGamepadAxis")]
    public static partial short GetGamepadAxis(SdlGamepad gamepad, SdlGamepadAxis axis);

    [LibraryImport(Library, EntryPoint = "SDL_AttachVirtualJoystick")]
    public static partial uint AttachVirtualJoystick(SdlVirtualJoystickDescription* description);

    [LibraryImport(Library, EntryPoint = "SDL_DetachVirtualJoystick")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool DetachVirtualJoystick(uint joystickId);

    [LibraryImport(Library, EntryPoint = "SDL_OpenJoystick")]
    public static partial SdlJoystick OpenJoystick(uint joystickId);

    [LibraryImport(Library, EntryPoint = "SDL_CloseJoystick")]
    public static partial void CloseJoystick(SdlJoystick joystick);

    [LibraryImport(Library, EntryPoint = "SDL_SetJoystickVirtualAxis")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SetJoystickVirtualAxis(SdlJoystick joystick, int axis, short value);

    [LibraryImport(Library, EntryPoint = "SDL_SetJoystickVirtualButton")]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SetJoystickVirtualButton(SdlJoystick joystick, int button, [MarshalAs(UnmanagedType.U1)] bool down);

    public static string GetError() => Marshal.PtrToStringUTF8((nint)GetErrorUtf8()) ?? string.Empty;

    public static SdlException Failure(string function) => new($"{function} failed: {GetError()}");
}

internal sealed class SdlException(string message) : Exception(message);

[StructLayout(LayoutKind.Sequential)]
internal struct SdlRect
{
    public int X;
    public int Y;
    public int Width;
    public int Height;

    public readonly Rectangle ToRectangle() => new(X, Y, Width, Height);
}

internal readonly struct SdlWindow(nint handle)
{
    private readonly nint _handle = handle;

    public bool IsNull => _handle == 0;

    public void Dispose()
    {
        if (!IsNull) Sdl.DestroyWindow(this);
    }
}

internal readonly struct SdlGamepad(nint handle)
{
    private readonly nint _handle = handle;

    public bool IsNull => _handle == 0;

    public void Dispose()
    {
        if (!IsNull) Sdl.CloseGamepad(this);
    }
}

internal readonly struct SdlJoystick(nint handle)
{
    private readonly nint _handle = handle;

    public bool IsNull => _handle == 0;

    public void Dispose()
    {
        if (!IsNull) Sdl.CloseJoystick(this);
    }
}

internal enum SdlEventType : uint
{
    Quit = 0x100,
    WindowPixelSizeChanged = 0x207,
    WindowMinimized = 0x209,
    WindowRestored = 0x20B,
    WindowFocusGained = 0x20E,
    WindowFocusLost = 0x20F,
    WindowCloseRequested = 0x210,
    KeyDown = 0x300,
    KeyUp = 0x301,
    TextInput = 0x303,
    MouseMotion = 0x400,
    MouseButtonDown = 0x401,
    MouseButtonUp = 0x402,
    MouseWheel = 0x403,
    GamepadAdded = 0x653,
    GamepadRemoved = 0x654,
}

internal enum SdlGamepadButton
{
    South,
    East,
    West,
    North,
    Back,
    Guide,
    Start,
    LeftStick,
    RightStick,
    LeftShoulder,
    RightShoulder,
    DPadUp,
    DPadDown,
    DPadLeft,
    DPadRight,
    Count = 26,
}

internal enum SdlGamepadAxis
{
    LeftX,
    LeftY,
    RightX,
    RightY,
    LeftTrigger,
    RightTrigger,
    Count,
}

internal static class SdlMouseButton
{
    public const byte Left = 1;
    public const byte Middle = 2;
    public const byte Right = 3;
    public const byte X1 = 4;
    public const byte X2 = 5;
}

[StructLayout(LayoutKind.Explicit, Size = 128)]
internal struct SdlEvent
{
    [FieldOffset(0)] public SdlEventType Type;
    [FieldOffset(0)] public SdlWindowEvent Window;
    [FieldOffset(0)] public SdlKeyboardEvent Key;
    [FieldOffset(0)] public SdlTextInputEvent Text;
    [FieldOffset(0)] public SdlMouseMotionEvent Motion;
    [FieldOffset(0)] public SdlMouseButtonEvent Button;
    [FieldOffset(0)] public SdlMouseWheelEvent Wheel;
    [FieldOffset(0)] public SdlGamepadDeviceEvent GamepadDevice;
}

[StructLayout(LayoutKind.Sequential)]
internal struct SdlWindowEvent
{
    public SdlEventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
    public int Data1;
    public int Data2;
}

[StructLayout(LayoutKind.Sequential)]
internal struct SdlKeyboardEvent
{
    public SdlEventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
    public uint Which;
    public uint Scancode;
    public uint Key;
    public ushort Modifiers;
    public ushort Raw;
    public byte Down;
    public byte Repeat;
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SdlTextInputEvent
{
    public SdlEventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
    public byte* Text;
}

[StructLayout(LayoutKind.Sequential)]
internal struct SdlMouseMotionEvent
{
    public SdlEventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
    public uint Which;
    public uint ButtonFlags;
    public float X;
    public float Y;
    public float RelativeX;
    public float RelativeY;
}

[StructLayout(LayoutKind.Sequential)]
internal struct SdlMouseButtonEvent
{
    public SdlEventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
    public uint Which;
    public byte Button;
    public byte Down;
    public byte Clicks;
    public byte Padding;
    public float X;
    public float Y;
}

[StructLayout(LayoutKind.Sequential)]
internal struct SdlMouseWheelEvent
{
    public SdlEventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
    public uint Which;
    public float X;
    public float Y;
    public int Direction;
    public float MouseX;
    public float MouseY;
    public int IntegerX;
    public int IntegerY;
}

[StructLayout(LayoutKind.Sequential)]
internal struct SdlGamepadDeviceEvent
{
    public SdlEventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint Which;
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SdlVirtualJoystickDescription
{
    public const ushort GamepadType = 1;

    public uint Version;
    public ushort Type;
    public ushort Padding;
    public ushort VendorId;
    public ushort ProductId;
    public ushort AxisCount;
    public ushort ButtonCount;
    public ushort BallCount;
    public ushort HatCount;
    public ushort TouchpadCount;
    public ushort SensorCount;
    public uint Padding2;
    public uint ButtonMask;
    public uint AxisMask;
    public byte* Name;
    public void* Touchpads;
    public void* Sensors;
    public void* UserData;
    public void* Update;
    public void* SetPlayerIndex;
    public void* Rumble;
    public void* RumbleTriggers;
    public void* SetLed;
    public void* SendEffect;
    public void* SetSensorsEnabled;
    public void* Cleanup;
}
