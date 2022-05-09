using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class itemReporteVenta
    {
        public string Fecha { get; set; }
        public List<productosReporte> Productos { get; set; }

        public decimal Total { get; set; }

        public itemReporteVenta() { }

        public itemReporteVenta(string Fecha, List<productosReporte> Productos, decimal Total)
        {
            this.Fecha = Fecha;
            this.Productos = Productos;
            this.Total = Total;
        }


    }
}
