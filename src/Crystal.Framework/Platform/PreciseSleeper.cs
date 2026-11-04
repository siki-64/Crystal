using System.Diagnostics;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Platform;

internal sealed unsafe class PreciseSleeper : IDisposable
{
    private const uint CreateWaitableTimerHighResolution = 0x00000002;

    private static readonly TimeSpan SpinThreshold = TimeSpan.FromMilliseconds(1);

    private HANDLE _timer = CreateWaitableTimerExW(null, null, CreateWaitableTimerHighResolution, TIMER_ALL_ACCESS);

    public void Sleep(TimeSpan duration)
    {
        var deadline = Stopwatch.GetTimestamp() + (long)(duration.TotalSeconds * Stopwatch.Frequency);
        var coarse = duration - SpinThreshold;
        if (coarse > TimeSpan.Zero)
        {
            if (_timer != HANDLE.NULL)
            {
                var dueTime = new LARGE_INTEGER { QuadPart = -coarse.Ticks };
                if (SetWaitableTimerEx(_timer, &dueTime, 0, null, null, null, 0)) WaitForSingleObject(_timer, INFINITE);
            }
            else
            {
                Thread.Sleep(coarse);
            }
        }

        while (Stopwatch.GetTimestamp() < deadline) Thread.SpinWait(32);
    }

    public void Dispose()
    {
        if (_timer == HANDLE.NULL) return;
        CloseHandle(_timer);
        _timer = HANDLE.NULL;
    }
}
