using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;

namespace PortfolioApi.Features.Projects;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    // 1. hier een centrale plek gemaakt voor de mapping (hoort bij Criterium 7)
    private static ProjectResponse ToResponse(Project p) => new(
        p.Id,
        p.Titel,
        p.Beschrijving,
        p.Categorie,
        p.GitHubUrl,
        p.Datum
    );

    // 2. Alle projecten opvragen (Async + AsNoTracking dit zorgt voor beteree performance)
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(CancellationToken ct = default)
    {
        var projects = await _context.Projects
            .AsNoTracking()
            .ToListAsync(ct);

        return projects.Select(ToResponse);
    }

    // 3. Eén project opvragen op basis van ID (Async)
    public async Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var project = await _context.Projects.FindAsync(new object[] { id }, ct);

        return project is null ? null : ToResponse(project);
    }


    // 3. Een nieuw project aanmaken (POST)
    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct = default)
    {
        var project = new Project
        {
            Titel = request.Titel,
            Beschrijving = request.Beschrijving,
            Categorie = request.Categorie,
            GitHubUrl = request.GitHubUrl ?? string.Empty,
            Datum = request.Datum
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync(ct);

        return ToResponse(project);
    }

    // 4. Een bestaand project bijwerken (PUT)
    public async Task<ProjectResponse?> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken ct = default)
    {
        var project = await _context.Projects.FindAsync(new object[] { id }, ct);
        if (project is null) return null;

        project.Titel = request.Titel;
        project.Beschrijving = request.Beschrijving;
        project.Categorie = request.Categorie;
        project.GitHubUrl = request.GitHubUrl ?? string.Empty;
        project.Datum = request.Datum;

        await _context.SaveChangesAsync(ct);

        return ToResponse(project);
    }

    // 5. Een project verwijderen (DELETE)
    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var project = await _context.Projects.FindAsync(new object[] { id }, ct);
        if (project is null) return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}