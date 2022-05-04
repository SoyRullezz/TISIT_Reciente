using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Data.OleDb;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = iTextSharp.text.Image;
using System.Globalization;

using CapaEntidades;
using CapaLogicaNegocio;

namespace TISIT
{
    public partial class ingreso : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        int doctor = 0;
        string nameDoctor;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);

            if (!IsPostBack)
            {
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));

                GVbind();
                GVbind1();
                GVbind2();
            }
        }

        protected void GVbind()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM registro_pacientes_llegada where tipo_cita = 'OPERACION' AND ingresado = 0", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridregistro.DataSource = dr;
                    gridregistro.DataBind();
                }
            }
        }

        protected void GVbind1()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ingresos_sl", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridingresos.DataSource = dr;
                    gridingresos.DataBind();
                }
            }
        }

        protected void GVbind2()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ingresos_sl", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridingresos1.DataSource = dr;
                    gridingresos1.DataBind();
                }
            }
        }

        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
            Session.Abandon();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            doctor = Convert.ToInt32(DropDownList1.SelectedValue);
            nameDoctor = DropDownList1.SelectedItem.Text;

            ViewState["Codigo2"] = Convert.ToInt32(DropDownList1.SelectedValue);
        }

        protected void gridregistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtpaciente.Text = HttpUtility.HtmlDecode(gridregistro.SelectedRow.Cells[1].Text);

            gridregistro.SelectedIndex = -1;


        }

        protected void gridingresos_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtpaciente1.Text = HttpUtility.HtmlDecode(gridingresos.SelectedRow.Cells[1].Text);
            txtfecha1.Text = HttpUtility.HtmlDecode(gridingresos.SelectedRow.Cells[2].Text);
            txtedad1.Text = HttpUtility.HtmlDecode(gridingresos.SelectedRow.Cells[3].Text);
            txtsexo1.Text = HttpUtility.HtmlDecode(gridingresos.SelectedRow.Cells[4].Text);
            txtmedico1.Text = HttpUtility.HtmlDecode(gridingresos.SelectedRow.Cells[5].Text);
            txtdiagnostico1.Text = HttpUtility.HtmlDecode(gridingresos.SelectedRow.Cells[6].Text);

            gridingresos.SelectedIndex = -1;
        }

        protected void gridingresos1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtpaciente2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[1].Text);
            txtfecha2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[2].Text);
            txtedad2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[3].Text);
            txtsexo2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[4].Text);
            txtpeso2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[5].Text);
            txttalla2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[6].Text);
            txtrepresentante2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[7].Text);
            txtmedico2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[8].Text);
            txtdiagnostico2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[9].Text);
            txtprocedimiento2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[10].Text);
            txtalergias2.Text = HttpUtility.HtmlDecode(gridingresos1.SelectedRow.Cells[11].Text);

            gridingresos1.SelectedIndex = -1;
        }

        protected void btnpdf_Click(object sender, EventArgs e)
        {
            insertar();
            GVbind();
            GVbind1();
            GVbind2();

            using (StringWriter sw = new StringWriter())
            {
                StringWriter sw1 = new StringWriter();

                using (HtmlTextWriter hw = new HtmlTextWriter(sw))

                {
                    HtmlTextWriter hw1 = new HtmlTextWriter(sw1);



                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=hoja_ingreso.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);


                    StringReader sr = new StringReader(sw.ToString());

                    Document documento = new Document(PageSize.A4, 25f, 25f, 25f, 25f);
                    PdfWriter writer = PdfWriter.GetInstance(documento, Response.OutputStream);

                    documento.Open();

                    BaseFont _titulo = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo = new iTextSharp.text.Font(_titulo, 20f, Font.BOLD);

                    BaseFont _titulo2 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo2 = new iTextSharp.text.Font(_titulo2, 15f, Font.BOLD);

                    BaseFont _titulo3 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo3 = new iTextSharp.text.Font(_titulo3, 11f, Font.BOLD);

                    BaseFont _subtitulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo = new iTextSharp.text.Font(_subtitulo, 12f);

                    BaseFont _subtitulo2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo2 = new iTextSharp.text.Font(_subtitulo2, 10f, Font.BOLD);

                    BaseFont _subtitulo3 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo3 = new iTextSharp.text.Font(_subtitulo3, 12f);

                    BaseFont _subtitulo4 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, true);
                    Font subtitulo4 = new iTextSharp.text.Font(_subtitulo3, 10f);

                    BaseFont _footer = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font footer = new iTextSharp.text.Font(_footer, 10f, Font.BOLD);

                    BaseFont _parrafor = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo = new iTextSharp.text.Font(_parrafor, 9f, Font.NORMAL);
                    BaseFont _Uparrafor = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font Uparrafo = new iTextSharp.text.Font(_Uparrafor, 9f, Font.UNDERLINE);

                    BaseFont _parrafor2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo2 = new iTextSharp.text.Font(_parrafor2, 11f, Font.NORMAL);

                    BaseFont _parrafor3 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo3 = new iTextSharp.text.Font(_parrafor3, 8f, Font.NORMAL);

                    BaseFont _parrafor4 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo4 = new iTextSharp.text.Font(_parrafor4, 5f, Font.NORMAL);

                    BaseFont _espacio = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font espacio = new iTextSharp.text.Font(_espacio, 10f, Font.NORMAL, BaseColor.WHITE);

                    BaseFont _espacio2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font espacio2 = new iTextSharp.text.Font(_espacio2, 5f, Font.NORMAL, BaseColor.WHITE);


                    Image logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/santa_lucia.png"));
                    logo.ScalePercent(40f);

                    Image circle_white = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/circle_white.png"));
                    circle_white.ScalePercent(1.5f);

                    Image circle_dark = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/circle_dark.png"));
                    circle_dark.ScalePercent(3.2f);

                    Image flechas = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/flechas.png"));
                    flechas.ScalePercent(14f);

                    Image flecha = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/flecha.png"));
                    flecha.ScalePercent(16f);

                    Image cuadros = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/cuadros.PNG"));
                    cuadros.ScalePercent(35f);

                    Image cuadros2 = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/cuadros2.PNG"));
                    cuadros2.ScalePercent(35f);

                    Image casilla = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/casilla.png"));
                    casilla.ScalePercent(2f);

                    Image casilla_check = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/casilla_check.png"));
                    casilla_check.ScalePercent(2f);

                    if (ddsexo.SelectedIndex == 0)
                    {
                        ddsexo.Text = "";
                    }

                    string dia = DateTime.Today.Day.ToString();
                    int mes = DateTime.Today.Month;
                    string year = DateTime.Today.Year.ToString();
                    string nombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mes);

                    //HEADER//
                    var tb1 = new PdfPTable(new float[] { 90f }) { WidthPercentage = 90f };
                    tb1.AddCell(new PdfPCell(logo) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("HOJA DE INGRESO", titulo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("No. EXPEDIENTE: _________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("NOMBRE DEL PACIENTE: " + txtpaciente.Text.ToUpper() + ".     TEMPERATURA: PTE. _________ FAM.___________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("FECHA DE NACIMIENTO: " + txtfecha.Text.ToUpper() + ".      EDAD: " + txtedad.Text.ToUpper() + ".     SEXO: " + ddsexo.Text + ".     PESO " + txtpeso.Text + " kg.      TALLA " + txttalla.Text + " cm.", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("DOMICILIO: " + txtdomicilio.Text.ToUpper() + " C.P.: " + txtcp.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("OCUPACIÓN: " + txtocupacion.Text.ToUpper() + ".        TEL. PACIENTE: " + txttel.Text + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("TEL. FAMILIAR: " + txtfamiliar.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("FECHA DE INGRESO: " + txtfechaentrada.Text.ToUpper() + ".        FECHA DE EGRESO: _____________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("HORA DE INGRESO: " + txthoraentrada.Text.ToUpper() + ".         HORA DE EGRESO: ______________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("DX. INGRESO: " + txtdxingreso.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(" ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("DX. EGRESO ____________________________________________________________________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("________________________________________________________________________________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("ESTUDIOS REALIZADOS: " + txtestudios.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("ALERGIAS: " + txtalergias.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("OBSERVACIONES: " + txtobservaciones.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("MÉDICO TRATANTE:", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(DropDownList1.SelectedItem.Text.ToUpper(), Uparrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("NOMBRE COMPLETO Y FIRMA", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102, Col. Morelos, Toluca, Estado de México, C.P. 50120", footer)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase("(722) 719 89 75 / 705 07 59", footer)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    documento.Add(tb1);

                    var tb2 = new PdfPTable(new float[] { 90f }) { WidthPercentage = 90f };
                    tb2.AddCell(new PdfPCell(logo) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("CARTA DE LIBERACIÓN DE RESPONSABILIDAD", titulo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("Toluca, Méx., a " + dia + " de " + nombreMes.ToUpper() + " de " + year + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("Por medio de la presente, DESLINDO DE TODA", parrafo2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("RESPONSABILIDAD a la ''CLÍNICA SANTA LUCÍA TOLUCA''", parrafo2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("de todos los procedimientos MÉDICOS QUIRÚRGICO'", parrafo2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("ANESTÉSICOS, que se me realicen, en el entendido que toda'", parrafo2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("desición médica es responsabilidad de MI Médico tratante.'", parrafo2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb2);

                    var tb5 = new PdfPTable(new float[] { 40f, 40f }) { WidthPercentage = 80f };
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase("_____________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase("_____________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb5.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL PACIENTE", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL RESPONSABLE", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(DropDownList1.SelectedItem.Text.ToUpper(), Uparrafo)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase("MÉDICO TRATANTE", parrafo)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb5.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102, Col. Morelos, Toluca, Estado de México, C.P. 50120", footer)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase("(722) 719 89 75 / 705 07 59", footer)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    documento.Add(tb5);

                    var tb3 = new PdfPTable(new float[] { 95f }) { WidthPercentage = 95f };
                    tb3.AddCell(new PdfPCell(logo) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("CARTA DE CONSENTIMIENTO BAJO INFORMACIÓN TRATAMIENTO MÉDICO", titulo2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Toluca, Méx., a " + dia + " de " + nombreMes.ToUpper() + " de " + year + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("NOMBRE DEL PACIENTE: " + txtpaciente.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Nombre del representante legal: " + txtrepresentante.Text.ToUpper() + " PARENTESCO: " + ddparentesco.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Bajo protesta de decir verdad, declaro que el Dr.: " + DropDownList1.SelectedItem.Text.ToUpper() + " me ha explicado", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("que mi diagnóstico es: " + txtdiagnostico.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Y que por tal motivo debo someterme al (los) siguiente(s) con fines diagnósticos y/o terapéuticos: " + txtprocedimiento.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(" ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(" ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Entiendo que todo acto médico diagnóstico de tratamiento, sea quirúrgico puede ocasionar una serie de complicaciones mayores o menores, a veces potencialmente serias que incluyen cierto riesgo de muerte y puede requerir tratamientos complementarios médicos y/o quirúrgicos, que aumenten la estancia hopitalaria. Dichas complicaciones a veces son derivadas directamente de la propia técnica, pero otras dependerá del procedimiento, del estado del paciente y de los tratamientos que ha recibido y de las posibles anomalías anatómicas y/o de la utilización de los equipos médicos. Reconozco que entre los posibles riesgos y complicaciones que pueden surgir se encuentra(n):", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_JUSTIFIED, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(txtcomplicaciones.Text.ToUpper(), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(" ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(" ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Los probables beneficios esperados son:", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(" " + txtbeneficios.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(" ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("El pronóstico es: " + txtpronostico.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Declaro que he comprendido las explicaciones que se me han facilitado en un lneguaje claro y sencillo, y que el médico que me atiende me ha permitido realizar todas las observaciones y me ha aclarado todas las dudas que le he planteado. También comprendo, que por escrito, en cualquier momento puedo revocar el consentimiento que ahora otorgo. Por ello manifiesto que estoy satisfecho(a) con la información reciba y que comprendo el alcance de los riesgos del procedimiento.", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_JUSTIFIED, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Del mismo modo designo a: " + txtrepresentante.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("para que exclusivamente reciba información sobre mi estado de salud, diagnóstico, tratamiento y/o pronóstico", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("En tales condiciones CONSIENTO en forma libre y espontánea y sin ningún tipo de presión que se me realice:", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(" " + txtprocedimiento.Text.ToUpper() + " ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(" ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb3);

                    var tb4 = new PdfPTable(new float[] { 40f, 40f }) { WidthPercentage = 80f };
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(" " + DropDownList1.SelectedItem.Text.ToUpper() + " ", Uparrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(" " + txtpaciente.Text.ToUpper() + " ", Uparrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL MÉDICO INFORMANTE", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL PACIENTE", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("_______________________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("_______________________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL TESTIGO", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL TESTIGO", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb4.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102, Col. Morelos, Toluca, Estado de México, C.P. 50120", footer)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("(722) 719 89 75 / 705 07 59", footer)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    documento.Add(tb4);

                    var tb6 = new PdfPTable(new float[] { 60f, 10f, 10f, 10f, 10f }) { WidthPercentage = 100f };
                    tb6.AddCell(new PdfPCell(logo) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb6.AddCell(new PdfPCell(new Phrase("CARTA DE CONSENTIMIENTO BAJO INFORMACIÓN DE LOS PROCEDIMIENTOS DE ANESTESIA", titulo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("DÍA", subtitulo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("MES", subtitulo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("AÑO", subtitulo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("HORA", subtitulo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(" " + dia + " ", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(" " + nombreMes.ToUpper() + " ", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(" " + year + " ", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("  " + txthoraentrada.Text.ToUpper() + " ", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("NOMBRE COMPLETO DEL PACIENTE: " + txtpaciente.Text.ToUpper() + "  \n\n\n\n\n", parrafo)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    var cell = new PdfPCell(new Phrase("NOMBRE COMPLETO DEL RESPONSABLE DEL PACIENTE: " + txtrepresentante.Text.ToUpper() + " ", parrafo)) { Border = 0, Rowspan = 3, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_TOP };
                    cell.Border = PdfPCell.BOTTOM_BORDER;
                    cell.Border = PdfPCell.LEFT_BORDER;
                    tb6.AddCell(cell);
                    var cell2 = new PdfPCell(new Phrase("RESPONSABLE DEL PACIENTE:", parrafo)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cell2.Border = PdfPCell.RIGHT_BORDER;
                    tb6.AddCell(cell2);
                    if (ddparentesco.Text == "FAMILIAR")
                    {
                        tb6.AddCell(new PdfPCell(new Phrase("FAMILIAR", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        tb6.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        tb6.AddCell(new PdfPCell(new Phrase("REPRESENTANTE LEGAL", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        var cell3 = new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cell3.Border = PdfPCell.RIGHT_BORDER;
                        tb6.AddCell(cell3);
                        var cell5 = new PdfPCell(new Phrase(".", parrafo3)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cell5.Border = PdfPCell.BOTTOM_BORDER;
                        tb6.AddCell(cell5);
                        tb6.AddCell(new PdfPCell(new Phrase("OTRO", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        var cell4 = new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cell4.Border = PdfPCell.RIGHT_BORDER;
                        tb6.AddCell(cell4);
                    }
                    else if (ddparentesco.Text == "REPRESENTANTE LEGAL")
                    {
                        tb6.AddCell(new PdfPCell(new Phrase("FAMILIAR", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        tb6.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        tb6.AddCell(new PdfPCell(new Phrase("REPRESENTANTE LEGAL", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        var cell3 = new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cell3.Border = PdfPCell.RIGHT_BORDER;
                        tb6.AddCell(cell3);
                        var cell5 = new PdfPCell(new Phrase(".", parrafo3)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cell5.Border = PdfPCell.BOTTOM_BORDER;
                        tb6.AddCell(cell5);
                        tb6.AddCell(new PdfPCell(new Phrase("OTRO", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        var cell4 = new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cell4.Border = PdfPCell.RIGHT_BORDER;
                        tb6.AddCell(cell4);
                    }
                    else if (ddparentesco.Text == "OTRO")
                    {
                        tb6.AddCell(new PdfPCell(new Phrase("FAMILIAR", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        tb6.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        tb6.AddCell(new PdfPCell(new Phrase("REPRESENTANTE LEGAL", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        var cell3 = new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cell3.Border = PdfPCell.RIGHT_BORDER;
                        tb6.AddCell(cell3);
                        var cell5 = new PdfPCell(new Phrase(".", parrafo3)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cell5.Border = PdfPCell.BOTTOM_BORDER;
                        tb6.AddCell(cell5);
                        tb6.AddCell(new PdfPCell(new Phrase("OTRO", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                        var cell4 = new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cell4.Border = PdfPCell.RIGHT_BORDER;
                        tb6.AddCell(cell4);
                    }

                    tb6.AddCell(new PdfPCell(new Phrase("" +
                        "POR MEDIO DE LA PRESENTE EN PLENA CAPACIDAD DE MIS FACULTADES COMO:\n" +
                        "                     PACIENTE (   )               RESPONSABLE DEL PACIENTE (   )\n\n" +
                        "ACEPTO Y AUTORIZO AL PERSONAL MÉDICO ADSCRITO AL SERVICIO DE ANESTESIOLOGÍA DE ESTA UNIDAD MÉDICA PARA QUE BAJO SU SUPERVISION ME SEA ADMINISTRADA LA TÉCNICA ANESTÉSICA QUE CONSIDERE NECESARIA PARA QUE SE REALICE\n\n" +
                        "                                            NOMBRE DEL ACTO O PROCEDIMIENTO PRINCIPAL QUE SE PLANEA REALIZAR\n\n" +
                        "LA SELECCIÓN ANESTÉSICA PARA ESTE PROCEDIMIENTO QUIRÚRGICO, COMO ME EXPLICÓ MI MÉDICO Y ESTANDO INFORMADO Y COMPREDIDO LO QUE SIGNIFICA CADA UNA DE ELLAS ES O SON:\n\n" +
                        "(   ) ANESTESIA GENERAL: INCLUYE LA ADMINISTRACIÓN DE MEDICAMENTOS INTRAVENOSOS Y GASES INHALADOS, LOS CUALES CAUSAN INCONCIENCIA\n\n" +
                        "(   ) ANESTESIA REGIONAL: INCLUYE LA ADMINISTRACIÓN DE MEDICAMENTOS MEDIANTE AGUJAS EN LOS ESPACIOS EPIDURAL Y/O ESPINAL Y EN CERCANÍA A NERVIOS, OCASIONANDO TEMPORALMENTE LA PERDIDA DE SENSACIONES DOLOROSAS Y MOTORAS EN ÁREAS DESEADAS PARA EL PROCEDIMIENTO QUIRÚRGICO Y DADO EL CASO, ANESTESIA POSTOPERATORIA\n\n" +
                        "(   ) ANESTESIA LOCAL: IMPLICA LA ADMINISTRACIÓN LOCAL DE MEDICAMENTOS, CON APOYO O NO DE MEDICAMENTOS SEDANTES INTRAVENOSOS\n\n" +
                        "ACEPTO Y AUTORIZO QUE EN CASO DE NO SER ADECUADA LA TÉCNICA ANESTÉSICA REGIONAL O LOCAL, SE ME ADMINISTRE LA ANESTESIA GENERAL.\n\n" +
                        "ACEPTO Y COMPRENDO QUE DURANTE EL CURSO DE LA ANESTESIA Y CIRUGIA, CAMBIOS IMPREDECIBLES EN MI CONDICION FISICA PUEDEN SURGIR, REQUIRIENDOSE HALER MODIFICACIONES A LOS CUIDADOS PORPORCIONADOS PREVIAMENTE: AUTORIZO, EN ESTE CASO, QUE EL ANESTESIOLOGO Y OTROS ESPECIALISTAS PUEDAN ACTUAR EN MI BENEFICIO COMO SU PRIORIDAD E INTERRUMPIR INCLUSO LA OPERACION.\n\n" +
                        "SOY CONSIENTE QUE LA ANESTESIA Y LA CIRUGIA NO SON CIENCIAS EXACTAS Y QUE NINGUNA GARANTIA PUEDE SER PROPORCIONADA EN LO QUE RESPECTA A LA ADMINISTRACION DE MEDICAMENTOS Y REALIZACION DE PROCEDIMIENTOS.HE SIDO INFORMADO DE LOS BENEFICIOS ASI COMO DE LOS RIESGOS FRECUENTES QUE ACOMPAFLAN A LA ANESTESIA SIENDO ESTOS: NAUSEA Y VOMITO, CEFALEA, DOLOR LUMBAR, MOLESTIAS Y ERITEMA EN LA GARGANTA, DOLOR MUSCULAR Y EDEMA EN TEJIDOS BLANDOS. \n\n" +
                        "DURANTE LOS PROCEDIMIENTOS DE CIRUGIA MENOR Y MAYOR PUEDE PRESENTARSE OTROS RIESGOS INESPERADOS, QUE INCLUYEN: LESIONES OCULARES, BOCA, LABIOS, PABELLON AURICULAR, PERDIDA PARCIAL 0 TOTAL DE DiENTES, DANO EN CUERDAS VOCALES, NEUMONIA SOMNOLIENCIA, ATURDIMIENTO, DOLOR, PARALISIS, LESION NERVIOSA, VASCULAR, ARTERIAL, TRASTORNO 0 DANO HEPATICO, TRASTORNO 0 DAt1 0 RENAL, REACCIONES ADVERSAS A MEDICAMENTOS Y EN CONTADOS CASOS LESION PERMANENTE DEL SISTEMA NERVIOSO CENTRAL, CORAZON, ACCIDENTE CEREBRO VASCULAR 0 INCLUSO LA MUERTE. \n\n" +
                        "ESTOS RIESGOS SON APLICABLES A TODO EL PROCEDIMIENTO QUIRURGICO INDEPENDIENTEMENTE DE LA ELECCION DE LA TECNICA ANESTESICA (GENERAL, REGIONAL O LOCAL).\n\n" +
                        "ACEPTO Y AUTORIZO SE ME REALICE UNA TRANSFUSION DE SANGRE 0 CUALQUIER OTRO PRODUCTO HEMATICO SI MI CONDICION LO REQUIERE.\n\n\n" +

                        "", parrafo))
                    { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });

                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    documento.Add(tb6);

                    var tb7 = new PdfPTable(new float[] { 14f, 14f, 14f, 14f, 14f, 14f, 14f }) { WidthPercentage = 98f };
                    tb7.AddCell(new PdfPCell(logo) { Colspan = 7, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb7.AddCell(new PdfPCell(new Phrase("\nCARTA DE CONSENTIMIENTO BAJO INFORMACIÓN DE LOS PROCEDIMIENTOS DE ANESTESIA\n", titulo3)) { Colspan = 7, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb7.AddCell(new PdfPCell(new Phrase("\n" +
                        "CERTIFICO QUE EN USO DE MI RAZÓN Y COMPLETA CONDICIÓN MENTAL, HE EXPRESADO  AL ANESTESIÓLOGO LOS SIGUIENTES ASPECTOS MÉDICOS:\n\n" +
                        "      1. TODAS MIS PATOLOGÍAS MAYORES Y MENORES\n" +
                        "      2. TODOS LOS PROCEDIMIENTOS ANESTÉSICOS A QUE HE SIDO SOMETIDO, ENFATIZANDO COMPLICACIONES      PREVIAS\n" +
                        "      3. TODOS LOS MEDICAMENTOS O SUSTANCIAS POSIBLES QUE ME PRODUCEN ALERGIA\n" +
                        "      4. TODOS LOS MEDICAMENTOS Y SUSTANCIAS TÓXICAS EMPLEADOS EN EL PASADO Y EN LA ACTUALIDAD\n" +
                        "      5. HE RESPONDIDO CON VERACIDAD A CUALQUIER PREGUNTA RELEVANTE RELACIONADA A LA APLICACIÓN DE      LA ANESTESIA\n" +
                        "FINALMENTE RECONOZCO EL PROPÓSITO Y LA NATURALEZA DEL PROCEDIMIENTO ANESTÉSICO, ASÍ COMO HE TENIDO LA OPORTUNIDAD DE PREGUNTAR Y RESOLVER TODAS MIS INQUIETUDES DE MANERA SATISFACTORIA.\n\n" +
                        "IGUALMENTE ENTIENDO QUE EN EL MOMENTO EN QUE LO DESEE, ESTE CONSENTIMIENTO PUEDE SER DESISTIDO POR MI PARTE\n" +

                        "", parrafo))
                    { Colspan = 7, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });

                    tb7.AddCell(new PdfPCell(new Phrase("MUJERES EMBARAZADAS", titulo3)) { Colspan = 7, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase("\n" +
                        "ACEPTO Y SOY CONSIENTE QUE LOS MEDICAMENTOS EN ANESTESIA, PUEDEN CRUZAR LA BARRERA PLACENTARIA PUDIENDO TEMPORALMENTE ANESTESIAR EL BEBÉ.\n\n" +
                        "AUNQUE COMPLICACIONES FATALES DURANTE EL EMBARAZO SON RARAS, ESTAS PUEDEN INCLUIR: DEFECTOS DE NACIMIENTO, PARTO PREMATURO, DAÑO AL SISTEMA NERVIOSO CENTRAL Y MUERTE.\n" +
                        "", parrafo))
                    { Colspan = 7, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });
                    tb7.AddCell(new PdfPCell(new Phrase("\n TODAS LAS PREGUNTAS FUERON SATISFACTORIAMENTE RESPONDIDAS Y EL PACIENTE ACEPTA EL PLAN ANESTÉSICO. \n\n", footer)) { Colspan = 7, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase("\nCLASIFICACIÓN (CÍRCULO)\n", titulo3)) { Colspan = 7, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase("I", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });
                    tb7.AddCell(new PdfPCell(new Phrase("II", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });
                    tb7.AddCell(new PdfPCell(new Phrase("III", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });
                    tb7.AddCell(new PdfPCell(new Phrase("IV", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });
                    tb7.AddCell(new PdfPCell(new Phrase("V", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });
                    tb7.AddCell(new PdfPCell(new Phrase("U", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });
                    tb7.AddCell(new PdfPCell(new Phrase("E", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });
                    tb7.AddCell(new PdfPCell(new Phrase("\n\n\n\n", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 });

                    documento.Add(tb7);

                    var tb8 = new PdfPTable(new float[] { 49f, 49f }) { WidthPercentage = 98f };
                    tb8.AddCell(new PdfPCell(new Phrase("\n AUTORIZA, PACIENTE O FAMILIAR RESPONSABLE\n\n\n\n\n\n\n\n\n\n\n NOMBRE Y FIRMA \n\n", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb8.AddCell(new PdfPCell(new Phrase("\n MÉDICO RESPONSABLE\n\n\n\n\n\n\n\n\n\n\n NOMBRE Y FIRMA \n\n", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb8.AddCell(new PdfPCell(new Phrase("\n\n\n", parrafo)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb8.AddCell(new PdfPCell(new Phrase("\n TESTIGO\n\n\n\n\n\n\n\n\n\n\n NOMBRE, PARENTESCO Y FIRMA \n\n", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb8.AddCell(new PdfPCell(new Phrase("\n MÉDICO RESPONSABLE\n\n\n\n\n\n\n\n\n\n\n NOMBRE Y FIRMA \n\n", parrafo)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb8.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb8.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb8.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb8);
                    documento.Close();
                    Response.Write(documento);
                    Response.End();


                }
            }

            limpiar1();
            

        }

        protected void btnpdf1_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                StringWriter sw1 = new StringWriter();

                using (HtmlTextWriter hw = new HtmlTextWriter(sw))

                {
                    HtmlTextWriter hw1 = new HtmlTextWriter(sw1);



                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=documento_cirugia.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);


                    StringReader sr = new StringReader(sw.ToString());

                    Document documento = new Document(PageSize.A4, 25f, 25f, 25f, 25f);
                    PdfWriter writer = PdfWriter.GetInstance(documento, Response.OutputStream);

                    documento.Open();

                    BaseFont _titulo = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo = new iTextSharp.text.Font(_titulo, 20f, Font.BOLD);

                    BaseFont _titulo2 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo2 = new iTextSharp.text.Font(_titulo2, 15f, Font.BOLD);

                    BaseFont _titulo3 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo3 = new iTextSharp.text.Font(_titulo3, 11f, Font.BOLD);

                    BaseFont _subtitulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo = new iTextSharp.text.Font(_subtitulo, 12f);

                    BaseFont _subtitulo2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo2 = new iTextSharp.text.Font(_subtitulo2, 10f, Font.BOLD);

                    BaseFont _subtitulo3 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo3 = new iTextSharp.text.Font(_subtitulo3, 12f);

                    BaseFont _subtitulo4 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, true);
                    Font subtitulo4 = new iTextSharp.text.Font(_subtitulo3, 10f);

                    BaseFont _footer = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font footer = new iTextSharp.text.Font(_footer, 10f, Font.BOLD);

                    BaseFont _parrafor = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo = new iTextSharp.text.Font(_parrafor, 9f, Font.NORMAL);
                    BaseFont _Uparrafor = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font Uparrafo = new iTextSharp.text.Font(_Uparrafor, 9f, Font.UNDERLINE);

                    BaseFont _parrafor2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo2 = new iTextSharp.text.Font(_parrafor2, 11f, Font.NORMAL);

                    BaseFont _parrafor3 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo3 = new iTextSharp.text.Font(_parrafor3, 8f, Font.NORMAL);

                    BaseFont _parrafor4 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo4 = new iTextSharp.text.Font(_parrafor4, 5f, Font.NORMAL);

                    BaseFont _espacio = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font espacio = new iTextSharp.text.Font(_espacio, 10f, Font.NORMAL, BaseColor.WHITE);

                    BaseFont _espacio2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font espacio2 = new iTextSharp.text.Font(_espacio2, 5f, Font.NORMAL, BaseColor.WHITE);


                    Image logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/santa_lucia.png"));
                    logo.ScalePercent(40f);

                    Image circle_white = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/circle_white.png"));
                    circle_white.ScalePercent(1.5f);

                    Image circle_dark = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/circle_dark.png"));
                    circle_dark.ScalePercent(3.2f);

                    Image flechas = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/flechas.png"));
                    flechas.ScalePercent(14f);

                    Image flecha = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/flecha.png"));
                    flecha.ScalePercent(16f);

                    Image cuadros = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/cuadros.PNG"));
                    cuadros.ScalePercent(35f);

                    Image cuadros2 = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/cuadros2.PNG"));
                    cuadros2.ScalePercent(35f);

                    Image casilla = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/casilla.png"));
                    casilla.ScalePercent(2f);

                    Image casilla_check = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/casilla_check.png"));
                    casilla_check.ScalePercent(2f);

                    if (ddsexo.SelectedIndex == 0)
                    {
                        ddsexo.Text = "";
                    }

                    string dia = DateTime.Today.Day.ToString();
                    int mes = DateTime.Today.Month;
                    string year = DateTime.Today.Year.ToString();
                    string nombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mes);

                    //HEADER//

                    var tb9 = new PdfPTable(new float[] { 60f, 40f }) { WidthPercentage = 100f, HorizontalAlignment = Element.ALIGN_LEFT };
                    tb9.AddCell(new PdfPCell(logo) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    if (txtsexo2.Text == "FEMENINO")
                    {
                        tb9.AddCell(new PdfPCell(new Phrase("  NOMBRE DEL PACIENTE \n\n " + txtpaciente2.Text.ToUpper() + " \n\n SEXO: F:  X   M:_____      EDAD: " + txtedad.Text.ToUpper() + " años", parrafo)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    }
                    else if (txtsexo2.Text == "MASCULINO")
                    {
                        tb9.AddCell(new PdfPCell(new Phrase("  NOMBRE DEL PACIENTE \n\n " + txtpaciente2.Text.ToUpper() + " \n\n SEXO: F:_____ M:  X         EDAD: " + txtedad2.Text.ToUpper() + " años", parrafo)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    }

                    tb9.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb9.AddCell(new PdfPCell(new Phrase("REGISTRO DE ANESTESIA Y RECUPERACIÓN", titulo2)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb9.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb9.AddCell(new PdfPCell(new Phrase("_________________________________________________________________________________________________________", parrafo)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb9.AddCell(new PdfPCell(new Phrase("  FECHA: " + DateTime.Now.ToString("dd/MM/yyyy"), parrafo2)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb9.AddCell(new PdfPCell(new Phrase("     CAMA: " + txtcama2.Text.ToString(), parrafo2)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });


                    documento.Add(tb9);

                    var tb10 = new PdfPTable(new float[] { 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f }) { WidthPercentage = 97.5f };

                    //0

                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("15", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("30", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("45", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("60", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("15", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("30", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("45", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("60", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("15", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("30", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("45", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("60", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //1

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("02", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //2

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    //3

                    tb10.AddCell(new PdfPCell(new Phrase("SpO2", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    //4

                    tb10.AddCell(new PdfPCell(new Phrase("TEMP\n   260", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    //5

                    tb10.AddCell(new PdfPCell(new Phrase("TEMP\n   260", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    //6

                    var cell8 = new PdfPCell(flechas) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cell8.Border = PdfPCell.LEFT_BORDER;
                    tb10.AddCell(cell8);

                    tb10.AddCell(new PdfPCell(new Phrase("T_A_\n   260", parrafo4)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    //7

                    tb10.AddCell(new PdfPCell(new Phrase("PULSO\n   220", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //8

                    tb10.AddCell(new PdfPCell(new Phrase("OR\n   200", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    //9

                    tb10.AddCell(new PdfPCell(new Phrase("1.LLEG_QUIR\n   180", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //10

                    tb10.AddCell(new PdfPCell(new Phrase("2. LANEST\n   160", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //11

                    tb10.AddCell(new PdfPCell(new Phrase("3. LANET\n   140", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //12

                    tb10.AddCell(new PdfPCell(new Phrase("4. LOPER\n   120", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //13

                    tb10.AddCell(new PdfPCell(new Phrase("5. TOPER\n   100", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //14

                    tb10.AddCell(new PdfPCell(new Phrase("6. ANEST.\n   80", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //15

                    tb10.AddCell(new PdfPCell(new Phrase("7. PREC.\n   60", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //16

                    tb10.AddCell(new PdfPCell(new Phrase("\n   40", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //17

                    tb10.AddCell(new PdfPCell(new Phrase("\n   20", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //18
                    var cell10 = new PdfPCell(new Phrase("TIEMPOSTAS", parrafo4)) { Border = 0, Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cell10.Border = PdfPCell.LEFT_BORDER;
                    tb10.AddCell(cell10);
                    tb10.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { Rowspan = 2, Colspan = 36, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    var cell9 = new PdfPCell(flecha) { Border = 0, Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cell9.Border = PdfPCell.LEFT_BORDER;
                    tb10.AddCell(cell9);
                    //19
                    tb10.AddCell(new PdfPCell(new Phrase("DIAGNÓSTICO", parrafo4)) { Rowspan = 2, Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("PREOPERATORIO:", parrafo3)) { Colspan = 21, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("DURACIÓN DE ANESTESIA:", parrafo3)) { Colspan = 15, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("OPERATORIO:", parrafo3)) { Colspan = 21, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("OBSERVACIONES:", parrafo3)) { Rowspan = 18, Colspan = 15, HorizontalAlignment = Element.ALIGN_CENTER });
                    //20
                    tb10.AddCell(new PdfPCell(new Phrase("OPERACIÓN", parrafo4)) { Rowspan = 2, Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("PROPUESTA:", parrafo3)) { Colspan = 21, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("REALIZADA:", parrafo3)) { Colspan = 21, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //21
                    tb10.AddCell(new PdfPCell(new Phrase("MEDICAMENTOS", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("DOSIS", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("MÉTODO Y TÉCNICA ANESTÉSICA", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("TOTAL", parrafo4)) { Colspan = 14, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb10.AddCell(new PdfPCell(new Phrase("ANESTESIÓLOGO:    " + txtanestesiologo2.Text.ToUpper(), parrafo3)) { Colspan = 19, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("CIRUJANO:    " + txtmedico2.Text.ToUpper(), parrafo3)) { Colspan = 20, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo3)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo3)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb10.AddCell(new PdfPCell(new Phrase("\n", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });


                    documento.Add(tb10);

                    var tb11 = new PdfPTable(new float[] { 10f, 85F }) { WidthPercentage = 95f };
                    tb11.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb11.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb11.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb11.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb11.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb11.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb11.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb11.AddCell(new PdfPCell(logo) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb11.AddCell(new PdfPCell(new Phrase("VALORACIÓN PREANESTÉSICA", titulo2)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb11.AddCell(new PdfPCell(new Phrase("______________________________________________________________________________________________________", parrafo)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb11.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb11);

                    var tb12 = new PdfPTable(new float[] { 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, }) { WidthPercentage = 97.5f };
                    tb12.AddCell(new PdfPCell(new Phrase("EDAD", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("SEXO", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("ESTATURA", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("PESO", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("T_A", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("P", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("R        T", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("TEGUMENTOS", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("Hb", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("H2o", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("Rh", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("G. Sanguíneo", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("T.PROT", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb12.AddCell(new PdfPCell(new Phrase(txtedad2.Text.ToUpper(), parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase(txtsexo2.Text.ToUpper(), parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase(txttalla2.Text.ToUpper() + " cm", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase(txtpeso2.Text.ToUpper(), parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb12.AddCell(new PdfPCell(new Phrase("ANTECEDENTES ANESTÉSICOS", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("ALERGIA", parrafo4)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("DENTADURA", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("CUELLO", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("ESTADO PSÍQUICO", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("OTROS", parrafo4)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 13, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb12.AddCell(new PdfPCell(new Phrase("APARATO RESPIRATORIO", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { Colspan = 10, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("APARATO CARDIOVASCULÁR", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { Colspan = 10, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("ORINA", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("DENSIDAD                                 ALBUMINA                       CILINDROS                       HEMATURA                       BILIRRUBINA                       GLUCOSA                       ACETONA", parrafo4)) { Colspan = 10, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("QUÍMICA SANGUÍNEA", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("UREA                            CREATINA                  GLUCOSA                  ALBUMINA                  GLOBULINA              PO2      PCO2       SAT%      Hb     Pn      K        Cl      Na", parrafo4)) { Colspan = 10, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("MEDICAMENTOS PREVIOS", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase(" ", parrafo4)) { Colspan = 10, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("R.A.Q.", parrafo4)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("   E               1               A               \n\n   U                               B   ", parrafo4)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("   E               2               A               \n\n   U                               B   ", parrafo4)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("   E               3               A               \n\n   U                               B   ", parrafo4)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("   E               4               A               \n\n   U                               B   ", parrafo4)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("   E               5               A               \n\n   U                               B   ", parrafo4)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("COMPLICACIONES ANESTÉSICAS", parrafo3)) { Colspan = 13, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n\n\n\n\n", parrafo4)) { Colspan = 13, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("COMPLICACIONES POSTANESTÉSICAS", parrafo3)) { Colspan = 13, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n\n\n\n\n", parrafo4)) { Colspan = 13, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n\n", parrafo4)) { Colspan = 13, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb12.AddCell(new PdfPCell(new Phrase("VALORACIÓN Y RECUPERACIÓN", subtitulo)) { Rowspan = 2, Colspan = 7, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("QUIRÓFANO", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("SALA DE RECUPERACIÓN", parrafo4)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("AL SALIR", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("0 MIN", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("20 MIN", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("60 MIN", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("90 MIN", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("120 MIN", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb12.AddCell(new PdfPCell(new Phrase("ACTIVIDAD MUSCULAR", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\nMOVIMIENTOS VOLUNTARIOS (4 EXTREMIDADES)=2 \nMOVIMIENTOS VOLUNTARIOS(2 EXTREMIDADES)=1 \nCOMPLETAMENTE INMOVIL     =0 \n", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb12.AddCell(new PdfPCell(cuadros) { Rowspan = 5, Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2f });

                    tb12.AddCell(new PdfPCell(new Phrase("RESPIRACION", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\nRESPIRACIONES AMPLIAS Y PACAZ DE TOSER=2 \nRESPIRACIONES LIMITADAS Y TOS DEBIL=1 \nAPNEA     =0 \nFRECUENCIA =F) \n", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb12.AddCell(new PdfPCell(new Phrase("CIRCULACIÓN", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\nTENSIÓN ARTERIAL=6 20/DE CIFRAS DE CONTROL=2 \nTENSIÓN ARTERIAL=6 20-50/ DE CIFRAS DE CONTROL=1 \nTENSIÓN ARTERIAL=6 50/DE CIFRAS DE CONTROL=0 \n(FRECUENCIA DE PULSO = P) Y (TENSIÓN ARTERIAL)=(TA) \n", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb12.AddCell(new PdfPCell(new Phrase("ESTADO DE CONCIENCIA", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\n OMPLETAMENTE DESPIERTO=2 \nRESPONDE AL SER LLAMADO=1 \nNO RESPONDE=0 \n", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb12.AddCell(new PdfPCell(new Phrase("COLORACIÓN", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("\nMUCOSAS SONROSADAS=2 \nPALIDAS=1 \nCIANOSIS=0 \n", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb12.AddCell(new PdfPCell(new Phrase("ALTA DE PISO \nMÉDICO REPONSABLE", parrafo3)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("TOTAL", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(cuadros2) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2f });

                    tb12.AddCell(new PdfPCell(new Phrase(".\n\n\n\n\n\n\n\n\n\n\n\n", espacio)) { Border = 0, Colspan = 13, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    tb12.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102, Col. Morelos, Toluca, Estado de México, C.P. 50120", footer)) { Colspan = 13, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb12.AddCell(new PdfPCell(new Phrase("(722) 719 89 75 / 705 07 59", footer)) { Colspan = 13, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    documento.Add(tb12);

                    var tb13 = new PdfPTable(new float[] { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, }) { WidthPercentage = 100f };
                    tb13.AddCell(new PdfPCell(logo) { Colspan = 8, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(new Phrase("LISTA DE VERIFICACIÓN CIRUGÍA SEGURA", titulo2)) { Colspan = 8, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb13.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 8, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb13.AddCell(new PdfPCell(new Phrase("Nombre del Paciente: \n\n" + txtpaciente2.Text.ToUpper() + "\n\n\n", parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(new Phrase("Fecha de Nacimiento: \n\n" + txtfecha2.Text.ToUpper() + "\n\n\n", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(new Phrase("Fecha de Elaboración:  \n\n" + DateTime.Now.ToString("dd/MM/yyyy") + "\n\n\n", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT });

                    tb13.AddCell(new PdfPCell(new Phrase("Diagnóstico: \n\n" + txtdiagnostico2.Text.ToUpper() + "\n\n\n", parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(new Phrase("Procedimiento Quirúrgico: \n\n" + txtprocedimiento2.Text.ToUpper() + "\n\n\n", parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT });

                    tb13.AddCell(new PdfPCell(new Phrase("ENTRADA (ANTES DE LA INDUCCIÓN DE LA ANESTESIA)", footer)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    var cell20 = new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f };
                    cell20.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell20);

                    tb13.AddCell(new PdfPCell(new Phrase("El paciente ha confirmado:", parrafo3)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    var cell21 = new PdfPCell(new Phrase("Demarcación del sitio / No procede", parrafo3)) { Border = 0, Colspan = 3, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell21.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell21);

                    var cell23 = new PdfPCell(new Phrase("     - Su identidad", parrafo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell23.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell23);
                    tb13.AddCell(new PdfPCell(new Phrase("- El sitio quirúrgico", parrafo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    var cell22 = new PdfPCell(new Phrase("Se ha completado el control de la seguridad de la anestesia", parrafo3)) { Border = 0, Colspan = 3, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell22.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell22);

                    var cell24 = new PdfPCell(new Phrase("     - Su procedimiento", parrafo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell24.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell24);
                    tb13.AddCell(new PdfPCell(new Phrase("- Su consentimiento", parrafo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    var cell25 = new PdfPCell(new Phrase("Pulsioximetro colocado y en funcionamiento", parrafo3)) { Border = 0, Colspan = 3, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell25.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell25);

                    tb13.AddCell(new PdfPCell(new Phrase("", espacio)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    var cell26 = new PdfPCell(new Phrase("     ¿Tiene el paciente alergias conocidas?", parrafo3)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell26.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell26);

                    var cell27 = new PdfPCell(new Phrase("     Vía aérea difícil / Riesgo de aspiración", parrafo3)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell27.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell27);

                    var cell28 = new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f };
                    cell28.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell28);
                    tb13.AddCell(new PdfPCell(new Phrase("No", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    tb13.AddCell(new PdfPCell(new Phrase("Si", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    tb13.AddCell(new PdfPCell(new Phrase("No", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    var cell29 = new PdfPCell(new Phrase("Si, hay instrumental y equipos / ayuda disponible", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell29.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell29);
                    var cell30 = new PdfPCell(new Phrase("   Cuáles: ___________________________________ ", parrafo3)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell30.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell30);
                    var cell31 = new PdfPCell(new Phrase("\n\n\n", parrafo3)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell31.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell31);

                    tb13.AddCell(new PdfPCell(new Phrase("", espacio)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    var cell32 = new PdfPCell(new Phrase("   Riesgo de hemorragia > 500 ml(7ml / kg en niños)", parrafo3)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell32.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell32);
                    tb13.AddCell(cell31);

                    var cell33 = new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f };
                    cell33.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell33);
                    tb13.AddCell(new PdfPCell(new Phrase("No", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    var cell34 = new PdfPCell(new Phrase("Si, y se ha previsto la disponibilidad de acceso intravenoso y líquidos adecuados\n\n", parrafo3)) { Colspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell34.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell34);
                    tb13.AddCell(new PdfPCell(new Phrase("PAUSA QUIRÚRGICA (ANTES DE LA INCISIÓN CUTÁNEA)", footer)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    var cell35 = new PdfPCell(casilla) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f };
                    cell35.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell35);
                    tb13.AddCell(new PdfPCell(new Phrase("Confirmar que todos los miembros del equipo de hayan presentado por su nombre y función", parrafo3)) { Rowspan = 2, Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    var cell355 = new PdfPCell(casilla) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f };
                    cell355.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell355);
                    var cell36 = new PdfPCell(new Phrase("Cirujano, anestesista y enfermo confirman verbalmente:", parrafo3)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell36.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell36);
                    tb13.AddCell(new PdfPCell(new Phrase("\n\n         - La identidad del paciente\n         - El sitio quirúrgico\n         - El procedimiento\n\n", parrafo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    var cell38 = new PdfPCell(new Phrase("", parrafo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell38.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell38);
                    tb13.AddCell(new PdfPCell(new Phrase("", espacio)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    var cell40 = new PdfPCell(new Phrase("\n\n   Previsión de eventos críticos:", parrafo3)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell40.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell40);
                    var cell400 = new PdfPCell(new Phrase("", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell400.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell400);
                    var cell41 = new PdfPCell(new Phrase("\n\n   ¿Se ha administrado profilaxis antibiótica en los últimos 60 minutos? \n\n\n\n", parrafo3)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell41.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell41);

                    var cell42 = new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f };
                    cell42.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell42);
                    var cell43 = new PdfPCell(new Phrase("EL CIRUJANO REVISA: Los pasos críticos o imprevistos, la duración de la operación y la pérdida de sangre prevista \n\n\n", parrafo3)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell43.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell43);
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    tb13.AddCell(new PdfPCell(new Phrase("Si \n\n\n", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    var cell44 = new PdfPCell(new Phrase("No procede \n\n\n", parrafo3)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell44.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell44);

                    var cell45 = new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f };
                    cell45.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell45);
                    var cell46 = new PdfPCell(new Phrase("EL EQUIPO DE ANESTESIA REVISA: Si el paciente presenta algún problema específico \n\n\n", parrafo3)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell46.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell46);
                    var cell47 = new PdfPCell(new Phrase("", parrafo3)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell47.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell47);

                    tb13.AddCell(new PdfPCell(new Phrase("", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb13.AddCell(new PdfPCell(new Phrase("", espacio)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    var cell48 = new PdfPCell(casilla) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f };
                    cell48.Border = PdfPCell.LEFT_BORDER;
                    tb13.AddCell(cell48);
                    var cell49 = new PdfPCell(new Phrase("EL EQUIPO DE ENFERMERÍA REVISA: Si se ha confirmado la esterilidad (con resultados de los indicadores) y si existen dudas o problemas relacionados con el instrumental y los equipos \n\n\n", parrafo3)) { Rowspan = 2, Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell49.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell49);
                    var cell50 = new PdfPCell(new Phrase("\n\n   ¿Pueden visualizarse las imágenes diagnósticas escenciales? \n\n\n\n", parrafo3)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell50.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell50);
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    tb13.AddCell(new PdfPCell(new Phrase("Si \n\n\n", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb13.AddCell(new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f });
                    var cell51 = new PdfPCell(new Phrase("No procede \n\n\n", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell51.Border = PdfPCell.RIGHT_BORDER;
                    tb13.AddCell(cell51);
                    tb13.AddCell(new PdfPCell(new Phrase("", espacio)) { Colspan = 8, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb13.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 8, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb13.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 8, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb13.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 8, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb13.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102, Col. Morelos, Toluca, Estado de México, C.P. 50120", footer)) { Colspan = 8, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb13.AddCell(new PdfPCell(new Phrase("(722) 719 89 75 / 705 07 59", footer)) { Colspan = 8, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb13);


                    var tb14 = new PdfPTable(new float[] { 5f, 45f, 5f, 45f }) { WidthPercentage = 100f };
                    tb14.AddCell(new PdfPCell(logo) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("SALIDA (ANTES DE QUE EL PACIENTE SALGA DE QUIRÓFANO)", footer)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    var cell52 = new PdfPCell(casilla) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f };
                    cell52.Border = PdfPCell.LEFT_BORDER;
                    tb14.AddCell(cell52);
                    var cell53 = new PdfPCell(new Phrase("El enfermo confirma verbalmente con el equipo \n\n", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell53.Border = PdfPCell.RIGHT_BORDER;
                    tb14.AddCell(cell53);
                    var cell55 = new PdfPCell(casilla) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2f, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cell55.Border = PdfPCell.LEFT_BORDER;
                    tb14.AddCell(cell55);
                    var cell54 = new PdfPCell(new Phrase("El cirujano, el anestesista y el enfermero revisan los principales aspectos de la recuperación y el tratamiento del paciente", parrafo3)) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cell54.Border = PdfPCell.RIGHT_BORDER;
                    tb14.AddCell(cell54);
                    var cell56 = new PdfPCell(new Phrase("   - El nombre del procedimiento realizado \n\n   - Que los recuentos de instrumentos, gasas y agujas son correctos (o no    proceden)\n\n   - El etiquetado de las muestras (que figuren el nombre del paciente)\n\n   - Si hay problemas que resolver relacionados con el instrumental y los    equipos", parrafo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell56.Border = PdfPCell.LEFT_BORDER;
                    tb14.AddCell(cell56);
                    tb14.AddCell(new PdfPCell(new Phrase("OBSERVACIONES", footer)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("\n\n", footer)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("\n\n", footer)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("\n\n", footer)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("\n\n", footer)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("\n\n", footer)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("\n\n", footer)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("\n\n", footer)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb14.AddCell(new PdfPCell(new Phrase("Nombre y Firma del Médico Cirujano \n\n\n\n\n\n\n" + txtmedico2.Text.ToString() + "\n\n", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("Nombre y Firma del Médico Anestesiólogo \n\n\n\n\n\n\n" + txtanestesiologo2.Text.ToString() + "\n\n", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("Nombre y Firma del Médico Ayudante No.1 \n\n\n\n\n\n\n" + txtayudante1.Text.ToString() + "\n\n", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("Nombre y Firma del Médico Ayudante No.2 \n\n\n\n\n\n\n" + txtayudante2.Text.ToString() + "\n\n", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("Nombre y Firma de la Enfermera Circulante o Coordinador de lista \n\n\n\n\n\n\n" + txtcirculante.Text.ToString() + "\n\n", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("Nombre y Firma del Instrumentista \n\n\n\n\n\n\n" + txteinstrumentista.Text.ToString() + "\n\n", parrafo3)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102, Col. Morelos, Toluca, Estado de México, C.P. 50120", footer)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase("(722) 719 89 75 / 705 07 59", footer)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb14.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });




                    documento.Add(tb14);


                    var tb15 = new PdfPTable(new float[] { 45f, 45f }) { WidthPercentage = 90f };
                    tb15.AddCell(new PdfPCell(new Phrase("FECHA: " + DateTime.Now.ToString("dd/MM/yyyy") + " ", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb15.AddCell(new PdfPCell(new Phrase("CIRUJANO: " + txtmedico2.Text.ToUpper() + " ", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb15.AddCell(new PdfPCell(new Phrase("CIRUGÍA: " + txtprocedimiento2.Text.ToString() + " ", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb15.AddCell(new PdfPCell(new Phrase("ENF. INSTRUMENTISTA: " + txteinstrumentista.Text.ToString(), parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb15.AddCell(new PdfPCell(new Phrase("PACIENTE: " + txtpaciente2.Text.ToString() + " ", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb15.AddCell(new PdfPCell(new Phrase("ENF. CIRCULANTE: " + txtcirculante.Text.ToString(), parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb15.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    documento.Add(tb15);

                    var tb16 = new PdfPTable(new float[] { 35f, 10f, 35f, 10f }) { WidthPercentage = 90f };
                    tb16.AddCell(new PdfPCell(new Phrase("SOLUCIONES", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("CANTIDAD", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("MATERIAL", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("CANTIDAD", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("FISIOLÓGICA 100 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("UNIFORME QX", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("FISIOLÓGICA 250 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("BATA C/TOALLA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("FISIOLÓGICA 500 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("KIT EXTRA(BATA QX, GUANTES, CEPILLO)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("SALINA BALANCEADA 250 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("KIT QX(GORRA, BOTAS, CUBREBOCA)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("SALINA BALANCEADA 500 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("KIT PACIENTE (BATA, GORRO, BOTAS)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("AGUA INY. AMPULA 10 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("CEPILLO JABÓN", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("IRRIGACIÓN 1000 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("CEPILLO ISODINE", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("GLUCOSA 250 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PAQ. GASA 7x5 C/5", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("MIXTA 500 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PAQ. GASA 10x10 C/5", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("HARTMAN 500 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GASA NO TEJIDA 10x10 C/5", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("MANITOL", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GASA NO TEJIDA 10x10 C/10", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PAQ. HISOPOS C/10", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("VENOSET", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PAQ. HISOPOS CON ISODINE", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("PUNZO 19G", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("FLECHAS MEROCEL (LASIK)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("PUNZO 24G", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("FLECHAS ULTRACEL", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("LLAVE DE 3 VÍAS", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GUANTES", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("PUNTA NASAL", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GUANTES  #6           #6 1/2", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("CANULA ENDOTRAQUEAL # ___________ \n\n", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GUANTES  #6           #7 1/2", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("CANULA GUEDEL (VDE) (AMA) (ROJA) (AZUL)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GUANTES  #8", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("SONDA NELATON # ___________ \n\n", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GUANTES SIN TALCO #6 1/2", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("ELECTRODOS", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GUANTES SIN TALCO #7 1/2", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("JERINGAS / AGUJAS", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GUANTES NITRILO MEDIANO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("JERINGA INSULINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GUANTES EXPLORACIÓN", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("JERINGA 3CC", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GUANTES EXPLOR. BAJO EN POLVO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("JERINGA 5CC", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("CUCHILLETES", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("JERINGA 10CC", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("CRECENT", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("JERINGA 20CC", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("15 GRADOS", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("AGUJA AMA #20         VDE #21", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("2.8        3.0       3.2", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("AGUJA NEG #20         AZUL #21", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("19GA           20GA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("AGUJA NAR #20         GRIS #21", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("19GA           20GA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("CASSETTES", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("HOJA DE BISTURI  #11           #15", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });


                    tb16.AddCell(new PdfPCell(new Phrase("CONSTELATION COMBINADO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("CONSTELATION FACO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("LÍQUIDOS PRESADOS", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("LAUREATE FACO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("SILICÓN 5000", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("OTRO:", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("EXOPLANTE TYPE:", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("TEGADERM 6x7", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("GAS C3F8", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("TEGADERM 10x12", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("AVASTIN DOSIS", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("BULTO CHICO LASIK", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("RIBOFLAVINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("BULTO CX MENOR", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("LENTE DE CONTACTO TERAPÉUTICO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("BULTO CX MAYOR(MAURO)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PROTECTOR OFT. SENCILLO      DOBLE", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("BULTO CX MAYOR(LEYVA)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("AZUL TRIPAN (UNIDADES)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("CAMPO FENESTRADO 60x60", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("AZUL BRILLANTE (UNIDADES)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("CAMPO PLANO 60x60", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("MARCADOR VIOLETA DE GENCIANA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(logo) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    tb16.AddCell(new PdfPCell(new Phrase("MATERIAL", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("CANTIDAD", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("MEDICAMENTOS", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("CANTIDAD", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("PUNTA CURVA(GRIS)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PISACAINA 1%", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("PUNTA I/A SILICÓN ANGULADA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PISACAINA 2%", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("CANULA CHARLES", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PISACAINA 2% AMPOLLETA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("PUNTA TORCIONAL 45° (MORADA)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PISACAINA C/EPINEFRINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("PUNTA RECTA (AZUL)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("BUVACAINA 7.5", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("INYECTOR LIQ. VISCOSOS", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("MIOSTAT", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("PINZA DE RETINA 23G", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("KETOROLACO AMPOLLETA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("DIATERMIA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("DEXAMETASONA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("CANULA DE AIRE", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("MIDAZOLAM 15 MG", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("KISITITOMOS", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ADRENALINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("DRENADOR CHAYET-LASIK", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("  (N)    (R)", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("METOCLOPRAMIDA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("OTROS:", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PROPOFOL 20 ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ATROPINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("USO LÁSER", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("MITOMICINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("USO OXÍGENO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ADALAT", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("USO MONITOR", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("FENTANILO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("SUTURAS", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("CEFTRIAXONA 1 GR", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("NYLON 4-0     (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("AMINOFILINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("NYLON 4-0     (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("AMINOFILINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("NYLON 5-0     (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("EFEDRINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("NYLON 6-0     (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("RANITIDINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("NYLON 10-0     (=)      (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ONDASETRON", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("VICRYL 4-0     (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("SEVORANE", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("VICRYL 5-0     (=)      (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("INSULINA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("VICRYL 6-0     (=)      (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("DEXTROSTIX", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("VICRYL 7-0     (=) ", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("OTROS:", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("SEDA 0-0 LIBRE", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("SEDA 4-0 LIBRE", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ENTES INTRAOCULARES", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("SEDA 5-0 LIBRE     (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ACRYSOF 1Q+CARTUCHO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("SEDA 6-0 LIBRE     (=)      (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("ACRYSOF NATURAL+CARTUCHO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("SEDA 7-0 LIBRE     (=)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("TECNIS+CARTUCHO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("SEDA 8-0 LIBRE     (-)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("SENSAR-CARTUCHO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("MERSILENE 5-0     (=)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("HOJA PRECARGADO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("PROLENE 5-0     (=)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("SPHERYS 204 + CARTUCHO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("PROLENE 10-0     (=)", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("FREDOM + CARTUCHO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("OTRA", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("PMMA + CARTUCHO", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("OTRO:", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("VISCOELÁSTICOS", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("BIOVISC", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("DUOVISC", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("Z-HYALIN PLUS", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("Z-HYALCOAT", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("OTROS", parrafo3)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb16.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102 Col. Morelos, Toluca, Estado de México, C.P 50120 Telefono 2325858", footer)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb16.AddCell(new PdfPCell(new Phrase("Página web: ilasermexico.com", footer)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    documento.Add(tb16);


                    documento.Close();
                    Response.Write(documento);
                    Response.End();


                }
            }
        }

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {

            Response.Write("<script>alert('El nombre del paciente es: " + txtpaciente1.Text + " ' )</script>");
            using (StringWriter sw = new StringWriter())
            {
                StringWriter sw1 = new StringWriter();

                using (HtmlTextWriter hw = new HtmlTextWriter(sw))

                {
                    HtmlTextWriter hw1 = new HtmlTextWriter(sw1);



                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=hoja_paciente.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);


                    StringReader sr = new StringReader(sw.ToString());

                    Document documento = new Document(PageSize.A4.Rotate(), 25f, 25f, 25f, 25f);
                    PdfWriter writer = PdfWriter.GetInstance(documento, Response.OutputStream);

                    documento.Open();

                    BaseFont _titulo = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo = new iTextSharp.text.Font(_titulo, 20f, Font.BOLD);

                    BaseFont _titulo2 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo2 = new iTextSharp.text.Font(_titulo2, 15f, Font.BOLD);

                    BaseFont _titulo3 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo3 = new iTextSharp.text.Font(_titulo3, 11f, Font.BOLD);

                    BaseFont _subtitulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo = new iTextSharp.text.Font(_subtitulo, 12f);

                    BaseFont _subtitulo2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo2 = new iTextSharp.text.Font(_subtitulo2, 10f, Font.BOLD);

                    BaseFont _subtitulo3 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo3 = new iTextSharp.text.Font(_subtitulo3, 12f);

                    BaseFont _subtitulo4 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, true);
                    Font subtitulo4 = new iTextSharp.text.Font(_subtitulo3, 11f);

                    BaseFont _footer = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font footer = new iTextSharp.text.Font(_footer, 10f, Font.BOLD);

                    BaseFont _parrafor = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo = new iTextSharp.text.Font(_parrafor, 9f, Font.NORMAL);

                    BaseFont _parrafor2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo2 = new iTextSharp.text.Font(_parrafor2, 11f, Font.NORMAL);

                    BaseFont _parrafor3 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo3 = new iTextSharp.text.Font(_parrafor3, 8f, Font.NORMAL);

                    BaseFont _parrafor4 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo4 = new iTextSharp.text.Font(_parrafor4, 5f, Font.NORMAL);

                    BaseFont _espacio = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font espacio = new iTextSharp.text.Font(_espacio, 10f, Font.NORMAL, BaseColor.WHITE);

                    BaseFont _espacio2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font espacio2 = new iTextSharp.text.Font(_espacio2, 5f, Font.NORMAL, BaseColor.WHITE);

                    Image logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/santa_lucia.png"));
                    logo.ScalePercent(40f);


                    //HEADER//
                    var tb1 = new PdfPTable(new float[] { 15f, 85f }) { WidthPercentage = 100f };
                    tb1.AddCell(new PdfPCell(logo) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb1.AddCell(new PdfPCell(new Phrase("                HOJA DE PACIENTE QUIRÚRGICO", titulo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });


                    documento.Add(tb1);

                    var tb2 = new PdfPTable(new float[] { 45f, 10f, 45f }) { WidthPercentage = 100f };
                    tb2.AddCell(new PdfPCell(new Phrase("FECHA: " + DateTime.Now.ToString("dd/MM/yyyy") + " ", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("NOMBRE: " + txtpaciente1.Text.ToString() + " ", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("A. PRE-OPERATORIO: " + txtdiagnostico1.Text.ToString() + " ", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(" ", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("DX:POS-OPERATORIO: _______________________________________", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("EDAD: " + txtedad1.Text.ToString() + " AÑOS.   SEXO: " + txtsexo1.Text.ToString() + " ", parrafo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });


                    documento.Add(tb2);

                    var tb3 = new PdfPTable(new float[] { 7.1f, 7.1f, 7.1f, 7.1f, 7.1f, 7.1f, 7.1f, 7.1f, 7.1f, 7.1f, 7.1f, 7.1f, 7.1f, 7.1f, }) { WidthPercentage = 99.4f };
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 14, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("HORA", parrafo3)) { Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("SIGNOS VITALES", parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("INGRESOS", parrafo3)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("EGRESOS", parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("ANESTESIA", parrafo3)) { Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("EDO. CONC.", parrafo3)) { Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Pulso", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("FR", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("SOP2", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("T/A", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("SOLUCIONES", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("ML", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("MEDICAMENTOS", parrafo4)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("VÍA/DOSIS", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("DRENES", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("VOMITO", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("SANGRE", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    //1
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //2
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //3
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //4
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //5
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //6
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //7
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //8
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //9
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //10
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //11
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //12
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //13
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //14
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //15
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //16
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //17
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    //18
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("", parrafo3)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb3.AddCell(new PdfPCell(new Phrase("OBS. PRE OPERATORIAS", parrafo3)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("OBS. TRANS-OPERATORIA", parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("OBS. POS-OPERATORIA", parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(txtenfermera1.Text.ToUpper(), parrafo3)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(txtenfermera1.Text.ToUpper(), parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(txtenfermera1.Text.ToUpper(), parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DE LA ENFERMERA", parrafo3)) { Colspan = 6, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DE LA ENFERMERA", parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DE LA ENFERMERA", parrafo3)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102 Col. Morelos, Toluca, Estado de México, C.P 50120 Telefono 2325858", footer)) { Colspan = 14, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase("Página web: ilasermexico.com", footer)) { Colspan = 14, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });



                    //tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });


                    documento.Add(tb3);



                    documento.Close();
                    Response.Write(documento);
                    Response.End();


                }
            }
        }

        protected void btnempty_Click(object sender, EventArgs e)
        {
            limpiar1();
            GVbind();
            GVbind1();
            GVbind2();
        }

        protected void insertar()
        {
            int result = 0;
            
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO ingresos_sl(paciente,fecha,edad,sexo,peso,talla,representante,medico,diagnostico,procedimiento,alergias) VALUES ('" + txtpaciente.Text + "','" + txtfecha.Text + "','" + txtedad.Text.ToUpper() + "', '" + ddsexo.Text.ToUpper() + "', '" + txtpeso.Text.ToUpper() + "', '" + txttalla.Text + "', '" + txtrepresentante.Text.ToUpper() + "', '" + DropDownList1.SelectedItem.Text.ToUpper() + "', '" + txtdiagnostico.Text.ToUpper() + "', '" + txtprocedimiento.Text.ToUpper() + "', '" + txtalergias.Text.ToUpper() + "')", con);

            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true);
                this.txtfecha.Text = string.Empty;
                this.txtedad.Text = string.Empty;
                this.txtpeso.Text = string.Empty;
                this.txttalla.Text = string.Empty;
                this.txtrepresentante.Text = string.Empty;
                this.txtdiagnostico.Text = string.Empty;
                this.txtprocedimiento.Text = string.Empty;
                this.txtalergias.Text = string.Empty;
                //GVbind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
            }
                
        }

        protected void limpiar1()
        {
            this.txtpaciente.Text = string.Empty;
            this.txtfecha.Text = string.Empty;
            this.txtedad.Text = string.Empty;
            this.txtpeso.Text = string.Empty;
            this.txttalla.Text = string.Empty;
            this.txtdomicilio.Text = string.Empty;
            this.txtcp.Text = string.Empty;
            this.txtocupacion.Text = string.Empty;
            this.txttel.Text = string.Empty;
            this.txtfamiliar.Text = string.Empty;
            this.txthoraentrada.Text = string.Empty;
            this.txtfechaentrada.Text = string.Empty;
            this.txtdxingreso.Text = string.Empty;
            this.txtestudios.Text = string.Empty;
            this.txtdiagnostico.Text = string.Empty;
            this.txtprocedimiento.Text = string.Empty;
            this.txtbeneficios.Text = string.Empty;
            this.txtpronostico.Text = string.Empty;
            this.txtalergias.Text = string.Empty;
            this.txtobservaciones.Text = string.Empty;
        }

        //[WebMethod]
        //public static List<Citas> listarCitas()
        //{
        //    List<Citas> lista = new List<Citas>();
        //    try
        //    {
        //        lista = CitasLN.getInstance().listarCitas();
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }


        //    //System.Diagnostics.Debug.WriteLine("listas");
        //    //foreach (var item in lista)
        //    //{
        //    //    System.Diagnostics.Debug.WriteLine(item.Nombre);

        //    //}
        //    return lista;

        //}

        //[WebMethod]
        //public static List<Ingresos> listarIngresos()
        //{
        //    List<Ingresos> lista1 = new List<Ingresos>();
        //    try
        //    {
        //        lista1 = IngresoLN.getInstance().listarIngresos();
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }


        //    //System.Diagnostics.Debug.WriteLine("listas");
        //    //foreach (var item in lista)
        //    //{
        //    //    System.Diagnostics.Debug.WriteLine(item.Nombre);

        //    //}
        //    return lista1;

        //}
    }
}