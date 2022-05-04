using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using CapaEntidades;

namespace CapaLogicaNegocio
{
    public class IngresoLN
    {
        #region "Patron Singleton"
        private static IngresoLN objIngreso = null;
        private IngresoLN() { }
        public static IngresoLN getInstance()
        {
            if (objIngreso == null)
            {
                objIngreso = new IngresoLN();
            }
            return objIngreso;
        }
        #endregion

        public List<Ingresos> listarIngresos()
        {
            List<Ingresos> lista1 = new List<Ingresos>();
            try
            {
                lista1 = IngresoDAO.getInstance().listarIngresos();
                return lista1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
