using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021646)]
    public class AddExperience_202308021646 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeExperienceDetails").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeExperienceDetails");
        }
        
        public override void Up()
        {
            Create.Table("EmployeeExperienceDetails").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().Nullable().ForeignKey("EmployeeGeneralDetails", "Id")
               .WithColumn("Company_no").AsInt32().Nullable()
               .WithColumn("Company_name").AsString(100).Nullable()
               .WithColumn("Designation").AsString(100).Nullable()
               .WithColumn("StartDate").AsDate().Nullable()
               .WithColumn("EndDate").AsDate().Nullable()
               .WithColumn("Reporting_to").AsString(100).Nullable()
               .WithColumn("Reason").AsString(100).Nullable()
               .WithColumn("Location").AsString(100).Nullable()
               .WithColumn("Exp_Certificate").AsString(500).Nullable()
               .WithColumn("Date_Created").AsDateTime().NotNullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).NotNullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).NotNullable();





    }
    }
}
