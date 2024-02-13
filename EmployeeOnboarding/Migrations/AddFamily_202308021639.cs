using System.ComponentModel.DataAnnotations.Schema;
using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021639)]
    public class AddFamily_202308021639 : Migration
    {

        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeFamilyDetails").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeFamilyDetails");
        }

        public override void Up()
        {
            Create.Table("EmployeeFamilyDetails").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().NotNullable().ForeignKey("EmployeeGeneralDetails","Id")
               .WithColumn("Family_no").AsInt32().NotNullable()
               .WithColumn("Relationship").AsString().NotNullable()
               .WithColumn("Name").AsString().NotNullable()
               .WithColumn("DOB").AsDate().NotNullable()
               .WithColumn("Occupation").AsString().NotNullable()
               .WithColumn("contact").AsInt64().NotNullable()
               .WithColumn("Date_Created").AsDateTime().Nullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).NotNullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).NotNullable();
     }

    }
}

