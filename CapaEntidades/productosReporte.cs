using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class productosReporte
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal { get; set; }

        public productosReporte() { }

        public productosReporte(int Id, string Nombre, int Cantidad, decimal Precio, decimal Subtotal)
        {
            this.Id = Id;
            this.Nombre = Nombre;
            this.Cantidad = Cantidad;
            this.Precio = Precio;
            this.Subtotal = Subtotal;
        }

    }
}
