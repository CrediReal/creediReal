using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion
{
    public partial class frmReporteVisitasUbigeo : PageBase
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNHojaRuta CNHojaRuta = new clsCNHojaRuta();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            //----------------- TITULO ------------------------>
            lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            cboDepartamento.CargarDepartamentos();
            cboDepartamento_OnSelectedIndexChanged(this,EventArgs.Empty);
        }


        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validar()) return;

                List<ReportParameter> ListaParametros = new List<ReportParameter>();
                List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

                string cCodDep = cboDepartamento.SelectedValue.Equals("0") ? "%" : cboDepartamento.SelectedValue;
                string cCodProv = string.IsNullOrEmpty(cboProvincia.SelectedValue) ||
                                  cboProvincia.SelectedValue.Equals("0")
                                    ? "%"
                                    : cboProvincia.SelectedValue;
                string cCodDis = string.IsNullOrEmpty(cboDistrito.SelectedValue) ||
                                  cboDistrito.SelectedValue.Equals("0")
                                    ? "%"
                                    : cboDistrito.SelectedValue;

                DataTable dtRpt = CNHojaRuta.RptVisitasUbigeo(string.Format("{0}{1}{2}", cCodDep, cCodProv, cCodDis));
                ListaDataSource.Add(new ReportDataSource("dsData", dtRpt));

                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;

                var cReporte = "RptVisitasUbigeo.rdlc";

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private bool validar()
        {
            if (string.IsNullOrEmpty(cboDepartamento.SelectedValue))
            {
                script.Mensaje("Seleccione el departamento para la busqueda.");
                return false;
            }

            return true;
        }

        protected void cboDepartamento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboDepartamento.SelectedValue))
            {
                cboProvincia.ListarProvincias(cboDepartamento.SelectedValue);
            }

            lblProvincia.Visible = !string.IsNullOrEmpty(cboDepartamento.SelectedValue);
            cboProvincia.Visible = !string.IsNullOrEmpty(cboDepartamento.SelectedValue);
        }

        protected void cboProvincia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboProvincia.SelectedValue))
            {
                cboDistrito.ListarDistritos(cboDepartamento.SelectedValue,cboProvincia.SelectedValue);
            }

            lblDistrito.Visible = !string.IsNullOrEmpty(cboProvincia.SelectedValue);
            cboDistrito.Visible = !string.IsNullOrEmpty(cboProvincia.SelectedValue);
        }
    }
}