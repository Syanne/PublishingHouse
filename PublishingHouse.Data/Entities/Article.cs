using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.Entities
{
    public class Article
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Authors { get; set; }
        public string Annotation { get; set; }
        public string ArticlePath { get; set; }
        public virtual ICollection<NGram> NGrams { get; set; } = new List<NGram>();
        public virtual ICollection<Signature> Signatures { get; set; } = new List<Signature>();
    }
}
