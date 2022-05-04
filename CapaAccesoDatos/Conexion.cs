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
    class Conexion
    {
        #region "PATRON SINGLETON"
        private static Conexion conexion = null;
        private Conexion() { }
        public static Conexion getInstance()
        {
            if (conexion == null)
            {
                conexion = new Conexion();

            }
            return conexion;
        }
        #endregion

        //Con esto obtenemos la ceda de conexion
        public SqlConnection ConexionBD()
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = "Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123";

            return conexion;
        }
    }
}
