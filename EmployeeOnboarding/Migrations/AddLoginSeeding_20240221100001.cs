using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(20240226100001)]
    public class AddLoginSeeding_20240221100001 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("Login")
.Row(new { Name = ("Guga Priya"), EmailId = "gugapriya@ideassion.com", Password = "GugaPriya@123", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, Created_by = "Admin", Modified_by = "Admin", Role = ("A"), Status = ("A") })
.Row(new { Name = ("Deepika Rajagopalan"), EmailId = "deepika@ideassion.com", Password = "Deepika@123", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, Created_by = "Admin", Modified_by = "Admin", Role = ("A"), Status = ("A") })
.Row(new { Name = ("Apoorva"), EmailId = "apoorva@ideassion.com", Password = "Apoorva@123", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, Created_by = "Admin", Modified_by = "Admin", Role = ("A"), Status = ("A") })
.Row(new { Name = ("Saran"), EmailId = "saran.m@ideassion.com", Password = "Saran@123", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, Created_by = "Admin", Modified_by = "Admin", Role = ("A"), Status = ("A") })
.Row(new { Name = ("Abhilashini"), EmailId = "abhilashini@ideassion.com", Password = "Abi@123", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, Created_by = "Admin", Modified_by = "Admin", Role = ("A"), Status = ("A") })
.Row(new { Name = ("Nirmal"), EmailId = "Nirmal@ideassion.com", Password = "Nirmal@123", Date_Created = DateTime.UtcNow, Date_Modified = DateTime.UtcNow, Created_by = "Admin", Modified_by = "Admin", Role = ("A"), Status = ("A") });

        }
    }
}
