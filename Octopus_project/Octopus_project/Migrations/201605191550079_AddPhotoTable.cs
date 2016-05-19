namespace Octopus_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        PublisherName = c.String(),
                        PublisherSurname = c.String(),
                        Path = c.String(),
                        Likes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Photos");
        }
    }
}
