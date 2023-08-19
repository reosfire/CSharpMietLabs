using System.Diagnostics;

namespace Lab2
{
	internal class BenchmarkRunner
	{
		private Stopwatch _stopwatch = new Stopwatch();

		public TimeSpan Run(Action action, int tries = 20)
		{
			long totalTiks = 0;
			for (int i = 0; i < tries; i++)
			{
				totalTiks += SimpleRun(action).Ticks;
			}
			return new TimeSpan(totalTiks / tries);
		}

		public TimeSpan SimpleRun(Action action)
		{
            _stopwatch.Restart();
            action();
            _stopwatch.Stop();
            return _stopwatch.Elapsed;
        }
	}
}
