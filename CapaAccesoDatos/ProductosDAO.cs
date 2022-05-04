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
    public class ProductosDAO
    {

        #region "Patron Singleton"

        private static ProductosDAO daoProductos = null;
        private ProductosDAO() { }

        public static ProductosDAO getInstance()
        {
            if (daoProductos == null)
            {
                daoProductos = new ProductosDAO();
            }
            return daoProductos;
        }


        #endregion


        public List<Productos> listarProductos()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            List<Productos> lista = new List<Productos>();
            SqlDataReader dr = null;
            Productos producto;


            try
            {
                conn = Conexion.getInstance().ConexionBD();

                cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Productos";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();

                dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    producto = new Productos();

                    producto.Id = dr.GetInt32(0);
                    producto.Nombre = dr.GetString(1);
                    producto.Descripcion = dr.GetString(2);
                    producto.Precio = dr.GetDecimal(3);
                    producto.Categoria = dr.GetString(4);
                    producto.ImgURL = dr.GetString(5);
                    producto.Cantidad = dr.GetDecimal(6);
                    producto.FechaEntrada = dr.GetString(7);
                    producto.FechaSalida = dr.GetString(8);
                    producto.Unidad = dr.GetString(9);
                    producto.Ubicacion = dr.GetString(10);


                    //Añadir objeto a la lista
                    lista.Add(producto);

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

        public Productos find(int Id)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            Productos producto = null;
            try
            {
                conn = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Productos WHERE Id = " + Id + "";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    producto = new Productos();

                    producto.Id = dr.GetInt32(0);
                    producto.Nombre = dr.GetString(1);
                    producto.Descripcion = dr.GetString(2);
                    producto.Precio = dr.GetDecimal(3);
                    producto.Categoria = dr.GetString(4);
                    producto.ImgURL = dr.GetString(5);
                    producto.Cantidad = dr.GetDecimal(6);
                    producto.FechaEntrada = dr.GetString(7);
                    producto.FechaSalida = dr.GetString(8);
                    producto.Unidad = dr.GetString(9);
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
            return producto;
        }
        public bool registrarProducto(Productos Producto)
        {


            //string extension = Path.GetExtension();


            SqlConnection con = null;
            SqlCommand cmd = null;
            bool response = false;
            try
            {
                con = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO Productos ([Id], [Nombre], [Descripcion], [Precio]," +
                    " [Categoria], [ImgURL], [Cantidad], [FechaEntrada], [FechaSalida], [Unidad], [Ubicacion])  VALUES (" + Producto.Id + ", '" + Producto.Nombre + "', '" + Producto.Descripcion + "', " + Producto.Precio + ", '" + Producto.Categoria + "', '" + Producto.ImgURL + "', " + Producto.Cantidad + ", '" + Producto.FechaEntrada + "', '" + Producto.FechaSalida + "', '" + Producto.Unidad + "', '" + Producto.Ubicacion + "')";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();

                int filas = cmd.ExecuteNonQuery();
                if (filas > 0) response = true;




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

        public bool actualizarProducto(Productos producto)
        {

            SqlConnection con = null;
            SqlCommand cmd = null;
            bool response = false;
            try
            {
                con = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand();
                cmd.CommandText = "UPDATE Productos SET [Nombre] = '" + producto.Nombre + "', [Descripcion] = '" + producto.Descripcion + "',[Precio] = " + producto.Precio + ",[Categoria] = '" + producto.Categoria + "', [Cantidad] = " + producto.Cantidad + ", [FechaSalida] = '" + producto.FechaSalida + "', [Unidad] = '" + producto.Unidad + "', [Ubicacion] = '" + producto.Ubicacion + "' WHERE [Id]=" + producto.Id + "";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();

                int filas = cmd.ExecuteNonQuery();
                if (filas > 0) response = true;
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
