using SGA.LogicaNegocio;
using SGA.Utilitarios;
using SGA.ENTIDADES;
using GEN.CapaNegocio;
using RPT.CapaNegocio;
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
    public partial class frmRptDetRecibos : System.Web.UI.Page
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

                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            }
            catch (Exception)
            {
                throw;
            }
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            cargarAgencia();
            cargarMoneda();
            CargarTiporecibos();
            ListarColAgencia(objUsuario.nIdAgencia);
            CargarConceptos(1);
            this.dtpFecIni.SeleccionarFecha = objUsuario.dFecSystem;
            this.dtpFecFin.SeleccionarFecha = objUsuario.dFecSystem;

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

        private void CargarConceptos(int nTipRec)
        {
            clsCNControlOpe LisConcep = new clsCNControlOpe();
            DataTable tbConcep = LisConcep.ListaConceptosPer(nTipRec, 4);
            cboConcepto.DataSource = tbConcep;
            cboConcepto.DataValueField = tbConcep.Columns[0].ToString();
            cboConcepto.DataTextField = tbConcep.Columns[1].ToString();
            cboConcepto.DataBind();

            //if (tbConcep.Rows.Count > 0)
            //{
            //    cboConcepto.SelectedValue = "1";
            //}

        }

        protected void cboConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nItem;
            if (Convert.ToInt32(this.cboConcepto.SelectedIndex.ToString()) <= 0)
            {
                nItem = 0;
                CargarSubItemCon(0);
            }
            else
            {
                nItem = Convert.ToInt32(this.cboConcepto.SelectedValue.ToString());
                CargarSubItemCon(nItem);
            }
        }

        private void CargarSubItemCon(int nCodCon)
        {
            clsCNControlOpe LisSubItenConcep = new clsCNControlOpe();
            DataTable tbItemConcep = LisSubItenConcep.ListarSubItemCon(nCodCon);
            this.cboDetalle.DataSource = tbItemConcep;
            this.cboDetalle.DataValueField = tbItemConcep.Columns[0].ToString();
            this.cboDetalle.DataTextField = tbItemConcep.Columns[1].ToString();
            this.cboDetalle.DataBind();

            if (tbItemConcep.Rows.Count > 0)
            {
                this.cboDetalle.Enabled = true;
            }
            else
            {
                this.cboDetalle.Enabled = false;
            }
        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            if (ValidarDatos() == "ERROR")
            {
                return;
            }
            List<ReportParameter> paramlist = new List<ReportParameter>();
            DateTime dFecIni = this.dtpFecIni.SeleccionarFecha.Date;
            DateTime dFecFin = this.dtpFecFin.SeleccionarFecha.Date;
            string idUsu = this.cboColaborador.SelectedValue.ToString();
            string idAge = this.cboAgencias.SelectedValue.ToString();
            string idMon = this.cboMoneda.SelectedValue.ToString();
            string idConcep = this.cboConcepto.SelectedValue.ToString();
            string idSubItem;
            if (this.cboDetalle.SelectedIndex < 0)
            {
                idSubItem = "0";
            }
            else
            {
                idSubItem = this.cboDetalle.SelectedValue.ToString();
            }
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("dsConRecibos", new clsRPTCNCaja().CNConRecibos(idAge, idUsu, dFecIni, dFecFin, idMon, idConcep, idSubItem)));

            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;
            var cReporte = "RptConRecibos.rdlc";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }
        
        private string ValidarDatos()
        {
            //==================================================================
            //--Validar Datos del Reporte
            //==================================================================
            if (string.IsNullOrEmpty(this.cboAgencias.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Seleccionar una Agencia");
                //MessageBox.Show("Debe Seleccionar una Agencia", "Reporte de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboAgencias.Focus();
                //this.cboAgencias.Select();
                return "ERROR";
            }
            if (this.cboColaborador.SelectedIndex < 0)
            {
                script.Mensaje("Debe Seleccionar una Agencia");
                //MessageBox.Show("Debe Seleccionar el Colaborador", "Reporte de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboColaborador.Focus();
                //this.cboColaborador.Select();
                return "ERROR";
            }
            if (string.IsNullOrEmpty(this.cboColaborador.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Seleccionar el Colaborador");
                //MessageBox.Show("Debe Seleccionar el Colaborador", "Reporte de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboColaborador.Focus();
                //this.cboColaborador.Select();
                return "ERROR";
            }
            if (this.cboTipRec.SelectedIndex < 0)
            {
                script.Mensaje("Debe Seleccionar el Tipo de Recibo");
                //MessageBox.Show("Debe Seleccionar el Tipo de Recibo", "Reporte de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboTipRec.Focus();
                //this.cboTipRec.Select();
                return "ERROR";
            }
            if (this.cboMoneda.SelectedIndex < 0)
            {
                script.Mensaje("Debe Seleccionar la Moneda");
                //MessageBox.Show("Debe Seleccionar la Moneda", "Reporte de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboMoneda.Focus();
                //this.cboMoneda.Select();
                return "ERROR";
            }
            if (this.cboConcepto.SelectedIndex < 0)
            {
                script.Mensaje("Debe Seleccionar el Concepto");
                //MessageBox.Show("Debe Seleccionar el Concepto", "Reporte de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboConcepto.Focus();
                //this.cboConcepto.Select();
                return "ERROR";
            }

            if (this.dtpFecIni.SeleccionarFecha > this.dtpFecFin.SeleccionarFecha)
            {
                script.Mensaje("La Fecha Final no Puede ser Menor que la Fecha Inicial");
                //MessageBox.Show("La Fecha Final no Puede ser Menor que la Fecha Inicial", "Reporte de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.dtpFecIni.Focus();
                return "ERROR";
            }
            return "OK";
        }

    }
}