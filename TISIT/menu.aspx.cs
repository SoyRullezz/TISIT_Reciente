using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TISIT
{
    public partial class alta_usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["usuario"] == null) { Response.Redirect("login.aspx"); }
            lbluser.Text = HttpUtility.HtmlDecode((string)Session["usuario"]);
            lblnombre_bienvenida.Text = HttpUtility.HtmlDecode((string)Session["usuario"]);
        }
    }
}