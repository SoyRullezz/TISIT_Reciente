using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace CapaAccesoDatos
{
    public class CategoriasDAO
    {
        #region "Patron Singleton"

        private static CategoriasDAO daoCategoria = null;
        private CategoriasDAO() { }

        public static CategoriasDAO getInstance()
        {
            if (daoCategoria == null)
            {
                daoCategoria = new CategoriasDAO();
            }
            return daoCategoria;
        }


        #endregion

        public bool RegistrarCategoria(Categorias objCategoria)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            bool response = false;
            try
            {
                con = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO Categorias ([Nombre_Categoria], [Mostrar_Orden]) VALUES ('" + objCategoria.Nombre_Categoria + "', '" + objCategoria.Mostrar_Orden + "')";
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

        public List<Categorias> listarCategorias()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            List<Categorias> lista = new List<Categorias>();
            SqlDataReader dr = null;
            Categorias categoria;


            try
            {
                conn = Conexion.getInstance().ConexionBD();

                cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Categorias";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();

                dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    categoria = new Categorias();

                    categoria.Id = dr.GetInt32(0);
                    categoria.Nombre_Categoria = dr.GetString(1);
                    categoria.Mostrar_Orden = dr.GetInt32(2);

                    //Añadir objeto a la lista
                    lista.Add(categoria);

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

        public bool actualizarCategoria(Categorias categoria)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            bool response = false;
            try
            {
                con = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand();
                cmd.CommandText = "UPDATE Categorias SET [Nombre_Categoria] = '" + categoria.Nombre_Categoria + "', [Mostrar_Orden] = " + categoria.Mostrar_Orden + " WHERE Id=" + categoria.Id + "";
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

        public bool eliminarCategoria(int Id)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            bool response = false;
            try
            {
                con = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM Categorias WHERE Id=" + Id + "";
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
