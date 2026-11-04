namespace Crystal.Platform;

internal static class SdlRuntime
{
    private static readonly Lock Gate = new();
    private static int _users;

    public static void Acquire()
    {
        lock (Gate)
        {
            if (_users == 0 && !Sdl.Init(Sdl.InitVideo | Sdl.InitGamepad)) throw Sdl.Failure("SDL_Init");
            _users++;
        }
    }

    public static void Release()
    {
        lock (Gate)
        {
            if (_users == 0) return;
            if (--_users == 0) Sdl.Quit();
        }
    }
}
