using System.ComponentModel.DataAnnotations;

namespace Encontro.Domain.Entities;

public class Team
{
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    [Display(Name = "Ordem")]
    public string Order { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [Display(Name = "Nome da Equipe")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Data de Criação")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Data de Atualização")]
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Person> People { get; set; } = new List<Person>();
}
