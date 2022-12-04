﻿using System.Linq;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day4
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day4_Test.txt");

            Assert.AreEqual(0, Day4Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day4.txt");

            Assert.AreEqual(0, Day4Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day4_Test.txt");

            Assert.AreEqual(0, Day4Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day4.txt");

            Assert.AreEqual(0, Day4Solution.PartTwo(input));
        }
    }

    public class Day4Solution
    {
        public static long PartOne(string input)
        {
            return 0;
        }

        public static long PartTwo(string input)
        {
            return 0;
        }
    }
}