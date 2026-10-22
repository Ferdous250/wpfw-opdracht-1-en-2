using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioApi.Features.Blogposts;

public static class BlogpostEndpoints
{
    public static void MapBlogpostEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/blogposts").WithTags("Blogposts");

        // Alle blogposts ophalen
        group.MapGet("", GetAll);

        // Één blogpost ophalen op basis van ID (met routenaam voor CreatedAtRoute)
        group.MapGet("{id:int}", GetById).WithName("GetBlogpost");

        // Nieuwe blogpost aanmaken
        group.MapPost("", Create);


        // Bestaande blogpost bijwerken
        group.MapPut("{id:int}", Update);

        // Blogpost verwijderen
        group.MapDelete("{id:int}", Delete);
    }

    private static async Task<Ok<IEnumerable<BlogpostResponse>>> GetAll(IBlogpostService service, CancellationToken ct)
    {
        var blogposts = await service.GetAllAsync(ct);
        return TypedResults.Ok(blogposts);
    }



    private static async Task<Results<Ok<BlogpostResponse>, NotFound>> GetById(int id, IBlogpostService service, CancellationToken ct)
    {
        var blogpost = await service.GetByIdAsync(id, ct);
        return blogpost is not null ? TypedResults.Ok(blogpost) : TypedResults.NotFound();
    }



    private static async Task<CreatedAtRoute<BlogpostResponse>> Create(CreateBlogpostRequest request, IBlogpostService service, CancellationToken ct)
    {
        var response = await service.CreateAsync(request, ct);
        
        // Verwijst naar de routenaam "GetBlogpost" hierboven
        return TypedResults.CreatedAtRoute(response, "GetBlogpost", new { id = response.Id });
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