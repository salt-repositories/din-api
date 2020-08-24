using FluentMigrator;
using static Din.Infrastructure.Migrations.Constants;

namespace Din.Infrastructure.Migrations
{
    [Migration(20200818)]
    public class ContentTables : Migration
    {
        public override void Up()
        {
            Create.Table("movie")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("system_id").AsInt32().NotNullable().Unique()
                .WithColumn("imdb_id").AsString().Nullable()
                .WithColumn("title").AsString().NotNullable()
                .WithColumn("overview").AsCustom(TEXT).Nullable()
                .WithColumn("status").AsString().Nullable()
                .WithColumn("downloaded").AsBoolean().Nullable()
                .WithColumn("has_file").AsBoolean().Nullable()
                .WithColumn("year").AsString().Nullable()
                .WithColumn("added").AsDateTime().Nullable()
                .WithColumn("plex_url").AsString().Nullable()
                .WithColumn("poster_path").AsString().Nullable()
                .WithColumn("tmdb_id").AsInt32().Nullable()
                .WithColumn("studio").AsString().Nullable()
                .WithColumn("in_cinemas").AsDateTime().Nullable()
                .WithColumn("physical_release").AsDateTime().Nullable()
                .WithColumn("youtube_trailer_id").AsString().Nullable();

            Create.Table("tvshow")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("system_id").AsInt32().NotNullable().Unique()
                .WithColumn("imdb_id").AsString().Nullable()
                .WithColumn("title").AsString().NotNullable()
                .WithColumn("overview").AsCustom(TEXT).Nullable()
                .WithColumn("status").AsString().Nullable()
                .WithColumn("downloaded").AsBoolean().Nullable()
                .WithColumn("has_file").AsBoolean().Nullable()
                .WithColumn("year").AsString().Nullable()
                .WithColumn("added").AsDateTime().Nullable()
                .WithColumn("plex_url").AsString().Nullable()
                .WithColumn("poster_path").AsString().Nullable()
                .WithColumn("tvdb_id").AsInt32().Nullable()
                .WithColumn("season_count").AsInt32().Nullable()
                .WithColumn("total_episode_count").AsInt32().Nullable()
                .WithColumn("episode_count").AsInt32().Nullable()
                .WithColumn("network").AsString().Nullable()
                .WithColumn("air_time").AsString().Nullable()
                .WithColumn("first_aired").AsDateTime().Nullable();

            Create.Table("content_rating")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("content_id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable()
                .WithColumn("votes").AsInt32().NotNullable()
                .WithColumn("value").AsDouble().NotNullable();

            Create.Table("season")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("tvshow_id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable()
                .ForeignKey("FK_season_tvshow", "tvshow", "id")
                .WithColumn("season_number").AsInt32().NotNullable()
                .WithColumn("episode_count").AsInt32().NotNullable()
                .WithColumn("total_episode_count").AsInt32().NotNullable();

            Create.Table("genre")
                .WithColumn("id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable().PrimaryKey()
                .WithColumn("name").AsString().NotNullable().Unique()
                .NotNullable();

            Create.Table("tvshow_genre")
                .WithColumn("tvshow_id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable()
                    .ForeignKey("FK_tvshow_genre_tvshow", "tvshow", "id")
                .WithColumn("genre_id").AsCustom(GUID_COLUMN_DEFINITION).NotNullable()
                    .ForeignKey("FK_tvshow_genre_genre", "genre", "id");
        }

        public override void Down()
        {
            Delete.Table("tvshow_genre");
            Delete.Table("genre");
            Delete.Table("season");
            Delete.Table("content_rating");
            Delete.Table("tvshow");
            Delete.Table("movie");
        }
    }
}
