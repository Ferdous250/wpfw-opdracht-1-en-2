using Microsoft.AspNetCore.Http.HttpResults;

namespace PortfolioApi.Features.Projects;

public static class ProjectEndpoints
{
    public static RouteGroupBuilder MapProjects(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/projects");

        group.MapGet("", GetAll);
        group.MapGet("/{id:int}", GetById).WithName("GetProject");

        return group;
    }

    private static Results<Ok<IEnumerable<ProjectResponse>>, NotFound> GetAll(IProjectService service)
    {
        var projects = service.GetAll();
        return TypedResults.Ok(projects);
    }

    private static Results<Ok<ProjectResponse>, NotFound> GetById(int id, IProjectService service)
    {
        var project = service.GetById(id);
        
        if (project is null)
        {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(project);
    }
}
        
      