using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class update_8_2_2012_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "ReferenceType",
                schema: "dbo",
                table: "STransactions",
                newName: "Description");

            migrationBuilder.AlterColumn<long>(
                name: "TransactionTypeId",
                schema: "dbo",
                table: "STransactions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                schema: "dbo",
                table: "STransactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchIdId",
                schema: "dbo",
                table: "STransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "STransactionDocuments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    STransactionId = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STransactionDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STransactionDocuments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "dbo",
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STransactionDocuments_STransactions_STransactionId",
                        column: x => x.STransactionId,
                        principalSchema: "dbo",
                        principalTable: "STransactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_STransactions_BranchId",
                schema: "dbo",
                table: "STransactions",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_STransactions_TransactionTypeId",
                schema: "dbo",
                table: "STransactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_STransactionDocuments_DocumentTypeId",
                schema: "dbo",
                table: "STransactionDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_STransactionDocuments_STransactionId",
                schema: "dbo",
                table: "STransactionDocuments",
                column: "STransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_STransactions_Branches_BranchId",
                schema: "dbo",
                table: "STransactions",
                column: "BranchId",
                principalSchema: "dbo",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_STransactions_STransactionType_TransactionTypeId",
                schema: "dbo",
                table: "STransactions",
                column: "TransactionTypeId",
                principalSchema: "dbo",
                principalTable: "STransactionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STransactions_Branches_BranchId",
                schema: "dbo",
                table: "STransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_STransactions_STransactionType_TransactionTypeId",
                schema: "dbo",
                table: "STransactions");

            migrationBuilder.DropTable(
                name: "STransactionDocuments",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_STransactions_BranchId",
                schema: "dbo",
                table: "STransactions");

            migrationBuilder.DropIndex(
                name: "IX_STransactions_TransactionTypeId",
                schema: "dbo",
                table: "STransactions");

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

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "dbo",
                table: "STransactions");

            migrationBuilder.DropColumn(
                name: "BranchIdId",
                schema: "dbo",
                table: "STransactions");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "dbo",
                table: "STransactions",
                newName: "ReferenceType");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionTypeId",
                schema: "dbo",
                table: "STransactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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
    }
}
