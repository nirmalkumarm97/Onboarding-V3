using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(20240803100000)]
    public class AlterEmployeeCertifications_20240803100000 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Alter.Table("EmployeeCertifications")
          .AddColumn("Specialization").AsString(30).Nullable();
        }
    }
}
