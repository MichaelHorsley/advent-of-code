using _2021.Helpers;
using NUnit.Framework;

namespace _2021.Day5
{
    [TestFixture]
    public class Day5
    {
        private string _testInput;
        private string _challengeInput;

        [SetUp]
        public void SetUp()
        {
            _testInput = FileHelper.GetResourceFile("_2021.Day5.test-input.txt");
            _challengeInput = FileHelper.GetResourceFile("_2021.Day5.test-challenge.txt");
        }

        [Test]
        public void Star1_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = FindOverlapping(list);

            Assert.AreEqual(5, sum);
        }

        [Test]
        public void Star1_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = FindOverlapping(list);

            Assert.AreEqual(3990, sum);
        }

        [Test]
        public void Star2_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = FindOverlappingWithDiagonal(list);

            Assert.AreEqual(12, sum);
        }

        [Test]
        public void Star2_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = FindOverlappingWithDiagonal(list);

            Assert.AreEqual(21305, sum);
        }

        private int FindOverlappingWithDiagonal(List<string> list)
        {
            var map = new int[1000, 1000];

            foreach (var overlapping in list)
            {
                var rangeList = overlapping.Split("->").ToList();

                var fromList = rangeList[0].Split(',').ToList();

                var fromX = int.Parse(fromList[0]);
                var fromY = int.Parse(fromList[1]);

                var toList = rangeList[1].Split(',').ToList();

                var toX = int.Parse(toList[0]);
                var toY = int.Parse(toList[1]);


                if (fromX == toX)
                {
                    if (fromY >= toY)
                    {
                        for (var startY = fromY; startY >= toY; startY--)
                        {
                            map[fromX, startY]++;
                        }
                    }
                    else
                    {
                        for (var startY = fromY; startY <= toY; startY++)
                        {
                            map[fromX, startY]++;
                        }
                    }
                }
                else if (fromY == toY)
                {
                    if (fromX >= toX)
                    {
                        for (var startX = fromX; startX >= toX; startX--)
                        {
                            map[startX, fromY]++;
                        }
                    }
                    else
                    {
                        for (var startX = fromX; startX <= toX; startX++)
                        {
                            map[startX, fromY]++;
                        }
                    }
                }
                else
                {
                    var changeX = fromX > toX ? -1 : 1;
                    var changeY = fromY > toY ? -1 : 1;

                    while (fromX != toX)
                    {
                        map[fromX, fromY]++;

                        fromX += changeX;
                        fromY += changeY;
                    }

                    map[toX, toY]++;
                }
            }

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Console.Write(map[x, y] == 0 ? "." : map[x, y]);
                }

                Console.WriteLine();
            }

            return map.Cast<int>().Count(count => count > 1);
        }

        private int FindOverlapping(List<string> list)
        {
            var map = new int[1000, 1000];

            foreach (var overlapping in list)
            {
                var rangeList = overlapping.Split("->").ToList();

                var fromList = rangeList[0].Split(',').ToList();

                var fromX = int.Parse(fromList[0]);
                var fromY = int.Parse(fromList[1]);

                var toList = rangeList[1].Split(',').ToList();

                var toX = int.Parse(toList[0]);
                var toY = int.Parse(toList[1]);


                if (fromX == toX)
                {
                    if (fromY >= toY)
                    {
                        for (var startY = fromY; startY >= toY; startY--)
                        {
                            map[fromX, startY]++;
                        }
                    }
                    else
                    {
                        for (var startY = fromY; startY <= toY; startY++)
                        {
                            map[fromX, startY]++;
                        }
                    }
                }
                else if (fromY == toY)
                {
                    if (fromX >= toX)
                    {
                        for (var startX = fromX; startX >= toX; startX--)
                        {
                            map[startX, fromY]++;
                        }
                    }
                    else
                    {
                        for (var startX = fromX; startX <= toX; startX++)
                        {
                            map[startX, fromY]++;
                        }
                    }
                }
                else
                {

                }
            }

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Console.Write(map[x, y] == 0 ? "." : map[x, y]);
                }

                Console.WriteLine();
            }

            return map.Cast<int>().Count(count => count > 1);
        }

        //[Test]
        //public void Star2_Test()
        //{
        //    var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
        //    var sum = PlayGameAndFindSumOfUncalledNumbersOnLosingBoard(list);

        //    Assert.AreEqual(1924, sum);
        //}

        //[Test]
        //public void Star2_Challenge()
        //{
        //    var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
        //    var sum = PlayGameAndFindSumOfUncalledNumbersOnLosingBoard(list);

        //    Assert.AreEqual(4880, sum);
        //}
    }
}
