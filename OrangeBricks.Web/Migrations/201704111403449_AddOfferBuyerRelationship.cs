namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOfferBuyerRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offers", "Property_Id", "dbo.Properties");
            DropIndex("dbo.Offers", new[] { "Property_Id" });
            AddColumn("dbo.Offers", "BuyerUserId", c => c.String(nullable: false));
            AlterColumn("dbo.Offers", "Property_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Offers", "Property_Id");
            AddForeignKey("dbo.Offers", "Property_Id", "dbo.Properties", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "Property_Id", "dbo.Properties");
            DropIndex("dbo.Offers", new[] { "Property_Id" });
            AlterColumn("dbo.Offers", "Property_Id", c => c.Int());
            DropColumn("dbo.Offers", "BuyerUserId");
            CreateIndex("dbo.Offers", "Property_Id");
            AddForeignKey("dbo.Offers", "Property_Id", "dbo.Properties", "Id");
        }
    }
}
