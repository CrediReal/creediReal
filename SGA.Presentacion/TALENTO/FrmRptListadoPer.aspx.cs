using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEN.CapaNegocio;
using SGA.Utilitarios;
using Microsoft.Reporting.WebForms;

namespace SGA.Presentacion.TALENTO
{
    public partial class FrmRptListadoPer : System.Web.UI.Page
    {
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

            cargarAgencia();
            CargoPersonal();
            EstPersonal();
        }

        private void cargarAgencia()
        {
            GEN.CapaNegocio.clsCNAgencia Agencia = new GEN.CapaNegocio.clsCNAgencia();
            var dt = Agencia.LisAgen();
            dt.Columns["cNombreAge"].ReadOnly = false;
            dt.Rows[0]["cNombreAge"] = "Todos";
            this.cboAgencia1.DataSource = dt;
            this.cboAgencia1.DataValueField = dt.Columns[0].ToString();
            this.cboAgencia1.DataTextField = dt.Columns[1].ToString();
            cboAgencia1.DataBind();


        }

        public void CargoPersonal()
        {
            clsCNListaCargoPersonal objCargoPersonal = new clsCNListaCargoPersonal();
            DataTable dtCargoPersonal = objCargoPersonal.ListacargoPersonal();
            dtCargoPersonal.Columns["cCargo"].ReadOnly = false;
            dtCargoPersonal.Rows[0]["cCargo"] = "Todos";
            this.cboCargoPersonal.DataSource = dtCargoPersonal;
            this.cboCargoPersonal.DataValueField = dtCargoPersonal.Columns[0].ToString();
            this.cboCargoPersonal.DataTextField = dtCargoPersonal.Columns[1].ToString();
            cboCargoPersonal.DataBind();

        }

        public void EstPersonal()
        {

            clsCNEstPersonal ListaEstPersonal = new clsCNEstPersonal();
            DataTable dtListaEstPersonal = ListaEstPersonal.ListaEstPersonal();
            dtListaEstPersonal.Columns["cEstPersonal"].ReadOnly = false;
            dtListaEstPersonal.Rows[0]["cEstPersonal"] = "Todos";
            this.cboEstPersonal.DataSource = dtListaEstPersonal;
            this.cboEstPersonal.DataValueField = dtListaEstPersonal.Columns[0].ToString();
            this.cboEstPersonal.DataTextField = dtListaEstPersonal.Columns[1].ToString();
            cboEstPersonal.DataBind();

        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            int idAge = Convert.ToInt32(this.cboAgencia1.SelectedValue);
            int idCargo = Convert.ToInt32(this.cboCargoPersonal.SelectedValue);
            int idEst = Convert.ToInt32(this.cboEstPersonal.SelectedValue);
            ListaDataSource.Add(new ReportDataSource("dsLisPersonal", new RPT.CapaNegocio.clsRPTCNCliente().CNListadoPersonal(idAge, idCargo, idEst)));

            string reportpath = "RptListadoPersonal.rdlc";

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }
    }
}