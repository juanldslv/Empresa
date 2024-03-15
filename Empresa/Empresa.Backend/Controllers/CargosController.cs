using Empresa.Backend.Data;
using Empresa.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargosController:ControllerBase
    {
        private readonly DataContext _context;
        public CargosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            return Ok(await _context.Cargos.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return Ok(cargo);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(Cargo cargo)
        {
            _context.Add(cargo);
            await _context.SaveChangesAsync();
            return Ok(cargo);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Cargo cargo)
        {
            _context.Update(cargo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            _context.Remove(cargo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
