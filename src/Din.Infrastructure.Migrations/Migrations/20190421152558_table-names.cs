using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Din.Data.Migrations.Migrations
{
    public partial class tablenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddedContent_Account_AccountRef",
                table: "AddedContent");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "LoginAttempt");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 30, nullable: true),
                    Hash = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountImage_ImageId",
                        column: x => x.ImageId,
                        principalTable: "AccountImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Device = table.Column<string>(nullable: true),
                    Os = table.Column<string>(nullable: true),
                    Browser = table.Column<string>(nullable: true),
                    PublicIp = table.Column<string>(nullable: true),
                    LocationId = table.Column<Guid>(nullable: true),
                    DateAndTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginAttempts_LoginLocation_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LoginLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ImageId",
                table: "Accounts",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Username",
                table: "Accounts",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempts_LocationId",
                table: "LoginAttempts",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddedContent_Accounts_AccountRef",
                table: "AddedContent",
                column: "AccountRef",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddedContent_Accounts_AccountRef",
                table: "AddedContent");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "LoginAttempts");

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Hash = table.Column<string>(nullable: true),
                    ImageId = table.Column<Guid>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountImage_ImageId",
                        column: x => x.ImageId,
                        principalTable: "AccountImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginAttempt",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Browser = table.Column<string>(nullable: true),
                    DateAndTime = table.Column<DateTime>(nullable: false),
                    Device = table.Column<string>(nullable: true),
                    LocationId = table.Column<Guid>(nullable: true),
                    Os = table.Column<string>(nullable: true),
                    PublicIp = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginAttempt_LoginLocation_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LoginLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ImageId",
                table: "Account",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Username",
                table: "Account",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempt_LocationId",
                table: "LoginAttempt",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddedContent_Account_AccountRef",
                table: "AddedContent",
                column: "AccountRef",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
