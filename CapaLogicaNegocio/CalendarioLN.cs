using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using CapaEntidades;

namespace CapaLogicaNegocio
{
    public class CalendarioLN
    {
        #region "Patron Singleton"
        private static CalendarioLN objCalendario = null;
        private CalendarioLN() { }
        public static CalendarioLN getInstance()
        {
            if (objCalendario == null)
            {
                objCalendario = new CalendarioLN();
            }
            return objCalendario;
        }
        #endregion


        public bool actualizarCita(Calendario calendario)
        {
            try
            {
                return CalendarioDAO.getInstance().actualizarCita(calendario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Calendario> listarCalendario()
        {
            List<Calendario> lista = new List<Calendario>();
            try
            {
                lista = CalendarioDAO.getInstance().listarCalendario();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
