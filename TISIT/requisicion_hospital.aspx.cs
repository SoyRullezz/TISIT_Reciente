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
    public partial class requisicion_hospital : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        DataTable dt = new DataTable();
        public decimal totalcantidad;
        public decimal totalpedir;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbcorreo.Text = HttpUtility.HtmlDecode((string)Session["correo"]);

            conteo();

            if (!Page.IsPostBack)
            {
                GVbind2();
                addInfo();
            }

            //permisosGenerales();
            //perm();
        }

        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
            Session.Abandon();
        }

        protected void conteo()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(folio) FROM requisiciones", con);
            int nFact = Convert.ToInt32(cmd.ExecuteScalar());
            nFact = nFact + 1;
            txtfolio.Text = nFact.ToString();
            con.Close();
        }

        protected void addInfo()
        {
            if (Session["datos_productos"] == null)
            {
                dt.Columns.Add("folio");
                dt.Columns.Add("id_producto");
                dt.Columns.Add("producto");
                dt.Columns.Add("detalle");
                dt.Columns.Add("unidad");
                dt.Columns.Add("pedido");
                dt.Columns.Add("motivo");
                Session["datos_productos"] = dt;
            }
        }

        protected void GVbind2()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM invProdHos", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    gridproductos.DataSource = dr;
                    gridproductos.DataBind();
                }
            }
        }

        protected void gridproductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gridproductos.SelectedRow.Cells[0].Text;
            txtnoproducto.Text = gridproductos.SelectedRow.Cells[0].Text;

            con.Open();
            string query1 = "SELECT cantidad FROM invProdHos where id_producto = " + id;
            SqlCommand cmd1 = new SqlCommand(query1, con);
            decimal stock_actual = Convert.ToDecimal(cmd1.ExecuteScalar());

            string query2 = "SELECT pedido FROM invProdHos where id_producto = " + id;
            SqlCommand cmd2 = new SqlCommand(query2, con);
            decimal pedido = Convert.ToDecimal(cmd2.ExecuteScalar());

            decimal cantidad_posible = stock_actual - pedido;
            con.Close();

            txtproducto.Text = HttpUtility.HtmlDecode(gridproductos.SelectedRow.Cells[1].Text);
            txtdetalle.Text = HttpUtility.HtmlDecode(gridproductos.SelectedRow.Cells[2].Text);
            txtcantidad.Text = Convert.ToString(cantidad_posible);
            txtunidad.Text = HttpUtility.HtmlDecode(gridproductos.SelectedRow.Cells[5].Text);
            
            gridproductos.SelectedIndex = -1;
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

        protected void btncotizar_Click(object sender, EventArgs e)
        {
            dt = (DataTable)Session["datos_productos"];

            decimal pedido = Convert.ToDecimal(txtpedir.Text);
            decimal cantidad = Convert.ToDecimal(txtcantidad.Text);

            if ((pedido > cantidad) || (pedido == 0))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
            }
            else
            {
                dt.Rows.Add(txtfolio.Text, txtnoproducto.Text, txtproducto.Text, txtdetalle.Text, txtunidad.Text, txtpedir.Text, txtmotivo.Text); ;
                gridreq.DataSource = dt;
                gridreq.DataBind();
            }
            decimal stock = cantidad - pedido;

            con.Open();

            string query1 = "SELECT pedido FROM invProdHos where id_producto = " + txtnoproducto.Text;
            SqlCommand cmd1 = new SqlCommand(query1, con);
            decimal pedido_acumulado = Convert.ToDecimal(cmd1.ExecuteScalar());

            decimal pedido_actual = pedido + pedido_acumulado;

            int result3;
            SqlCommand cmd4 = new SqlCommand("UPDATE invProdHos SET pedido = '" + pedido_actual + "' where id_producto = '" + txtnoproducto.Text + "' ", con);
            result3 = cmd4.ExecuteNonQuery();

            con.Close();

            txtnoproducto.Text = string.Empty;
            txtproducto.Text = string.Empty;
            txtdetalle.Text = string.Empty;
            txtcantidad.Text = string.Empty;
            txtpedir.Text = string.Empty;
            txtunidad.Text = string.Empty;
            txtmotivo.Text = string.Empty;  

            GVbind2();
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                int activo = 0;

                SqlCommand agregar = new SqlCommand("insert into req_material values(@FOLIO,@ID_PRODUCTO,@NOMBRE,@DETALLE,@UNIDAD,@PEDIDO,@MOTIVO, @ACTIVO)", con);
                con.Open();

                foreach (GridViewRow row in gridreq.Rows)
                {
                    agregar.Parameters.Clear();


                    agregar.Parameters.AddWithValue("@FOLIO", Convert.ToInt32(row.Cells[0].Text));
                    agregar.Parameters.AddWithValue("@ID_PRODUCTO", Convert.ToInt32(row.Cells[1].Text));
                    agregar.Parameters.AddWithValue("@NOMBRE", Convert.ToString(row.Cells[2].Text));
                    agregar.Parameters.AddWithValue("@DETALLE", Convert.ToString(HttpUtility.HtmlDecode(row.Cells[3].Text.ToUpper())));
                    agregar.Parameters.AddWithValue("@UNIDAD", Convert.ToString(row.Cells[4].Text.ToUpper()));
                    agregar.Parameters.AddWithValue("@PEDIDO", Convert.ToDecimal(row.Cells[5].Text));
                    agregar.Parameters.AddWithValue("@MOTIVO", Convert.ToString(row.Cells[6].Text));
                    agregar.Parameters.AddWithValue("@ACTIVO", Convert.ToInt32(activo));
                    agregar.ExecuteNonQuery();
                }

                SqlCommand agregar1 = new SqlCommand("insert into requisiciones values(@FOLIO,@FECHA,@ACTIVO)", con);
                int valor = gridreq.Rows.Count;
                string folio = gridreq.Rows[valor - 1].Cells[0].Text;
                string servicio = gridreq.Rows[valor - 1].Cells[3].Text;

                string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                int activo1 = 0;

                agregar1.Parameters.Clear();
                agregar1.Parameters.AddWithValue("@FOLIO", Convert.ToInt32(folio));
                agregar1.Parameters.AddWithValue("@FECHA", Convert.ToString(fecha));
                agregar1.Parameters.AddWithValue("@ACTIVO", Convert.ToInt32(activo1));
                agregar1.ExecuteNonQuery();

                con.Close();

                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true);

                txtfolio.Text = string.Empty;

                gridreq.SelectedIndex = 0;
                gridreq.DataBind();
                Session.Remove("datos_productos");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaReq()", true);
                throw;

            }
            finally
            {
                conteo();
                addInfo();
            }
        }

        protected void btnpedir_Click(object sender, EventArgs e)
        {

            con.Open();

            string queryad = "SELECT cantidad FROM invProdHos WHERE id_producto= '" + txtnoproducto.Text + "'";
            SqlCommand cmdad = new SqlCommand(queryad, con);
            decimal cantidad = Convert.ToDecimal(cmdad.ExecuteScalar());

            string queryad1 = "SELECT pedido FROM invProdHos WHERE id_producto= '" + txtnoproducto.Text + "'";
            SqlCommand cmdad1 = new SqlCommand(queryad1, con);
            decimal pedido = Convert.ToDecimal(cmdad1.ExecuteScalar());

            totalcantidad = cantidad - pedido;
            totalpedir = pedido + Convert.ToDecimal(txtpedir.Text);

            con.Close();

            if(Convert.ToDecimal(txtpedir.Text) <= totalcantidad)
            {
                int result = 0;
                string fecha = DateTime.Now.ToString("yyyy-MM-dd");
                con.Open();
                //SqlCommand cmd = new SqlCommand("INSERT INTO req_material(nombre,cantidad,unidad,pedido) VALUES ('" + txtproducto.Text.ToUpper() + "','" + txtdetalle.Text.ToUpper() + "','" + txtcantidad.Text.ToUpper() + "','" + txtunidad.Text.ToUpper() + "','" + txtubicacion.Text.ToUpper() + "')", con);
                SqlCommand cmd1 = new SqlCommand("UPDATE invProdHos set pedido = '" + totalpedir + "' WHERE id_producto= '" + txtnoproducto.Text + "'", con);

                result = cmd1.ExecuteNonQuery();

                if (result > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaExito()", true);
                    this.txtproducto.Text = string.Empty;
                    this.txtcantidad.Text = string.Empty;
                    this.txtunidad.Text = string.Empty;
                    this.txtnoproducto.Text = string.Empty;
                    this.txtpedir.Text = string.Empty;

                    GVbind2();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
                    this.txtproducto.Text = string.Empty;
                    this.txtcantidad.Text = string.Empty;
                    this.txtunidad.Text = string.Empty;
                    this.txtnoproducto.Text = string.Empty;
                    this.txtpedir.Text = string.Empty;
                }
            }
            else if (Convert.ToDecimal(txtpedir.Text) > totalcantidad || Convert.ToDecimal(txtpedir.Text) == 0 )
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
                this.txtnoproducto.Text = string.Empty;
                this.txtproducto.Text = string.Empty;
                this.txtcantidad.Text = string.Empty;
                this.txtunidad.Text = string.Empty;
                this.txtpedir.Text = string.Empty;
            }
            

        }

        protected void gridproductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal cantidad = Convert.ToDecimal(e.Row.Cells[3].Text);
                decimal pedido = Convert.ToDecimal(e.Row.Cells[4].Text);


                //con.Open();
                //string queryad = "SELECT pedido FROM invProdHos WHERE usuario= '" + id + "'";
                //SqlCommand cmdad = new SqlCommand(queryad, con);
                //decimal pedido = Convert.ToDecimal(cmdad.ExecuteScalar());
                //con.Close();

                decimal mitad = cantidad * 0.5m;
                decimal cuarto = cantidad * 0.75m;

                if (pedido == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    e.Row.ForeColor = System.Drawing.Color.DarkGreen;
                }

                if (pedido >= cantidad)
                {
                    e.Row.BackColor = System.Drawing.Color.Crimson;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }

                if ((pedido != 0) && (pedido > 0) && (pedido < cuarto))
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                }

                if ((pedido < cantidad) && (pedido >= cuarto))
                {
                    e.Row.BackColor = System.Drawing.Color.DarkOrange;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
        }

        protected void GridProductos_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id = gridreq.SelectedRow.Cells[1].Text;
            decimal pedido = Convert.ToDecimal(gridreq.SelectedRow.Cells[5].Text);

            con.Open();

            string query1 = "SELECT pedido FROM invProdHos where id_producto = '" + id + "' ";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            decimal pedido_acumulado = Convert.ToDecimal(cmd1.ExecuteScalar());

            decimal pedido_nuevo = pedido_acumulado - pedido;

            int result3;
            SqlCommand cmd4 = new SqlCommand("UPDATE invProdHos SET pedido = '" + pedido_nuevo + "' where id_producto = '" + id + "' ", con);
            result3 = cmd4.ExecuteNonQuery();

            con.Close();

            int id_fila = Convert.ToInt32(gridreq.SelectedRow.RowIndex);
            dt = (DataTable)Session["datos_productos"];
            dt.Rows.RemoveAt(id_fila);


            Session["datos_productos"] = dt;
            gridreq.DataSource = dt;
            gridreq.DataBind();

            gridreq.SelectedIndex = -1;

            GVbind2();

        }
    }


}