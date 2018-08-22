using System;
using System.Collections.Generic;
using static Kingsley.TextAnalyzer.UniqueWords;

namespace Kingsley.TextAnalyzer
{
    public class AnalyzeText
    {
        #region Properties

        /// <summary>
        /// The total number of words in the text (not necessarily unique words).
        /// </summary>
        public int TotalWordCount { get { return uniqueWords.TotalWordCount; } }

        /// <summary>
        /// The number of unique words in the body of text.
        /// </summary>
        public int UniqueWordCount { get { return uniqueWords.UniqueWordCount; } }

        /// <summary>
        /// Alphabetical list of unique words from the text.
        /// </summary>
        public List<string> UniqueWords { get { return uniqueWords.AlphabetizedWords; } }

        /// <summary>
        /// Alphabetical list of unique words with their corresponding counts.
        /// </summary>
        public List<UniqueWord> UniqueWordCounts { get { return uniqueWords.AlphabetizedWordCounts; } }

        /// <summary>
        /// Unique words and counts.
        /// </summary>
        private UniqueWords uniqueWords;

        #endregion

        #region Constructor

        /// <summary>
        /// Analyzes a text string 
        /// </summary>
        /// <param name="text"></param>
        public AnalyzeText(string text)
        {
            uniqueWords = new UniqueWords();
            FindWords(text);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds all of the words in a text using whitespace as the delimiter.
        /// </summary>
        /// <param name="text">Text to analyze.</param>
        private void FindWords(string text)
        {
            string[] rawWords = text.Split(' ');
            foreach (string rawWord in rawWords)
            {
                AddWord(rawWord);
            }
        }

        /// <summary>
        /// Removes any unwanted punctuation and adds the word to the unique word collection.
        /// </summary>
        /// <param name="text">A raw word pulled from a text.</param>
        private void AddWord(string word)
        {
            word = CleanWord(word);
            if (!string.IsNullOrEmpty(word))
            {
                uniqueWords.AddWord(word);
            }
        }

        /// <summary>
        /// Removes any unwanted punctuation from the word.
        /// </summary>
        /// <param name="text">A raw, uncleaned word.</param>
        /// <returns>The word without unwanted punctuation.</returns>
        private string CleanWord(string rawWord)
        {
            List<char> characters = new List<char>(rawWord.ToCharArray());

            for (int i = characters.Count - 1; i >= 0 ; i--)
            {
                if (char.IsPunctuation(characters[i]) && characters[i] != '-')
                {
                    characters.RemoveAt(i);
                }
            }

            return new string(characters.ToArray());
        }

        #endregion
    }
}
