using System.Collections.Generic;

namespace PublishingHouse.Services.Algorithm.Interface
{
    public interface ISignatureAlgorithm
    {
        IEnumerable<string> GetSignatures(string query);
    }
}
