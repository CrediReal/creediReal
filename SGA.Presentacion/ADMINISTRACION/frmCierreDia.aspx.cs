using SGA.ENTIDADES;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.ADMINISTRACION
{
    public partial class frmCierreDia : System.Web.UI.Page
    {
        clsWebJScript script = new clsWebJScript();

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
                clsUsuario objUsuario;
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                else
                {
                    objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
                }
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                this.lblFechaSistema.Text = objUsuario.dFecSystem.ToLongDateString();
                cargarProcesos();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void cargarProcesos()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            CRE.CapaNegocio.clsCNCierreCredito LisProCie = new CRE.CapaNegocio.clsCNCierreCredito();
            DateTime dfecha = objUsuario.dFecSystem;
            DataTable dtVerificaCierreCaja = LisProCie.CNValidaCierreCajas(dfecha);
            if (Convert.ToInt32(dtVerificaCierreCaja.Rows[0]["nCajasApe"]) > 0)
            {
                script.Mensaje("Existen cajas o agencias sin cerrar, verificar");
                this.BotonProcesar1.Enabled = false;
                return;
            }
            DataTable dtLisProCie = LisProCie.CNdtCierreCre(dfecha);
            ViewState["dtLisProCie"] = dtLisProCie;
            dtgLisProCierre.DataSource = dtLisProCie;
            dtgLisProCierre.DataBind();
            if (dtgLisProCierre.Rows.Count <= 0)
            {
                BotonProcesar1.Enabled = false;
            }
        }

        protected void BotonProcesar1_Click(object sender, EventArgs e)
        {
            string cNomSp, cCtr = "E";
            int nNumPro = dtgLisProCierre.Rows.Count;
            var dtLisProCie=(DataTable)ViewState["dtLisProCie"];
            for (int i = 0; i < nNumPro; i++)
            {
                if (!(bool)dtLisProCie.Rows[i]["lEstado"])
                {
                    CRE.CapaNegocio.clsCNCierreCredito EjeSpCierre = new CRE.CapaNegocio.clsCNCierreCredito();
                    cNomSp = dtLisProCie.Rows[i]["cStoreProc"].ToString().Trim();
                    int nIdProceso = Convert.ToInt32(dtLisProCie.Rows[i]["idProceso"]);
                    DataTable dtRetornoPro = EjeSpCierre.ProcesoCieDia(cNomSp, nIdProceso);

                    if (dtRetornoPro.Rows[0]["nResultado"].ToString() == "0")
                    {
                        string cMnesaje = dtRetornoPro.Rows[0]["cMensaje"].ToString();
                        script.Mensaje(cMnesaje);
                        cCtr = "E";
                        break;
                    }
                    else
                    {
                        dtLisProCie.Rows[i]["lEstado"] = true;
                        cCtr = "OK";

                    }
                }

            }

            if (cCtr == "OK")
            {
                BotonProcesar1.Visible = false;
                script.Mensaje("Proceso de Cierre a Culminado Satisfactoriamente.");                
            }
        }
    }
}