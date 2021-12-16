using System.Diagnostics;
using _2021.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace _2021.Day15
{
    [TestFixture]
    public class Day15
    {
        private string _testInput = "";
        private string _challengeInput = "";

        [SetUp]
        public void SetUp()
        {
            _testInput = FileHelper.GetResourceFile("_2021.Day15.test-input.txt");
            _challengeInput = FileHelper.GetResourceFile("_2021.Day15.test-challenge.txt");
        }

        [Test]
        public void Star1_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim()).ToList();

            var lowestRiskPathRoute = LowestRiskPathThroughCave(list);

            Assert.AreEqual(40, lowestRiskPathRoute);
        }

        [Test]
        public void Star1_Challenge()
        {
            var sw = Stopwatch.StartNew();

            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim()).ToList();
            var lowestRiskPathRoute = LowestRiskPathThroughCave(list);

            
            Assert.AreEqual(363, lowestRiskPathRoute);
        }

        [Test]
        public void Star2_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim()).ToList();

            var lowestRiskPathRoute = LowestRiskPathThroughCaveFullCave(list);

            Assert.AreEqual(315, lowestRiskPathRoute);
        }

        [Test]
        public void Star2_Challenge()
        {
            var sw = Stopwatch.StartNew();
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim()).ToList();
            var lowestRiskPathRoute = LowestRiskPathThroughCaveFullCave(list);
            Console.WriteLine(sw.ElapsedMilliseconds);
            Assert.AreEqual(2835, lowestRiskPathRoute);
        }

        private int LowestRiskPathThroughCaveFullCave(List<string> list)
        {
            var startingMaxY = list.Count;
            var startingMaxX = list.First().Length;

            var fullMapMaxX = startingMaxX * 5;
            var fullMapMaxY = startingMaxY * 5;

            var cave = new int[fullMapMaxX, fullMapMaxY];

            for (int yPosition = 0; yPosition < fullMapMaxY; yPosition++)
            {
                var sanitisedY = yPosition % startingMaxY;
                var yMultiple = yPosition / startingMaxY;

                var row = list[sanitisedY].ToArray().Select(x => int.Parse(x.ToString())).ToList();

                for (int xPosition = 0; xPosition < fullMapMaxX; xPosition++)
                {
                    var sanitisedX = xPosition % startingMaxX;
                    var xMultiple = xPosition / startingMaxX;

                    var originalRisk = row[sanitisedX] + yMultiple + xMultiple;

                    if (originalRisk > 9)
                    {
                        originalRisk %= 9;
                    }

                    cave[xPosition, yPosition] = originalRisk;
                }
            }

            return AllSquaresCost(cave);
        }

        private int LowestRiskPathThroughCave(List<string> list)
        {
            var maxY = list.Count;
            var maxX = list.First().Length;

            var cave = new int[maxX, maxY];

            for (int yPosition = 0; yPosition < maxY; yPosition++)
            {
                var row = list[yPosition].ToArray().Select(x => int.Parse(x.ToString())).ToList();

                for (int xPosition = 0; xPosition < maxX; xPosition++)
                {
                    cave[xPosition, yPosition] = row[xPosition];
                }
            }

            return AllSquaresCost(cave);
        }

        private int AllSquaresCost(int[,] cave)
        {
            var maxX = cave.GetLength(0);
            var maxY = cave.GetLength(1);

            CavePoint[,] caveRisk = new CavePoint[maxX, maxY];

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var risk = cave[x, y];

                    if (x == 0 && y == 0)
                    {
                        caveRisk[x, y] = new CavePoint(0){CurrentMinRisk = 0};
                    }
                    else
                    {
                        caveRisk[x, y] = new CavePoint(risk);
                    }
                }
            }

            while (true)
            {
                var minRiskChanged = false;

                for (int y = 0; y < maxY; y++)
                {
                    for (int x = 0; x < maxX; x++)
                    {
                        var cavePoint = caveRisk[x, y];

                        try { ApplyTotalRisk(caveRisk[x, y - 1], cavePoint.CurrentMinRisk, ref minRiskChanged); } catch { } //North
                        try { ApplyTotalRisk(caveRisk[x, y + 1], cavePoint.CurrentMinRisk, ref minRiskChanged); } catch { } //South
                        try { ApplyTotalRisk(caveRisk[x + 1, y], cavePoint.CurrentMinRisk, ref minRiskChanged); } catch { } //East
                        try { ApplyTotalRisk(caveRisk[x - 1, y], cavePoint.CurrentMinRisk, ref minRiskChanged); } catch { } //West
                    }
                }

                if (!minRiskChanged)
                {
                    break;
                }
            }
            
            return caveRisk[maxX-1, maxY-1].CurrentMinRisk;
        }

        private void ApplyTotalRisk(CavePoint targetCavePoint, int parentTotalRisk, ref bool minRiskChanged)
        {
            var combinedRiskToGetThere = parentTotalRisk + targetCavePoint.OwnRisk;

            if (targetCavePoint.CurrentMinRisk > combinedRiskToGetThere)
            {
                targetCavePoint.CurrentMinRisk = combinedRiskToGetThere;

                if (!minRiskChanged)
                {
                    minRiskChanged = true;
                }
            }
        }

        public class CavePoint
        {
            public int OwnRisk { get; }
            public int CurrentMinRisk { get; set; }

            public CavePoint(int ownRisk)
            {
                OwnRisk = ownRisk;
                CurrentMinRisk = int.MaxValue;
            }
        }
    }
}