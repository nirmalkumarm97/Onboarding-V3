using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(20240221100000)]
    public class AlterloginTable_20240221100000 : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Alter.Table("Login")
           .AddColumn("Role").AsString(30).Nullable();
        }
    }
}
