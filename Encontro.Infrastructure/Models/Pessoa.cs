using System.ComponentModel.DataAnnotations;

namespace Encontro.Infrastructure.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
