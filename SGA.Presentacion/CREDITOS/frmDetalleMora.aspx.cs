using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.Utilitarios;
using Microsoft.Reporting.WebForms;
using System.Data;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmDetalleMora : System.Web.UI.Page
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
                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
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
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
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
            if (validar())
            {
                clsUsuario objUsuario;
                if (Session["DatosUsuarioSession"] == null)
                {
                    Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                    objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
                }
                else
                {
                    objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
                }
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
                int pnAtraIni = Convert.ToInt32(txtAtrIni.Text);
                int pnAtraFin = Convert.ToInt32(txtAtrFin.Text);
                int pnMonIni = Convert.ToInt32(txtMonIni.Text);
                int pnMonFin = Convert.ToInt32(txtMonFin.Text);
                DateTime dFecha = objUsuario.dFecSystem;

                DataTable dtMora = new RPT.CapaNegocio.clsRPTCNCredito().CNMora(pnAtraIni, pnAtraFin, pnMonIni, pnMonFin, CodAna);

                if (dtMora.Rows.Count > 0)
                {
                    List<ReportParameter> ListaParametros = new List<ReportParameter>();
                    List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();


                    DataTable dtRutaLogo = new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo();

                    ListaParametros.Add(new ReportParameter("dFecha", dFecha.ToString(), false));


                    ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", dtRutaLogo));
                    ListaDataSource.Add(new ReportDataSource("dtsDetalleMora", dtMora));

                    string reportpath = "RptMoraDiaria.rdlc";
                    Session["ListaParametros"] = ListaParametros;
                    Session["ListaDataSource"] = ListaDataSource;
                    Session["lModal"] = true;

                    var cReporte = reportpath;

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

                }
                else
                {
                    script.Mensaje("No existen datos para el reporte");
                    return;
                }

            }
        }

        private bool validar()
        {
            Boolean lProcesa = true;
            if (Convert.ToDecimal(txtMonFin.Text) < Convert.ToDecimal(txtMonIni.Text))
            {
                script.Mensaje("Monto Final debe ser que Monto Inicial");
                lProcesa = false;
                return lProcesa;
            }

            if (Convert.ToInt32(txtAtrFin.Text) < Convert.ToInt32(txtAtrIni.Text))
            {
                script.Mensaje("Atraso Final debe ser que Atraso Inicial");
                lProcesa = false;
                return lProcesa;
            }

            return lProcesa;
        }
    }
}