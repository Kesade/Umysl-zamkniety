using System.Data.Entity.Migrations;

namespace Contexts.Migrations
{
    public partial class UserModelUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Roles", c => c.String());
            AddColumn("dbo.Users", "Login", c => c.String());
            AddColumn("dbo.Users", "Password", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Login");
            DropColumn("dbo.Users", "Roles");
        }
    }
}