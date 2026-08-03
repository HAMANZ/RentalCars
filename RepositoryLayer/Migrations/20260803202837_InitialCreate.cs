using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class InitialCreate : Migration
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
                name: "AppLabel",
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
                    table.PrimaryKey("PK_AppLabel", x => x.Id);
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
                name: "Brand",
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
                    table.PrimaryKey("PK_Brand", x => x.Id);
                },
                comment: "Brand Table is for predefined data used in the app");

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
                name: "Country",
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
                    table.PrimaryKey("PK_Country", x => x.Id);
                },
                comment: "Country Table is for predefined countries used in the app");

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
                name: "Language",
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
                    table.PrimaryKey("PK_Language", x => x.id);
                },
                comment: "Language Table is for predefined data used in the app");

            migrationBuilder.CreateTable(
                name: "LicensePlate",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoggerAction",
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
                    table.PrimaryKey("PK_LoggerAction", x => x.Id);
                },
                comment: "Logger Action  Table  for adding the Logs for the action");

            migrationBuilder.CreateTable(
                name: "LoggerError",
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
                    table.PrimaryKey("PK_LoggerError", x => x.Id);
                },
                comment: "Logger Error   Table  for adding the Logs for the each error occured in app");

            migrationBuilder.CreateTable(
                name: "LookUpTable",
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
                    table.PrimaryKey("PK_LookUpTable", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTemplate",
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
                    table.PrimaryKey("PK_MessageTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationality",
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
                    table.PrimaryKey("PK_Nationality", x => x.Id);
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
                name: "PlateOwner",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlateOwner", x => x.Id);
                });

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
                name: "Suppliers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
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
                name: "City",
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
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "Country",
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
                        name: "FK_LookUps_LookUpTable_TableId",
                        column: x => x.TableId,
                        principalSchema: "dbo",
                        principalTable: "LookUpTable",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalId = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Nationality_NationalId",
                        column: x => x.NationalId,
                        principalSchema: "dbo",
                        principalTable: "Nationality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LicensePlateOwnership",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlateId = table.Column<long>(type: "bigint", nullable: true),
                    PlateOwnerId = table.Column<int>(type: "int", nullable: false),
                    PlateOwnerId1 = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_LicensePlateOwnership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicensePlateOwnership_LicensePlate_LicensePlateId",
                        column: x => x.LicensePlateId,
                        principalSchema: "dbo",
                        principalTable: "LicensePlate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LicensePlateOwnership_PlateOwner_PlateOwnerId1",
                        column: x => x.PlateOwnerId1,
                        principalSchema: "dbo",
                        principalTable: "PlateOwner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    SupplierId = table.Column<int>(type: "int", nullable: true),
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
                name: "EUser",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EUserId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName_ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName_ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderId = table.Column<long>(type: "bigint", nullable: false),
                    FToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EUser_User_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                        name: "FK_Branches_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "dbo",
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarOwners",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PassportNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CommercialRegister = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    CityId = table.Column<long>(type: "bigint", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarOwners_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "dbo",
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarOwners_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarOwners_Nationality_NationalId",
                        column: x => x.NationalId,
                        principalSchema: "dbo",
                        principalTable: "Nationality",
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
                        name: "FK_LookUpMultiLang_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "dbo",
                        principalTable: "Language",
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
                name: "Cars",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentKM = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    LicensePlateId = table.Column<long>(type: "bigint", nullable: true),
                    CarOwnerId = table.Column<long>(type: "bigint", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: true),
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
                        name: "FK_Cars_Brand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "dbo",
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_CarOwners_CarOwnerId",
                        column: x => x.CarOwnerId,
                        principalSchema: "dbo",
                        principalTable: "CarOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_LicensePlate_LicensePlateId",
                        column: x => x.LicensePlateId,
                        principalSchema: "dbo",
                        principalTable: "LicensePlate",
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
                    CarId = table.Column<int>(type: "int", nullable: true),
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
                    CarId = table.Column<int>(type: "int", nullable: true),
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
                name: "Inspection",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Inspection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspection_Cars_CarId",
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
                    CarId = table.Column<int>(type: "int", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceCompanyId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "OilChangeSchedules",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeKM = table.Column<int>(type: "int", nullable: false),
                    ChangeIntervalKM = table.Column<int>(type: "int", nullable: false),
                    OilType = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "RentalContracts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DailyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remaining = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_RentalContracts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
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
                    CarId = table.Column<int>(type: "int", nullable: true),
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
                    CarId = table.Column<int>(type: "int", nullable: true),
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
                    CarId = table.Column<int>(type: "int", nullable: true),
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
                name: "Payment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalContractId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is Deleted Record"),
                    Created_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Created_by"),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false, comment: "User Id that Updated_by"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Created_at"),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime that Updated_at")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_RentalContracts_RentalContractId",
                        column: x => x.RentalContractId,
                        principalSchema: "dbo",
                        principalTable: "RentalContracts",
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
                values: new object[] { 1L, null, "RentalCar", null, null, "", new DateTime(2026, 8, 3, 23, 28, 36, 218, DateTimeKind.Local).AddTicks(4883), 1L, "", "oonlinetutoring@gmail.com", "", null, false, "", null, "RentalCar.jpg", null, null, "P@ssw0rdsse", "09999999", null, null, null, null, null, null, null, "", new DateTime(2026, 8, 3, 23, 28, 36, 218, DateTimeKind.Local).AddTicks(4908), 1L, null, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Country",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Description", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(3481), 1L, null, false, "Palestinne", new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(3496), 1L },
                    { 2L, null, new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(3502), 1L, null, false, "Lebanon", new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(3505), 1L },
                    { 3L, null, new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(3509), 1L, null, false, "Turkey", new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(3512), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Gender",
                columns: new[] { "Id", "Code", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 1L, "M", new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(195), 1L, false, "Male", new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(214), 1L },
                    { 2L, "F", new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(225), 1L, false, "Female", new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(228), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Language",
                columns: new[] { "id", "Created_at", "Created_by", "Flag", "Is_deleted", "Is_ltr", "LanguageCode", "Name", "Name_ex", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 3, 23, 28, 36, 218, DateTimeKind.Local).AddTicks(8273), 1L, null, false, false, "ar", "Arabic", null, new DateTime(2026, 8, 3, 23, 28, 36, 218, DateTimeKind.Local).AddTicks(8295), 1L },
                    { 2, new DateTime(2026, 8, 3, 23, 28, 36, 218, DateTimeKind.Local).AddTicks(8306), 1L, null, false, false, "en", "English", null, new DateTime(2026, 8, 3, 23, 28, 36, 218, DateTimeKind.Local).AddTicks(8309), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c2462983-7a27-4b10-bb93-51446f2485aa", "43214d49-86a2-47a9-b56d-fc0d87d64cc1", "EUser", "EUSER" },
                    { "b4bd84c9-8aeb-4493-9517-34425416ad85", "9368cbd1-882f-424e-8c8e-aaf015972f4d", "Adminstrator", "ADMINSTRATOR" },
                    { "7401a50e-f530-42af-b7d4-a6123c86d3ad", "40f23dbe-5571-4c27-a99d-d7405e5a2d95", "Tutor", "TUTOR" },
                    { "fd4e661c-af07-4d20-b769-5b474f002c76", "90c81468-bea8-490c-bc17-47f553fd731c", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d78ed66e-fa44-453f-b5f1-b4b0ee8ac2ce", 0, "bc2f8758-7b49-4dfa-8946-bd6cfc307a89", "hudaabumayha.ham@gmail.com", false, false, null, null, "ADMIN", null, null, false, "f7e71d6a-b20b-4547-bcd4-159febadeae6", false, "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "City",
                columns: new[] { "Id", "CountryId", "Created_at", "Created_by", "Is_deleted", "Name", "Updated_at", "Updated_by" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(2052), 1L, false, "AL-Kouds", new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(2071), 1L },
                    { 2L, 2L, new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(2079), 1L, false, "Beirut", new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(2083), 1L },
                    { 3L, 3L, new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(2088), 1L, false, "Istanbul", new DateTime(2026, 8, 3, 23, 28, 36, 219, DateTimeKind.Local).AddTicks(2091), 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "EUser",
                columns: new[] { "Id", "Created_at", "Created_by", "EUserId", "FToken", "FirstName_ar", "FirstName_en", "GenderId", "Is_deleted", "LastName_ar", "LastName_en", "Profile", "Updated_at", "Updated_by" },
                values: new object[] { "d78ed66e-fa44-453f-b5f1-b4b0ee8ac2ce", new DateTime(2026, 8, 3, 23, 28, 36, 196, DateTimeKind.Local).AddTicks(9215), 1L, 0L, null, null, null, 1L, false, null, null, null, new DateTime(2026, 8, 3, 23, 28, 36, 217, DateTimeKind.Local).AddTicks(9218), 1L });

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
                name: "IX_Cars_LicensePlateId",
                schema: "dbo",
                table: "Cars",
                column: "LicensePlateId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                schema: "dbo",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_NationalId",
                schema: "dbo",
                table: "Customers",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_EUser_GenderId",
                schema: "dbo",
                table: "EUser",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspection_CarId",
                schema: "dbo",
                table: "Inspection",
                column: "CarId");

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
                name: "IX_LicensePlateOwnership_LicensePlateId",
                schema: "dbo",
                table: "LicensePlateOwnership",
                column: "LicensePlateId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlateOwnership_PlateOwnerId1",
                schema: "dbo",
                table: "LicensePlateOwnership",
                column: "PlateOwnerId1");

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
                name: "IX_OilChangeSchedules_CarId",
                schema: "dbo",
                table: "OilChangeSchedules",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RentalContractId",
                schema: "dbo",
                table: "Payment",
                column: "RentalContractId");

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
                name: "IX_SpareParts_SupplierId",
                schema: "dbo",
                table: "SpareParts",
                column: "SupplierId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accidents",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Announcements",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AppLabel",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AppSettings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BatterySchedules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Contactus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Inspection",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Insurances",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LicensePlateOwnership",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LoggerAction",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LoggerError",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LookUpMultiLang",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Media",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MessageTemplate",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OilChangeSchedules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ResetPasswords",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoleClaims",
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
                name: "Gender",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InsuranceCompanies",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PlateOwner",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Language",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LookUps",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RentalContracts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
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
                name: "LookUpTable",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Cars",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Statuses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Brand",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CarOwners",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LicensePlate",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "City",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Nationality",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "dbo");
        }
    }
}
