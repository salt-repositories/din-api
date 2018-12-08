﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Din.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginLocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContinentCode = table.Column<string>(nullable: true),
                    ContinentName = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true),
                    CountryName = table.Column<string>(nullable: true),
                    RegionCode = table.Column<string>(nullable: true),
                    RegionName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLocation", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "LoginAttempt",
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
                    table.PrimaryKey("PK_LoginAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginAttempt_LoginLocation_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LoginLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: true),
                    Hash = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    UserRef = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_User_UserRef",
                        column: x => x.UserRef,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Data = table.Column<byte[]>(nullable: true),
                    AccountRef = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountImage_Account_AccountRef",
                        column: x => x.AccountRef,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddedContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SystemId = table.Column<int>(nullable: false),
                    ForeignId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Eta = table.Column<int>(nullable: false),
                    Percentage = table.Column<double>(nullable: false),
                    AccountRef = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddedContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddedContent_Account_AccountRef",
                        column: x => x.AccountRef,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserRef",
                table: "Account",
                column: "UserRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_Username",
                table: "Account",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountImage_AccountRef",
                table: "AccountImage",
                column: "AccountRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddedContent_AccountRef",
                table: "AddedContent",
                column: "AccountRef");

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempt_LocationId",
                table: "LoginAttempt",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountImage");

            migrationBuilder.DropTable(
                name: "AddedContent");

            migrationBuilder.DropTable(
                name: "LoginAttempt");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "LoginLocation");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
