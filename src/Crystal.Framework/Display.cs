using System.Runtime.InteropServices;
using Crystal.Platform;

namespace Crystal;

public sealed record Display(string Name, Rectangle Bounds, Rectangle WorkArea, bool IsPrimary, float ContentScale)
{
    public static unsafe IReadOnlyList<Display> GetAll()
    {
        SdlRuntime.Acquire();
        try
        {
            int count;
            var ids = Sdl.GetDisplays(&count);
            if (ids == null) throw Sdl.Failure("SDL_GetDisplays");
            try
            {
                var primary = Sdl.GetPrimaryDisplay();
                var displays = new Display[count];
                for (var index = 0; index < count; index++) displays[index] = FromId(ids[index], primary);
                return displays;
            }
            finally
            {
                Sdl.Free(ids);
            }
        }
        finally
        {
            SdlRuntime.Release();
        }
    }

    internal static unsafe Display FromId(uint id, uint primaryId)
    {
        SdlRect bounds, workArea;
        if (!Sdl.GetDisplayBounds(id, &bounds)) throw Sdl.Failure("SDL_GetDisplayBounds");
        if (!Sdl.GetDisplayUsableBounds(id, &workArea)) throw Sdl.Failure("SDL_GetDisplayUsableBounds");
        var name = Marshal.PtrToStringUTF8((nint)Sdl.GetDisplayName(id)) ?? string.Empty;
        return new Display(name, bounds.ToRectangle(), workArea.ToRectangle(), id == primaryId, Sdl.GetDisplayContentScale(id));
    }
}
