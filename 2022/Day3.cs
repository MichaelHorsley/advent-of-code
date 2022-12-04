using System.Linq;
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

            Assert.AreEqual(157, Day3Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day3.txt");

            Assert.AreEqual(8349, Day3Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day3_Test.txt");

            Assert.AreEqual(70, Day3Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day3.txt");

            Assert.AreEqual(2681, Day3Solution.PartTwo(input));
        }
    }

    public class Day3Solution
    {
        public static long PartOne(string input)
        {
            var listOfBackpacks = input.Replace("\n", "").Split("\r").ToList();

            var sumOfDuplicateItemPriorities = 0;

            listOfBackpacks.ForEach(backpack =>
            {
                var componentOne = backpack.ToCharArray(0, backpack.Length/2);
                var componentTwo = backpack.ToCharArray(backpack.Length/2, backpack.Length/2);

                var duplicatedItem = componentOne.Intersect(componentTwo).ToList()[0];

                var priority = WorkOutPriority(duplicatedItem);

                sumOfDuplicateItemPriorities += priority;
            });

            return sumOfDuplicateItemPriorities;
        }

        public static long PartTwo(string input)
        {
            var listOfBackpacks = input.Replace("\n", "").Split("\r").ToList();

            var sumOfDuplicateItemPriorities = 0;

            for (var position = 0; position < listOfBackpacks.Count; position+=3)
            {
                var duplicatedItem = listOfBackpacks[position].Intersect(listOfBackpacks[position + 1]).Intersect(listOfBackpacks[position + 2]).ToList()[0];

                var priority = WorkOutPriority(duplicatedItem);

                sumOfDuplicateItemPriorities += priority;
            }

            return sumOfDuplicateItemPriorities;
        }

        private static int WorkOutPriority(char duplicatedItem)
        {
            const int lowerStart = 'a';

            if (duplicatedItem >= lowerStart)
            {
                return duplicatedItem - 96;
            }

            return duplicatedItem - 38;
        }
    }
}