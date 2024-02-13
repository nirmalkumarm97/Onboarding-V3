using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021643)]
    public class AddRequiredDocs_202308021643 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeRequiredDocuments").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeRequiredDocuments");
        }

        public override void Up()
        {
            Create.Table("EmployeeRequiredDocuments").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().Nullable().ForeignKey("EmployeeGeneralDetails", "Id")
               .WithColumn("Aadhar").AsString().NotNullable()
               .WithColumn("Pan").AsString().NotNullable()
               .WithColumn("Driving_license").AsString(500).Nullable()
               .WithColumn("Passport").AsString(500).Nullable()
               .WithColumn("Date_Created").AsDateTime().NotNullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).NotNullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).NotNullable();


        }
    }
}
