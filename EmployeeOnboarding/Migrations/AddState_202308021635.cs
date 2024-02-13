using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021635)]
    public class AddState_202308021635 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey().FromTable("State").ForeignColumn("Country_Id").ToTable("Country").PrimaryColumn("Id");
            Delete.Table("State");
        }

        public override void Up()
        {
            Create.Table("State").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("Country_Id").AsInt32().NotNullable().ForeignKey("Country","Id")
                .WithColumn("State_Name").AsString(100).NotNullable()
                .WithColumn("Date_Created").AsDate().NotNullable()
                .WithColumn("Date_Modified").AsDate().NotNullable();
        }
    }
}
