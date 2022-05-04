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
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Drawing;
using System.Data.OleDb;

namespace TISIT
{
    public partial class login : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=mssql-47235-0.cloudclusters.net,19442;Initial Catalog=TISIT;Persist Security Info=True;User ID=admin;Password=Prueba123");

        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string query1 = "SELECT contra FROM usuarios WHERE usuario='" + txtcorreo.Text + "'";
            SqlCommand sqlcmd1 = new SqlCommand(query1, con);
            String pass = Convert.ToString(sqlcmd1.ExecuteScalar());

            string query2 = "SELECT usuario FROM usuarios WHERE usuario='" + txtcorreo.Text + "'";
            SqlCommand sqlcmd2 = new SqlCommand(query2, con);
            String usu = Convert.ToString(sqlcmd2.ExecuteScalar());
            con.Close();

            //Response.Write("<script>alert('Pass1: " + pass +"')</script>");

            using (SqlConnection conn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                if (txtcontra.Text == "" || txtcorreo.Text == "")
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaCamposVacios()", true);
                    Response.Redirect("login.aspx");
                }
                else if (txtcontra.Text != "" || txtcorreo.Text != "")
                {
                    if (txtcorreo.Text != usu)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
                        Response.Redirect("login.aspx");
                    }
                    else if (txtcorreo.Text == usu)
                    {
                        String pass2 = Desencripta(pass);

                        if (txtcontra.Text == pass2)
                        {
                            conn3.Open();

                            string query = "SELECT COUNT(1) FROM usuarios WHERE usuario='" + txtcorreo.Text + "' AND contra= '" + Encripta(txtcontra.Text) + "'";
                            SqlCommand sqlcmd = new SqlCommand(query, conn3);
                            int count = Convert.ToInt32(sqlcmd.ExecuteScalar());

                            if (count == 1)
                            {
                                Session["correo"] = txtcorreo.Text.Trim();
                                Response.Redirect("inicio.aspx");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertaError()", true);
                            Response.Redirect("login.aspx");
                        }
                    }
                }
            }
        }
    }
}