using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Add_RepairCategory_And_Fix_RepairCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "EUser",
                keyColumn: "Id",
                keyValue: "fc5a2f93-20d7-4f2e-a837-9b70841c5a4d");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "0accae0a-8150-4487-89f4-ec627cc4fe3e");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "2bf862eb-31ef-46e1-8bb6-b3149c49f00d");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "383ea1b8-ce46-466e-abe9-b8b3cdb83bc7");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "4778a50e-c072-4821-aae7-b98e86830584");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "4af7c261-94fd-4eec-9496-966f5f51bac5");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "77a43f03-f813-4879-bd3a-7c5f8ea37ee5");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "85e30e12-b916-40d4-ac2c-e1cccb694f5f");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "fe536b5f-b2c5-4906-a4b3-24e380a645bc");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: "fc5a2f93-20d7-4f2e-a837-9b70841c5a4d");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Repairs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Is_InsuranceStatus",
                schema: "dbo",
                table: "Statuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_WorkOrderStatus",
                schema: "dbo",
                table: "Statuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                schema: "dbo",
                table: "Repairs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RepairCategoryId",
                schema: "dbo",
                table: "Repairs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RepairCategory",
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
                    table.PrimaryKey("PK_RepairCategory", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_RepairCategory_RepairCategoryId",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropTable(
                name: "RepairCategory",
                schema: "dbo");

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
                name: "Is_InsuranceStatus",
                schema: "dbo",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Is_WorkOrderStatus",
                schema: "dbo",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "RepairCategoryId",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "dbo",
                table: "Repairs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 896, DateTimeKind.Local).AddTicks(9716), new DateTime(2026, 8, 18, 16, 0, 11, 896, DateTimeKind.Local).AddTicks(9747) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 900, DateTimeKind.Local).AddTicks(3242), new DateTime(2026, 8, 18, 16, 0, 11, 900, DateTimeKind.Local).AddTicks(3264) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(7081), new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(7109) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(7123), new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(7129) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(7140), new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(7147) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(9414), new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(9439) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(9452), new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(9460) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(9469), new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(9476) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 897, DateTimeKind.Local).AddTicks(7989), new DateTime(2026, 8, 18, 16, 0, 11, 897, DateTimeKind.Local).AddTicks(8014) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 897, DateTimeKind.Local).AddTicks(8029), new DateTime(2026, 8, 18, 16, 0, 11, 897, DateTimeKind.Local).AddTicks(8036) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 897, DateTimeKind.Local).AddTicks(4963), new DateTime(2026, 8, 18, 16, 0, 11, 897, DateTimeKind.Local).AddTicks(4992) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 897, DateTimeKind.Local).AddTicks(5009), new DateTime(2026, 8, 18, 16, 0, 11, 897, DateTimeKind.Local).AddTicks(5017) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 900, DateTimeKind.Utc).AddTicks(7010));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(118));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(126));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(131));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(134));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(137));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(140));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(143));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 20,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(145));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 21,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(147));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 22,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(150));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 23,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(220));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 24,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(223));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 30,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(225));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 31,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(228));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 32,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(230));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 40,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(233));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 41,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(235));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 42,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(237));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 43,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(241));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 44,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(244));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 50,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(247));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 51,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(249));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 52,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(252));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 53,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(254));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 54,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(257));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 55,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(260));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 60,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(262));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 61,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(265));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 62,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(267));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 70,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(270));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 71,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(272));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 72,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(275));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 80,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(279));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 81,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(282));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 82,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(284));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 83,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(287));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 90,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(289));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 91,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(291));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 92,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(294));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 93,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(296));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 94,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(299));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 95,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(301));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 100,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(304));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 101,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(307));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 102,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(309));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 103,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(312));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 104,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(314));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 105,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(317));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 106,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(319));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 107,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(363));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 108,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(366));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 109,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(370));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 110,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(373));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 111,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(376));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 112,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(379));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(1149), new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(1176) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(1191), new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(1198) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(1208), new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(1215) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(1225), new DateTime(2026, 8, 18, 16, 0, 11, 898, DateTimeKind.Local).AddTicks(1232) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4af7c261-94fd-4eec-9496-966f5f51bac5", "c93fe255-399c-4093-90d4-184fcf825d3a", "EUser", "EUSER" },
                    { "fe536b5f-b2c5-4906-a4b3-24e380a645bc", "d21fbcd1-bf0d-457f-b12f-11c9a0cdd02a", "Adminstrator", "ADMINSTRATOR" },
                    { "4778a50e-c072-4821-aae7-b98e86830584", "d0b25c5e-b611-4f88-bfaa-993972397d2f", "Supplier", "SUPPLIER" },
                    { "77a43f03-f813-4879-bd3a-7c5f8ea37ee5", "dd339da1-b81c-45d7-9edb-c3ff1baa4ae6", "PlateOwner", "PLATEOWNER" },
                    { "0accae0a-8150-4487-89f4-ec627cc4fe3e", "ed090301-b387-42e7-a908-1185116a042b", "CarOwner", "CAROWNER" },
                    { "85e30e12-b916-40d4-ac2c-e1cccb694f5f", "c6f47ea5-dea4-42f1-a19f-d5b2d460427b", "Accountant", "ACCOUNTANT" },
                    { "2bf862eb-31ef-46e1-8bb6-b3149c49f00d", "8b00f7f2-4d60-4b87-8c2b-1f7db7eabaa8", "Investor", "INVESTOR" },
                    { "383ea1b8-ce46-466e-abe9-b8b3cdb83bc7", "67ddcb74-cc6b-4b74-839f-85554584ce9c", "Customer", "CUSTOMER" }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(1724));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(3613));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(3616));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(3618));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(5062));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7744));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7751));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7755));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7757));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7760));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7763));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7765));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7767));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7769));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7771));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7773));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7776));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7778));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7780));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 901, DateTimeKind.Utc).AddTicks(7782));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(631));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4041));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4454));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4461));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4465));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4538));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4541));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4543));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4546));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4549));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4551));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4554));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 18, 13, 0, 11, 902, DateTimeKind.Utc).AddTicks(4557));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 18, 16, 0, 11, 867, DateTimeKind.Local).AddTicks(3376), new DateTime(2026, 8, 18, 16, 0, 11, 895, DateTimeKind.Local).AddTicks(5652) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fc5a2f93-20d7-4f2e-a837-9b70841c5a4d", 0, "8314e9e3-ac39-4d98-a3aa-cf36f51053a3", "hudaabumayha.ham@gmail.com", false, false, null, null, "ADMIN", null, null, false, "6573c53b-c753-4748-ab7c-ed21255e130a", false, "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "EUser",
                columns: new[] { "Id", "Created_at", "Created_by", "DOB", "EUserId", "EmergencyContact", "FToken", "FullName", "FullName_ar", "GenderId", "Is_deleted", "LastLoginAt", "Profile", "Updated_at", "Updated_by" },
                values: new object[] { "fc5a2f93-20d7-4f2e-a837-9b70841c5a4d", new DateTime(2026, 8, 18, 16, 0, 11, 895, DateTimeKind.Local).AddTicks(9784), 1L, null, 0L, null, null, null, null, 1L, false, null, null, new DateTime(2026, 8, 18, 16, 0, 11, 896, DateTimeKind.Local).AddTicks(1536), 1L });
        }
    }
}
