namespace PublishingHouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedSignatures : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Signatures", "Hash", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Signatures", "Hash", c => c.Long(nullable: false));
        }
    }
}
