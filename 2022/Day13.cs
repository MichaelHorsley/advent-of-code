using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day13
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day13_Test.txt");

            Assert.AreEqual(13, Day13Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day13.txt");

            Assert.AreEqual(0, Day13Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day13_Test.txt");

            Assert.AreEqual(0, Day13Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day13.txt");

            Assert.AreEqual(0, Day13Solution.PartTwo(input));
        }

        [TestCase("[1]", "[2]", true)]
        [TestCase("[3]", "[2]", false)]
        public void PartOne_BothValuesAreIntegers(string left, string right, bool expected)
        {
            var pairInTheRightOrder = Day13Solution.PairInTheRightOrder(left, right);

            Assert.AreEqual(expected, pairInTheRightOrder);
        }

        [TestCase("[1,2]", "[1,3]", true)]
        [TestCase("[1,3]", "[1,2]", false)]
        public void PartOne_BothValuesAreArrays_EqualLength(string left, string right, bool expected)
        {
            var pairInTheRightOrder = Day13Solution.PairInTheRightOrder(left, right);

            Assert.AreEqual(expected, pairInTheRightOrder);
        }

        [TestCase("[1]", "[1,3]", false)]
        [TestCase("[1,3]", "[1]", true)]
        public void PartOne_BothValuesAreArrays_DifferentLengths(string left, string right, bool expected)
        {
            var pairInTheRightOrder = Day13Solution.PairInTheRightOrder(left, right);

            Assert.AreEqual(expected, pairInTheRightOrder);
        }
    }

    public class Day13Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var rowsList = input.Replace("\r", "").Split("\n").ToList();

            var pairsInTheRightOrderCount = 0;

            for (var index = 0; index < rowsList.Count; index+=3)
            {
                var leftRow = rowsList[index];
                var rightRow = rowsList[index+1];

                if (PairInTheRightOrder(leftRow, rightRow))
                {
                    pairsInTheRightOrderCount += index+1;
                }
            }

            return pairsInTheRightOrderCount;
        }

        public static bool PairInTheRightOrder(string leftRow, string rightRow)
        {
            var leftNode = GetNode(leftRow);
            var rightNode = GetNode(rightRow);

            var maxChildCount = new List<int>{ leftNode.Children.Count, rightNode.Children.Count }.Max();

            for (int position = 0; position < maxChildCount; position++)
            {
                if (position + 1 > rightNode.Children.Count)
                {
                    return true;
                }

                if (position + 1 > leftNode.Children.Count)
                {
                    return false;
                }
                
                var leftNodeChild = leftNode.Children[position];
                var rightNodeChild = rightNode.Children[position];

                if (leftNodeChild.Value != rightNodeChild.Value)
                {
                    return leftNodeChild.Value < rightNodeChild.Value;
                }
            }

            return false;
        }

        private static Node GetNode(string row)
        {
            var charArray = row.ToCharArray().Select(x => x.ToString());

            var node = new Node(null, NodeType.Array, null);

            foreach (var character in charArray.Skip(1))
            {
                if (character == "[")
                {
                    continue;
                }

                if (character == ",")
                {
                    continue;
                }

                if (character == "]")
                {
                    if (node.Parent != null)
                    {
                        node = node.Parent;
                    }

                    continue;
                }

                node.Children.Add(new Node(node, NodeType.Value, int.Parse(character)));
            }

            return node;
        }

        [Benchmark]
        public static int PartTwo(string input)
        {
            //var heightMapLetters = input.Replace("\r", "").Split("\n").ToList();

            return 0;
        }

        private class Node
        {
            public readonly Node? Parent;
            public readonly NodeType Type;
            public readonly int? Value;

            public List<Node> Children { get; set; } = new List<Node>();

            public Node(Node parent, NodeType type, int? value)
            {
                Parent = parent;
                Type = type;
                Value = value;
            }
        }
    }

    internal enum NodeType
    {
        Array,
        Value
    }
}