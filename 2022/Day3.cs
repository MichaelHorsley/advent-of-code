using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day3
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day3_Test.txt");

            Assert.AreEqual(0, Day3Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day3.txt");

            Assert.AreEqual(0, Day3Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day3_Test.txt");

            Assert.AreEqual(0, Day3Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day3.txt");

            Assert.AreEqual(0, Day3Solution.PartTwo(input));
        }
    }

    public class Day3Solution
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