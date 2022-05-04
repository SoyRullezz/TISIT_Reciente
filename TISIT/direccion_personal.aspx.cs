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
    public partial class direccion_personal : System.Web.UI.Page
    {

        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

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
            string cn = "select * from direccion_personal where activo = 1";
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(cn, con);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }

            if (dt.Rows.Count > 0)
            {
                gridir.DataSource = dt;
                gridir.DataBind();
            }
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            consultaempleados.Visible = true;
            agregardireccion.Visible = false;
        }

        protected void gridempleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            agregardireccion.Visible = true;
            consultaempleados.Visible = false;
            txtnouser.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[1].Text);
            txtnombre.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[2].Text);
            txtpaterno.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[3].Text);
            txtmaterno.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[4].Text);
            gridempleados.SelectedIndex = -1;
        }

        protected void btnbuscarempleado_Click(object sender, EventArgs e)
        {
            consultaempleados.Visible = true;
            if (string.IsNullOrEmpty(txtbuscar.Text))
            {
                GVbind();
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(cs))
                {
                    sqlcon.Open();
                    string query = "SELECT COUNT(1) FROM alta_empleados WHERE (id_empleado like '%" + txtbuscar.Text + "%' or no_empleado like '%" + txtbuscar.Text + "%' or nombre like '%" + txtbuscar.Text + "%' or paterno like '%" + txtbuscar.Text + "%' or materno like '%" + txtbuscar.Text + "%') AND activo = 0";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                    int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                    sqlcon.Close();

                    if (count == 0)
                    {
                        SqlCommand cmd = new SqlCommand("select * FROM alta_empleados where id_empleado like '%" + txtbuscar.Text + "%' or no_empleado like '%" + txtbuscar.Text + "%' or nombre like '%" + txtbuscar.Text + "%' or paterno like '%" + txtbuscar.Text + "%' or materno like '%" + txtbuscar.Text + "%' AND activo = 1", sqlcon);
                        cmd.Connection = con;
                        gridempleados.DataSourceID = "";
                        cmd.Connection = sqlcon;
                        sqlcon.Open();
                        gridempleados.DataSource = cmd.ExecuteReader();
                        gridempleados.DataBind();
                    }
                    else if (count == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "BAJA()", true);
                        count = 0;
                    }
                }
            }
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            FileInfo f1 = new FileInfo(FileUpload1.FileName);
            byte[] documentContent = FileUpload1.FileBytes;
            string name = f1.Name;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("savecomprobante", con);
                cmd.CommandType = CommandType.StoredProcedure;
                int result = 0;

                cmd.Parameters.Add("@no_empleado", SqlDbType.Int).Value = this.txtnouser.Text.ToUpper();
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtnombre.Text.ToUpper();
                cmd.Parameters.Add("@paterno", SqlDbType.VarChar).Value = this.txtpaterno.Text.ToUpper();
                cmd.Parameters.Add("@materno", SqlDbType.VarChar).Value = this.txtmaterno.Text.ToUpper();
                cmd.Parameters.Add("@calle", SqlDbType.VarChar).Value = this.txtdire.Text.ToUpper();
                cmd.Parameters.Add("@colonia", SqlDbType.VarChar).Value = this.txtcolonia.Text.ToUpper();
                cmd.Parameters.Add("@interior", SqlDbType.Int).Value = this.txtinterior.Text;
                cmd.Parameters.Add("@exterior", SqlDbType.Int).Value = this.txtexterior.Text;
                cmd.Parameters.Add("@municipio", SqlDbType.VarChar).Value = this.txtmunicipio.Text.ToUpper();
                cmd.Parameters.Add("@entidad", SqlDbType.VarChar).Value = this.txtentidad.Text.ToUpper();
                cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = this.txtcodigo.Text;
                cmd.Parameters.Add("@telefono", SqlDbType.BigInt).Value = this.txttelefono.Text;
                cmd.Parameters.Add("@celular", SqlDbType.BigInt).Value = this.txtcelular.Text;

                cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@filecontent", SqlDbType.VarBinary).Value = documentContent;

                cmd.Parameters.Add("@activo", SqlDbType.Bit).Value = 1;

                result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaguardado()", true);
                    this.txtnouser.Text = string.Empty;
                    this.txtnombre.Text = string.Empty;
                    this.txtdire.Text = string.Empty;
                    this.txtcolonia.Text = string.Empty;
                    this.txtinterior.Text = string.Empty;
                    this.txtexterior.Text = string.Empty;
                    this.txtmunicipio.Text = string.Empty;
                    this.txtcodigo.Text = string.Empty;
                    this.txttelefono.Text = string.Empty;
                    this.txtcelular.Text = string.Empty;
                    this.txtpaterno.Text = string.Empty;
                    this.txtmaterno.Text = string.Empty;

                    fillData();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "errorguardado()", true);
                    this.txtnouser.Text = string.Empty;
                    this.txtnombre.Text = string.Empty;
                    this.txtdire.Text = string.Empty;
                    this.txtcolonia.Text = string.Empty;
                    this.txtinterior.Text = string.Empty;
                    this.txtexterior.Text = string.Empty;
                    this.txtmunicipio.Text = string.Empty;
                    this.txtcodigo.Text = string.Empty;
                    this.txttelefono.Text = string.Empty;
                    this.txtcelular.Text = string.Empty;
                    this.txtpaterno.Text = string.Empty;
                    this.txtmaterno.Text = string.Empty;
                }
            }
        }

        protected void btnconsultar_Click(object sender, EventArgs e)
        {
            //consultadireccion.Visible = true;
            agregardireccion.Visible = false;
            consultaempleados.Visible = false;
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            LinkButton ink = (LinkButton)sender;
            GridViewRow gr = (GridViewRow)ink.NamingContainer;
            int id = int.Parse(gridir.DataKeys[gr.RowIndex].Value.ToString());
            Download(id);

            gridir.Columns[0].Visible = false;
        }
        private void Download(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("getcomprobante", con);
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

        protected void gridir_SelectedIndexChanged(object sender, EventArgs e)
        {
            //consultadireccion.Visible = false;
            agregardireccion.Visible = true;
            txtnouser.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[0].Text);
            txtnombre.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[1].Text);
            txtpaterno.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[2].Text);
            txtmaterno.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[3].Text);
            txtdire.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[4].Text);
            txtinterior.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[6].Text);
            txtexterior.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[7].Text);
            txtcolonia.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[5].Text);
            txtmunicipio.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[8].Text);
            txtentidad.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[9].Text);
            txtcodigo.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[10].Text);
            txttelefono.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[11].Text);
            txtcelular.Text = HttpUtility.HtmlDecode(gridir.SelectedRow.Cells[12].Text);
            lblcomprobante.Visible = false;
            FileUpload1.Visible = false;
            btnguardar.Visible = false;
            //btnconsultar.Visible = false;
            btneditar.Visible = true;
            btncancelaredit.Visible = true;
            btneditardocu.Visible = true;
            //btnbuscar.Visible = false;
        }

        protected void btneditar_Click(object sender, EventArgs e)
        {
            if (FileUpload1.Visible == true)
            {
                FileInfo f1 = new FileInfo(FileUpload1.FileName);
                byte[] documentContent = FileUpload1.FileBytes;
                string name = f1.Name;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("updatecomprobante", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int result = 0;

                    cmd.Parameters.Add("@no_empleado", SqlDbType.Int).Value = this.txtnouser.Text;
                    cmd.Parameters.Add("@calle", SqlDbType.VarChar).Value = this.txtdire.Text.ToUpper();
                    cmd.Parameters.Add("@colonia", SqlDbType.VarChar).Value = this.txtcolonia.Text.ToUpper();
                    cmd.Parameters.Add("@interior", SqlDbType.Int).Value = this.txtinterior.Text;
                    cmd.Parameters.Add("@exterior", SqlDbType.Int).Value = this.txtexterior.Text;
                    cmd.Parameters.Add("@municipio", SqlDbType.VarChar).Value = this.txtmunicipio.Text.ToUpper();
                    cmd.Parameters.Add("@entidad", SqlDbType.VarChar).Value = this.txtentidad.Text.ToUpper();
                    cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = this.txtcodigo.Text;
                    cmd.Parameters.Add("@telefono", SqlDbType.BigInt).Value = this.txttelefono.Text;
                    cmd.Parameters.Add("@celular", SqlDbType.BigInt).Value = this.txtcelular.Text;

                    cmd.Parameters.Add("@filename1", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@filecontent1", SqlDbType.VarBinary).Value = documentContent;

                    result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaguardado()", true);
                        this.txtnouser.Text = string.Empty;
                        this.txtnombre.Text = string.Empty;
                        this.txtdire.Text = string.Empty;
                        this.txtcolonia.Text = string.Empty;
                        this.txtinterior.Text = string.Empty;
                        this.txtexterior.Text = string.Empty;
                        this.txtmunicipio.Text = string.Empty;
                        this.txtcodigo.Text = string.Empty;
                        this.txttelefono.Text = string.Empty;
                        this.txtcelular.Text = string.Empty;
                        this.txtpaterno.Text = string.Empty;
                        this.txtmaterno.Text = string.Empty;

                        lblcomprobante.Visible = true;
                        FileUpload1.Visible = true;
                        btnguardar.Visible = true;
                        btneditar.Visible = false;
                        btncancelaredit.Visible = false;
                        btneditardocu.Visible = false;

                        fillData();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "errorguardado()", true);
                        this.txtnouser.Text = string.Empty;
                        this.txtnombre.Text = string.Empty;
                        this.txtdire.Text = string.Empty;
                        this.txtcolonia.Text = string.Empty;
                        this.txtinterior.Text = string.Empty;
                        this.txtexterior.Text = string.Empty;
                        this.txtmunicipio.Text = string.Empty;
                        this.txtcodigo.Text = string.Empty;
                        this.txttelefono.Text = string.Empty;
                        this.txtcelular.Text = string.Empty;
                        this.txtpaterno.Text = string.Empty;
                        this.txtmaterno.Text = string.Empty;
                    }
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update direccion_personal set calle=@calle, colonia=@colonia, no_int=@interior, no_ext=@exterior, municipio=@municipio, entidad=@entidad, codigo_postal=@codigo, tel_casa=@telefono, celular=@celular where no_empleado=@no_empleado", con);
                    int result = 0;

                    cmd.Parameters.Add("@no_empleado", SqlDbType.Int).Value = this.txtnouser.Text;
                    cmd.Parameters.Add("@calle", SqlDbType.VarChar).Value = this.txtdire.Text.ToUpper();
                    cmd.Parameters.Add("@colonia", SqlDbType.VarChar).Value = this.txtcolonia.Text.ToUpper();
                    cmd.Parameters.Add("@interior", SqlDbType.Int).Value = this.txtinterior.Text;
                    cmd.Parameters.Add("@exterior", SqlDbType.Int).Value = this.txtexterior.Text;
                    cmd.Parameters.Add("@municipio", SqlDbType.VarChar).Value = this.txtmunicipio.Text.ToUpper();
                    cmd.Parameters.Add("@entidad", SqlDbType.VarChar).Value = this.txtentidad.Text.ToUpper();
                    cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = this.txtcodigo.Text;
                    cmd.Parameters.Add("@telefono", SqlDbType.BigInt).Value = this.txttelefono.Text;
                    cmd.Parameters.Add("@celular", SqlDbType.BigInt).Value = this.txtcelular.Text;
                    cmd.ExecuteNonQuery();

                    result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExitoActualizar()", true);
                        this.txtnouser.Text = string.Empty;
                        this.txtnombre.Text = string.Empty;
                        this.txtdire.Text = string.Empty;
                        this.txtcolonia.Text = string.Empty;
                        this.txtinterior.Text = string.Empty;
                        this.txtexterior.Text = string.Empty;
                        this.txtmunicipio.Text = string.Empty;
                        this.txtcodigo.Text = string.Empty;
                        this.txttelefono.Text = string.Empty;
                        this.txtcelular.Text = string.Empty;
                        this.txtpaterno.Text = string.Empty;
                        this.txtmaterno.Text = string.Empty;
                        this.txtentidad.Text = string.Empty;

                        lblcomprobante.Visible = true;
                        FileUpload1.Visible = true;
                        btnguardar.Visible = true;
                        btneditar.Visible = false;
                        btncancelaredit.Visible = false;
                        btneditardocu.Visible = false;

                        fillData();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "erroractualizado()", true);
                        this.txtnouser.Text = string.Empty;
                        this.txtnombre.Text = string.Empty;
                        this.txtdire.Text = string.Empty;
                        this.txtcolonia.Text = string.Empty;
                        this.txtinterior.Text = string.Empty;
                        this.txtexterior.Text = string.Empty;
                        this.txtmunicipio.Text = string.Empty;
                        this.txtcodigo.Text = string.Empty;
                        this.txttelefono.Text = string.Empty;
                        this.txtcelular.Text = string.Empty;
                        this.txtpaterno.Text = string.Empty;
                        this.txtmaterno.Text = string.Empty;
                        this.txtentidad.Text = string.Empty;
                    }
                }
            }
        }

        protected void btncancelaredit_Click(object sender, EventArgs e)
        {
            this.txtnouser.Text = string.Empty;
            this.txtnombre.Text = string.Empty;
            this.txtdire.Text = string.Empty;
            this.txtcolonia.Text = string.Empty;
            this.txtinterior.Text = string.Empty;
            this.txtexterior.Text = string.Empty;
            this.txtmunicipio.Text = string.Empty;
            this.txtcodigo.Text = string.Empty;
            this.txttelefono.Text = string.Empty;
            this.txtcelular.Text = string.Empty;
            this.txtpaterno.Text = string.Empty;
            this.txtmaterno.Text = string.Empty;
            //consultadireccion.Visible = true;
            agregardireccion.Visible = true;
            gridir.SelectedIndex = -1;
            btneditar.Visible = false;
            btneditardocu.Visible = false;
            btncancelaredit.Visible = false;
            btnguardar.Visible = true;
            //btnconsultar.Visible = true;
            //btnbuscar.Visible = true;
        }

        protected void btneditardocu_Click(object sender, EventArgs e)
        {
            FileUpload1.Visible = true;
            lblcomprobante.Visible = true;
            btneditardocu.Visible = true;
            btncancelardocu.Visible = true;
        }

        protected void btncancelardocu_Click(object sender, EventArgs e)
        {
            FileUpload1.Visible = false;
            lblcomprobante.Visible = false;
            btncancelardocu.Visible = false;
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