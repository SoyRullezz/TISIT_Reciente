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
    public partial class alta_personal : System.Web.UI.Page
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

            //perm();
            //permisosGenerales();
        }
        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
            Session.Abandon();
        }
        public bool ExisteEmpleado(string noEmpleado)
        {
            bool revisar = false;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from alta_empleados where no_empleado= '" + noEmpleado + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    revisar = true;
                }
                dr.Close();
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
            }
            finally
            {
                con.Close();
            }

            return revisar;
        }
        protected void GVbind()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT no_empleado,nombre,paterno,materno,fecha_nacimiento,curp,rfc,nss FROM alta_empleados where activo = 1", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridempleados.DataSource = dr;
                    gridempleados.DataBind();
                }
            }
        }
        protected void GVbind2()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_empleado,no_empleado,nombre,paterno,materno,fecha_nacimiento,curp,rfc,nss FROM alta_empleados where activo = 0", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridempleados2.DataSource = dr;
                    gridempleados2.DataBind();
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int result = 0;
            Random r = new Random();
            int num1 = 0;
            num1 = r.Next(1, 100000);
            string fecha = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(txtfecha.Text));
            if (ExisteEmpleado(Convert.ToString(num1)) == false)
            {
                if (ExisteEmpleado(Convert.ToString(txtnoempleado.Text)) == false)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO alta_empleados(no_empleado,nombre,paterno,materno,fecha_nacimiento,curp,rfc,nss,activo) VALUES ('" + txtnoempleado.Text + "','" + txtnombre.Text.ToUpper() + "', '" + txtpaterno.Text.ToUpper() + "', '" + txtmaterno.Text.ToUpper() + "', '" + fecha + "', '" + txtcurp.Text.ToUpper() + "', '" + txtrfc.Text.ToUpper() + "', '" + txtnss.Text.ToUpper() + "',1)", con);
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO usuarios(no_empleado, activo) VALUES ('" + txtnoempleado.Text + "',1)", con);

                    result = cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    if (result > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true);
                        this.txtnoempleado.Text = string.Empty;
                        this.txtnombre.Text = string.Empty;
                        this.txtpaterno.Text = string.Empty;
                        this.txtmaterno.Text = string.Empty;
                        this.txtfecha.Text = string.Empty;
                        this.txtcurp.Text = string.Empty;
                        this.txtrfc.Text = string.Empty;
                        this.txtnss.Text = string.Empty;
                        GVbind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "ErrorEmpleado()", true);
                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "ErrorID()", true);
            }
        }

        protected void gridempleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            guardardatos.Visible = true;
            txtnoempleado.Visible = true;
            Session["id_empleado"] = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[0].Text);
            txtnoempleado.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[1].Text);
            txtnombre.Visible = true;
            txtnombre.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[2].Text);
            txtpaterno.Visible = true;
            txtpaterno.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[3].Text);
            txtmaterno.Visible = true;
            txtmaterno.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[4].Text);
            txtfecha.Visible = true;
            txtfecha.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[5].Text);
            txtcurp.Visible = true;
            txtcurp.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[6].Text);
            txtrfc.Visible = true;
            txtrfc.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[7].Text);
            txtnss.Visible = true;
            txtnss.Text = HttpUtility.HtmlDecode(gridempleados.SelectedRow.Cells[8].Text);

            btneditar.Visible = true;
            btncancelaredit.Visible = true;
            btnguardar.Visible = false;
            gridempleados.SelectedIndex = -1;
        }

        protected void gridempleados2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = HttpUtility.HtmlDecode(gridempleados2.SelectedRow.Cells[1].Text);

            try
            {

                using (SqlConnection con = new SqlConnection(cs))
                {
                    int res = 0;
                    int res1 = 0;
                    int res2 = 0;
                    int res3 = 0;
                    int res4 = 0;

                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE alta_empleados SET activo = 1 where no_empleado = '" + id + "'", con);
                    SqlCommand cmd1 = new SqlCommand("UPDATE contrato_tisit SET activo = 1 where no_empleado = '" + id + "'", con);
                    SqlCommand cmd2 = new SqlCommand("UPDATE direccion_personal SET activo = 1 where no_empleado = '" + id + "'", con);
                    SqlCommand cmd3 = new SqlCommand("UPDATE documentacion SET activo = 1 where no_empleado = '" + id + "'", con);
                    SqlCommand cmd4 = new SqlCommand("UPDATE usuarios SET activo = 1 where no_empleado = '" + id + "'", con);

                    res = cmd.ExecuteNonQuery();
                    res1 = cmd1.ExecuteNonQuery();
                    res2 = cmd2.ExecuteNonQuery();
                    res3 = cmd3.ExecuteNonQuery();
                    res4 = cmd4.ExecuteNonQuery();

                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExitoEditar()", true);
                        gridempleados2.SelectedIndex = -1;
                        Session.Clear();
                        GVbind();
                        GVbind2();

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "ErrorActualizar()", true);
                        GVbind2();
                    }

                }
            }
            catch
            {

            }
        }

        protected void btncancelaredit_Click(object sender, EventArgs e)
        {
            this.txtnoempleado.Text = string.Empty;
            this.txtnombre.Text = string.Empty;
            this.txtpaterno.Text = string.Empty;
            this.txtmaterno.Text = string.Empty;
            this.txtfecha.Text = string.Empty;
            this.txtcurp.Text = string.Empty;
            this.txtrfc.Text = string.Empty;
            this.txtnss.Text = string.Empty;
            btncancelaredit.Visible = false;
            btneditar.Visible = false;
            btnguardar.Visible = true;
            //guardardatos.Visible = true;
            gridempleados.SelectedIndex = -1;
        }

        protected void btneditar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                int res = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE alta_empleados SET no_empleado = '" + txtnoempleado.Text + "', nombre = '" + txtnombre.Text.ToUpper() + "',paterno = '" + txtpaterno.Text.ToUpper() + "',materno = '" + txtmaterno.Text.ToUpper() + "', fecha_nacimiento= '" + txteditfecha.Text + "', curp = '" + txtcurp.Text.ToUpper() + "', rfc = '" + txtrfc.Text.ToUpper() + "', nss = '" + txtnss.Text.ToUpper() + "' where id_empleado = '" + Session["id_empleado"] + "'", con);

                res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExitoEditar()", true);
                    this.txtnoempleado.Text = string.Empty;
                    this.txtnombre.Text = string.Empty;
                    this.txtpaterno.Text = string.Empty;
                    this.txtmaterno.Text = string.Empty;
                    this.txtfecha.Text = string.Empty;
                    this.txteditfecha.Text = string.Empty;
                    this.txtcurp.Text = string.Empty;
                    this.txtrfc.Text = string.Empty;
                    this.txtnss.Text = string.Empty;
                    guardardatos.Visible = false;

                    //consultaempleado.Visible = true;
                    //gridempleados.Visible = true;
                    //guardardatos.Visible = false;
                    //txtnoempleado.Visible = false;
                    //txtnombre.Visible = false;
                    //txtpaterno.Visible = false;
                    //txtmaterno.Visible = false;
                    //txtfecha.Visible = false;
                    //txtcurp.Visible = false;
                    //txtnss.Visible = false;
                    //txtrfc.Visible = false;
                    //btnguardar.Visible = false;
                    ////Label1.Visible = true;
                    ////txtbuscar.Visible = true;
                    ////btnbuscar.Visible = true;
                    //btncancelar.Visible = true;
                    gridempleados.SelectedIndex = -1;
                    GVbind();
                    Session.Clear();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "ErrorActualizar()", true);
                }
            }
        }
        protected void gridempleados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //consultaempleado.Visible = true;
            int id = Convert.ToInt32(gridempleados.DataKeys[e.RowIndex].Value);

            //int id = Convert.ToInt32(gridempleados.DataKeyNames);

            try
            {

                using (SqlConnection con = new SqlConnection(cs))
                {

                    int res = 0;
                    int res1 = 0;
                    int res2 = 0;
                    int res3 = 0;
                    int res4 = 0;

                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE alta_empleados SET activo = 0 where no_empleado = '" + id + "'", con);
                    SqlCommand cmd1 = new SqlCommand("UPDATE contrato_tisit SET activo = 0 where no_empleado = '" + id + "'", con);
                    SqlCommand cmd2 = new SqlCommand("UPDATE direccion_personal SET activo = 0 where no_empleado = '" + id + "'", con);
                    SqlCommand cmd3 = new SqlCommand("UPDATE documentacion SET activo = 0 where no_empleado = '" + id + "'", con);
                    SqlCommand cmd4 = new SqlCommand("UPDATE usuarios SET activo = 0 where no_empleado = '" + id + "'", con);
                    res = cmd.ExecuteNonQuery();
                    res1 = cmd1.ExecuteNonQuery();
                    res2 = cmd2.ExecuteNonQuery();
                    res3 = cmd3.ExecuteNonQuery();
                    res4 = cmd3.ExecuteNonQuery();

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