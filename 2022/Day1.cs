using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day1
    {
        [Test]
        public void PartOne_Test()
        {
            var contents = FileHelper.GetInputFromFile("Day1_Test.txt");

            Assert.AreEqual(24000, Day1Solution.PartOne(contents));
        }

        [Test]
        public void PartOne_Star()
        {
            var contents = FileHelper.GetInputFromFile("Day1.txt");

            Assert.AreEqual(72240, Day1Solution.PartOne(contents));
        }

        [Test]
        public void PartTwo_Test()
        {
            var contents = FileHelper.GetInputFromFile("Day1_Test.txt");

            Assert.AreEqual(45000, Day1Solution.PartTwo(contents));
        }

        [Test]
        public void PartTwo_Star()
        {
            var contents = FileHelper.GetInputFromFile("Day1.txt");

            Assert.AreEqual(210957, Day1Solution.PartTwo(contents));
        }
    }

    public class Day1Solution
    {
        public static long PartOne(string input)
        {
            var elfList = GetCalorieList(input);

            return elfList.Max();
        }

        private static List<int> GetCalorieList(string input)
        {
            var list = input.Split('\r').ToList();

            var elfList = new List<int>();

            var sum = 0;

            foreach (var calorieInput in list)
            {
                if (string.IsNullOrWhiteSpace(calorieInput))
                {
                    elfList.Add(sum);
                    sum = 0;
                }
                else
                {
                    sum += int.Parse(calorieInput);
                }
            }

            elfList.Add(sum);

            return elfList;
        }

        public static long PartTwo(string input)
        {
            var elfList = GetCalorieList(input);

            return elfList.OrderByDescending(x => x).Take(3).Sum();
        }
    }
}
