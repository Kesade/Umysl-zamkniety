using System.Data.Entity.Migrations;

namespace Contexts.Migrations
{
    public partial class IndexUniqueLogin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Login", c => c.String(maxLength: 30));
            CreateIndex("dbo.Users", "Login", true);
        }

        public override void Down()
        {
            DropIndex("dbo.Users", new[] {"Login"});
            AlterColumn("dbo.Users", "Login", c => c.String());
        }
    }
}