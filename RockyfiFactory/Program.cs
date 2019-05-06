using System;
using Love;
using Rockyfi.Expr;
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
    


    class Program
    {
        static void Main(string[] args)
        {
            Boot.Init(new BootConfig
            {
                WindowResizable = true,
            });
            Boot.Run(new SceneHugeList());
        }

        //static void Main(string[] args)
        //{
        //    Boot.Init(new BootConfig
        //    {
        //        WindowResizable = true,
        //    });
        //    Boot.Run(new SceneNormalXML());
        //}

        //static void Main(string[] args)
        //{
        //    Boot.Init(new BootConfig
        //    {

        //    });
        //    Boot.Run(new SceneTestFactory());
        //}

        //static void Main(string[] args)
        //{
        //    new TestProgram().Test();
        //}
    }

}