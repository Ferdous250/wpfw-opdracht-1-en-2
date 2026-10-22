namespace PortfolioApi.Features.Projects;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponse>> GetAllAsync(CancellationToken cancelt = default);
    Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken cancelt = default);

    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken cancelt = default);

    Task<ProjectResponse?> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken cancelt = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancelt = default);
}