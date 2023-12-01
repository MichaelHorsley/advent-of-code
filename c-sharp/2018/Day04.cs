using NUnit.Framework;

namespace _2018
{
    [TestFixture]
    public class Day04
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day04_Test.txt");

            Assert.AreEqual(240, Day04Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day04.txt");

            Assert.AreEqual(0, Day04Solution.PartOne(input));
        }

        //[Test]
        //public void PartTwo_Test()
        //{
        //    var input = FileHelper.GetInputFromFile("Day04_Test.txt");

        //    Assert.AreEqual(4, Day04Solution.PartTwo(input));
        //}

        //[Test]
        //public void PartTwo_Star()
        //{
        //    var input = FileHelper.GetInputFromFile("Day04.txt");

        //    Assert.AreEqual(849, Day04Solution.PartTwo(input));
        //}
    }

    public class Day04Solution
    {
        public static double PartOne(string input)
        {
            var list = input.Split(Environment.NewLine).ToList();

            var currentGuard = "";
            var sleepStart = "";
            var sleepEnd = "";

            foreach (var row in list)
            {
                var splitParts = row.Split(" ").ToList();

                switch (splitParts.Count)
                {
                    case 6:
                        currentGuard = splitParts[3];
                        break;
                    case 4:
                        break;
                }
            }

            return 0;
        }

        public static double PartTwo(string input)
        {
            throw new NotImplementedException();
        }
    }
}