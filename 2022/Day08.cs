using BenchmarkDotNet.Attributes;
using NUnit.Framework;
using System.Linq;

namespace _2022
{
    [TestFixture]
    public class Day08
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day08_Test.txt");

            Assert.AreEqual(21, Day08Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day08.txt");

            Assert.AreEqual(1698, Day08Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day08_Test.txt");

            Assert.AreEqual(8, Day08Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day08.txt");

            Assert.AreEqual(672280, Day08Solution.PartTwo(input));
        }
    }

    public class Day08Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var treeHeightRows = input.Replace("\r", "").Split("\n").ToList();

            var xLength = treeHeightRows.First().Length;
            var yLength = treeHeightRows.Count;

            var treeHeightArray = new TreeHeight[xLength, yLength];

            for (var yIndex = 0; yIndex < yLength; yIndex++)
            {
                var treeHeightRow = treeHeightRows[yIndex];
                var list = treeHeightRow.ToCharArray().Select(x => int.Parse(x.ToString())).ToList();

                for (var xIndex = 0; xIndex < list.Count; xIndex++)
                {
                    var treeHeight = list[xIndex];

                    treeHeightArray[xIndex, yIndex] = new TreeHeight { Height = treeHeight };
                }
            }

            //check from above
            for (var x = 0; x < xLength; x++)
            {
                var maxHeight = -1;

                for (var y = 0; y < yLength; y++)
                {
                    var tree = treeHeightArray[x, y];

                    if (tree.Height > maxHeight)
                    {
                        tree.VisibleAtAll = true;
                        maxHeight = tree.Height;
                    }
                }
            }

            //check from below
            for (var x = 0; x < xLength; x++)
            {
                var maxHeight = -1;

                for (var y = yLength - 1; y >= 0; y--)
                {
                    var tree = treeHeightArray[x, y];

                    if (tree.Height > maxHeight)
                    {
                        tree.VisibleAtAll = true;
                        maxHeight = tree.Height;
                    }
                }
            }

            //check from the left
            for (var y = 0; y < yLength; y++)
            {
                var maxHeight = -1;

                for (var x = 0; x < xLength; x++)
                {
                    var tree = treeHeightArray[x, y];

                    if (tree.Height > maxHeight)
                    {
                        tree.VisibleAtAll = true;
                        maxHeight = tree.Height;
                    }
                }
            }

            //check from the right
            for (var y = 0; y < yLength; y++)
            {
                var maxHeight = -1;

                for (var x = xLength-1; x >= 0; x--)
                {
                    var tree = treeHeightArray[x, y];

                    if (tree.Height > maxHeight)
                    {
                        tree.VisibleAtAll = true;
                        maxHeight = tree.Height;
                    }
                }
            }

            return CountVisible(treeHeightArray);
        }

        private static int CountVisible(TreeHeight[,] treeHeightArray)
        {
            return treeHeightArray.Cast<TreeHeight>().Count(treeHeight => treeHeight.VisibleAtAll);
        }

        [Benchmark]
        public static long PartTwo(string input)
        {
            var treeHeightRows = input.Replace("\r", "").Split("\n").ToList();

            var xLength = treeHeightRows.First().Length;
            var yLength = treeHeightRows.Count;

            var treeHeightArray = new TreeHeight[xLength, yLength];

            for (var yIndex = 0; yIndex < yLength; yIndex++)
            {
                var treeHeightRow = treeHeightRows[yIndex];
                var list = treeHeightRow.ToCharArray().Select(x => int.Parse(x.ToString())).ToList();

                for (var xIndex = 0; xIndex < list.Count; xIndex++)
                {
                    var treeHeight = list[xIndex];

                    treeHeightArray[xIndex, yIndex] = new TreeHeight { Height = treeHeight };
                }
            }

            for (var overallGridX = 0; overallGridX < xLength; overallGridX++)
            {
                for (var overallGridY = 0; overallGridY < yLength; overallGridY++)
                {
                    var tree = treeHeightArray[overallGridX, overallGridY];
                    
                    var numberOfTreesLookingUp = LookUp(overallGridY, treeHeightArray, overallGridX, tree);
                    var numberOfTreesLookingDown = LookDown(overallGridY, yLength, treeHeightArray, overallGridX, tree);
                    var numberOfTreesLookingRight = LookRight(overallGridX, xLength, treeHeightArray, overallGridY, tree);
                    var numberOfTreesLookingLeft = LookLeft(overallGridX, treeHeightArray, overallGridY, tree);

                    tree.ScenicValue = numberOfTreesLookingUp * numberOfTreesLookingDown * numberOfTreesLookingRight * numberOfTreesLookingLeft;
                }
            }

            return treeHeightArray.Cast<TreeHeight>().Max(treeHeight => treeHeight.ScenicValue);
        }

        private static int LookLeft(int overallGridX, TreeHeight[,] treeHeightArray, int overallGridY, TreeHeight tree)
        {
            var numberOfTrees = 0;

            for (var tempX = overallGridX - 1; tempX >= 0; tempX--)
            {
                var treeAbove = treeHeightArray[tempX, overallGridY];

                numberOfTrees++;

                if (treeAbove.Height >= tree.Height)
                {
                    break;
                }
            }

            return numberOfTrees;
        }

        private static int LookRight(int overallGridX, int xLength, TreeHeight[,] treeHeightArray, int overallGridY, TreeHeight tree)
        {
            var numberOfTrees = 0;

            for (var tempX = overallGridX + 1; tempX < xLength; tempX++)
            {
                var treeAbove = treeHeightArray[tempX, overallGridY];

                numberOfTrees++;

                if (treeAbove.Height >= tree.Height)
                {
                    break;
                }
            }

            return numberOfTrees;
        }

        private static int LookDown(int overallGridY, int yLength, TreeHeight[,] treeHeightArray, int overallGridX, TreeHeight tree)
        {
            var numberOfTrees = 0;

            for (var tempY = overallGridY + 1; tempY < yLength; tempY++)
            {
                var treeAbove = treeHeightArray[overallGridX, tempY];
                
                numberOfTrees++;

                if (treeAbove.Height >= tree.Height)
                {
                    break;
                }
            }

            return numberOfTrees;
        }

        private static int LookUp(int overallGridY, TreeHeight[,] treeHeightArray, int overallGridX, TreeHeight tree)
        {
            var numberOfTrees = 0;

            for (var tempY = overallGridY - 1; tempY >= 0; tempY--)
            {
                var treeAbove = treeHeightArray[overallGridX, tempY];

                numberOfTrees++;

                if (treeAbove.Height >= tree.Height)
                {
                    break;
                }
            }

            return numberOfTrees;
        }
    }

    public class TreeHeight
    {
        public int Height { get; set; }
        public bool VisibleAtAll { get; set; }
        public int ScenicValue { get; set; }
    }
}