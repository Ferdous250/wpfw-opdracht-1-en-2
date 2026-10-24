using Microsoft.AspNetCore.Http.HttpResults; 

namespace PortfolioApi.Features.Blogposts;

//deze klasse static gemaakt heeft alleen functies en maakt er nooit een object van. manier van extension methode.
public static class BlogpostEndpoints
{

    public static void MapBlogpostEndpoints(this IEndpointRouteBuilder app) 
    {
        //met deze regel zorgt het ervoor dat alles hieronder begint met /api/blogposts, zodat ik dat niet telkens hoef te doen.
        var groep = app.MapGroup("/api/blogposts").WithTags("Blogposts"); //.WithTags("Blogposts") zorgt dat swagger ui onder blogpost bij elkaar.

        // Alle blogposts ophalen
        groep.MapGet("", GetAll);

        // Één blogpost ophalen op basis van ID int voor alleen cijfers dus geen letters anders 404 terug.
        groep.MapGet("{id:int}", GetById).WithName("GetBlogpost");

        // Nieuwe blogpost aanmaken
        groep.MapPost("", Create);


        // Bestaande blogpost bijwerken
        groep.MapPut("{id:int}", Update);

        // Blogpost verwijderen
        groep.MapDelete("{id:int}", Delete);
    }

    private static async Task<Ok<IEnumerable<BlogpostResponse>>> GetAll(IBlogpostService service, CancellationToken ct)
    {
        var blogposts = await service.GetAllAsync(ct);
        return TypedResults.Ok(blogposts);
    }



    //heeft 2 mogelijkheden een 200 OK of 404 NotFound gegeven
    private static async Task<Results<Ok<BlogpostResponse>, NotFound>> GetById(int id, IBlogpostService service, CancellationToken ct)
    {
        var blogpost = await service.GetByIdAsync(id, ct);
        return blogpost is not null ? TypedResults.Ok(blogpost) : TypedResults.NotFound(); //geeft null terug als blogpost niet bestaat.
    }


    //slaat een nieuwe blogpost op en geeft hier een blogpostresponse terug, met een neiwue ID dat de databse heeft gemaakt.
    private static async Task<CreatedAtRoute<BlogpostResponse>> Create(CreateBlogpostRequest request, IBlogpostService service, CancellationToken ct)
    {
        var response = await service.CreateAsync(request, ct);
        
        // Verwijst naar de routenaam "GetBlogpost" hierboven
        return TypedResults.CreatedAtRoute(response, "GetBlogpost", new { id = response.Id }); //hier geef ik de drie argumenten
    }



    private static async Task<Results<Ok<BlogpostResponse>, NotFound>> Update(int id, UpdateBlogpostRequest request, IBlogpostService service, CancellationToken ct)
    {
        var response = await service.UpdateAsync(id, request, ct);
        return response is not null ? TypedResults.Ok(response) : TypedResults.NotFound();
    }



    private static async Task<Results<NoContent, NotFound>> Delete(int id, IBlogpostService service, CancellationToken ct)
    {
        var success = await service.DeleteAsync(id, ct);
        return success ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}