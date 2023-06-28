using _2021.Helpers;
using NUnit.Framework;

namespace _2021.Day8
{
    [TestFixture]
    public class Day8
    {
        private string _testInput;
        private string _challengeInput;

        [SetUp]
        public void SetUp()
        {
            _testInput = FileHelper.GetResourceFile("_2021.Day8.test-input.txt");
            _challengeInput = FileHelper.GetResourceFile("_2021.Day8.test-challenge.txt");
        }

        [Test]
        public void Star1_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = CountHowMany1478(list);

            Assert.AreEqual(26, sum);
        }

        [Test]
        public void Star1_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = CountHowMany1478(list);

            Assert.AreEqual(274, sum);
        }

        [Test]
        public void Star2_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = WorkOutSignalAndSumValues(list);

            Assert.AreEqual(0, sum);
        }

        [Test]
        public void Star2_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = WorkOutSignalAndSumValues(list);

            Assert.AreEqual(0, sum);
        }

        private int WorkOutSignalAndSumValues(List<string> list)
        {
            var sum = 0;

            foreach (var line in list)
            {
                var inputs = line.Split('|')[0].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                

            }

            return sum;
        }

        private int CountHowMany1478(List<string> list)
        {
            int count = 0;

            foreach (var line in list)
            {
                var displays = line.Split('|')[1].Split(" ").ToList();

                foreach (var display in displays)
                {
                    if (display.Length == 2 || display.Length == 3 || display.Length == 4 || display.Length == 7)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
