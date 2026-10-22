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

    // 1. Alle projecten opvragen (Async)
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(CancellationToken cancelt = default)
    {
        return await _context.Projects
            .Select(p => new ProjectResponse(
                p.Id,
                p.Titel,
                p.Beschrijving,
                p.Categorie,
                p.GitHubUrl,
                p.Datum))
            .ToListAsync(cancelt);
    }


    // 2. Eén project opvragen op basis van ID (Async)
    public async Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken cancelt = default)
    {
        var project = await _context.Projects.FindAsync(new object[] { id }, cancelt);

        if (project is null) return null;

        return new ProjectResponse(
            project.Id,
            project.Titel,
            project.Beschrijving,
            project.Categorie,
            project.GitHubUrl,
            project.Datum);
    }

    // 3. Een nieuw project aanmaken (POST)
    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken cancelt = default)
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
        await _context.SaveChangesAsync(cancelt);

        return new ProjectResponse(
            project.Id,
            project.Titel,
            project.Beschrijving,
            project.Categorie,
            project.GitHubUrl,
            project.Datum);
    }

    // 4. Een bestaand project bijwerken (PUT)
    public async Task<ProjectResponse?> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken cancelt = default)
    {
        var project = await _context.Projects.FindAsync(new object[] { id }, cancelt);
        if (project is null) return null;

        project.Titel = request.Titel;
        project.Beschrijving = request.Beschrijving;
        project.Categorie = request.Categorie;
        project.GitHubUrl = request.GitHubUrl ?? string.Empty;
        project.Datum = request.Datum;

        await _context.SaveChangesAsync(cancelt);

        return new ProjectResponse(
            project.Id,
            project.Titel,
            project.Beschrijving,
            project.Categorie,
            project.GitHubUrl,
            project.Datum);
    }

    // 5. Een project verwijderen (DELETE)
    public async Task<bool> DeleteAsync(int id, CancellationToken cancelt = default)
    {
        var project = await _context.Projects.FindAsync(new object[] { id }, cancelt);
        if (project is null) return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(cancelt);
        return true;
    }
}