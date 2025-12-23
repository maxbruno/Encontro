using System.ComponentModel.DataAnnotations;
using Encontro.Domain.Interfaces;

namespace Encontro.Domain.Entities;

public class Team : ISoftDeletable
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

    // Soft Delete
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
