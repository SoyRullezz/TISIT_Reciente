using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Detalle_Ventas
    {
        public int Id { get; set; }
        public int Id_Venta { get; set; }
        public decimal Precio_Unitario { get; set; }
        public int Cantidad { get; set; }
        public int Id_Producto { get; set; }
        public decimal Subtotal { get; set; }

        public Detalle_Ventas() { }

        public Detalle_Ventas(int Id, int Id_Venta, decimal Precio_Unitario, int Cantidad, int Id_Producto, decimal Subtotal)
        {
            this.Id = Id;
            this.Id_Venta = Id_Venta;
            this.Precio_Unitario = Precio_Unitario;
            this.Cantidad = Cantidad;
            this.Id_Producto = Id_Producto;
            this.Subtotal = Subtotal;
        }
    }
}
