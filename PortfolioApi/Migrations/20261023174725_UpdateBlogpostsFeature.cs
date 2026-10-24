using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlogpostsFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "GitHubUrl",
                value: "https://github.com/Ferdous250/wpfw-opdracht-1-en-2");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Beschrijving", "GitHubUrl", "Titel" },
                values: new object[] { "Een handige webapplicatie.", "https://github.com/Ferdous250/wpfw-opdracht-1-en-2", "Task App" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "GitHubUrl",
                value: "https://github.com/jouw-gebruikersnaam/portfolio-api");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Beschrijving", "GitHubUrl", "Titel" },
                values: new object[] { "Een handige webapplicatie om dagelijkse taken te beheren.", "https://github.com/jouw-gebruikersnaam/task-manager", "Task Manager App" });
        }
    }
}
