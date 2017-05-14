using PublishingHouse.Services.Algorithm.Interface;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PublishingHouse.Services.Algorithm
{
    public class NGramAlgorythm : INGramAlgorithm
    {
        static int N { get; set; } = 3;
        public IEnumerable<string> GetNGramsCollection(string query)
        {
            var ngram = new List<string>();
            var words = Regex.Split(query, "[-.?!)(,:]");

            foreach (var word in words)
            {
                if (word.Length >= N)
                    for (int i = 0; i <= word.Length - N; i++)
                        ngram.Add(word.Substring(i, N));
            }

            return ngram;
        }
        
        public static int GetNGramSize(int n)
        {
            return N;
        }
    }
}
