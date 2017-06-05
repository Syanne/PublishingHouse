using PublishingHouse.Data.Entities;
using System.Data.Entity;

namespace PublishingHouse.Data.Entities
{
    public class DataContext: DbContext
    {
        public DbSet<NGram> NGrams { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Signature> Signatures { get; set; }
        public DataContext() : base("PublishingHouse") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                        .HasMany(s => s.NGrams)
                        .WithMany(c => c.Articles)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("ArticleId");
                            cs.MapRightKey("NGramId");
                            cs.ToTable("ArticleNGram");
                        });

            modelBuilder.Entity<Article>()
                        .HasMany(s => s.Signatures)
                        .WithMany(c => c.Articles)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("ArticleId");
                            cs.MapRightKey("SignatureId");
                            cs.ToTable("ArticleSignature");
                        });
        }
    }
}
