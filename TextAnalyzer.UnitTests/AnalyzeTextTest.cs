using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using static TextAnalyzer.UniqueWords;

namespace TextAnalyzer.UnitTests
{
    [TestClass]
    public class AnalyzeTextTest
    {
        #region Properties

        /// <summary>
        /// Cache of the setup for test cases so that they only need to be run once.
        /// </summary>
        private List<Tuple<TestCase, AnalyzeText>> TextAnalyses
        {
            get
            {
                if (_textAnalyses == null)
                {
                    SetUpTestCases();
                }
                return _textAnalyses;
            }
        }

        /// <summary>
        /// Cached value of TextAnalyses.
        /// </summary>
        private List<Tuple<TestCase, AnalyzeText>> _textAnalyses;

        #endregion

        #region Tests

        [TestMethod]
        public void TotalWordCountTest()
        {
            int totalWordCount;

            foreach (Tuple<TestCase, AnalyzeText> test in TextAnalyses)
            {
                totalWordCount = test.Item2.TotalWordCount;
                Assert.IsTrue(totalWordCount == test.Item1.TotalWordCount, test.Item1.Text + 
                    " - Result: " + totalWordCount + " - Correct Answer: " + test.Item1.TotalWordCount);
            }
        }

        [TestMethod]
        public void UniqueWordCountTest()
        {
            int uniqueWordCount;

            foreach (Tuple<TestCase, AnalyzeText> test in TextAnalyses)
            {
                uniqueWordCount = test.Item2.UniqueWordCount;
                Assert.IsTrue(uniqueWordCount == test.Item1.UniqueWordCount, test.Item1.Text +
                    " - Result: " + uniqueWordCount + " - Correct Answer: " + test.Item1.UniqueWordCount);
            }
        }

        [TestMethod]
        public void UniqueWordsTest()
        {
            List<string> uniqueWords;

            foreach (Tuple<TestCase, AnalyzeText> test in TextAnalyses)
            {
                uniqueWords = test.Item2.UniqueWords;
                Assert.IsTrue(UniqueWordsAreEqual(uniqueWords, test.Item1.UniqueWords), test.Item1.Text);
            }
        }

        [TestMethod]
        public void UniqueWordCountsTest()
        {
            List<UniqueWord> uniqueWordCounts;

            foreach (Tuple<TestCase, AnalyzeText> test in TextAnalyses)
            {
                uniqueWordCounts = test.Item2.UniqueWordCounts;
                Assert.IsTrue(UniqueWordCountsAreEqual(uniqueWordCounts, test.Item1.UniqueWordCounts), test.Item1.Text);
            }
        }

        #endregion

        #region Non-testing methods

        /// <summary>
        /// Determines if two lists of strings contain the same strings in the same order.
        /// </summary>
        /// <param name="wordsA">First list of strings.</param>
        /// <param name="wordsB">Second list of strings.</param>
        /// <returns>True if both lists of strings contain the same strings in the same order.</returns>
        private bool UniqueWordsAreEqual(List<string> wordsA, List<string> wordsB)
        {
            if (wordsA.Count != wordsB.Count)
            {
                return false;
            }

            for (int i = 0; i < wordsA.Count; i++)
            {
                if (wordsA[i] != wordsB[i])
                {
                    return false;
                }
            }
            
            return true;
        }

