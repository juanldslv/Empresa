using System.ComponentModel.DataAnnotations;

namespace Empresa.Shared.Entities
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido.")]
        public int? Cedula { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido.")]
        [MaxLength(50)]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El Campo {0} es requerido.")]
        public string Foto { get; set; } = null!; // nullable string to allow for employees without a photo
        [Required(ErrorMessage = "El Campo {0} es requerido.")]
        public DateTime Fecha_Ingreso { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido.")]

        public int Id_Cargo { get; set; }

        
    }
}
