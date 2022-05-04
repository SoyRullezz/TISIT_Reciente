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
    public partial class doctores : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["usuario"] == null) { Response.Redirect("login.aspx"); }
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);

            if (!Page.IsPostBack)
            {
                GVbind();
                GVbind2();
            }

            //permisosGenerales();
            //perm();
        }
        protected void GVbind()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM doctores where activo = 1", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    griddoctores.DataSource = dr;
                    griddoctores.DataBind();
                }
            }
        }

        protected void GVbind2()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM doctores where activo = 0", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    griddoctores1.DataSource = dr;
                    griddoctores1.DataBind();
                }
            }
        }
        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
            Session.Abandon();
        }
        protected void btnguardar_Click(object sender, EventArgs e)
        {
            con.Open();
            int result = 0;
            string nombre_completo = txtnombredoctor.Text + "  " + txtpaternodoctor.Text + "  " + txtmaternodoctor.Text;
            SqlCommand cmd = new SqlCommand("INSERT INTO doctores(nombre_doctor, apaterno,amaterno,hospital,especialista,activo) VALUES ('" + txtnombredoctor.Text.ToUpper() + "', '" + txtpaternodoctor.Text.ToUpper() + "', '" + txtmaternodoctor.Text.ToUpper() + "', '" + txthospital.Text.ToUpper() + "','" + txtespecialista.Text.ToUpper() + "',1)", con);
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true);
                this.txtnombredoctor.Text = string.Empty;
                this.txtpaternodoctor.Text = string.Empty;
                this.txtmaternodoctor.Text = string.Empty;
                this.txthospital.Text = string.Empty;
                this.txtespecialista.Text = string.Empty;
                GVbind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
            }
        }

        protected void griddoctores_SelectedIndexChanged(object sender, EventArgs e)
        {
            guardardatosdoctores.Visible = true;
            //consultadoctores.Visible = false;
            nombre.Visible = false;
            paterno.Visible = false;
            materno.Visible = false;
            completo.Visible = true;
            paternoup.Visible = true;
            maternoup.Visible = true;
            btnguardar.Visible = false;
            //btnconsultar.Visible = false;
            btneditar.Visible = true;
            btncancelaredit.Visible = true;

            Session["id_doctor"] = HttpUtility.HtmlDecode(griddoctores.SelectedRow.Cells[0].Text);
            txtnombretotal.Text = HttpUtility.HtmlDecode(griddoctores.SelectedRow.Cells[1].Text);
            txtpaternoupdate.Text = HttpUtility.HtmlDecode(griddoctores.SelectedRow.Cells[2].Text);
            txtmaternoupdate.Text = HttpUtility.HtmlDecode(griddoctores.SelectedRow.Cells[3].Text);
            txthospital.Text = HttpUtility.HtmlDecode(griddoctores.SelectedRow.Cells[4].Text);
            txtespecialista.Text = HttpUtility.HtmlDecode(griddoctores.SelectedRow.Cells[5].Text);
        }

        protected void griddoctores1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = HttpUtility.HtmlDecode(griddoctores1.SelectedRow.Cells[0].Text);

            try
            {

                using (SqlConnection con = new SqlConnection(cs))
                {
                    int res = 0;

                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE doctores SET activo = 1 where id_doctor = '" + id + "'", con);

                    res = cmd.ExecuteNonQuery();

                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExitoEditar()", true);
                        griddoctores1.SelectedIndex = -1;
                        Session.Clear();
                        GVbind();
                        GVbind2();

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "ErrorActualizar()", true);
                        GVbind();
                        GVbind2();
                    }

                }
            }
            catch
            {

            }
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            //consultadoctores.Visible = true;
            ClientScript.RegisterClientScriptBlock(this.GetType(), "randomtext", "confirmDelete()", true);
        }

        protected void btneditar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                int res = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE doctores SET nombre_doctor = '" + txtnombretotal.Text.ToUpper() + "', apaterno = '" + txtpaternoupdate.Text.ToUpper() + "', amaterno = '" + txtmaternoupdate.Text.ToUpper() + "', hospital = '" + txthospital.Text.ToUpper() + "',especialista = '" + txtespecialista.Text.ToUpper() + "' where id_doctor = '" + Session["id_doctor"] + "'", con);
                res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExitoEditar()", true);
                    guardardatosdoctores.Visible = true;
                    //consultadoctores.Visible = true;
                    nombre.Visible = true;
                    paterno.Visible = true;
                    materno.Visible = true;
                    completo.Visible = false;
                    paternoup.Visible = false;
                    maternoup.Visible = false;
                    btnguardar.Visible = true;
                    //btnconsultar.Visible = true;
                    btneditar.Visible = false;
                    btncancelaredit.Visible = false;
                    griddoctores.SelectedIndex = -1;

                    this.txthospital.Text = string.Empty;
                    this.txtespecialista.Text = string.Empty;

                    GVbind();
                    Session.Clear();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "ErrorActualizar()", true);
                }
            }
        }

        protected void btncancelaredit_Click(object sender, EventArgs e)
        {
            guardardatosdoctores.Visible = true;
            nombre.Visible = true;
            paterno.Visible = true;
            materno.Visible = true;
            completo.Visible = false;
            paternoup.Visible = false;
            maternoup.Visible = false;

            btnguardar.Visible = true;
            btneditar.Visible = false;
            btncancelaredit.Visible = false;

            this.txthospital.Text = string.Empty;
            this.txtespecialista.Text = string.Empty;

            griddoctores.SelectedIndex = -1;
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            guardardatosdoctores.Visible = true;
            //consultadoctores.Visible = false;
            nombre.Visible = true;
            paterno.Visible = true;
            materno.Visible = true;
            completo.Visible = false;
            paternoup.Visible = false;
            maternoup.Visible = false;
            btnguardar.Visible = true;
            //btnconsultar.Visible = true;
            btneditar.Visible = false;
            btncancelaredit.Visible = false;

            this.txtnombredoctor.Text = string.Empty;
            this.txtpaternodoctor.Text = string.Empty;
            this.txtmaternodoctor.Text = string.Empty;
            this.txthospital.Text = string.Empty;
            this.txtespecialista.Text = string.Empty;
        }
        protected void griddoctores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(griddoctores.DataKeys[e.RowIndex].Value);

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {

                    int res = 0;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE doctores SET activo = 0 where id_doctor = '" + id + "'", con);
                    res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "randomtext", $"correvtoborrado({id})", true);
                        GVbind();
                        GVbind2();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "randomtext", $"errorBorrar({id})", true);
                        GVbind();
                    }

                }
            }
            catch
            {


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
    }
}