using System.Collections.Generic;

namespace PublishingHouse.Services.Algorithm.Interface
{
    public interface INGramAlgorithm
    {
        IEnumerable<string> GetNGramsCollection(string query);
    }
}
