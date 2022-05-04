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
    public class IngresoDAO
    {
        #region "Patron Singleton"

        private static IngresoDAO daoIngresos = null;
        private IngresoDAO() { }

        public static IngresoDAO getInstance()
        {
            if (daoIngresos == null)
            {
                daoIngresos = new IngresoDAO();
            }
            return daoIngresos;
        }


        #endregion


        public List<Ingresos> listarIngresos()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            List<Ingresos> lista1 = new List<Ingresos>();
            SqlDataReader dr = null;
            Ingresos ingreso;


            try
            {
                conn = Conexion.getInstance().ConexionBD();

                cmd = new SqlCommand();
                cmd.CommandText = "SELECT id_ingreso,paciente,fecha,edad,sexo,medico,diagnostico FROM ingresos_sl";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();

                dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    ingreso = new Ingresos();

                    ingreso.id_ingreso = dr.GetInt32(0);
                    ingreso.paciente = dr.GetString(1);
                    ingreso.fecha = dr.GetString(2);
                    ingreso.edad = dr.GetInt32(3);
                    ingreso.sexo = dr.GetString(4);
                    ingreso.medico = dr.GetString(5);
                    ingreso.diagnostico = dr.GetString(6);

                    //Añadir objeto a la lista
                    lista1.Add(ingreso);

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


            return lista1;
        }
    }
}
