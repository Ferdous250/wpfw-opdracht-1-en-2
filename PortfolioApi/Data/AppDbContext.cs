
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
}