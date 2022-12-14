using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day14
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day14_Test.txt");

            Assert.AreEqual(24, Day14Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day14.txt");

            Assert.AreEqual(805, Day14Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day14_Test.txt");

            Assert.AreEqual(93, Day14Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day14.txt");

            Assert.AreEqual(25161, Day14Solution.PartTwo(input));
        }
    }

    public class Day14Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var rowsList = input.Replace("\r", "").Split("\n").ToList();

            var list = input.Replace("\r", "").Replace("->", "").Split('\n', ' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            var minX = int.MaxValue;
            var maxX = int.MinValue;
            var maxY = int.MinValue;

            foreach (var wallPosition in list)
            {
                var wallX = int.Parse(wallPosition.Split(',')[0]);
                var wallY = int.Parse(wallPosition.Split(',')[1]);

                if(wallX < minX) minX = wallX;
                if(wallX > maxX) maxX = wallX;
                if(wallY > maxY) maxY = wallY;
            }

            var xSize = maxX - minX;

            var xOffSet = 500 - minX;

            var caveSystem = new string[xSize+1,maxY+1];

            for (int x = 0; x <= xSize; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    caveSystem[x, y] = ".";
                }
            }

            foreach (var wallPath in rowsList)
            {
                var wallPoints = wallPath.Split(" -> ").ToList();

                for (var index = 0; index < wallPoints.Count-1; index++)
                {
                    var startingWallPoint = wallPoints[index];
                    var endingWallPoint = wallPoints[index+1];

                    var startingPositionX = int.Parse(startingWallPoint.Split(',')[0]);
                    var startingPositionY = int.Parse(startingWallPoint.Split(',')[1]);

                    var endingPositionX = int.Parse(endingWallPoint.Split(',')[0]);
                    var endingPositionY = int.Parse(endingWallPoint.Split(',')[1]);

                    if (startingPositionX != endingPositionX && startingPositionY != endingPositionY)
                    {
                        throw new Exception();
                    }

                    if (startingPositionX != endingPositionX)
                    {
                        var increment = startingPositionX < endingPositionX ? 1 : -1;

                        while (startingPositionX != endingPositionX)
                        {
                            caveSystem[startingPositionX - minX, startingPositionY] = "#";
                            startingPositionX += increment;
                        }

                        caveSystem[startingPositionX - minX, startingPositionY] = "#";
                    }
                    else
                    {
                        var increment = startingPositionY < endingPositionY ? 1 : -1;

                        while (startingPositionY != endingPositionY)
                        {
                            caveSystem[startingPositionX - minX, startingPositionY] = "#";
                            startingPositionY += increment;
                        }

                        caveSystem[startingPositionX - minX, startingPositionY] = "#";
                    }
                }
            }

            int sandCount = 0;

            try
            {
                while (true)
                {
                    var sandPositionX = 500;
                    var sandPositionY = 0;

                    for (int y = 0; y <= maxY; y++)
                    {
                        for (int x = 0; x <= xSize; x++)
                        {
                            Console.Write(caveSystem[x, y]);
                        }

                        Console.WriteLine();
                    }

                    while (true)
                    {
                        if (caveSystem[sandPositionX - minX, sandPositionY + 1] == ".")
                        {
                            //fall
                            sandPositionY++;
                            continue;
                        }

                        if (caveSystem[sandPositionX - minX, sandPositionY + 1] == "o" || caveSystem[sandPositionX - minX, sandPositionY + 1] == "#")
                        {
                            if (caveSystem[sandPositionX - minX - 1, sandPositionY + 1] == ".")
                            {
                                //continue fall left
                                sandPositionX--;
                                sandPositionY++;
                                continue;
                            }
                            else if (caveSystem[sandPositionX - minX + 1, sandPositionY + 1] == ".")
                            {
                                //continue fall right
                                sandPositionX++;
                                sandPositionY++;
                                continue;
                            }
                            else
                            {
                                //rest
                                caveSystem[sandPositionX - minX, sandPositionY] = "o";
                                sandCount++;
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {

            }

            return sandCount;
        }

        [Benchmark]
        public static int PartTwo(string input)
        {
            var rowsList = input.Replace("\r", "").Split("\n").ToList();

            var list = input.Replace("\r", "").Replace("->", "").Split('\n', ' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            var minX = int.MaxValue;
            var maxX = int.MinValue;
            var maxY = int.MinValue;

            foreach (var wallPosition in list)
            {
                var wallX = int.Parse(wallPosition.Split(',')[0]);
                var wallY = int.Parse(wallPosition.Split(',')[1]);

                if (wallX < minX) minX = wallX;
                if (wallX > maxX) maxX = wallX;
                if (wallY > maxY) maxY = wallY;
            }

            maxY += 2;

            maxX = maxY * 4 + 1;

            var centerX = (int)Math.Floor((decimal)(maxX / 2));

            var xOffSet = 500 - centerX;

            var caveSystem = new string[maxX + 1, maxY + 1];

            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    caveSystem[x, y] = y == maxY ? "#" : ".";
                }
            }

            foreach (var wallPath in rowsList)
            {
                var wallPoints = wallPath.Split(" -> ").ToList();

                for (var index = 0; index < wallPoints.Count - 1; index++)
                {
                    var startingWallPoint = wallPoints[index];
                    var endingWallPoint = wallPoints[index + 1];

                    var startingPositionX = int.Parse(startingWallPoint.Split(',')[0]);
                    var startingPositionY = int.Parse(startingWallPoint.Split(',')[1]);

                    var endingPositionX = int.Parse(endingWallPoint.Split(',')[0]);
                    var endingPositionY = int.Parse(endingWallPoint.Split(',')[1]);

                    if (startingPositionX != endingPositionX && startingPositionY != endingPositionY)
                    {
                        throw new Exception();
                    }

                    if (startingPositionX != endingPositionX)
                    {
                        var increment = startingPositionX < endingPositionX ? 1 : -1;

                        while (startingPositionX != endingPositionX)
                        {
                            caveSystem[startingPositionX - xOffSet, startingPositionY] = "#";
                            startingPositionX += increment;
                        }

                        caveSystem[startingPositionX - xOffSet, startingPositionY] = "#";
                    }
                    else
                    {
                        var increment = startingPositionY < endingPositionY ? 1 : -1;

                        while (startingPositionY != endingPositionY)
                        {
                            caveSystem[startingPositionX - xOffSet, startingPositionY] = "#";
                            startingPositionY += increment;
                        }

                        caveSystem[startingPositionX - xOffSet, startingPositionY] = "#";
                    }
                }
            }

            DrawItOut(maxY, maxX, caveSystem);

            int sandCount = 0;

            while (true)
            {
                var sandPositionX = 500;
                var sandPositionY = 0;

                while (true)
                {
                    if (caveSystem[sandPositionX - xOffSet, sandPositionY] == "o")
                    {
                        return sandCount;
                    }

                    if (caveSystem[sandPositionX - xOffSet, sandPositionY + 1] == ".")
                    {
                        //fall
                        sandPositionY++;
                        continue;
                    }

                    if (caveSystem[sandPositionX - xOffSet, sandPositionY + 1] == "o" || caveSystem[sandPositionX - xOffSet, sandPositionY + 1] == "#")
                    {
                        if (caveSystem[sandPositionX - xOffSet - 1, sandPositionY + 1] == ".")
                        {
                            //continue fall left
                            sandPositionX--;
                            sandPositionY++;
                            continue;
                        }
                        else if (caveSystem[sandPositionX - xOffSet + 1, sandPositionY + 1] == ".")
                        {
                            //continue fall right
                            sandPositionX++;
                            sandPositionY++;
                            continue;
                        }
                        else
                        {
                            //rest
                            caveSystem[sandPositionX - xOffSet, sandPositionY] = "o";
                            sandCount++;
                            break;
                        }
                    }
                }
            }
        }

        private static void DrawItOut(int maxY, int maxX, string[,] caveSystem)
        {
            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    Console.Write(caveSystem[x, y]);
                }

                Console.WriteLine();
            }
        }
    }
}