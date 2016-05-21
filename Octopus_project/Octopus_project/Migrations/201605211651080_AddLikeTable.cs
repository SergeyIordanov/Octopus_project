namespace Octopus_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLikeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        PhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Likes");
        }
    }
}
