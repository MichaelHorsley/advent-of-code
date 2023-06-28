using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day12
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day12_Test.txt");

            Assert.AreEqual(31, Day12Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day12.txt");

            Assert.AreEqual(481, Day12Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day12_Test.txt");

            Assert.AreEqual(29, Day12Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day12.txt");

            Assert.AreEqual(480, Day12Solution.PartTwo(input));
        }
    }

    public class Day12Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var heightMapLetters = input.Replace("\r", "").Split("\n").ToList();

            var heightMap = new int[heightMapLetters[0].Length, heightMapLetters.Count];

            var startingPositionX = 0;
            var startingPositionY = 0;
            var endPositionX = 0;
            var endPositionY = 0;

            for (var y = 0; y < heightMapLetters.Count; y++)
            {
                var row = heightMapLetters[y];
                var charArray = row.ToCharArray();

                for (var x = 0; x < charArray.Length; x++)
                {
                    var letter = charArray[x];

                    if (letter >= 97)
                    {
                        heightMap[x, y] = (int)letter - 96;
                    }
                    else
                    {
                        if (letter == 'S')
                        {
                            startingPositionX = x;
                            startingPositionY = y;
                            heightMap[x, y] = 1;
                        }
                        else
                        {
                            endPositionX = x;
                            endPositionY = y;
                            heightMap[x, y] = 26;
                        }
                    }
                }
            }

            var routes = new List<HashSet<string>>();

            var positions = new List<Position>
            {
                new Position{X = startingPositionX, Y = startingPositionY, PlacesBeen = new HashSet<string>()}
            };

            BreadthFirstMoveTowardsGoal(endPositionX, endPositionY, heightMap, routes, positions);

            return routes.Min(x => x.Count);
        }

        [Benchmark]
        public static int PartTwo(string input)
        {
            var heightMapLetters = input.Replace("\r", "").Split("\n").ToList();

            var heightMap = new int[heightMapLetters[0].Length, heightMapLetters.Count];

            var startingPositionX = 0;
            var startingPositionY = 0;

            for (var y = 0; y < heightMapLetters.Count; y++)
            {
                var row = heightMapLetters[y];
                var charArray = row.ToCharArray();

                for (var x = 0; x < charArray.Length; x++)
                {
                    var letter = charArray[x];

                    if (letter >= 97)
                    {
                        heightMap[x, y] = (int)letter - 96;
                    }
                    else
                    {
                        if (letter == 'E')
                        {
                            startingPositionX = x;
                            startingPositionY = y;
                            heightMap[x, y] = 26;
                        }
                        else
                        {
                            heightMap[x, y] = 1;
                        }
                    }
                }
            }

            var routes = new List<HashSet<string>>();

            var positions = new List<Position>
            {
                new Position{X = startingPositionX, Y = startingPositionY, PlacesBeen = new HashSet<string>()}
            };

            BreadthFirstMoveTowardsStart(heightMap, routes, positions);

            return routes.Min(x => x.Count);
        }

        private static void BreadthFirstMoveTowardsStart(int[,] heightMap, List<HashSet<string>> routes, List<Position> positions)
        {
            var placesBeen = new HashSet<string>();

            while (positions.Count > 0)
            {
                var position = positions[0];

                positions.RemoveAt(0);

                if (heightMap[position.X, position.Y] == 1)
                {
                    routes.Add(position.PlacesBeen);
                }
                else
                {
                    position.PlacesBeen.Add($"{position.X}-{position.Y}");
                    placesBeen.Add($"{position.X}-{position.Y}");

                    CheckAndAddTowardsStart(heightMap, placesBeen, position.PlacesBeen, positions, position.X + 1, position.Y, position);
                    CheckAndAddTowardsStart(heightMap, placesBeen, position.PlacesBeen, positions, position.X - 1, position.Y, position);
                    CheckAndAddTowardsStart(heightMap, placesBeen, position.PlacesBeen, positions, position.X, position.Y + 1, position);
                    CheckAndAddTowardsStart(heightMap, placesBeen, position.PlacesBeen, positions, position.X, position.Y - 1, position);
                }
            }
        }

        private static void BreadthFirstMoveTowardsGoal(int endPositionX, int endPositionY, int[,] heightMap, List<HashSet<string>> routes, List<Position> positions)
        {
            var placesBeen = new HashSet<string>();

            while (positions.Count > 0)
            {
                var position = positions[0];

                positions.RemoveAt(0);

                if (position.X == endPositionX && position.Y == endPositionY)
                {
                    routes.Add(position.PlacesBeen);
                }
                else
                {
                    position.PlacesBeen.Add($"{position.X}-{position.Y}");
                    placesBeen.Add($"{position.X}-{position.Y}");

                    CheckAndAdd(heightMap, placesBeen, position.PlacesBeen, positions, position.X + 1, position.Y, position);
                    CheckAndAdd(heightMap, placesBeen, position.PlacesBeen, positions, position.X - 1, position.Y, position);
                    CheckAndAdd(heightMap, placesBeen, position.PlacesBeen, positions, position.X, position.Y + 1, position);
                    CheckAndAdd(heightMap, placesBeen, position.PlacesBeen, positions, position.X, position.Y - 1, position);
                }
            }
        }

        private static void CheckAndAddTowardsStart(int[,] heightMap, HashSet<string> placesBeen, HashSet<string> positionPlacesBeen, List<Position> positions, int targetX, int targetY, Position position)
        {
            if (!placesBeen.Contains($"{targetX}-{targetY}") && CanMoveThereTowardsStart(position.X, position.Y, targetX, targetY, heightMap))
            {
                positions.Add(new Position { X = targetX, Y = targetY, PlacesBeen = new HashSet<string>(positionPlacesBeen) });
                placesBeen.Add($"{targetX}-{targetY}");
            }
        }

        private static void CheckAndAdd(int[,] heightMap, HashSet<string> placesBeen, HashSet<string> positionPlacesBeen, List<Position> positions, int targetX, int targetY, Position position)
        {
            if (!placesBeen.Contains($"{targetX}-{targetY}") && CanMoveThere(position.X, position.Y, targetX, targetY, heightMap))
            {
                positions.Add(new Position { X = targetX, Y = targetY, PlacesBeen = new HashSet<string>(positionPlacesBeen) });
                placesBeen.Add($"{targetX}-{targetY}");
            }
        }

        private static bool CanMoveThereTowardsStart(int currentX, int currentY, int targetX, int targetY, int[,] heightMap)
        {
            int targetHeight;

            try
            {
                targetHeight = heightMap[targetX, targetY];
            }
            catch
            {
                return false;
            }

            var height = heightMap[currentX, currentY];

            return height - 1 <= targetHeight;
        }

        private static bool CanMoveThere(int currentX, int currentY, int targetX, int targetY, int[,] heightMap)
        {
            int targetHeight;

            try
            {
                targetHeight = heightMap[targetX, targetY];
            }
            catch
            {
                return false;
            }

            var height = heightMap[currentX, currentY];

            return height + 1 >= targetHeight;

        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public HashSet<string> PlacesBeen { get; set; } = new HashSet<string>();
    }
}