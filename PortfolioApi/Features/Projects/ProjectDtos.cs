namespace PortfolioApi.Features.Projects;

using System.ComponentModel.DataAnnotations;

//hier de DTO voor het aanmaken van een project. (POST)
public class CreateProjectRequest
{
    [Required(ErrorMessage = "Titel is verplicht.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Titel moet tussen 2 en 100 tekens lang zijn.")]
    public string Titel { get; set; } = string.Empty;

    [Required(ErrorMessage = "Beschrijving is verplicht.")]
    public string Beschrijving { get; set; } = string.Empty;

    [Required(ErrorMessage = "Categorie is verplicht.")]
    public string Categorie { get; set; } = string.Empty;

    public string? GitHubUrl { get; set; }

    public DateTime Datum { get; set; } = DateTime.UtcNow;
}


// hier de DTO voor het bijjwerken van een project (PUT)
public class UpdateProjectRequest
{
    [Required(ErrorMessage = "Titel is verplicht.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Titel moet tussen 2 en 100 tekens lang zijn.")]
    public string Titel { get; set; } = string.Empty;

    [Required(ErrorMessage = "Beschrijving is verplicht.")]
    public string Beschrijving { get; set; } = string.Empty;

    [Required(ErrorMessage = "Categorie is verplicht.")]
    public string Categorie { get; set; } = string.Empty;

    public string? GitHubUrl { get; set; }

    public DateTime Datum { get; set; }
}

//hier een DTO voor response 
public record ProjectResponse(
    int Id, 
    string Titel, 
    string Beschrijving, 
    string Categorie, 
    string GitHubUrl, 
    DateTime Datum);
    