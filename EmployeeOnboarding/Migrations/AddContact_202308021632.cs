using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{

    [Migration(202308021632)]
    public class AddContact_202308021632 : Migration
    {

        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeContactDetails").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeContactDetails");
        }

        public override void Up()
        {
            Create.Table("EmployeeContactDetails").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().NotNullable().ForeignKey("EmployeeGeneralDetails","Id")
               .WithColumn("Address_Type").AsString().NotNullable()
               .WithColumn("Address1").AsString().NotNullable()
               .WithColumn("Address2").AsString().NotNullable()
               .WithColumn("Country_Id").AsInt32().NotNullable()
               .WithColumn("State_Id").AsInt32().NotNullable()
               .WithColumn("City_Id").AsInt32().NotNullable()
               .WithColumn("Pincode").AsString().NotNullable()
               .WithColumn("Date_Created").AsDateTime().NotNullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).NotNullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).NotNullable();




        }
    }
}
