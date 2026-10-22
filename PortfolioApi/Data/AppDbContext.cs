
//haalt entitiy framework en project model op.
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Features.Projects;

namespace PortfolioApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();


protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // test data zodat de tabel niet leeg blijft waneer de databse aangemaakt word
    modelBuilder.Entity<Project>().HasData(
        new Project
        {
            Id = 1,
            Titel = "Portfolio API",
            Beschrijving = "Een ASP.NET Core Web API voor mijn persoonlijke portfolio.",
            Categorie = "Backend",
            GitHubUrl = "https://github.com/jouw-gebruikersnaam/portfolio-api",
            Datum = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc)
        },
        new Project
        {
            Id = 2,
            Titel = "Task Manager App",
            Beschrijving = "Een handige webapplicatie om dagelijkse taken te beheren.",
            Categorie = "Fullstack",
            GitHubUrl = "https://github.com/jouw-gebruikersnaam/task-manager",
            Datum = new DateTime(2026, 2, 10, 0, 0, 0, DateTimeKind.Utc)
        }
    );
}
}