namespace BookWarehouse.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryItems",
                c => new
                    {
                        InventoryItemId = c.Guid(nullable: false, identity: true),
                        Edition = c.String(),
                        Quality = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TitleId = c.Guid(nullable: false),
                        WarehouseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryItemId)
                .ForeignKey("dbo.Titles", t => t.TitleId, cascadeDelete: true)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId, cascadeDelete: true)
                .Index(t => t.TitleId)
                .Index(t => t.WarehouseId);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        TitleId = c.Guid(nullable: false, identity: true),
                        Isbn = c.Long(nullable: false),
                        YearPublished = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TitleId);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        WarehouseId = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.WarehouseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryItems", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.InventoryItems", "TitleId", "dbo.Titles");
            DropIndex("dbo.InventoryItems", new[] { "WarehouseId" });
            DropIndex("dbo.InventoryItems", new[] { "TitleId" });
            DropTable("dbo.Warehouses");
            DropTable("dbo.Titles");
            DropTable("dbo.InventoryItems");
        }
    }
}
