namespace Contexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebioInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        EntryId = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Entries", t => t.EntryId)
                .Index(t => t.AuthorId)
                .Index(t => t.EntryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Roles = c.String(),
                        Login = c.String(maxLength: 30),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Login, unique: true);
            
            CreateTable(
                "dbo.Entries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DiaryId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Body = c.String(),
                        Title = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Diaries", t => t.DiaryId)
                .Index(t => t.DiaryId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Diaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        Title = c.String(),
                        Timestamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entries", "DiaryId", "dbo.Diaries");
            DropForeignKey("dbo.Diaries", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Comments", "EntryId", "dbo.Entries");
            DropForeignKey("dbo.Entries", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.Users");
            DropIndex("dbo.Diaries", new[] { "AuthorId" });
            DropIndex("dbo.Entries", new[] { "AuthorId" });
            DropIndex("dbo.Entries", new[] { "DiaryId" });
            DropIndex("dbo.Users", new[] { "Login" });
            DropIndex("dbo.Comments", new[] { "EntryId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropTable("dbo.Diaries");
            DropTable("dbo.Entries");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}
