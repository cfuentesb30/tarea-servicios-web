using ExpensesApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ExpensesApplication.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class DetalleController : ControllerBase
    {
        private readonly DetalleContext _context;

        public DetalleController(DetalleContext context)
        {
            _context = context;

            if (_context.Detalles.Count() == 0)
            {
                _context.Detalles.Add(new Detalle {
                    EmpleadoID= 1,
                    Articulo="Papel bond", 
                    AreaDestinada="Administracion", 
                    FechaCompra= "10-10-11", 
                    PrecioUnidad= 80.99, 
                    Total= 160.65 });

                _context.SaveChanges();
            }
        }

        //GET:api/Detalles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalle>>> GetDetalles() 
        {
            return await _context.Detalles.ToListAsync(); 
        }


        //GET: api/Detalles/1
        [HttpGet("{id}")] 
        public async Task<ActionResult<Detalle>> GetDetalle(long id)
        {
            var detalle = await _context.Detalles.FirstOrDefaultAsync(q => q.Id == id);

            if (detalle == null)
            {
                return NotFound();
            }

            return detalle;
        }


        //POST: api/Detalle
        [HttpPost]
        public async Task<ActionResult<Detalle>> PostDetalle(Detalle item)
        {
            _context.Detalles.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalle), new { id = item.Id }, item); 
        }

        //PUT: api/Detalle/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalle(long id,Detalle item) 
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: api/Detalle/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalle(long id)
        {
            var detalle = await _context.Detalles.FindAsync(id);

            if (detalle == null)
            {
                return NotFound();
            }

            _context.Detalles.Remove(detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("[action]/{FechaCompra}")]
        public async Task<ActionResult<Detalle>> GetByDate(string fechaCompra)
        {
            var detalle = await _context.Detalles.FirstOrDefaultAsync(q => q.FechaCompra == fechaCompra);

            if (detalle == null)
            {
                return NotFound();
            }

            return detalle;
        }

        [HttpGet]
        [Route("[action]/{EmpleadoID}")]
        public async Task<ActionResult<Detalle>> GetByEID(long empleadoID)
        {
            var detalle = await _context.Detalles.FirstOrDefaultAsync(q => q.EmpleadoID == empleadoID);

            if (detalle == null)
            {
                return NotFound();
            }

            return detalle;
        }

    }
}
