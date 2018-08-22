using System;
using System.Collections.Generic;
using System.Text;

namespace Kingsley.TextAnalyzer
{
    public class UniqueWords
    {
        #region Properties

        /// <summary>
        /// Alphabetized list of unique words.
        /// </summary>
        internal List<string> AlphabetizedWords
        {
            get
            {
                if (_alphabetizedWords == null)
                {
                    _alphabetizedWords = AlphabetizedWordList();
                }
                return _alphabetizedWords;
            }
        }

        /// <summary>
        /// Cached value for AlphabetizedWords.
        /// </summary>
        private List<string> _alphabetizedWords;

        /// <summary>
        /// Alphabetized list of unique words with occurrence counts.
        /// </summary>
        internal List<UniqueWord> AlphabetizedWordCounts
        {
            get
            {
                if (_alphabetizedWordCounts == null)
                {
                    _alphabetizedWordCounts = AlphabetizedWordCountList();
                }
                return _alphabetizedWordCounts;
            }
        }

        /// <summary>
        /// Cached value for AlphabetizedWordCounts.
        /// </summary>
        private List<UniqueWord> _alphabetizedWordCounts;

        /// <summary>
        /// The total number of words including duplicates.
        /// </summary>
        internal int TotalWordCount { get; private set; }

        /// <summary>
        /// The number of unique words in the collection.
        /// </summary>
        internal int UniqueWordCount { get { return wordCounts.Count; } }

        /// <summary>
        /// Number of occurrences of each unique word.
        /// </summary>
        private Dictionary<string, int> wordCounts { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Store a collection of words and compute stats for the collection.
        /// </summary>
        internal UniqueWords()
        {
            wordCounts = new Dictionary<string, int>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a word to the collection of words.
        /// </summary>
        /// <param name="word"></param>
        internal void AddWord(string word)
        {
            if (wordCounts.ContainsKey(word))
            {
                wordCounts[word] += 1;
            }
            else
            {
                wordCounts.Add(word, 1);
            }

            TotalWordCount++;

            //Remove the cached sorted lists so that they are recreated on demand.
            _alphabetizedWords = null;
            _alphabetizedWordCounts = null;
        }

        /// <summary>
        /// Creates an alphabetized list of words only from the unique words.
        /// </summary>
        /// <returns></returns>
        private List<string> AlphabetizedWordList()
        {
            List<string> words = new List<string>(wordCounts.Keys);
            words.Sort();
            return words;
        }

        /// <summary>
        /// Creates an alphabetized list of words + word counts from the unique words.
        /// </summary>
        /// <returns></returns>
        private List<UniqueWord> AlphabetizedWordCountList()
        {
            UniqueWord uniqueWord;
            int wordCount = 0;
            List<UniqueWord> wordCountList = new List<UniqueWord>();

            foreach (string word in AlphabetizedWords)
            {
                wordCounts.TryGetValue(word, out wordCount);
                uniqueWord = new UniqueWord(word, wordCount);
                wordCountList.Add(uniqueWord);
            }

            return wordCountList;
        }

        #endregion

        #region Structs

        /// <summary>
        /// A single unique word and the number of times that it occurs.
        /// </summary>
        public struct UniqueWord
        {
            public string Word;
            public int Count;

            public UniqueWord(string word, int count)
            {
                Word = word;
                Count = count;
            }

            /// <summary>
            /// Determines if two unique words are materially equivalent.
            /// </summary>
            /// <param name="otherWord">The other unique word to check for equivalence</param>
            /// <returns>True if they contain the same word with the same word count.</returns>
            public bool Equals(UniqueWord otherWord)
            {
                return Word == otherWord.Word && Count == otherWord.Count;
            }
        }

        #endregion
    }
}
