using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;

namespace CapaAccesoDatos
{
    public class CalendarioDAO
    {

        #region "Patron Singleton"

        private static CalendarioDAO daoCalendario = null;
        private CalendarioDAO() { }

        public static CalendarioDAO getInstance()
        {
            if (daoCalendario == null)
            {
                daoCalendario = new CalendarioDAO();
            }
            return daoCalendario;
        }


        #endregion

        public List<Calendario> listarCalendario()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            List<Calendario> lista = new List<Calendario>();
            SqlDataReader dr = null;
            Calendario calendario;


            try
            {
                conn = Conexion.getInstance().ConexionBD();

                cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM recurrentes";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();

                dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    calendario = new Calendario();

                    calendario.id_cita = dr.GetInt32(0);
                    calendario.id_cliente = dr.GetInt32(1);
                    calendario.id_doctor = dr.GetInt32(2);
                    calendario.nombreDoctor = dr.GetString(3);
                    calendario.apellidoDoctor = dr.GetString(4);
                    calendario.maternoDoctor = dr.GetString(5);
                    calendario.nombreCliente = dr.GetString(6);
                    calendario.apellidoCliente = dr.GetString(7);
                    calendario.apellidoMCliente = dr.GetString(8);
                    calendario.edad = dr.GetInt32(9);
                    calendario.correo = dr.GetString(10);
                    calendario.telefono = dr.GetInt64(11);
                    calendario.fechaCita = dr.GetDateTime(12).ToString("yyyy-MM-dd");
                    calendario.hora = dr.GetTimeSpan(13).ToString();
                    calendario.tipo_cita = dr.GetString(14);
                    calendario.motivo = dr.GetString(15);
                    calendario.mensaje = dr.GetString(16);
                    calendario.status = dr.GetString(17);

                    lista.Add(calendario);

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
