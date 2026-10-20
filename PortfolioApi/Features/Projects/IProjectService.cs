namespace PortfolioApi.Features.Projects;

public interface IProjectService
{
    IEnumerable<ProjectResponse> GetAll();
    ProjectResponse? GetById(int id); 
}