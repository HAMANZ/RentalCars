using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Add_Repair_DescriptionAr_NoteAr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartsCost",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "RepairDate",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.AddColumn<double>(
                name: "PartsCost",
                schema: "dbo",
                table: "WorkOrders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCost",
                schema: "dbo",
                table: "WorkOrders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "price",
                schema: "dbo",
                table: "WorkOrders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Description_ar",
                schema: "dbo",
                table: "Repairs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note_ar",
                schema: "dbo",
                table: "Repairs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartsCost",
                schema: "dbo",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                schema: "dbo",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "price",
                schema: "dbo",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "Description_ar",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "Note_ar",
                schema: "dbo",
                table: "Repairs");

            migrationBuilder.AddColumn<double>(
                name: "PartsCost",
                schema: "dbo",
                table: "Repairs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RepairDate",
                schema: "dbo",
                table: "Repairs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "TotalCost",
                schema: "dbo",
                table: "Repairs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
