using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        //Not 6462 - too high
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day13.txt");

            Assert.AreEqual(6187, Day13Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day13_Test.txt");

            Assert.AreEqual(140, Day13Solution.PartTwo(input));
        }

        [TestCase("[1,1,3,1,1]", "[1,1,5,1,1]", true)]
        [TestCase("[[1],[2,3,4]]", "[[1],4]", true)]
        [TestCase("[9]", "[[8,7,6]]", false)]
        [TestCase("[[4,4],4,4]", "[[4,4],4,4,4]", true)]
        [TestCase("[7,7,7,7]", "[7,7,7]", false)]
        [TestCase("[]", "[3]", true)]
        [TestCase("[[[]]]", "[[]]", false)]
        [TestCase("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]", false)]
        public void PartOne_RowByRow(string left, string right, bool expected)
        {
            var pairInTheRightOrder = Day13Solution.PairInTheRightOrder(left, right);

            Assert.AreEqual(expected, pairInTheRightOrder);
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day13.txt");

            Assert.AreEqual(23520, Day13Solution.PartTwo(input));
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

        [TestCase("[1]", "[1,3]", true)]
        [TestCase("[1,3]", "[1]", false)]
        public void PartOne_BothValuesAreArrays_DifferentLengths(string left, string right, bool expected)
        {
            var pairInTheRightOrder = Day13Solution.PairInTheRightOrder(left, right);

            Assert.AreEqual(expected, pairInTheRightOrder);
        }

        [TestCase("[1]", "[[1,3]]", true)]
        public void PartOne_BothValuesAreArrays_IntVsArray(string left, string right, bool expected)
        {
            var pairInTheRightOrder = Day13Solution.PairInTheRightOrder(left, right);

            Assert.AreEqual(expected, pairInTheRightOrder);
        }

        [TestCase("[9,7,7,7]", "[9,7,7,7,2]", true)]
        [TestCase("[9,1,3,2,3]", "[9,1,3,2]", false)]
        [TestCase("[[],[1,[7]]]", "[[2,4,6,[[4]],0]]", true)]
        [TestCase("[[],[9,2,5,[],0]]", "[[7,[],9],[]]", true)]
        [TestCase("[[7,8,6,10],[8,[]]]", "[[7,[8],10],[5,4,[]]]", true)]
        [TestCase("[[],[]]", "[[8,2,[[9,2,8],[9,5,0,0,7],7,7]]]", true)]
        [TestCase("[[10,5,[[7]],[0],3],[[],1,2,8],[[7]]]", "[[7]]", false)]
        public void PartOne_AllThePairs(string left, string right, bool expected)
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
                    pairsInTheRightOrderCount += (index/3)+1;
                }
            }

            return pairsInTheRightOrderCount;
        }

        [Benchmark]
        public static int PartTwo(string input)
        {
            var rowsList = input.Replace("\r", "").Split("\n").ToList();

            var nodes = new List<Node>
            {
                GetNode("[[2]]"),
                GetNode("[[6]]")
            };

            for (var index = 0; index < rowsList.Count; index ++)
            {
                var row = rowsList[index];

                if (string.IsNullOrWhiteSpace(row))
                {
                    continue;
                }

                nodes.Add(GetNode(row));
            }

            nodes = BubbleSortNode(nodes);

            var list = nodes.Where(x => x.OriginalString.Equals("[[2]]") || x.OriginalString.Equals("[[6]]")).ToList();

            return (nodes.IndexOf(list[0])+1) * (nodes.IndexOf(list[1])+1);
        }

        private static List<Node> BubbleSortNode(List<Node> nodes)
        {
            var list = new List<Node>();

            foreach (var nodeToBeAdded in nodes)
            {
                if (list.Count == 0)
                {
                    list.Add(nodeToBeAdded);
                }
                else
                {
                    var added = false;
                    for (var index = 0; index < list.Count; index++)
                    {
                        var existingNode = list[index];

                        var insertBefore = (bool)CompareNodes(nodeToBeAdded, existingNode);

                        if (insertBefore)
                        {
                            list.Insert(index, nodeToBeAdded);
                            added = true;
                            break;
                        }
                    }

                    if (!added)
                    {
                        list.Add(nodeToBeAdded);
                    }
                }
            }

            return list;
        }

        public static bool PairInTheRightOrder(string leftRow, string rightRow)
        {
            var leftNode = GetNode(leftRow);
            var rightNode = GetNode(rightRow);

            return (bool)CompareNodes(leftNode, rightNode);
        }

        private static bool? CompareNodes(Node leftNode, Node rightNode)
        {
            var maxChildCount = new List<int> { leftNode.Children.Count, rightNode.Children.Count }.Max();

            for (int position = 0; position < maxChildCount; position++)
            {
                if (position + 1 > rightNode.Children.Count)
                {
                    return false;
                }

                if (position + 1 > leftNode.Children.Count)
                {
                    return true;
                }

                var leftNodeChild = leftNode.Children[position];
                var rightNodeChild = rightNode.Children[position];

                if (leftNodeChild.Type == NodeType.Value && rightNodeChild.Type == NodeType.Value)
                {
                    if (leftNodeChild.Value == rightNodeChild.Value)
                    {
                        continue;
                    }

                    return leftNodeChild.Value < rightNodeChild.Value;

                }
                else
                {
                    var compareNodes = CompareNodes(
                        leftNodeChild.Type == NodeType.Array ? leftNodeChild : new Node(leftNodeChild), 
                        rightNodeChild.Type == NodeType.Array ? rightNodeChild : new Node(rightNodeChild)
                    );

                    if (compareNodes == null)
                    {
                        continue;
                    }

                    return compareNodes;
                }
            }

            return null;
        }

        private static Node GetNode(string row)
        {
            var charArray = row.ToCharArray().Select(x => x.ToString()).Skip(1).ToList();

            var node = new Node(null, NodeType.Array, null, row);

            for (var position = 0; position < charArray.Count(); position++)
            {
                var character = charArray[position];

                if (character == "[")
                {
                    var newNode = new Node(node, NodeType.Array, null, row);

                    node.Children.Add(newNode);

                    node = newNode;

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

                int numberLength = 0;
                var sb = new StringBuilder();

                while (int.TryParse(charArray[position + numberLength], out _))
                {
                    sb.Append(charArray[position + numberLength]);
                    numberLength++;
                }

                node.Children.Add(new Node(node, NodeType.Value, int.Parse(sb.ToString()), row));
            }

            return node;
        }

        private class Node
        {
            public readonly string OriginalString;
            public readonly Node? Parent;
            public readonly NodeType Type;
            public readonly int? Value;

            public List<Node> Children { get; set; } = new List<Node>();

            public Node(Node parent, NodeType type, int? value, string originalString)
            {
                OriginalString = originalString;
                Parent = parent;
                Type = type;
                Value = value;
            }

            public Node(Node valueNodeToTurnIntoArray)
            {
                Parent = valueNodeToTurnIntoArray.Parent;
                Type = NodeType.Array;
                Children = new List<Node> { valueNodeToTurnIntoArray };
            }
        }
    }

    internal enum NodeType
    {
        Array,
        Value
    }
}