using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Features.Blogposts;

// Wat de API teruggeeft aan de buitenwereld
public record BlogpostResponse(
    int Id,
    string Titel,
    string Inhoud,
    string Auteur,
    DateTime Datum
);

//  hier staat wat de gebruiker dus meestuurt bij het aanmaken (POST)
public class CreateBlogpostRequest
{
    [Required(ErrorMessage = "Titel is verplicht.")]
    [MaxLength(150)]
    public string Titel { get; set; } = string.Empty;

    [Required(ErrorMessage = "Inhoud is verplicht.")]
    public string Inhoud { get; set; } = string.Empty;


    public string Auteur { get; set; } = string.Empty;

    public DateTime Datum { get; set; } = DateTime.UtcNow;
}

//  hier staat wat de gebruiker meestuurt bij het bijwerken (PUTT)
public class UpdateBlogpostRequest
{
    [Required(ErrorMessage = "Titel is verplicht.")]
    [MaxLength(150)]
    public string Titel { get; set; } = string.Empty;

    [Required(ErrorMessage = "Inhoud is verplicht.")]
    public string Inhoud { get; set; } = string.Empty;


    public string Auteur { get; set; } = string.Empty;

    public DateTime Datum { get; set; } = DateTime.UtcNow;
}