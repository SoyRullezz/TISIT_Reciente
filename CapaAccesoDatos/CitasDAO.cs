using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using System.Data;
using System.Data.SqlClient;

namespace CapaAccesoDatos
{
    public class CitasDAO
    {
        #region "Patron Singleton"

        private static CitasDAO daoCitas = null;
        private CitasDAO() { }

        public static CitasDAO getInstance()
        {
            if (daoCitas == null)
            {
                daoCitas = new CitasDAO();
            }
            return daoCitas;
        }


        #endregion


        public List<Citas> listarCitas()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            List<Citas> lista = new List<Citas>();
            SqlDataReader dr = null;
            Citas cita;


            try
            {
                conn = Conexion.getInstance().ConexionBD();

                cmd = new SqlCommand();
                cmd.CommandText = "SELECT id_llegada,nombre_paciente,nombre_doctor,tipo_cita,fecha_ingreso,hora_ingreso FROM registro_pacientes_llegada where tipo_cita = 'OPERACION' AND ingresado = 0";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();

                dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    cita = new Citas();

                    cita.id_llegada = dr.GetInt32(0);
                    cita.nombre_paciente = dr.GetString(1);
                    cita.nombre_doctor = dr.GetString(2);
                    cita.tipo_cita = dr.GetString(3);
                    cita.fecha_ingreso = dr.GetString(4);
                    cita.hora_ingreso = dr.GetString(5);

                    //Añadir objeto a la lista
                    lista.Add(cita);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return lista;
        }
    }
}
