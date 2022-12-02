using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day2
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day2_Test.txt");

            Assert.AreEqual(15, Day2Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day2.txt");

            Assert.AreEqual(13446, Day2Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day2_Test.txt");

            Assert.AreEqual(12, Day2Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day2.txt");

            Assert.AreEqual(13509, Day2Solution.PartTwo(input));
        }
    }

    public class Day2Solution
    {
        private static readonly Dictionary<RockPaperScissors, int> PlayerMoveScoreDictionary = new Dictionary<RockPaperScissors, int>
        {
            { RockPaperScissors.Rock, 1 },
            { RockPaperScissors.Paper, 2 },
            { RockPaperScissors.Scissors, 3 }
        };

        private static readonly Dictionary<string, RockPaperScissors> InputToPlayerMoveDictionary = new Dictionary<string, RockPaperScissors>
        {
            { "A", RockPaperScissors.Rock },
            { "B", RockPaperScissors.Paper },
            { "C", RockPaperScissors.Scissors },
            { "X", RockPaperScissors.Rock },
            { "Y", RockPaperScissors.Paper },
            { "Z", RockPaperScissors.Scissors }
        };

        static readonly Dictionary<RockPaperScissors, RockPaperScissors> LoseAgainstDictionary = new Dictionary<RockPaperScissors, RockPaperScissors>
        {
            {RockPaperScissors.Rock, RockPaperScissors.Scissors },
            {RockPaperScissors.Paper, RockPaperScissors.Rock },
            {RockPaperScissors.Scissors, RockPaperScissors.Paper }
        };

        static readonly Dictionary<RockPaperScissors, RockPaperScissors> WinAgainstDictionary = new Dictionary<RockPaperScissors, RockPaperScissors>
        {
            {RockPaperScissors.Rock, RockPaperScissors.Paper },
            {RockPaperScissors.Paper, RockPaperScissors.Scissors },
            {RockPaperScissors.Scissors, RockPaperScissors.Rock }
        };

        private enum RockPaperScissors
        {
            Rock,
            Paper,
            Scissors
        }

        public static long PartOne(string input)
        {
            var games = input.Split('\r').ToList();

            var sumOfGames = 0;

            foreach (var game in games)
            {
                var playerInputs = game.Replace("\r", "").Replace("\n", "").Split(' ').ToList();

                var firstPlayer = InputToPlayerMoveDictionary[playerInputs[0]];
                var secondPlayer = InputToPlayerMoveDictionary[playerInputs[1]];

                sumOfGames += GetGameScore(secondPlayer, firstPlayer);
            }

            return sumOfGames;
        }

        public static long PartTwo(string input)
        {
            var games = input.Split('\r').ToList();

            var sumOfGames = 0;

            foreach (var game in games)
            {
                var playerInputs = game.Replace("\r", "").Replace("\n", "").Split(' ').ToList();

                var firstPlayerMove = InputToPlayerMoveDictionary[playerInputs[0]];

                var intendedGameResult = playerInputs[1];

                RockPaperScissors secondPlayerMove = SetUpSecondPlayerMoveToGetIntendedGameResult(firstPlayerMove, intendedGameResult);

                sumOfGames += GetGameScore(secondPlayerMove, firstPlayerMove);
            }

            return sumOfGames;
        }

        private static RockPaperScissors SetUpSecondPlayerMoveToGetIntendedGameResult(RockPaperScissors firstPlayerMove, string intendedGameResult)
        {
            RockPaperScissors secondPlayerMove;

            if (intendedGameResult == "X") //player should lose against elf
            {
                secondPlayerMove = LoseAgainstDictionary[firstPlayerMove];
            }
            else if (intendedGameResult == "Y") //player should draw against elf
            {
                secondPlayerMove = firstPlayerMove;
            }
            else //player should win against elf
            {
                secondPlayerMove = WinAgainstDictionary[firstPlayerMove];
            }

            return secondPlayerMove;
        }

        private static int GetGameScore(RockPaperScissors secondPlayer, RockPaperScissors firstPlayer)
        {
            var gamescore = 0;

            gamescore += PlayerMoveScoreDictionary[secondPlayer];

            if (firstPlayer == secondPlayer)
            {
                gamescore += 3;
            }
            else
            {
                switch (firstPlayer)
                {
                    case RockPaperScissors.Rock:
                        if (secondPlayer == RockPaperScissors.Paper)
                        {
                            gamescore += 6;
                        }

                        break;
                    case RockPaperScissors.Paper:
                        if (secondPlayer == RockPaperScissors.Scissors)
                        {
                            gamescore += 6;
                        }

                        break;
                    case RockPaperScissors.Scissors:
                        if (secondPlayer == RockPaperScissors.Rock)
                        {
                            gamescore += 6;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return gamescore;
        }
    }
}