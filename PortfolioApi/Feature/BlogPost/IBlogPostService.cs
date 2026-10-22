namespace PortfolioApi.Features.Blogposts;

public interface IBlogpostService
{
    Task<IEnumerable<BlogpostResponse>> GetAllAsync(CancellationToken ct = default);

    Task<BlogpostResponse?> GetByIdAsync(int id, CancellationToken ct = default);

    Task<BlogpostResponse> CreateAsync(CreateBlogpostRequest request, CancellationToken ct = default);

    Task<BlogpostResponse?> UpdateAsync(int id, UpdateBlogpostRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}