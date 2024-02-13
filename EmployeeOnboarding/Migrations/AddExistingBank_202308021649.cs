using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021649)]
    public class AddExistingBank_202308021649 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeExistingBankAccount").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeExistingBankAccount");
        }
        
        public override void Up()
        {
            Create.Table("EmployeeExistingBankAccount").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().Nullable().ForeignKey("EmployeeGeneralDetails", "Id")
               .WithColumn("Account_name").AsString(100).Nullable()
               .WithColumn("Bank_name").AsString(100).Nullable()
               .WithColumn("Bank_Branch").AsString(100).Nullable()
               .WithColumn("Account_number").AsInt64().Nullable()
               .WithColumn("IFSC_code").AsString(100).Nullable()
               .WithColumn("Joint_Account").AsBoolean().Nullable()
               .WithColumn("Proof_submitted").AsString().Nullable()
               .WithColumn("Bank_Documents").AsString(500).Nullable()
               .WithColumn("Date_Created").AsDateTime().Nullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).Nullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).Nullable();




    }
    }
}
