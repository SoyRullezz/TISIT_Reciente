using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class citasReporte
    {
        public int Id_Cita { get; set; }
        public string Tipo_Cita { get; set; }
        public string Nombre_Doctor { get; set; }
        public string ApellidoP_Doctor { get; set; }
        public string ApellidoM_Doctor { get; set; }
        public string Nombre_Cliente { get; set; }
        public string ApellidoP_Cliente { get; set; }
        public string ApellidoM_Cliente { get; set; }
        public decimal Concepto { get; set; }

        public citasReporte() { }

        public citasReporte(int Id_Cita, string Nombre_Doctor, string ApellidoP_Doctor, string ApellidoM_Doctor, string Nombre_Cliente, string ApellidoP_Cliente, string ApellidoM_Cliente, decimal Concepto)
        {
            this.Id_Cita = Id_Cita;
            this.Nombre_Doctor = Nombre_Doctor;
            this.ApellidoP_Doctor = ApellidoP_Doctor;
            this.ApellidoM_Doctor = ApellidoM_Doctor;
            this.Nombre_Cliente = Nombre_Cliente;
            this.ApellidoP_Cliente = ApellidoP_Cliente;
            this.ApellidoM_Cliente = ApellidoM_Cliente;
            this.Concepto = Concepto;
        }

    }
}
