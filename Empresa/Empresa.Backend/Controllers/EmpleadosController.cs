using Empresa.Backend.Data;
using Empresa.Backend.Helpers;
using Empresa.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly DataContext _context;
        
        private readonly IConfiguration _configuration;
        
        FileStorage fileStorage=null!;


        public EmpleadosController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok(await _context.Empleados.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            fileStorage = new(empleado.Foto);
            if (!fileStorage.ExisteImagen() && empleado==null)
            {

                return NotFound();
            }
            
            return Ok(empleado);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostAsync([FromBody] EmpleadoDTO empleadoDTO)
        {
            try
            {
                 fileStorage = new(_configuration.GetValue<string>("PathI"));
                string PathI= fileStorage.CrearCarpetaAleatoria();
                fileStorage = new(PathI);
                PathI=fileStorage.GuardarImagen(empleadoDTO.File.FileName, await FileStorage.ConvertIFormFileToBytesAsync(empleadoDTO.File));
                Empleado emplea = empleadoDTO.Empleado!;
                emplea.Foto = PathI;
                _context.Add(emplea);
                await _context.SaveChangesAsync();
                return Ok(emplea);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
            
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync(Empleado empleado)
        {
            _context.Update(empleado);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            _context.Remove(empleado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
