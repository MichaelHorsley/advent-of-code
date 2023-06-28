using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day15
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day15_Test.txt");

            Assert.AreEqual(26, Day15Solution.PartOne(input, 10));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day15.txt");

            Assert.AreEqual(4873353, Day15Solution.PartOne(input, 2000000));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day15_Test.txt");

            Assert.AreEqual(56000011, Day15Solution.PartTwo(input, 20));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day15.txt");

            Assert.AreEqual(11600823139120, Day15Solution.PartTwo(input, 4000000));
        }
    }

    public class Day15Solution
    {
        [Benchmark]
        public static int PartOne(string input, int yPlane)
        {
            var rowsList = input.Replace("\r", "").Split("\n").ToList();

            var sensorList = new List<Sensor>();

            foreach (var row in rowsList)
            {
                var commandSplit = row.Split(":").ToList();

                var sensorText = commandSplit[0].Replace("Sensor at ", "");
                var closestBeaconText = commandSplit[1].Replace(" closest beacon is at ", "");

                var sensor = new Sensor
                {
                    X = int.Parse(sensorText.Split(", ")[0].Split('=')[1]),
                    Y = int.Parse(sensorText.Split(", ")[1].Split('=')[1]),
                    BeaconX = int.Parse(closestBeaconText.Split(", ")[0].Split('=')[1]),
                    BeaconY = int.Parse(closestBeaconText.Split(", ")[1].Split('=')[1]),
                };

                sensorList.Add(sensor);
            }

            foreach (var sensor in sensorList)
            {
                sensor.WorkOutManhattanDistance();
            }

            var startingX = sensorList.Select(x => x.MinX).Min();
            var endingX = sensorList.Select(x => x.MaxX).Max();

            var checkedPositions = 0;

            while (startingX != endingX)
            {
                if (sensorList.Any(x => x.ContainsCoordinatesWithinRange(startingX, yPlane)))
                {
                    checkedPositions++;
                }

                startingX++;
            }


            return checkedPositions-1;
        }

        [Benchmark]
        public static ulong PartTwo(string input, int boundary)
        {
            var rowsList = input.Replace("\r", "").Split("\n").ToList();

            var sensorList = new List<Sensor>();

            foreach (var row in rowsList)
            {
                var commandSplit = row.Split(":").ToList();

                var sensorText = commandSplit[0].Replace("Sensor at ", "");
                var closestBeaconText = commandSplit[1].Replace(" closest beacon is at ", "");

                var sensor = new Sensor
                {
                    X = int.Parse(sensorText.Split(", ")[0].Split('=')[1]),
                    Y = int.Parse(sensorText.Split(", ")[1].Split('=')[1]),
                    BeaconX = int.Parse(closestBeaconText.Split(", ")[0].Split('=')[1]),
                    BeaconY = int.Parse(closestBeaconText.Split(", ")[1].Split('=')[1]),
                };

                sensorList.Add(sensor);
            }

            foreach (var sensor in sensorList)
            {
                sensor.WorkOutManhattanDistance();
            }

            var sensorOuterReach = new HashSet<string>();

            for (var index = 0; index < sensorList.Count; index++)
            {
                var sensor = sensorList[index];
                sensor.AddLevelOutOfReach(sensorOuterReach);
            }

            foreach (var coord in sensorOuterReach)
            {
                var x = int.Parse(coord.Split('|')[0]);
                var y = int.Parse(coord.Split('|')[1]);

                if (x < 0 || x > boundary)
                {
                    continue;
                }

                if (y < 0 || y > boundary)
                {
                    continue;
                }

                if (!sensorList.Any(sensor => sensor.ContainsCoordinatesWithinRange(x, y)))
                {
                    return (ulong)x * (ulong)4000000 + (ulong)y;
                }
            }

            return 0;
        }
    }

    public class Sensor
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int MinX { get; set; }
        public int MinY { get; set; }


        public int BeaconX { get; set; }
        public int BeaconY { get; set; }

        public int ManhattanDistance { get; set; }

        public void WorkOutManhattanDistance()
        {
            var xDifference = Math.Abs(X - BeaconX);
            var yDifference = Math.Abs(Y - BeaconY);

            ManhattanDistance = xDifference + yDifference;
            
            MaxX = X + ManhattanDistance;
            MinX = X - ManhattanDistance;
            
            MaxY = Y + ManhattanDistance;
            MinY = Y - ManhattanDistance;
        }

        public bool ContainsCoordinatesWithinRange(int x, int y)
        {
            var xDifference = Math.Abs(x - X);
            var yDifference = Math.Abs(y - Y);

            var distance = xDifference + yDifference;

            return distance <= ManhattanDistance;
        }

        public void AddLevelOutOfReach(HashSet<string> sensorOuterReach)
        {
            var outOfReachScope = ManhattanDistance + 1;

            var xDifference = 0;
            var yDifference= outOfReachScope;

            //top
            while (yDifference != 0)
            {
                sensorOuterReach.Add($"{X + xDifference}|{Y + yDifference}");
                sensorOuterReach.Add($"{X - xDifference}|{Y + yDifference}");
                sensorOuterReach.Add($"{X + xDifference}|{Y - yDifference}");
                sensorOuterReach.Add($"{X - xDifference}|{Y - yDifference}");

                xDifference++;
                yDifference--;
            }
        }
    }
}