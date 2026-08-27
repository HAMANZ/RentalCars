using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class update_8_14_2012_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_EUser_Id",
                schema: "dbo",
                table: "Suppliers");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "EUser",
                keyColumn: "Id",
                keyValue: "d71d7884-9297-4ba0-a5a2-4483c3c05c2c");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "26be2831-0343-4ae2-9fd7-4c35adf255d9");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "52c50387-f601-4ec8-94f9-c576442e95ce");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "54cbd5c4-ca4e-4c4f-ba61-343cb3ae87cf");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "a4b17bd7-787a-4f6d-b298-94af9cd4d794");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "adf2f14e-a484-4e03-aee6-0cc7baeb84bb");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "b82711c2-8928-4808-8e13-463d5f045d27");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "e33bbc9e-628e-4ac9-94d3-6751df95cc27");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "f72f21c0-e58b-4533-a40b-dec91dd86f88");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: "d71d7884-9297-4ba0-a5a2-4483c3c05c2c");

            // SQL Server cannot ALTER a column to add/remove IDENTITY ("To change the
            // IDENTITY property of a column, the column needs to be dropped and
            // recreated"). Suppliers moved from a TPT child of EUser (string PK) to a
            // standalone entity with its own bigint identity PK, so the table is
            // dropped and recreated instead of altered in place.
            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_Suppliers_SupplierId",
                schema: "dbo",
                table: "SpareParts");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.AlterColumn<long>(
                name: "SupplierId",
                schema: "dbo",
                table: "SpareParts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_Suppliers_SupplierId",
                schema: "dbo",
                table: "SpareParts",
                column: "SupplierId",
                principalSchema: "dbo",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "SpareParts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Whse",
                schema: "dbo",
                table: "SpareParts",
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
                    { "4778a50e-c072-4821-aae7-b98e86830584", "d0b25c5e-b611-4f88-bfaa-993972397d2f", "Supplier", "SUPPLIER" },
                    { "4af7c261-94fd-4eec-9496-966f5f51bac5", "c93fe255-399c-4093-90d4-184fcf825d3a", "EUser", "EUSER" },
                    { "fe536b5f-b2c5-4906-a4b3-24e380a645bc", "d21fbcd1-bf0d-457f-b12f-11c9a0cdd02a", "Adminstrator", "ADMINSTRATOR" },
                    { "383ea1b8-ce46-466e-abe9-b8b3cdb83bc7", "67ddcb74-cc6b-4b74-839f-85554584ce9c", "Customer", "CUSTOMER" },
                    { "2bf862eb-31ef-46e1-8bb6-b3149c49f00d", "8b00f7f2-4d60-4b87-8c2b-1f7db7eabaa8", "Investor", "INVESTOR" },
                    { "85e30e12-b916-40d4-ac2c-e1cccb694f5f", "c6f47ea5-dea4-42f1-a19f-d5b2d460427b", "Accountant", "ACCOUNTANT" },
                    { "0accae0a-8150-4487-89f4-ec627cc4fe3e", "ed090301-b387-42e7-a908-1185116a042b", "CarOwner", "CAROWNER" },
                    { "77a43f03-f813-4879-bd3a-7c5f8ea37ee5", "dd339da1-b81c-45d7-9edb-c3ff1baa4ae6", "PlateOwner", "PLATEOWNER" }
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

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Suppliers",
                columns: new[] { "Id", "Address", "Created_at", "Created_by", "Description", "Email", "Is_deleted", "Name", "Name_ar", "Phone", "Updated_at", "Updated_by", "UserId" },
                values: new object[] { 1L, null, new DateTime(2026, 8, 18, 16, 0, 11, 867, DateTimeKind.Local).AddTicks(3376), 1L, null, "Supplier.S@gmail.com", false, "Supplier", "Supplier", null, new DateTime(2026, 8, 18, 16, 0, 11, 895, DateTimeKind.Local).AddTicks(5652), 1L, null });

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

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_UserId",
                schema: "dbo",
                table: "Suppliers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_EUser_UserId",
                schema: "dbo",
                table: "Suppliers",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "EUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_EUser_UserId",
                schema: "dbo",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_UserId",
                schema: "dbo",
                table: "Suppliers");

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
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: "fc5a2f93-20d7-4f2e-a837-9b70841c5a4d");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "SpareParts");

            migrationBuilder.DropColumn(
                name: "Whse",
                schema: "dbo",
                table: "SpareParts");

            // Mirror of the Up() rebuild: drop and recreate Suppliers back to its
            // original TPT-linked shape (string PK, FK to EUser.Id).
            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_Suppliers_SupplierId",
                schema: "dbo",
                table: "SpareParts");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_EUser_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "EUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AlterColumn<string>(
                name: "SupplierId",
                schema: "dbo",
                table: "SpareParts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_Suppliers_SupplierId",
                schema: "dbo",
                table: "SpareParts",
                column: "SupplierId",
                principalSchema: "dbo",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 409, DateTimeKind.Local).AddTicks(6003), new DateTime(2026, 8, 13, 17, 7, 43, 409, DateTimeKind.Local).AddTicks(6032) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 413, DateTimeKind.Local).AddTicks(1197), new DateTime(2026, 8, 13, 17, 7, 43, 413, DateTimeKind.Local).AddTicks(1221) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(1816), new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(1837) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(1846), new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(1850) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(1855), new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(1859) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(3912), new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(3932) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(3942), new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(3946) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(3951), new DateTime(2026, 8, 13, 17, 7, 43, 411, DateTimeKind.Local).AddTicks(3955) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(3624), new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(3652) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(3665), new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(3671) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(841), new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(884) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(902), new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(906) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 413, DateTimeKind.Utc).AddTicks(6066));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(220));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(232));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(244));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(247));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(250));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(252));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(255));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 20,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(258));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 21,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(261));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 22,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(263));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 23,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(268));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 24,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(270));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 30,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(273));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 31,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(275));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 32,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(278));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 40,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(280));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 41,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(283));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 42,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(286));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 43,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(288));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 44,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(291));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 50,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(294));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 51,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(297));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 52,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(299));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 53,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(302));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 54,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(305));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 55,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(309));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 60,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(317));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 61,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(319));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 62,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(322));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 70,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(325));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 71,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(328));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 72,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(335));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 80,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(338));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 81,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(343));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 82,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(351));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 83,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(474));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 90,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(478));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 91,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(487));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 92,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(494));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 93,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(497));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 94,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(500));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 95,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(505));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 100,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(509));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 101,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(512));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 102,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(514));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 103,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(519));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 104,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(522));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 105,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(525));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 106,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(527));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 107,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(531));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 108,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(533));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 109,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(536));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 110,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(539));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 111,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(542));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 112,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(547));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(6329), new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(6352) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(6362), new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(6366) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(6373), new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(6376) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(6382), new DateTime(2026, 8, 13, 17, 7, 43, 410, DateTimeKind.Local).AddTicks(6386) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "54cbd5c4-ca4e-4c4f-ba61-343cb3ae87cf", "4e4a2506-1613-4395-b5ed-8e57f131978a", "EUser", "EUSER" },
                    { "52c50387-f601-4ec8-94f9-c576442e95ce", "8e23d6ec-3b82-4efc-866b-d8f77012b24a", "Supplier", "SUPPLIER" },
                    { "b82711c2-8928-4808-8e13-463d5f045d27", "1b5386da-f158-4aa8-b63b-7f082e7fde5d", "PlateOwner", "PLATEOWNER" },
                    { "f72f21c0-e58b-4533-a40b-dec91dd86f88", "e9bb37e7-5e63-40c3-9c2e-a8286731894e", "CarOwner", "CAROWNER" },
                    { "26be2831-0343-4ae2-9fd7-4c35adf255d9", "d2b3cff3-8bf1-4f05-a737-05ee1e5f75de", "Accountant", "ACCOUNTANT" },
                    { "e33bbc9e-628e-4ac9-94d3-6751df95cc27", "8fa81119-5d7d-4922-a8c1-fa07eb14bdf3", "Investor", "INVESTOR" },
                    { "a4b17bd7-787a-4f6d-b298-94af9cd4d794", "073cbb5b-b7cb-48de-a2ee-be2f133e1f09", "Customer", "CUSTOMER" },
                    { "adf2f14e-a484-4e03-aee6-0cc7baeb84bb", "06c8e018-2012-4934-a971-eb59a0e91e07", "Adminstrator", "ADMINSTRATOR" }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(2005));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(4237));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(4246));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(4248));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(5911));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9215));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9222));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9226));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9230));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9233));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9238));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9240));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9343));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9353));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9357));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9360));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9365));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9368));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 414, DateTimeKind.Utc).AddTicks(9371));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(2269));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(6748));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7380));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7396));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7404));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7407));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7414));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7417));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7423));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7426));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7432));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7438));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 13, 14, 7, 43, 415, DateTimeKind.Utc).AddTicks(7440));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d71d7884-9297-4ba0-a5a2-4483c3c05c2c", 0, "f33a39c0-27f7-4345-ae55-a54c6fdb4708", "hudaabumayha.ham@gmail.com", false, false, null, null, "ADMIN", null, null, false, "8ddcbb3c-e0b2-4366-b5d9-7862553bfe52", false, "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "EUser",
                columns: new[] { "Id", "Created_at", "Created_by", "DOB", "EUserId", "EmergencyContact", "FToken", "FullName", "FullName_ar", "GenderId", "Is_deleted", "LastLoginAt", "Profile", "Updated_at", "Updated_by" },
                values: new object[] { "d71d7884-9297-4ba0-a5a2-4483c3c05c2c", new DateTime(2026, 8, 13, 17, 7, 43, 385, DateTimeKind.Local).AddTicks(5475), 1L, null, 0L, null, null, null, null, 1L, false, null, null, new DateTime(2026, 8, 13, 17, 7, 43, 408, DateTimeKind.Local).AddTicks(7234), 1L });

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_EUser_Id",
                schema: "dbo",
                table: "Suppliers",
                column: "Id",
                principalSchema: "dbo",
                principalTable: "EUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
