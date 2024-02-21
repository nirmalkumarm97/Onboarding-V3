using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    //[Migration(20240221100001)]
    public class AddLoginSeeding_20240221100001 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("Login")
               .Row(new { Name = ("Apoorva"), Email = ("imthyaz@ideassion.com"), EmailId = "apoorva@ideassion.com" , Password = "Apoorva@123" , Role = ("A"), Status = ("A") });
        }
    }
}
