using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Calendario
    {

        public int id_cita { get; set; }

        public int id_cliente { get; set; }

        public int id_doctor { get; set; }

        public string nombreDoctor { get; set; }

        public string apellidoDoctor { get; set; }

        public string maternoDoctor { get; set; }

        public string nombreCliente { get; set; }

        public string apellidoCliente { get; set; }

        public string apellidoMCliente { get; set; }

        public int edad { get; set; }

        public string correo { get; set; }

        public long telefono { get; set; }

        public string fechaCita { get; set; }

        public string hora { get; set; }

        public string tipo_cita { get; set; }

        public string motivo { get; set; }

        public string mensaje { get; set; }

        public string status { get; set; }


        public Calendario() { }

        public Calendario(int id_cita, int id_cliente, int id_doctor, string nombreDoctor, string apellidoDoctor, string maternoDoctor, string nombreCliente, string apellidoCliente, string apellidoMCliente, int edad, string correo, int telefono, string fechaCita, string hora, string tipo_cita, string motivo, string mensaje, string status)
        {
            this.id_cita = id_cita;
            this.id_cliente = id_cliente;

            this.id_doctor = id_doctor;
            this.nombreDoctor = nombreDoctor;
            this.apellidoDoctor = apellidoDoctor;
            this.maternoDoctor = maternoDoctor;
            this.nombreCliente = nombreCliente;
            this.apellidoCliente = apellidoCliente;
            this.apellidoMCliente = apellidoMCliente;
            this.edad = edad;
            this.correo = correo;
            this.telefono = telefono;
            this.fechaCita = fechaCita;
            this.hora = hora;
            this.tipo_cita = tipo_cita;
            this.motivo = motivo;
            this.mensaje = mensaje;
            this.status = status;

        }
    }
}
