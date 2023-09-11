using System.Diagnostics;

namespace Foundation
{
    public class BenchmarkRunner
    {
        private readonly Stopwatch _stopwatch = new();

        public TimeSpan Run(Action action, int tries = 20)
        {
            long totalTicks = 0;
            for (int i = 0; i < tries; i++)
            {
                totalTicks += SimpleRun(action).Ticks;
            }
            return new TimeSpan(totalTicks / tries);
        }

        private TimeSpan SimpleRun(Action action)
        {
            _stopwatch.Restart();
            action();
            _stopwatch.Stop();
            return _stopwatch.Elapsed;
        }
    }
}
