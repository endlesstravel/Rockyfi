using System;
using Love;

namespace RockyfiFactory
{
    class Util
    {
        static public float WatchTime(Action action)
        {
            var stopWatch = new System.Diagnostics.Stopwatch();
            double startTime = Timer.GetTime();
            var now = System.DateTime.Now; // <-- Value is copied into local
            stopWatch.Start();
            action();
            stopWatch.Stop();
            long delta = stopWatch.ElapsedMilliseconds;
            System.Console.WriteLine("" + stopWatch.ElapsedMilliseconds / 1000.0f
                + "   \t" + (Timer.GetTime() - startTime)
                + "   \t" + (System.DateTime.Now.Ticks - now.Ticks) / (float)System.TimeSpan.TicksPerSecond
                );
            return (System.DateTime.Now.Ticks - now.Ticks) / (float)System.TimeSpan.TicksPerSecond;
        }
    }

}