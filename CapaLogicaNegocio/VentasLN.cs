using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using CapaEntidades;

namespace CapaLogicaNegocio
{
    public class VentasLN
    {
        #region "Patron Singleton"
        private static VentasLN objVenta = null;
        private VentasLN() { }
        public static VentasLN getInstance()
        {
            if (objVenta == null)
            {
                objVenta = new VentasLN();
            }
            return objVenta;
        }
        #endregion

        public List<infoConsultaReportes> buscarVentaFecha(string FF, string FI, string tipo)
        {
            try
            {
                return VentasDAO.getInstance().buscarVentaFecha(FF, FI, tipo);
            }
            catch (Exception ex)
            { throw ex; }


        }

        public Ventas registrarVenta(Ventas Venta)
        {
            try
            {
                return VentasDAO.getInstance().registrarVenta(Venta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
