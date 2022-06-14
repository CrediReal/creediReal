using SGA.LogicaNegocio;
using SGA.Utilitarios;
using RPT.CapaNegocio;
using SGA.ENTIDADES;
using GEN.CapaNegocio;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CAJ.CapaNegocio;

namespace SGA.Presentacion.CAJA
{
    public partial class frmVerificarCierreAge : System.Web.UI.Page
    {

        #region Variables Globales
        clsWebJScript script = new clsWebJScript();
        clsCNControlOpe ControlOpe = new clsCNControlOpe();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Request.QueryString["usuario"] != null)
                {
                    Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }

                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            }
            catch (Exception)
            {
                throw;
            }
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            this.dtpFechaSis.SeleccionarFecha = objUsuario.dFecSystem;

        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            string msg = "";
            DateTime dfecpro = Convert.ToDateTime(this.dtpFechaSis.SeleccionarFecha.ToShortDateString());
            clsCNControlOpe VerEstCie = new clsCNControlOpe();
            DataTable tbEstCie = VerEstCie.VerificarEstadoCierre(dfecpro, ref msg);
            if (msg == "OK")
            {
                this.dtgEstCie.DataSource = tbEstCie;
                this.dtgEstCie.DataBind();
            }
            else
            {
                script.Mensaje(msg);
                //MessageBox.Show(msg, "Verificar Estado Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}