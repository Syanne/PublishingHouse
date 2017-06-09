using System.Collections.Generic;

namespace PublishingHouse.Services.Algorithm.Interface
{
    public interface ISignatureAlgorithm
    {
        string GetDefaultSignature { get; }
        IEnumerable<string> GetSignatures(string query);
    }
}
