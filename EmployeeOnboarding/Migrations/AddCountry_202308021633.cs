using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021633)]
    public class AddCountry_202308021633 : Migration
    {
        public override void Down()
        {
            Delete.Table("Country");
        }

        public override void Up()
        {
            Create.Table("Country").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("Country_Name").AsString(100).NotNullable()
               .WithColumn("Date_Created").AsDate().NotNullable()
               .WithColumn("Date_Modified").AsDate().NotNullable();
        }
    }
}
