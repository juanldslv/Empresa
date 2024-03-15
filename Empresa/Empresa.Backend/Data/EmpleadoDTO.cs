using Empresa.Shared.Entities;

namespace Empresa.Backend.Data
{
    public class EmpleadoDTO
    {
        public Empleado Empleado { get; set; } = null!;
        public IFormFile File { get; set; } = null!;
    }
}
