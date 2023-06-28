using System.Linq;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day18
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day18_Test.txt");

            Assert.AreEqual(0, Day18Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day18.txt");

            Assert.AreEqual(0, Day18Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day18_Test.txt");

            Assert.AreEqual(0, Day18Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day18.txt");

            Assert.AreEqual(0, Day18Solution.PartTwo(input));
        }
    }

    public class Day18Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var rowsList = input.Replace("\r", "").Split("\n").ToList();

            return 0;
        }

        [Benchmark]
        public static int PartTwo(string input)
        {
            var rowsList = input.Replace("\r", "").Split("\n").ToList();

            return 0;
        }
    }
}