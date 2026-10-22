using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Features.Blogposts;

public class Blogpost
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Titel is verplicht.")]
    [MaxLength(150)]
    public string Titel { get; set; } = string.Empty;

    [Required(ErrorMessage = "Inhoud is verplicht.")]
    public string Inhoud { get; set; } = string.Empty;

    public string Auteur { get; set; } = string.Empty;

    public DateTime Datum { get; set; } = DateTime.UtcNow;
}