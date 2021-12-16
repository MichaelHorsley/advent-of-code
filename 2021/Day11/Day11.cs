using _2021.Helpers;
using NUnit.Framework;

namespace _2021.Day11
{
    [TestFixture]
    public class Day11
    {
        private string _testInput;
        private string _challengeInput;

        [SetUp]
        public void SetUp()
        {
            _testInput = FileHelper.GetResourceFile("_2021.Day11.test-input.txt");
            _challengeInput = FileHelper.GetResourceFile("_2021.Day11.test-challenge.txt");
        }

        [Test]
        public void Star1_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = CountSquidFlashes(list);

            Assert.AreEqual(1656, sum);
        }

        [Test]
        public void Star1_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = CountSquidFlashes(list);
            Assert.AreEqual(1661, sum);
        }

        [Test]
        public void Star2_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = WhenTheyAllFlashTogether(list);

            Assert.AreEqual(195, sum);
        }

        [Test]
        public void Star2_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = WhenTheyAllFlashTogether(list);
            Assert.AreEqual(334, sum);
        }

        private ulong WhenTheyAllFlashTogether(List<string> list)
        {
            var squidArray = new Squid[list.First().Length, list.Count];

            SetUpSquidArray(list, squidArray);

            ulong flashCount = 0;

            ulong stepCount = 0;

            while (true)
            {
                for (int yPosition = 0; yPosition < 10; yPosition++)
                {
                    for (int xPosition = 0; xPosition < 10; xPosition++)
                    {
                        FlashDirection(squidArray, xPosition, yPosition, ref flashCount);
                    }
                }

                stepCount++;

                bool allExploded = true;

                foreach (var squid in squidArray)
                {
                    if (!squid.ExplodedThisStep)
                    {
                        allExploded = false;
                    }
                }

                if (allExploded)
                {
                    return stepCount;
                }

                //reset
                foreach (var squid in squidArray)
                {
                    squid.ExplodedThisStep = false;
                }
            }
        }

        private ulong CountSquidFlashes(List<string> list)
        {
            var squidArray = new Squid[list.First().Length, list.Count];

            SetUpSquidArray(list, squidArray);

            ulong flashCount = 0;

            for (int step = 1; step <= 100; step++)
            {
                for (int yPosition = 0; yPosition < 10; yPosition++)
                {
                    for (int xPosition = 0; xPosition < 10; xPosition++)
                    {
                        FlashDirection(squidArray, xPosition, yPosition, ref flashCount);
                    }
                }

                //reset
                foreach (var squid in squidArray)
                {
                    squid.ExplodedThisStep = false;
                }
            }

            return flashCount;
        }

        private void FlashDirection(Squid[,] squidArray, int startingXPosition, int startingYPosition, ref ulong flashCount)
        {
            try
            {
                var squid = squidArray[startingXPosition, startingYPosition];

                if (!squid.ExplodedThisStep)
                {
                    squid.EnergyValue += 1;

                    if (squid.EnergyValue == 10)
                    {
                        flashCount++;

                        squid.ExplodedThisStep = true;
                        squid.EnergyValue = 0;

                        FlashDirection(squidArray, startingXPosition, startingYPosition + 1, ref flashCount); //North
                        FlashDirection(squidArray, startingXPosition + 1, startingYPosition + 1, ref flashCount); //North-East
                        FlashDirection(squidArray, startingXPosition + 1, startingYPosition, ref flashCount); //East
                        FlashDirection(squidArray, startingXPosition + 1, startingYPosition - 1, ref flashCount);  //South-East
                        FlashDirection(squidArray, startingXPosition, startingYPosition - 1, ref flashCount);  //South
                        FlashDirection(squidArray, startingXPosition - 1, startingYPosition - 1, ref flashCount);  //South-West
                        FlashDirection(squidArray, startingXPosition - 1, startingYPosition, ref flashCount);  //West
                        FlashDirection(squidArray, startingXPosition - 1, startingYPosition + 1, ref flashCount);  //North-West
                    }
                }
            }
            catch
            {

            }
        }

        private static void SetUpSquidArray(List<string> list, Squid[,] squidArray)
        {
            for (int y = 0; y < list.Count; y++)
            {
                var squidValue = list[y].ToArray().Select(squidValue => int.Parse(squidValue.ToString())).ToList();

                for (int x = 0; x < list.First().Length; x++)
                {
                    squidArray[x, y] = new Squid(squidValue[x]);
                }
            }
        }

        //[Test]
        //public void Star2_Test()
        //{
        //    var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
        //    var sum = CountSquidFlashes(list);

        //    Assert.AreEqual(0, sum);
        //}

        //[Test]
        //public void Star2_Challenge()
        //{
        //    var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
        //    var sum = CountSquidFlashes(list);

        //    Assert.AreEqual(0, sum);
        //}
    }

    internal class Squid
    {
        public int EnergyValue;
        public bool ExplodedThisStep = false;

        public Squid(int startingValue)
        {
            EnergyValue = startingValue;
        }
    }
}
