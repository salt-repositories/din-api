using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Din.Infrastructure.DataAccess.Migrations
{
    public partial class split_content_rating_foreign_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_rating_movie_content_id",
                table: "content_rating");

            migrationBuilder.DropForeignKey(
                name: "FK_content_rating_tv_show_content_id",
                table: "content_rating");

            migrationBuilder.DropIndex(
                name: "IX_content_rating_content_id",
                table: "content_rating");

            migrationBuilder.DropColumn(
                name: "content_id",
                table: "content_rating");

            migrationBuilder.AddColumn<Guid>(
                name: "movie_id",
                table: "content_rating",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "tvshow_id",
                table: "content_rating",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_content_rating_movie_id",
                table: "content_rating",
                column: "movie_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_content_rating_tvshow_id",
                table: "content_rating",
                column: "tvshow_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_content_rating_movie_movie_id",
                table: "content_rating",
                column: "movie_id",
                principalTable: "movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_content_rating_tv_show_tvshow_id",
                table: "content_rating",
                column: "tvshow_id",
                principalTable: "tv_show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_rating_movie_movie_id",
                table: "content_rating");

            migrationBuilder.DropForeignKey(
                name: "FK_content_rating_tv_show_tvshow_id",
                table: "content_rating");

            migrationBuilder.DropIndex(
                name: "IX_content_rating_movie_id",
                table: "content_rating");

            migrationBuilder.DropIndex(
                name: "IX_content_rating_tvshow_id",
                table: "content_rating");

            migrationBuilder.DropColumn(
                name: "movie_id",
                table: "content_rating");

            migrationBuilder.DropColumn(
                name: "tvshow_id",
                table: "content_rating");

            migrationBuilder.AddColumn<Guid>(
                name: "content_id",
                table: "content_rating",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_content_rating_content_id",
                table: "content_rating",
                column: "content_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_content_rating_movie_content_id",
                table: "content_rating",
                column: "content_id",
                principalTable: "movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_content_rating_tv_show_content_id",
                table: "content_rating",
                column: "content_id",
                principalTable: "tv_show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
