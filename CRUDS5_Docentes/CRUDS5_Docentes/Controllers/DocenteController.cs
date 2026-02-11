using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDS5_Docentes.Models;

namespace CRUDS5_Docentes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocenteController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public DocenteController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Docente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocenteModel>>> GetCliente()
        {
            return await _context.Cliente.ToListAsync();
        }

        // GET: api/Docente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocenteModel>> GetDocenteModel(int id)
        {
            var docenteModel = await _context.Cliente.FindAsync(id);

            if (docenteModel == null)
            {
                return NotFound();
            }

            return docenteModel;
        }

        // PUT: api/Docente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocenteModel(int id, DocenteModel docenteModel)
        {
            if (id != docenteModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(docenteModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocenteModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Docente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocenteModel>> PostDocenteModel(DocenteModel docenteModel)
        {
            _context.Cliente.Add(docenteModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocenteModel", new { id = docenteModel.Id }, docenteModel);
        }

        // DELETE: api/Docente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocenteModel(int id)
        {
            var docenteModel = await _context.Cliente.FindAsync(id);
            if (docenteModel == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(docenteModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocenteModelExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
