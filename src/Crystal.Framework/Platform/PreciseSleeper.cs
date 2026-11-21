using System.Diagnostics;
using Crystal.Interop;
using static Crystal.Interop.Windows;

namespace Crystal.Platform;

internal sealed unsafe class PreciseSleeper : IDisposable
{
    private static readonly TimeSpan SpinThreshold = TimeSpan.FromMilliseconds(1);

    private void* _timer = CreateWaitableTimerExW(null, null, CREATE_WAITABLE_TIMER_HIGH_RESOLUTION, TIMER_ALL_ACCESS);

    public void Sleep(TimeSpan duration) =>
        SleepUntil(Stopwatch.GetTimestamp() + (long)(duration.TotalSeconds * Stopwatch.Frequency));

    public void SleepUntil(long deadline)
    {
        var coarse = Stopwatch.GetElapsedTime(Stopwatch.GetTimestamp(), deadline) - SpinThreshold;
        if (coarse > TimeSpan.Zero)
        {
            if (_timer != null)
            {
                var dueTime = new LARGE_INTEGER { QuadPart = -coarse.Ticks };
                if (SetWaitableTimerEx(_timer, &dueTime, 0, null, null, null, 0) != FALSE) WaitForSingleObject(_timer, INFINITE);
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
        if (_timer == null) return;
        CloseHandle(_timer);
        _timer = null;
    }
}
