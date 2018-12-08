using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Din.Migrations
{
    public partial class removalofuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_User_UserRef",
                table: "Account");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Account_UserRef",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "UserRef",
                table: "Account");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Account",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Account",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserRef",
                table: "Account",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserRef",
                table: "Account",
                column: "UserRef",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_User_UserRef",
                table: "Account",
                column: "UserRef",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
