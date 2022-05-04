using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Nombre_Categoria { get; set; }

        public int Mostrar_Orden { get; set; }

        public Categorias() { }

        public Categorias(int id, string nombre_Categoria, int mostrar_Orden)
        {
            this.Id = id;
            this.Nombre_Categoria = nombre_Categoria;
            this.Mostrar_Orden = mostrar_Orden;
        }
    }
}
