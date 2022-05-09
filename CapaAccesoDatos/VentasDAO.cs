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
    public class VentasDAO
    {
        #region "Patron Singleton"

        private static VentasDAO daoVentas = null;
        private VentasDAO() { }

        public static VentasDAO getInstance()
        {
            if (daoVentas == null)
            {
                daoVentas = new VentasDAO();
            }
            return daoVentas;
        }


        #endregion

        public List<infoConsultaReportes> buscarVentaFecha(string FF, string FI, string tipo)
        {
            string Fecha_Final;
            if (FF == "")
            {
                Fecha_Final = "20170405";
            }
            else
            {
                Fecha_Final = Convert.ToDateTime(FF).ToString("yyyy-MM-dd").Replace("-", "");
            }


            string Fecha_Inicial = Convert.ToDateTime(FI).ToString("yyyy-MM-dd").Replace("-", "");


            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<infoConsultaReportes> ventasBusqueda = new List<infoConsultaReportes>();



            try
            {
                con = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand();
                cmd.CommandText = "consultaReporte";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FF", FF);
                cmd.Parameters.AddWithValue("@FI", FI);
                cmd.Parameters.AddWithValue("@tipo", tipo);

                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    infoConsultaReportes result = new infoConsultaReportes();


                    result.Fecha = dr.GetDateTime(0).ToString("yyyy-MM-dd");
                    result.Id_Producto = dr.GetInt32(1);
                    result.Nombre = dr.GetString(2);
                    result.Precio = dr.GetDecimal(3);
                    result.Cantidad = dr.GetInt32(4);
                    result.Subtotal = dr.GetDecimal(5);


                    ventasBusqueda.Add(result);

                }


            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                con.Close();

            }
            return ventasBusqueda;

        }

        public Ventas registrarVenta(Ventas Venta)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            SqlDataReader dr2 = null;

            Ventas nuevaVenta = null;

            try
            {
                con = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO ventas ([Total], [Fecha], [Hora]) VALUES (" + Venta.Total + ", '" + Venta.Fecha + "', '" + Venta.Hora + "')";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();

                int filas = cmd.ExecuteNonQuery();
                if (filas > 0)
                {
                    cmd.CommandText = "SELECT MAX(Id) AS id FROM ventas";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    dr2 = cmd.ExecuteReader();
                    dr2.Read();

                    cmd.CommandText = "SELECT * FROM ventas WHERE Id=" + dr2.GetInt32(0) + "";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    dr2.Close();

                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        nuevaVenta = new Ventas();

                        nuevaVenta.Id = dr.GetInt32(0);
                        nuevaVenta.Total = dr.GetDecimal(1);
                        nuevaVenta.Fecha = dr.GetDateTime(2).ToString("yyyy-MM-dd");
                        nuevaVenta.Hora = dr.GetTimeSpan(3).ToString();
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return nuevaVenta;
        }

    }
}
