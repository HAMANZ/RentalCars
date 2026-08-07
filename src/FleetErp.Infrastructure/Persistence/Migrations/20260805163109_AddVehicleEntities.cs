using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FleetErp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlateNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Make = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Vin = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    EngineNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ChassisNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Odometer = table.Column<int>(type: "int", nullable: false),
                    DailyRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Notes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    InvestorId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    FuelTypeId = table.Column<int>(type: "int", nullable: false),
                    TransmissionTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vehicles_investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "investors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicles_lookup_items_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicles_lookup_items_StatusId",
                        column: x => x.StatusId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicles_lookup_items_TransmissionTypeId",
                        column: x => x.TransmissionTypeId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicles_lookup_items_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vehicle_documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vehicle_documents_lookup_items_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicle_documents_vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_documents_DocumentTypeId",
                table: "vehicle_documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_documents_VehicleId",
                table: "vehicle_documents",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_FuelTypeId",
                table: "vehicles",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_InvestorId",
                table: "vehicles",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_PlateNumber",
                table: "vehicles",
                column: "PlateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_StatusId",
                table: "vehicles",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_TransmissionTypeId",
                table: "vehicles",
                column: "TransmissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_VehicleTypeId",
                table: "vehicles",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_Vin",
                table: "vehicles",
                column: "Vin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicle_documents");

            migrationBuilder.DropTable(
                name: "vehicles");
        }
    }
}
