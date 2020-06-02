namespace D.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Capacity = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                        BuildingId = c.Int(nullable: false),
                        AlternateBuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.AlternateBuildingId)
                .ForeignKey("dbo.Buildings", t => t.BuildingId)
                .Index(t => t.BuildingId)
                .Index(t => t.AlternateBuildingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Rooms", "AlternateBuildingId", "dbo.Buildings");
            DropIndex("dbo.Rooms", new[] { "AlternateBuildingId" });
            DropIndex("dbo.Rooms", new[] { "BuildingId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Buildings");
        }
    }
}
