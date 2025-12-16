using System.ComponentModel.DataAnnotations;

namespace Encontro.Domain.Entities;

public enum EventType
{
    [Display(Name = "Segue-me")]
    SeguiMe = 1,
    
    [Display(Name = "ECC")]
    ECC = 2
}

public class Event
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    [Display(Name = "Nome")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo de evento é obrigatório")]
    [Display(Name = "Tipo de Evento")]
    public EventType EventType { get; set; }

    [Display(Name = "Data de Criação")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Data de Atualização")]
    public DateTime UpdatedAt { get; set; }
}
