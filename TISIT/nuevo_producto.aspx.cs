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
    public partial class nuevo_producto : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        public static string fileName;

        protected void Page_Load(object sender, EventArgs e)

        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);

            if (!IsPostBack)
            {
                dropCategorias.Items.Clear();
                dropCategorias.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<--Selecciona una Categoria-->", "0"));
                dropCategorias.Items[0].Attributes["disabled"] = "disabled";
                dropCategorias.Items.FindByValue("0").Selected = true;

                dropCategorias.Items.Insert(1, new System.Web.UI.WebControls.ListItem("No perecedero", "No perecedero"));
                dropCategorias.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Perecedero A", "Perecedero A"));
                dropCategorias.Items.Insert(3, new System.Web.UI.WebControls.ListItem("Perecedero B", "Perecedero B"));
                dropCategorias.Items.Insert(4, new System.Web.UI.WebControls.ListItem("Perecedero C", "Perecedero C"));

                dropUnidad.Items.Clear();
                dropUnidad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<--Selecciona una Unidad de Medida-->", "0"));
                dropUnidad.Items[0].Attributes["disabled"] = "disabled";
                dropUnidad.Items.FindByText("<--Selecciona una Unidad de Medida-->").Selected = true;
                dropUnidad.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Toneladas", "Toneladas"));
                dropUnidad.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Kilogramos", "Kilogramos"));
                dropUnidad.Items.Insert(3, new System.Web.UI.WebControls.ListItem("Litros", "Litros"));
                dropUnidad.Items.Insert(4, new System.Web.UI.WebControls.ListItem("Piezas", "Piezas"));
                dropUnidad.Items.Insert(5, new System.Web.UI.WebControls.ListItem("Metros", "Metros"));

                edit_dropUnidad.Items.Clear();
                foreach (System.Web.UI.WebControls.ListItem item in dropUnidad.Items)
                {
                    edit_dropUnidad.Items.Add(item);
                }

                edit_dropCategorias.Items.Clear();
                foreach (System.Web.UI.WebControls.ListItem item in dropCategorias.Items)
                {
                    edit_dropCategorias.Items.Add(item);
                }


                dpUbicacion.Items.Clear();
                dpUbicacion.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<--Selecciona una Ubicacion-->"));
                dpUbicacion.Items[0].Attributes["disabled"] = "disabled";
                dpUbicacion.Items.FindByText("<--Selecciona una Ubicacion-->").Selected = true;
                dpUbicacion.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Patio [Seccion 1]", "Patio [Seccion 1]"));
                dpUbicacion.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Patio [Seccion 2]", "Patio [Seccion 2]"));
                dpUbicacion.Items.Insert(3, new System.Web.UI.WebControls.ListItem("Patio [Seccion 3]", "Patio [Seccion 3]"));
                dpUbicacion.Items.Insert(4, new System.Web.UI.WebControls.ListItem("Bodega [Seccion 1]", "Bodega [Seccion 1]"));
                dpUbicacion.Items.Insert(5, new System.Web.UI.WebControls.ListItem("Bodega [Seccion 2]", "Bodega [Seccion 2]"));
                dpUbicacion.Items.Insert(6, new System.Web.UI.WebControls.ListItem("Bodega [Seccion 3]", "Bodega [Seccion 3]"));

                edit_dropUbicacion.Items.Clear();
                foreach (System.Web.UI.WebControls.ListItem item in dpUbicacion.Items)
                {
                    edit_dropUbicacion.Items.Add(item);
                }



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
        public static bool actualizarProducto(Productos objetoProducto)
        {

            bool response = false;
            Productos objProducto = new Productos();
            objProducto.Id = Convert.ToInt32(objetoProducto.Id);
            objProducto.Nombre = objetoProducto.Nombre;
            objProducto.Descripcion = objetoProducto.Descripcion;
            objProducto.Precio = Convert.ToDecimal(objetoProducto.Precio);
            objProducto.Categoria = objetoProducto.Categoria;
            objProducto.Ubicacion = objetoProducto.Ubicacion;
            objProducto.Cantidad = Convert.ToDecimal(objetoProducto.Cantidad);
            objProducto.Unidad = objetoProducto.Unidad;

            int IdEntero = objetoProducto.Id;

            Productos producto = ProductosLN.getInstance().find(IdEntero);

            objProducto.FechaEntrada = producto.FechaEntrada;

            switch (objetoProducto.Categoria)
            {
                case "No perecedero":

                    objProducto.FechaSalida = Convert.ToDateTime(producto.FechaEntrada).AddMonths(6).ToString("d");

                    break;

                case "Perecedero A":

                    objProducto.FechaSalida = Convert.ToDateTime(producto.FechaEntrada).AddMonths(1).ToString("d");

                    break;

                case "Perecedero B":

                    objProducto.FechaSalida = Convert.ToDateTime(producto.FechaEntrada).AddMonths(2).ToString("d");

                    break;

                case "Perecedero C":

                    objProducto.FechaSalida = Convert.ToDateTime(producto.FechaEntrada).AddMonths(6).ToString("d");

                    break;


            }


            response = ProductosLN.getInstance().actualizarProducto(objProducto);

            return response;



        }

        [WebMethod]
        public static void saveImg()
        {
            string fullpath = AppDomain.CurrentDomain.BaseDirectory;
            string upload = fullpath + WC.ImagenRuta;

            var httpPostedFile = HttpContext.Current.Request.Files["UploadImg"];

            httpPostedFile.SaveAs(upload + fileName);


        }

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            bool response = false;
            Productos producto = new Productos();

            if (FUImgProducto.HasFiles)
            {

                string fullpath = AppDomain.CurrentDomain.BaseDirectory;
                string upload = fullpath + WC.ImagenRuta;
                string filename = Guid.NewGuid().ToString();
                string extension = System.IO.Path.GetExtension(FUImgProducto.FileName.ToLower());
                string junto = upload + filename + extension;

                producto.ImgURL = filename + extension;

                //Crear codigo de barras
                var codigo = new Random();
                int intcodigo = Convert.ToInt32(codigo.Next(100000, 1000000));
                string strcodigo = intcodigo.ToString();

                producto.Id = intcodigo;

                Barcode codigoB = new Barcode();

                codigoB.IncludeLabel = true;
                codigoB.Alignment = AlignmentPositions.CENTER;
                codigoB.LabelFont = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 14, FontStyle.Regular);

                Image codeImg = codigoB.Encode(TYPE.CODE128, strcodigo, System.Drawing.Color.Black, System.Drawing.Color.White, 200, 100);



                string root = fullpath + WC.CodigoRuta + strcodigo + ".jpg";


                producto.Nombre = txbNombre.Text;

                producto.Descripcion = txbDescripcion.Text;
                producto.Categoria = dropCategorias.SelectedItem.Text;
                producto.Ubicacion = dpUbicacion.SelectedItem.Text;


                producto.Cantidad = Convert.ToDecimal(txbCantidad.Text);
                producto.Precio = Convert.ToDecimal(txbPrecio.Text);



                producto.Unidad = dropUnidad.SelectedItem.Text;
                producto.FechaEntrada = DateTime.Now.ToString("d");


                switch (dropCategorias.SelectedItem.Text)
                {
                    case "No perecedero":

                        producto.FechaSalida = DateTime.Now.AddMonths(6).ToString("d");

                        break;

                    case "Perecedero A":

                        producto.FechaSalida = DateTime.Now.AddMonths(1).ToString("d");

                        break;

                    case "Perecedero B":

                        producto.FechaSalida = DateTime.Now.AddMonths(2).ToString("d");

                        break;

                    case "Perecedero C":

                        producto.FechaSalida = DateTime.Now.AddMonths(6).ToString("d");

                        break;


                }


                response = ProductosLN.getInstance().registrarProducto(producto);

                if (response)
                {
                    txbNombre.Text = string.Empty;
                    txbDescripcion.Text = string.Empty;
                    dropUnidad.SelectedIndex = 0;
                    txbCantidad.Text = string.Empty;
                    dropCategorias.SelectedIndex = 0;
                    dpUbicacion.SelectedIndex = 0;
                    txbPrecio.Text = string.Empty;

                    FUImgProducto.Dispose();
                    codigoB.SaveImage(root, SaveTypes.JPG);
                    FUImgProducto.PostedFile.SaveAs(upload + filename + extension);


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "Exitoso()", true);
                }

            }
        }

        [WebMethod]
        public static void imprimirPDF(string Id, string Nombre, string Descripcion, string Precio, string CategoriaId, string ImgURL)
        {
            string fullpath = AppDomain.CurrentDomain.BaseDirectory;
            string filename = Id + ".pdf";

            using (PdfWriter pdfWriter = new PdfWriter(fullpath + "\\ReportesProductos\\" + filename))
            using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
            using (Document document = new Document(pdfDocument))
            {
                document.SetMargins(75, 35, 70, 35);
                document.Add(new Paragraph("VISTA DETALLADA DEL PRODUCTO").SetFontSize(25).SetTextAlignment(TextAlignment.CENTER));

                iText.Layout.Style styleCell = new iText.Layout.Style().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER);
                iText.Layout.Element.Table tabla = new iText.Layout.Element.Table(4).UseAllAvailableWidth();
                iText.Layout.Element.Cell celda = new iText.Layout.Element.Cell().Add(new Paragraph("Nombre").SetFontSize(15));
                tabla.AddHeaderCell(celda.AddStyle(styleCell));
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Descripcion").SetFontSize(15));
                tabla.AddHeaderCell(celda.AddStyle(styleCell));
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Precio").SetFontSize(15));
                tabla.AddHeaderCell(celda.AddStyle(styleCell));
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("CategoriaId").SetFontSize(15));
                tabla.AddHeaderCell(celda.AddStyle(styleCell));

                celda = new iText.Layout.Element.Cell().Add(new Paragraph(Nombre));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph(Descripcion));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$" + Precio));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph(CategoriaId));
                tabla.AddCell(celda);

                document.Add(tabla);

                ImageData imgProductoData = ImageDataFactory.Create(fullpath + "\\Imagenes\\productos\\" + ImgURL);
                iText.Layout.Element.Image imgProducto = new iText.Layout.Element.Image(imgProductoData);

                ImageData imgCbData = ImageDataFactory.Create(fullpath + "\\Imagenes\\productos\\imgCodigos\\" + Id + ".jpg");
                iText.Layout.Element.Image imgCb = new iText.Layout.Element.Image(imgCbData);


                iText.Layout.Element.Table tablaImg = new iText.Layout.Element.Table(2).UseAllAvailableWidth();
                iText.Layout.Element.Cell celdaImg = new iText.Layout.Element.Cell().Add(imgProducto.SetAutoScale(true));
                tablaImg.AddCell(celdaImg);
                celdaImg = new iText.Layout.Element.Cell().Add(imgCb.SetAutoScale(true));
                tablaImg.AddCell(celdaImg);

                document.Add(tablaImg);
            }

        }

        public void btnImprimir_Click(object sender, EventArgs e)
        {
            //string fullpath = AppDomain.CurrentDomain.BaseDirectory;
            //Bitmap image = new Bitmap(Bitmap.FromFile(fullpath + "\\Imagenes\\186730.bmp"));
            //var printer = new Printer("mImpresora", Vip.Printer.Enums.PrinterType.Epson);
            //printer.AlignCenter();
            //printer.WriteLine("Meu texto aqui!");
            //printer.Image(image);
            //printer.PartialPaperCut();
            //printer.PrintDocument();

            string fullpath = AppDomain.CurrentDomain.BaseDirectory;
            string filename = Guid.NewGuid().ToString();
            string join = fullpath + "\\ReportesProductos\\" + filename + ".pdf";

            FileStream file = new FileStream(join, FileMode.Create);
            PdfWriter pw = new PdfWriter(file);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document document = new Document(pdfDocument, PageSize.A4);
            document.SetMargins(75, 35, 70, 35);

            document.Add(new Paragraph("Vista Detalla del Producto"));
            document.Close();

            File.Open(join, FileMode.Open);
            //string fullpath = AppDomain.CurrentDomain.BaseDirectory;
            //Printer printer = new Printer("mImpresora");
            //Bitmap image = new Bitmap(Bitmap.FromFile(fullpath + "\\Imagenes\\186730.bmp"));


            //printer.DoubleWidth3();
            //printer.AlignCenter();
            //printer.Append("Abarrotes Amsiedad");
            //printer.Separator('~');

            //printer.Append("Pampas----------$10.00");
            //printer.Append("Comcacola-------$12.00");
            //printer.Append("Cimgarros-------$55.00");
            //printer.AlignRight();
            //printer.BoldMode("TOTAL:");
            //printer.Append("$77.00");
            //printer.Separator('~');
            //printer.AlignCenter();
            //printer.Image(image);
            //printer.NewLine();
            //printer.BoldMode("Gracias por su compra");

            //printer.NewLines(3);
            //printer.Separator('~');


            //printer.FullPaperCut();
            //printer.PrintDocument();



        }

        protected void permisosGenerales()
        {
            con.Open();

            //ADMIN
            string queryad = "SELECT admin FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdad = new SqlCommand(queryad, con);
            bool ciertoad = Convert.ToBoolean(cmdad.ExecuteScalar());

            string queryadl = "SELECT adminlect FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdadl = new SqlCommand(queryadl, con);
            bool ciertoadl = Convert.ToBoolean(cmdadl.ExecuteScalar());

            string queryadb = "SELECT adminbloq FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdadb = new SqlCommand(queryadb, con);
            bool ciertoadb = Convert.ToBoolean(cmdadb.ExecuteScalar());

            if (ciertoadb == true)
            {
                adminlink.Visible = false;

            }
            //else if ((ciertoad == true) || (ciertoadl == true))
            //{
            //    btnadmin.Enabled = true;

            //}

            //MEDICOS Y CITAS
            string queryp = "SELECT med FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmd1 = new SqlCommand(queryp, con);
            bool ciertop = Convert.ToBoolean(cmd1.ExecuteScalar());

            string querypl = "SELECT medlect FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdpl = new SqlCommand(querypl, con);
            bool ciertopl = Convert.ToBoolean(cmdpl.ExecuteScalar());

            string querypb = "SELECT medbloq FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdpb = new SqlCommand(querypb, con);
            bool ciertopb = Convert.ToBoolean(cmdpb.ExecuteScalar());

            if (ciertopb == true)
            {
                faclink.Visible = false;
            }
            ////else if ((ciertop == true) || (ciertopl == true))
            ////{
            ////    btnpred.Enabled = true;
            ////}

            //DOCUMENTOS Y SOLICITUD
            string queryds = "SELECT med FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdds1 = new SqlCommand(queryds, con);
            bool ciertods = Convert.ToBoolean(cmdds1.ExecuteScalar());

            string querydsl = "SELECT medlect FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmddsl = new SqlCommand(querydsl, con);
            bool ciertodsl = Convert.ToBoolean(cmddsl.ExecuteScalar());

            string querydsb = "SELECT medbloq FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmddsb = new SqlCommand(querydsb, con);
            bool ciertodsb = Convert.ToBoolean(cmddsb.ExecuteScalar());

            if (ciertodsb == true)
            {
                docpaclink.Visible = false;
            }
            ////else if ((ciertop == true) || (ciertopl == true))
            ////{
            ////    btnpred.Enabled = true;
            ////}


            //PRODUCTOS
            string querydp = "SELECT prod FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmddp = new SqlCommand(querydp, con);
            bool ciertodp = Convert.ToBoolean(cmddp.ExecuteScalar());

            string querydpl = "SELECT prodlect FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmddpl = new SqlCommand(querydpl, con);
            bool ciertodpl = Convert.ToBoolean(cmddpl.ExecuteScalar());

            string querydpb = "SELECT prodbloq FROM usuarios WHERE usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmddpb = new SqlCommand(querydpb, con);
            bool ciertodpb = Convert.ToBoolean(cmddpb.ExecuteScalar());

            if (ciertodpb == true)
            {
                deslink.Visible = false;
            }
            ////else if ((ciertop == true) || (ciertopl == true))
            ////{
            ////    btnpred.Enabled = true;
            ////}

            con.Close();
        }

        public void perm()
        {
            con.Open();
            //Permisos Administracion

            string queryp1 = "SELECT usuario1 FROM usuarios where usuario = '" + lbcorreo.Text + "'";
            SqlCommand cmdp1 = new SqlCommand(queryp1, con);
            bool ciertop1 = Convert.ToBoolean(cmdp1.ExecuteScalar());

            if (ciertop1 == false)
            {
                usulink.Visible = false;
            }

            string queryp2 = "SELECT dir FROM usuarios where usuario = '" + lbcorreo.Text + "'";
            SqlCommand cmdp2 = new SqlCommand(queryp2, con);
            bool ciertop2 = Convert.ToBoolean(cmdp2.ExecuteScalar());

            if (ciertop2 == false)
            {
                direclink.Visible = false;
            }

            string queryp3 = "SELECT lab FROM usuarios where usuario = '" + lbcorreo.Text + "'";
            SqlCommand cmdp3 = new SqlCommand(queryp3, con);
            bool ciertop3 = Convert.ToBoolean(cmdp3.ExecuteScalar());

            if (ciertop3 == false)
            {
                lablink.Visible = false;
            }

            string queryp4 = "SELECT per FROM usuarios where usuario = '" + lbcorreo.Text + "'";
            SqlCommand cmdp4 = new SqlCommand(queryp4, con);
            bool ciertop4 = Convert.ToBoolean(cmdp4.ExecuteScalar());

            if (ciertop4 == false)
            {
                perlink.Visible = false;
            }

            string queryp5 = "SELECT doc FROM usuarios where usuario = '" + lbcorreo.Text + "'";
            SqlCommand cmdp5 = new SqlCommand(queryp5, con);
            bool ciertop5 = Convert.ToBoolean(cmdp5.ExecuteScalar());

            if (ciertop5 == false)
            {
                doculink.Visible = false;
            }

            //PERMISOS FACTURAS

            string queryp6 = "SELECT docto FROM usuarios where usuario = '" + lbcorreo.Text + "'";
            SqlCommand cmdp6 = new SqlCommand(queryp6, con);
            bool ciertop6 = Convert.ToBoolean(cmdp6.ExecuteScalar());

            if (ciertop6 == false)
            {
                doclink.Visible = false;
            }

            string queryp7 = "SELECT pac FROM usuarios where usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdp7 = new SqlCommand(queryp7, con);
            bool ciertop7 = Convert.ToBoolean(cmdp7.ExecuteScalar());

            if (ciertop7 == false)
            {
                verlink.Visible = false;
            }

            string queryp8 = "SELECT cit FROM usuarios where usuario = '" + lbcorreo.Text + "'";
            SqlCommand cmdp8 = new SqlCommand(queryp8, con);
            bool ciertop8 = Convert.ToBoolean(cmdp8.ExecuteScalar());

            if (ciertop8 == false)
            {
                faclink1.Visible = false;
            }

            string queryp9 = "SELECT ag FROM usuarios where usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdp9 = new SqlCommand(queryp9, con);
            bool ciertop9 = Convert.ToBoolean(cmdp9.ExecuteScalar());

            if (ciertop9 == false)
            {
                reporlink.Visible = false;
            }

            string queryp10 = "SELECT cal FROM usuarios where usuario = '" + lbcorreo.Text + "'";
            SqlCommand cmdp10 = new SqlCommand(queryp10, con);
            bool ciertop10 = Convert.ToBoolean(cmdp10.ExecuteScalar());

            if (ciertop10 == false)
            {
                callink.Visible = false;
            }

            //PERMISOS PRODUCTOS


            string queryp13 = "SELECT inv FROM usuarios where usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdp13 = new SqlCommand(queryp13, con);
            bool ciertop13 = Convert.ToBoolean(cmdp13.ExecuteScalar());

            if (ciertop13 == false)
            {
                nuevolink.Visible = false;
            }

            string queryp14 = "SELECT nprod FROM usuarios where usuario = '" + lbcorreo.Text + "'";
            SqlCommand cmdp14 = new SqlCommand(queryp14, con);
            bool ciertop14 = Convert.ToBoolean(cmdp14.ExecuteScalar());

            if (ciertop14 == false)
            {
                proclink.Visible = false;
            }

            string queryp15 = "SELECT ven FROM usuarios where usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdp15 = new SqlCommand(queryp15, con);
            bool ciertop15 = Convert.ToBoolean(cmdp15.ExecuteScalar());

            if (ciertop15 == false)
            {
                consulink.Visible = false;
            }

            //DOCUMENTOS Y SOLICITUDES

            string queryp16 = "SELECT sol FROM usuarios where usuario = '" + lbcorreo.Text + "'";
            SqlCommand cmdp16 = new SqlCommand(queryp16, con);
            bool ciertop16 = Convert.ToBoolean(cmdp16.ExecuteScalar());

            if (ciertop16 == false)
            {
                sollink.Visible = false;
            }

            string queryp17 = "SELECT cons FROM usuarios where usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdp17 = new SqlCommand(queryp17, con);
            bool ciertop17 = Convert.ToBoolean(cmdp17.ExecuteScalar());

            if (ciertop17 == false)
            {
                conslink.Visible = false;
            }

            string queryp18 = "SELECT ing FROM usuarios where usuario= '" + lbcorreo.Text + "'";
            SqlCommand cmdp18 = new SqlCommand(queryp18, con);
            bool ciertop18 = Convert.ToBoolean(cmdp18.ExecuteScalar());

            if (ciertop18 == false)
            {
                inglink.Visible = false;
            }

            con.Close();

        }

        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
            Session.Abandon();
        }
    }
}