using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Add_RepairType_And_Fix_RepairTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_RepairCategory_RepairCategoryId",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_RepairCategoryId",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "EUser",
                keyColumn: "Id",
                keyValue: "40f339d6-531e-421b-b785-9d9dad30599f");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "47c40e72-a7d4-484c-ab61-7af0e9bf94c4");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "5552e0f0-17ea-4630-99af-cc5baa8d034a");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "7d79cb05-7bdb-4da0-8bd2-513388f2018a");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "8a7bf430-1473-4b95-b112-8caf29395a48");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "c3986590-8944-4a0d-817e-70e8d9a583e2");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "c4d3002a-0f9e-426a-80dc-e64d4113d737");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "c7450c74-84da-4db3-879d-3186cb35e2b0");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "ec05effb-9f77-47f0-8531-8540fa829142");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: "40f339d6-531e-421b-b785-9d9dad30599f");

            migrationBuilder.DropColumn(
                name: "RepairCategoryId",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                schema: "dbo",
                table: "Repairs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PartsCost",
                schema: "dbo",
                table: "Repairs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RepairDate",
                schema: "dbo",
                table: "Repairs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RepairTypeId",
                schema: "dbo",
                table: "Repairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCost",
                schema: "dbo",
                table: "Repairs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "RepairType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 92, DateTimeKind.Local).AddTicks(8687), new DateTime(2026, 8, 27, 1, 58, 17, 92, DateTimeKind.Local).AddTicks(8730) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 99, DateTimeKind.Local).AddTicks(2447), new DateTime(2026, 8, 27, 1, 58, 17, 99, DateTimeKind.Local).AddTicks(2508) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(2712), new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(2785) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(2802), new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(2809) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(2820), new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(2830) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(5989), new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(6027) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(6042), new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(6048) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(6057), new DateTime(2026, 8, 27, 1, 58, 17, 96, DateTimeKind.Local).AddTicks(6063) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 93, DateTimeKind.Local).AddTicks(9038), new DateTime(2026, 8, 27, 1, 58, 17, 93, DateTimeKind.Local).AddTicks(9073) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 93, DateTimeKind.Local).AddTicks(9087), new DateTime(2026, 8, 27, 1, 58, 17, 93, DateTimeKind.Local).AddTicks(9093) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 93, DateTimeKind.Local).AddTicks(5078), new DateTime(2026, 8, 27, 1, 58, 17, 93, DateTimeKind.Local).AddTicks(5113) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 93, DateTimeKind.Local).AddTicks(5132), new DateTime(2026, 8, 27, 1, 58, 17, 93, DateTimeKind.Local).AddTicks(5139) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 99, DateTimeKind.Utc).AddTicks(8517));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5176));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5189));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5198));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5318));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5323));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5327));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5331));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 20,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5335));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 21,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5338));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 22,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5342));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 23,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5346));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 24,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5350));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 30,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5354));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 31,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5358));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 32,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5363));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 40,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5366));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 41,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 42,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5374));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 43,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5377));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 44,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5384));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 50,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5387));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 51,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5393));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 52,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5398));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 53,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5402));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 54,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5406));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 55,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5409));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 60,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5413));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 61,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5417));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 62,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5421));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 70,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5426));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 71,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5429));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 72,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5432));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 80,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5436));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 81,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5440));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 82,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5443));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 83,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5447));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 90,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5453));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 91,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5459));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 92,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5463));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 93,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5466));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 94,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5473));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 95,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5476));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 100,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5568));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 101,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5576));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 102,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5583));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 103,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5588));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 104,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5591));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 105,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5596));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 106,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5600));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 107,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5604));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 108,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5608));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 109,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5612));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 110,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5616));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 111,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5620));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 112,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(5624));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 94, DateTimeKind.Local).AddTicks(2602), new DateTime(2026, 8, 27, 1, 58, 17, 94, DateTimeKind.Local).AddTicks(2634) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 94, DateTimeKind.Local).AddTicks(2648), new DateTime(2026, 8, 27, 1, 58, 17, 94, DateTimeKind.Local).AddTicks(2654) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 94, DateTimeKind.Local).AddTicks(2688), new DateTime(2026, 8, 27, 1, 58, 17, 94, DateTimeKind.Local).AddTicks(2702) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 94, DateTimeKind.Local).AddTicks(2711), new DateTime(2026, 8, 27, 1, 58, 17, 94, DateTimeKind.Local).AddTicks(2717) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "952d1a6e-9779-480d-8d37-18c09326f3db", "8fafc2bc-6ac0-44b0-94d7-844d9ae29748", "EUser", "EUSER" },
                    { "e190e721-dc47-46d8-b69d-90292452042d", "70c89d1b-58dc-4b6d-b5cf-46b9c65f3315", "Adminstrator", "ADMINSTRATOR" },
                    { "73cf4cfd-e05b-45e4-b5c2-2876714b2b4b", "584f2b4e-1652-40b7-a8a6-bd33adb746a4", "Supplier", "SUPPLIER" },
                    { "b6ce9d08-161e-4dcf-952e-de668e940a85", "b6b05b15-edd3-4175-b36a-973a2f8300d9", "PlateOwner", "PLATEOWNER" },
                    { "ddea2205-f6c2-47f3-8a4b-659dde82d6f1", "cfaf2859-a1f1-4f97-85ee-f797e0cea255", "CarOwner", "CAROWNER" },
                    { "6eac4958-cd2d-45ab-b8ad-bfe6d67362fc", "ed99bc31-efdf-46cc-a1d2-0809da1e4df1", "Accountant", "ACCOUNTANT" },
                    { "4c39da44-ac6e-4690-bbff-06ed5835784a", "b45ebf8e-e95e-41a4-9a76-ac34a9d313cb", "Investor", "INVESTOR" },
                    { "5f042b95-ef2d-47ad-8984-80f39175ff33", "ae8414b4-cc41-441d-b3ee-d3cc11a68b95", "Customer", "CUSTOMER" }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 100, DateTimeKind.Utc).AddTicks(8472));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(1088));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(1096));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(1104));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(1106));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(3166));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6866));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6874));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6878));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6884));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6889));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6892));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6895));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6899));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6901));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6905));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6908));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6911));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6915));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6918));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 101, DateTimeKind.Utc).AddTicks(6921));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(1470));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(7538));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8262));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8277));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8282));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8287));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8291));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8300));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8304));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8308));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8313));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8317));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 26, 22, 58, 17, 102, DateTimeKind.Utc).AddTicks(8321));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 27, 1, 58, 17, 76, DateTimeKind.Local).AddTicks(4512), new DateTime(2026, 8, 27, 1, 58, 17, 90, DateTimeKind.Local).AddTicks(6817) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ab5c9ba4-887c-4a55-926b-feafe762931f", 0, "4123e37e-a8f6-4ad8-a76b-9343204c2fa1", "hudaabumayha.ham@gmail.com", false, false, null, null, "ADMIN", null, null, false, "99f941aa-a98d-4bac-8953-be64cf27eaf0", false, "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "EUser",
                columns: new[] { "Id", "Created_at", "Created_by", "DOB", "EUserId", "EmergencyContact", "FToken", "FullName", "FullName_ar", "GenderId", "Is_deleted", "LastLoginAt", "Profile", "Updated_at", "Updated_by" },
                values: new object[] { "ab5c9ba4-887c-4a55-926b-feafe762931f", new DateTime(2026, 8, 27, 1, 58, 17, 91, DateTimeKind.Local).AddTicks(1217), 1L, null, 0L, null, null, null, null, 1L, false, null, null, new DateTime(2026, 8, 27, 1, 58, 17, 91, DateTimeKind.Local).AddTicks(7331), 1L });

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_RepairTypeId",
                schema: "dbo",
                table: "Repairs",
                column: "RepairTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_RepairType_RepairTypeId",
                schema: "dbo",
                table: "Repairs",
                column: "RepairTypeId",
                principalSchema: "dbo",
                principalTable: "RepairType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_RepairType_RepairTypeId",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropTable(
                name: "RepairType",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_RepairTypeId",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "EUser",
                keyColumn: "Id",
                keyValue: "ab5c9ba4-887c-4a55-926b-feafe762931f");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "4c39da44-ac6e-4690-bbff-06ed5835784a");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "5f042b95-ef2d-47ad-8984-80f39175ff33");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "6eac4958-cd2d-45ab-b8ad-bfe6d67362fc");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "73cf4cfd-e05b-45e4-b5c2-2876714b2b4b");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "952d1a6e-9779-480d-8d37-18c09326f3db");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "b6ce9d08-161e-4dcf-952e-de668e940a85");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "ddea2205-f6c2-47f3-8a4b-659dde82d6f1");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "e190e721-dc47-46d8-b69d-90292452042d");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: "ab5c9ba4-887c-4a55-926b-feafe762931f");

            migrationBuilder.DropColumn(
                name: "Note",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "PartsCost",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "RepairDate",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "RepairTypeId",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.AddColumn<int>(
                name: "RepairCategoryId",
                schema: "dbo",
                table: "Repairs",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 469, DateTimeKind.Local).AddTicks(9491), new DateTime(2026, 8, 20, 23, 53, 40, 469, DateTimeKind.Local).AddTicks(9525) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 473, DateTimeKind.Local).AddTicks(243), new DateTime(2026, 8, 20, 23, 53, 40, 473, DateTimeKind.Local).AddTicks(276) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(3156), new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(3176) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(3184), new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(3188) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(3197), new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(3204) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(4955), new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(4981) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(4994), new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(5000) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(5009), new DateTime(2026, 8, 20, 23, 53, 40, 471, DateTimeKind.Local).AddTicks(5016) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(6582), new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(6604) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(6613), new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(6617) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(4059), new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(4086) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(4099), new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(4102) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 473, DateTimeKind.Utc).AddTicks(5913));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(775));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(789));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(797));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(802));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(806));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(811));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(815));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 20,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(819));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 21,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(823));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 22,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(827));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 23,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(833));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 24,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(838));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 30,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(842));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 31,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(848));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 32,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(856));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 40,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(859));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 41,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(862));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 42,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(865));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 43,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(868));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 44,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(871));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 50,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(873));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 51,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(876));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 52,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(879));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 53,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(881));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 54,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(884));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 55,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(887));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 60,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(889));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 61,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(892));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 62,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(895));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 70,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(897));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 71,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(900));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 72,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(903));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 80,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(927));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 81,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(930));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 82,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(933));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 83,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(936));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 90,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(938));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 91,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(941));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 92,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(943));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 93,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(946));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 94,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(948));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 95,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(951));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 100,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(954));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 101,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(957));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 102,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(959));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 103,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(962));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 104,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(964));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 105,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(967));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 106,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(969));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 107,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(972));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 108,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(974));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 109,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(977));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 110,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(979));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 111,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(982));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 112,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(984));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(8976), new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(8998) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(9008), new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(9011) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(9018), new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(9022) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(9027), new DateTime(2026, 8, 20, 23, 53, 40, 470, DateTimeKind.Local).AddTicks(9031) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5552e0f0-17ea-4630-99af-cc5baa8d034a", "ec0ae896-8036-430d-b6e6-5d137cfd2ef8", "EUser", "EUSER" },
                    { "7d79cb05-7bdb-4da0-8bd2-513388f2018a", "01493bc6-bc60-41e2-832c-c1ee9cd3e607", "Adminstrator", "ADMINSTRATOR" },
                    { "c7450c74-84da-4db3-879d-3186cb35e2b0", "fbf7475e-02f2-4b45-bdfa-fe14da3a0931", "Supplier", "SUPPLIER" },
                    { "47c40e72-a7d4-484c-ab61-7af0e9bf94c4", "04270e54-3df5-4a53-9cfc-badc0c75140f", "PlateOwner", "PLATEOWNER" },
                    { "c3986590-8944-4a0d-817e-70e8d9a583e2", "1b8afe89-d866-4225-a892-4e6e05563709", "CarOwner", "CAROWNER" },
                    { "c4d3002a-0f9e-426a-80dc-e64d4113d737", "f77e25f4-c86b-4428-ab9f-956680acf550", "Accountant", "ACCOUNTANT" },
                    { "ec05effb-9f77-47f0-8531-8540fa829142", "7ecafc1c-386d-4b41-a75f-fa1c832f80e7", "Investor", "INVESTOR" },
                    { "8a7bf430-1473-4b95-b112-8caf29395a48", "13a955f2-510c-4a6f-bfa3-35c491d13e78", "Customer", "CUSTOMER" }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(3067));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(5117));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(5123));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(5125));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(5127));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(6779));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9297));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9303));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9306));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9322));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9325));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9327));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9329));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9331));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9333));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9335));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9337));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9340));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9342));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9344));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 474, DateTimeKind.Utc).AddTicks(9346));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(2172));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6201));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6749));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6757));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6760));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6764));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6767));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6769));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6773));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6777));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6780));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6783));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 20, 20, 53, 40, 475, DateTimeKind.Utc).AddTicks(6786));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 20, 23, 53, 40, 458, DateTimeKind.Local).AddTicks(8501), new DateTime(2026, 8, 20, 23, 53, 40, 468, DateTimeKind.Local).AddTicks(7443) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "40f339d6-531e-421b-b785-9d9dad30599f", 0, "5359fce8-44e4-42f4-83e7-fc741ba15180", "hudaabumayha.ham@gmail.com", false, false, null, null, "ADMIN", null, null, false, "26aac709-dc4b-4f49-88e3-923ffde3f1b8", false, "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "EUser",
                columns: new[] { "Id", "Created_at", "Created_by", "DOB", "EUserId", "EmergencyContact", "FToken", "FullName", "FullName_ar", "GenderId", "Is_deleted", "LastLoginAt", "Profile", "Updated_at", "Updated_by" },
                values: new object[] { "40f339d6-531e-421b-b785-9d9dad30599f", new DateTime(2026, 8, 20, 23, 53, 40, 469, DateTimeKind.Local).AddTicks(1040), 1L, null, 0L, null, null, null, null, 1L, false, null, null, new DateTime(2026, 8, 20, 23, 53, 40, 469, DateTimeKind.Local).AddTicks(2306), 1L });

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_RepairCategoryId",
                schema: "dbo",
                table: "Repairs",
                column: "RepairCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_RepairCategory_RepairCategoryId",
                schema: "dbo",
                table: "Repairs",
                column: "RepairCategoryId",
                principalSchema: "dbo",
                principalTable: "RepairCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
