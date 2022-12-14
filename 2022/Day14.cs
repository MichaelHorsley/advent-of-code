using System.Linq;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day14
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day14_Test.txt");

            Assert.AreEqual(0, Day14Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day14.txt");

            Assert.AreEqual(0, Day14Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day14_Test.txt");

            Assert.AreEqual(0, Day14Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day14.txt");

            Assert.AreEqual(0, Day14Solution.PartTwo(input));
        }
    }

    public class Day14Solution
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