using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;

namespace PortfolioApi.Features.Projects;

//hier heb ik dus de interface geimplementeerd
public class ProjectService : IProjectService
{

    // ASP.NEt geeft hier automatisch AppDBContext mee via injection, heb ik geregistreerd in program.cs, 
    // nu hoef ik zelf geen new AppDbContext() te gebruiken.
    //readonly zorgt ervoor dat de veld na de constructor niet opnieuw kan meegegevn.
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }



    // 1. hier heb ik een centrale plek gemaakt voor de mapping. wil ik iets erbij doen kan dat hier. zet het om in een DTO
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
        var projects = await _context.Projects  //is de DbSet dus de tabel Project 
            .AsNoTracking() // heb deze erbij gezet zorgt ervoor dat EF Core de objecten niet hoeft te volgen op wijzigingen.
            .ToListAsync(ct); // hier voert het pas de query uit. hier gaat de EF naar de database


        return projects.Select(ToResponse); // zorgt ervoor dat elk project naar een projectresponse veradnerd.
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