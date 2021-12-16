using _2021.Helpers;
using NUnit.Framework;

namespace _2021.Day13
{
    [TestFixture]
    public class Day13
    {
        private string _testInput;
        private string _challengeInput;

        [SetUp]
        public void SetUp()
        {
            _testInput = FileHelper.GetResourceFile("_2021.Day13.test-input.txt");
            _challengeInput = FileHelper.GetResourceFile("_2021.Day13.test-challenge.txt");
        }

        [Test]
        public void Star1_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();

            var dotsRemainingVisible = FoldOnceAndCountDots(list);

            Assert.AreEqual(17, dotsRemainingVisible);
        }

        [Test]
        public void Star1_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = FoldOnceAndCountDots(list);
            Assert.AreEqual(708, sum);
        }

        [Test]
        public void Star2_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var origami = FoldAndReturnOrigami(list);

            var maxX = origami.GetLength(0);
            var maxY = origami.GetLength(1);

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    Console.Write(origami[x,y] ?? ".");
                }

                Console.WriteLine();
            }

            //EBLUBRFH
        }

        private string[,] FoldAndReturnOrigami(List<string> origamiInstructions)
        {
            var instructionsStart = origamiInstructions.IndexOf("");

            List<string> hashMarkList = origamiInstructions.Take(instructionsStart).ToList();

            List<string> foldInstructions = origamiInstructions.Skip(instructionsStart + 1).Select(x => x.Replace("fold along ", "")).ToList();

            return GetFoldedOrigami(hashMarkList, foldInstructions);
        }

        private int FoldOnceAndCountDots(List<string> origamiInstructions)
        {
            var instructionsStart = origamiInstructions.IndexOf("");

            List<string> hashMarkList = origamiInstructions.Take(instructionsStart).ToList();

            List<string> foldInstructions = origamiInstructions.Skip(instructionsStart+1).Select(x => x.Replace("fold along ", "")).Take(1).ToList();

            var foldedOrigami = GetFoldedOrigami(hashMarkList, foldInstructions);

            var dotCount = 0;

            foreach (var s in foldedOrigami)
            {
                if (s is "#")
                {
                    dotCount += 1;
                }    
            }

            return dotCount;
        }

        private static string[,] GetFoldedOrigami(List<string> hashMarkList, List<string> foldInstructions)
        {
            var maxX = hashMarkList.Max(x => int.Parse(x.Split(',')[0]));
            var maxY = hashMarkList.Max(x => int.Parse(x.Split(',')[1]));

            var currentX = maxX + 1;
            var currentY = maxY + 1;

            var origamiPaperBeforeFolding = new string[currentX, currentY];

            MarkHashesOnPaper(hashMarkList, origamiPaperBeforeFolding);

            foreach (var foldInstruction in foldInstructions)
            {
                if (foldInstruction.Contains("y"))
                {
                    var foldedOrigamiPaper =
                        FoldY(foldInstruction, currentX, origamiPaperBeforeFolding, ref currentY, currentX);

                    origamiPaperBeforeFolding = foldedOrigamiPaper;
                }
                else
                {
                    var foldedOrigamiPaper = FoldX(foldInstruction, currentY, origamiPaperBeforeFolding, ref currentX);

                    origamiPaperBeforeFolding = foldedOrigamiPaper;
                }
            }

            return origamiPaperBeforeFolding;
        }

        private static string[,] FoldX(string foldInstruction, int currentY, string[,] origamiPaperBeforeFolding, ref int currentMaxX)
        {
            var foldLine = int.Parse(foldInstruction.Replace("x=", ""));

            var foldedOrigamiPaper = new string[foldLine, currentY];

            for (int y = 0; y < currentY; y++)
            {
                for (int x = 0; x < foldLine; x++)
                {
                    foldedOrigamiPaper[x, y] = origamiPaperBeforeFolding[x, y];
                }
            }

            for (int y = 0; y < currentY; y++)
            {
                for (int x = foldLine + 1; x < currentMaxX; x++)
                {
                    var targetX = foldLine - (x - foldLine);

                    var valueToCopy = origamiPaperBeforeFolding[x, y];

                    if (valueToCopy == "#")
                    {
                        foldedOrigamiPaper[targetX, y] = "#";
                    }
                }
            }

            currentMaxX = foldLine;
            return foldedOrigamiPaper;
        }

        private static string[,] FoldY(string foldInstruction, int currentX, string[,] origamiPaperBeforeFolding, ref int currentMaxY, int currentMaxX)
        {
            var foldLine = int.Parse(foldInstruction.Replace("y=", ""));

            var foldedOrigamiPaper = new string[currentX, foldLine];

            for (int y = 0; y < foldLine; y++)
            {
                for (int x = 0; x < currentX; x++)
                {
                    foldedOrigamiPaper[x, y] = origamiPaperBeforeFolding[x, y];
                }
            }

            for (int y = foldLine + 1; y < currentMaxY; y++)
            {
                var targetY = foldLine - (y - foldLine);

                for (int x = 0; x < currentMaxX; x++)
                {
                    var valueToCopy = origamiPaperBeforeFolding[x, y];

                    if (valueToCopy == "#")
                    {
                        foldedOrigamiPaper[x, targetY] = "#";
                    }
                }
            }

            currentMaxY = foldLine;

            return foldedOrigamiPaper;
        }

        private static void MarkHashesOnPaper(List<string> hashMarkList, string[,] origamiPaper)
        {
            foreach (var markPosition in hashMarkList)
            {
                var positions = markPosition.Split(',').ToList();

                origamiPaper[int.Parse(positions[0]), int.Parse(positions[1])] = "#";
            }
        }
    }
}