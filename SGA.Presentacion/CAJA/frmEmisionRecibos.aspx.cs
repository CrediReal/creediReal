using SGA.LogicaNegocio;
using SGA.Utilitarios;
using CAJ.CapaNegocio;
using SGA.ENTIDADES;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace SGA.Presentacion.CAJA
{
    public partial class frmEmisionRecibos : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNControlOpe ControlOpe = new clsCNControlOpe();
        bool relacionCobranza;

        #endregion

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
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            //===========================================================================================
            //--Validar Inicio de Operaciones
            //===========================================================================================
            if (this.ValidarInicioOpe() != "A")
            {
                this.pnlRecibo.Visible = false;
                return;
            }
            DatosUsuario();
            //======================================================
            //--Cargar Datos
            //======================================================
            CargarTiporecibos();
            if (this.cboTipRec.SelectedIndex < 0)  //(string.IsNullOrEmpty(txtCodUsu.Text.Trim()))
            {
                CargarConceptos(0);
            }
            else
            {
                CargarConceptos(Convert.ToInt32(cboTipRec.SelectedValue.ToString().Trim()));
            }
            CargarSubItemCon(0);
            HabilitarControles(false);
            this.cboAgencias.SelectedValue = Convert.ToString(objUsuario.nIdAgencia);

            //===========================================================
            //--Validar Parametro de Configuración
            //===========================================================
            if (RetParamConfig() == 2)
            {
                //===========================================================
                //--Valida si el Usuario tiene Nivel para Eliminar Recibos
                //===========================================================
                if (ValNivAut() == 0)
                {
                    this.btnEliminar.Visible = false;
                }
                else
                {
                    this.btnEliminar.Visible = true;
                }
            }

        }

        private void DatosUsuario()
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
            this.dtpFechaSis.SeleccionarFecha = objUsuario.dFecSystem;
            this.txtCodUsu.Text = objUsuario.idUsuario.ToString();
            txtUsuario.Text = objUsuario.cWinuser.ToString();
            int nidCli = Convert.ToInt32(objUsuario.idUsuario);
            CLI.CapaNegocio.clsCNRetDatosCliente RetDatCli = new CLI.CapaNegocio.clsCNRetDatosCliente();
            DataTable DatosCli = RetDatCli.ListarDatosCli(nidCli, "D");
            if (DatosCli.Rows.Count > 0)
            {
                this.txtNomUsu.Text = DatosCli.Rows[0]["cNombre"].ToString();
            }
            else
            {
                txtNomUsu.Text = "";
            }
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
            clsCNControlOpe ValidaOpe = new clsCNControlOpe();
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

        private void CargarTiporecibos()
        {
            clsCNControlOpe LisTiprec = new clsCNControlOpe();
            DataTable tbTipRec = LisTiprec.ListarTipRec();
            this.cboTipRec.DataSource = tbTipRec;
            this.cboTipRec.DataValueField = tbTipRec.Columns[0].ToString();
            this.cboTipRec.DataTextField = tbTipRec.Columns[1].ToString();
            this.cboTipRec.DataBind();
            cboConcepto.SelectedValue = "1";


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

        private void HabilitarControles(Boolean Val)
        {
            this.cboTipRec.Enabled = Val;
            this.cboConcepto.Enabled = Val;
            this.cboMoneda.Enabled = Val;
            this.cboDetalle.Enabled = Val;
            this.cboAgencias.Enabled = false;
            this.conBuscarCliente1.EnableViewState = false;
            //this.conBusCol.btnConsultar.Enabled = Val;
            //this.conBusCli.btnBusCliente.Enabled = Val;
            this.txtMonRec.Enabled = Val;
            //this.txtTotRec.Enabled = Val;
            this.txtSustento.Enabled = Val;
            //this.chcColab.Checked = false;
            //this.chcColab.Enabled = Val;
            //this.chcCliente.Checked = false;
            //this.chcCliente.Enabled = Val;
        }

        private int RetParamConfig()
        {
            clsCNControlOpe ParamConfig = new clsCNControlOpe();
            return ParamConfig.RetParamConfiguracion(1);
        }

        private int ValNivAut()
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
            clsCNControlOpe ValNivAuto = new clsCNControlOpe();
            return ValNivAuto.RetNivelAutorizacion(1, Convert.ToInt32(hPerfil.Value));
        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
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
            if (ValidarDatIng() == "ERROR")
            {
                return;
            }
            if (Convert.ToDouble(txtMonRec.Text) <= 0)
            {
                script.Mensaje("El Monto de Recibo Debe Ser Mayor a Cero...");
                //MessageBox.Show("El Monto de Recibo Debe Ser Mayor a Cero...", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonRec.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cboConcepto.Text))
            {
                script.Mensaje("Debe seleccionar un concepto");
                //MessageBox.Show("Debe seleccionar un concepto", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //======================================================================
            //--Obtener Datos Generales
            //======================================================================
            int TipRec = Convert.ToInt32(this.cboTipRec.SelectedValue.ToString());
            int TipCon = Convert.ToInt32(this.cboConcepto.SelectedValue.ToString());
            int idCol;
            int idCli;
            int nSubItem;

            //======================================================================
            //--Valida, que Mínimo Registre un Colaborador o Cliente
            //======================================================================
            if (string.IsNullOrEmpty(this.conBuscarCliente1.idCliente.ToString()))
            {
                script.Mensaje("Debe Registrar por lo Menos un Colaborador o Cliente");
                //MessageBox.Show("Debe Registrar por lo Menos un Colaborador o Cliente", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //---Validar dato del Cliente
            if (string.IsNullOrEmpty(this.conBuscarCliente1.idCliente.ToString()))
            {
                idCli = 0;
            }
            else
            {
                idCli = Convert.ToInt32(this.conBuscarCliente1.idCliente.ToString());
            }
            ////--Validar dato del colaborador
            //if (string.IsNullOrEmpty(this.conBusCol.txtCod.Text.Trim()))
            //{
            //    idCol = 0;
            //}
            //else
            //{
            //    idCol = Convert.ToInt32(this.conBusCol.txtCod.Text.Trim());
            //}
            //--Validar si tiene sub items
            if (Convert.ToInt32(this.cboDetalle.SelectedIndex.ToString()) < 0)
            {
                nSubItem = 0;
            }
            else
            {
                nSubItem = Convert.ToInt32(this.cboDetalle.SelectedValue.ToString());
            }

            int TipMon = Convert.ToInt32(this.cboMoneda.SelectedValue.ToString());
            double nMonRec = Convert.ToDouble(this.txtMonRec.Text);
            double nMonTot = Convert.ToDouble(this.txtMonRec.Text);
            string cSust = this.txtSustento.Text.Trim();
            int idUsu = Convert.ToInt32(this.txtCodUsu.Text.Trim());

            int idAgeOri = objUsuario.nIdAgencia;
            int idAgeDest = Convert.ToInt32(this.cboAgencias.SelectedValue.ToString());

            string msg = "";

            clsCNControlOpe GuardarRec = new clsCNControlOpe();
            string cResul = GuardarRec.GuardarRecibo(TipRec, TipCon, nSubItem, 0, idCli, TipMon, nMonRec, 0, nMonTot, cSust, this.dtpFechaSis.SeleccionarFecha, idUsu, idAgeOri, idAgeDest, 0, ref msg);
            if (msg == "OK")
            {
                this.txtNroRec.Text = cResul;
                script.Mensaje("El Recibo se Registro Correctamente, NRO RECIBO: " + cResul);
                //MessageBox.Show("El Recibo se Registro Correctamente, NRO RECIBO: " + cResul, "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataTable dtRecibo = GuardarRec.BuscarRecibo(Convert.ToInt32(cResul), ref msg);
                //EmitirVoucher(dtRecibo);
            }
            else
            {
                script.Mensaje("Error al Registrar el Recibo");
                //MessageBox.Show(msg, "Error al Registrar el Recibo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            HabilitarControles(false);
            this.chcBuscar.Enabled = true;
            this.btnNuevo.Enabled = true;
            this.btnGrabar.Enabled = false;
            this.btnCancelar.Enabled = true;
            this.btnEliminar.Enabled = false;
            //this.btnImprimir.Enabled = true;
            //this.conBusCol.btnConsultar.Enabled = false;
            //this.conBusCli.btnBusCliente.Enabled = false;
        }

        private string ValidarDatIng()
        {

            if (string.IsNullOrEmpty(txtCodUsu.Text.Trim()))
            {
                script.Mensaje("Debe Existir un Codigo de Usuario Para Registrar el Recibo");
                //MessageBox.Show("Debe Existir un Codigo de Usuario Para Registrar el Recibo", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //txtCodUsu.Select();
                txtCodUsu.Focus();
                return "ERROR";
            }

            if (string.IsNullOrEmpty(cboTipRec.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Elegir un Tipo de Recibo");
                //MessageBox.Show("Debe Elegir un Tipo de Recibo", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //cboTipRec.Select();
                cboTipRec.Focus();
                return "ERROR";
            }

            if (string.IsNullOrEmpty(cboConcepto.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Elegir un Concepto para el Recibo");
                //MessageBox.Show("Debe Elegir un Concepto para el Recibo", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //cboConcepto.Select();
                cboConcepto.Focus();
                return "ERROR";
            }

            if (string.IsNullOrEmpty(cboMoneda.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Elegir la Moneda para el Recibo");
                //MessageBox.Show("Debe Elegir la Moneda para el Recibo", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //cboMoneda.Select();
                cboMoneda.Focus();
                return "ERROR";
            }

            if (string.IsNullOrEmpty(txtMonRec.Text.Trim()))//&& Convert.ToInt32(txtMonRec.Text.Trim()) == 0)
            {
                script.Mensaje("Debe Ingresar el Monto del Recibo");
                //MessageBox.Show("Debe Ingresar el Monto del Recibo", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //txtMonRec.Select();
                txtMonRec.Focus();
                return "ERROR";
            }

            if (string.IsNullOrEmpty(txtTotRec.Text.Trim()))//&& Convert.ToInt32(txtTotRec.Text.Trim()) == 0)
            {
                script.Mensaje("El Monto Total del Recibo no debe Ser Cero(0)...");
                //MessageBox.Show("El Monto Total del Recibo no debe Ser Cero(0)...", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //txtMonRec.Select();
                txtMonRec.Focus();
                return "ERROR";
            }

            if (string.IsNullOrEmpty(txtSustento.Text.Trim()))
            {
                script.Mensaje("Debe Ingresar el Sustento del Recibo");
                //MessageBox.Show("Debe Ingresar el Sustento del Recibo", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //txtSustento.Select();
                txtSustento.Focus();
                return "ERROR";
            }
            

            if (cboAgencias.SelectedValue.ToString() == "0")
            {
                script.Mensaje("Debe seleccionar una agencia destino");
                //MessageBox.Show("Debe seleccionar una agencia destino", "Registro de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //cboAgencias.Select();
                cboAgencias.Focus();
                return "ERROR";
            }
            return "OK";
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            CargarTiporecibos();
            if (this.cboTipRec.SelectedIndex < 0)  //(string.IsNullOrEmpty(txtCodUsu.Text.Trim()))
            {
                CargarConceptos(0);
            }
            else
            {
                CargarConceptos(Convert.ToInt32(cboTipRec.SelectedValue.ToString().Trim()));
            }
            CargarSubItemCon(0);
            cargarMoneda();
            cargarAgencia();
            //CargarSubItemCon();
            LimpiarControles();
            HabilitarControles(true);
            this.chcBuscar.Enabled = false;
            this.chcBuscar.Checked = false;
            this.txtNroRec.Enabled = false;
            this.btnNuevo.Enabled = false;
            this.btnGrabar.Enabled = true;
            this.btnCancelar.Enabled = true;
            this.btnEliminar.Enabled = false;
            //this.btnImprimir.Enabled = false;
            //this.conBusCol.btnConsultar.Enabled = false;
            //this.conBusCli.btnBusCliente.Enabled = false;
            this.cboTipRec.SelectedValue = "1";
            this.cboTipRec.Focus();
        }

        private void LimpiarControles()
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
            this.cboAgencias.SelectedValue = Convert.ToString(objUsuario.nIdAgencia);
            this.txtNroRec.Text="";
            //this.conBusCol.txtCod.Clear();
            //this.conBusCol.txtCargo.Clear();
            //this.conBusCol.txtNom.Clear();
            //this.conBusCli.txtCodCli.Clear();
            //this.conBusCli.txtNombre.Clear();
            //this.conBusCli.txtNroDoc.Clear();
            this.txtMonRec.Text = "";
            this.txtTotRec.Text = "";
            this.txtSustento.Text = "";
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

        protected void cboConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nItem;

            if (cboConcepto.SelectedIndex > 0)
            {
                nItem = Convert.ToInt32(this.cboConcepto.SelectedValue);
                CargarSubItemCon(nItem);
            }
            else
            {
                CargarSubItemCon(0);
            }

        }

        protected void cboTipRec_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaConceptos();
        }

        private void CargaConceptos()
        {
            if (cboTipRec.SelectedValue.ToString() == "1")
            {
                CargarConceptos(1);

                lblAge.Text = "Ag. Origen:";

            }
            else
            {
                CargarConceptos(2);
                lblAge.Text = "Ag. Destino:";
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

        protected void cboDetalle_SelectedIndexChanged(object sender, EventArgs e)
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
            if (this.cboDetalle.SelectedValue.ToString() == "1")
            {

                this.cboAgencias.Enabled = true;
            }
            else
            {
                this.cboAgencias.SelectedValue = Convert.ToString(objUsuario.nIdAgencia);
                this.cboAgencias.Enabled = false;
            }
        }
        
        protected void txtMonRec_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMonRec.Text.Trim()))
            {
                double nSuma = Convert.ToDouble(this.txtMonRec.Text);
                this.txtTotRec.Text = nSuma.ToString();
            }

        }

        protected void cboAgencias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            if (relacionCobranza)
            {
                script.Mensaje("No se puede eliminar este recibo ya que esta asociado a un cobro de crédito.");
                return;
            }

            if (confirmValue == "Si")
            {
                string msge = "";
                clsCNControlOpe ExtornarRec = new clsCNControlOpe();
                string cRpta = ExtornarRec.ExtornarRecibo(Convert.ToInt32(txtNroRec.Text.Trim()), ref msge);
                if (cRpta == "OK")
                {
                    script.Mensaje("El recibo se extorno correctamente.");
                }
                else
                {
                    script.Mensaje(msge);
                }
            }
            HabilitarControles(false);
            this.chcBuscar.Enabled = true;
            this.txtNroRec.Enabled = false;
            this.BotonBuscar1.Enabled = false;
            this.btnNuevo.Enabled = true;
            this.btnGrabar.Enabled = false;
            this.btnCancelar.Enabled = true;
            this.btnEliminar.Enabled = false;
            this.btnImprimir.Enabled = false;
        }

        protected void chcBuscar_CheckedChanged(object sender, EventArgs e)
        {
            HabilitarControles(false);
            LimpiarControles();
            if (this.chcBuscar.Checked)
            {
                this.txtNroRec.Enabled = true;
                this.BotonBuscar1.Enabled = true;
                this.btnNuevo.Enabled = false;
                this.btnGrabar.Enabled = false;
                this.btnCancelar.Enabled = false;
                this.btnEliminar.Enabled = false;
                this.txtNroRec.Focus();
            }
            else
            {
                this.txtNroRec.Enabled = false;
                this.BotonBuscar1.Enabled = false;
                this.btnNuevo.Enabled = true;
                this.btnGrabar.Enabled = false;
                this.btnCancelar.Enabled = false;
                this.btnEliminar.Enabled = false;
            }
        }

        protected void BotonBuscar1_Click(object sender, EventArgs e)
        {
            BuscarRecibo();
        }

        private void LimpiarControlesBus()
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
            this.cboAgencias.SelectedValue = objUsuario.nIdAgencia.ToString();
            conBuscarCliente1.LimpiarControl();
            this.txtMonRec.Text="";
            //this.txtMonItf.Clear();
            this.txtTotRec.Text="";
            this.txtSustento.Text="";
        }

        private void BuscarRecibo()
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

            LimpiarControlesBus();
            if (string.IsNullOrEmpty(this.txtNroRec.Text.Trim()))
            {
                script.Mensaje("Debe ingresar el número de recibo a buscar.");                
                this.txtNroRec.Focus();
                return;
            }

            string msge = "";
            clsCNControlOpe DatosRecibo = new clsCNControlOpe();
            DataTable tbRec = DatosRecibo.BuscarRecibo(Convert.ToInt32(txtNroRec.Text.Trim()), ref msge);
            if (msge == "OK")
            {
                if (tbRec.Rows.Count == 0)
                {
                    script.Mensaje("El número de recibo buscado no existe.");
                    this.txtNroRec.Focus();
                    this.btnCancelar.Enabled = true;
                    this.btnEliminar.Enabled = false;
                    return;
                }

                if (tbRec.Rows[0]["cEstadoRec"].ToString() == "X")
                {
                    script.Mensaje("El numero de recibo buscado esta elimindao");                    
                    this.txtNroRec.Focus();
                    this.btnCancelar.Enabled = true;
                    this.btnEliminar.Enabled = false;
                    return;
                }

                if (RetParamConfig() == 1)
                {
                    if (tbRec.Rows[0]["idUsuOpe"].ToString().Trim() != this.txtCodUsu.Text.Trim())
                    {
                        script.Mensaje("El Recibo fue Generado por Otro Usuario: No puede eliminarlo.");                        
                        this.txtNroRec.Focus();
                        this.btnCancelar.Enabled = true;
                        this.btnEliminar.Enabled = false;
                        return;
                    }
                }

                if (tbRec.Rows[0]["dFechaReg"].ToString() != this.dtpFechaSis.SeleccionarFecha.ToString())
                {
                    script.Mensaje("El recibo fue generado en otra fecha.");
                    
                    this.txtNroRec.Focus();
                    this.btnCancelar.Enabled = true;
                    this.btnEliminar.Enabled = false;
                    return;
                }

                if (RetParamConfig() == 1)
                {
                    if (Convert.ToInt32(tbRec.Rows[0]["idAgencia"].ToString()) != objUsuario.nIdAgencia)
                    {
                        script.Mensaje("El recibo fue generado en otra agencia.");
                        
                        this.txtNroRec.Focus();
                        this.btnCancelar.Enabled = true;
                        this.btnEliminar.Enabled = false;
                        return;
                    }
                }

                relacionCobranza = Convert.ToBoolean(tbRec.Rows[0]["RecXCobro"]);

                //============================================================
                //--Cargar Datos del Recibo
                //============================================================
                
                this.cboTipRec.SelectedValue = tbRec.Rows[0]["idTipRecibo"].ToString();
                CargaConceptos();
                this.cboMoneda.SelectedValue = tbRec.Rows[0]["idMoneda"].ToString();
                this.cboConcepto.SelectedValue = tbRec.Rows[0]["idConcepto"].ToString();
                this.cboDetalle.SelectedValue = tbRec.Rows[0]["idSubItem"].ToString();

                this.cboAgencias.SelectedValue = tbRec.Rows[0]["idAgencia"].ToString();
                this.lblAge.Text = (tbRec.Rows[0]["idTipRecibo"].ToString() == "1" ? "Ag. Origen" : "Ag. Destino");

                this.cboDetalle.Enabled = false;
                if (tbRec.Rows[0]["idColaborador"].ToString() != "0")
                {
                   // this.chcColab.Checked = true;
                    //this.conBusCol.txtCod.Text = tbRec.Rows[0]["idColaborador"].ToString();
                    //this.conBusCol.txtCargo.Text = tbRec.Rows[0]["cCargo"].ToString();
                    //this.conBusCol.txtNom.Text = tbRec.Rows[0]["NombreCol"].ToString();
                }
                else
                {
                    this.conBuscarCliente1.LimpiarControl();
                }

                //--datos del Cliente
                if (tbRec.Rows[0]["idCli"].ToString() != "0")
                {
                    //this.chcCliente.Checked = true;
                    //this.conBuscarCliente1.txtDocumento.txtCodCli.Text = tbRec.Rows[0]["idCli"].ToString();
                    this.conBuscarCliente1.txtDocumento.Text = tbRec.Rows[0]["cDocumentoID"].ToString();
                    this.conBuscarCliente1.txtNombres.Text = tbRec.Rows[0]["cNombre"].ToString();
                }
                else
                {
                    this.conBuscarCliente1.LimpiarControl();
                }

                //Asignar=tbRec.Rows[0]["idColaborador"];
                this.txtMonRec.Text = tbRec.Rows[0]["nMontoRec"].ToString();
                //this.txtMonItf.Text = tbRec.Rows[0]["nMontoITF"].ToString();
                this.txtTotRec.Text = tbRec.Rows[0]["nMontoTot"].ToString();
                this.txtSustento.Text = tbRec.Rows[0]["cSustento"].ToString();
                this.btnNuevo.Enabled = false;
                this.btnGrabar.Enabled = false;
                this.btnCancelar.Enabled = true;
                this.btnEliminar.Enabled = true;
                this.cboAgencias.Enabled = false;
            }
            else
            {
                script.Mensaje(msge);
            }
        }

        //protected void btnImprimir_Click(object sender, EventArgs e)
        //{
        //    List<ReportParameter> paramlist = new List<ReportParameter>();
        //    int idRec = Convert.ToInt32(this.txtNroRec.Text);
        //    paramlist.Add(new ReportParameter("idRec", idRec.ToString(), false));
        //    string reportpath = "/RPT/RptRecibosIE";
        //    new frmReporte(paramlist, reportpath).ShowDialog();
        //    this.btnImprimir.Enabled = true;


        //    List<ReportParameter> ListaParametros = new List<ReportParameter>();
        //    List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

        //    ListaDataSource.Add(new ReportDataSource("dtsPPG", new RPT.CapaNegocio.clsRPTCNPlanPagos().CNCronogramaPagos(idCuenta)));
        //    ListaDataSource.Add(new ReportDataSource("dtsAgencia", new RPT.CapaNegocio.clsRPTCNAgencia().CNAgenciaUsuario(EntityLayer.clsVarGlobal.nIdAgencia, EntityLayer.clsVarGlobal.User.idUsuario)));

        //    Session["ListaParametros"] = ListaParametros;
        //    Session["ListaDataSource"] = ListaDataSource;
        //    Session["lModal"] = true;

        //    var cReporte = "rptPlanPagoPosInt.rdlc";

        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);


        //}

               

        
    }
}