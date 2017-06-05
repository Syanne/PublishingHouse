namespace PublishingHouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Signatures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Authors = c.String(),
                        Annotation = c.String(),
                        ArticlePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NGrams",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NGramValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Signatures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Hash = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleNGram",
                c => new
                    {
                        ArticleId = c.Long(nullable: false),
                        NGramId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleId, t.NGramId })
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.NGrams", t => t.NGramId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.NGramId);
            
            CreateTable(
                "dbo.ArticleSignature",
                c => new
                    {
                        ArticleId = c.Long(nullable: false),
                        SignatureId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleId, t.SignatureId })
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Signatures", t => t.SignatureId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.SignatureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleSignature", "SignatureId", "dbo.Signatures");
            DropForeignKey("dbo.ArticleSignature", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleNGram", "NGramId", "dbo.NGrams");
            DropForeignKey("dbo.ArticleNGram", "ArticleId", "dbo.Articles");
            DropIndex("dbo.ArticleSignature", new[] { "SignatureId" });
            DropIndex("dbo.ArticleSignature", new[] { "ArticleId" });
            DropIndex("dbo.ArticleNGram", new[] { "NGramId" });
            DropIndex("dbo.ArticleNGram", new[] { "ArticleId" });
            DropTable("dbo.ArticleSignature");
            DropTable("dbo.ArticleNGram");
            DropTable("dbo.Signatures");
            DropTable("dbo.NGrams");
            DropTable("dbo.Articles");
        }
    }
}
