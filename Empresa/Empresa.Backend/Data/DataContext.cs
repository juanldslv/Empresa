using Empresa.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Backend.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
                
        }
        public DbSet<Cargo> Cargos { get; set; } = null!;
        public DbSet<Empleado> Empleados { get; set; } = null!;

    }
}
