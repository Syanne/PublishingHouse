using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.Entities
{
    public class Signature
    {
        [Key]
        public long Id { get; set; }

        public string Hash { get; set; }
        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
