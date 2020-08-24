using FluentMigrator;
using static Din.Infrastructure.Migrations.Constants;

namespace Din.Infrastructure.Migrations
{
    [Migration(20200820)]
    public class Episode : Migration
    {
        public override void Up()
        {
            Create.Table("episode")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("tvshow_id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable()
                    .ForeignKey("FK_episode_tvshow", "tvshow", "id")
                .WithColumn("season_number").AsInt32().NotNullable()
                .WithColumn("episode_number").AsInt32().NotNullable()
                .WithColumn("title").AsCustom(TEXT).NotNullable()
                .WithColumn("air_date").AsDateTime().NotNullable()
                .WithColumn("overview").AsCustom(TEXT).Nullable()
                .WithColumn("has_file").AsBoolean().Nullable()
                .WithColumn("monitored").AsBoolean().Nullable();
        }

        public override void Down()
        {
            Delete.Table("episode");
        }
    }
}
