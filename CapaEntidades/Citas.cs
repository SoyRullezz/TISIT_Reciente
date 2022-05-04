using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Citas
    {
        public int id_llegada { get; set; }

        public string nombre_paciente { get; set; }

        public string nombre_doctor { get; set; }

        public string tipo_cita { get; set; }

        public string fecha_ingreso { get; set; }

        public string hora_ingreso { get; set; }


        public Citas() { }

        public Citas(int id_llegada, string nombre_paciente, string nombre_doctor, string tipo_cita, string fecha_ingreso, string hora_ingreso)
        {
            this.id_llegada = id_llegada;
            this.nombre_paciente = nombre_paciente;
            this.nombre_doctor = nombre_doctor;
            this.tipo_cita = tipo_cita;
            this.fecha_ingreso = fecha_ingreso;
            this.hora_ingreso = hora_ingreso;
        }
    }
}
