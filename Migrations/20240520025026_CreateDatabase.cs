﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REBOOST.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Batteries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExternalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Capacity = table.Column<float>(type: "real", nullable: true),
                    PricePerHour = table.Column<float>(type: "real", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batteries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cabinets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExternalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressZipCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AddressStreet = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AddressNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AddressDistrict = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AddressLatitude = table.Column<float>(type: "real", nullable: true),
                    AddressLongitude = table.Column<float>(type: "real", nullable: true),
                    DrawerNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabinets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Billing = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CabinetBatteries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    FkCabinetId = table.Column<int>(type: "int", nullable: false),
                    FkBatteryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetBatteries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CabinetBatteries_Batteries_FkBatteryId",
                        column: x => x.FkBatteryId,
                        principalTable: "Batteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinetBatteries_Cabinets_FkCabinetId",
                        column: x => x.FkCabinetId,
                        principalTable: "Cabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FkCabinetFromId = table.Column<int>(type: "int", nullable: true),
                    FkCabinetToId = table.Column<int>(type: "int", nullable: true),
                    FkUserId = table.Column<int>(type: "int", nullable: false),
                    FkBatteryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rents_Batteries_FkBatteryId",
                        column: x => x.FkBatteryId,
                        principalTable: "Batteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rents_Cabinets_FkCabinetFromId",
                        column: x => x.FkCabinetFromId,
                        principalTable: "Cabinets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rents_Cabinets_FkCabinetToId",
                        column: x => x.FkCabinetToId,
                        principalTable: "Cabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rents_Users_FkUserId",
                        column: x => x.FkUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FkUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenLogins_Users_FkUserId",
                        column: x => x.FkUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FkCabinetId = table.Column<int>(type: "int", nullable: false),
                    FkUserId = table.Column<int>(type: "int", nullable: false),
                    FkBatteryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_Batteries_FkBatteryId",
                        column: x => x.FkBatteryId,
                        principalTable: "Batteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tokens_Cabinets_FkCabinetId",
                        column: x => x.FkCabinetId,
                        principalTable: "Cabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tokens_Users_FkUserId",
                        column: x => x.FkUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CabinetBatteries_FkBatteryId",
                table: "CabinetBatteries",
                column: "FkBatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetBatteries_FkCabinetId",
                table: "CabinetBatteries",
                column: "FkCabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_FkBatteryId",
                table: "Rents",
                column: "FkBatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_FkCabinetFromId",
                table: "Rents",
                column: "FkCabinetFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_FkCabinetToId",
                table: "Rents",
                column: "FkCabinetToId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_FkUserId",
                table: "Rents",
                column: "FkUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenLogins_FkUserId",
                table: "TokenLogins",
                column: "FkUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_FkBatteryId",
                table: "Tokens",
                column: "FkBatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_FkCabinetId",
                table: "Tokens",
                column: "FkCabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_FkUserId",
                table: "Tokens",
                column: "FkUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabinetBatteries");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "TokenLogins");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Batteries");

            migrationBuilder.DropTable(
                name: "Cabinets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
