

namespace PortfolioApi.Features.Projects;

//hier een interface gemaakt klassen gebruiken de methode en in projectservice word de methode opgebouwd.
public interface IProjectService
{
    IEnumerable<ProjectResponse> GetAll();
    ProjectResponse? GetById(int id); 
}