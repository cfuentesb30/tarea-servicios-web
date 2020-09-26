using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesApplication.Models
{
    public class Empleado
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string FechaNacimiento { get; set; }
    }
}
