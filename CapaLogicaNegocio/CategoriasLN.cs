using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CategoriasLN
    {
        #region "Patron Singleton"
        private static CategoriasLN objCategoria = null;
        private CategoriasLN() { }
        public static CategoriasLN getInstance()
        {
            if (objCategoria == null)
            {
                objCategoria = new CategoriasLN();
            }
            return objCategoria;
        }
        #endregion

        public bool RegistrarCategoria(Categorias objCategoria)
        {
            try
            {
                return CategoriasDAO.getInstance().RegistrarCategoria(objCategoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Categorias> listarCategorias()
        {
            List<Categorias> lista = new List<Categorias>();
            try
            {
                lista = CategoriasDAO.getInstance().listarCategorias();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool actualizarCategoria(Categorias categoria)
        {
            try
            {
                return CategoriasDAO.getInstance().actualizarCategoria(categoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool eliminarCategoria(int Id)
        {
            try
            {
                return CategoriasDAO.getInstance().eliminarCategoria(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
