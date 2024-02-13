using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021644)]
    public class AddEducation_202308021644 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeEducationDetails").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeEducationDetails");
        }

        public override void Up()
        {
            Create.Table("EmployeeEducationDetails").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().NotNullable().ForeignKey("EmployeeGeneralDetails","Id")
               .WithColumn("Education_no").AsInt32().Nullable()
               .WithColumn("Qualification").AsString(100).NotNullable()
               .WithColumn("University").AsString(100).NotNullable()
               .WithColumn("Institution_name").AsString(100).NotNullable()
               .WithColumn("Degree_achieved").AsString(100).NotNullable()
               .WithColumn("specialization").AsString().NotNullable()
               .WithColumn("Passoutyear").AsInt32().NotNullable()
               .WithColumn("Percentage").AsString(500).NotNullable()
               .WithColumn("Edu_certificate").AsString(500).NotNullable()
               .WithColumn("Date_Created").AsDateTime().NotNullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).NotNullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).NotNullable();





    }
    }
}
