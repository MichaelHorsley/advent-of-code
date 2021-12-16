using _2021.Helpers;
using NUnit.Framework;

namespace _2021.Day12
{
    [TestFixture]
    public class Day12
    {
        private string _testInput;
        private string _challengeInput;

        [SetUp]
        public void SetUp()
        {
            _testInput = FileHelper.GetResourceFile("_2021.Day12.test-input.txt");
            _challengeInput = FileHelper.GetResourceFile("_2021.Day12.test-challenge.txt");
        }

        [Test]
        public void Star1_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = TotalPathsThroughCaves(list, false);

            Assert.AreEqual(10, sum);
        }

        [Test]
        public void Star1_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = TotalPathsThroughCaves(list, false);
            Assert.AreEqual(4885, sum);
        }

        [Test]
        public void Star2_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            var sum = TotalPathsThroughCaves(list, true);

            Assert.AreEqual(36, sum);
        }

        [Test]
        public void Star2_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim().Replace("  ", " ")).ToList();
            
            var sum = TotalPathsThroughCaves(list, true);
            
            Assert.AreEqual(117095, sum);
        }

        private ulong TotalPathsThroughCaves(List<string> nodeConnections, bool considerSmallCavesMoreThanOnce)
        {
            var nodeDictionary = new Dictionary<string, HashSet<string>>();

            foreach (var connection in nodeConnections)
            {
                var nodes = connection.Split('-').ToList();

                var keyNode = nodes[0];
                var destinationNode = nodes[1];

                if (!nodeDictionary.ContainsKey(keyNode))
                {
                    nodeDictionary.Add(keyNode, new HashSet<string>());
                }

                if (!nodeDictionary.ContainsKey(destinationNode))
                {
                    nodeDictionary.Add(destinationNode, new HashSet<string>());
                }

                nodeDictionary[keyNode].Add(nodes[1]);
                nodeDictionary[destinationNode].Add(nodes[0]);
            }

            var listOfRoutes = new List<List<string>>();

            CarryOn("start", nodeDictionary, listOfRoutes, new List<string>(), considerSmallCavesMoreThanOnce);

            return (ulong)listOfRoutes.Count;
        }

        private void CarryOn(string nodeName, Dictionary<string, HashSet<string>> nodeDictionary, List<List<string>> listOfRoutes, List<string> routeSoFar, bool considerSmallCavesMoreThanOnce)
        {
            routeSoFar.Add(nodeName);

            if (nodeName == "end")
            {
                listOfRoutes.Add(routeSoFar);
            }
            else
            {
                if (nodeDictionary.ContainsKey(nodeName))
                {
                    var nodesToMoveToNext = nodeDictionary[nodeName].ToList();

                    foreach (var node in nodesToMoveToNext)
                    {
                        if (node != "start" && !alreadyVisitedSmallCave(node, routeSoFar, considerSmallCavesMoreThanOnce))
                        {
                            CarryOn(node, nodeDictionary, listOfRoutes, new List<string>(routeSoFar), considerSmallCavesMoreThanOnce);
                        }
                    }
                }
            }
        }

        private bool alreadyVisitedSmallCave(string nodeToVisit, List<string> routeSoFar, bool considerSmallCavesMoreThanOnce)
        {
            if (routeSoFar.Contains(nodeToVisit))
            {
                var charAsNumber = nodeToVisit.ToCharArray().First();

                var isUpperCase = charAsNumber <= 'a';

                if (!isUpperCase)
                {
                    if (considerSmallCavesMoreThanOnce)
                    {
                        var grouped = routeSoFar.Where(x => !IsUpperCase(x)).GroupBy(x => x).ToList();

                        if (grouped.Any(x => x.Count() == 2))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (routeSoFar.Contains(nodeToVisit))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool IsUpperCase(string x)
        {
            return x.ToCharArray().First() <= 'a';
        }
    }
}
