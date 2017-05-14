using PublishingHouse.Data.Entities;
using System.Data.Entity;

namespace PublishingHouse.Data.Entities
{
    public class DataContext: DbContext
    {
        public DbSet<NGram> NGram { get; set; }
        public DbSet<Article> Article { get; set; }
        public DataContext() : base("WebSystem") { }

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

        }
    }
}
