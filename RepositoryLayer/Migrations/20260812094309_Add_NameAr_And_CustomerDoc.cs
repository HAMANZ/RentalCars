using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Add_NameAr_And_CustomerDoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EUser_Customers_CustomerId1",
                schema: "dbo",
                table: "EUser");

            migrationBuilder.DropIndex(
                name: "IX_EUser_CustomerId1",
                schema: "dbo",
                table: "EUser");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "EUser",
                keyColumn: "Id",
                keyValue: "33f4d0b8-9568-4576-95a6-e1724aa153a2");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "0ed6bf6b-600e-4e82-a9dc-b186d9ea05fd");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "12948cbc-c643-457e-b32f-daf35b35643d");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "308301f4-e83f-40f8-9624-66c89317d8b0");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "327f80d6-0836-4823-84e9-a4aab00e7e77");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "6c32fd82-47a5-4a14-856e-03101fffb005");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "7bcc0a7c-e8ad-4af9-99cd-94aa01b1286f");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "c2a70822-c005-419c-ab4e-209b1ad42fd2");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "ce97e2a3-47fa-4714-bb96-635c1186df6a");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: "33f4d0b8-9568-4576-95a6-e1724aa153a2");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "dbo",
                table: "EUser");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                schema: "dbo",
                table: "EUser");

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "STransactionType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "SpareParts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Repairs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "PlateTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "PlateRegion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Nationalities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "MessageTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Media",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "LookUpTables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "InsuranceTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "InsuranceCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Gender",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "FuelTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DOB",
                schema: "dbo",
                table: "EUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContact",
                schema: "dbo",
                table: "EUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "DocumentTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomerDoc",
                schema: "dbo",
                table: "Documents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "CarStatus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                schema: "dbo",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 442, DateTimeKind.Local).AddTicks(271), new DateTime(2026, 8, 12, 12, 43, 5, 442, DateTimeKind.Local).AddTicks(294) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 444, DateTimeKind.Local).AddTicks(5032), new DateTime(2026, 8, 12, 12, 43, 5, 444, DateTimeKind.Local).AddTicks(5053) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(3465), new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(3481) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(3488), new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(3490) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(3495), new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(3498) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(4695), new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(4711) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(4717), new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(4719) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(4724), new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(4727) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 442, DateTimeKind.Local).AddTicks(8736), new DateTime(2026, 8, 12, 12, 43, 5, 442, DateTimeKind.Local).AddTicks(8753) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 442, DateTimeKind.Local).AddTicks(8761), new DateTime(2026, 8, 12, 12, 43, 5, 442, DateTimeKind.Local).AddTicks(8764) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 442, DateTimeKind.Local).AddTicks(6719), new DateTime(2026, 8, 12, 12, 43, 5, 442, DateTimeKind.Local).AddTicks(6743) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 442, DateTimeKind.Local).AddTicks(6843), new DateTime(2026, 8, 12, 12, 43, 5, 442, DateTimeKind.Local).AddTicks(6847) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 444, DateTimeKind.Utc).AddTicks(8206));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(828));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(834));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(837));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(840));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(842));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(844));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(847));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 20,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(849));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 21,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(851));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 22,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(854));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 23,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(856));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 24,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(860));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 30,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(862));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 31,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(865));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 32,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(868));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 40,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(870));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 41,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(872));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 42,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(875));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 43,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(877));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 44,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(879));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 50,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(882));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 51,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(885));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 52,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(887));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 53,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(889));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 54,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(891));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 55,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(894));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 60,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(895));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 61,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(992));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 62,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(995));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 70,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(997));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 71,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(999));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 72,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1001));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 80,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1006));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 81,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1009));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 82,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1012));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 83,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1014));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 90,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 91,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1019));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 92,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1021));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 93,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1024));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 94,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1026));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 95,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1028));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 100,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1031));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 101,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1033));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 102,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1035));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 103,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1037));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 104,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1040));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 105,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1042));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 106,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1044));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 107,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1046));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 108,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1048));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 109,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1050));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 110,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1056));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 111,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1058));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 112,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(1061));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(344), new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(360) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(368), new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(371) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(376), new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(379) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(384), new DateTime(2026, 8, 12, 12, 43, 5, 443, DateTimeKind.Local).AddTicks(387) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7a29780f-8af9-4787-ae5d-7bff96d801ce", "d0fb0f74-7b58-453c-b0ca-613cd07d4522", "EUser", "EUSER" },
                    { "175fa95c-f6a8-4a06-b9aa-f0f4bb24525e", "9cf4cf1a-3993-4c78-987f-285db74fa4aa", "Supplier", "SUPPLIER" },
                    { "721458fc-9dcd-4376-9b09-779f80c4a243", "14705190-a7be-4a49-a58c-a120695880b5", "PlateOwner", "PLATEOWNER" },
                    { "ad008764-03a3-48db-8d5e-a4f1008dceab", "79e601ed-501c-461d-870b-2c1ed9261226", "CarOwner", "CAROWNER" },
                    { "668bad66-398b-4185-891d-e7951bf5a7c0", "07c43e93-be9a-44aa-bef8-90325f72fe69", "Accountant", "ACCOUNTANT" },
                    { "80af3f91-23c0-47f3-b620-2a7ed4e6ab95", "3256ab52-0680-44d0-83de-4534e7c0e03a", "Investor", "INVESTOR" },
                    { "02f39e4e-73bc-4d2d-81c1-4e6e82a41812", "b545d708-d6d1-45cc-bc35-18e78376fcb7", "Customer", "CUSTOMER" },
                    { "729e0f59-d350-4321-8d1a-21c0e16e4ca9", "9ebfea82-e6cf-45e0-8eab-e57d8abe1c66", "Adminstrator", "ADMINSTRATOR" }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(2513));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(3793));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(3798));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(3799));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(3802));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(5240));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7082));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7087));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7089));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7092));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7095));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7100));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7101));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7103));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7105));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7107));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7109));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7111));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7114));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(7115));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 445, DateTimeKind.Utc).AddTicks(9025));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1605));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1910));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1916));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1919));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1923));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1926));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1929));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1933));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1936));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1938));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1940));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 9, 43, 5, 446, DateTimeKind.Utc).AddTicks(1942));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "19ea9c91-8e5f-4cd9-acc8-f6ff9ef34d8a", 0, "e58772d8-e7dd-42cb-8fb4-ae2770252b6e", "hudaabumayha.ham@gmail.com", false, false, null, null, "ADMIN", null, null, false, "0f088d80-88b9-492d-808d-7b74c8bbc065", false, "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "EUser",
                columns: new[] { "Id", "Created_at", "Created_by", "DOB", "EUserId", "EmergencyContact", "FToken", "FullName", "FullName_ar", "GenderId", "Is_deleted", "LastLoginAt", "Profile", "Updated_at", "Updated_by" },
                values: new object[] { "19ea9c91-8e5f-4cd9-acc8-f6ff9ef34d8a", new DateTime(2026, 8, 12, 12, 43, 5, 431, DateTimeKind.Local).AddTicks(7656), 1L, null, 0L, null, null, null, null, 1L, false, null, null, new DateTime(2026, 8, 12, 12, 43, 5, 441, DateTimeKind.Local).AddTicks(5592), 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "EUser",
                keyColumn: "Id",
                keyValue: "19ea9c91-8e5f-4cd9-acc8-f6ff9ef34d8a");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "02f39e4e-73bc-4d2d-81c1-4e6e82a41812");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "175fa95c-f6a8-4a06-b9aa-f0f4bb24525e");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "668bad66-398b-4185-891d-e7951bf5a7c0");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "721458fc-9dcd-4376-9b09-779f80c4a243");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "729e0f59-d350-4321-8d1a-21c0e16e4ca9");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "7a29780f-8af9-4787-ae5d-7bff96d801ce");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "80af3f91-23c0-47f3-b620-2a7ed4e6ab95");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: "ad008764-03a3-48db-8d5e-a4f1008dceab");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: "19ea9c91-8e5f-4cd9-acc8-f6ff9ef34d8a");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "STransactionType");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "SpareParts");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "PlateTypes");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "PlateRegion");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "MessageTemplates");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "LookUpTables");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "InsuranceTypes");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "InsuranceCompanies");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Gender");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "FuelTypes");

            migrationBuilder.DropColumn(
                name: "DOB",
                schema: "dbo",
                table: "EUser");

            migrationBuilder.DropColumn(
                name: "EmergencyContact",
                schema: "dbo",
                table: "EUser");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "IsCustomerDoc",
                schema: "dbo",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "CarStatus");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                schema: "dbo",
                table: "Branches");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                schema: "dbo",
                table: "EUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                schema: "dbo",
                table: "EUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 22, DateTimeKind.Local).AddTicks(1757), new DateTime(2026, 8, 12, 11, 13, 7, 22, DateTimeKind.Local).AddTicks(1793) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 27, DateTimeKind.Local).AddTicks(6116), new DateTime(2026, 8, 12, 11, 13, 7, 27, DateTimeKind.Local).AddTicks(6142) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(80), new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(119) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(134), new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(138) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(150), new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(154) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2792), new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2816) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2831), new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2835) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2848), new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2853) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(8134), new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(8157) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Gender",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(8171), new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(8178) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(3998), new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(4052) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(4088), new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(4097) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(2564));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9663));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9674));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9765));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9771));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9777));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9780));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9785));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 20,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9789));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 21,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9791));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 22,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9797));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 23,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9800));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 24,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9806));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 30,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9809));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 31,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9815));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 32,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9818));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 40,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9822));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 41,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9825));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 42,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9829));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 43,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9832));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 44,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9836));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 50,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9839));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 51,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9842));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 52,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9845));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 53,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9851));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 54,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9854));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 55,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9859));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 60,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9863));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 61,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9865));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 62,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9868));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 70,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9872));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 71,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9875));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 72,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9879));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 80,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9881));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 81,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9885));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 82,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9889));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 83,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9892));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 90,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9895));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 91,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9902));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 92,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9906));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 93,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9912));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 94,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9919));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 95,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9922));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 100,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9926));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 101,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(31));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 102,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(36));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 103,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(42));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 104,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(50));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 105,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(57));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 106,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(72));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 107,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(86));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 108,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(89));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 109,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(104));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 110,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(118));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 111,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(141));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 112,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(144));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(1977), new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2037) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2054), new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2060) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2069), new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2074) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "PlateTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Created_at", "Updated_at" },
                values: new object[] { new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2084), new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2089) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "308301f4-e83f-40f8-9624-66c89317d8b0", "0e1988fa-6e3b-4d50-858c-e71b078bf9f6", "EUser", "EUSER" },
                    { "0ed6bf6b-600e-4e82-a9dc-b186d9ea05fd", "cf65bbb1-8538-4fd1-9009-d22c2a284812", "Supplier", "SUPPLIER" },
                    { "12948cbc-c643-457e-b32f-daf35b35643d", "6f7cac06-5fe3-45e8-abc0-f251994b12d5", "PlateOwner", "PLATEOWNER" },
                    { "7bcc0a7c-e8ad-4af9-99cd-94aa01b1286f", "f2b93541-4f64-495f-ba43-b9af1109d7ef", "CarOwner", "CAROWNER" },
                    { "c2a70822-c005-419c-ab4e-209b1ad42fd2", "ce7d8c5d-caae-4216-a87a-885fddd997b9", "Accountant", "ACCOUNTANT" },
                    { "6c32fd82-47a5-4a14-856e-03101fffb005", "434444eb-cdb6-4c5b-ab4e-fb8c2e9882f6", "Investor", "INVESTOR" },
                    { "327f80d6-0836-4823-84e9-a4aab00e7e77", "ff720dd7-48ea-4556-a82d-5f6e473ae12f", "Customer", "CUSTOMER" },
                    { "ce97e2a3-47fa-4714-bb96-635c1186df6a", "4d1fdec4-1fba-49ce-a0b0-7af260dca608", "Adminstrator", "ADMINSTRATOR" }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(3297));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(6523));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(6532));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(6534));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountCategory",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(6540));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(9586));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4243));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4255));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4259));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4269));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4272));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4280));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4283));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4285));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4289));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4292));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4296));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4300));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4303));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4305));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccountTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4309));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 1L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(9131));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 2L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(5811));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 3L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6499));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 4L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6510));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 5L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6515));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 6L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6519));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 7L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6527));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 8L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6532));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 9L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6535));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 10L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6541));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 11L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6544));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 12L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6547));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "SAccounts",
                keyColumn: "AccountId",
                keyValue: 13L,
                column: "Created_at",
                value: new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6552));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "33f4d0b8-9568-4576-95a6-e1724aa153a2", 0, "b0b843e5-3bc8-4cb1-9326-52315b2fd8ac", "hudaabumayha.ham@gmail.com", false, false, null, null, "ADMIN", null, null, false, "dd16f79b-7db3-44db-9b6d-1984f3caca0e", false, "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "EUser",
                columns: new[] { "Id", "Created_at", "Created_by", "CustomerId", "CustomerId1", "EUserId", "FToken", "FullName", "FullName_ar", "GenderId", "Is_deleted", "LastLoginAt", "Profile", "Updated_at", "Updated_by" },
                values: new object[] { "33f4d0b8-9568-4576-95a6-e1724aa153a2", new DateTime(2026, 8, 12, 11, 13, 7, 1, DateTimeKind.Local).AddTicks(127), 1L, null, null, 0L, null, null, null, 1L, false, null, null, new DateTime(2026, 8, 12, 11, 13, 7, 21, DateTimeKind.Local).AddTicks(727), 1L });

            migrationBuilder.CreateIndex(
                name: "IX_EUser_CustomerId1",
                schema: "dbo",
                table: "EUser",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EUser_Customers_CustomerId1",
                schema: "dbo",
                table: "EUser",
                column: "CustomerId1",
                principalSchema: "dbo",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
