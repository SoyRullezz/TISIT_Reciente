using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace TISIT
{
    public partial class permisos : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        public string puesto;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);

            pnladmin.Visible = false;
            pnlmed.Visible = false;
            pnlprod.Visible = false;
            pnldoc.Visible = false;
            lbpuesto.Visible = false;

            cdespeciales.Visible = false;

            if (!IsPostBack)
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
                SqlCommand cmd = new SqlCommand("select no_empleado,concat(nombre, ' ', apaterno, ' ', amaterno) as nombre,puesto FROM contrato_tisit where activo = 1", con);
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
            txtempleado.Text = GridView1.SelectedRow.Cells[0].Text;
            txtbuscar.Text = GridView1.SelectedRow.Cells[1].Text;

            lbpuesto.Text = GridView1.SelectedRow.Cells[2].Text;
        }

        public byte[] Clave = Encoding.ASCII.GetBytes("Tenkui2202");
        public byte[] IV = Encoding.ASCII.GetBytes("Devjoker7.37hAES");

        public string Encripta(string Cadena)
        {

            byte[] inputBytes = Encoding.ASCII.GetBytes(Cadena);
            byte[] encripted;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(Clave, IV), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }

        public string Desencripta(string Cadena)
        {
            byte[] inputBytes = Convert.FromBase64String(Cadena);
            byte[] resultBytes = new byte[inputBytes.Length];
            string textoLimpio = String.Empty;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(Clave, IV), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(objCryptoStream, true))
                    {
                        textoLimpio = sr.ReadToEnd();
                    }
                }
            }
            return textoLimpio;
        }
        protected void btnguardar_Click(object sender, EventArgs e)
        {
            String pass = Encripta("1234");
            radio();
            //String pass = "";

            string username = lbpuesto.Text + txtempleado.Text;
            con.Open();

            int result3 = 1;
            SqlCommand cmd4 = new SqlCommand("UPDATE usuarios SET usuario = '" + username + "', contra = '" + pass + "', " +
                "admin = '" + admin.Text + "', adminlect = '0', adminbloq = '" + adminbloq.Text + "', " +
                "med = '" + med.Text + "', medlect = '0', medbloq = '" + medbloq.Text + "', " +
                "docpac = '" + docpac.Text + "', docpaclect = '0', docpacbloq = '" + docpacbloq.Text + "', " +
                "prod = '" + prod.Text + "', prodlect = '0', prodbloq = '" + prodbloq.Text + "' " +
                "where no_empleado = '" + txtempleado.Text + "' ", con);
            result3 = cmd4.ExecuteNonQuery();

            admin.Checked = false;
            //adminlect.Checked = false;
            adminbloq.Checked = true;
            med.Checked = false;
            //medlect.Checked = false;
            medbloq.Checked = true;
            prod.Checked = false;
            //prodlect.Checked = false;
            prodbloq.Checked = true;
            docpac.Checked = false;
            docpacbloq.Checked = true;

            admin.Text = "";
            //adminlect.Text = "";
            adminbloq.Text = "";
            med.Text = "";
            //medlect.Text = "";
            medbloq.Text = "";
            prod.Text = "";
            //prodlect.Text = "";
            prodbloq.Text = "";
            docpac.Text = "";
            docpacbloq.Text = "";

            if (result3 > 0)
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "randomtext", $"alertaUsuario({username})", true);
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true);
                Response.Write("<script>alert('EL USUARIO ES: " + username + "')</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
            }

            string queryad = "SELECT admin FROM usuarios where no_empleado = '" + txtempleado.Text + "' ";
            SqlCommand cmdad = new SqlCommand(queryad, con);
            bool ciertoad = Convert.ToBoolean(cmdad.ExecuteScalar());

            string queryadl = "SELECT adminlect FROM usuarios where no_empleado = '" + txtempleado.Text + "' ";
            SqlCommand cmdadl = new SqlCommand(queryadl, con);
            bool ciertoadl = Convert.ToBoolean(cmdadl.ExecuteScalar());

            if (ciertoad == true || ciertoadl == true)
            {
                pnladmin.Visible = true;
                cdespeciales.Visible = true;
                pnlpermisos.Visible = false;
                btnbuscar.Disabled = true;
            }

            string queryf = "SELECT med FROM usuarios where no_empleado = '" + txtempleado.Text + "' ";
            SqlCommand cmd1 = new SqlCommand(queryf, con);
            bool ciertop = Convert.ToBoolean(cmd1.ExecuteScalar());

            string queryfl = "SELECT medlect FROM usuarios where no_empleado = '" + txtempleado.Text + "' ";
            SqlCommand cmdfl = new SqlCommand(queryfl, con);
            bool ciertopl = Convert.ToBoolean(cmdfl.ExecuteScalar());

            if (ciertop == true || ciertopl == true)
            {
                pnlmed.Visible = true;
                cdespeciales.Visible = true;
                pnlpermisos.Visible = false;
                btnbuscar.Disabled = true;
            }

            string queryd = "SELECT prod FROM usuarios where no_empleado = '" + txtempleado.Text + "' ";
            SqlCommand cmdd = new SqlCommand(queryd, con);
            bool ciertod = Convert.ToBoolean(cmdd.ExecuteScalar());

            string querydl = "SELECT prodlect FROM usuarios where no_empleado = '" + txtempleado.Text + "' ";
            SqlCommand cmddl = new SqlCommand(querydl, con);
            bool ciertodl = Convert.ToBoolean(cmddl.ExecuteScalar());

            if (ciertod == true || ciertodl == true)
            {
                pnlprod.Visible = true;
                cdespeciales.Visible = true;
                pnlpermisos.Visible = false;
                btnbuscar.Disabled = true;
            }

            string queryds = "SELECT docpac FROM usuarios where no_empleado = '" + txtempleado.Text + "' ";
            SqlCommand cmdds = new SqlCommand(queryds, con);
            bool ciertods = Convert.ToBoolean(cmdds.ExecuteScalar());

            string querydsl = "SELECT docpaclect FROM usuarios where no_empleado = '" + txtempleado.Text + "' ";
            SqlCommand cmddsl = new SqlCommand(querydsl, con);
            bool ciertodsl = Convert.ToBoolean(cmddsl.ExecuteScalar());

            if (ciertods == true || ciertodsl == true)
            {
                pnldoc.Visible = true;
                cdespeciales.Visible = true;
                pnlpermisos.Visible = false;
                btnbuscar.Disabled = true;
            }


            con.Close();

        }

        protected void btnespeciales_Click(object sender, EventArgs e)
        {
            int result3 = 0;
            check();
            con.Open();

            SqlCommand cmd4 = new SqlCommand("UPDATE usuarios SET " +
                "usuario1 = '" + user.Text + "', dir = '" + dir.Text + "', lab = '" + lab.Text + "', per = '" + per.Text + "',  doc = '" + doc.Text + "', " +
                "docto = '" + docto.Text + "', pac = '" + pac.Text + "', cit = '" + cit.Text + "', ag = '" + ag.Text + "', cal = '" + cal.Text + "', " +
                "inv = '" + inv.Text + "', nprod = '" + nprod.Text + "', ven = '" + ven.Text + "', " +
                "sol = '" + sol.Text + "', cons = '" + cons.Text + "', ing = '" + ing.Text + "' " +
                "where no_empleado = '" + txtempleado.Text + "' ", con);
            result3 = cmd4.ExecuteNonQuery();

            if (result3 > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
            }

            this.txtbuscar.Text = string.Empty;
            this.txtempleado.Text = string.Empty;

            user.Checked = false;
            dir.Checked = false;
            lab.Checked = false;
            per.Checked = false;
            doc.Checked = false;
            docto.Checked = false;
            pac.Checked = false;
            cit.Checked = false;
            ag.Checked = false;
            cal.Checked = false;
            inv.Checked = false;
            nprod.Checked = false;
            ven.Checked = false;
            sol.Checked = false;
            cons.Checked = false;
            ing.Checked = false;

            user.Text = "";
            dir.Text = "";
            lab.Text = "";
            per.Text = "";
            doc.Text = "";
            docto.Text = "";
            pac.Text = "";
            cit.Text = "";
            ag.Text = "";
            cal.Text = "";
            inv.Text = "";
            nprod.Text = "";
            ven.Text = "";
            sol.Text = "";
            cons.Text = "";
            ing.Text = "";

            pnlpermisos.Visible = true;
            pnladmin.Visible = false;
            pnlmed.Visible = false;
            pnlprod.Visible = false;
            pnldoc.Visible = false;

            cdespeciales.Visible = false;
            btnbuscar.Disabled = false;

            con.Close();

        }

        private void radio()
        {
            if (admin.Checked)
            {
                admin.Text = "true";
                //adminlect.Text = "false";
                adminbloq.Text = "false";
            }
            //else if (adminlect.Checked)
            //{
            //    admin.Text = "false";
            //    adminlect.Text = "true";
            //    adminbloq.Text = "false";
            //}
            else if (adminbloq.Checked)
            {
                admin.Text = "false";
                //adminlect.Text = "false";
                adminbloq.Text = "true";
            }

            if (med.Checked)
            {
                med.Text = "true";
                //medlect.Text = "false";
                medbloq.Text = "false";
            }
            //else if (medlect.Checked)
            //{
            //    med.Text = "false";
            //    medlect.Text = "true";
            //    medbloq.Text = "false";
            //}
            else if (medbloq.Checked)
            {
                med.Text = "false";
                //medlect.Text = "false";
                medbloq.Text = "true";
            }

            if (docpac.Checked)
            {
                docpac.Text = "true";
                //docpaclect.Text = "false";
                docpacbloq.Text = "false";
            }
            //else if (docpaclect.Checked)
            //{
            //    docpac.Text = "false";
            //    docpaclect.Text = "true";
            //    docpacbloq.Text = "false";
            //}
            else if (docpacbloq.Checked)
            {
                docpac.Text = "false";
                //docpaclect.Text = "false";
                docpacbloq.Text = "true";
            }

            if (prod.Checked)
            {
                prod.Text = "true";
                //prodlect.Text = "false";
                prodbloq.Text = "false";
            }
            //else if (prodlect.Checked)
            //{
            //    prod.Text = "false";
            //    prodlect.Text = "true";
            //    prodbloq.Text = "false";
            //}
            else if (prodbloq.Checked)
            {
                prod.Text = "false";
                //prodlect.Text = "false";
                prodbloq.Text = "true";
            }

        }

        private void check()
        {

            if (user.Checked)
            {
                user.Text = "true";

            }
            else
            {
                user.Text = "false";
            }

            if (dir.Checked)
            {
                dir.Text = "true";

            }
            else
            {
                dir.Text = "false";
            }

            if (lab.Checked)
            {
                lab.Text = "true";

            }
            else
            {
                lab.Text = "false";
            }

            if (per.Checked)
            {
                per.Text = "true";
            }
            else
            {
                per.Text = "false";
            }

            if (doc.Checked)
            {
                doc.Text = "true";

            }
            else
            {
                doc.Text = "false";
            }

            //PERSONAL MEDICO Y CITAS

            if (docto.Checked)
            {
                docto.Text = "true";

            }
            else
            {
                docto.Text = "false";
            }

            if (pac.Checked)
            {
                pac.Text = "true";

            }
            else
            {
                pac.Text = "false";
            }

            if (cit.Checked)
            {
                cit.Text = "true";

            }
            else
            {
                cit.Text = "false";
            }

            if (ag.Checked)
            {
                ag.Text = "true";

            }
            else
            {
                ag.Text = "false";
            }

            if (cal.Checked)
            {
                cal.Text = "true";

            }
            else
            {
                cal.Text = "false";
            }

            //DOCUMENTOS Y SOLICITUDES

            if (sol.Checked)
            {
                sol.Text = "true";

            }
            else
            {
                sol.Text = "false";
            }

            if (cons.Checked)
            {
                cons.Text = "true";

            }
            else
            {
                cons.Text = "false";
            }

            if (ing.Checked)
            {
                ing.Text = "true";

            }
            else
            {
                ing.Text = "false";
            }

            //PRODUCTOS

            if (inv.Checked)
            {
                inv.Text = "true";

            }
            else
            {
                inv.Text = "false";
            }

            if (nprod.Checked)
            {
                nprod.Text = "true";

            }
            else
            {
                nprod.Text = "false";
            }

            if (ven.Checked)
            {
                ven.Text = "true";

            }
            else
            {
                ven.Text = "false";
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