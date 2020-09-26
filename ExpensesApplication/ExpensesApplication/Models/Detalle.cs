using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ExpensesApplication.Controllers
{
    public class Detalle
    {
        public long Id { get; set; }
        public long EmpleadoID { get; set; }
        public string Articulo { get; set; }
        public string AreaDestinada { get; set; }
        public string FechaCompra { get; set; }
        public double PrecioUnidad { get; set; }
        public double Total { get; set; }

    }
}
