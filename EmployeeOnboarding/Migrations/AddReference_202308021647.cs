using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021647)]
    public class AddReference_202308021647 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeReferenceDetails").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeReferenceDetails");
        }
        
        public override void Up()
        {
            Create.Table("EmployeeReferenceDetails").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().Nullable().ForeignKey("EmployeeGeneralDetails", "Id")
               .WithColumn("Referral_name").AsString(100).Nullable()
               .WithColumn("Designation").AsString(100).Nullable()
               .WithColumn("Company_name").AsString(100).Nullable()
               .WithColumn("Contact_number").AsInt32().Nullable()
               .WithColumn("Email_Id").AsString(100).Nullable()
               .WithColumn("Authorize").AsBoolean().Nullable()
               .WithColumn("Date_Created").AsDateTime().Nullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).Nullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).Nullable();

    }
    }
}
