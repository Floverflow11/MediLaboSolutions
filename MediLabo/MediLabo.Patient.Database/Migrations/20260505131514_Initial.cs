using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediLabo.Patient.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "DateOfBirth", "FirstName", "Gender", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "1 Brookside St", new DateOnly(1966, 12, 31), "Test", "F", "TestNone", "100-222-3333" },
                    { 2, "2 High St", new DateOnly(1945, 6, 24), "Test", "M", "TestBorderline", "200-333-4444" },
                    { 3, "3 Club Road", new DateOnly(2004, 6, 18), "Test", "M", "TestInDanger", "300-444-5555" },
                    { 4, "4 Valley Dr", new DateOnly(2002, 6, 28), "Test", "F", "TestEarlyOnset", "400-555-6666" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
