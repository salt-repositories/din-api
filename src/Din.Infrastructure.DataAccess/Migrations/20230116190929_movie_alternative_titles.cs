using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Din.Infrastructure.DataAccess.Migrations
{
    public partial class movie_alternative_titles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "alternative_titles",
                table: "movie",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alternative_titles",
                table: "movie");
        }
    }
}
