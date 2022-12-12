using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using NUnit.Framework;

namespace _2022
{
    [TestFixture]
    public class Day7
    {
        [Test]
        public void PartOne_Test()
        {
            var input = FileHelper.GetInputFromFile("Day7_Test.txt");

            Assert.AreEqual(95437, Day7Solution.PartOne(input));
        }

        [Test]
        public void PartOne_Star()
        {
            var input = FileHelper.GetInputFromFile("Day7.txt");

            Assert.AreEqual(1844187, Day7Solution.PartOne(input));
        }

        [Test]
        public void PartTwo_Test()
        {
            var input = FileHelper.GetInputFromFile("Day7_Test.txt");

            Assert.AreEqual(24933642, Day7Solution.PartTwo(input));
        }

        [Test]
        public void PartTwo_Star()
        {
            var input = FileHelper.GetInputFromFile("Day7.txt");

            Assert.AreEqual(4978279, Day7Solution.PartTwo(input));
        }
    }

    public class Day7Solution
    {
        [Benchmark]
        public static int PartOne(string input)
        {
            var currentNode = CreateFileSystemStructure(input);

            var sizeList = new List<long>();

            WorkOutSizeDictionary(sizeList, currentNode);

            return (int)sizeList.Where(x => x < 100000).Sum();
        }

        [Benchmark]
        public static long PartTwo(string input)
        {
            const long fileSystemMaxSize = 70000000;
            const long spaceRequired = 30000000;

            var currentNode = CreateFileSystemStructure(input);

            var currentSpacedUsed = currentNode.GetSizeOfFolder();
            var currentSpaceFree = fileSystemMaxSize - currentSpacedUsed;

            var spacedNeededToFree = spaceRequired - currentSpaceFree;

            var sizeList = new List<long>();

            WorkOutSizeDictionary(sizeList, currentNode);

            return (int)sizeList.Where(x => x > spacedNeededToFree).OrderBy(x => x).First();
        }

        [Benchmark]
        private static FolderNode CreateFileSystemStructure(string input)
        {
            var commands = input.Replace("\r", "").Split("\n").Skip(1).ToList();

            FolderNode currentNode = new FolderNode { Name = "/", ParentFolderNode = null };

            foreach (var command in commands)
            {
                var chunkedCommand = command.Split(" ").ToList();

                switch (chunkedCommand[0])
                {
                    case "$":
                        switch (chunkedCommand[1])
                        {
                            case "ls":
                                break;
                            case "cd":
                                switch (chunkedCommand[2])
                                {
                                    case "..":
                                        currentNode = currentNode.ParentFolderNode;
                                        break;
                                    default:
                                        currentNode = currentNode.Folders.First(x => x.Name == chunkedCommand[2]);
                                        break;
                                }

                                break;
                        }

                        break;
                    case "dir":
                        currentNode.Folders.Add(new FolderNode
                        {
                            Name = chunkedCommand[1],
                            ParentFolderNode = currentNode
                        });
                        break;
                    default:
                        currentNode.Files.Add(new FileNode
                        {
                            Size = Convert.ToInt32(chunkedCommand[0])
                        });
                        break;
                }
            }

            while (currentNode.ParentFolderNode != null)
            {
                currentNode = currentNode.ParentFolderNode;
            }

            return currentNode;
        }

        [Benchmark]
        private static void WorkOutSizeDictionary(List<long> sizeDictionary, FolderNode currentNode)
        {
            var sizeOfFolder = currentNode.GetSizeOfFolder();

            sizeDictionary.Add(sizeOfFolder);

            currentNode.Folders.ForEach(x =>
            {
                WorkOutSizeDictionary(sizeDictionary, x);
            });
        }
    }

    public class FolderNode
    {
        public List<FolderNode> Folders { get; set; } = new List<FolderNode>();
        public List<FileNode> Files { get; set; } = new List<FileNode>();
        public string Name { get; set; }
        public FolderNode ParentFolderNode { get; set; }

        public long GetSizeOfFolder()
        {
            return Files.Select(x => x.Size).Sum() + Folders.Select(x => x.GetSizeOfFolder()).Sum();
        }
    }

    public class FileNode
    {
        public int Size {get; set; }
    }
}