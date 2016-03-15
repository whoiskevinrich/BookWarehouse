namespace BookWarehouse.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Logging : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogId = c.Guid(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Action = c.String(),
                        TitleId = c.Guid(nullable: false),
                        OldValue = c.String(),
                        NewValue = c.String(),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.Titles", t => t.TitleId, cascadeDelete: true)
                .Index(t => t.TitleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "TitleId", "dbo.Titles");
            DropIndex("dbo.Logs", new[] { "TitleId" });
            DropTable("dbo.Logs");
        }
    }
}
