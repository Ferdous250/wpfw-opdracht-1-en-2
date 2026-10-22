namespace PortfolioApi.Features.Blogposts;

public class Blogpost
{
    public int Id { get; set; }

    public string Titel { get; set; } = string.Empty;

    public string Inhoud { get; set; } = string.Empty;

    public string Auteur { get; set; } = string.Empty;

    public DateTime Publicatiedatum  { get; set; } = DateTime.UtcNow;
}