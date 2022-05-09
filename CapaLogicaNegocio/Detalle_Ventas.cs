using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using CapaEntidades;

namespace CapaLogicaNegocio
{
    public class Detalle_VentasLN
    {
        #region "Patron Singleton"
        private static Detalle_VentasLN objDetalle_Venta = null;
        private Detalle_VentasLN() { }
        public static Detalle_VentasLN getInstance()
        {
            if (objDetalle_Venta == null)
            {
                objDetalle_Venta = new Detalle_VentasLN();
            }
            return objDetalle_Venta;
        }
        #endregion




        public bool registrarVenta(List<Detalle_Ventas> items)
        {
            try
            {
                return Detalle_VentasDAO.getInstance().registrarDetalle_Ventas(items);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
