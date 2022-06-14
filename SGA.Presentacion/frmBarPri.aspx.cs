using SGA.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion
{
    public partial class frmBarPri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            clsUsuario ObjetoUsuario = new clsUsuario();
            ObjetoUsuario = (clsUsuario)(Session["DatosUsuarioSession"]);

            lblUsuario.Text = ObjetoUsuario.cUsuario;
            lblNombres.Text = "";
            lblFecha.Text = ObjetoUsuario.dFecSystem.ToLongDateString() +" - Oficina: " + ObjetoUsuario.cNombreAge;
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            string strScript = "";
            strScript += "<script type='text/javascript'>";
            strScript += "top.location.href = ";
            strScript += "'" + "frmPrincipal.aspx" + "'";
            strScript += "</script>";
            ClientScript.RegisterClientScriptBlock(typeof(string), "RetMismo", strScript, false);
        }

        protected void btnCerrSession_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            string strScript = "";
            strScript += "<script type='text/javascript' >";
            strScript += "top.location.href = ";
            strScript += "'" + "frmInicioBoot.aspx" + "'";
            strScript += "</script>";
            ClientScript.RegisterClientScriptBlock(typeof(string), "Inicio", strScript, false);
        }

        protected void btnMapaProceso_Click(object sender, EventArgs e)
        {
            string strScript = "";
            strScript += "<script type='text/javascript'>";
            strScript += "window.open('MapaProceso/index.htm' , 'Mapa Proceso') ";
            strScript += "</script>";
            ClientScript.RegisterClientScriptBlock(typeof(string), "AbrirPagina", strScript, false);
        }
    }
}