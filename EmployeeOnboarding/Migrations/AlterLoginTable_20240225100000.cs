using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(20240225100000)]
    public class AlterLoginTable_20240225100000 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Alter.Table("Login")
          .AddColumn("OTP").AsInt32().Nullable();
        }
    }
}
