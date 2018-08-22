using System.Collections.Generic;
using static TextAnalyzer.UniqueWords;

namespace TextAnalyzer.Web.Models
{
    public class TextAnalysisModel
    {
        /// <summary>
        /// The total number of words in the text (not necessarily unique words).
        /// </summary>
        public int TotalWordCount { get; set; }

        /// <summary>
        /// The number of unique words in the body of text.
        /// </summary>
        public int UniqueWordCount { get; set; }

        /// <summary>
        /// Alphabetical list of unique words with their corresponding counts.
        /// </summary>
        public UniqueWord[] UniqueWordCounts { get; set; }
    }
}
