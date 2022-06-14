using Microsoft.Reporting.WebForms;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmCarteraVigente : System.Web.UI.Page
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
                if (Request.QueryString["perfil"] != null)
                {
                    hPerfil.Value=Request.QueryString["perfil"].ToString();
                }
                clsUsuario objUsuario;
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                else
                {
                    objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
                }
                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                cargarAsesor();
                this.cboAsesor.SelectedValue = objUsuario.idUsuario.ToString();
                if (Convert.ToInt32(hPerfil.Value).In(1, 2))
                {
                    cboAsesor.Enabled = true;
                }
                else
                {
                    cboAsesor.Enabled = false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarAsesor()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            GEN.CapaNegocio.clsCNPersonalCreditos ListaPersonalCre = new GEN.CapaNegocio.clsCNPersonalCreditos();
            DataTable dt = ListaPersonalCre.ListarPersonalCre(objUsuario.nIdAgencia, 0, 0);
            this.cboAsesor.DataSource = dt;
            cboAsesor.DataValueField = dt.Columns[0].ToString();
            cboAsesor.DataTextField = dt.Columns[1].ToString();
            cboAsesor.DataBind();
        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            String CodAna = "";

            CodAna = cboAsesor.SelectedValue;

            if (string.IsNullOrEmpty(CodAna))
            {
                script.Mensaje("Debe seleccionar por lo menos un asesor");
                return;
            }
            ListaDataSource.Add(new ReportDataSource("dtsCarAnalista", new RPT.CapaNegocio.clsRPTCNCredito().CNCarteraVigente(CodAna)));
            ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));
            
            string reportpath = "rptLisCreCliVig.rdlc";
            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }
    }
}