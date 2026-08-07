using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FleetErp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    notification_type_id = table.Column<int>(type: "int", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    message = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    reference_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    reference_id = table.Column<int>(type: "int", nullable: true),
                    due_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    read_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    read_by_user_id = table.Column<int>(type: "int", nullable: true),
                    dismissed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    dismissed_by_user_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_notifications_lookup_items_notification_type_id",
                        column: x => x.notification_type_id,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notifications_lookup_items_status_id",
                        column: x => x.status_id,
                        principalTable: "lookup_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_due_at",
                table: "notifications",
                column: "due_at");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_notification_type_id",
                table: "notifications",
                column: "notification_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_reference_type_reference_id",
                table: "notifications",
                columns: new[] { "reference_type", "reference_id" });

            migrationBuilder.CreateIndex(
                name: "IX_notifications_status_id",
                table: "notifications",
                column: "status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notifications");
        }
    }
}
