using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Intial_migration_2026_8_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Announcements",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppLabels",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Label Name for the website"),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "friendly Name for Label"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: " Description for label"),
                    LanguagId = table.Column<int>(type: "int", nullable: false, comment: "For which language  this label"),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLabels", x => x.Id);
                },
                comment: "App Label table for adding the label of the website in different languages");

            migrationBuilder.CreateTable(
                name: "AppSettings",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Logo for website"),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationApiUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Contact data Used for website"),
                    PrivacyPolicy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefundPolicy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Email Used for website"),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Facebook Link"),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Twitter Link"),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "LinkedIn Link"),
                    Youtube = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Youtube Link"),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Instagram Link"),
                    Snapchat = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "snapchat Link"),
                    Tiktok = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "tiktok Link"),
                    Whatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Whatsapp Link"),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                },
                comment: "App Setting table to add all information used and related for the website like: Application name, Contact data ........");

            migrationBuilder.CreateTable(
                name: "Brands",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                },
                comment: "Brand Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "CarStatus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStatus", x => x.Id);
                },
                comment: "CarStatus Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "Contactus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Seen = table.Column<bool>(type: "bit", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactus", x => x.Id);
                },
                comment: "Contactus tabel");

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                },
                comment: "Country Table is for predefined countries used in the app");

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                },
                comment: "DocumentType Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "FuelTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                },
                comment: "Gender Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "InsuranceCompanies",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_ltr = table.Column<bool>(type: "bit", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.id);
                },
                comment: "Language Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "LoggerActions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Method Name Of Action Logged"),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Action Type: Add ,Read ,Delete ,Update"),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Parameters of the Action Logged"),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggerActions", x => x.Id);
                },
                comment: "Logger Action  Table  for adding the Logs for the action");

            migrationBuilder.CreateTable(
                name: "LoggerErrors",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Method Name Of Action Logged"),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Action Type: Add ,Read ,Delete ,Update"),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Parameters of the Action Logged"),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Exception Message of the Error"),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggerErrors", x => x.Id);
                },
                comment: "Logger Error   Table  for adding the Logs for the each error occured in app");

            migrationBuilder.CreateTable(
                name: "LookUpTables",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SysDate = table.Column<DateTime>(type: "date", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpTables", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuItems_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "dbo",
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageTemplates",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                },
                comment: "Nationality Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "User Id"),
                    Is_Seen = table.Column<bool>(type: "bit", nullable: false),
                    NotificationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                },
                comment: "Notification Table is for storing all notifications for each User");

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlateRegion",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlateRegion", x => x.Id);
                },
                comment: "PlateRegion Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "PlateTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlateTypes", x => x.Id);
                },
                comment: "PlateType Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "Repairs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LaborCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SAccountCategory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAccountCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                },
                comment: "Status Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "STransactionType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STransactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "City Table is for predefined cities used in the app");

            migrationBuilder.CreateTable(
                name: "LookUps",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableId = table.Column<int>(type: "int", nullable: true),
                    code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<long>(name: "UserId`", type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    SysDate = table.Column<DateTime>(type: "date", nullable: true),
                    isPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUps", x => x.id);
                    table.ForeignKey(
                        name: "FK_LookUps_LookUpTables_TableId",
                        column: x => x.TableId,
                        principalSchema: "dbo",
                        principalTable: "LookUpTables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LicensePlates",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlateTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PlateRegionId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicensePlates_PlateRegion_PlateRegionId",
                        column: x => x.PlateRegionId,
                        principalSchema: "dbo",
                        principalTable: "PlateRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicensePlates_PlateTypes_PlateTypeId",
                        column: x => x.PlateTypeId,
                        principalSchema: "dbo",
                        principalTable: "PlateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SAccountTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAccountTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SAccountTypes_SAccountCategory_AccountCategoryId",
                        column: x => x.AccountCategoryId,
                        principalSchema: "dbo",
                        principalTable: "SAccountCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "AccountType Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevorked = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "RefreshToken Table is for storing the token when I have to refresh the token in the api when the token is expired");

            migrationBuilder.CreateTable(
                name: "ResetPasswords",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    OTP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    InsertDateTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetPasswords_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ResetPassword Table is for storing otp code");

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<long>(type: "bigint", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "dbo",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LookUpMultiLang",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LookUpId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    SysDate = table.Column<DateTime>(type: "date", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpMultiLang", x => x.id);
                    table.ForeignKey(
                        name: "FK_LookUpMultiLang_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "dbo",
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LookUpMultiLang_LookUps_LookUpId",
                        column: x => x.LookUpId,
                        principalSchema: "dbo",
                        principalTable: "LookUps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    SysDate = table.Column<DateTime>(type: "date", nullable: true),
                    isVideo = table.Column<bool>(type: "bit", nullable: true),
                    LookUpId = table.Column<long>(type: "bigint", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.id);
                    table.ForeignKey(
                        name: "FK_Media_LookUps_LookUpId",
                        column: x => x.LookUpId,
                        principalSchema: "dbo",
                        principalTable: "LookUps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SAccounts",
                schema: "dbo",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountTypeId = table.Column<int>(type: "int", nullable: false),
                    OwnerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AccountTypeId1 = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAccounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_SAccounts_SAccountTypes_AccountTypeId1",
                        column: x => x.AccountTypeId1,
                        principalSchema: "dbo",
                        principalTable: "SAccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STransactions",
                schema: "dbo",
                columns: table => new
                {
                    TransactionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    DebitAccountId = table.Column<long>(type: "bigint", nullable: false),
                    CreditAccountId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    ReferenceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<int>(type: "int", nullable: true),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionTypeId1 = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_STransactions_SAccounts_CreditAccountId",
                        column: x => x.CreditAccountId,
                        principalSchema: "dbo",
                        principalTable: "SAccounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STransactions_SAccounts_DebitAccountId",
                        column: x => x.DebitAccountId,
                        principalSchema: "dbo",
                        principalTable: "SAccounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STransactions_STransactionType_TransactionTypeId1",
                        column: x => x.TransactionTypeId1,
                        principalSchema: "dbo",
                        principalTable: "STransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChassisNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    CurrentKM = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvestorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    FuelTypeId = table.Column<long>(type: "bigint", nullable: true),
                    LicensePlateId = table.Column<long>(type: "bigint", nullable: true),
                    CarOwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: true),
                    CarStatusId = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "dbo",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "dbo",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_CarStatus_CarStatusId",
                        column: x => x.CarStatusId,
                        principalSchema: "dbo",
                        principalTable: "CarStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_FuelTypes_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalSchema: "dbo",
                        principalTable: "FuelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_LicensePlates_LicensePlateId",
                        column: x => x.LicensePlateId,
                        principalSchema: "dbo",
                        principalTable: "LicensePlates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accidents",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accidents_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "dbo",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BatterySchedules",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    InstallDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LifeMonths = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Warranty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatterySchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatterySchedules_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "dbo",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarDocuments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarDocuments_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "dbo",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarDocuments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "dbo",
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inspections",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspections_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "dbo",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Premium = table.Column<double>(type: "float", nullable: false),
                    CoverageAmount = table.Column<double>(type: "float", nullable: true),
                    Deductible = table.Column<double>(type: "float", nullable: true),
                    CoverageDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RenewalReminderSent = table.Column<bool>(type: "bit", nullable: false),
                    InsuranceCompanyId = table.Column<long>(type: "bigint", nullable: true),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    InsuranceTypeId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurances_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "dbo",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insurances_InsuranceCompanies_InsuranceCompanyId",
                        column: x => x.InsuranceCompanyId,
                        principalSchema: "dbo",
                        principalTable: "InsuranceCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insurances_InsuranceTypes_InsuranceTypeId",
                        column: x => x.InsuranceTypeId,
                        principalSchema: "dbo",
                        principalTable: "InsuranceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insurances_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OilChangeSchedules",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeKM = table.Column<int>(type: "int", nullable: false),
                    ChangeIntervalKM = table.Column<int>(type: "int", nullable: false),
                    OilType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OilChangeSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OilChangeSchedules_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "dbo",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TireSchedules",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    InstallDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallKM = table.Column<int>(type: "int", nullable: false),
                    ExpectedKM = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TireSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TireSchedules_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "dbo",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Violations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Violations_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "dbo",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentKM = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "dbo",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceDocuments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    InsuranceId = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceDocuments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "dbo",
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceDocuments_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalSchema: "dbo",
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentalContracts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OdometerStart = table.Column<int>(type: "int", nullable: false),
                    OdometerEnd = table.Column<int>(type: "int", nullable: true),
                    DailyRate = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    PaidAmount = table.Column<double>(type: "float", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    InvestorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalContracts_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "dbo",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentalContracts_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentalPayments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true),
                    RentalContractId = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalPayments_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalSchema: "dbo",
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentalPayments_RentalContracts_RentalContractId",
                        column: x => x.RentalContractId,
                        principalSchema: "dbo",
                        principalTable: "RentalContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarOwners",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PassportNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CommercialRegister = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    CityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarOwners_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "dbo",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarOwners_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarOwners_Nationalities_NationalId",
                        column: x => x.NationalId,
                        principalSchema: "dbo",
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EUser",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EUserId = table.Column<long>(type: "bigint", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName_ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GenderId = table.Column<long>(type: "bigint", nullable: true),
                    FToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CustomerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_by = table.Column<long>(type: "bigint", nullable: false),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EUser_Gender_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "dbo",
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EUser_User_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_EUser_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "EUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Nationalities_NationalId",
                        column: x => x.NationalId,
                        principalSchema: "dbo",
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "dbo",
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_EUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "EUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Investors",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    NationalId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investors_EUser_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "EUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Investors_Nationalities_NationalId",
                        column: x => x.NationalId,
                        principalSchema: "dbo",
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Investors_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlateOwners",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlateOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlateOwners_EUser_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "EUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_EUser_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "EUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuPermissions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    CanView = table.Column<bool>(type: "bit", nullable: false),
                    CanCreate = table.Column<bool>(type: "bit", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuPermissions_EUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "EUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenuPermissions_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalSchema: "dbo",
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicensePlateOwnerships",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlateId = table.Column<long>(type: "bigint", nullable: true),
                    PlateOwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlateOwnerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicensePlateOwnerships_LicensePlates_LicensePlateId",
                        column: x => x.LicensePlateId,
                        principalSchema: "dbo",
                        principalTable: "LicensePlates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LicensePlateOwnerships_PlateOwners_PlateOwnerId",
                        column: x => x.PlateOwnerId,
                        principalSchema: "dbo",
                        principalTable: "PlateOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQty = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpareParts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "dbo",
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderDetails",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderId = table.Column<int>(type: "int", nullable: true),
                    RepairId = table.Column<int>(type: "int", nullable: true),
                    SparePartId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderDetails_Repairs_RepairId",
                        column: x => x.RepairId,
                        principalSchema: "dbo",
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderDetails_SpareParts_SparePartId",
                        column: x => x.SparePartId,
                        principalSchema: "dbo",
                        principalTable: "SpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderDetails_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalSchema: "dbo",
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AppSettings",
                columns: new[] { "Id", "ApplicationApiUrl", "ApplicationName", "ApplicationUrl", "ContactEmail", "ContactWebsite", "Created_at", "Created_by", "Description", "Email", "Facebook", "Instagram", "Is_deleted", "LicenseDetail", "LinkedIn", "Logo", "Mobile", "Mobile2", "Password", "Phone", "Phone2", "PrivacyPolicy", "RefundPolicy", "ShortDescription", "Snapchat", "TermsConditions", "Tiktok", "Twitter", "Updated_at", "Updated_by", "Whatsapp", "Youtube" },
                values: new object[] { 1L, null, "RentalCar", null, null, "", new DateTime(2026, 8, 12, 11, 13, 7, 22, DateTimeKind.Local).AddTicks(1757), 1L, "", "oonlinetutoring@gmail.com", "", null, false, "", null, "RentalCar.jpg", null, null, "P@ssw0rdsse", "09999999", null, null, null, null, null, null, null, "", new DateTime(2026, 8, 12, 11, 13, 7, 22, DateTimeKind.Local).AddTicks(1793), 1L, null, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Branches",
                columns: new[] { "Id", "CityId", "Created_at", "Created_by", "Is_deleted", "Name", "Phone", "Updated_at", "Updated_by" },
                values: new object[] { 1, null, new DateTime(2026, 8, 12, 11, 13, 7, 27, DateTimeKind.Local).AddTicks(6116), 1L, false, "Saida", null, new DateTime(2026, 8, 12, 11, 13, 7, 27, DateTimeKind.Local).AddTicks(6142), 1L });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Brands",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 15L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Jeep", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 17L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Volvo", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 16L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Porsche", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 20L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Renault", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 14L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Lexus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 13L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Mazda", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 12L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Mitsubishi", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 11L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Volkswagen", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 10L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Chevrolet", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 9L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Ford", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 8L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Audi", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 7L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "BMW", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 6L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Mercedes-Benz", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 5L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Honda", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 4L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Nissan", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 3L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Hyundai", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 2L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Toyota", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 1L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Kia", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 18L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Subaru", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 19L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Peugeot", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "CarStatus",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Available", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 3L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Reserved", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 4L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Maintenance", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 5L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Accident", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 6L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Out of Service", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 7L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Sold", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 8L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Inactive", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 2L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Rented", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Countries",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Description", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 2L, null, new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2831), 1L, null, false, "Lebanon", new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2835), 1L },
                    { 3L, null, new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2848), 1L, null, false, "Turkey", new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2853), 1L },
                    { 1L, null, new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2792), 1L, null, false, "Palestinne", new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(2816), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "DocumentTypes",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 6L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Customs Document", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 5L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Ownership Certificate", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 4L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Vehicle License", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 3L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Technical Inspection", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 2L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Vehicle Insurance", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 1L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Vehicle Registration", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 10L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Other", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 9L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Maintenance Record", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 7L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Purchase Invoice", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "DocumentTypes",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[] { 8L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Rental Agreement", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "FuelTypes",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 7L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "CNG", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 6L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "LPG", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 5L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Electric", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 4L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Plug-in Hybrid", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 3L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Hybrid", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 2L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Diesel", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 1L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Gasoline", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Gender",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 1L, "M", new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(8134), 1L, false, "Male", new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(8157), 1L },
                    { 2L, "F", new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(8171), 1L, false, "Female", new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(8178), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "InsuranceCompanies",
                columns: new[] { "Id", "Address", "Created_at", "Created_by", "Description", "Email", "Is_deleted", "Mobile", "Name", "Phone", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 8L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "LIA Assurex", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 7L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Arabia Insurance", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 6L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Arope Insurance", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 10L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Comin Insurance", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 11L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Cumberland Insurance", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 12L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Securite Assurance", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 13L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Commercial Insurance Company", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 14L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Trust Insurance", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 15L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "UCA", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 5L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Fidelity Assurance", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 4L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Bankers Assurance", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 9L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "GroupMed", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 2L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Libano-Suisse", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 3L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Allianz SNA", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 1L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, null, null, false, null, "Medgulf", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "InsuranceTypes",
                columns: new[] { "Id", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Full Coverage", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Personal Accident Insurance", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Theft Protection", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Collision Damage Waiver (CDW)", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Comprehensive", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Third Party", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "id", "Created_at", "Created_by", "Flag", "Is_deleted", "Is_ltr", "LanguageCode", "Name", "Name_ex", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(3998), 1L, null, false, false, "ar", "Arabic", null, new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(4052), 1L },
                    { 2, new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(4088), 1L, null, false, false, "en", "English", null, new DateTime(2026, 8, 12, 11, 13, 7, 23, DateTimeKind.Local).AddTicks(4097), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "MenuItems",
                columns: new[] { "Id", "Created_at", "Created_by", "Icon", "IsActive", "Is_deleted", "Name", "ParentId", "SortOrder", "Title", "Updated_at", "Updated_by", "Url" },
                values: new object[,]
                {
                    { 100, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9926), 0L, "fas fa-cogs", true, false, "Administration", null, 11, "Administration", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "#" },
                    { 10, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9663), 0L, "fas fa-car", true, false, "FleetManagement", null, 2, "Fleet Management", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "#" },
                    { 40, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9822), 0L, "fas fa-shield-alt", true, false, "Insurance", null, 5, "Insurance", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "#" },
                    { 50, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9839), 0L, "fas fa-tools", true, false, "Maintenance", null, 6, "Maintenance", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "#" },
                    { 60, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9863), 0L, "fas fa-folder", true, false, "Documents", null, 7, "Documents", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "#" },
                    { 70, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9872), 0L, "fas fa-exclamation-triangle", true, false, "AccidentsAndFines", null, 8, "Accidents & Fines", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "#" },
                    { 80, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9881), 0L, "fas fa-wallet", true, false, "Finance", null, 9, "Finance", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "#" },
                    { 90, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9895), 0L, "fas fa-chart-bar", true, false, "Reports", null, 10, "Reports", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "#" },
                    { 30, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9809), 0L, "fas fa-users", true, false, "Customers", null, 4, "Customers", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "#" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "MenuItems",
                columns: new[] { "Id", "Created_at", "Created_by", "Icon", "IsActive", "Is_deleted", "Name", "ParentId", "SortOrder", "Title", "Updated_at", "Updated_by", "Url" },
                values: new object[,]
                {
                    { 20, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9789), 0L, "fas fa-file-contract", true, false, "RentalManagement", null, 3, "Rental Management", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "#" },
                    { 1, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(2564), 0L, "fas fa-tachometer-alt", true, false, "Dashboard", null, 1, "Dashboard", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Dashboard" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PaymentMethods",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Cash", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 7L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Cheque", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 6L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Debit Card", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 5L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Credit Card", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 4L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Bank Transfer", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 3L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "WISH", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 2L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "OMT", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PlateRegion",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 2L, "Y", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Aley", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 1L, "B", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Beirut", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 4L, "N", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Nabatieh", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 3L, "G", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Jounieh", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 6L, "S", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Sidon", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 7L, "T", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Tripoli", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 5L, "O", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Ouzai", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 8L, "K", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Baalbek", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 9L, "Z", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Zahle", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PlateTypes",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 1L, "PRIVATE", new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(1977), 1L, false, "Private ", new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2037), 1L },
                    { 2L, "P", new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2054), 1L, false, "Public", new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2060), 1L },
                    { 4L, "RENTAL", new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2084), 1L, false, "Rental Vehicle", new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2089), 1L },
                    { 3L, "C", new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2069), 1L, false, "Company/Commercial", new DateTime(2026, 8, 12, 11, 13, 7, 24, DateTimeKind.Local).AddTicks(2074), 1L }
                });

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

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SAccountCategory",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Name_ar", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 4L, "EXPENSE", new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(6534), 0L, false, "EXPENSE", "المصروفات", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 3L, "REVENUE", new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(6532), 0L, false, "REVENUE", "الإيرادات", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 2L, "LIABILITY", new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(6523), 0L, false, "LIABILITY", "الخصوم", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 1L, "ASSET", new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(3297), 0L, false, "ASSET", "الأصول", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 5L, "EQUITY", new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(6540), 0L, false, "EQUITY", "حقوق الملكية", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SAccounts",
                columns: new[] { "AccountId", "AccountTypeId", "AccountTypeId1", "Balance", "Code", "Created_at", "Created_by", "Currency", "IsActive", "Is_deleted", "Name", "Name_ar", "OwnerId", "OwnerType", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 12L, 14, null, 0.0, "EXPENSE-BATTERY", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6547), 0L, "USD", true, false, "مصروف البطاريات", "", null, "Company", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 8L, 10, null, 0.0, "EXPENSE-MAINTENANCE", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6532), 0L, "USD", true, false, "مصروف صيانة السيارات", "", null, "Company", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 11L, 13, null, 0.0, "EXPENSE-TIRES", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6544), 0L, "USD", true, false, "مصروف الإطارات", "", null, "Company", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 13L, 16, null, 0.0, "EQUITY-INVESTOR-001", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6552), 0L, "USD", true, false, "رأس مال المستثمر - تجريبي", "", 1, "Investor", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 10L, 12, null, 0.0, "EXPENSE-INSURANCE", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6541), 0L, "USD", true, false, "مصروف التأمين", "", null, "Company", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 6L, 7, null, 0.0, "REVENUE-RENTAL", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6519), 0L, "USD", true, false, "إيرادات تأجير السيارات", "", null, "Company", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 7L, 8, null, 0.0, "REVENUE-FINES", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6527), 0L, "USD", true, false, "إيرادات الغرامات", "", null, "Company", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SAccounts",
                columns: new[] { "AccountId", "AccountTypeId", "AccountTypeId1", "Balance", "Code", "Created_at", "Created_by", "Currency", "IsActive", "Is_deleted", "Name", "Name_ar", "OwnerId", "OwnerType", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 5L, 5, null, 0.0, "INVESTOR-PAYABLE-001", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6515), 0L, "USD", true, false, "مستحقات المستثمر - تجريبي", "", 1, "Investor", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 4L, 4, null, 0.0, "VEHICLE-001", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6510), 0L, "USD", true, false, "حساب سيارة تجريبية", "", 1, "Vehicle", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 3L, 3, null, 0.0, "CUSTOMER-001", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6499), 0L, "USD", true, false, "حساب عميل تجريبي", "", 1, "Customer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 2L, 2, null, 0.0, "BANK-MAIN", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(5811), 0L, "USD", true, false, "الحساب البنكي الرئيسي", "الحساب البنكي الرئيسي", null, "Bank", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 1L, 1, null, 0.0, "CASHBOX-MAIN", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(9131), 0L, "USD", true, false, "الصندوق الرئيسي", "", null, "Cashbox", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 9L, 11, null, 0.0, "EXPENSE-FUEL", new DateTime(2026, 8, 12, 8, 13, 7, 31, DateTimeKind.Utc).AddTicks(6535), 0L, "USD", true, false, "مصروف الوقود", "", null, "Company", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Statuses",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 4L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Cancelled", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 3L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Pending", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 6L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Renewal Required", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 1L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Active", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 5L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Suspended", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 2L, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, false, "Expired", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "33f4d0b8-9568-4576-95a6-e1724aa153a2", 0, "b0b843e5-3bc8-4cb1-9326-52315b2fd8ac", "hudaabumayha.ham@gmail.com", false, false, null, null, "ADMIN", null, null, false, "dd16f79b-7db3-44db-9b6d-1984f3caca0e", false, "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(80), 1L, false, "AL-Kouds", new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(119), 1L },
                    { 2L, 2L, new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(134), 1L, false, "Beirut", new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(138), 1L },
                    { 3L, 3L, new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(150), 1L, false, "Istanbul", new DateTime(2026, 8, 12, 11, 13, 7, 25, DateTimeKind.Local).AddTicks(154), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "EUser",
                columns: new[] { "Id", "Created_at", "Created_by", "CustomerId", "CustomerId1", "EUserId", "FToken", "FullName", "FullName_ar", "GenderId", "Is_deleted", "LastLoginAt", "Profile", "Updated_at", "Updated_by" },
                values: new object[] { "33f4d0b8-9568-4576-95a6-e1724aa153a2", new DateTime(2026, 8, 12, 11, 13, 7, 1, DateTimeKind.Local).AddTicks(127), 1L, null, null, 0L, null, null, null, 1L, false, null, null, new DateTime(2026, 8, 12, 11, 13, 7, 21, DateTimeKind.Local).AddTicks(727), 1L });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "MenuItems",
                columns: new[] { "Id", "Created_at", "Created_by", "Icon", "IsActive", "Is_deleted", "Name", "ParentId", "SortOrder", "Title", "Updated_at", "Updated_by", "Url" },
                values: new object[,]
                {
                    { 61, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9865), 0L, "fas fa-file", true, false, "CarDocuments", 60, 1, "Car Documents", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/CarDocuments" },
                    { 62, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9868), 0L, "fas fa-file-alt", true, false, "DocumentTypes", 60, 2, "Document Types", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/DocumentTypes" },
                    { 71, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9875), 0L, "fas fa-car-crash", true, false, "Accidents", 70, 1, "Accidents", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Accidents" },
                    { 72, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9879), 0L, "fas fa-ticket-alt", true, false, "Fines", 70, 2, "Fines", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Fines" },
                    { 82, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9889), 0L, "fas fa-credit-card", true, false, "PaymentMethods", 80, 2, "Payment Methods", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/PaymentMethods" },
                    { 83, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9892), 0L, "fas fa-receipt", true, false, "Expenses", 80, 3, "Expenses", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Expenses" },
                    { 110, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(118), 0L, "fas fa-list-alt", true, false, "SAccountTypes", 80, 4, "Account Types", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/SAccountTypes" },
                    { 111, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(141), 0L, "fas fa-exchange-alt", true, false, "STransactionTypes", 80, 5, "Transaction Types", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/STransactionTypes" },
                    { 91, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9902), 0L, "fas fa-file-invoice", true, false, "RentalReport", 90, 1, "Rental Report", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Reports/Rental" },
                    { 92, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9906), 0L, "fas fa-tools", true, false, "MaintenanceReport", 90, 2, "Maintenance Report", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Reports/Maintenance" },
                    { 94, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9919), 0L, "fas fa-chart-line", true, false, "FinancialReport", 90, 4, "Financial Report", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Reports/Financial" },
                    { 55, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9859), 0L, "fas fa-tools", true, false, "Repairs", 50, 5, "Repairs", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Repairs" },
                    { 95, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9922), 0L, "fas fa-car", true, false, "FleetReport", 90, 5, "Fleet Report", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Reports/Fleet" },
                    { 101, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(31), 0L, "fas fa-users-cog", true, false, "Users", 100, 1, "Users", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Admin/Users" },
                    { 102, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(36), 0L, "fas fa-user-shield", true, false, "Roles", 100, 2, "Roles", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Admin/Roles" },
                    { 103, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(42), 0L, "fas fa-key", true, false, "UserMenuPermissions", 100, 3, "User Menu Permissions", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Admin/UserMenuPermissions" },
                    { 104, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(50), 0L, "fas fa-list", true, false, "MenuItems", 100, 4, "Menu Items", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Admin/MenuItems" },
                    { 105, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(57), 0L, "fas fa-bullhorn", true, false, "Announcements", 100, 5, "Announcements", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Admin/Announcements" },
                    { 106, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(72), 0L, "fas fa-cog", true, false, "SystemSettings", 100, 6, "System Settings", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Admin/Settings" },
                    { 112, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(144), 0L, "fas fa-check-circle", true, false, "Statuses", 100, 7, "Statuses", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Statuses" },
                    { 93, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9912), 0L, "fas fa-shield-alt", true, false, "InsuranceReport", 90, 3, "Insurance Report", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Reports/Insurance" },
                    { 54, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9854), 0L, "fas fa-car-battery", true, false, "BatterySchedule", 50, 4, "Battery Schedule", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/BatterySchedule" },
                    { 81, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9885), 0L, "fas fa-money-check-alt", true, false, "Payments", 80, 1, "Payments", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Payments" },
                    { 52, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9845), 0L, "fas fa-oil-can", true, false, "OilChangeSchedule", 50, 2, "Oil Change Schedule", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/OilChangeSchedule" },
                    { 11, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9674), 0L, "fas fa-car-side", true, false, "Cars", 10, 1, "Cars", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Cars" },
                    { 12, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9765), 0L, "fas fa-user-tie", true, false, "CarOwners", 10, 2, "Car Owners", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/CarOwners" },
                    { 13, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9771), 0L, "fas fa-tags", true, false, "Brands", 10, 3, "Brands", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Brands" },
                    { 53, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9851), 0L, "fas fa-circle", true, false, "TireSchedule", 50, 3, "Tire Schedule", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/TireSchedule" },
                    { 15, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9780), 0L, "fas fa-gas-pump", true, false, "FuelTypes", 10, 5, "Fuel Types", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/FuelTypes" },
                    { 16, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9785), 0L, "fas fa-info-circle", true, false, "CarStatuses", 10, 6, "Car Statuses", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/CarStatuses" },
                    { 107, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(86), 0L, "fas fa-info-circle", true, false, "PlateTypes", 10, 9, "Plate Types", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/PlateTypes" },
                    { 108, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(89), 0L, "fas fa-map-marker-alt", true, false, "PlateRegions", 10, 10, "Plate Regions", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/PlateRegions" },
                    { 109, new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(104), 0L, "fas fa-user-tie", true, false, "PlateOwners", 10, 11, "Plate Owners", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/PlateOwners" },
                    { 21, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9791), 0L, "fas fa-file-signature", true, false, "RentalContracts", 20, 1, "Rental Contracts", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/RentalContracts" },
                    { 14, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9777), 0L, "fas fa-car", true, false, "CarModels", 10, 4, "Car Models", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/CarModels" },
                    { 23, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9800), 0L, "fas fa-undo", true, false, "Returns", 20, 3, "Vehicle Returns", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Returns" },
                    { 22, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9797), 0L, "fas fa-calendar-check", true, false, "Reservations", 20, 2, "Reservations", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Reservations" },
                    { 51, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9842), 0L, "fas fa-wrench", true, false, "MaintenanceRecords", 50, 1, "Maintenance Records", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Maintenance" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "MenuItems",
                columns: new[] { "Id", "Created_at", "Created_by", "Icon", "IsActive", "Is_deleted", "Name", "ParentId", "SortOrder", "Title", "Updated_at", "Updated_by", "Url" },
                values: new object[,]
                {
                    { 43, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9832), 0L, "fas fa-shield-alt", true, false, "InsuranceTypes", 40, 3, "Insurance Types", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/InsuranceTypes" },
                    { 42, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9829), 0L, "fas fa-building", true, false, "InsuranceCompanies", 40, 2, "Insurance Companies", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/InsuranceCompanies" },
                    { 44, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9836), 0L, "fas fa-check-circle", true, false, "InsuranceStatuses", 40, 4, "Insurance Statuses", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/InsuranceStatuses" },
                    { 41, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9825), 0L, "fas fa-file-alt", true, false, "InsurancePolicies", 40, 1, "Insurance Policies", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Insurance" },
                    { 32, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9818), 0L, "fas fa-id-card", true, false, "CustomerDocuments", 30, 2, "Customer Documents", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/CustomerDocuments" },
                    { 31, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9815), 0L, "fas fa-user", true, false, "CustomerList", 30, 1, "Customers", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/Customers" },
                    { 24, new DateTime(2026, 8, 12, 8, 13, 7, 28, DateTimeKind.Utc).AddTicks(9806), 0L, "fas fa-money-bill-wave", true, false, "RentalPayments", 20, 4, "Rental Payments", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "/RentalPayments" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SAccountTypes",
                columns: new[] { "Id", "AccountCategoryId", "Code", "Created_at", "Created_by", "IsActive", "Is_deleted", "Name", "Name_ar", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 9L, 3L, "OTHER_REVENUE", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4285), 0L, true, false, "Other Revenue", "إيرادات أخرى", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 13L, 4L, "TIRE", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4300), 0L, true, false, "Tires", "الإطارات", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 12L, 4L, "INSURANCE", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4296), 0L, true, false, "Insurance", "التأمين", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 11L, 4L, "FUEL", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4292), 0L, true, false, "Fuel", "الوقود", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 10L, 4L, "MAINTENANCE", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4289), 0L, true, false, "Vehicle Maintenance", "صيانة السيارات", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 14L, 4L, "BATTERY", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4303), 0L, true, false, "Batteries", "البطاريات", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 8L, 3L, "FINE_REVENUE", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4283), 0L, true, false, "Fine Revenue", "إيرادات الغرامات", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 3L, 1L, "CUSTOMER", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4255), 0L, true, false, "Customer Accounts", "حسابات العملاء", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 6L, 2L, "OTHER_LIABILITY", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4272), 0L, true, false, "Other Liabilities", "التزامات أخرى", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 5L, 2L, "INVESTOR_PAYABLE", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4269), 0L, true, false, "Investor Payables", "مستحقات المستثمرين", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 4L, 1L, "VEHICLE", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4259), 0L, true, false, "Vehicles", "السيارات", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 2L, 1L, "BANK", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4243), 0L, true, false, "Bank", "البنك", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 1L, 1L, "CASH", new DateTime(2026, 8, 12, 8, 13, 7, 29, DateTimeKind.Utc).AddTicks(9586), 0L, true, false, "Cash", "الصندوق", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 15L, 4L, "OTHER_EXPENSE", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4305), 0L, true, false, "Other Expenses", "مصاريف أخرى", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 7L, 3L, "RENTAL_REVENUE", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4280), 0L, true, false, "Car Rental Revenue", "إيرادات تأجير السيارات", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L },
                    { 16L, 5L, "INVESTOR_CAPITAL", new DateTime(2026, 8, 12, 8, 13, 7, 30, DateTimeKind.Utc).AddTicks(4309), 0L, true, false, "Investor Capital", "رأس مال المستثمرين", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accidents_CarId",
                schema: "dbo",
                table: "Accidents",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_BatterySchedules_CarId",
                schema: "dbo",
                table: "BatterySchedules",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CityId",
                schema: "dbo",
                table: "Branches",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CarDocuments_CarId",
                schema: "dbo",
                table: "CarDocuments",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarDocuments_DocumentTypeId",
                schema: "dbo",
                table: "CarDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CarOwners_CityId",
                schema: "dbo",
                table: "CarOwners",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CarOwners_CountryId",
                schema: "dbo",
                table: "CarOwners",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CarOwners_NationalId",
                schema: "dbo",
                table: "CarOwners",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BranchId",
                schema: "dbo",
                table: "Cars",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                schema: "dbo",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarOwnerId",
                schema: "dbo",
                table: "Cars",
                column: "CarOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarStatusId",
                schema: "dbo",
                table: "Cars",
                column: "CarStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_FuelTypeId",
                schema: "dbo",
                table: "Cars",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_InvestorId",
                schema: "dbo",
                table: "Cars",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_LicensePlateId",
                schema: "dbo",
                table: "Cars",
                column: "LicensePlateId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                schema: "dbo",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_NationalId",
                schema: "dbo",
                table: "Customers",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeId",
                schema: "dbo",
                table: "Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                schema: "dbo",
                table: "Documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EUser_CustomerId1",
                schema: "dbo",
                table: "EUser",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_EUser_GenderId",
                schema: "dbo",
                table: "EUser",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_CarId",
                schema: "dbo",
                table: "Inspections",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceDocuments_DocumentTypeId",
                schema: "dbo",
                table: "InsuranceDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceDocuments_InsuranceId",
                schema: "dbo",
                table: "InsuranceDocuments",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_CarId",
                schema: "dbo",
                table: "Insurances",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsuranceCompanyId",
                schema: "dbo",
                table: "Insurances",
                column: "InsuranceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsuranceTypeId",
                schema: "dbo",
                table: "Insurances",
                column: "InsuranceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_StatusId",
                schema: "dbo",
                table: "Insurances",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Investors_NationalId",
                schema: "dbo",
                table: "Investors",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_Investors_StatusId",
                schema: "dbo",
                table: "Investors",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlateOwnerships_LicensePlateId",
                schema: "dbo",
                table: "LicensePlateOwnerships",
                column: "LicensePlateId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlateOwnerships_PlateOwnerId",
                schema: "dbo",
                table: "LicensePlateOwnerships",
                column: "PlateOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlates_PlateRegionId",
                schema: "dbo",
                table: "LicensePlates",
                column: "PlateRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlates_PlateTypeId",
                schema: "dbo",
                table: "LicensePlates",
                column: "PlateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LookUpMultiLang_LanguageId",
                schema: "dbo",
                table: "LookUpMultiLang",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LookUpMultiLang_LookUpId",
                schema: "dbo",
                table: "LookUpMultiLang",
                column: "LookUpId");

            migrationBuilder.CreateIndex(
                name: "IX_LookUps_TableId",
                schema: "dbo",
                table: "LookUps",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_LookUpId",
                schema: "dbo",
                table: "Media",
                column: "LookUpId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ParentId",
                schema: "dbo",
                table: "MenuItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OilChangeSchedules_CarId",
                schema: "dbo",
                table: "OilChangeSchedules",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "dbo",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_CarId",
                schema: "dbo",
                table: "RentalContracts",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_CustomerId",
                schema: "dbo",
                table: "RentalContracts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_InvestorId",
                schema: "dbo",
                table: "RentalContracts",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_StatusId",
                schema: "dbo",
                table: "RentalContracts",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalPayments_PaymentMethodId",
                schema: "dbo",
                table: "RentalPayments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalPayments_RentalContractId",
                schema: "dbo",
                table: "RentalPayments",
                column: "RentalContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswords_UserId",
                schema: "dbo",
                table: "ResetPasswords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "dbo",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SAccounts_AccountTypeId1",
                schema: "dbo",
                table: "SAccounts",
                column: "AccountTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_SAccountTypes_AccountCategoryId",
                schema: "dbo",
                table: "SAccountTypes",
                column: "AccountCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_SupplierId",
                schema: "dbo",
                table: "SpareParts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_STransactions_CreditAccountId",
                schema: "dbo",
                table: "STransactions",
                column: "CreditAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_STransactions_DebitAccountId",
                schema: "dbo",
                table: "STransactions",
                column: "DebitAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_STransactions_TransactionTypeId1",
                schema: "dbo",
                table: "STransactions",
                column: "TransactionTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_TireSchedules_CarId",
                schema: "dbo",
                table: "TireSchedules",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "dbo",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "dbo",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuPermissions_MenuItemId",
                schema: "dbo",
                table: "UserMenuPermissions",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuPermissions_UserId_MenuItemId",
                schema: "dbo",
                table: "UserMenuPermissions",
                columns: new[] { "UserId", "MenuItemId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "dbo",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_CarId",
                schema: "dbo",
                table: "Violations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderDetails_RepairId",
                schema: "dbo",
                table: "WorkOrderDetails",
                column: "RepairId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderDetails_SparePartId",
                schema: "dbo",
                table: "WorkOrderDetails",
                column: "SparePartId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderDetails_WorkOrderId",
                schema: "dbo",
                table: "WorkOrderDetails",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_CarId",
                schema: "dbo",
                table: "WorkOrders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_StatusId",
                schema: "dbo",
                table: "WorkOrders",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarOwners_CarOwnerId",
                schema: "dbo",
                table: "Cars",
                column: "CarOwnerId",
                principalSchema: "dbo",
                principalTable: "CarOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Investors_InvestorId",
                schema: "dbo",
                table: "Cars",
                column: "InvestorId",
                principalSchema: "dbo",
                principalTable: "Investors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalContracts_Customers_CustomerId",
                schema: "dbo",
                table: "RentalContracts",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalContracts_Investors_InvestorId",
                schema: "dbo",
                table: "RentalContracts",
                column: "InvestorId",
                principalSchema: "dbo",
                principalTable: "Investors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarOwners_EUser_Id",
                schema: "dbo",
                table: "CarOwners",
                column: "Id",
                principalSchema: "dbo",
                principalTable: "EUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_EUser_Id",
                schema: "dbo",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Accidents",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Announcements",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AppLabels",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AppSettings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BatterySchedules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CarDocuments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Contactus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Inspections",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InsuranceDocuments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LicensePlateOwnerships",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LoggerActions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LoggerErrors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LookUpMultiLang",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Media",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MessageTemplates",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OilChangeSchedules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RentalPayments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ResetPasswords",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "STransactions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TireSchedules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserMenuPermissions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Violations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WorkOrderDetails",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DocumentTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Insurances",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PlateOwners",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LookUps",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PaymentMethods",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RentalContracts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SAccounts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "STransactionType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MenuItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Repairs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SpareParts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WorkOrders",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InsuranceCompanies",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InsuranceTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LookUpTables",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SAccountTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Cars",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SAccountCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Brands",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CarOwners",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CarStatus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FuelTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Investors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LicensePlates",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Statuses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PlateRegion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PlateTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Gender",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Nationalities",
                schema: "dbo");
        }
    }
}
