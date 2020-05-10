using FluentMigrator;
using static Din.Infrastructure.Migrations.Constants;

namespace Din.Infrastructure.Migrations
{
    [Migration(20200508)]
    public class RefreshToken : Migration
    {
        public override void Up()
        {
            Create.Table("account_refresh_token")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("token").AsFixedLengthString(256).NotNullable()
                .WithColumn("account_id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable()
                .WithColumn("creation_date").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("account_refresh_token");
        }
    }
}
