using System.ComponentModel.DataAnnotations;
using Encontro.Domain.Interfaces;

namespace Encontro.Domain.Entities;

public class EventParticipant : ISoftDeletable
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O evento é obrigatório")]
    [Display(Name = "Evento")]
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    [Required(ErrorMessage = "A pessoa é obrigatória")]
    [Display(Name = "Pessoa")]
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;

    [Display(Name = "Equipe")]
    public int? TeamId { get; set; }
    public Team? Team { get; set; }

    [Display(Name = "Função")]
    public int? RoleId { get; set; }
    public Role? Role { get; set; }

    [Display(Name = "Data de Inscrição")]
    public DateTime RegisteredAt { get; set; }

    [Required(ErrorMessage = "A etapa é obrigatória")]
    [Range(1, 10, ErrorMessage = "A etapa deve estar entre 1 e 10")]
    [Display(Name = "Etapa")]
    public int Stage { get; set; }

    [MaxLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres")]
    [Display(Name = "Observações")]
    public string? Notes { get; set; }

    [Display(Name = "Indicado")]
    public bool? Indicated { get; set; }

    [Display(Name = "Aceitou")]
    public bool? Accepted { get; set; }

    // Soft Delete
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
