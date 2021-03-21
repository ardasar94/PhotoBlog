namespace PhotoBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ilk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(),
                        PhotoPath = c.String(),
                        CreatingTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HashTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HashTagBlogPosts",
                c => new
                    {
                        HashTag_Id = c.Int(nullable: false),
                        BlogPost_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HashTag_Id, t.BlogPost_Id })
                .ForeignKey("dbo.HashTags", t => t.HashTag_Id, cascadeDelete: true)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPost_Id, cascadeDelete: true)
                .Index(t => t.HashTag_Id)
                .Index(t => t.BlogPost_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HashTagBlogPosts", "BlogPost_Id", "dbo.BlogPosts");
            DropForeignKey("dbo.HashTagBlogPosts", "HashTag_Id", "dbo.HashTags");
            DropIndex("dbo.HashTagBlogPosts", new[] { "BlogPost_Id" });
            DropIndex("dbo.HashTagBlogPosts", new[] { "HashTag_Id" });
            DropTable("dbo.HashTagBlogPosts");
            DropTable("dbo.HashTags");
            DropTable("dbo.BlogPosts");
        }
    }
}
