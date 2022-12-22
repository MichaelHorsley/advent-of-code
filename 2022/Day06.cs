using System.Linq;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day06
    {
        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
        public void PartOne_Test(string input, int expected)
        {
            Assert.AreEqual(expected, Day06Solution.PartOne(input));
        }

        [Benchmark]
        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day06.txt");

            Assert.AreEqual(1647, Day06Solution.PartOne(input));
        }

        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 23)]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
        public void PartTwo_Test(string input, int expected)
        {
            Assert.AreEqual(expected, Day06Solution.PartTwo(input));
        }

        [Benchmark]
        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day06.txt");

            Assert.AreEqual(2447, Day06Solution.PartTwo(input));
        }
    }

    public class Day06Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            const int lengthToTake = 4;

            return FindStartOfInputWithoutDuplicateCharactersHashSetApproach(input, lengthToTake);
        }

        [Benchmark]
        public static int PartTwo(string input)
        {
            const int lengthToTake = 14;

            return FindStartOfInputWithoutDuplicateCharactersHashSetApproach(input, lengthToTake);
        }

        [Benchmark]
        private static int FindStartOfInputWithoutDuplicateCharactersHashSetApproach(string input, int lengthToTake)
        {
            for (var position = 0; position < input.Length; position++)
            {
                var substring = input.Substring(position, lengthToTake).ToHashSet();

                if (substring.Count == lengthToTake)
                {
                    return position + lengthToTake;
                }
            }

            return 0;
        }
    }
}