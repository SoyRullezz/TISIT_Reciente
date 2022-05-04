using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Ingresos
    {
        public int id_ingreso { get; set; }

        public string paciente { get; set; }

        public string fecha { get; set; }

        public int edad { get; set; }

        public string sexo { get; set; }

        public string medico { get; set; }

        public string diagnostico { get; set; }

        public Ingresos() { }

        public Ingresos(int id_ingreso, string paciente, string fecha, int edad, string sexo,string medico, string diagnostico)
        {
            this.id_ingreso = id_ingreso;
            this.paciente = paciente;
            this.fecha = fecha;
            this.edad = edad;
            this.sexo = sexo;
            this.medico = medico;
            this.diagnostico = diagnostico;
        }
    }
}
