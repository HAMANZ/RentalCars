using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FleetErp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInvestorEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "investors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    NationalId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_investors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_investors_lookup_items_StatusId",
                        column: x => x.StatusId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "investor_documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    InvestorId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_investor_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_investor_documents_investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "investors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_investor_documents_lookup_items_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_investor_documents_DocumentTypeId",
                table: "investor_documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_investor_documents_InvestorId",
                table: "investor_documents",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_investors_Email",
                table: "investors",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_investors_NationalId",
                table: "investors",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_investors_StatusId",
                table: "investors",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "investor_documents");

            migrationBuilder.DropTable(
                name: "investors");
        }
    }
}
