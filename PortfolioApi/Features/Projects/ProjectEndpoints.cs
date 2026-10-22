
using Microsoft.AspNetCore.Http.HttpResults;
using PortfolioApi.Features.Projects;

namespace PortfolioApi.Features.Projects;

public static class ProjectEndpoints
{
    public static void MapProjects(this IEndpointRouteBuilder app)
    {
        // Hier maak ik een groep aan voor alle project endpoints
        var group = app.MapGroup("/api/projects").WithTags("Projects");

        // Hiermee kan ik alle projecten ophalen
        group.MapGet("", GetAll);


        // Hiermee kan ik een project ophalen met het id
        group.MapGet("{id:int}", GetById).WithName("GetProject");

        // Hiermee kan ik een nieuw project toevoegen
        group.MapPost("", Create);


        // Hiermee kan ik een bestaand project aanpassen
        group.MapPut("{id:int}", Update);
        // Hiermee kan ik een project verwijderen
        group.MapDelete("{id:int}", Delete);
    }



    // Deze methode haalt alle projecten op
    private static async Task<Ok<IEnumerable<ProjectResponse>>> GetAll(IProjectService service, CancellationToken ct)
    {
        var projects = await service.GetAllAsync(ct);

        // Ik stuur de projecten terug met statuscode 200 OK
        return TypedResults.Ok(projects);
    }



    // Deze methode haalt één project op aan de hand van het id
    private static async Task<Results<Ok<ProjectResponse>, NotFound>> GetById(int id, IProjectService service, CancellationToken ct)
    {
        var project = await service.GetByIdAsync(id, ct);

        // Als het project bestaat, stuur ik het terug.
        // Anders geef ik aan dat het project niet gevonden is
        return project is not null ? TypedResults.Ok(project) : TypedResults.NotFound();
    }


    // Deze methode maakt een nieuw project aan
    private static async Task<CreatedAtRoute<ProjectResponse>> Create(CreateProjectRequest request, IProjectService service, CancellationToken ct)
    {
        var response = await service.CreateAsync(request, ct);

        // Ik stuur het nieuwe project terug en verwijs naar de route om het op te halen
        return TypedResults.CreatedAtRoute(response, "GetProject", new { id = response.Id });
    }


    // Deze methode past een bestaand project aan
    private static async Task<Results<Ok<ProjectResponse>, NotFound>> Update(int id, UpdateProjectRequest request, IProjectService service, CancellationToken ct)
    {
        var response = await service.UpdateAsync(id, request, ct);

        // Als het aanpassen lukt, stuur ik het project terug.
        // Als het project niet bestaat, geef ik NotFound terug
        return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
    }
    

    // Deze methode verwijdert een project
    private static async Task<Results<NoContent, NotFound>> Delete(int id, IProjectService service, CancellationToken ct)
    {
        var success = await service.DeleteAsync(id, ct);

        // Als het verwijderen lukt, stuur ik NoContent terug.
        // Anders geef ik aan dat het project niet gevonden is
        return success ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}
