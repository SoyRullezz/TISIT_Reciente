using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

namespace TISIT
{
    public partial class consentimiento : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        protected void Page_Load(object sender, EventArgs e)
        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);

            GVbind();

            ddsexo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));

            //permisosGenerales();
            //perm();
        }

        protected void GVbind()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM registro_pacientes_llegada where tipo_cita = 'OPERACION'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridregistro.DataSource = dr;
                    gridregistro.DataBind();
                }
            }
        }

        protected void gridregistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtpaciente.Text = HttpUtility.HtmlDecode(gridregistro.SelectedRow.Cells[1].Text);
            txtmedico.Text = HttpUtility.HtmlDecode(gridregistro.SelectedRow.Cells[2].Text);

            gridregistro.SelectedIndex = -1;
        }

        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
            Session.Abandon();
        }

        protected void btnpdf_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                StringWriter sw1 = new StringWriter();

                using (HtmlTextWriter hw = new HtmlTextWriter(sw))

                {
                    HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=hoja_ingreso_carta_consentimiento.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);


                    StringReader sr = new StringReader(sw.ToString());

                    Document documento = new Document(PageSize.A4, 25f, 25f, 25f, 25f);
                    PdfWriter writer = PdfWriter.GetInstance(documento, Response.OutputStream);

                    documento.Open();

                    BaseFont _titulo = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo = new iTextSharp.text.Font(_titulo, 22f, Font.BOLD);

                    BaseFont _subtitulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo = new iTextSharp.text.Font(_subtitulo, 12f);

                    BaseFont _subtitulo2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo2 = new iTextSharp.text.Font(_subtitulo2, 10f, Font.BOLD);

                    BaseFont _subtitulo3 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo3 = new iTextSharp.text.Font(_subtitulo3, 14f);

                    BaseFont _footer = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font footer = new iTextSharp.text.Font(_footer, 9f, Font.BOLD);

                    BaseFont _parrafor = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo = new iTextSharp.text.Font(_parrafor, 10f, Font.NORMAL);

                    BaseFont _espacio = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font espacio = new iTextSharp.text.Font(_espacio, 10f, Font.NORMAL, BaseColor.WHITE);

                    Image logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/ilaser.png"));
                    logo.ScalePercent(60f);

                    string dia = DateTime.Today.Day.ToString();
                    int mes = DateTime.Today.Month;
                    string year = DateTime.Today.Year.ToString();
                    string nombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mes);

                    //HEADER//
                    var tb1 = new PdfPTable(new float[] { 80f }) { WidthPercentage = 80f };
                    tb1.AddCell(new PdfPCell(logo) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb1.AddCell(new PdfPCell(new Phrase("HOJA DE INGRESO", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb1);

                    if (ddsexo.SelectedIndex == 0)
                    {
                        ddsexo.Text = "";
                    }

                    var tb2 = new PdfPTable(new float[] { 80f }) { WidthPercentage = 80f };
                    tb2.AddCell(new PdfPCell(new Phrase("TOLUCA MEXICO A " + dia + " DE " + nombreMes.ToString().ToUpper() + " DEL " + year + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("NOMBRE DEL PACIENTE: " + txtpaciente.Text.ToUpper(), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("FECHA DE NACIMIENTO: " + txtfecha.Text + "    EDAD: " + txtedad.Text + "    SEXO: " + ddsexo.Text + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("ESTADO CIVIL: " + txtestado.Text.ToUpper() + "         OCUPACIÓN: " + txtocupacion.Text + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("DOMICILIO: " + txtdomicilio.Text.ToUpper() + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("C.P.: " + txtcp.Text + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("TELEFONO: " + txttel.Text + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("CORREO: " + txtcorreo.Text.ToUpper() + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("NOMBRE DEL MEDICO: " + txtmedico.Text.ToUpper() + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("HORA DE ENTRADA: " + txtentrada.Text + "   HORA DE SALIDA:__________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("ALERGIAS: " + txtalergias.Text.ToUpper() + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("OBSERVACIONES: " + txtobservaciones.Text.ToUpper() + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102 Col. Morelos, Toluca, Estado de México, C.P 50120 Telefono 2325858", footer)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase("Página web: ilasermexico.com", footer)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb2.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb2);



                    var tb3 = new PdfPTable(new float[] { 80f }) { WidthPercentage = 80f };
                    tb3.AddCell(new PdfPCell(logo) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    documento.Add(tb3);

                    var tb4 = new PdfPTable(new float[] { 80f }) { WidthPercentage = 80f };
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("Carta de consentimiento bajo información", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("Toluca, México a " + dia + " DE " + nombreMes.ToString().ToUpper() + " DEL " + year + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb4.AddCell(new PdfPCell(new Phrase("Nombre del Paciente: " + txtpaciente.Text.ToUpper(), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("Bajo protesta de decir la verdad declaro que el Dr.: " + txtmedico.Text.ToUpper(), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("Me ha explicado que mi diagnóstico es: " + txtdiagnostico.Text.ToUpper(), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("Y que por tal motivo debo someterme a (los) Siguientes Procedimientos con fines de diagnostico y/o terapéuticos: " + txtprocedimiento.Text.ToUpper(), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("Entiendo que todo acto médico diagnostico de tratamiento, sea quirúrgico o no quirúrgico, puede ocasionar una serie de complicaciones mayores o menores. Reconozco que entre los posibles riesgos y complicaciones que puedan seguir se encuentran(n): infección ametropía residual, flap incompleto, ectasia cornal.", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase("Declaro que he comprendido las explicaciones que se me han facilitado en un lenguaje claro y sencillo y el médico que me atiende me ha permitido realizar todas las observaciones y me ha aclarado todas las dudas que he planteado", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb4);


                    var tb5 = new PdfPTable(new float[] { 40f, 40f }) { WidthPercentage = 80f };
                    tb5.AddCell(new PdfPCell(new Phrase("_____________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase("_____________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb5.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL PACIENTE", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL TESTIGO", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase("_____________________________", parrafo)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL MÉDICO", parrafo)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb5.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102 Col. Morelos, Toluca, Estado de México, C.P 50120 Telefono 2325858", footer)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase("Página web: ilasermexico.com", footer)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb5);


                    var tb7 = new PdfPTable(new float[] { 80f }) { WidthPercentage = 80f };
                    tb7.AddCell(new PdfPCell(logo) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb7.AddCell(new PdfPCell(new Phrase("CARTA DE LIBERACION DE RESPONSABILIDAD", subtitulo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase("Toluca, México a " + dia + " DE " + nombreMes.ToString().ToUpper() + " DEL " + year + "", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase("Por medio de la presente DESLINDO de TODA RESPONSABILIDAD a la clínica 'iLASER METEPEC S.A. DE C.V.' de todos los procedimientos MEDICOS QUIRURGICO que se me realicen, en el entendido que toda decisión medica es responsabilidad de mi médico tratante.", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb7);

                    var tb6 = new PdfPTable(new float[] { 40f, 40f }) { WidthPercentage = 80f };
                    tb6.AddCell(new PdfPCell(new Phrase("_____________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("_____________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb6.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL PACIENTE", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL RESPONSABLE", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("_____________________________", parrafo)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("MEDICO TRATANTE", parrafo)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb6.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102 Col. Morelos, Toluca, Estado de México, C.P 50120 Telefono 2325858", footer)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb6.AddCell(new PdfPCell(new Phrase("Página web: ilasermexico.com", footer)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb6);

                    documento.Close();
                    Response.Write(documento);
                    Response.End();



                }
            }
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

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {
            this.txtpaciente.Text = string.Empty;
            this.txtfecha.Text = string.Empty;
            this.txtedad.Text = string.Empty;
            //this.ddsexo.Text = string.Empty;
            this.txtestado.Text = string.Empty;
            this.txtocupacion.Text = string.Empty;
            this.txtdomicilio.Text = string.Empty;

            this.txtcp.Text = string.Empty;
            this.txttel.Text = string.Empty;
            this.txtmedico.Text = string.Empty;
            this.txtentrada.Text = string.Empty;
            this.txtalergias.Text = string.Empty;
            this.txtdiagnostico.Text = string.Empty;
            this.txtprocedimiento.Text = string.Empty;
            this.txtobservaciones.Text = string.Empty;
        }
    }
}