using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class infoConsultaReportes
    {
        public string Fecha { get; set; }
        public int Id_Producto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }

        public infoConsultaReportes()
        {

        }
        public infoConsultaReportes(string Fecha, int Id_Producto, string Nombre, decimal Precio, int Cantidad, decimal Subtotal)
        {
            this.Fecha = Fecha;
            this.Id_Producto = Id_Producto;
            this.Nombre = Nombre;
            this.Precio = Precio;
            this.Cantidad = Cantidad;
            this.Subtotal = Subtotal;
        }

    }
}
