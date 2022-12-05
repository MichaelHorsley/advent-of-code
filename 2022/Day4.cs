using System.Linq;
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

            Assert.AreEqual(2, Day4Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day4.txt");

            Assert.AreEqual(487, Day4Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day4_Test.txt");

            Assert.AreEqual(4, Day4Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day4.txt");

            Assert.AreEqual(849, Day4Solution.PartTwo(input));
        }
    }

    public class Day4Solution
    {
        public static long PartOne(string input)
        {
            var rows = input.Replace("\n", "").Split('\r').ToList();

            var sum = 0;

            rows.ForEach(row =>
            {
                var sections = row.Split(',').ToList();

                var firstSection = sections[0].Split('-');
                var secondSection = sections[1].Split('-');

                if (SectionContainsTheOther(firstSection, secondSection) ||
                    SectionContainsTheOther(secondSection, firstSection))
                {
                    sum++;
                }
            });

            return sum;
        }

        private static bool SectionContainsTheOther(string[] firstSection, string[] secondSection)
        {
            return int.Parse(firstSection[0]) >= int.Parse(secondSection[0]) 
                && int.Parse(firstSection[1]) <= int.Parse(secondSection[1]);
        }

        public static long PartTwo(string input)
        {
            var rows = input.Replace("\n", "").Split('\r').ToList();

            var sum = 0;

            rows.ForEach(row =>
            {
                var sections = row.Split(',').ToList();

                var firstSection = sections[0].Split('-');
                var secondSection = sections[1].Split('-');

                var firstSectionLow = int.Parse(firstSection[0]);
                var firstSectionHigh = int.Parse(firstSection[1]);

                var secondSectionLow = int.Parse(secondSection[0]);
                var secondSectionHigh = int.Parse(secondSection[1]);

                if (SectionsOverlap(firstSectionLow, firstSectionHigh, secondSectionLow, secondSectionHigh)
                    || SectionsOverlap(secondSectionLow, secondSectionHigh, firstSectionLow, firstSectionHigh))
                {
                    sum++;
                }
            });

            return sum;
        }

        private static bool SectionsOverlap(int firstSectionLow, int firstSectionHigh, int secondSectionLow, int secondSectionHigh)
        {
            if (firstSectionLow >= secondSectionLow && firstSectionLow <= secondSectionHigh)
            {
                return true;
            }

            if (firstSectionHigh >= secondSectionLow && firstSectionHigh <= secondSectionHigh)
            {
                return true;
            }

            return false;
        }
    }
}