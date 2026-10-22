using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Beschrijving", "Categorie", "Datum", "GitHubUrl", "Titel" },
                values: new object[,]
                {
                    { 1, "Een ASP.NET Core Web API voor mijn persoonlijke portfolio.", "Backend", new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "https://github.com/jouw-gebruikersnaam/portfolio-api", "Portfolio API" },
                    { 2, "Een handige webapplicatie om dagelijkse taken te beheren.", "Fullstack", new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "https://github.com/jouw-gebruikersnaam/task-manager", "Task Manager App" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
