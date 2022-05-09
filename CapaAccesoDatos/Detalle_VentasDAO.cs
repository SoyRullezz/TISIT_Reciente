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
    public class Detalle_VentasDAO
    {
        #region "Patron Singleton"

        private static Detalle_VentasDAO daoDetalle_Ventas = null;
        private Detalle_VentasDAO() { }

        public static Detalle_VentasDAO getInstance()
        {
            if (daoDetalle_Ventas == null)
            {
                daoDetalle_Ventas = new Detalle_VentasDAO();
            }
            return daoDetalle_Ventas;
        }
        #endregion

        public bool registrarDetalle_Ventas(List<Detalle_Ventas> items)
        {
            string values = "";
            for (int i = 0; i < items.Count; i++)
            {
                if (i == items.Count - 1)
                {
                    values += "(" + items[i].Id_Venta + ", " + items[i].Precio_Unitario + ", " + items[i].Cantidad + ", " + items[i].Id_Producto + ", " + items[i].Subtotal + ")";

                }
                else
                {
                    values += "(" + items[i].Id_Venta + ", " + items[i].Precio_Unitario + ", " + items[i].Cantidad + ", " + items[i].Id_Producto + ", " + items[i].Subtotal + "), ";

                }
            }
            SqlConnection con = null;
            SqlCommand cmd = null;
            bool response = false;
            try
            {
                con = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO detalle_ventas ([Id_Venta], [Precio_Unitario], [Cantidad], [Id_Producto], [Subtotal]) VALUES " + values;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();

                int filas = cmd.ExecuteNonQuery();

                if (filas > 0) { response = true; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return response;
        }
    }
}
