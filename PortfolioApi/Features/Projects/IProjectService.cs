namespace PortfolioApi.Features.Projects;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponse>> GetAllAsync(CancellationToken ct = default);
    Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken ct = default);

    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct = default);

    Task<ProjectResponse?> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}