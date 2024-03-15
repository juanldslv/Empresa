using System.ComponentModel.DataAnnotations;

namespace Empresa.Shared.Entities
{
    public class Cargo
    {
        [Key]
        public int Id_Cargo { get; set; }

        [Required(ErrorMessage = "El Campo {0} es requerido.")]
        [MaxLength(255)]
        public string Nombre { get; set; } = null!;
    }
}