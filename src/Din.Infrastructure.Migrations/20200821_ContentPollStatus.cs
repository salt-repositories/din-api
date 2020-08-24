using FluentMigrator;
using static Din.Infrastructure.Migrations.Constants;

namespace Din.Infrastructure.Migrations
{
    [Migration(20200821)]
    public class ContentPollStatus : Migration
    {
        public override void Up()
        {
            Create.Table("content_poll_status")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("content_id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable()
                .WithColumn("plex_url_polled").AsDateTime().Nullable()
                .WithColumn("poster_url_polled").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("content_poll_status");
        }
    }
}
