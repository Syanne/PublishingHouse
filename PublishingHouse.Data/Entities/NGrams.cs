using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.Entities
{
    public class NGram
    {
        [Key]
        public long Id { get; set; }
        public string NGramValue { get; set; }
        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
