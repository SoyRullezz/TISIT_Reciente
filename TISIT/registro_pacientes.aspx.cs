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
    public partial class registro_pacientes : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        public string nameDoctor;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);

            GVbind();
            GVbind2();

            //permisosGenerales();
            //perm();

            //dpdoctor.DataBind();
            //dpdoctor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void GVbind()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM registro_pacientes_llegada", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridregistro.DataSource = dr;
                    gridregistro.DataBind();
                }
            }
        }

        protected void GVbind2()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_cita, nombreCliente, apellidoCliente, apellidoMCliente, concat(nombreDoctor, ' ', apellidoDoctor, ' ', maternoDoctor) as 'nombre_doctor', tipo_cita, hora FROM recurrentes where fechaCita = '" + fecha + "'  AND status = 1", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridpacientes.DataSource = dr;
                    gridpacientes.DataBind();
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //doctor = Convert.ToInt32(DropDownList1.SelectedValue);
            //nameDoctor = dpdoctor.SelectedItem.Text;

            //ViewState["Codigo2"] = Convert.ToInt32(DropDownList1.SelectedValue);
        }

        protected void btnregistrar_Click(object sender, EventArgs e)
        {
            //if (dpdoctor.SelectedIndex == 0)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaInfo()", true);
            //}
            //else
            //{
            int result = 0;
            string nombre_completo = this.txtnombre.Text + "  " + this.txtpaterno.Text + "  " + this.txtmaterno.Text;
            string doctor = txtdoctor.Text;
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            string hora = DateTime.Now.ToString("HH:mm:ss");
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO registro_pacientes_llegada(nombre_paciente,nombre_doctor,tipo_cita,fecha_ingreso,hora_ingreso,ingresado) VALUES ('" + nombre_completo.ToUpper() + "','" + doctor + "','" + txttipo.Text + "','" + fecha + "','" + hora + "',0)", con);
            SqlCommand cmd1 = new SqlCommand("UPDATE recurrentes set status = 0 where id_cita = '" + txtcita.Text + "'", con);
            result = cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            if (result > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true);
                this.txtnombre.Text = string.Empty;
                this.txtpaterno.Text = string.Empty;
                this.txtmaterno.Text = string.Empty;
                this.txtdoctor.Text = string.Empty;
                this.txtcita.Text = string.Empty;
                this.txthora.Text = string.Empty;
                this.txttipo.Text = string.Empty;

                GVbind();
                GVbind2();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaerror()", true);
                this.txtnombre.Text = string.Empty;
                this.txtpaterno.Text = string.Empty;
                this.txtmaterno.Text = string.Empty;
                this.txtdoctor.Text = string.Empty;
                this.txtcita.Text = string.Empty;
                this.txthora.Text = string.Empty;
                this.txttipo.Text = string.Empty;
            }
            //}
        }
        protected void gridpacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtcita.Text = gridpacientes.SelectedRow.Cells[0].Text;
            txtnombre.Text = HttpUtility.HtmlDecode(gridpacientes.SelectedRow.Cells[1].Text);
            txtpaterno.Visible = true;
            txtpaterno.Text = HttpUtility.HtmlDecode(gridpacientes.SelectedRow.Cells[2].Text);
            txtmaterno.Visible = true;
            txtmaterno.Text = HttpUtility.HtmlDecode(gridpacientes.SelectedRow.Cells[3].Text);
            txtdoctor.Text = HttpUtility.HtmlDecode(gridpacientes.SelectedRow.Cells[4].Text);
            txttipo.Text = HttpUtility.HtmlDecode(gridpacientes.SelectedRow.Cells[5].Text);
            txthora.Text = HttpUtility.HtmlDecode(gridpacientes.SelectedRow.Cells[6].Text);

            gridpacientes.SelectedIndex = -1;
        }

        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
            Session.Abandon();
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