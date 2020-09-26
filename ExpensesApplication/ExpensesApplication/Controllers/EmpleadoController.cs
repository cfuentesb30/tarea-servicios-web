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
    [Route ("api/[controller]")]//Si ponemos [controller] no es necesario ponerlo en el buscador
    [ApiController]
    public class EmpleadoController : ControllerBase //Which provides general MVC handling. The Controller class inherits from ControllerBase and is the default implement of a controller
    {
        private readonly EmpleadoContext _context;

        public EmpleadoController(EmpleadoContext context)// EmpleadoContext es a base de datos inyectada por medio de dependencias. Context is responsible for interacting with the data of DbContext.
        {
            _context = context;

            if (_context.Empleados.Count() == 0)
            {
                //Create a new TodoItem if collection is empty
                //means you can't delete all TodoItems
                _context.Empleados.Add(new Empleado { 
                    Nombre = "Sam", 
                    Correo= "sdpineda@hotmail.com",
                    FechaNacimiento= "2-6-2020"});
                _context.SaveChanges();
            }

        }

        //GET:api/Empleado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados() // Task representa una operación asíncrona que retorna un valor. ActionResult<T> .NET Core serializa el objeto automaticamente a JSON.
        {
            return await _context.Empleados.ToListAsync(); //await y async usarlo siempre que consumas un recurso externo a tu código (un fichero, una base de datos, un servicio online...). Esto es para que la interfaz no se estanque mientras se hace la conexion.
        }


        //GET: api/Empleado/1
        [HttpGet("{id}")] // Esto permite que al ingresar en la URL el valor, este inmediatamente se traslada al parámetro del método.
        public async Task<ActionResult<Empleado>> GetEmpleado(long id) 
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(q => q.Id == id);
            //var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
            {
            return NotFound();
            }

            return empleado;
        }


        //POST: api/Alumno
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado item)
        {
            _context.Empleados.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmpleado), new { id = item.Id }, item); //pg.53 y 56 
        }

        //pg. 58
        //PUT: api/Empleado/2
        [HttpPut ("{id}")] //Es para modificar
        public async Task<IActionResult> PutEmpleado(long id, Empleado item) //Se usa comunmente para las respuestas que se reciben
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State= EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        //PUT: api/Empleado/2
        [HttpDelete("{id}")] //Es para modificar
        public async Task<IActionResult> DeleteEmpleado(long id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null )
            {
                return NotFound();
            }

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("[action]/{Nombre}")]
        //GET: api/Empleado/karen
        public async Task<ActionResult<Empleado>> GetByName(string nombre)
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(q => q.Nombre == nombre);
            //var empleado = await _context.Empleados.FindAsync(nombre);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }

        [HttpGet]
        [Route("[action]/{FechaNacimiento}")]
        public async Task<ActionResult<Empleado>> GetByBirthday(string fechaNacimiento)
        {
            var detalle = await _context.Empleados.FirstOrDefaultAsync(q => q.FechaNacimiento == fechaNacimiento);

            if (detalle == null)
            {
                return NotFound();
            }

            return detalle;
        }

    }
}
