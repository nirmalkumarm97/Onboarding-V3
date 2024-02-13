using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021637)]
    public class AddCity_202308021637 : Migration
    {

        public override void Down()
        {
            Delete.ForeignKey().FromTable("City").ForeignColumn("State_Id").ToTable("State").PrimaryColumn("Id");
            Delete.Table("City");
        }

        public override void Up()
        {
            Create.Table("City").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("State_Id").AsInt32().NotNullable().ForeignKey("State", "Id")
                .WithColumn("City_Name").AsString(100).NotNullable()
                .WithColumn("Date_Created").AsDate().NotNullable()
                .WithColumn("Date_Modified").AsDate().NotNullable();
        }
    }
}
