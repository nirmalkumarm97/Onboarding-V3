using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021648)]
    public class AddHealth_202308021648 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeHealthInformation").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeHealthInformation");
        }
        
        public override void Up()
        {
            Create.Table("EmployeeHealthInformation").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().NotNullable().ForeignKey("EmployeeGeneralDetails", "Id")
               .WithColumn("Specific_health_condition").AsString(100).Nullable()
               .WithColumn("Allergies").AsString(100).Nullable()
               .WithColumn("surgery").AsBoolean().NotNullable()
               .WithColumn("Surgery_explaination").AsString(100).Nullable()
               .WithColumn("Night_shifts").AsBoolean().NotNullable()
               .WithColumn("Disability").AsBoolean().NotNullable()
               .WithColumn("Disability_explanation").AsString(100).Nullable()
               .WithColumn("CovidVaccine").AsInt32().NotNullable()
               .WithColumn("Vaccine_certificate").AsString().NotNullable()
               .WithColumn("Health_documents").AsString().Nullable()
               .WithColumn("Date_Created").AsDateTime().NotNullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).NotNullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).NotNullable();

    }
    }
}
