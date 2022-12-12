using System.Linq;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day12
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day12_Test.txt");

            Assert.AreEqual(0, Day12Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day12.txt");

            Assert.AreEqual(0, Day12Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day12_Test.txt");

            Assert.AreEqual(0, Day12Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day12.txt");

            Assert.AreEqual(0, Day12Solution.PartTwo(input));
        }
    }

    public class Day12Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var heightMapLetters = input.Replace("\r", "").Split("\n").ToList();

            var heightMap = new int[heightMapLetters[0].Length, heightMapLetters.Count];

            for (var y = 0; y < heightMapLetters.Count; y++)
            {
                var row = heightMapLetters[y];
                var charArray = row.ToCharArray();

                for (var x = 0; x < charArray.Length; x++)
                {
                    var letter = charArray[x];

                    if (letter >= 97)
                    {
                        //convert to map
                    }
                    else
                    {
                        //Convert to starting position
                    }
                }
            }

            return 0;
        }

        [Benchmark]
        public static int PartTwo(string input)
        {
            //var monkeyText = input.Replace("\r", "").Split("\n").ToList();

            return 0;
        }
    }
}