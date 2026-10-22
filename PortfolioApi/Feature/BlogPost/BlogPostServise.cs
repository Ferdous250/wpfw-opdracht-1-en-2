using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;

namespace PortfolioApi.Features.Blogposts;

public class BlogpostService : IBlogpostService
{
    private readonly AppDbContext _context;

    public BlogpostService(AppDbContext context)
    {
        _context = context;
    }

    // 1. hier een centrale plek gemaakt voor de mapping (hoort bij Criterium 7)
    private static BlogpostResponse ToResponse(Blogpost b) => new(
        b.Id,
        b.Titel,
        b.Inhoud,
        b.Auteur,
        b.Datum
    );

    // 2. Alle blogposts opvragen (Async + AsNoTracking dit zorgt voor beteree performance)
    public async Task<IEnumerable<BlogpostResponse>> GetAllAsync(CancellationToken ct = default)
    {
        var blogposts = await _context.Blogposts
            .AsNoTracking()
            .ToListAsync(ct);

        return blogposts.Select(ToResponse);
    }

    // Eén blogpost opvragen op basis van ID (Async)
    public async Task<BlogpostResponse?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var blogpost = await _context.Blogposts.FindAsync(new object[] { id }, ct);

        return blogpost is null ? null : ToResponse(blogpost);
    }

    

    // Een nieuwe blogpost aanmaken (POST)
    public async Task<BlogpostResponse> CreateAsync(CreateBlogpostRequest request, CancellationToken ct = default)
    {
        var blogpost = new Blogpost
        {
            Titel = request.Titel,
            Inhoud = request.Inhoud,
            Auteur = request.Auteur,
            Datum = request.Datum
        };

        _context.Blogposts.Add(blogpost);
        await _context.SaveChangesAsync(ct);

        return ToResponse(blogpost);
    }



    // Een bestaande blogpost bijwerken (PUT)
    public async Task<BlogpostResponse?> UpdateAsync(int id, UpdateBlogpostRequest request, CancellationToken ct = default)
    {
        var blogpost = await _context.Blogposts.FindAsync(new object[] { id }, ct);
        if (blogpost is null) return null;

        blogpost.Titel = request.Titel;
        blogpost.Inhoud = request.Inhoud;
        blogpost.Auteur = request.Auteur;
        blogpost.Datum = request.Datum;

        await _context.SaveChangesAsync(ct);

        return ToResponse(blogpost);
    }



    // Een blogpost verwijderen (DELETE)
    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var blogpost = await _context.Blogposts.FindAsync(new object[] { id }, ct);
        if (blogpost is null) return false;

        _context.Blogposts.Remove(blogpost);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}