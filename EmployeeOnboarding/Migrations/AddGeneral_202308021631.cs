using EmployeeOnboarding.Data.Enum;
using FluentMigrator;
namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021631)]
    public class AddGeneral_202308021631 : Migration
    {

        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeGeneralDetails").ForeignColumn("Login_ID").ToTable("Login").PrimaryColumn("Id");
            Delete.Table("EmployeeGeneralDetails");
        }

        public override void Up()
        {
            Create.Table("EmployeeGeneralDetails").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("Login_ID").AsInt32().NotNullable().ForeignKey("Login", "Id")
               .WithColumn("Empid").AsString().Nullable()
               .WithColumn("Empname").AsString().NotNullable()
               .WithColumn("Official_EmailId").AsString().Nullable()
               .WithColumn("Personal_Emailid").AsString().NotNullable()
               .WithColumn("Contact_no").AsInt64().NotNullable()
               .WithColumn("DOB").AsDate().NotNullable()
               .WithColumn("Nationality").AsString().NotNullable()
               .WithColumn("Gender").AsInt64().NotNullable()
               .WithColumn("MaritalStatus").AsInt32().Nullable()
               .WithColumn("DateOfMarriage").AsDate().Nullable()
               .WithColumn("BloodGrp").AsInt16().NotNullable()
               .WithColumn("Profile_pic").AsString().NotNullable()
               .WithColumn("Date_Created").AsDateTime().NotNullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString().NotNullable()
               .WithColumn("Modified_by").AsString().Nullable()
               .WithColumn("Status").AsString().NotNullable();
           
        }

    }
}
