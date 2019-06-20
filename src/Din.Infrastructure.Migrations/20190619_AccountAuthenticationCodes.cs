using FluentMigrator;
using static Din.Infrastructure.Migrations.Constants;

namespace Din.Infrastructure.Migrations
{
    [Migration(20190619)]
    public class AccountAuthenticationCodes : Migration
    {
        public override void Up()
        {
            Alter.Table("account")
                .AddColumn("email").AsString().NotNullable().Unique()
                .AddColumn("active").AsBoolean().NotNullable();

            Create.Table("account_authentication_code")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("account_id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable()
                .ForeignKey("FK_account_codes_account", "account", "id")
                .WithColumn("generated").AsDateTime().NotNullable()
                .WithColumn("code").AsString().NotNullable()
                .WithColumn("active").AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete
                .Column("email")
                .Column("active")
                .FromTable("account").InSchema("");

            Delete.Table("account_authentication_code").InSchema("");
        }
    }
}
