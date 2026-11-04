using Crystal.Platform;
using TerraFX.Interop.Windows;

namespace Crystal;

public sealed unsafe class GameWindow : IDisposable
{
    private SdlWindow _window;
    private string _title;
    private bool _allowUserResizing;

    internal GameWindow(GameOptions options)
    {
        _title = options.Title;
        _allowUserResizing = options.AllowUserResizing;
        if (options.HostWindowHandle != 0)
        {
            Handle = options.HostWindowHandle;
            IsHosted = true;
            return;
        }

        var flags = Sdl.WindowHighPixelDensity | Sdl.WindowHidden;
        if (options.AllowUserResizing) flags |= Sdl.WindowResizable;

        _window = Sdl.CreateWindow(options.Title, options.Width, options.Height, flags);
        if (_window.IsNull) throw Sdl.Failure("SDL_CreateWindow");

        Handle = Sdl.GetPointerProperty(Sdl.GetWindowProperties(_window), Sdl.WindowWin32HwndProperty, 0);
        if (Handle == 0) throw Sdl.Failure("SDL_GetPointerProperty(SDL.window.win32.hwnd)");
    }

    public nint Handle { get; }

    public bool IsHosted { get; }

    public string Title
    {
        get => _title;
        set
        {
            RequireOwned();
            if (!Sdl.SetWindowTitle(_window, value)) throw Sdl.Failure("SDL_SetWindowTitle");
            _title = value;
        }
    }

    public bool AllowUserResizing
    {
        get => _allowUserResizing;
        set
        {
            RequireOwned();
            if (!Sdl.SetWindowResizable(_window, value)) throw Sdl.Failure("SDL_SetWindowResizable");
            _allowUserResizing = value;
        }
    }

    public Point ClientSize
    {
        get
        {
            if (IsHosted)
            {
                RECT client;
                if (!Windows.GetClientRect((HWND)Handle, &client)) return Point.Zero;
                return new Point(client.right - client.left, client.bottom - client.top);
            }

            int width, height;
            if (!Sdl.GetWindowSizeInPixels(_window, &width, &height)) throw Sdl.Failure("SDL_GetWindowSizeInPixels");
            return new Point(width, height);
        }
    }

    public Point Position
    {
        get
        {
            RequireOwned();
            int x, y;
            if (!Sdl.GetWindowPosition(_window, &x, &y)) throw Sdl.Failure("SDL_GetWindowPosition");
            return new Point(x, y);
        }
        set
        {
            RequireOwned();
            if (!Sdl.SetWindowPosition(_window, value.X, value.Y)) throw Sdl.Failure("SDL_SetWindowPosition");
            Sdl.SyncWindow(_window);
        }
    }

    public void Resize(int width, int height)
    {
        RequireOwned();
        if (!Sdl.SetWindowSize(_window, width, height)) throw Sdl.Failure("SDL_SetWindowSize");
        Sdl.SyncWindow(_window);
    }

    public Display Display
    {
        get
        {
            RequireOwned();
            var id = Sdl.GetDisplayForWindow(_window);
            if (id == 0) throw Sdl.Failure("SDL_GetDisplayForWindow");
            return Display.FromId(id, Sdl.GetPrimaryDisplay());
        }
    }

    public bool IsMinimized => IsHosted ? Windows.IsIconic((HWND)Handle) : (Sdl.GetWindowFlags(_window) & Sdl.WindowMinimized) != 0;

    public bool IsTextInputEnabled
    {
        get => !IsHosted && Sdl.TextInputActive(_window);
        set
        {
            if (value == IsTextInputEnabled) return;
            RequireOwned();
            var succeeded = value ? Sdl.StartTextInput(_window) : Sdl.StopTextInput(_window);
            if (!succeeded) throw Sdl.Failure(value ? "SDL_StartTextInput" : "SDL_StopTextInput");
        }
    }

    public event EventHandler? ClientSizeChanged;

    public event EventHandler<TextInputEventArgs>? TextInput;

    internal bool HasTextInputHandlers => TextInput != null;

    internal bool IsHostForeground => Windows.GetForegroundWindow() == (HWND)Handle;

    internal void Show()
    {
        if (IsHosted) return;
        if (!Sdl.ShowWindow(_window)) throw Sdl.Failure("SDL_ShowWindow");
    }

    internal float PixelDensity => IsHosted ? 1f : Sdl.GetWindowPixelDensity(_window) is var density && density > 0 ? density : 1f;

    internal void SetRelativeMouseMode(bool enabled)
    {
        RequireOwned();
        if (!Sdl.SetWindowRelativeMouseMode(_window, enabled)) throw Sdl.Failure("SDL_SetWindowRelativeMouseMode");
    }

    internal void WarpMouse(int x, int y)
    {
        RequireOwned();
        var density = PixelDensity;
        Sdl.WarpMouseInWindow(_window, x / density, y / density);
    }

    internal void OnClientSizeChanged() => ClientSizeChanged?.Invoke(this, EventArgs.Empty);

    internal void OnTextInput(char character) => TextInput?.Invoke(this, new TextInputEventArgs(character));

    public void Dispose()
    {
        if (IsHosted) return;
        _window.Dispose();
        _window = default;
    }

    private void RequireOwned()
    {
        if (IsHosted) throw new InvalidOperationException("The host owns this window; Crystal only renders into it.");
    }
}
