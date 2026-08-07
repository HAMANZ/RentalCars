using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FleetErp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "maintenance_records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    MaintenanceTypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OdometerAtService = table.Column<int>(type: "int", nullable: true),
                    ServiceProvider = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Notes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    IsExpensePosted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_maintenance_records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_maintenance_records_lookup_items_MaintenanceTypeId",
                        column: x => x.MaintenanceTypeId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_maintenance_records_lookup_items_StatusId",
                        column: x => x.StatusId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_maintenance_records_vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "maintenance_documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MaintenanceRecordId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_maintenance_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_maintenance_documents_lookup_items_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_maintenance_documents_maintenance_records_MaintenanceRecordId",
                        column: x => x.MaintenanceRecordId,
                        principalTable: "maintenance_records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_documents_DocumentTypeId",
                table: "maintenance_documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_documents_IsDeleted",
                table: "maintenance_documents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_documents_MaintenanceRecordId",
                table: "maintenance_documents",
                column: "MaintenanceRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_records_CompletedDate",
                table: "maintenance_records",
                column: "CompletedDate");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_records_IsDeleted",
                table: "maintenance_records",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_records_MaintenanceTypeId",
                table: "maintenance_records",
                column: "MaintenanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_records_ScheduledDate",
                table: "maintenance_records",
                column: "ScheduledDate");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_records_StatusId",
                table: "maintenance_records",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_records_VehicleId",
                table: "maintenance_records",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "maintenance_documents");

            migrationBuilder.DropTable(
                name: "maintenance_records");
        }
    }
}
