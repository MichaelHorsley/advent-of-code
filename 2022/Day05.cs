using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day05
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day05_Test.txt");

            Assert.AreEqual("CMZ", Day05Solution.PartOne(input));
        }

        [Benchmark]
        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day05.txt");

            Assert.AreEqual("ZWHVFWQWW", Day05Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day05_Test.txt");

            Assert.AreEqual("MCD", Day05Solution.PartTwo(input));
        }

        [Benchmark]
        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day05.txt");

            Assert.AreEqual("HZFZCCWWV", Day05Solution.PartTwo(input));
        }
    }

    public class Day05Solution
    {
        [Benchmark]
        public static string PartOne(string input)
        {
            var stackAndCommands = input.Replace("\n", "").Split('\r').ToList();

            var dictionaryOfStacks = SetUpTheStartingContainerStacks(stackAndCommands);

            var indexOf = stackAndCommands.IndexOf("");
            
            stackAndCommands.RemoveRange(0, indexOf+1);

            foreach (var command in stackAndCommands)
            {
                var row = command.Split(' ').ToList();

                var numberOfContainersToMove = int.Parse(row[1]);
                var initialContainerStack = row[3];
                var targetContainerStack = row[5];

                for (var numberOfMoves = 0; numberOfMoves < numberOfContainersToMove; numberOfMoves++)
                {
                    dictionaryOfStacks[targetContainerStack].Push(dictionaryOfStacks[initialContainerStack].Pop());
                }
            }

            var stringBuilder = new StringBuilder();

            foreach (var key in dictionaryOfStacks.Keys)
            {
                stringBuilder.Append(dictionaryOfStacks[key].Peek());
            }

            return stringBuilder.ToString();
        }

        private static Dictionary<string, Stack<string>> SetUpTheStartingContainerStacks(List<string> list)
        {
            var dictionaryOfStacks = new Dictionary<string, Stack<string>>();
            
            var indexOf = list.IndexOf("");

            var enumerable = list.Take(indexOf).Reverse().ToList();

            for (var index = 0; index < enumerable.Count; index++)
            {
                var stackOfContainers = enumerable[index];

                if (index == 0)
                {
                    var charArray = stackOfContainers.ToCharArray();

                    for (var position = 0; position < charArray.Length; position += 4)
                    {
                        var container = charArray[position + 1].ToString();

                        dictionaryOfStacks.Add(container, new Stack<string>());
                    }
                }
                else
                {
                    var charArray = stackOfContainers.ToCharArray();

                    for (var position = 0; position <= charArray.Length / 4; position++)
                    {
                        var container = charArray[position * 4 + 1].ToString();

                        if (!string.IsNullOrWhiteSpace(container))
                        {
                            dictionaryOfStacks[(position + 1).ToString()].Push(container);
                        }
                    }
                }
            }

            return dictionaryOfStacks;
        }

        [Benchmark]
        public static string PartTwo(string input)
        {
            var stackAndCommands = input.Replace("\n", "").Split('\r').ToList();

            var dictionaryOfStacks = SetUpTheStartingContainerStacks(stackAndCommands);

            var indexOf = stackAndCommands.IndexOf("");

            stackAndCommands.RemoveRange(0, indexOf + 1);

            foreach (var command in stackAndCommands)
            {
                var row = command.Split(' ').ToList();

                var numberOfContainersToMove = int.Parse(row[1]);
                var initialContainerStack = row[3];
                var targetContainerStack = row[5];

                var holdingArray = new List<string>();

                for (var numberOfMoves = 0; numberOfMoves < numberOfContainersToMove; numberOfMoves++)
                {
                    holdingArray.Add(dictionaryOfStacks[initialContainerStack].Pop());
                }

                holdingArray.Reverse();

                foreach (var container in holdingArray)
                {
                    dictionaryOfStacks[targetContainerStack].Push(container);
                }
            }

            var stringBuilder = new StringBuilder();

            foreach (var key in dictionaryOfStacks.Keys)
            {
                stringBuilder.Append(dictionaryOfStacks[key].Peek());
            }

            return stringBuilder.ToString();
        }
    }
}