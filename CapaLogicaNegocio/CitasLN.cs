using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using CapaEntidades;

namespace CapaLogicaNegocio
{
    public class CitasLN
    {
        #region "Patron Singleton"
        private static CitasLN objCita = null;
        private CitasLN() { }
        public static CitasLN getInstance()
        {
            if (objCita == null)
            {
                objCita = new CitasLN();
            }
            return objCita;
        }
        #endregion

        public List<Citas> listarCitas()
        {
            List<Citas> lista = new List<Citas>();
            try
            {
                lista = CitasDAO.getInstance().listarCitas();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
