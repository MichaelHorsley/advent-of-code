using System;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;
using System.Linq;

namespace _2022
{
    [TestFixture]
    public class Day10
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day10_Test.txt");

            Assert.AreEqual(13140, Day10Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day10.txt");

            Assert.AreEqual(16480, Day10Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day10_Test.txt");

            Day10Solution.PartTwo(input);
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day10.txt");

            Day10Solution.PartTwo(input);
        }
    }

    public class Day10Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var commands = input.Replace("\r", "").Split("\n").ToList();

            var x = 1;
            var cycle = 1;
            var sum = 0;

            foreach (var command in commands)
            {
                var list = command.Split(' ').ToList();

                switch (list[0])
                {
                    case "noop":
                        cycle++;

                        break;

                    case "addx":
                        for (int newCycle = 0; newCycle < 2; newCycle++)
                        {
                            cycle++;

                            if ((cycle - 20) % 40 == 0 && newCycle != 1)
                            {
                                sum += x * cycle;
                            }

                        }

                        x += int.Parse(list[1]);

                        break;
                }

                if ((cycle - 20) % 40 == 0)
                {
                    sum += x * cycle;
                }
            }

            return sum;
        }

        [Benchmark]
        public static void PartTwo(string input)
        {
            var commands = input.Replace("\r", "").Split("\n").ToList();

            var x = 1;
            var pixel = 0;

            foreach (var command in commands)
            {
                var list = command.Split(' ').ToList();

                switch (list[0])
                {
                    default:
                        Console.Write(pixel >= x - 1 && pixel <= x + 1 ? "#" : ".");

                        pixel++;

                        pixel = ResetLine(pixel);
                        break;

                    case "addx":
                        for (var newCycle = 0; newCycle < 2; newCycle++)
                        {
                            Console.Write(pixel >= x - 1 && pixel <= x + 1 ? "#" : ".");

                            pixel++;

                            pixel = ResetLine(pixel);
                        }

                        x += int.Parse(list[1]);
                        break;
                }
            }
        }

        private static int ResetLine(int pixel)
        {
            if (pixel % 40 == 0)
            {
                Console.WriteLine();
                pixel = 0;
            }

            return pixel;
        }
    }
}