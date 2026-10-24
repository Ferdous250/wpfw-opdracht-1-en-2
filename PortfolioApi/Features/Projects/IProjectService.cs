namespace PortfolioApi.Features.Projects;

//hier een interface gemaakt waarbij ik alleen methodes heb gemaakt Projectservice krijgt dit.
public interface IProjectService
{
    Task<IEnumerable<ProjectResponse>> GetAllAsync(CancellationToken ct = default); //haalt alle projecten op
    Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken ct = default); //een project ophalen

    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct = default); //een project maken

    Task<ProjectResponse?> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken ct = default); //een project wijzigen 
    Task<bool> DeleteAsync(int id, CancellationToken ct = default); // en hier een project verwijderen
}