using FluentMigrator;
using static Din.Infrastructure.Migrations.Constants;


namespace Din.Infrastructure.Migrations
{
    [Migration(20190519)]
    public class InitialCreation : Migration
    {
        public override void Up()
        {
            Create.Table("account")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("username").AsString(40).NotNullable().Unique()
                .WithColumn("hash").AsString().NotNullable()
                .WithColumn("account_role").AsString().NotNullable();

            Create.Table("account_image")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("account_id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().ForeignKey("FK_account_image_account", "account", "id")
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("data").AsByte().NotNullable();

            Create.Table("added_content")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("system_id").AsString().NotNullable()
                .WithColumn("foreign_id").AsString().NotNullable()
                .WithColumn("title").AsString().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable()
                .WithColumn("status").AsString().NotNullable()
                .WithColumn("eta").AsInt32().Nullable()
                .WithColumn("percentage").AsDouble().Nullable()
                .WithColumn("account_id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable()
                .ForeignKey("FK_added_content_account", "account", "id");

            Create.Table("login_location")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("continent_code").AsString().Nullable()
                .WithColumn("continent_name").AsString().Nullable()
                .WithColumn("country_code").AsString().Nullable()
                .WithColumn("country_name").AsString().Nullable()
                .WithColumn("region_code").AsString().Nullable()
                .WithColumn("region_name").AsString().Nullable()
                .WithColumn("city").AsString().Nullable()
                .WithColumn("zip_code").AsString().Nullable()
                .WithColumn("latitude").AsString().Nullable()
                .WithColumn("longitude").AsString().Nullable();

            Create.Table("login_attempt")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("username").AsString(40).NotNullable()
                .WithColumn("device").AsString().NotNullable()
                .WithColumn("os").AsString().NotNullable()
                .WithColumn("browser").AsString().NotNullable()
                .WithColumn("public_ip").AsString().NotNullable()
                .WithColumn("location_id").AsCustom(GUID_COLUMN_DEFINITION).Nullable()
                .ForeignKey("FK_login_attempt_login_location", "login_location", "id")
                .WithColumn("date").AsDateTime().NotNullable()
                .WithColumn("status").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("login_attempt");
            Delete.Table("login_location");
            Delete.Table("added_content");
            Delete.Table("account_image");
            Delete.Table("account");
        }
    }
}
