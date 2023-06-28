using _2021.Helpers;
using NUnit.Framework;

namespace _2021.Day4
{
    [TestFixture]
    public class Day4
    {
        private string _testInput;
        private string _challengeInput;

        [SetUp]
        public void SetUp()
        {
            _testInput = FileHelper.GetResourceFile("_2021.Day4.test-input.txt");
            _challengeInput = FileHelper.GetResourceFile("_2021.Day4.test-challenge.txt");
        }

        [Test]
        public void Star1_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = PlayGameAndFindSumOfUncalledNumbersOnWinningBoard(list);

            Assert.AreEqual(4512, sum);
        }

        [Test]
        public void Star1_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = PlayGameAndFindSumOfUncalledNumbersOnWinningBoard(list);

            Assert.AreEqual(16716, sum);
        }

        [Test]
        public void Star2_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = PlayGameAndFindSumOfUncalledNumbersOnLosingBoard(list);

            Assert.AreEqual(1924, sum);
        }

        [Test]
        public void Star2_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = PlayGameAndFindSumOfUncalledNumbersOnLosingBoard(list);

            Assert.AreEqual(4880, sum);
        }

        private static int PlayGameAndFindSumOfUncalledNumbersOnWinningBoard(List<string> list)
        {
            var numbersCalled = list.First().Split(',').Select(int.Parse).ToList();

            var bingoBoardsAsList = list.Skip(2).ToList();

            var listOfListsOfStrings = new List<List<string>>
            {
                new()
            };

            foreach (var row in bingoBoardsAsList)
            {
                if (row == "")
                {
                    listOfListsOfStrings.Add(new List<string>());
                }
                else
                {
                    listOfListsOfStrings.Last().Add(row);
                }
            }

            List<BingoBoard> boards = new List<BingoBoard>();

            foreach (var listOfListsOfString in listOfListsOfStrings)
            {
                boards.Add(new BingoBoard(listOfListsOfString));
            }

            int sum = 0;
            bool shouldBreak = false;

            foreach (var numberToCall in numbersCalled)
            {
                foreach (var bingoBoard in boards)
                {
                    var hasWon = bingoBoard.CallNumber(numberToCall);

                    if (hasWon)
                    {
                        sum = bingoBoard.SumAllUnmarked() * numberToCall;
                        shouldBreak = true;
                        break;
                    }
                }

                if (shouldBreak)
                {
                    break;
                }
            }

            return sum;
        }
        
        private static int PlayGameAndFindSumOfUncalledNumbersOnLosingBoard(List<string> list)
        {
            var numbersCalled = list.First().Split(',').Select(int.Parse).ToList();

            var bingoBoardsAsList = list.Skip(2).ToList();

            var listOfListsOfStrings = new List<List<string>>
            {
                new()
            };

            foreach (var row in bingoBoardsAsList)
            {
                if (row == "")
                {
                    listOfListsOfStrings.Add(new List<string>());
                }
                else
                {
                    listOfListsOfStrings.Last().Add(row);
                }
            }

            List<BingoBoard> boards = new List<BingoBoard>();

            foreach (var listOfListsOfString in listOfListsOfStrings)
            {
                boards.Add(new BingoBoard(listOfListsOfString));
            }

            int sum = 0;
            bool shouldBreak = false;
            bool shouldRemoveBoard = false;

            List<BingoBoard> bingoBoardToRemove = new List<BingoBoard>();

            foreach (var numberToCall in numbersCalled)
            {
                foreach (var bingoBoard in boards)
                {
                    var hasWon = bingoBoard.CallNumber(numberToCall);

                    if (hasWon)
                    {
                        if (boards.Count == 1)
                        {
                            sum = bingoBoard.SumAllUnmarked() * numberToCall;
                            shouldBreak = true;
                        }
                        else
                        {
                            bingoBoardToRemove.Add(bingoBoard);
                            shouldRemoveBoard = true;
                        }
                    }

                    if (shouldBreak)
                    {
                        break;
                    }
                }

                if (shouldRemoveBoard)
                {
                    foreach (var bingoBoard in bingoBoardToRemove)
                    {
                        boards.Remove(bingoBoard);
                    }

                    shouldRemoveBoard = false;
                }

                if (shouldBreak)
                {
                    break;
                }
            }

            return sum;
        }
    }

    public class BingoBoard
    {
        private BoardSquare[,] board = new BoardSquare[5,5];
        private List<int> numbers = new List<int>();

        public BingoBoard(List<string> lists)
        {
            for (int x = 0; x < lists.Count; x++)
            {
                var list = lists[x].Split(" ").ToList();

                for (int y = 0; y < list.Count; y++)
                {
                    var numberToAdd = int.Parse(list[y]);

                    board[x,y] = new BoardSquare(numberToAdd);

                    numbers.Add(numberToAdd);
                }
            }
        }

        public bool CallNumber(int numberToCall)
        {
            if (numbers.Contains(numberToCall))
            {
                foreach (var boardSquare in board)
                {
                    if (boardSquare.numberOnBoard == numberToCall)
                    {
                        boardSquare.beenCalled = true;
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    if (CheckRows(i))
                    {
                        return true;
                    }

                    if (CheckColumns(i))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckColumns(int position)
        {
            bool allBeenCalled = true;

            for (int i = 0; i < 5; i++)
            {
                if (!board[i, position].beenCalled)
                {
                    allBeenCalled = false;

                    break;
                }
            }

            return allBeenCalled;
        }

        private bool CheckRows(int position)
        {
            bool allBeenCalled = true;
            
            for (int i = 0; i < 5; i++)
            {
                if (!board[position, i].beenCalled)
                {
                    allBeenCalled = false;

                    break;
                }
            }

            return allBeenCalled;
        }

        public int SumAllUnmarked()
        {
            int sum = 0;

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    var boardSquare = board[x, y];

                    if (!boardSquare.beenCalled)
                    {
                        sum += boardSquare.numberOnBoard;
                    }
                }
            }

            return sum;
        }
    }

    internal class BoardSquare
    {
        public int numberOnBoard { get; set; }
        public bool beenCalled { get; set; }

        public BoardSquare(int numberToAdd)
        {
            numberOnBoard = numberToAdd;
            beenCalled = false;
        }
    }
}
