using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;
using System.Linq;
using System.Numerics;

namespace _2022
{
    [TestFixture]
    public class Day11
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day11_Test.txt");

            Assert.AreEqual(10605, Day11Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day11.txt");

            Assert.AreEqual(57838, Day11Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day11_Test.txt");

            Assert.AreEqual((BigInteger)2713310158, Day11Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day11.txt");

            Assert.AreEqual((BigInteger)15050382231, Day11Solution.PartTwo(input));
        }
    }

    public class Day11Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var monkeyText = input.Replace("\r", "").Split("\n").ToList();

            var monkeys = new List<Monkey>();

            GetMonkeysWithItems(monkeyText, monkeys);

            for (int round = 0; round < 20; round++)
            {
                for (var index = 0; index < monkeys.Count; index++)
                {
                    var monkey = monkeys[index];

                    foreach (var monkeyItem in monkey.Items)
                    {
                        var itemWorryLevel = monkey.WorryOperation(monkeyItem);

                        var itemNewWorryLevel = itemWorryLevel / 3;

                        var targetMonkey = itemNewWorryLevel % monkey.DivisibleBy == 0 
                            ? monkey.TargetMonkeyIfTrue 
                            : monkey.TargetMonkeyIfFalse;

                        monkeys[targetMonkey].Items.Add((ulong)itemNewWorryLevel);
                        monkey.NumberOfItemsInspected++;
                    }

                    monkey.Items = new List<BigInteger>();
                }
            }

            var ints = monkeys.Select(x => x.NumberOfItemsInspected).OrderByDescending(x => x).Take(2).ToList();


            return ints[0] * ints[1];
        }

        [Benchmark]
        public static BigInteger PartTwo(string input)
        {
            var monkeyText = input.Replace("\r", "").Split("\n").ToList();

            var monkeys = new List<Monkey>();

            GetMonkeysWithItems(monkeyText, monkeys);

            var lowestCommonDonomitor = monkeys.Select(x => x.DivisibleBy).Aggregate((a, x) => a * x);

            for (int round = 0; round < 10000; round++)
            {
                for (var index = 0; index < monkeys.Count; index++)
                {
                    var monkey = monkeys[index];

                    foreach (var monkeyItem in monkey.Items)
                    {
                        var itemWorryLevel = monkey.WorryOperation(monkeyItem);

                        BigInteger.DivRem(itemWorryLevel, monkey.DivisibleBy, out var remainder);

                        var targetMonkey = remainder == 0
                            ? monkey.TargetMonkeyIfTrue
                            : monkey.TargetMonkeyIfFalse;

                        if (itemWorryLevel >= lowestCommonDonomitor)
                        {
                            itemWorryLevel %= lowestCommonDonomitor;
                        }

                        monkeys[targetMonkey].Items.Add(itemWorryLevel);
                        monkey.NumberOfItemsInspected++;
                    }

                    monkey.Items = new List<BigInteger>();
                }
            }

            var ints = monkeys.Select(x => x.NumberOfItemsInspected).OrderByDescending(x => x).Take(2).ToList();

            var i = (ulong)((long)ints[0] * (long)ints[1]);
            return i;
        }

        private static void GetMonkeysWithItems(List<string> monkeyText, List<Monkey> monkeys)
        {
            Monkey currentMonkey = null;

            var monkeyCommandPosition = 0;

            foreach (var monkeyInput in monkeyText)
            {
                switch (monkeyCommandPosition)
                {
                    case 0:
                        currentMonkey = new Monkey();
                        break;
                    case 1:
                        currentMonkey.Items = monkeyInput.Split(':')[1].Split(',').Select(BigInteger.Parse).ToList();
                        break;
                    case 2:
                        var newWorry = monkeyInput.Split('=')[1].Trim();
                        var operation = newWorry.ToCharArray()[4].ToString();
                        var operationTarget = newWorry.Split(operation)[1].Trim();
                        currentMonkey.WorryOperation = CurrentMonkeyWorryOperation(operation, operationTarget);
                        break;
                    case 3:
                        var testDivisbleBy = ulong.Parse(monkeyInput.Split("by")[1].Trim());
                        currentMonkey.DivisibleBy = testDivisbleBy;
                        break;
                    case 4:
                        currentMonkey.TargetMonkeyIfTrue = int.Parse(monkeyInput.Split("monkey")[1].Trim());
                        break;
                    case 5:
                        currentMonkey.TargetMonkeyIfFalse = int.Parse(monkeyInput.Split("monkey")[1].Trim());
                        break;
                }

                monkeyCommandPosition++;

                if (monkeyCommandPosition == 7)
                {
                    monkeyCommandPosition = 0;
                    monkeys.Add(currentMonkey);
                }
            }

            monkeys.Add(currentMonkey);
        }

        private static Func<BigInteger, BigInteger> CurrentMonkeyWorryOperation(string operation, string operationTarget)
        {
            if (operationTarget == "old")
            {
                if (operation == "*")
                {
                    return item => BigInteger.Multiply(item, item);
                }
                else
                {
                    return item => BigInteger.Add(item, item);
                }
            }
            else
            {
                var converted = BigInteger.Parse(operationTarget);

                if (operation == "*")
                {
                    return item => BigInteger.Multiply(item, converted);
                }
                else
                {
                    return item => BigInteger.Add(item, converted);
                }
            }
        }

        private class Monkey
        {
            public List<BigInteger> Items { get; set; } = new List<BigInteger>();
            public BigInteger DivisibleBy { get; set; }
            public Func<BigInteger, BigInteger> WorryOperation;
            public int TargetMonkeyIfTrue;
            public int TargetMonkeyIfFalse;
            public int NumberOfItemsInspected = 0;
        }
    }
}