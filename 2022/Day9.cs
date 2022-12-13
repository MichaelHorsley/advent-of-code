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

            Assert.AreEqual(88, Day9Solution.PartOne(input));
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

            Assert.AreEqual(2566, Day9Solution.PartTwo(input));
        }
    }

    public class Day9Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var ropeMovements = input.Replace("\r", "").Split("\n").ToList();

            var nodeList = new List<Node>
            {
                new Node { X = 0, Y = 0 },
                new Node { X = 0, Y = 0 }
            };

            return MoveRope(ropeMovements, nodeList);
        }

        [Benchmark]
        public static long PartTwo(string input)
        {
            var ropeMovements = input.Replace("\r", "").Split("\n").ToList();

            var nodeList = new List<Node>
            {
                new Node { X = 0, Y = 0 },
                new Node { X = 0, Y = 0 },
                new Node { X = 0, Y = 0 },
                new Node { X = 0, Y = 0 },
                new Node { X = 0, Y = 0 },
                new Node { X = 0, Y = 0 },
                new Node { X = 0, Y = 0 },
                new Node { X = 0, Y = 0 },
                new Node { X = 0, Y = 0 },
                new Node { X = 0, Y = 0 },
            };

            return MoveRope(ropeMovements, nodeList);
        }

        private static int MoveRope(List<string> ropeMovements, List<Node> nodeList)
        {
            var tailPositionHashset = new HashSet<string>();

            foreach (var ropeMovement in ropeMovements)
            {
                var commandSplit = ropeMovement.Split(' ').ToList();

                switch (commandSplit[0])
                {
                    case "D":
                        MoveDown(nodeList, int.Parse(commandSplit[1]), tailPositionHashset);
                        break;

                    case "L":
                        MoveLeft(nodeList, int.Parse(commandSplit[1]), tailPositionHashset);
                        break;

                    case "U":
                        MoveUp(nodeList, int.Parse(commandSplit[1]), tailPositionHashset);
                        break;

                    case "R":
                        MoveRight(nodeList, int.Parse(commandSplit[1]), tailPositionHashset);
                        break;
                }
            }

            return tailPositionHashset.Count;
        }

        private static void MoveDown(List<Node> nodes, int numberOfSteps, HashSet<string> tailPositionHashset)
        {
            for (var stepNumber = 0; stepNumber < numberOfSteps; stepNumber++)
            {
                nodes[0].Y--;

                for (var index = 1; index < nodes.Count; index++)
                {
                    var head = nodes[index-1];
                    var tail = nodes[index];

                    tail.MoveTowards(head.X, head.Y);
                }

                tailPositionHashset.Add($"{nodes.Last().X}-${nodes.Last().Y}");
            }
        }

        private static void MoveLeft(List<Node> nodes, int numberOfSteps, HashSet<string> tailPositionHashset)
        {
            for (var stepNumber = 0; stepNumber < numberOfSteps; stepNumber++)
            {
                nodes[0].X--;

                for (var index = 1; index < nodes.Count; index++)
                {
                    var head = nodes[index - 1];
                    var tail = nodes[index];

                    tail.MoveTowards(head.X, head.Y);
                }

                tailPositionHashset.Add($"{nodes.Last().X}-${nodes.Last().Y}");
            }
        }

        private static void MoveUp(List<Node> nodes, int numberOfSteps, HashSet<string> tailPositionHashset)
        {
            for (var stepNumber = 0; stepNumber < numberOfSteps; stepNumber++)
            {
                nodes[0].Y++;

                for (var index = 1; index < nodes.Count; index++)
                {
                    var head = nodes[index - 1];
                    var tail = nodes[index];

                    tail.MoveTowards(head.X, head.Y);
                }

                tailPositionHashset.Add($"{nodes.Last().X}-${nodes.Last().Y}");
            }
        }

        private static void MoveRight(List<Node> nodes, int numberOfSteps, HashSet<string> tailPositionHashset)
        {
            for (var stepNumber = 0; stepNumber < numberOfSteps; stepNumber++)
            {
                nodes[0].X++;

                for (var index = 1; index < nodes.Count; index++)
                {
                    var head = nodes[index - 1];
                    var tail = nodes[index];

                    tail.MoveTowards(head.X, head.Y);
                }

                tailPositionHashset.Add($"{nodes.Last().X}-${nodes.Last().Y}");
            }
        }
    }

    internal class Node
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
                    if (!MoveToNextToHead(targetX, targetY))
                    {
                        MoveDiagonally(targetX, targetY);
                    }
                }
            }
        }

        private void MoveDiagonally(int targetX, int targetY)
        {
            if (NodeAlreadyTouchingTargetPosition(targetX, targetY, X + 1, Y + 1)) { X++; Y++; return; } //Move North-East
            if (NodeAlreadyTouchingTargetPosition(targetX, targetY, X - 1, Y + 1)) { X--; Y++; return; } //Move North-West
            if (NodeAlreadyTouchingTargetPosition(targetX, targetY, X - 1, Y - 1)) { X--; Y--; return; } //Move South-West
            if (NodeAlreadyTouchingTargetPosition(targetX, targetY, X + 1, Y - 1)) { X++; Y--; return; } //Move South-East
        }

        private bool MoveToNextToHead(int targetX, int targetY)
        {
            if (AdjacentToNode(targetX, targetY, X+1, Y+1)) { X++; Y++; return true; } //Move North-East
            if (AdjacentToNode(targetX, targetY, X-1, Y+1)) { X--; Y++; return true; } //Move North-West
            if (AdjacentToNode(targetX, targetY, X-1, Y-1)) { X--; Y--; return true; } //Move South-West
            if (AdjacentToNode(targetX, targetY, X+1, Y-1)) { X++; Y--; return true; } //Move South-East

            return false;
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