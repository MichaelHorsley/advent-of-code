using System.Diagnostics;
using System.Linq;
using System.Numerics;
using _2021.Helpers;
using NUnit.Framework;

namespace _2021.Day14
{
    [TestFixture]
    public class Day14
    {
        private string _testInput = "";
        private string _challengeInput = "";

        [SetUp]
        public void SetUp()
        {
            _testInput = FileHelper.GetResourceFile("_2021.Day14.test-input.txt");
            _challengeInput = FileHelper.GetResourceFile("_2021.Day14.test-challenge.txt");
        }

        [Test]
        public void Star1_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim()).ToList();

            var dotsRemainingVisible = WorkOutPolymerChainAndSubtractLeastCommonFromMostCommon(list, 10);

            Assert.AreEqual(1588, dotsRemainingVisible);
        }

        [Test]
        public void Star1_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim()).ToList();
            var sum = WorkOutPolymerChainAndSubtractLeastCommonFromMostCommon(list, 10);
            Assert.AreEqual(2768, sum);
        }

        [Test]
        public void Star2_Test()
        {
            var list = _testInput.Split(Environment.NewLine).Select(x => x.Trim()).ToList();

            var dotsRemainingVisible = WorkOutPolymerChainAndSubtractLeastCommonFromMostCommon(list, 40);

            Assert.AreEqual(2188189693529, dotsRemainingVisible);
        }

        [Test]
        public void Star2_Challenge()
        {
            var list = _challengeInput.Split(Environment.NewLine).Select(x => x.Trim()).ToList();
            var sum = WorkOutPolymerChainAndSubtractLeastCommonFromMostCommon(list, 40);
            Assert.AreEqual(2914365137499, sum);
        }

        public ulong WorkOutPolymerChainAndSubtractLeastCommonFromMostCommon(List<string> input, int stepLimit)
        {
            var sequenceChain = input.First().ToArray().Select(x => x.ToString()).ToList();

            var sequenceInsertions = input.Skip(2).ToDictionary(key => key.Replace(" -> ", ",").Split(',')[0], value=> value.Replace(" -> ", ",").Split(',')[1]);

            Dictionary<string, ulong> frequencyDictionary = new Dictionary<string, ulong>();
            
            Dictionary<string, ulong> pairsDictionary = new Dictionary<string, ulong>();

            SetUpInitialPairsDictionary(sequenceChain, pairsDictionary);

            SetUpInitialFrequencyDictionary(sequenceChain, frequencyDictionary);

            for (int step = 0; step < stepLimit; step++)
            {
                var newPairsDictionary = new Dictionary<string, ulong>();

                foreach (KeyValuePair<string, ulong> keyValuePair in pairsDictionary)
                {
                    var oldKey = keyValuePair.Key;

                    if (sequenceInsertions.ContainsKey(oldKey))
                    {
                        var elementToInsert = sequenceInsertions[oldKey];

                        IncrementFrequencyDictionary(frequencyDictionary, elementToInsert, keyValuePair.Value);

                        var oldKeyElements = oldKey.ToArray();

                        var firstElementCombination = oldKeyElements[0] + elementToInsert;
                        var secondElementCombination = elementToInsert + oldKeyElements[1];

                        AddNewPairIncrementsToDictionary(newPairsDictionary, firstElementCombination, keyValuePair);
                        AddNewPairIncrementsToDictionary(newPairsDictionary, secondElementCombination, keyValuePair);
                    }
                }

                pairsDictionary = newPairsDictionary;
            }

            var orderedByFrequency = frequencyDictionary.OrderByDescending(x => x.Value);

            BigInteger bigNumber = new BigInteger();

            foreach (var keyValuePair in orderedByFrequency)
            {
                bigNumber += keyValuePair.Value;
            }

            return orderedByFrequency.First().Value - orderedByFrequency.Last().Value;
        }

        private static void SetUpInitialFrequencyDictionary(List<string> sequenceChain, Dictionary<string, ulong> frequencyDictionary)
        {
            for (int i = 0; i < sequenceChain.Count; i++)
            {
                string key = $"{sequenceChain[i]}";

                if (!frequencyDictionary.ContainsKey(key))
                {
                    frequencyDictionary.Add(key, 0);
                }

                frequencyDictionary[key] += 1;
            }
        }

        private static void SetUpInitialPairsDictionary(List<string> sequenceChain, Dictionary<string, ulong> pairsDictionary)
        {
            for (int i = 0; i < sequenceChain.Count - 1; i++)
            {
                string key = $"{sequenceChain[i]}{sequenceChain[i + 1]}";

                if (!pairsDictionary.ContainsKey(key))
                {
                    pairsDictionary.Add(key, 0);
                }

                pairsDictionary[key] += 1;
            }
        }

        private static void AddNewPairIncrementsToDictionary(Dictionary<string, ulong> newPairsDictionary, string newElementCombination, KeyValuePair<string, ulong> oldElementCombination)
        {
            if (!newPairsDictionary.ContainsKey(newElementCombination))
            {
                newPairsDictionary.Add(newElementCombination, (ulong)0);
            }

            newPairsDictionary[newElementCombination] += oldElementCombination.Value;
        }

        private static void IncrementFrequencyDictionary(Dictionary<string, ulong> frequencyDictionary, string elementToInsert, ulong value)
        {
            if (!frequencyDictionary.ContainsKey(elementToInsert))
            {
                frequencyDictionary.Add(elementToInsert, (ulong)0);
            }

            frequencyDictionary[elementToInsert] += value;
        }

        private static void WorkOutFrequencyFromSequence(int stepLimit, List<string> sequenceChain, Dictionary<string, string> sequenceInsertions, Dictionary<string, ulong> frequencyDictionary)
        {
            for (int currentStep = 0; currentStep < stepLimit; currentStep++)
            {
                for (int position = sequenceChain.Count - 2; position >= 0; position--)
                {
                    var key = $"{sequenceChain[position]}{sequenceChain[position + 1]}";

                    if (sequenceInsertions.ContainsKey(key))
                    {
                        var newElement = sequenceInsertions[key];

                        sequenceChain.Insert(position + 1, newElement);

                        if (!frequencyDictionary.ContainsKey(newElement))
                        {
                            frequencyDictionary.Add(newElement, (ulong)0);
                        }

                        frequencyDictionary[newElement] += 1;
                    }
                }

                Console.WriteLine("Step: " + currentStep);
            }
        }
    }
}