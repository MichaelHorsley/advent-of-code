using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;
using System.Linq;

namespace _2022
{
    [TestFixture]
    public class Day9
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day9_Test.txt");

            Assert.AreEqual(13, Day9Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day9.txt");

            Assert.AreEqual(6090, Day9Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day9_Test.txt");

            Assert.AreEqual(36, Day9Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day9.txt");

            Assert.AreEqual(0, Day9Solution.PartTwo(input));
        }
    }

    public class Day9Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var ropeMovements = input.Replace("\r", "").Split("\n").ToList();

            var head = new Node { X = 0, Y = 0 };
            var tail = new Node { X = 0, Y = 0 };
            var tailPositionHashset = new HashSet<string>();

            foreach (var ropeMovement in ropeMovements)
            {
                var commandSplit = ropeMovement.Split(' ').ToList();

                switch (commandSplit[0])
                {
                    case "D":
                        MoveDown(head, tail, int.Parse(commandSplit[1]), tailPositionHashset);
                        break;

                    case "L":
                        MoveLeft(head, tail, int.Parse(commandSplit[1]), tailPositionHashset);
                        break;

                    case "U":
                        MoveUp(head, tail, int.Parse(commandSplit[1]), tailPositionHashset);
                        break;

                    case "R":
                        MoveRight(head, tail, int.Parse(commandSplit[1]), tailPositionHashset); 
                        break;
                }
            }

            return tailPositionHashset.Count;
        }

        private static void MoveDown(Node head, Node tail, int numberOfSteps, HashSet<string> tailPositionHashset)
        {
            for (var stepNumber = 0; stepNumber < numberOfSteps; stepNumber++)
            {
                head.Y--;
                tail.MoveTowards(head.X, head.Y);
                tailPositionHashset.Add($"{tail.X}-${tail.Y}");
            }
        }

        private static void MoveLeft(Node head, Node tail, int numberOfSteps, HashSet<string> tailPositionHashset)
        {
            for (var stepNumber = 0; stepNumber < numberOfSteps; stepNumber++)
            {
                head.X--;
                tail.MoveTowards(head.X, head.Y);
                tailPositionHashset.Add($"{tail.X}-${tail.Y}");
            }
        }

        private static void MoveUp(Node head, Node tail, int numberOfSteps, HashSet<string> tailPositionHashset)
        {
            for (var stepNumber = 0; stepNumber < numberOfSteps; stepNumber++)
            {
                head.Y++;
                tail.MoveTowards(head.X, head.Y);
                tailPositionHashset.Add($"{tail.X}-${tail.Y}");
            }
        }

        private static void MoveRight(Node head, Node tail, int numberOfSteps, HashSet<string> tailPositionHashset)
        {
            for (var stepNumber = 0; stepNumber < numberOfSteps; stepNumber++)
            {
                head.X++;
                tail.MoveTowards(head.X, head.Y);
                tailPositionHashset.Add($"{tail.X}-${tail.Y}");
            }
        }

        [Benchmark]
        public static long PartTwo(string input)
        {
            return 0;
        }
    }

    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void MoveTowards(int targetX, int targetY)
        {
            if (!NodeAlreadyTouchingTargetPosition(targetX, targetY, X, Y))
            {
                if (Y == targetY)
                {
                    MoveXCloser(targetX);
                }
                else if (X == targetX)
                {
                    MoveYCloser(targetY);
                }
                else
                {
                    MoveToNextToHead(targetX, targetY);
                }
            }
        }

        private void MoveToNextToHead(int targetX, int targetY)
        {
            if (AdjacentToNode(targetX, targetY, X+1, Y+1)) { X++; Y++; return; } //Move North-East
            if (AdjacentToNode(targetX, targetY, X-1, Y+1)) { X--; Y++; return; } //Move North-West
            if (AdjacentToNode(targetX, targetY, X-1, Y-1)) { X--; Y--; return; } //Move South-West
            if (AdjacentToNode(targetX, targetY, X+1, Y-1)) { X++; Y--; return; } //Move South-East
        }

        private void MoveYCloser(int targetY)
        {
            if (Y + 1 == targetY - 1)
            {
                Y++;
            }
            else
            {
                Y--;
            }
        }

        private void MoveXCloser(int targetX)
        {
            if (X + 1 == targetX - 1)
            {
                X++;
            }
            else
            {
                X--;
            }
        }

        private bool NodeAlreadyTouchingTargetPosition(int targetX, int targetY, int currentX, int currentY)
        {
            if (targetX == currentX && targetY == currentY) return true; // on top

            if (targetX == currentX + 1 && targetY == currentY + 1) return true; // north-east
            if (targetX == currentX + 1 && targetY == currentY - 1) return true; // south-east
            if (targetX == currentX - 1 && targetY == currentY - 1) return true; // south-west
            if (targetX == currentX - 1 && targetY == currentY + 1) return true; // north-west

            if (AdjacentToNode(targetX, targetY, X, Y)) return true;

            return false;
        }

        private bool AdjacentToNode(int targetX, int targetY, int currentX, int currentY)
        {
            if (targetX == currentX && targetY == currentY + 1) return true;
            if (targetX == currentX + 1 && targetY == currentY) return true;
            if (targetX == currentX && targetY == currentY - 1) return true;
            if (targetX == currentX - 1 && targetY == currentY) return true;

            return false;
        }
    }
}