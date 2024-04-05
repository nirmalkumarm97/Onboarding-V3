using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(20240405000111)]
    public class AlterEmployeeColleagueDetails_20240405000111 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Alter.Table("EmployeeColleagueDetails")
                .AddColumn("RelationShip").AsString().Nullable();
        }
    }
}
