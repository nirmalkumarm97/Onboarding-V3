using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021640)]
    public class AddHobbyMembership_202308021640 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeHobbyMembership").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id"); 
            Delete.Table("EmployeeHobbyMembership");
        }

        public override void Up()
        {
            Create.Table("EmployeeHobbyMembership").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().NotNullable().ForeignKey("EmployeeGeneralDetails","Id")
               .WithColumn("ProfessionalBody").AsBoolean().Nullable()
               .WithColumn("ProfessionalBody_name").AsString(500).Nullable()
               .WithColumn("Hobbies").AsString().Nullable()
               .WithColumn("Date_Created").AsDateTime().NotNullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).NotNullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).NotNullable();



    }
    }
}
