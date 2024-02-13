using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021645)]
    public class AddCertification_202308021645 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeCertifications").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeCertifications");
        }

        public override void Up()
        {
            Create.Table("EmployeeCertifications").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().Nullable().ForeignKey("EmployeeGeneralDetails","Id")
               .WithColumn("Certificate_no").AsInt32().Nullable()
               .WithColumn("Certificate_name").AsString(100).Nullable()
               .WithColumn("Issued_by").AsString(100).Nullable()
               .WithColumn("Valid_till").AsDate().Nullable()
               .WithColumn("Duration").AsInt32().Nullable()
               .WithColumn("Percentage").AsString(100).Nullable()
               .WithColumn("proof").AsString(500).Nullable()
               .WithColumn("Date_Created").AsDateTime().Nullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).Nullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).Nullable();



    }
    }
}
