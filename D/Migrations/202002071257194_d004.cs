namespace D.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d004 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Country_Id", c => c.Int());
            AddColumn("dbo.Teachers", "Country_Id", c => c.Int());
            CreateIndex("dbo.Students", "Country_Id");
            CreateIndex("dbo.Teachers", "Country_Id");
            AddForeignKey("dbo.Students", "Country_Id", "dbo.Countries", "Id");
            AddForeignKey("dbo.Teachers", "Country_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Students", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Teachers", new[] { "Country_Id" });
            DropIndex("dbo.Students", new[] { "Country_Id" });
            DropColumn("dbo.Teachers", "Country_Id");
            DropColumn("dbo.Students", "Country_Id");
        }
    }
}
