using System;
using System.Diagnostics;

namespace SqlServerVsRedis
{
    public class StopwatchTimer : IDisposable
    {
        private readonly Action<int> callback;
        private readonly Stopwatch stopwatch;
        public StopwatchTimer(Action<int> callback)
        {
            this.callback = callback;
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            callback((int)stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}