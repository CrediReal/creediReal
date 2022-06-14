using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SGA.ENTIDADES;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmPlanPagos : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        DataTable dtSolicitud = new DataTable("dtSolicitud");
        DataTable ppg = new DataTable("dtPlanPago");
        int nNumsolicitud = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["usuario"] != null)
                {
                    Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); }

                if (IsPostBack) return;

                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            if (idCliente == 0)
            {
                return;
            }
            GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
            DataTable dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "S", "[2]");
            if (dtDatosCuentaSolCliente.Rows.Count == 1)
            {
                 int nIdSolCta = Convert.ToInt32(dtDatosCuentaSolCliente.Rows[0][0]);

            GEN.CapaNegocio.clsCNRetornaNumSolicitud RetornaNumSolicitud = new   GEN.CapaNegocio.clsCNRetornaNumSolicitud();
            DataTable dtDatosNumSolicutd = RetornaNumSolicitud.RetornaNumSolicitud(nIdSolCta, "S", "[2]");
            if (dtDatosNumSolicutd.Rows.Count == 0)
            {
                script.Mensaje("No se encontraron datos para la búsqueda" + idCliente.ToString());
            }
            else
            {
                int idSolicitud = Convert.ToInt32(dtDatosNumSolicutd.Rows[0]["idSolicitud"].ToString());
                GEN.CapaNegocio.clsCNBuscaSolicitud Solicitud = new GEN.CapaNegocio.clsCNBuscaSolicitud();
                dtSolicitud = Solicitud.DatoSolicitud(idSolicitud, 1);
                if (dtSolicitud.Rows.Count == 0)
                {
                    return;
                }
                int idProducto = Convert.ToInt32(dtSolicitud.Rows[0]["idProducto"]);
                decimal nMonto = Convert.ToDecimal(dtSolicitud.Rows[0]["nCapitalSolicitado"]);
                this.txtMonto.Text = nMonto.ToString();
                this.nudCuotas.Text = dtSolicitud.Rows[0]["nCuotas"].ToString();
                this.txtDiasGracia.Text = Convert.ToString(dtSolicitud.Rows[0]["nDiasGracia"]);
                int idMoneda = Convert.ToInt32(dtSolicitud.Rows[0]["IdMoneda"]);
                this.TxtTasa.Text = dtSolicitud.Rows[0]["nTasaCompensatoria"].ToString();
                this.dtFechaDesembolso.SeleccionarFecha =Convert.ToDateTime(dtSolicitud.Rows[0]["dFechaDesembolsoSugerido"].ToString());
                this.nudPlazo.Text = Convert.ToString(dtSolicitud.Rows[0]["nPlazoCuota"]);
                //cboTipoCalculo1.SelectedValue = Convert.ToInt32(dtSolicitud.Rows[0]["idTipoCalculo"]);
                //cboTipoPeriodo1.SelectedValue = Convert.ToInt32(dtSolicitud.Rows[0]["idTipoPeriodo"]);
                //this.boton2.Enabled = false;
                //this.btnProcesar1.Enabled = true;
                //this.conBusCuentaCli1.txtNroBusqueda.Enabled = false;
                //this.conBusCuentaCli1.btnBusCliente1.Enabled = false;
            }

            
            }
        }

      

        protected void BotonProcesar1_Click(object sender, EventArgs e)
        {
            double nTasEfeMen = double.Parse(this.TxtTasa.Text) / 100; // Tasa de Interes Efectiva Mensual
            if (nTasEfeMen == 0)
            {
                script.Mensaje("La tasa de interés no debe ser igual a cero...");
                return;
            }
            
            double nMonDesemb = double.Parse(this.txtMonto.Text); // Monto Desembolsado
            DateTime dFecDesemb = dtFechaDesembolso.SeleccionarFecha.Date;// DateTime.Now.Date; //Fecha de Desembolso
            int nNumCuoCta = int.Parse(this.nudCuotas.Text); // Número de Cuotas del Crédito
            int nDiaGraCta = Convert.ToInt32(this.txtDiasGracia.Text); // Días de Gracia
            short nTipPerPag = 1;// Convert.ToInt16(cboTipoPeriodo1.SelectedValue); // Tipo de periodo de pago (Fecha o Periodo Fijo)
            int nDiaFecPag = Convert.ToInt32(this.nudPlazo.Text); // Día o Periodo fijo de pago

            //llamando a la función que calcula el plan de pagos en la capa de negocios
            clsCNCronograma cncronogrma = new clsCNCronograma();
            double nTasEfeDia;

            nTasEfeDia = Math.Pow((1.0 + nTasEfeMen), (1.0 / 30.0)) - 1; //Tasa de interes efectiva diaria
            ppg = cncronogrma.CalculaPpgFlat(nMonDesemb, nTasEfeMen, dFecDesemb, nNumCuoCta, nDiaGraCta, nTipPerPag, nDiaFecPag, nNumsolicitud);
            this.dtgBase1.DataSource = ppg;
            dtgBase1.DataBind();

        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
        {
            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            if (idCliente == 0)
            {
                return;
            }
            GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
            DataTable dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "S", "[2]");
           
           // int nNumsolicitud = Convert.ToInt32(dtDatosCuentaSolCliente.Rows[0][0]); 
            DateTime dfecDesemb = Convert.ToDateTime(dtDatosCuentaSolCliente.Rows[0]["Fecha_Desembolso"]); //Fecha de Desembolso
            int nNumsolicitud  = Convert.ToInt32(dtDatosCuentaSolCliente.Rows[0][0]);
            if (nNumsolicitud <= 0)
            {
                script.Mensaje("Se necesita cargar los datos de una solicitud para grabar el plan de pagos");
                return;
            }
            double nTasEfeMen = double.Parse(this.TxtTasa.Text) / 100; // Tasa de Interes Efectiva Mensual
            if (nTasEfeMen == 0)
            {
                script.Mensaje("La tasa de interés no debe ser igual a cero...");
                return;
            }

            double nMonDesemb = double.Parse(this.txtMonto.Text); // Monto Desembolsado
            DateTime dFecDesemb = dtFechaDesembolso.SeleccionarFecha.Date; //Fecha de Desembolso
            int nNumCuoCta = int.Parse(this.nudCuotas.Text); // Número de Cuotas del Crédito
            int nDiaGraCta = Convert.ToInt32(this.txtDiasGracia.Text); // Días de Gracia
            short nTipPerPag = 1;// Convert.ToInt16(cboTipoPeriodo1.SelectedValue); // Tipo de periodo de pago (Fecha o Periodo Fijo)
            int nDiaFecPag = Convert.ToInt32(this.nudPlazo.Text); // Día o Periodo fijo de pago

            //llamando a la función que calcula el plan de pagos en la capa de negocios
            clsCNCronograma cncronogrma = new clsCNCronograma();
            double nTasEfeDia;

            nTasEfeDia = Math.Pow((1.0 + nTasEfeMen), (1.0 / 30.0)) - 1; //Tasa de interes efectiva diaria
            ppg = cncronogrma.CalculaPpgFlat(nMonDesemb, nTasEfeMen, dFecDesemb, nNumCuoCta, nDiaGraCta, nTipPerPag, nDiaFecPag, nNumsolicitud);
            ppg.Columns.Add("cfecha", typeof(string));

            foreach (DataRow item in ppg.Rows)
            {
                item["cfecha"] = Convert.ToDateTime(item["fecha"]).ToString("yyyy-MM-dd");
            }
            //this.dtgBase1.DataSource = ppg;
            //dtgBase1.DataBind();

            if (ppg.Rows.Count <= 0)
            {
                script.Mensaje("No se ha generado un plan de pagos para grabarlo");
                return;
            }
            //if (dfecDesemb < EntityLayer.clsVarGlobal.dFecSystem)
            //{
            //    script.Mensaje("La Fecha de desembolso no puede ser menor a la fecha del Sistema");
            //    return;
            //}
            DataSet ds = new DataSet("dsPlanPagos");
            ds.Tables.Add(ppg);
            string xmlPpg = ds.GetXml();
            CRE.CapaNegocio.clsCNPlanPago InsPlanPago = new CRE.CapaNegocio.clsCNPlanPago();
            DataTable TablaInsPpg = InsPlanPago.InsPpg(xmlPpg, dfecDesemb);
            //this.dtgBase1.DataSource = TablaInsPpg;
            //dtgBase1.DataBind();
            //this.dtgBase1.Columns["N° Cuota"].Width = 50;
            script.Mensaje("Plan de Pagos se grabó correctamente");
            //MessageBox.Show("Plan de Pagos se Grabó OK", "Plan de Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //this.btnGrabar1.Enabled = false;
            //btnImprimir1.Enabled = true;
            //btnProcesaPpg.Enabled = false;
        }

       
    }
}