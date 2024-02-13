using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021642)]
    public class AddEmergencyContact_202308021642 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeEmergencyContactDetails").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeEmergencyContactDetails");
        }

        public override void Up()
        {
            Create.Table("EmployeeEmergencyContactDetails").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().NotNullable().ForeignKey("EmployeeGeneralDetails", "Id")
               .WithColumn("emergency_no").AsInt32().Nullable()
               .WithColumn("Relationship").AsString(100).NotNullable()
               .WithColumn("Relation_name").AsString(100).NotNullable()
               .WithColumn("Contact_number").AsInt64().NotNullable()
               .WithColumn("Contact_address").AsString(500).NotNullable()
               .WithColumn("Date_Created").AsDateTime().NotNullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).NotNullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).NotNullable();




    }
    }
}
