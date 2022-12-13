using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day13
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day13_Test.txt");

            Assert.AreEqual(0, Day13Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day13.txt");

            Assert.AreEqual(0, Day13Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day13_Test.txt");

            Assert.AreEqual(0, Day13Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day13.txt");

            Assert.AreEqual(0, Day13Solution.PartTwo(input));
        }

        [TestCase("[1]", "[2]", true)]
        [TestCase("[3]", "[2]", false)]
        public void PartOne_BothValuesAreIntegers(string left, string right, bool expected)
        {
            var pairInTheRightOrder = Day13Solution.PairInTheRightOrder(left, right);

            Assert.AreEqual(expected, pairInTheRightOrder);
        }
    }

    public class Day13Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var rowsList = input.Replace("\r", "").Split("\n").ToList();

            int pairsInTheRightOrderCount = 0;

            for (var index = 0; index < rowsList.Count; index+=3)
            {
                var leftRow = rowsList[index];
                var rightRow = rowsList[index+1];

                if (PairInTheRightOrder(leftRow, rightRow))
                {
                    pairsInTheRightOrderCount++;
                }
            }

            return 0;
        }

        public static bool PairInTheRightOrder(string leftRow, string rightRow)
        {
            throw new System.NotImplementedException();
        }

        [Benchmark]
        public static int PartTwo(string input)
        {
            //var heightMapLetters = input.Replace("\r", "").Split("\n").ToList();

            return 0;
        }
    }
}