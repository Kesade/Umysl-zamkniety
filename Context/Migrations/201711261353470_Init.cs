using System.Data.Entity.Migrations;

namespace Contexts.Migrations
{
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Comments",
                    c => new
                    {
                        Id = c.Int(false, true),
                        AuthorId = c.Int(false),
                        EntryId = c.Int(false),
                        Timestamp = c.DateTime(false, 7, storeType: "datetime2"),
                        Body = c.String()
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
                        Id = c.Int(false, true),
                        Name = c.String()
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Entries",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Timestamp = c.DateTime(false, 7, storeType: "datetime2"),
                        DiaryId = c.Int(false),
                        AuthorId = c.Int(false),
                        Body = c.String(),
                        Title = c.String(),
                        Type = c.Int(false)
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
                        Id = c.Int(false, true),
                        AuthorId = c.Int(false),
                        Title = c.String(),
                        Timestamp = c.DateTime(false, 7, storeType: "datetime2")
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
            DropIndex("dbo.Diaries", new[] {"AuthorId"});
            DropIndex("dbo.Entries", new[] {"AuthorId"});
            DropIndex("dbo.Entries", new[] {"DiaryId"});
            DropIndex("dbo.Comments", new[] {"EntryId"});
            DropIndex("dbo.Comments", new[] {"AuthorId"});
            DropTable("dbo.Diaries");
            DropTable("dbo.Entries");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}