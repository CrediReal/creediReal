using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmSimulPlanPagos : System.Web.UI.Page
    {
        #region Variables Globales
        DataTable ppg = new DataTable("dtPlanPago");
        clsWebJScript script = new clsWebJScript();
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
                this.dtFechaDesembolso.SeleccionarFecha = DateTime.Now.Date;
                this.txtDiasGracia.Text = "0";
                TxtTasa.Text = "6";
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void BotonProcesar1_Click(object sender, EventArgs e)
        {   
            if (txtMonto.Value <= 0)
            {
                script.Mensaje("El monto debe ser mayor a cero.");
                return;
            }

            if (this.nudPlazo.Value <= 0)
            {
                script.Mensaje("La frecuencia de pago debe ser mayor a cero.");
                return;
            }

            if (this.nudCuotas.Value <= 0)
            {
                script.Mensaje("El número de cuotas debe ser mayor a cero.");
                return;
            }

            double nTasEfeMen = this.TxtTasa.Value / 100.00; // Tasa de Interes Efectiva Mensual
            if (nTasEfeMen == 0)
            {
                script.Mensaje("La tasa de interés no debe ser igual a cero.");
                return;
            }
            int nNumsolicitud = 0;
            double nMonDesemb = this.txtMonto.Value; // Monto Desembolsado
            DateTime dFecDesemb = dtFechaDesembolso.SeleccionarFecha; //Fecha de Desembolso
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

        
        Boolean CamposSonValidos()
        {


            if (double.Parse(txtMonto.Text) < 0)
            {
                script.Mensaje("La tasa de interés no debe ser igual a cero...");
                return false;
            }

            //if (string.IsNullOrEmpty(txtMonto.Text))
            //{
            //    MessageBox.Show("El monto debe ser mayor a CERO", "Migracion de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return false;
            //}

            //if (Convert.ToInt32(nudCuotas.Value) == 0)
            //{
            //    MessageBox.Show("Número de cuotas debe ser mayor a CERO", "Migracion de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return false;
            //}

            //if (Convert.ToInt32(nudPlazo.Value) == 0)
            //{
            //    MessageBox.Show("Plazo debe ser mayor a CERO", "Migracion de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return false;
            //}

            //if (string.IsNullOrEmpty(txtTasaInteres.Text))
            //{
            //    MessageBox.Show("No existe tasa de interes", "Solicitud de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return false;
            //}
            return true;

        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            txtMonto.Text = "0";
            nudCuotas.Text = "30";
            nudPlazo.Text = "1";
            //dtFechaDesembolso.Text = null;
            txtDiasGracia.Text = "0";
            TxtTasa.Text = "6";
            txtMonto.Focus();
            dtgBase1.DataSource = null;
            dtgBase1.DataBind();
        }
    }
}