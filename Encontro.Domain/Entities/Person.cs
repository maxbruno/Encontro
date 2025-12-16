using System.ComponentModel.DataAnnotations;

namespace Encontro.Domain.Entities;

public class Person
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres")]
    [Display(Name = "Nome")]
    public string Name { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "O tipo deve ter no máximo 50 caracteres")]
    [Display(Name = "Tipo")]
    public string? Type { get; set; }

    [Display(Name = "Data de Nascimento")]
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [StringLength(20, ErrorMessage = "O celular deve ter no máximo 20 caracteres")]
    [Phone(ErrorMessage = "Formato de celular inválido")]
    [Display(Name = "Celular")]
    public string? CellPhone { get; set; }

    [StringLength(150, ErrorMessage = "O email deve ter no máximo 150 caracteres")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres")]
    [Display(Name = "Endereço")]
    public string? Address { get; set; }

    [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres")]
    [Phone(ErrorMessage = "Formato de telefone inválido")]
    [Display(Name = "Telefone")]
    public string? Phone { get; set; }

    [StringLength(100, ErrorMessage = "O bairro deve ter no máximo 100 caracteres")]
    [Display(Name = "Bairro")]
    public string? District { get; set; }

    [StringLength(10, ErrorMessage = "O CEP deve ter no máximo 10 caracteres")]
    [Display(Name = "CEP")]
    public string? ZipCode { get; set; }

    [StringLength(100, ErrorMessage = "O grupo deve ter no máximo 100 caracteres")]
    [Display(Name = "Grupo")]
    public string? Group { get; set; }

    [StringLength(150, ErrorMessage = "O nome do pai deve ter no máximo 150 caracteres")]
    [Display(Name = "Nome do Pai")]
    public string? FatherName { get; set; }

    [StringLength(150, ErrorMessage = "O nome da mãe deve ter no máximo 150 caracteres")]
    [Display(Name = "Nome da Mãe")]
    public string? MotherName { get; set; }

    [StringLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres")]
    [Display(Name = "Observações")]
    [DataType(DataType.MultilineText)]
    public string? Notes { get; set; }

    [StringLength(255, ErrorMessage = "O caminho da foto deve ter no máximo 255 caracteres")]
    [Display(Name = "Foto")]
    public string? PhotoUrl { get; set; }
}
