using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021634)]
    public class AddCountrySeeding_202308021634 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("Country").Row(new { Id = "1" });
        }

        public override void Up()
        {
            Insert.IntoTable("Country").Row(new { Id = "1", Country_Name = ("India"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow) });
        }
    }
}

