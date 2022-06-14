using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion.CREDITOS
{
    public partial class FrmCobroMasivo : System.Web.UI.Page
    {
        clsWebJScript script = new clsWebJScript();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["usuario"] != null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
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
            cargarAsesores();
            cargarMoneda();

            if (this.ValidarInicioOpe() != "A")
            {
                btnGrabar1.Enabled = false;
                btnProcesar1.Visible = false;
                return;
            }

            if (ValidarCorteFracc() == "ERROR")
            {
                btnGrabar1.Enabled = false;
                btnProcesar1.Visible = false;
                return;
            }
            btnProcesar1.Visible = true;
            
        }

        public string ValidarInicioOpe()
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
            CAJ.CapaNegocio.clsCNControlOpe ValidaOpe = new CAJ.CapaNegocio.clsCNControlOpe();
            string cEstCie = ValidaOpe.ValidaIniOpe(objUsuario.dFecSystem, objUsuario.idUsuario, objUsuario.nIdAgencia);
            // Si Estado es: F--> Falta Iniciar, A--> Caja Abierta, C--> Caja Cerrada
            //string cRpta = this.ValidarInicioOpe();
            switch (cEstCie) // Si Estado es: F--> Falta Iniciar, A--> Caja Abierta, C--> Caja Cerrada  
            {
                case "F":
                    script.Mensaje("Falta Realizar el Inicio de sus Operaciones");
                    //MessageBox.Show("Falta Realizar el Inicio de sus Operaciones", "Validar Inicio de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Dispose();
                    break;
                case "A":
                    break;
                case "C":
                    script.Mensaje("El Usuario ya Cerro Sus Operaciones");
                    //MessageBox.Show("El Usuario ya Cerro Sus Operaciones", "Validar Cierre de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Dispose();
                    break;
                default:
                    script.Mensaje(cEstCie);
                    //MessageBox.Show(cEstCie, "Error al Validar Estado de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.Dispose();
                    break;
            }
            return cEstCie;
        }

        private void cargarAsesores()
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
            this.cboPersonalCreditos1.DataSource = dt;
            cboPersonalCreditos1.DataValueField = dt.Columns[0].ToString();
            cboPersonalCreditos1.DataTextField = dt.Columns[1].ToString();
            cboPersonalCreditos1.DataBind();
            cboPersonalCreditos1.Items.Insert(0, new ListItem("", "0"));
            cboPersonalCreditos1.SelectedIndex = 0;
            foreach (DataRow item in dt.Rows)
            {
                if (item["idUsuario"].ToString()==objUsuario.idUsuario.ToString())
                {
                    cboPersonalCreditos1.SelectedValue = objUsuario.idUsuario.ToString();
                }
            }
        }

        private void cargarMoneda()
        {
            GEN.CapaNegocio.clsCNMoneda moneda = new GEN.CapaNegocio.clsCNMoneda();
            var dt = moneda.listarMoneda();
            this.cboMoneda1.DataSource = dt;
            this.cboMoneda1.DataValueField = dt.Columns[0].ToString();
            this.cboMoneda1.DataTextField = dt.Columns[1].ToString();
            cboMoneda1.DataBind();
        }

        private void cargarCreditosPago()
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
            Int32 nIdAsesor = Convert.ToInt32(this.cboPersonalCreditos1.SelectedValue.ToString());
            CRE.CapaNegocio.clsCNCredito Credito = new CRE.CapaNegocio.clsCNCredito();
            var dtLisCrexAna = Credito.LisCrexAna(nIdAsesor, objUsuario.idUsuario);
            this.txtNumRea4.Text = dtLisCrexAna.Rows.Count.ToString();
            if (dtLisCrexAna.Rows.Count <= 0)
            {
                script.Mensaje("El Asesor no tiene créditos asignados");
                return;
            }
            dtLisCrexAna.Columns["nMonPagCuota"].ReadOnly = false;
            dtLisCrexAna.Columns["nMonPagMora"].ReadOnly = false;
            dtLisCrexAna.Columns["lSeleCta"].ReadOnly = false;

            this.hIdAsesor.Value = nIdAsesor.ToString();

            ViewState["dtLisCrexAna"] = dtLisCrexAna;
            dtgBase1.DataSource = dtLisCrexAna;
            this.dtgBase1.DataBind();
            this.dtgBase1.Enabled = true;
            //this.FormatoGrid();
            //this.HabilitarGrid(true);
            this.cboPersonalCreditos1.Enabled = false;
            if (dtLisCrexAna.Rows.Count > 0)
            {
                this.btnGrabar1.Enabled = true;
            }
            this.btnCancelar1.Enabled = true;
            this.btnProcesar1.Enabled = false;

            //this.txtNumRea1.Visible = false;
            //this.txtNumRea2.Visible = false;
            //this.txtNumRea3.Visible = false;
            //this.txtNumRea5.Visible = false;
        }

        private void SumaPagos()
        {
            double nTotPagadoCuo = 0;
            double nTotPagadoMora = 0;
            int nNumCrePagados = 0;
            var dtLisCrexAna =(DataTable)ViewState["dtLisCrexAna"];
            for (int i = 0; i < dtLisCrexAna.Rows.Count; i++)
            {
                bool lSeleCta = Convert.ToBoolean(dtLisCrexAna.Rows[i]["lSeleCta"]);
                if (lSeleCta)
                {
                    //dtgBase1.Rows[i].DefaultCellStyle.BackColor = Color.SpringGreen;
                    nTotPagadoCuo += Convert.ToDouble(dtLisCrexAna.Rows[i]["nMonPagCuota"]);
                    nTotPagadoMora += Convert.ToDouble(dtLisCrexAna.Rows[i]["nMonPagMora"]);
                    if ((Convert.ToDouble(dtLisCrexAna.Rows[i]["nMonPagCuota"]) + Convert.ToDouble(dtLisCrexAna.Rows[i]["nMonPagMora"])) > 0.00)
                    {
                        nNumCrePagados++;
                    }
                }
                else
                {
                    //dtgBase1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
            this.txtNumRea1.Text = nTotPagadoCuo.ToString();
            this.txtNumRea2.Text = nTotPagadoMora.ToString();
            this.txtNumRea3.Text = (nTotPagadoCuo + nTotPagadoMora).ToString();
            this.txtNumRea5.Text = nNumCrePagados.ToString();
        }

        protected void btnProcesar1_Click(object sender, EventArgs e)
        {
            cargarCreditosPago();
        }

        protected void chkSel_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            GridViewRow gridRow = (sender as CheckBox).NamingContainer as GridViewRow;
            var idCuenta = gridRow.Cells[7].Text;            

            var dtLisCrexAna = (DataTable)ViewState["dtLisCrexAna"];
            foreach (DataRow item in dtLisCrexAna.Rows)
            {
                if (item["idCuenta"].ToString() == idCuenta)
                {
                    item["lSeleCta"] = chk.Checked;
                }
            }

            ViewState["dtLisCrexAna"] = dtLisCrexAna;
            SumaPagos();
        }

        protected void txtMonPagCuota_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtMonPagCuota = (TextBox)row.FindControl("txtMonPagCuota");
            var idCuenta = row.Cells[7].Text;
            var nTotDeuda = Convert.ToDouble(row.Cells[14].Text);
            var nSalAPagar = Convert.ToDouble(row.Cells[15].Text);
            var nSalMora = Convert.ToDouble(row.Cells[16].Text);
            var nPagaDeuda = Convert.ToDouble(txtMonPagCuota.Text);

            if (nPagaDeuda > nTotDeuda)
            {
                txtMonPagCuota.Text = nSalAPagar.ToString();
                txtMonPagCuota.Focus();
                script.Mensaje("Monto a pagar no puede superar a la deuda de la cuota");
            }

           
            var dtLisCrexAna = (DataTable)ViewState["dtLisCrexAna"];
            foreach (DataRow item in dtLisCrexAna.Rows)
            {
                if (item["idCuenta"].ToString() == idCuenta)
                {
                    item["nMonPagCuota"] = Convert.ToDouble(txtMonPagCuota.Text);
                }
            }

            ViewState["dtLisCrexAna"] = dtLisCrexAna;
            SumaPagos();
        }

        protected void btnCancelar1_Click(object sender, EventArgs e)
        {
            this.dtgBase1.Enabled = false;
            this.dtgBase1.DataSource = null;
            this.dtgBase1.DataBind();
            this.btnGrabar1.Enabled = false;
            this.btnCancelar1.Enabled = false;
            this.btnProcesar1.Enabled = true;
            this.dtgBase1.DataSource = "";
            this.txtNumRea4.Text = "0";
            this.txtNumRea1.Text = "0";
            this.txtNumRea2.Text = "0";
            this.txtNumRea3.Text = "0";
            this.txtNumRea5.Text = "0";

            this.txtNumRea1.Visible = true;
            this.txtNumRea2.Visible = true;
            this.txtNumRea3.Visible = true;
            this.txtNumRea5.Visible = true;
            LiberarCuenta();
        }

        public void LiberarCuenta()
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
            if (hIdAsesor.Value != "-1")
            {
                new CRE.CapaNegocio.clsCNCredito().DesbMasByAse(Convert.ToInt32(hIdAsesor.Value), objUsuario.idUsuario);
            }
        }

        protected void btnGrabar1_Click(object sender, EventArgs e)
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

            CRE.CapaNegocio.clsCNPlanPago PlanPago = new CRE.CapaNegocio.clsCNPlanPago();
            DataTable dtPlanPago = new DataTable("dtPlanPago");
            DataTable dtPlanPagado = new DataTable("dtPlanPagado");
            double nPagaMora = 0.00;
            double nPagaDeuda = 0.00;
            int nNumCredito = 0;
            var dtLisCrexAna = (DataTable)ViewState["dtLisCrexAna"];
            if (!validapago())
            {
                return;
            }
            for (int i = 0; i < dtLisCrexAna.Rows.Count; i++)
            {
                bool lSeleCta = Convert.ToBoolean(dtLisCrexAna.Rows[i]["lSeleCta"]);
                if (!lSeleCta)
                {
                    continue;
                }
                nPagaMora = Convert.ToDouble(dtLisCrexAna.Rows[i]["nMonPagMora"]);
                nPagaDeuda = Convert.ToDouble(dtLisCrexAna.Rows[i]["nMonPagCuota"]);
                if ((nPagaMora + nPagaDeuda) <= 0)
                {
                    continue;
                }
                nNumCredito = Convert.ToInt32(dtLisCrexAna.Rows[i]["idCuenta"]);
                dtPlanPago = PlanPago.CNdtPlanPago(nNumCredito);
                if (nPagaDeuda + nPagaMora > 0)
                {
                    dtPlanPagado = PlanPago.dtCNPagoDistribuido(dtPlanPago, nPagaDeuda, true);
                    DataSet ds = new DataSet("dsPlanPagos");
                    ds.Tables.Add(dtPlanPago);
                    string xmlPpg = ds.GetXml();
                    DataTable TablaUpPpg = PlanPago.UpCobroPpg(xmlPpg, objUsuario.dFecSystem, objUsuario.idUsuario, objUsuario.nIdAgencia, nPagaMora, nNumCredito,1,"XXX");
                    //MessageBox.Show("Cobro satisfactorio con kardesx N°: " + TablaUpPpg.Rows[0][0].ToString(), "Cobro de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ds.Dispose();
                }
            }
            this.btnGrabar1.Enabled = false;
            this.txtNumRea1.Visible = true;
            this.txtNumRea2.Visible = true;
            this.txtNumRea3.Visible = true;
            this.txtNumRea5.Visible = true;
            LiberarCuenta();
            script.Mensaje("Cobro en Lote Realizado Satisfactoriamente");
           
        }

        private bool validapago()
        {
            var dtLisCrexAna = (DataTable)ViewState["dtLisCrexAna"];

            bool lvalida = true;
            double nTotDeuda = 0.00;
            double nPagaDeuda = 0.00;

            if (this.txtNumRea3.Text.Trim() == "" || Convert.ToDouble(this.txtNumRea3.Text) <= 0)
            {
                script.Mensaje("No hay montos a ser pagados");
                lvalida = false;
                return lvalida;
            }
            for (int i = 0; i < dtLisCrexAna.Rows.Count; i++)
            {
                bool lSeleCta = Convert.ToBoolean(dtLisCrexAna.Rows[i]["lSeleCta"]);
                if (!lSeleCta)
                {
                    continue;
                }
                nTotDeuda = Convert.ToDouble(dtLisCrexAna.Rows[i]["nSaldoTot"]);
                nPagaDeuda = Convert.ToDouble(dtLisCrexAna.Rows[i]["nMonPagCuota"]);
                if (nPagaDeuda > nTotDeuda)
                {
                    script.Mensaje("Monto a pagar no puede superar a la deuda de la cuota");
                    lvalida = false;
                    break;
                }
                nTotDeuda = Convert.ToDouble(dtLisCrexAna.Rows[i]["nSalMora"]);
                nPagaDeuda = Convert.ToDouble(dtLisCrexAna.Rows[i]["nMonPagMora"]);
                if (nPagaDeuda > nTotDeuda)
                {
                    script.Mensaje("Monto a pagar de Mora no puede superar a la deuda de Mora");
                    lvalida = false;
                    break;
                }
            }
            return lvalida;
        }

        private string ValidarCorteFracc()
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
            string cRpta = "OK";
            string msge = "";
            CAJ.CapaNegocio.clsCNControlOpe ValCorFra = new CAJ.CapaNegocio.clsCNControlOpe();
            string cCorFra = ValCorFra.ValidaCorteFracc(objUsuario.dFecSystem, Convert.ToInt32(objUsuario.idUsuario.ToString()), objUsuario.nIdAgencia, ref msge);
            if (msge == "OK")
            {
                if (cCorFra == "0")
                {
                    script.Mensaje("Primero debe Realizar su Corte Fraccionario... por Favor..");
                    cRpta = "ERROR";
                }
            }
            else
            {
                script.Mensaje(msge);
                cRpta = "ERROR";
            }
            return cRpta;
        }
    }
}