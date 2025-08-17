using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Foss.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptTerms = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "Id", "AcceptTerms", "Country", "Dob", "Email", "Gender", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, true, 0, new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.johnson@example.com", 0, "Alice Johnson", "+92-300-1234567" },
                    { 2, true, 3, new DateTime(1990, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "brian.smith@example.com", 1, "Brian Smith", "+45-20-123456" },
                    { 3, true, 4, new DateTime(1988, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "carlos.diaz@example.com", 1, "Carlos Diaz", "+212-600-123456" },
                    { 4, true, 2, new DateTime(1992, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "diana.miller@example.com", 0, "Diana Miller", "+44-7700-900123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Speakers");
        }
    }
}
