using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FleetErp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInsuranceEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "insurance_companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Address = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    ContactPerson = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_companies", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "insurance_records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    InsuranceCompanyId = table.Column<int>(type: "int", nullable: false),
                    InsuranceTypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PolicyNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Premium = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Deductible = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    CoverageDetails = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    RenewalReminderSent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_insurance_records_insurance_companies_InsuranceCompanyId",
                        column: x => x.InsuranceCompanyId,
                        principalTable: "insurance_companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_insurance_records_lookup_items_InsuranceTypeId",
                        column: x => x.InsuranceTypeId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_insurance_records_lookup_items_StatusId",
                        column: x => x.StatusId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_insurance_records_vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "insurance_documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    InsuranceRecordId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_insurance_documents_insurance_records_InsuranceRecordId",
                        column: x => x.InsuranceRecordId,
                        principalTable: "insurance_records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_insurance_documents_lookup_items_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_companies_IsActive",
                table: "insurance_companies",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_companies_IsDeleted",
                table: "insurance_companies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_companies_Name",
                table: "insurance_companies",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_documents_DocumentTypeId",
                table: "insurance_documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_documents_ExpiresAt",
                table: "insurance_documents",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_documents_InsuranceRecordId",
                table: "insurance_documents",
                column: "InsuranceRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_documents_IsDeleted",
                table: "insurance_documents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_records_EndDate",
                table: "insurance_records",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_records_InsuranceCompanyId",
                table: "insurance_records",
                column: "InsuranceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_records_InsuranceTypeId",
                table: "insurance_records",
                column: "InsuranceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_records_IsDeleted",
                table: "insurance_records",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_records_PolicyNumber",
                table: "insurance_records",
                column: "PolicyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_records_StartDate",
                table: "insurance_records",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_records_StatusId",
                table: "insurance_records",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_records_VehicleId",
                table: "insurance_records",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "insurance_documents");

            migrationBuilder.DropTable(
                name: "insurance_records");

            migrationBuilder.DropTable(
                name: "insurance_companies");
        }
    }
}
