using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Update_Transactions_Documents_And_PlateRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicensePlates_PlateRegion_PlateRegionId",
                schema: "dbo",
                table: "LicensePlates");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "EUser",
                keyColumn: "Id",
                keyValue: "e9efb2e1-0bd2-4ce0-a474-7f5414ab42bf");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "1be6e818-cace-40b7-b2f4-55831c334f46");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "7164da1e-7aa8-4f39-96b6-8a29d9c2704d");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "737c1137-340c-484d-966e-c0d69b8144b3");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "7ad4bb7f-0d84-4e36-becd-28f1a404c63d");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "a1f43538-4667-4bb4-83d2-96fd6b522ec5");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "c8aa5746-9166-4d07-97cd-4517436de47d");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "e80ea031-fa4d-41a2-a8f7-f2fa2baf5178");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "f035a5da-43ba-4d7e-82b7-8196830eea8e");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: "e9efb2e1-0bd2-4ce0-a474-7f5414ab42bf");

            migrationBuilder.AlterColumn<long>(
                name: "PlateRegionId",
                schema: "dbo",
                table: "LicensePlates",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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
                name: "FK_LicensePlates_PlateRegion_PlateRegionId",
                schema: "dbo",
                table: "LicensePlates",
                column: "PlateRegionId",
                principalSchema: "dbo",
                principalTable: "PlateRegion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicensePlates_PlateRegion_PlateRegionId",
                schema: "dbo",
                table: "LicensePlates");

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

            migrationBuilder.AlterColumn<long>(
                name: "PlateRegionId",
                schema: "dbo",
                table: "LicensePlates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 290, DateTimeKind.Local).AddTicks(7101), new DateTime(2026, 8, 12, 17, 19, 14, 290, DateTimeKind.Local).AddTicks(7132) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 347, DateTimeKind.Local).AddTicks(260), new DateTime(2026, 8, 12, 17, 19, 14, 347, DateTimeKind.Local).AddTicks(288) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(3335), new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(3368) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(3381), new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(3387) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(3395), new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(3401) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(5560), new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(5584) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(5596), new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(5602) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(5609), new DateTime(2026, 8, 12, 17, 19, 14, 345, DateTimeKind.Local).AddTicks(5615) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(4375), new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(4406) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(4420), new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(4426) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(849), new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(918) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(944), new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(950) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(4257));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7473));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7480));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7484));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7487));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7489));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7491));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7494));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 20,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7497));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 21,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7499));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 22,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7501));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 23,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7503));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 24,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7506));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 30,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7508));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 31,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7510));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 32,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7512));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 40,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7514));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 41,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7518));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 42,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7521));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 43,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7523));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 44,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7526));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 50,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7528));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 51,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7531));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 52,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7533));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 53,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7535));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 54,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7537));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 55,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7539));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 60,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7542));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 61,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7544));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 62,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7546));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 70,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7548));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 71,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7550));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 72,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7553));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 80,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7555));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 81,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7558));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 82,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7560));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 83,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7562));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 90,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7564));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 91,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7643));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 92,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7646));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 93,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7649));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 94,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7651));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 95,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7653));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 100,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7655));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 101,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7657));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 102,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7659));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 103,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7662));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 104,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7664));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 105,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7668));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 106,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7673));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 107,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7676));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 108,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7678));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 109,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7682));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 110,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7685));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 111,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7688));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 112,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(7691));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(7275), new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(7301) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(7317), new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(7323) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(7332), new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(7338) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(7346), new DateTime(2026, 8, 12, 17, 19, 14, 344, DateTimeKind.Local).AddTicks(7351) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7ad4bb7f-0d84-4e36-becd-28f1a404c63d", "9b0a4395-1f43-4a34-aabb-e9649c6723eb", "EUser", "EUSER" },
                    { "a1f43538-4667-4bb4-83d2-96fd6b522ec5", "7440725a-0ee2-4aa7-a755-baaa2a16bed3", "Supplier", "SUPPLIER" },
                    { "e80ea031-fa4d-41a2-a8f7-f2fa2baf5178", "5efa2a1b-cd36-4eae-bf73-576d10679fbd", "PlateOwner", "PLATEOWNER" },
                    { "737c1137-340c-484d-966e-c0d69b8144b3", "a06bd582-6c4a-4775-b914-dc069e78c12b", "CarOwner", "CAROWNER" },
                    { "c8aa5746-9166-4d07-97cd-4517436de47d", "6f19bee2-36de-4964-b427-eb5a0fde9fcd", "Accountant", "ACCOUNTANT" },
                    { "f035a5da-43ba-4d7e-82b7-8196830eea8e", "049b0b5c-b0dd-4977-8eed-1dbb20e95073", "Investor", "INVESTOR" },
                    { "7164da1e-7aa8-4f39-96b6-8a29d9c2704d", "8c01b35f-35ae-4a34-b5e1-7841309d2eb6", "Customer", "CUSTOMER" },
                    { "1be6e818-cace-40b7-b2f4-55831c334f46", "2fdfc811-8410-4a4d-8bac-331d44d8591f", "Adminstrator", "ADMINSTRATOR" }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 347, DateTimeKind.Utc).AddTicks(8921));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(348));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(352));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(353));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(355));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(1460));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4073));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4078));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4082));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4083));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4085));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4087));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4089));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4154));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4156));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4158));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4160));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4162));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(4164));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(6549));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 348, DateTimeKind.Utc).AddTicks(9785));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(163));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(168));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(171));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(173));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(175));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(177));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(179));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(182));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(184));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(186));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 14, 19, 14, 349, DateTimeKind.Utc).AddTicks(189));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e9efb2e1-0bd2-4ce0-a474-7f5414ab42bf", 0, "fcfd8050-7ec8-41aa-ad44-3b7fe1482bd4", "hudaabumayha.ham@gmail.com", false, false, null, null, "ADMIN", null, null, false, "e774fe8a-02a8-4f88-a135-1e1bcfccffac", false, "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "EUser",
                columns: new[] { "Id", "Created_at", "Created_by", "DOB", "EUserId", "EmergencyContact", "FToken", "FullName", "FullName_ar", "GenderId", "Is_deleted", "LastLoginAt", "Profile", "Updated_at", "Updated_by" },
                values: new object[] { "e9efb2e1-0bd2-4ce0-a474-7f5414ab42bf", new DateTime(2026, 8, 12, 17, 19, 14, 177, DateTimeKind.Local).AddTicks(8282), 1L, null, 0L, null, null, null, null, 1L, false, null, null, new DateTime(2026, 8, 12, 17, 19, 14, 290, DateTimeKind.Local).AddTicks(1814), 1L });

            migrationBuilder.AddForeignKey(
                name: "FK_LicensePlates_PlateRegion_PlateRegionId",
                schema: "dbo",
                table: "LicensePlates",
                column: "PlateRegionId",
                principalSchema: "dbo",
                principalTable: "PlateRegion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
