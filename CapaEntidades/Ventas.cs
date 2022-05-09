using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Ventas
    {
        public int Id { get; set; }

        public decimal Total { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }

        public Ventas() { }

        public Ventas(int Id, decimal Total, string Fecha, string Hora)
        {
            this.Id = Id;
            this.Total = Total;
            this.Fecha = Fecha;
            this.Hora = Hora;
        }
    }
}
