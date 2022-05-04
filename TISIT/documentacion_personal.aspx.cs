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
using System.Drawing;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace TISIT
{
    public partial class documentacion_personal : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-LEO;Initial Catalog=TISIT;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);

            GVbind();
            fillData();

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
                SqlCommand cmd = new SqlCommand("SELECT * FROM alta_empleados where activo = 1", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridempleados.DataSource = dr;
                    gridempleados.DataBind();
                }
            }
        }


        private void fillData()
        {
            DataTable dt = new DataTable();
            string cn = "select * from documentacion where activo = 1";
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(cn, con);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }

            if (dt.Rows.Count > 0)
            {
                gridocu.DataSource = dt;
                gridocu.DataBind();
            }
        }

        protected void btnb2_Click(object sender, EventArgs e)
        {
            //consultaempleados.Visible = true;
            agregardocumentacion.Visible = false;
        }

        protected void btnguardardocu_Click(object sender, EventArgs e)
        {
            FileInfo f1 = new FileInfo(fileacta.FileName);
            byte[] documentContent = fileacta.FileBytes;
            string name = f1.Name;
            FileInfo f2 = new FileInfo(fileine.FileName);
            byte[] documentContent2 = fileine.FileBytes;
            string name2 = f2.Name;
            FileInfo f3 = new FileInfo(filecomprobante.FileName);
            byte[] documentContent3 = filecomprobante.FileBytes;
            string name3 = f3.Name;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("savedocu", con);
                cmd.CommandType = CommandType.StoredProcedure;
                int result = 0;

                cmd.Parameters.Add("@no_empleado", SqlDbType.Int).Value = this.txtnumberdocu.Text;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtnombredocu.Text.ToUpper();
                cmd.Parameters.Add("@paterno", SqlDbType.VarChar).Value = this.txtpaterno.Text.ToUpper();
                cmd.Parameters.Add("@materno", SqlDbType.VarChar).Value = this.txtmaterno.Text.ToUpper();
                cmd.Parameters.Add("@correo_personal", SqlDbType.VarChar).Value = this.txtcorreopersonal.Text.ToUpper();
                cmd.Parameters.Add("@correo_institucional", SqlDbType.VarChar).Value = this.txtcorreoinstitucional.Text.ToUpper();

                cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@filecontent", SqlDbType.VarBinary).Value = documentContent;

                cmd.Parameters.Add("@filename1", SqlDbType.VarChar).Value = name2;
                cmd.Parameters.Add("@filecontent1", SqlDbType.VarBinary).Value = documentContent2;

                cmd.Parameters.Add("@filename2", SqlDbType.VarChar).Value = name3;
                cmd.Parameters.Add("@filecontent2", SqlDbType.VarBinary).Value = documentContent3;

                cmd.Parameters.Add("@activo", SqlDbType.Bit).Value = 1;

                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true);
                    this.txtnumberdocu.Text = string.Empty;
                    this.txtpaterno.Text = string.Empty;
                    this.txtmaterno.Text = string.Empty;
                    this.txtnombredocu.Text = string.Empty;
                    this.txtcorreopersonal.Text = string.Empty;
                    this.txtcorreoinstitucional.Text = string.Empty;

                    fillData();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "errorguardado()", true);
                    this.txtnumberdocu.Text = string.Empty;
                    this.txtnombredocu.Text = string.Empty;
                    this.txtpaterno.Text = string.Empty;
                    this.txtmaterno.Text = string.Empty;
                    this.txtcorreopersonal.Text = string.Empty;
                    this.txtcorreoinstitucional.Text = string.Empty;
                }
            }
        }


        protected void gridempleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            agregardocumentacion.Visible = true;
            txtnumberdocu.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[1].Text);
            txtnombredocu.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[2].Text);
            txtpaterno.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[3].Text);
            txtmaterno.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[4].Text);
            gridempleados.SelectedIndex = -1;
        }

        protected void btncancelarempleado_Click(object sender, EventArgs e)
        {
            agregardocumentacion.Visible = true;
        }

        private void Download1(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("getacta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@no_empleado", SqlDbType.Int).Value = id;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            string name = dt.Rows[0]["FileName1"].ToString();
            byte[] documentBytes = (byte[])dt.Rows[0]["FileContent1"];
            Response.ClearContent();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-Disposition", string.Format("attachment; FileName={0}", name));
            Response.AddHeader("content-Length", documentBytes.Length.ToString());
            Response.BinaryWrite(documentBytes);
            Response.Flush();
            Response.Close();
        }
        private void Download2(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("getidentificaion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@no_empleado", SqlDbType.Int).Value = id;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            string name = dt.Rows[0]["FileName2"].ToString();
            byte[] documentBytes = (byte[])dt.Rows[0]["FileContent2"];
            Response.ClearContent();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-Disposition", string.Format("attachment; FileName={0}", name));
            Response.AddHeader("content-Length", documentBytes.Length.ToString());
            Response.BinaryWrite(documentBytes);
            Response.Flush();
            Response.Close();
        }
        private void Download3(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("getcomprobanteestudios", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@no_empleado", SqlDbType.Int).Value = id;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            string name = dt.Rows[0]["FileName3"].ToString();
            byte[] documentBytes = (byte[])dt.Rows[0]["FileContent3"];
            Response.ClearContent();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-Disposition", string.Format("attachment; FileName={0}", name));
            Response.AddHeader("content-Length", documentBytes.Length.ToString());
            Response.BinaryWrite(documentBytes);
            Response.Flush();
            Response.Close();
        }
        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            LinkButton ink = (LinkButton)sender;
            GridViewRow gr = (GridViewRow)ink.NamingContainer;
            int id = int.Parse(gridocu.DataKeys[gr.RowIndex].Value.ToString());
            Download1(id);

            gridocu.Columns[0].Visible = false;
        }
        protected void LinkButton14_Click(object sender, EventArgs e)
        {
            LinkButton ink = (LinkButton)sender;
            GridViewRow gr = (GridViewRow)ink.NamingContainer;
            int id = int.Parse(gridocu.DataKeys[gr.RowIndex].Value.ToString());
            Download2(id);

            gridocu.Columns[1].Visible = false;
        }
        protected void LinkButton15_Click1(object sender, EventArgs e)
        {
            LinkButton ink = (LinkButton)sender;
            GridViewRow gr = (GridViewRow)ink.NamingContainer;
            int id = int.Parse(gridocu.DataKeys[gr.RowIndex].Value.ToString());
            Download3(id);

            gridocu.Columns[2].Visible = false;
        }

        protected void gridocu_SelectedIndexChanged(object sender, EventArgs e)
        {
            agregardocumentacion.Visible = true;
            txtnumberdocu.Text = HttpUtility.HtmlDecode(gridocu.SelectedRow.Cells[0].Text);
            txtnombredocu.Text = HttpUtility.HtmlDecode(gridocu.SelectedRow.Cells[1].Text);
            txtpaterno.Text = HttpUtility.HtmlDecode(gridocu.SelectedRow.Cells[2].Text);
            txtmaterno.Text = HttpUtility.HtmlDecode(gridocu.SelectedRow.Cells[3].Text);
            txtcorreopersonal.Text = HttpUtility.HtmlDecode(gridocu.SelectedRow.Cells[4].Text);
            txtcorreoinstitucional.Text = HttpUtility.HtmlDecode(gridocu.SelectedRow.Cells[5].Text);
            acta.Visible = false;
            ine.Visible = false;
            comprobante.Visible = false;
            btneditar.Visible = true;
            btnine.Visible = true;
            btncomprobante.Visible = true;
            btnacta.Visible = true;
            btnguardardocu.Visible = false;
            btncancel.Visible = true;
            gridocu.SelectedIndex = -1;
        }

        protected void btnconsultar_Click1(object sender, EventArgs e)
        {
            agregardocumentacion.Visible = false;
            btncancel.Visible = true;
        }

        protected void btncancelardocu_Click(object sender, EventArgs e)
        {
            agregardocumentacion.Visible = true;
            btneditar.Visible = false;
            btncancel.Visible = false;
            btnacta.Visible = false;
            btnine.Visible = false;
            btncomprobante.Visible = false;
            btnguardardocu.Visible = true;
            ine.Visible = true;
            comprobante.Visible = true;
            acta.Visible = true;
            this.txtnumberdocu.Text = string.Empty;
            this.txtnombredocu.Text = string.Empty;
            this.txtcorreopersonal.Text = string.Empty;
            this.txtcorreoinstitucional.Text = string.Empty;
        }

        protected void btnacta_Click(object sender, EventArgs e)
        {
            acta.Visible = true;
            btncancelaredit.Visible = true;
        }

        protected void btnine_Click(object sender, EventArgs e)
        {
            ine.Visible = true;
            btncancelaredit.Visible = true;
        }

        protected void btncomprobante_Click(object sender, EventArgs e)
        {
            comprobante.Visible = true;
            btncancelaredit.Visible = true;
        }

        protected void btncancelaredit_Click(object sender, EventArgs e)
        {
            acta.Visible = false;
            ine.Visible = false;
            comprobante.Visible = false;
            btncancelaredit.Visible = false;
        }

        protected void btneditar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                if (acta.Visible == false && comprobante.Visible == false && ine.Visible == false)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update documentacion set correo_personal=@correo_personal, correo_institucional=@correo_institucional where no_empleado=@no_empleado", con);
                    int result = 0;

                    cmd.Parameters.Add("@no_empleado", SqlDbType.VarChar).Value = this.txtnumberdocu.Text;
                    cmd.Parameters.Add("@correo_personal", SqlDbType.VarChar).Value = this.txtcorreopersonal.Text.ToUpper();
                    cmd.Parameters.Add("@correo_institucional", SqlDbType.VarChar).Value = this.txtcorreoinstitucional.Text.ToUpper();
                    cmd.ExecuteNonQuery();

                    result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExitoActualizar()", true);
                        agregardocumentacion.Visible = true;
                        acta.Visible = true;
                        ine.Visible = true;
                        comprobante.Visible = true;
                        btneditar.Visible = false;
                        btnine.Visible = false;
                        btncomprobante.Visible = false;
                        btnacta.Visible = false;
                        btnguardardocu.Visible = true;
                        btncancel.Visible = false;

                        this.txtnumberdocu.Text = string.Empty;
                        this.txtnombredocu.Text = string.Empty;
                        this.txtcorreopersonal.Text = string.Empty;
                        this.txtcorreoinstitucional.Text = string.Empty;

                        fillData();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "erroractualizado()", true);
                        this.txtnumberdocu.Text = string.Empty;
                        this.txtnombredocu.Text = string.Empty;
                        this.txtcorreopersonal.Text = string.Empty;
                        this.txtcorreoinstitucional.Text = string.Empty;
                    }
                }
                else if (acta.Visible == true && comprobante.Visible == false && ine.Visible == false)
                {
                    FileInfo f1 = new FileInfo(fileacta.FileName);
                    byte[] documentContent = fileacta.FileBytes;
                    string name = f1.Name;
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("updatedocuacta", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    int result1 = 0;

                    cmd1.Parameters.Add("@no_empleado", SqlDbType.Int).Value = this.txtnumberdocu.Text;
                    cmd1.Parameters.Add("@correo_personal", SqlDbType.VarChar).Value = this.txtcorreopersonal.Text.ToUpper();
                    cmd1.Parameters.Add("@correo_institucional", SqlDbType.VarChar).Value = this.txtcorreoinstitucional.Text.ToUpper();

                    cmd1.Parameters.Add("@filename1", SqlDbType.VarChar).Value = name;
                    cmd1.Parameters.Add("@filecontent1", SqlDbType.VarBinary).Value = documentContent;

                    result1 = cmd1.ExecuteNonQuery();
                    if (result1 > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExitoActualizar()", true);
                        this.txtnumberdocu.Text = string.Empty;
                        this.txtnombredocu.Text = string.Empty;
                        this.txtcorreopersonal.Text = string.Empty;
                        this.txtcorreoinstitucional.Text = string.Empty;
                        agregardocumentacion.Visible = false;
                        fillData();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "erroractualizado()", true);
                        this.txtnumberdocu.Text = string.Empty;
                        this.txtnombredocu.Text = string.Empty;
                        this.txtcorreopersonal.Text = string.Empty;
                        this.txtcorreoinstitucional.Text = string.Empty;
                    }
                }
                else if (acta.Visible == false && ine.Visible == true && comprobante.Visible == false)
                {
                    FileInfo f2 = new FileInfo(fileine.FileName);
                    byte[] documentContent = fileine.FileBytes;
                    string name = f2.Name;
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("updatedocuine", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    int result1 = 0;

                    cmd2.Parameters.Add("@no_empleado", SqlDbType.Int).Value = this.txtnumberdocu.Text;
                    cmd2.Parameters.Add("@correo_personal", SqlDbType.VarChar).Value = this.txtcorreopersonal.Text.ToUpper();
                    cmd2.Parameters.Add("@correo_institucional", SqlDbType.VarChar).Value = this.txtcorreoinstitucional.Text.ToUpper();

                    cmd2.Parameters.Add("@filename2", SqlDbType.VarChar).Value = name;
                    cmd2.Parameters.Add("@filecontent2", SqlDbType.VarBinary).Value = documentContent;

                    result1 = cmd2.ExecuteNonQuery();
                    if (result1 > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExitoActualizar()", true);
                        this.txtnumberdocu.Text = string.Empty;
                        this.txtnombredocu.Text = string.Empty;
                        this.txtcorreopersonal.Text = string.Empty;
                        this.txtcorreoinstitucional.Text = string.Empty;
                        agregardocumentacion.Visible = false;
                        fillData();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "erroractualizado()", true);
                        this.txtnumberdocu.Text = string.Empty;
                        this.txtnombredocu.Text = string.Empty;
                        this.txtcorreopersonal.Text = string.Empty;
                        this.txtcorreoinstitucional.Text = string.Empty;
                    }
                }
                else if (acta.Visible == false && ine.Visible == false && comprobante.Visible == true)
                {
                    FileInfo f3 = new FileInfo(filecomprobante.FileName);
                    byte[] documentContent = filecomprobante.FileBytes;
                    string name = f3.Name;
                    con.Open();
                    SqlCommand cmd3 = new SqlCommand("updatedoucomprobante", con);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    int result3 = 0;

                    cmd3.Parameters.Add("@no_empleado", SqlDbType.Int).Value = this.txtnumberdocu.Text;
                    cmd3.Parameters.Add("@correo_personal", SqlDbType.VarChar).Value = this.txtcorreopersonal.Text.ToUpper();
                    cmd3.Parameters.Add("@correo_institucional", SqlDbType.VarChar).Value = this.txtcorreoinstitucional.Text.ToUpper();

                    cmd3.Parameters.Add("@filename3", SqlDbType.VarChar).Value = name;
                    cmd3.Parameters.Add("@filecontent3", SqlDbType.VarBinary).Value = documentContent;

                    result3 = cmd3.ExecuteNonQuery();
                    if (result3 > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExitoActualizar()", true);
                        this.txtnumberdocu.Text = string.Empty;
                        this.txtnombredocu.Text = string.Empty;
                        this.txtcorreopersonal.Text = string.Empty;
                        this.txtcorreoinstitucional.Text = string.Empty;
                        agregardocumentacion.Visible = false;
                        fillData();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "erroractualizado()", true);
                        this.txtnumberdocu.Text = string.Empty;
                        this.txtnombredocu.Text = string.Empty;
                        this.txtcorreopersonal.Text = string.Empty;
                        this.txtcorreoinstitucional.Text = string.Empty;
                    }
                }
                else if (acta.Visible == true && comprobante.Visible == true && ine.Visible == true)
                {
                    FileInfo f11 = new FileInfo(fileacta.FileName);
                    byte[] documentContent1 = fileacta.FileBytes;
                    FileInfo f21 = new FileInfo(fileine.FileName);
                    byte[] documentContent2 = fileine.FileBytes;
                    FileInfo f31 = new FileInfo(filecomprobante.FileName);
                    byte[] documentContent3 = filecomprobante.FileBytes;
                    string name = f11.Name;
                    string name2 = f21.Name;
                    string name3 = f31.Name;

                    con.Open();
                    SqlCommand cmd4 = new SqlCommand("updatedocugeneral", con);
                    cmd4.CommandType = CommandType.StoredProcedure;
                    int result4 = 0;

                    cmd4.Parameters.Add("@no_empleado", SqlDbType.Int).Value = this.txtnumberdocu.Text;
                    cmd4.Parameters.Add("@correo_personal", SqlDbType.VarChar).Value = this.txtcorreopersonal.Text.ToUpper();
                    cmd4.Parameters.Add("@correo_institucional", SqlDbType.VarChar).Value = this.txtcorreoinstitucional.Text.ToUpper();

                    cmd4.Parameters.Add("@filename1", SqlDbType.VarChar).Value = name;
                    cmd4.Parameters.Add("@filecontent1", SqlDbType.VarBinary).Value = documentContent1;
                    cmd4.Parameters.Add("@filename2", SqlDbType.VarChar).Value = name2;
                    cmd4.Parameters.Add("@filecontent2", SqlDbType.VarBinary).Value = documentContent2;
                    cmd4.Parameters.Add("@filename3", SqlDbType.VarChar).Value = name3;
                    cmd4.Parameters.Add("@filecontent3", SqlDbType.VarBinary).Value = documentContent3;

                    result4 = cmd4.ExecuteNonQuery();
                    if (result4 > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExitoActualizar()", true);
                        this.txtnumberdocu.Text = string.Empty;
                        this.txtnombredocu.Text = string.Empty;
                        this.txtcorreopersonal.Text = string.Empty;
                        this.txtcorreoinstitucional.Text = string.Empty;
                        agregardocumentacion.Visible = false;
                        fillData();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "erroractualizado()", true);
                        this.txtnumberdocu.Text = string.Empty;
                        this.txtnombredocu.Text = string.Empty;
                        this.txtcorreopersonal.Text = string.Empty;
                        this.txtcorreoinstitucional.Text = string.Empty;
                    }
                }
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            agregardocumentacion.Visible = true;
            acta.Visible = true;
            ine.Visible = true;
            comprobante.Visible = true;
            btneditar.Visible = false;
            btnine.Visible = false;
            btncomprobante.Visible = false;
            btnacta.Visible = false;
            btnguardardocu.Visible = true;
            btncancel.Visible = false;

            this.txtnumberdocu.Text = string.Empty;
            this.txtnombredocu.Text = string.Empty;
            this.txtcorreopersonal.Text = string.Empty;
            this.txtcorreoinstitucional.Text = string.Empty;
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
    }
}