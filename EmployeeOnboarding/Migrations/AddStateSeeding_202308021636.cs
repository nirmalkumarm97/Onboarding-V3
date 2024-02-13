using EmployeeOnboarding.Data;
using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021636)]
    public class AddStateSeeding_202308021636 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("State").Row(new { Country_Id = 1 });
        }

        public override void Up()
        {
            Insert.IntoTable("State").Row(new { Id = "1", State_Name = ("Andhra Pradesh"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "2", State_Name = ("Arunachal Pradesh"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "3", State_Name = ("Assam"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "4", State_Name = ("Bihar"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "5", State_Name = ("Chhattisgarh"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "6", State_Name = ("Goa"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "7", State_Name = ("Gujarat"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "8", State_Name = ("Haryana"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "9", State_Name = ("Himachal Pradesh"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "10", State_Name = ("Jharkhand"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "11", State_Name = ("Karnataka"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "12", State_Name = ("Kerala"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "13", State_Name = ("Madhya Pradesh"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "14", State_Name = ("Maharashtra"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "15", State_Name = ("Manipur"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "16", State_Name = ("Meghalaya"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "17", State_Name = ("Mizoram"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "18", State_Name = ("Nagaland"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "19", State_Name = ("Odisha"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "20", State_Name = ("Punjab"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "21", State_Name = ("Rajasthan"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "22", State_Name = ("Sikkim"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "23", State_Name = ("TamilNadu"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "24", State_Name = ("Telangana"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "25", State_Name = ("Tripura"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "26", State_Name = ("Uttarakhand"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "27", State_Name = ("Uttar Pradesh"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "28", State_Name = ("West Bengal"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "29", State_Name = ("Andaman and Nicobar Islands"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "30", State_Name = ("Chandigarh"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "31", State_Name = ("Dadraand Nagar Haveli and Daman & Diu"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "32", State_Name = ("The Government of NCT of Delhi"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "33", State_Name = ("Jammu & Kashmir"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "34", State_Name = ("Ladakh"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "35", State_Name = ("Lakshadweep"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
            Insert.IntoTable("State").Row(new { Id = "36", State_Name = ("Puducherry"), Date_Created = (DateTime.UtcNow), Date_Modified = (DateTime.UtcNow), Country_Id = ("1") });
        }
    }
}
