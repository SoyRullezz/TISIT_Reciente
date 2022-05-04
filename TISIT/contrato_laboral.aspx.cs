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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using System.Net.Mail;
using System.Drawing;
using System.Data.OleDb;
using System.Globalization;
using System.Net;
using iTextSharp.text.pdf.draw;
using Image = iTextSharp.text.Image;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace TISIT
{
    public partial class contrato_laboral : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        protected void Page_Load(object sender, EventArgs e)

        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);
            //if (Session["usuario"] == null) { Response.Redirect("Contact.aspx"); }
            //lbldetalles.Text = "Usuario:" + "" + Session["usuario"];
            //lblfecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //lblhora.Text = DateTime.Now.ToString("HH:mm");

            if (!IsPostBack)
            {
                GVbind();
                GVbind2();

                //dptipemple.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }

            no_contrato.Visible = false;
            btneditar.Visible = false;
            btncancelar.Visible = false;

            //permisosGenerales();
            //perm();

        }

        protected void GVbind2()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select id_contrato,no_empleado,nombre,apaterno,amaterno,tipo_empleado,tipo_contrato,tiempo,puesto,area,f_inicio,f_termino,sueldo,periodo_pago FROM contrato_tisit where activo = 1", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridcontra.DataSource = dr;
                    gridcontra.DataBind();
                }
            }
        }

        protected void GVbind()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select id_empleado,no_empleado,nombre,paterno,materno FROM alta_empleados where activo = 1", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GridView1.DataSource = dr;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtusuario.Text = GridView1.SelectedRow.Cells[1].Text;
            txtnumber.Text = GridView1.SelectedRow.Cells[1].Text;
            txtnames.Text = GridView1.SelectedRow.Cells[2].Text;
            txtpaterno.Text = GridView1.SelectedRow.Cells[3].Text;
            txtmaterno.Text = GridView1.SelectedRow.Cells[4].Text;
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("alta_usuario.aspx");
        }
        protected void btnexcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = EjemploGrid.xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            gridcontra.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();

        }

        protected void btnpdf_Click(object sender, EventArgs e)
        {
            btnpdf_Click1(sender, e, gridcontra);
        }

        //EXPORTAR A PDF TABLA USUARIOS
        protected void btnpdf_Click1(object sender, EventArgs e, GridView gridcontra)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))

                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=REPORTE CONTRATO LABORAL.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    gridcontra.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document documento = new Document(PageSize.A3.Rotate(), 40f, 40f, 40f, 10f);
                    PdfWriter writer = PdfWriter.GetInstance(documento, Response.OutputStream);

                    //documento.SetPageSize(PageSize.A4.Rotate());
                    documento.Open();

                    BaseFont _titulo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
                    Font titulo = new iTextSharp.text.Font(_titulo, 24f, Font.BOLD, new BaseColor(0, 0, 0));

                    Image logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/assets/images/logo/tenkui.png"));
                    logo.ScalePercent(40f);

                    var content = writer.DirectContent;
                    var bordes = new Rectangle(documento.PageSize);

                    bordes.Left += documento.LeftMargin;
                    bordes.Right -= documento.RightMargin;
                    bordes.Top -= documento.TopMargin;
                    bordes.Bottom += documento.BottomMargin;
                    content.SetColorStroke(BaseColor.BLUE);
                    content.Rectangle(bordes.Left, bordes.Bottom, bordes.Width, bordes.Height);
                    content.Stroke();

                    var tb1 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                    tb1.AddCell(new PdfPCell(logo) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER }); ;
                    tb1.AddCell(new PdfPCell(new Phrase("Reporte de Contratos", titulo)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    documento.Add(tb1);
                    Phrase espacio = new Phrase("");
                    documento.Add(espacio);


                    gridcontra.AllowPaging = true;
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, documento, sr);
                    documento.Close();
                    Response.Write(documento);
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void btneditar_contrato_Click(object sender, EventArgs e)

        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ContratoLaboral SET nombre = %", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridcontra.DataSource = dr;
                    gridcontra.DataBind();
                }

            }

        }
        protected void btneditar_contrato_Click1(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from ContratoLaboral", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridcontra.DataSource = dr;
                    gridcontra.DataBind();
                }
            }
        }

        protected void gridcontra_SelectedIndexChanged1(object sender, EventArgs e)
        {
            no_contrato.Visible = true;
            btneditar.Visible = true;
            btncancelar.Visible = true;
            btnsave.Visible = false;


            txtcontrato.Text = gridcontra.SelectedRow.Cells[0].Text;
            txtnumber.Text = gridcontra.SelectedRow.Cells[1].Text;
            txtnames.Text = gridcontra.SelectedRow.Cells[2].Text;
            txtpaterno.Text = gridcontra.SelectedRow.Cells[3].Text;
            txtmaterno.Text = gridcontra.SelectedRow.Cells[4].Text;
            dptipemple.Text = gridcontra.SelectedRow.Cells[5].Text;
            dptipocontra.Text = gridcontra.SelectedRow.Cells[6].Text;
            dptiempo.Text = gridcontra.SelectedRow.Cells[7].Text;
            dppuesto.Text = gridcontra.SelectedRow.Cells[8].Text;
            txtarea.Text = gridcontra.SelectedRow.Cells[9].Text;
            txtfechainicio.Text = gridcontra.SelectedRow.Cells[10].Text;
            txtfechaterm.Text = gridcontra.SelectedRow.Cells[11].Text;
            txtsueldo.Text = gridcontra.SelectedRow.Cells[12].Text;

            gridcontra.SelectedIndex = -1;
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            btncancelar.Visible = false;
            btneditar.Visible = false;
            btnsave.Visible = true;

            txtcontrato.Text = "";
            txtnumber.Text = "";
            txtnames.Text = "";
            txtpaterno.Text = "";
            txtmaterno.Text = "";
            dptipemple.Text = "";
            dptipocontra.Text = "";
            dptiempo.Text = "";
            dppuesto.Text = "";
            txtarea.Text = "";
            txtfechainicio.Text = "";
            txtfechaterm.Text = "";
            txtsueldo.Text = "";
        }

        protected void btnsave_Click1(object sender, EventArgs e)
        {
            con.Open();
            int result = 0;
            SqlCommand cmd = new SqlCommand("INSERT INTO contrato_tisit(" +
                "no_empleado, nombre, apaterno, amaterno, tipo_empleado, " +
                "tipo_contrato, tiempo, puesto, area, " +
                "f_inicio, f_termino, sueldo, periodo_pago,activo) VALUES " +
                "('" + this.txtnumber.Text + "','" + this.txtnames.Text.ToUpper() + "','" + this.txtpaterno.Text.ToUpper() + "','" + this.txtmaterno.Text.ToUpper() + "','" + this.dptipemple.SelectedValue.ToUpper() + "'," +
                "'" + this.dptipocontra.SelectedValue.ToUpper() + "','" + this.dptiempo.SelectedValue.ToUpper() + "','" + this.dppuesto.SelectedValue.ToUpper() + "','" + this.txtarea.Text.ToUpper() + "'," +
                "'" + this.txtfechainicio.Text.ToUpper() + "','" + this.txtfechaterm.Text.ToUpper() + "','" + this.txtsueldo.Text.ToUpper() + "','" + this.dpperiodopago.SelectedValue.ToUpper() + "',1)", con);

            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true);

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
            }


            con.Close();

            txtcontrato.Text = "";
            txtnumber.Text = "";
            txtnames.Text = "";
            txtpaterno.Text = "";
            txtmaterno.Text = "";
            dptipemple.Text = "";
            dptipocontra.Text = "";
            dptiempo.Text = "";
            dppuesto.Text = "";
            txtarea.Text = "";
            txtfechainicio.Text = "";
            txtfechaterm.Text = "";
            txtsueldo.Text = "";

            GVbind2();
        }

        protected void btneditar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                {
                    int res = 0;
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                    "Update contrato_tisit SET no_empleado='" + txtnumber.Text.ToUpper() + "', nombre='" + txtnames.Text.ToUpper() + "', " +
                    "apaterno='" + txtpaterno.Text.ToUpper() + "', amaterno='" + txtmaterno.Text.ToUpper() + "', tipo_empleado='" + dptipemple.SelectedValue.ToUpper() + "',tipo_contrato='" + dptipocontra.SelectedValue.ToUpper() + "'," +
                    "tiempo='" + dptiempo.SelectedValue.ToUpper() + "', puesto='" + dppuesto.SelectedValue.ToUpper() + "', area='" + txtarea.Text.ToUpper() + "'," +
                    "f_inicio='" + txtfechainicio.Text.ToUpper() + "',f_termino='" + txtfechaterm.Text.ToUpper() + "', sueldo='" + txtsueldo.Text.ToUpper() + "', " +
                    "periodo_pago='" + dpperiodopago.SelectedValue.ToUpper() + "' WHERE  id_contrato = ' " + txtcontrato.Text + "'", con);
                    res = cmd.ExecuteNonQuery();

                    if (res > 0)
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true); ;
                        gridcontra.EditIndex = -1;

                    }
                }

            }

            txtcontrato.Text = "";
            txtnumber.Text = "";
            txtnames.Text = "";
            txtpaterno.Text = "";
            txtmaterno.Text = "";
            dptipemple.Text = "";
            dptipocontra.Text = "";
            dptiempo.Text = "";
            dppuesto.Text = "";
            txtarea.Text = "";
            txtfechainicio.Text = "";
            txtfechaterm.Text = "";
            txtsueldo.Text = "";

            btnsave.Visible = true;
            GVbind2();

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