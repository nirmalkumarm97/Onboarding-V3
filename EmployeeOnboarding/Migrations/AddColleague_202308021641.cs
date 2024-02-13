using FluentMigrator;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021641)]
    public class AddColleague_202308021641 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("EmployeeColleagueDetails").ForeignColumn("EmpGen_Id").ToTable("EmployeeGeneralDetails").PrimaryColumn("Id");
            Delete.Table("EmployeeColleagueDetails");
        }

        public override void Up()
        {
            Create.Table("EmployeeColleagueDetails").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("EmpGen_Id").AsInt32().NotNullable().ForeignKey("EmployeeGeneralDetails", "Id")
               .WithColumn("colleague_no").AsInt32().NotNullable()
               .WithColumn("Employee_id").AsString().Nullable()
               .WithColumn("Colleague_Name").AsString().Nullable()
               .WithColumn("Location").AsString().Nullable()
               .WithColumn("Date_Created").AsDateTime().NotNullable()
               .WithColumn("Date_Modified").AsDateTime().Nullable()
               .WithColumn("Created_by").AsString(100).NotNullable()
               .WithColumn("Modified_by").AsString(100).Nullable()
               .WithColumn("Status").AsString(30).NotNullable();

    }
}
}
