using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;
using CapaLogicaNegocio;
using System.IO;
using BarcodeLib;
using System.Drawing;
using Image = System.Drawing.Image;

using iText;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.IO.Font;
using System.Configuration;
using System.Data.SqlClient;

namespace TISIT
{

    public partial class ventas : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        public static string fileName;

        protected void Page_Load(object sender, EventArgs e)

        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);


            if (!IsPostBack)
            {



                VerificarProductos();
            }

            //permisosGenerales();
            //perm();

        }

        protected void VerificarProductos()
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("d"));
            System.Diagnostics.Debug.WriteLine(DateTime.Now.AddMonths(6).ToString("d"));
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "SELECT * from Productos";
                SqlCommand cmdm = new SqlCommand(query, con);
                SqlDataReader reg = cmdm.ExecuteReader();

                DateTime hoy = DateTime.Now;

                List<string> pruductosAVencer = new List<string>();
                string mensaje = "";
                string lee_mensaje = "";
                while (reg.Read())
                {
                    DateTime date_1 = Convert.ToDateTime(reg["FechaSalida"]);
                    int diasTranscurridos = (date_1 - hoy).Days;
                    string productod = reg["Nombre"].ToString();

                    //System.Diagnostics.Debug.WriteLine(diasTranscurridos);


                    if (diasTranscurridos <= 15)
                    {
                        mensaje = mensaje + "-- El producto " + reg["Nombre"].ToString() + " tiene " + diasTranscurridos.ToString() + " dias para salir. ";

                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("aun no");
                    }
                }
                lee_mensaje = "'" + mensaje + "'";
                System.Diagnostics.Debug.WriteLine(lee_mensaje);
                string javaScript = "MostrarMensaje(" + lee_mensaje + ")";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);

                con.Close();
            }
        }

        [WebMethod]
        public static List<Productos> listarProductos()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                lista = ProductosLN.getInstance().listarProductos();
            }
            catch (Exception e)
            {

                throw e;
            }


            //System.Diagnostics.Debug.WriteLine("listas");
            //foreach (var item in lista)
            //{
            //    System.Diagnostics.Debug.WriteLine(item.Nombre);

            //}
            return lista;

        }
        //string id, string nombre, string descripcion, string categoria, string cantidad, string unidad, string ubicacion

        [WebMethod]
        public static bool registrarDetalle_Ventas(List<Detalle_Ventas> sellListFormat)
        {
            bool response = false;
            try
            {
                response = Detalle_VentasLN.getInstance().registrarVenta(sellListFormat);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response;
        }

        [WebMethod]
        public static Ventas registrarVenta(Ventas venta)
        {
            Ventas objetoVenta = new Ventas();
            objetoVenta.Total = venta.Total;
            objetoVenta.Fecha = Convert.ToDateTime(venta.Fecha).ToString("yyyy-MM-dd");
            objetoVenta.Hora = venta.Hora;
            return VentasLN.getInstance().registrarVenta(objetoVenta);
        }

        [WebMethod]
        public static Productos addProduct(string Id)
        {

            int IdEntero = Convert.ToInt32(Id);
            Productos objProducto = new Productos();

            try
            {
                objProducto = ProductosLN.getInstance().find(IdEntero);
            }
            catch (Exception e)
            {
                throw e;
            }

            return objProducto;



        }
        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
            Session.Abandon();
        }







    }
}