        /// <summary>
        /// Determines if two lists of word counts countain the same words in the same order with the same word counts.
        /// </summary>
        /// <param name="wordsA">First list of strings.</param>
        /// <param name="wordsB">Second list of strings.</param>
        /// <returns>True if both lists of word counts contain the same words in the same order.</returns>
        private bool UniqueWordCountsAreEqual(List<UniqueWord> wordsA, List<UniqueWord> wordsB)
        {
            if (wordsA.Count != wordsB.Count)
            {
                return false;
            }

            for (int i = 0; i < wordsA.Count; i++)
            {
                if (!wordsA[i].Equals(wordsB[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Prepares an instance of AnalyzeText with each test case to be used by unit tests.
        /// </summary>
        private void SetUpTestCases()
        {
            _textAnalyses = new List<Tuple<TestCase, AnalyzeText>>();
            Tuple<TestCase, AnalyzeText> testAnswer;
            AnalyzeText textAnalysis;
            List<TestCase> testCases = RetrieveTestCases();
            foreach (TestCase testCase in testCases)
            {
                textAnalysis = new AnalyzeText(testCase.Text);
                testAnswer = new Tuple<TestCase, AnalyzeText>(testCase, textAnalysis);
                _textAnalyses.Add(testAnswer);
            }
        }

        /// <summary>
        /// Retrieves test cases from AnalyzeTextTestCases.xml.
        /// </summary>
        /// <returns></returns>
        private List<TestCase> RetrieveTestCases()
        {
            List<string> uniqueWords;
            List<UniqueWord> wordCounts;
            TestCase testCase;
            var testCases = new List<TestCase>();

            //Hello World!
            uniqueWords = new List<string>(new string[] { "Hello", "World" });
            wordCounts = new List<UniqueWord>(new UniqueWord[] { new UniqueWord("Hello", 1), new UniqueWord("World", 1) });
            testCase = new TestCase(2, 2, uniqueWords, wordCounts, "Hello World!");
            testCases.Add(testCase);

            //
            uniqueWords = new List<string>(new string[0]);
            wordCounts = new List<UniqueWord>(new UniqueWord[0]);
            testCase = new TestCase(0, 0, uniqueWords, wordCounts, "");
            testCases.Add(testCase);

            //He said, "He said he is."
            uniqueWords = new List<string>(new string[] { "he", "He", "is", "said"});
            wordCounts = new List<UniqueWord>(new UniqueWord[] { new UniqueWord("he", 1), new UniqueWord("He", 2), new UniqueWord("is", 1), new UniqueWord("said", 2) });
            testCase = new TestCase(6, 4, uniqueWords, wordCounts, "He said, \"He said he is.\"");
            testCases.Add(testCase);

            //hyphenated-word word
            uniqueWords = new List<string>(new string[] { "hyphenated-word", "word" });
            wordCounts = new List<UniqueWord>(new UniqueWord[] { new UniqueWord("hyphenated-word", 1), new UniqueWord("word", 1) });
            testCase = new TestCase(2, 2, uniqueWords, wordCounts, "hyphenated-word word");
            testCases.Add(testCase);

            //line
            //line
            //taboo
            //
            //line
            uniqueWords = new List<string>(new string[] { "line", "taboo" });
            wordCounts = new List<UniqueWord>(new UniqueWord[] { new UniqueWord("line", 3), new UniqueWord("taboo", 1) });
            testCase = new TestCase(4, 2, uniqueWords, wordCounts, "line\nline\rtaboo\r\nline");
            testCases.Add(testCase);

            //we're aren't ain't were
            uniqueWords = new List<string>(new string[] { "ain\'t", "aren\'t", "were", "we\'re" });
            wordCounts = new List<UniqueWord>(new UniqueWord[] { new UniqueWord("ain\'t", 1), new UniqueWord("aren\'t", 1), new UniqueWord("were", 1), new UniqueWord("we\'re", 1) });
            testCase = new TestCase(4, 4, uniqueWords, wordCounts, "we\'re aren\'t ain\'t were");
            testCases.Add(testCase);

            return testCases;
        }

        #endregion

        #region Structs

        /// <summary>
        /// A single test case.
        /// </summary>
        internal struct TestCase
        {
            internal string Text;
            internal int TotalWordCount;
            internal int UniqueWordCount;
            internal List<string> UniqueWords;
            internal List<UniqueWord> UniqueWordCounts;

            internal TestCase(int totalWordCount, int uniqueWordCount, List<string> uniqueWords, List<UniqueWord> uniqueWordCounts, string text)
            {
                Text = text;
                TotalWordCount = totalWordCount;
                UniqueWordCount = uniqueWordCount;
                UniqueWords = uniqueWords;
                UniqueWordCounts = uniqueWordCounts;
            }
        }

        #endregion
    }
}
