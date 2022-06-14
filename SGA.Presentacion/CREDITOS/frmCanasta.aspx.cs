using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEN.CapaNegocio;
using Microsoft.Reporting.WebForms;
using RPT.CapaNegocio;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmCanasta : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        DataTable dtSolicitud = new DataTable("dtSolicitud");
        DataTable ppg = new DataTable("dtPlanPago");
        clsCNCronograma cncronograma = new clsCNCronograma();
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
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }

                if (IsPostBack) return;


                var objusuario=(SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
                //nudPlazo.Text = objusuario.dFecSystem.Day.ToString();
                dtFechaDesembolso.SeleccionarFecha = objusuario.dFecSystem.Date;
                BotonImprimir1.Visible = false;
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
            clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new clsCNRetornsCuentaSolCliente();
            DataTable dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "S", "[2]");
            if (dtDatosCuentaSolCliente.Rows.Count == 1)
            {
                int nIdSolCta = Convert.ToInt32(dtDatosCuentaSolCliente.Rows[0][0]);

                clsCNRetornaNumSolicitud RetornaNumSolicitud = new clsCNRetornaNumSolicitud();
                DataTable dtDatosNumSolicutd = RetornaNumSolicitud.RetornaNumSolicitud(nIdSolCta, "S", "[2]");
                if (dtDatosNumSolicutd.Rows.Count == 0)
                {
                    script.Mensaje("No se encontraron datos para la búsqueda" + idCliente.ToString());
                }
                else
                {
                    int idSolicitud = Convert.ToInt32(dtDatosNumSolicutd.Rows[0]["idSolicitud"].ToString());
                    clsCNBuscaSolicitud Solicitud = new clsCNBuscaSolicitud();
                    dtSolicitud = Solicitud.DatoSolicitud(idSolicitud, 1);
                    if (dtSolicitud.Rows.Count == 0)
                    {
                        return;
                    }
                    int idProducto = Convert.ToInt32(dtSolicitud.Rows[0]["idProducto"]);
                    decimal nMonto = Convert.ToDecimal(dtSolicitud.Rows[0]["nCapitalSolicitado"]);
                    this.txtMonto.Text = nMonto.ToString();
                    this.nudCuotas.Text = dtSolicitud.Rows[0]["nCuotas"].ToString();
                    //this.txtDiasGracia.Text = Convert.ToString(dtSolicitud.Rows[0]["nDiasGracia"]);
                    int idMoneda = Convert.ToInt32(dtSolicitud.Rows[0]["IdMoneda"]);
                    this.TxtTasa.Text = dtSolicitud.Rows[0]["nTasaCompensatoria"].ToString();
                    this.dtFechaDesembolso.SeleccionarFecha = Convert.ToDateTime(dtSolicitud.Rows[0]["dFechaDesembolsoSugerido"].ToString());
                    this.nudPlazo.Text = Convert.ToString(dtSolicitud.Rows[0]["nPlazoCuota"]);
                }


            }
        }
        
        protected void BotonProcesar1_Click(object sender, EventArgs e)
        {
            double nTasEfeMen = double.Parse(this.TxtTasa.Text) / 100; // Tasa de Interes Efectiva Mensual


            if (Session["idCliente"] == null)
            {
                script.Mensaje("Seleccione un cliente para generar cronograma");
                return;
            }

            double nMonDesemb = double.Parse(this.txtMonto.Text); // Monto Desembolsado
            DateTime dFecDesemb = dtFechaDesembolso.SeleccionarFecha.Date;// DateTime.Now.Date; //Fecha de Desembolso
            int nNumCuoCta = int.Parse(this.nudCuotas.Text); // Número de Cuotas del Crédito
            int nDiaGraCta = 0; // Días de Gracia
            short nTipPerPag = 1;// Convert.ToInt16(cboTipoPeriodo1.SelectedValue); // Tipo de periodo de pago (Fecha o Periodo Fijo)
            int nDiaFecPag = Convert.ToInt32(this.nudPlazo.Text); // Día o Periodo fijo de pago

            double nTasEfeDia;

            nTasEfeDia = Math.Pow((1.0 + nTasEfeMen), (1.0 / 360)) - 1; //Tasa de interes efectiva diaria
            ppg = cncronograma.CalculaPpgFlat(nMonDesemb, nTasEfeMen, dFecDesemb, nNumCuoCta, nDiaGraCta, nTipPerPag, nDiaFecPag, nNumsolicitud);
            this.dtgBase1.DataSource = ppg;
            dtgBase1.DataBind();
            BotonImprimir1.Visible = false;
            BotonGrabar1.Visible = true;
        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
        {
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            var objusuario = (clsUsuario)Session["DatosUsuarioSession"];

            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            if (idCliente == 0)
            {
                return;
            }

            var dtDatosCanasta = cncronograma.BuscarCanasta(idCliente, 2017);
            if (dtDatosCanasta.Rows.Count > 0)
            {
                BotonImprimir1.Visible = true;
                script.Mensaje("Cliente ya cuenta con registro de canasta");
                return;
            }
            DateTime dfecDesemb = dtFechaDesembolso.SeleccionarFecha;
            int nNumsolicitud = idCliente;
            double nTasEfeMen = double.Parse(this.TxtTasa.Text) / 100; // Tasa de Interes Efectiva Mensual
            

            double nMonDesemb = double.Parse(this.txtMonto.Text); // Monto Desembolsado
            DateTime dFecDesemb = dtFechaDesembolso.SeleccionarFecha.Date; //Fecha de Desembolso
            int nNumCuoCta = int.Parse(this.nudCuotas.Text); // Número de Cuotas del Crédito
            int nDiaGraCta = 0; // Días de Gracia
            short nTipPerPag = 1;//  Tipo de periodo de pago (Fecha o Periodo Fijo)
            int nDiaFecPag = Convert.ToInt32(this.nudPlazo.Text); // Día o Periodo fijo de pago
;
            double nTasEfeDia;

            nTasEfeDia = Math.Pow((1.0 + nTasEfeMen), (1.0 / 30.0)) - 1; //Tasa de interes efectiva diaria
            ppg = cncronograma.CalculaPpgFlat(nMonDesemb, nTasEfeMen, dFecDesemb, nNumCuoCta, nDiaGraCta, nTipPerPag, nDiaFecPag, nNumsolicitud);
            ppg.Columns.Add("cfecha", typeof(String));

            foreach (DataRow item in ppg.Rows)
            {
                item["cfecha"] = Convert.ToDateTime(item["fecha"]).ToString("yyyy-MM-dd");
            }
            if (ppg.Rows.Count <= 0)
            {
                script.Mensaje("Error a grabar canasta");
                BotonImprimir1.Visible = false;
                return;                
            }
            DataSet ds = new DataSet("dsPlanPagos");
            ds.Tables.Add(ppg);
            String xmlPpg = ds.GetXml();
            DataTable TablaInsPpg = cncronograma.RegistrarCronogramaCanasta(xmlPpg, dfecDesemb, objusuario.idUsuario, Convert.ToDecimal(txtMonto.Text), Convert.ToInt32(nudCuotas.Text), Convert.ToDecimal(nTasEfeMen));
            BotonImprimir1.Visible = true;
            script.Mensaje("Canasta registrada correctamente");
        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            var dtCronograma = new clsCNCronograma().CronogramaCanasta(idCliente,2017);

            if (dtCronograma.Rows.Count == 0)
            {
                script.Mensaje("No existe cronograma para la cuenta ingresada");
                return;
            }

            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("dtsAgencia", new clsRPTCNAgencia().CNDatoAgencia(objUsuario.nIdAgencia)));
            ListaDataSource.Add(new ReportDataSource("dtsPPG", dtCronograma));
            ListaDataSource.Add(new ReportDataSource("dtsCuenta", new clsRPTCNCredito().CNDatosCuenta(0)));
            ListaDataSource.Add(new ReportDataSource("dtsCliente", new clsRPTCNCliente().CNDireccion(0)));

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = "rptCronogramaCanasta.rdlc";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);
        
        }

        protected void ComboBoxBase1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProducto.SelectedIndex>-1)
            {
                if (cboProducto.SelectedValue=="22")
                {
                    txtMonto.Text = "10";
                    txtDepositoMin.Text = "5";
                    nudPlazo.Text = "1";
                    nudCuotas.Text = "30";
                    TxtTasa.Text = "0";
                }
                if (cboProducto.SelectedValue == "23")
                {
                    txtMonto.Text = "10";
                    txtDepositoMin.Text = "7";
                    nudPlazo.Text = "7";
                    nudCuotas.Text = "4";
                    TxtTasa.Text = "0";
                }
                if (cboProducto.SelectedValue == "24")
                {
                    txtMonto.Text = "15";
                    txtDepositoMin.Text = "15";
                    nudPlazo.Text = "15";
                    nudCuotas.Text = "4";
                    TxtTasa.Text = "0";
                }
                if (cboProducto.SelectedValue == "25")
                {
                    txtMonto.Text = "30";
                    txtDepositoMin.Text = "30";
                    nudPlazo.Text = "30";
                    nudCuotas.Text = "3";
                    TxtTasa.Text = "0";
                }
                if (cboProducto.SelectedValue == "20")
                {
                    txtMonto.Text = "300";
                    txtDepositoMin.Text = "0";
                    nudPlazo.Text = "120";
                    nudCuotas.Text = "1";
                    TxtTasa.Text = "7";
                }
            }
        }
    }
}