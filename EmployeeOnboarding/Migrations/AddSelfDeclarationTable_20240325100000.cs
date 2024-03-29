using FluentMigrator;

namespace EmployeeOnboarding.Migrations
{
    [Migration(20240325100000)]
    public class AddSelfDeclarationTable_20240325100000 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("SelfDeclaration").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
              .WithColumn("EmpGen_Id").AsInt32().Nullable().ForeignKey("EmployeeGeneralDetails", "Id")
              .WithColumn("Name").AsString().NotNullable()
              .WithColumn("CreatedDate").AsDateTime().NotNullable()
              .WithColumn("CreatedBy").AsInt32().NotNullable()
              .WithColumn("ModifiedDate").AsDateTime().Nullable()
              .WithColumn("ModifiedBy").AsInt32().Nullable()
              .WithColumn("Status").AsString().NotNullable();
        }
    }
}
