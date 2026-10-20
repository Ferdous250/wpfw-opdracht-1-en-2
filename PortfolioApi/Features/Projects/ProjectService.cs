//hier nog testdata gemaakt morgen verder met en dan echte data erjn zetten

namespace PortfolioApi.Features.Projects;

public class ProjectService : IProjectService
{
    public IEnumerable<ProjectResponse> GetAll()
    {
        return new List<ProjectResponse>
        {
            new(1, "Avond4daagse", "Een applicatie voor de Avond4daagse", "Applicatie", "https://github.com/Ferdous250", DateTime.Now)
        };
    }

    public ProjectResponse? GetById(int id)
    {
        if (id == 1)
        {
            return new ProjectResponse(1, "Avond4daagse", "Een applicatie voor de Avond4daagse", "Applicatie", "https://github.com/Ferdous250", DateTime.Now);
        }
        return null;
    }
}