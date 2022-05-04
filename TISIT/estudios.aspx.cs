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

namespace TISIT
{
    public partial class estudios : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        protected void Page_Load(object sender, EventArgs e)
        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);

            if (!Page.IsPostBack)
            {
                GVbind();
            }

            //permisosGenerales();
            //perm();
        }

        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
            Session.Abandon();
        }

        protected void GVbind()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_cita, concat(nombreCliente, ' ', apellidoCliente, ' ', apellidoMCliente) as 'nombre_paciente', concat(nombreDoctor, ' ', apellidoDoctor, ' ', maternoDoctor) as 'nombre_doctor', tipo_cita, motivo FROM recurrentes where tipo_cita = 'ESTUDIOS' AND status = 1", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridcitas.DataSource = dr;
                    gridcitas.DataBind();
                }
            }
        }

        protected void gridcitas_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtcita.Text = HttpUtility.HtmlDecode(gridcitas.SelectedRow.Cells[0].Text);
            txtpaciente.Text = HttpUtility.HtmlDecode(gridcitas.SelectedRow.Cells[1].Text);
            txtdoctor.Text = HttpUtility.HtmlDecode(gridcitas.SelectedRow.Cells[2].Text);
            txtmotivo.Text = HttpUtility.HtmlDecode(gridcitas.SelectedRow.Cells[4].Text);

            gridcitas.SelectedIndex = -1;
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
                    Response.AddHeader("content-disposition", "attachment;filename=solicitud_estudios.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //gridConsulta.RenderControl(hw);

                    StringReader sr = new StringReader(sw.ToString());

                    Document documento = new Document(PageSize.A4, 25f, 25f, 25f, 25f);
                    PdfWriter writer = PdfWriter.GetInstance(documento, Response.OutputStream);

                    documento.Open();

                    BaseFont _titulo = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, true);
                    Font titulo = new iTextSharp.text.Font(_titulo, 22f, Font.BOLD);

                    BaseFont _subtitulo = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, true);
                    Font subtitulo = new iTextSharp.text.Font(_subtitulo, 12f);

                    BaseFont _subtitulo2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo2 = new iTextSharp.text.Font(_subtitulo2, 10f, Font.BOLD);

                    BaseFont _subtitulo3 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font subtitulo3 = new iTextSharp.text.Font(_subtitulo3, 14f);

                    BaseFont _footer = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font footer = new iTextSharp.text.Font(_footer, 9f, Font.BOLD);

                    BaseFont _parrafor = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafo = new iTextSharp.text.Font(_parrafor, 10f, Font.NORMAL);

                    BaseFont _parrafou = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font parrafou = new iTextSharp.text.Font(_parrafou, 10f, Font.UNDERLINE);

                    BaseFont _espacio = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
                    Font espacio = new iTextSharp.text.Font(_espacio, 10f, Font.NORMAL, BaseColor.WHITE);


                    Image logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/santa_lucia.png"));
                    logo.ScalePercent(40f);

                    Image ojos = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/ojoso.png"));
                    ojos.ScalePercent(70f);

                    Image circle_white = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/circle_white.png"));
                    circle_white.ScalePercent(1.5f);

                    Image circle_dark = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/circle_dark.png"));
                    circle_dark.ScalePercent(3.2f);

                    Image croquis = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/santa_lucia_croquis.PNG"));
                    croquis.ScalePercent(70f);

                    Image telefono = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/telefono.png"));
                    telefono.ScalePercent(5f);

                    Image carta = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"~/assets2/images/logo/letra.png"));
                    carta.ScalePercent(5f);

                    var cell_espacio = new PdfPCell(new PdfPCell(new Phrase("")) { Border = 0, MinimumHeight = 7f, CalculatedHeight = 7f });

                    //HEADER//
                    var tb1 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                    tb1.AddCell(new PdfPCell(new Phrase("Solicitud de estudios", titulo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb1.AddCell(cell_espacio);
                    tb1.AddCell(cell_espacio);
                    documento.Add(tb1);

                    string dia = DateTime.Today.Day.ToString();
                    string mes = DateTime.Today.Month.ToString();
                    string year = DateTime.Today.Year.ToString();

                    ////HEADER 2//
                    var tb2 = new PdfPTable(new float[] { 25f, 30f, 10f, 35f }) { WidthPercentage = 100f };
                    tb2.AddCell(new PdfPCell(logo) { Rowspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb2.AddCell(new PdfPCell(ojos) { Rowspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb2.AddCell(new PdfPCell(new Phrase("Fecha: " + dia + "/" + mes + "/" + year + "", subtitulo)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });

                    if (ddestudio.SelectedValue == "0")
                    {
                        tb2.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb2.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb2.AddCell(new PdfPCell(new Phrase("Sin interpretación", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (ddestudio.SelectedValue == "1")
                    {
                        tb2.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb2.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb2.AddCell(new PdfPCell(new Phrase("Urgente", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (ddestudio.SelectedValue == "2")
                    {
                        tb2.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb2.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb2.AddCell(new PdfPCell(new Phrase("Con interpretación", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    tb2.AddCell(new PdfPCell(new Phrase("Especificar zona de prioridad", parrafo)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });

                    documento.Add(tb2);

                    tb1.AddCell(cell_espacio);

                    var tb3 = new PdfPTable(new float[] { 20f, 60f, 20f }) { WidthPercentage = 100f };
                    tb3.AddCell(new PdfPCell(new Phrase("Paciente: ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(HttpUtility.HtmlDecode(txtpaciente.Text).ToUpper(), parrafou)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_LEFT });

                    tb3.AddCell(new PdfPCell(new Phrase("Dr: ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(HttpUtility.HtmlDecode(txtdoctor.Text).ToUpper(), parrafou)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb3.AddCell(new PdfPCell(new Phrase("Diagnóstico: ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(HttpUtility.HtmlDecode(txtmotivo.Text).ToUpper(), parrafou)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb3.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    documento.Add(tb3);


                    var tb4 = new PdfPTable(new float[] { 5f, 5f, 40f, 5f, 40f }) { WidthPercentage = 95f };

                    tb4.AddCell(new PdfPCell(new Phrase("Córnea Cirugía Refractiva", subtitulo)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb4.AddCell(new PdfPCell(new Phrase("Giaucoma", subtitulo)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb4.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 5, Border = 0 });


                    tb4.AddCell(new PdfPCell(new Phrase("1)", subtitulo)) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    if (cb1.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Topografía óptica con cámara de Scheimpflug y anillos de placido (Sirius)", parrafo)) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cbg1.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Campos Visuales (Humphery 860)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb4.AddCell(new PdfPCell(new Phrase("  OD__________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb4.AddCell(new PdfPCell(new Phrase("  OI___________________________", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    tb4.AddCell(new PdfPCell(new Phrase("1)", subtitulo)) { Rowspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    if (cb2.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Topografía óptica con doble cámara de Scheimpflug y anillos de placido(Galilei)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    if (cbg2.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("OCT Nervio Óptico (spectrallis)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cb3.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Microscopia Especular (CSO)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    if (cbg3.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Paquimetría Óptica con camara de Scheimpflug", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cb4.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Paquimetría Óptica con camara de Scheimpflug", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    if (cbg4.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Microscopia Especular", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cb5.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Ultrabiomicroscópía-UBM Quantec Medical Aviso)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    if (cbg5.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Ultrabiomicroscópía-UBM Quantec Medical Aviso)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cb6.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Ecografía MODO B(Quantec Medical aviso)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    if (cbg6.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Ecografía MODO B nervio óptico(Quantec Medical aviso)", parrafo)) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cb7.Checked)
                    {
                        tb4.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb4.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb4.AddCell(new PdfPCell(new Phrase("Topografía y aberrometria ORBSCAN lll", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });


                    documento.Add(tb4);

                    var tb5 = new PdfPTable(new float[] { 5f, 5f, 40f, 5f, 40f }) { WidthPercentage = 95f };

                    tb5.AddCell(new PdfPCell(new Phrase("Retina", subtitulo)) { Colspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb5.AddCell(new PdfPCell(new Phrase("Segmento Anterior", subtitulo)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb5.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 5, Border = 0 });

                    tb5.AddCell(new PdfPCell(new Phrase("2)", subtitulo)) { Rowspan = 6, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    if (cbr1.Checked)
                    {
                        tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb5.AddCell(new PdfPCell(new Phrase("Fluorangiografía de retina(VISUCAM 500)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    if (cbs1.Checked)
                    {
                        tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb5.AddCell(new PdfPCell(new Phrase("Ultrasonido Modo B(Quantec Medical aviso)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cbr2.Checked)
                    {
                        tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb5.AddCell(new PdfPCell(new Phrase("OCT Retina (spectrallis)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    if (cbs2.Checked)
                    {
                        tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb5.AddCell(new PdfPCell(new Phrase("Ecografía Modo B(Quantec Medical aviso)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cbr3.Checked)
                    {
                        tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb5.AddCell(new PdfPCell(new Phrase("OCT Mácula (spectrallis)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    if (cbs3.Checked)
                    {
                        tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb5.AddCell(new PdfPCell(new Phrase("Microscopia Especular (CSO)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cbr4.Checked)
                    {
                        tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb5.AddCell(new PdfPCell(new Phrase("Fotografía polo posterior (VISUCAM 500)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    if (cbs4.Checked)
                    {
                        tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb5.AddCell(new PdfPCell(new Phrase("Ultrabiomicroscopía-UBM Quantec Medical Aivo)", parrafo)) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cbr5.Checked)
                    {
                        tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb5.AddCell(new PdfPCell(new Phrase("Ultrabiomicroscopía-UBM Quantec Medical Aviso", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    //tb5.AddCell(new PdfPCell(new Phrase(".", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb5.AddCell(new PdfPCell(new Phrase(" ", parrafo)) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    //tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //tb5.AddCell(new PdfPCell(new Phrase("Ecografía MODO B (Quantec Medical aviso)", parrafo)) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    if (cbr6.Checked)
                    {
                        tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb5.AddCell(new PdfPCell(new Phrase("Ecografía MODO B (Quantec Medical aviso)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb5.AddCell(new PdfPCell(new Phrase(".", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb5.AddCell(new PdfPCell(new Phrase(".", parrafo)) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    //tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //tb5.AddCell(new PdfPCell(new Phrase("Ecografía MODO B (Quantec Medical aviso)", parrafo)) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    documento.Add(tb5);

                    //if (cbr6.Checked)
                    //{
                    //    tb5.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}
                    //else
                    //{
                    //    tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}
                    //tb5.AddCell(new PdfPCell(new Phrase("Ecografía MODO B (Quantec Medical aviso)", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    //tb5.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //tb5.AddCell(new PdfPCell(new Phrase(".", parrafo)) { Rowspan = 3, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    //documento.Add(tb5);


                    var tb6 = new PdfPTable(new float[] { 10f, 15f, 15f, 10f }) { WidthPercentage = 50f, HorizontalAlignment = Element.ALIGN_LEFT };

                    tb6.AddCell(new PdfPCell(new Phrase("Laser", subtitulo)) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb6.AddCell(new PdfPCell(new Phrase(".", espacio)) { Colspan = 4, Border = 0 });

                    if (cbl1.Checked)
                    {
                        tb6.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb6.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb6.AddCell(new PdfPCell(new Phrase("Láser argón", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb6.AddCell(new PdfPCell(new Phrase("TP", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    if (cbl4.Checked)
                    {
                        tb6.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    }
                    else
                    {
                        tb6.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    }

                    if (cbl2.Checked)
                    {
                        tb6.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb6.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb6.AddCell(new PdfPCell(new Phrase("Láser YAG", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb6.AddCell(new PdfPCell(new Phrase("PIL", parrafo)) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    if (cbl5.Checked)
                    {
                        tb6.AddCell(new PdfPCell(circle_dark) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    }
                    else
                    {
                        tb6.AddCell(new PdfPCell(circle_white) { Rowspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    }

                    if (cbl3.Checked)
                    {
                        tb6.AddCell(new PdfPCell(circle_dark) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else
                    {
                        tb6.AddCell(new PdfPCell(circle_white) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    tb6.AddCell(new PdfPCell(new Phrase("Láser SLT", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    documento.Add(tb6);



                    var tb7 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });

                    tb7.AddCell(new PdfPCell(new Phrase("Notas: ", parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(txtnotas.Text, parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase(".", espacio)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    tb7.AddCell(new PdfPCell(new Phrase("1) Retirar lente de contacto rígido", parrafo)) { Border = 0 });
                    tb7.AddCell(new PdfPCell(new Phrase("2) No consumir lácteos ni huevo 24 hrs antes de su estudio, asistir en ayunas  y con un acompañante", parrafo)) { Border = 0 });


                    documento.Add(tb7);

                    var tb8 = new PdfPTable(new float[] { 30f, 70f }) { WidthPercentage = 100f };
                    tb8.AddCell(new PdfPCell(croquis) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb8.AddCell(new PdfPCell(new Phrase("HORARIOS", subtitulo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb8.AddCell(new PdfPCell(new Phrase("Lunes a Viernes 9am a 6pm", subtitulo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb8.AddCell(new PdfPCell(new Phrase("Sábado 9am a 1pm", subtitulo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb8.AddCell(new PdfPCell(new Phrase("PREVIA CITA", subtitulo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb8.AddCell(new PdfPCell(telefono) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tb8.AddCell(new PdfPCell(new Phrase("     (722)719 8975 / 705 0759", subtitulo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb8.AddCell(new PdfPCell(new Phrase("Felipe Villanueva #1102 Col. Morelos,", subtitulo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb8.AddCell(new PdfPCell(new Phrase("Toluca, Estado de México, C.P.50120", subtitulo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tb8.AddCell(new PdfPCell(carta) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tb8.AddCell(new PdfPCell(new Phrase("clinicasantaluciatoluca@gmail.com", subtitulo3)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tb8.AddCell(new PdfPCell(new Phrase("¡Contamos con estacionamiento propio!", subtitulo3)) { Colspan = 2, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });

                    documento.Add(tb8);

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
            this.txtcita.Text = string.Empty;
            this.txtdoctor.Text = string.Empty;
            this.txtnotas.Text = string.Empty;
            this.txtmotivo.Text = string.Empty;

            cb1.Checked = false;
            cb2.Checked = false;
            cb3.Checked = false;
            cb4.Checked = false;
            cb5.Checked = false;
            cb6.Checked = false;
            cb7.Checked = false;

            cbg1.Checked = false;
            cbg2.Checked = false;
            cbg3.Checked = false;
            cbg4.Checked = false;
            cbg5.Checked = false;
            cbg6.Checked = false;

            cbr1.Checked = false;
            cbr2.Checked = false;
            cbr3.Checked = false;
            cbr4.Checked = false;
            cbr5.Checked = false;
            cbr6.Checked = false;

            cbs1.Checked = false;
            cbs2.Checked = false;
            cbs3.Checked = false;
            cbs4.Checked = false;

            cbl1.Checked = false;
            cbl2.Checked = false;
            cbl3.Checked = false;
            cbl4.Checked = false;
            cbl5.Checked = false;
        }
    }
}