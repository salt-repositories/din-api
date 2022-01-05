using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Din.Infrastructure.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    username = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account_role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "content_poll_status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    content_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    plex_url_polled = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    poster_url_polled = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "login_location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    continent_code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    continent_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country_code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    region_code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    region_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zip_code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    latitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    longitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    system_id = table.Column<int>(type: "int", nullable: false),
                    imdb_id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    overview = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    downloaded = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    has_file = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    year = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    added = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    plex_url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    poster_path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tmdb_id = table.Column<int>(type: "int", nullable: false),
                    studio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    in_cinemas = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    physical_release = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    youtube_trailer_id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "refresh_token",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    creation_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tv_show",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    system_id = table.Column<int>(type: "int", nullable: false),
                    imdb_id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    overview = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    downloaded = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    has_file = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    year = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    added = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    plex_url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    poster_path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tvdb_id = table.Column<int>(type: "int", nullable: false),
                    season_count = table.Column<int>(type: "int", nullable: false),
                    total_episode_count = table.Column<int>(type: "int", nullable: false),
                    episode_count = table.Column<int>(type: "int", nullable: false),
                    network = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    air_time = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    first_aired = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account_authorization_code",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    generated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_account_authorization_code_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account_image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AccountId1 = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data = table.Column<byte[]>(type: "longblob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_account_image_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_account_image_account_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "account",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "added_content",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    system_id = table.Column<int>(type: "int", nullable: false),
                    foreign_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_added = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_added_content_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "login_attempt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    username = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    device = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    os = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    browser = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    public_ip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    location_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_login_attempt_login_location_location_id",
                        column: x => x.location_id,
                        principalTable: "login_location",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "content_rating",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    content_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    votes = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_content_rating_movie_content_id",
                        column: x => x.content_id,
                        principalTable: "movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_content_rating_tv_show_content_id",
                        column: x => x.content_id,
                        principalTable: "tv_show",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "episode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tvshow_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    season_number = table.Column<int>(type: "int", nullable: false),
                    episode_number = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    air_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    overview = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    has_file = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    monitored = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_episode_tv_show_tvshow_id",
                        column: x => x.tvshow_id,
                        principalTable: "tv_show",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "season",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tvshow_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    season_number = table.Column<int>(type: "int", nullable: false),
                    episode_count = table.Column<int>(type: "int", nullable: false),
                    total_episode_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_season_tv_show_tvshow_id",
                        column: x => x.tvshow_id,
                        principalTable: "tv_show",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tv_show_genre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tvshow_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    genre_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tv_show_genre_genre_genre_id",
                        column: x => x.genre_id,
                        principalTable: "genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tv_show_genre_tv_show_tvshow_id",
                        column: x => x.tvshow_id,
                        principalTable: "tv_show",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_account_email",
                table: "account",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_username",
                table: "account",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_authorization_code_AccountId",
                table: "account_authorization_code",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_account_image_account_id",
                table: "account_image",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_image_AccountId1",
                table: "account_image",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_added_content_account_id",
                table: "added_content",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_content_rating_content_id",
                table: "content_rating",
                column: "content_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_episode_tvshow_id",
                table: "episode",
                column: "tvshow_id");

            migrationBuilder.CreateIndex(
                name: "IX_genre_name",
                table: "genre",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_login_attempt_location_id",
                table: "login_attempt",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_system_id",
                table: "movie",
                column: "system_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_season_tvshow_id",
                table: "season",
                column: "tvshow_id");

            migrationBuilder.CreateIndex(
                name: "IX_tv_show_system_id",
                table: "tv_show",
                column: "system_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tv_show_genre_genre_id",
                table: "tv_show_genre",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_tv_show_genre_tvshow_id",
                table: "tv_show_genre",
                column: "tvshow_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_authorization_code");

            migrationBuilder.DropTable(
                name: "account_image");

            migrationBuilder.DropTable(
                name: "added_content");

            migrationBuilder.DropTable(
                name: "content_poll_status");

            migrationBuilder.DropTable(
                name: "content_rating");

            migrationBuilder.DropTable(
                name: "episode");

            migrationBuilder.DropTable(
                name: "login_attempt");

            migrationBuilder.DropTable(
                name: "refresh_token");

            migrationBuilder.DropTable(
                name: "season");

            migrationBuilder.DropTable(
                name: "tv_show_genre");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "movie");

            migrationBuilder.DropTable(
                name: "login_location");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "tv_show");
        }
    }
}
