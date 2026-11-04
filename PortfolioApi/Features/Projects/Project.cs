using System.ComponentModel.DataAnnotations;
namespace PortfolioApi.Features.Projects;

public class Project
{
    public int Id {get; set; }

    [Required(ErrorMessage = "Titel is verplicht.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Titel moet tussen 2 en 100 tekens lang zijn.")]
    public string Titel { get; set; } = string.Empty; 

    public string Beschrijving { get; set; } = string.Empty; 

    [Required(ErrorMessage = "Categorie is verplicht.")]
    public string Categorie { get; set; } = string.Empty;

    public string GitHubUrl { get; set; } = string.Empty;

    public DateTime Datum { get; set; }
}