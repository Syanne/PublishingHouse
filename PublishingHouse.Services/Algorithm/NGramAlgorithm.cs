using PublishingHouse.Services.Algorithm.Interface;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PublishingHouse.Services.Algorithm
{
    internal class NGramAlgorithm : INGramAlgorithm
    {
        static int N { get; set; } = 3;
        public IEnumerable<string> GetNGramsCollection(string query)
        {
            var ngram = new List<string>();
            var words = Regex.Split(query.ToLower(), @"[\s\p{P}]");

            foreach (var word in words)
            {
                if (word.Length >= N)
                    for (int i = 0; i <= word.Length - N; i++)
                        ngram.Add(word.Substring(i, N));
            }

            return ngram;
        }
    }
}
