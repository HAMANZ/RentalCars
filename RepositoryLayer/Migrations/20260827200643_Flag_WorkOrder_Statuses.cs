using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Flag_WorkOrder_Statuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Is_WorkOrderStatus",
                value: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Is_WorkOrderStatus",
                value: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Is_WorkOrderStatus",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Is_WorkOrderStatus",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Is_WorkOrderStatus",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Is_WorkOrderStatus",
                value: false);
        }
    }
}
