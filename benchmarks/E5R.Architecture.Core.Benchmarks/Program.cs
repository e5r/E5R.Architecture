using BenchmarkDotNet.Running;

namespace E5R.Architecture.Core.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<CheckerUtilBenchmarks>();
        }
    }
}
