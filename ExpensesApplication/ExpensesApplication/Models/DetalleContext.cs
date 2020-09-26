using ExpensesApplication.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApplication.Models
{
    public class DetalleContext : DbContext
    {
        public DetalleContext(DbContextOptions<DetalleContext> options)
         : base (options) 
        {


        }

        public DbSet<Detalle> Detalles { get; set; }

    }
}
