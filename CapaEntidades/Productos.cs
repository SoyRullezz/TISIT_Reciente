using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Productos
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public string ImgURL { get; set; }

        public string Categoria { get; set; }

        public string FechaEntrada { get; set; }

        public string FechaSalida { get; set; }

        public decimal Cantidad { get; set; }

        public string Unidad { get; set; }
        public string Ubicacion { get; set; }



        public Productos() { }

        public Productos(int Id, string Nombre, string Descripcion, decimal Precio, string ImgURL, string Categoria, string FechaSalida, string FechaEntrada, decimal Cantidad, string Unidad, string Ubicacion)
        {
            this.Id = Id;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Precio = Precio;
            this.ImgURL = ImgURL;
            this.Categoria = Categoria;
            this.Unidad = Unidad;
            this.Cantidad = Cantidad;
            this.FechaSalida = FechaSalida;
            this.FechaEntrada = FechaEntrada;
            this.Ubicacion = Ubicacion;
        }
    }
}
