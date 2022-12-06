using _2022;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchMarking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Runner>();
        }
    }

    public class Runner
    {
        [Benchmark]
        public void Run()
        {
            var input = FileHelper.GetInputFromFile("Day5.txt");

            Day5Solution.PartTwo(input);
        }
    }
}