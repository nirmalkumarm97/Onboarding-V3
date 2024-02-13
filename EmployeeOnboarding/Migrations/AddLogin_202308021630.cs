using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202308021630)]
    public class AddLogin_202308021630: Migration
    {
        public override void Down()
        {
            Delete.Table("Login");
        }
        public override void Up()
        {

            Create.Table("Login")
               .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("Name").AsString(100).NotNullable()
               .WithColumn("EmailId").AsString(100).NotNullable()
               .WithColumn("Password").AsString(50).Nullable()
               .WithColumn("Invited_Status").AsString(50).Nullable()
               .WithColumn("Date_Created").AsDateTime().NotNullable()
               .WithColumn("Date_Modified").AsDateTime().NotNullable()
               .WithColumn("Created_by").AsString(100).NotNullable()
               .WithColumn("Modified_by").AsString(100).NotNullable()
               .WithColumn("Status").AsString(30).NotNullable();

        }
    }
}
