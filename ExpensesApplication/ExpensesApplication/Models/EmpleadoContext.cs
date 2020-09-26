using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApplication.Models
{
    public class EmpleadoContext : DbContext
    {
        public EmpleadoContext(DbContextOptions<EmpleadoContext> options)
         : base (options) //The base keyword is used to access the base class from within a derived class
        {


        }

        public DbSet<Empleado> Empleados { get; set; } //Empleado es la tabla para la base de datos
    }
}
