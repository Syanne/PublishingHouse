using PublishingHouse.Services;

namespace PublishingHouse.Models
{
    public class SearchRequest
    {
        public string SearchString { get; set; }
        public SearchMode SearchMode { get; set; }
    }

    public enum SearchMode
    {
        NGram,
        Signature,
        Trie
    }
}