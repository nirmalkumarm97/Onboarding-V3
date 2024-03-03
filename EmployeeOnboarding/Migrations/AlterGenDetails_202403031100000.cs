using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(202403031100000)]
   public  class AlterGenDetails_202403031100000 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Alter.Table("EmployeeGeneralDetails")
          .AddColumn("UserId").AsInt64().Nullable();
        }
    }
}
