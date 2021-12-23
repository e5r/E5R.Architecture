using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;

namespace E5R.Architecture.Core.Benchmarks
{
    [MarkdownExporter]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(NumeralSystem.Arabic)]
    public class CheckerUtilBenchmarks
    {
        private object varName = null;

        [Params(1000, 10000)]
        public int N;

        [Benchmark]
        public void WithChecker()
        {
            for (int n = 0; n <= N; n++)
            {
                try
                {
                    Checker.NotNullArgument(varName, nameof(varName));
                }
                catch (Exception)
                {
                }
            }
        }

        [Benchmark]
        public void WithCheckerAndExpression()
        {
            for (int n = 0; n <= N; n++)
            {
                try
                {
                    Checker.NotNullArgument(varName, () => varName);
                }
                catch (Exception)
                {
                }
            }
        }

        [Benchmark(Baseline = true)]
        public void WithoutChecker()
        {
            for (int n = 0; n <= N; n++)
            {
                try
                {
                    if (varName == null)
                    {
                        throw new ArgumentNullException(nameof(varName));
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
