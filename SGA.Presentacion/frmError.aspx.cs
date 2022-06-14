using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion
{
    public partial class frmError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            if (Request.QueryString["error"] != null)
            {
                lblError.Text = Request.QueryString["error"].ToString();
            }
        }

        protected void BotonAtras1_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            string strScript = "";
            strScript += "<script language='Jscript'>";
            strScript += "top.location.href = ";
            strScript += "'" + "frmInicioBoot.aspx" + "'";
            strScript += "</script>";
            ClientScript.RegisterStartupScript(typeof(string), "retInicio", strScript);    
        }
    }
}