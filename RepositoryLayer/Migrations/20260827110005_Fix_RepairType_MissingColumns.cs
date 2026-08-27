using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Fix_RepairType_MissingColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "dbo",
                table: "RepairType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "RepairType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RepairCategoryId",
                schema: "dbo",
                table: "RepairType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RepairType_RepairCategoryId",
                schema: "dbo",
                table: "RepairType",
                column: "RepairCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairType_RepairCategory_RepairCategoryId",
                schema: "dbo",
                table: "RepairType",
                column: "RepairCategoryId",
                principalSchema: "dbo",
                principalTable: "RepairCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairType_RepairCategory_RepairCategoryId",
                schema: "dbo",
                table: "RepairType");

            migrationBuilder.DropIndex(
                name: "IX_RepairType_RepairCategoryId",
                schema: "dbo",
                table: "RepairType");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "dbo",
                table: "RepairType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "RepairType");

            migrationBuilder.DropColumn(
                name: "RepairCategoryId",
                schema: "dbo",
                table: "RepairType");
        }
    }
}
