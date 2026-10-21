//hier nog testdata gemaakt morgen verder met en dan echte data erjn zetten
using PortfolioApi.Data;
using PortfolioApi.Features.Projects;


namespace PortfolioApi.Features.Projects;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ProjectResponse> GetAll()
    {
        return _context.Projects
            .Select(p => new ProjectResponse(
                p.Id,
                p.Titel,
                p.Beschrijving,
                p.Categorie,
                p.GitHubUrl,
                p.Datum))
            .ToList();
    }

    public ProjectResponse? GetById(int id)
    {
        var project = _context.Projects.Find(id);

        if (project is null) return null;

        return new ProjectResponse(
            project.Id,
            project.Titel,
            project.Beschrijving,
            project.Categorie,
            project.GitHubUrl,
            project.Datum);
    }
}
