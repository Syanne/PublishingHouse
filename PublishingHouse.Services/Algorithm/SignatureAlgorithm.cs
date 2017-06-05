using PublishingHouse.Services.Algorithm.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PublishingHouse.Services.Algorithm
{
    class SignatureAlgorithm : ISignatureAlgorithm
    {
        private Dictionary<int, char[]> alphabet;
        internal Dictionary<int, char[]> Alphabet
        {
            get
            {
                if (alphabet == null)
                    alphabet = new Dictionary<int, char[]> {
                                {0, new char[]  { 'а', 'б'} },
                                {1, new char[]  { 'в', 'г', 'д'} },
                                {2, new char[]  { 'е', 'ж'} },
                                {3, new char[]  { 'з', 'и', 'й'} },
                                {4, new char[]  { 'к', 'л'} },
                                {5, new char[]  { 'м', 'н', 'о' } },
                                {6, new char[]  { 'п', 'р'} },
                                {7, new char[]  { 'с', 'т', 'у' } },
                                {8, new char[]  { 'ф', 'х'} },
                                {9, new char[]  { 'ц', 'ч', 'ш' } },
                                {10, new char[]  { 'щ', 'ъ'} },
                                {11, new char[]  { 'ы', 'ь', 'э' } },
                                {12, new char[]  { 'ю', 'я'} }
                                };
                return alphabet;
            }
        }

        public IEnumerable<string> GetSignatures(string query)
        {
            int size = Alphabet.Count;

            return Regex
                .Split(query.ToLower(), @"[\s\p{P}]")
                .Distinct()
                .Select(GetSignature);
        }

        private string GetSignature(string word)
        {
            var array = Enumerable.Repeat(0, Alphabet.Count).ToArray();

            foreach (var character in word)
            {
                var result = Alphabet.FirstOrDefault(key => key.Value.Any(ch => ch == character));

                if (!result.Equals(new KeyValuePair<int, char[]>()))
                    array[result.Key] = 1;
            }

            return string.Join("", array);
        }
    }
}
