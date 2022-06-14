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
    public partial class frmRptCobroRec : System.Web.UI.Page
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
            cargarAgencia();
            cargarMoneda();
            CargarTiporecibos();
            ListarColAgencia(objUsuario.nIdAgencia);
            this.dtpFecIni.SeleccionarFecha = objUsuario.dFecSystem;
            this.dtpFecFin.SeleccionarFecha = objUsuario.dFecSystem;
            if (this.cboTipRec.SelectedIndex < 0)
            {
                CargarConceptos(0);
            }
            else
            {
                CargarConceptos(Convert.ToInt32(cboTipRec.SelectedValue.ToString().Trim()));
            }



        }

        private void cargarAgencia()
        {
            GEN.CapaNegocio.clsCNAgencia Agencia = new GEN.CapaNegocio.clsCNAgencia();
            var dt = Agencia.LisAgen();
            this.cboAgencias.DataSource = dt;
            this.cboAgencias.DataValueField = dt.Columns[0].ToString();
            this.cboAgencias.DataTextField = dt.Columns[1].ToString();
            cboAgencias.DataBind();
            this.cboAgencias.SelectedValue = "1";

        }


        private void ListarColAgencia(int idAge)
        {
            clsCNControlOpe LisColAge = new clsCNControlOpe();
            DataTable tbColAge = LisColAge.ListarColabAgencias(idAge);
            this.cboColaborador.DataSource = tbColAge;
            this.cboColaborador.DataValueField = tbColAge.Columns[0].ToString();
            this.cboColaborador.DataTextField = tbColAge.Columns[1].ToString();
            cboColaborador.DataBind();
            cboTipRec.SelectedValue = "1";



        }

        private void CargarTiporecibos()
        {
            clsCNControlOpe LisTiprec = new clsCNControlOpe();
            DataTable tbTipRec = LisTiprec.ListarTipRec();
            this.cboTipRec.DataSource = tbTipRec;
            this.cboTipRec.DataValueField = tbTipRec.Columns[0].ToString();
            this.cboTipRec.DataTextField = tbTipRec.Columns[1].ToString();
            cboTipRec.DataBind();
            cboTipRec.SelectedValue = "1";
        }

        private void cargarMoneda()
        {
            GEN.CapaNegocio.clsCNMoneda moneda = new GEN.CapaNegocio.clsCNMoneda();
            var dt = moneda.listarMoneda();
            this.cboMoneda.DataSource = dt;
            this.cboMoneda.DataValueField = dt.Columns[0].ToString();
            this.cboMoneda.DataTextField = dt.Columns[1].ToString();
            cboMoneda.DataBind();
            this.cboMoneda.SelectedValue = "1";
        }


        private void CargarConceptos(int nTipRec)
        {
            clsCNControlOpe LisConcep = new clsCNControlOpe();
            DataTable tbConcep = LisConcep.ListaConceptosPer(nTipRec, 4);
            cboConcepto.DataSource = tbConcep;
            cboConcepto.DataValueField = tbConcep.Columns[0].ToString();
            cboConcepto.DataTextField = tbConcep.Columns[1].ToString();
            cboConcepto.DataBind();
            
        }

        
        protected void ComboBoxMoneda1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboAgencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nItem;
            nItem = Convert.ToInt32(this.cboAgencias.SelectedValue.ToString());
            ListarColAgencia(nItem);
        }

        protected void cboTipRec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipRec.SelectedValue.ToString() == "1")
            {
                CargarConceptos(1);
            }
            else
            {
                CargarConceptos(2);
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {


            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();
            DateTime dFecIni = this.dtpFecIni.SeleccionarFecha.Date;
            DateTime dFecFin = this.dtpFecFin.SeleccionarFecha.Date;
            string idUsu = cboColaborador.SelectedValue.ToString();
            string idAge = cboAgencias.SelectedValue.ToString();
            string idMon = cboMoneda.SelectedValue.ToString();
            string idConcep = this.cboConcepto.SelectedValue.ToString();

            ListaDataSource.Add(new ReportDataSource("dsConRecibos", new clsRPTCNCaja().CNRecibosCobrados(idAge, idUsu, dFecIni, dFecFin, idMon, idConcep)));

            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;
            var cReporte = "RptCobroRec.rdlc";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        
  

      
    }
}