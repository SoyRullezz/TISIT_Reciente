
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace TISIT
{
    public partial class calendarioadmin : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        int doctor = 0;
        string nameDoctor;
        string status = "";
        string cita;

        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        protected void Page_Load(object sender, EventArgs e)
        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);

            if (!IsPostBack)
            {
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Seleccionar", "0"));

                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }

            //permisosGenerales();
            //perm();
        }
        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
            Session.Abandon();
        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime control1 = DateTime.Now;
            if (e.Day.IsToday)
            {
                e.Cell.Text = "Hoy";
                e.Day.IsSelectable = true;
            }

            if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                e.Day.IsSelectable = false;
            }


            if (e.Day.Date < control1)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.LightGray;
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //para mostrar fecha
            txtCalendar.Text = Calendar1.SelectedDate.ToShortDateString();
            Label16.Text = "";
            txtNombreDoctor.Text = "";
            txtApellidoDoctor.Text = "";
            txtNpaciente.Text = "";
            txtAPpaciente.Text = "";
            txtAMpaciente.Text = "";
            txtfechaelegida.Text = "";
            txthoralegida.Text = "";
            Edadpaciente.Text = "";
            txtCorreo.Text = "";
            txttelefono.Text = "";
            txtmensaje.Text = "";
            txtmotivo.Text = "";
            DropDownList1.SelectedIndex = 0;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            doctor = Convert.ToInt32(DropDownList1.SelectedValue);
            nameDoctor = DropDownList1.SelectedItem.Text;

            ViewState["Codigo2"] = Convert.ToInt32(DropDownList1.SelectedValue);
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cita = DropDownList2.SelectedItem.Text;

            //ViewState["Codigo2"] = Convert.ToInt32(DropDownList1.SelectedValue);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string inf = txtCalendar.Text;
            if (!string.IsNullOrEmpty(inf))
            {
                DateTime dia1 = Convert.ToDateTime(inf.ToString());
                byte dia2 = (byte)dia1.DayOfWeek;
                if (doctor == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "noselectDoc()", true);
                }
                else
                {

                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[2] {
                            new DataColumn("hora", typeof(string)),
                            new DataColumn("dis", typeof(string)) });

                    //fin de semana
                    if (dia2 == 6)
                    {
                        for (int i = 9; i <= 15; i++)
                        {

                            using (SqlConnection sqlcon = new SqlConnection(cs))
                            {
                                sqlcon.Open();
                                string query = "select count(id_cliente) from recurrentes where id_doctor=@id_doc and fechaCita=@fechacita and hora=@hora";
                                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                                sqlcmd.Parameters.AddWithValue("@id_doc", doctor);
                                sqlcmd.Parameters.AddWithValue("@fechacita", $"{inf.Substring(6, 4)}/{inf.Substring(3, 2)}/{inf.Substring(0, 2)}");
                                sqlcmd.Parameters.AddWithValue("@hora", $"{i}:00:00");
                                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                                if (count == 0)
                                {
                                    status = "Disponible";
                                    dt.Rows.Add($"{i}:00:00", status);
                                }
                                //if (count == 1)
                                //{
                                //    status = "No Disponible";
                                //}

                                GridView1.DataSource = dt;
                                GridView1.DataBind();

                                sqlcon.Close();
                            }
                        }
                    }
                    //entre semana
                    if (dia2 < 6)
                    {
                        for (int i = 9; i <= 18; i++)
                        {

                            using (SqlConnection sqlcon = new SqlConnection(cs))
                            {
                                sqlcon.Open();
                                string query = "select count(id_cliente) from recurrentes where id_doctor=@id_doc and fechaCita=@fechacita and hora=@hora";
                                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                                sqlcmd.Parameters.AddWithValue("@id_doc", doctor);
                                sqlcmd.Parameters.AddWithValue("@fechacita", $"{inf.Substring(6, 4)}/{inf.Substring(3, 2)}/{inf.Substring(0, 2)}");//esta agregar 
                                sqlcmd.Parameters.AddWithValue("@hora", $"{i}:00:00");
                                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                                if (count == 0)
                                {
                                    status = "Disponible";
                                    dt.Rows.Add($"{i}:00:00", status);
                                }
                                //if (count == 1)
                                //{
                                //    status = "No Disponible";
                                //}

                                GridView2.DataSource = dt;
                                GridView2.DataBind();

                                sqlcon.Close();
                            }
                        }
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", $"horarios({inf.Substring(6, 4)},{inf.Substring(3, 2)},{inf.Substring(0, 2)})", true);
                    Label16.Text = nameDoctor.ToString();
                    DropDownList1.SelectedIndex = 0;
                    status = "";


                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "noDatoCalendar()", true);
                DropDownList1.SelectedIndex = 0;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //datos del totor
            String[] datoscortado = Label16.Text.Split(' ');
            txtNombreDoctor.Text = datoscortado[0];
            txtApellidoDoctor.Text = datoscortado[1];
            txtApellidoMaterno.Text = datoscortado[2];

            //LLama la fecha requerida
            txtfechaelegida.Text = Calendar1.SelectedDate.ToShortDateString();
            //txtfechaelegida.Text = txtCalendar.Text;

            //trae la fecha requerida
            if (GridView2.Rows.Count == 0)
            {
                //Hora elegida
                txthoralegida.Text = GridView1.SelectedRow.Cells[0].Text;
            }
            else
            {
                //Hora elegida
                txthoralegida.Text = GridView2.SelectedRow.Cells[0].Text;
            }
        }

        protected void datasend_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNombreDoctor.Text) && !String.IsNullOrEmpty(txthoralegida.Text))
            {
                if (!string.IsNullOrEmpty(txtNpaciente.Text))
                {
                    if (!string.IsNullOrEmpty(txtAPpaciente.Text))
                    {
                        if (!string.IsNullOrEmpty(Edadpaciente.Text))

                        {

                            Boolean valor;
                            String expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                            if (Regex.IsMatch(txtCorreo.Text, expresion))
                            {
                                if (Regex.Replace(txtCorreo.Text, expresion, String.Empty).Length == 0)
                                {
                                    valor = true;
                                }
                                else
                                {
                                    valor = false;
                                }
                            }
                            else
                            {
                                valor = false;
                            }
                            if (valor)
                            {
                                if (!string.IsNullOrEmpty(txttelefono.Text))
                                {
                                    if (!string.IsNullOrEmpty(txtmotivo.Text))
                                    {

                                        //llenamos los datos a la base de datos
                                        int result = 0;
                                        //variable random para id cliente
                                        Random r = new Random();

                                        using (SqlConnection con = new SqlConnection(cs))
                                        {
                                            con.Open();
                                            System.Diagnostics.Debug.WriteLine(ViewState["Codigo"]);
                                            string fecha = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(txtfechaelegida.Text));

                                            SqlCommand cmd = new SqlCommand("INSERT INTO recurrentes VALUES ('" + r.Next(1, 50) + "', '" + Convert.ToInt32(ViewState["Codigo2"]) + "', '" + txtNombreDoctor.Text + "', '" + txtApellidoDoctor.Text + "', '" + txtApellidoMaterno.Text + "', '" + txtNpaciente.Text + "', '" + txtAPpaciente.Text + "', '" + txtAMpaciente.Text + "', '" + Convert.ToInt32(Edadpaciente.Text) + "', '" + txtCorreo.Text + "', '" + Convert.ToInt64(txttelefono.Text) + "', '" + fecha + "', '" + txthoralegida.Text + "', '" + cita + "', '" + txtmotivo.Text + "', '" + txtmensaje.Text + "', ' 1 ')", con);
                                            result = cmd.ExecuteNonQuery();
                                            if (result > 0)
                                            {
                                                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "Exito()", true);
                                                this.txtNombreDoctor.Text = string.Empty;
                                                this.txtApellidoDoctor.Text = string.Empty;
                                                this.txtApellidoMaterno.Text = string.Empty;
                                                this.txtNpaciente.Text = string.Empty;
                                                this.txtAPpaciente.Text = string.Empty;
                                                this.txtAMpaciente.Text = string.Empty;
                                                this.txtfechaelegida.Text = string.Empty;
                                                this.txthoralegida.Text = string.Empty;
                                                this.txtmotivo.Text = string.Empty;
                                                this.txtmensaje.Text = string.Empty;
                                                this.txtCorreo.Text = string.Empty;
                                                this.Edadpaciente.Text = string.Empty;
                                                this.txttelefono.Text = string.Empty;
                                                this.Label16.Text = string.Empty;
                                                //limpiar el calendario 
                                                Calendar1.SelectedDates.Clear();
                                                txtCalendar.Text = string.Empty;

                                            }
                                            else
                                            {
                                                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "Algiosaliomal()", true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "noDatoMolestia()", true);
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "Telefono()", true);
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "Correo()", true);
                            }

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "Edad()", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "ApellidoPaciente()", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "NombrePaciente()", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "llenaCampos()", true);
                Label16.Text = String.Empty;
            }

            //Response.Write("<script>alert('TIPO DE CITA: " + cita + "')</script>");
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