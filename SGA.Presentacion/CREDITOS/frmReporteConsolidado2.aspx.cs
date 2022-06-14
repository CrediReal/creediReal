using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using SGA.Utilitarios;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmReporteConsolidado2 : System.Web.UI.Page
    {
        SGA.ENTIDADES.clsUsuario objUsuario;
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
                    hPerfil.Value = Request.QueryString["perfil"].ToString();
                }

                objUsuario = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];

                if (IsPostBack) return;
                
                cargarAsesor();

                if (Convert.ToInt32(hPerfil.Value) == 1 || Convert.ToInt32(hPerfil.Value) == 2)
                {
                    this.cboAsesor.SelectedValue = "0";
                    cboAsesor.Enabled = true;
                }
                else
                {
                    this.cboAsesor.SelectedValue = objUsuario.idUsuario.ToString();
                    cboAsesor.Enabled = false;
                }

                if (objUsuario.idUsuario == 2 || objUsuario.idUsuario == 21 || objUsuario.idUsuario == 36)
                {
                    btnResumenCartera.Visible = true;
                }
                else
                {
                    btnResumenCartera.Visible = false;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        private void cargarAsesor()
        {
            
            GEN.CapaNegocio.clsCNPersonalCreditos ListaPersonalCre = new GEN.CapaNegocio.clsCNPersonalCreditos();
            DataTable dt = ListaPersonalCre.ListarPersonalCre(objUsuario.nIdAgencia, Convert.ToInt32(hPerfil.Value), 0);
            this.cboAsesor.DataSource = dt;
            cboAsesor.DataValueField = dt.Columns[0].ToString();
            cboAsesor.DataTextField = dt.Columns[1].ToString();
            cboAsesor.DataBind();
        }

        protected void btnResumen_Click(object sender, EventArgs e)
        {
            try
            {
                List<ReportParameter> ListaParametros = new List<ReportParameter>();
                List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

                String CodAna = "";

                //foreach (DataRowView item in chklAsesores.CheckedItems)
                //{
                //    CodAna += (item.Row["idUsuario"] + ",");
                //}

                CodAna = cboAsesor.SelectedValue;

                if (string.IsNullOrEmpty(CodAna))
                {
                    script.Mensaje("Debe seleccionar por lo menos un asesor");
                    return;
                }

                int nDiaAtrMin = Convert.ToInt32(txtNroDia.Text);                
                //objUsuario = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
                int idUsuario = objUsuario.idUsuario == 2 || objUsuario.idUsuario == 36 ? Convert.ToInt32(CodAna) : objUsuario.idUsuario;

                string reportpath = "";

                ListaDataSource.Add(new ReportDataSource("dsConsolidado", new SGA.LogicaNegocio.clsCNCredito().rptConsolidado2(nDiaAtrMin, idUsuario)));
                ListaParametros.Add(new ReportParameter("nDiaAtrMin", nDiaAtrMin.ToString(), false));
                ListaParametros.Add(new ReportParameter("nIdUsuario", idUsuario.ToString(), false));
                reportpath = "rptConsolidado2.rdlc";

                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;

                var cReporte = reportpath;

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);
            }
            catch (Exception ex)
            {                
                throw;
            }
        }

        protected void btnResumenCartera_Click(object sender, EventArgs e)
        {
            try
            {
                List<ReportParameter> ListaParametros = new List<ReportParameter>();
                List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

                string reportpath = "";

                ListaDataSource.Add(new ReportDataSource("dsResumen", new SGA.LogicaNegocio.clsCNCredito().rptResumenCart()));
                //ListaParametros.Add(new ReportParameter("nDiaAtrMin", nDiaAtrMin.ToString(), false));
                //ListaParametros.Add(new ReportParameter("nIdUsuario", idUsuario.ToString(), false));
                reportpath = "rptResumenCart.rdlc";

                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;

                var cReporte = reportpath;

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}