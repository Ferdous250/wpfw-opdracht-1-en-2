namespace PortfolioApi.Features.Projects;

public record ProjectResponse(int Id, string Titel, string Beschrijving, string Categorie, string GitHubUrl, DateTime Datum);