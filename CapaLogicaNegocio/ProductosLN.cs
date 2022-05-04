using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using CapaEntidades;

namespace CapaLogicaNegocio
{
    public class ProductosLN

    {
        #region "Patron Singleton"
        private static ProductosLN objProducto = null;
        private ProductosLN() { }
        public static ProductosLN getInstance()
        {
            if (objProducto == null)
            {
                objProducto = new ProductosLN();
            }
            return objProducto;
        }
        #endregion
        public bool registrarProducto(Productos Producto)
        {
            try
            {
                return ProductosDAO.getInstance().registrarProducto(Producto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Productos find(int Id)
        {
            try
            {
                return ProductosDAO.getInstance().find(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Productos> listarProductos()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                lista = ProductosDAO.getInstance().listarProductos();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool actualizarProducto(Productos producto)
        {
            try
            {
                return ProductosDAO.getInstance().actualizarProducto(producto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
