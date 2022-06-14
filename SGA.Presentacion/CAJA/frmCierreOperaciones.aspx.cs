using SGA.LogicaNegocio;
using SGA.Utilitarios;
using CAJ.CapaNegocio;
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

namespace SGA.Presentacion.CAJA
{
    public partial class frmCierreOperaciones : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNControlOpe ControlOpe = new clsCNControlOpe();
        public DataTable tbIngSol;
        public DataTable tbEgrSol;

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
            //DataTable dtEstadoCajaAgencia = ControlOpe.CNValidaAgenciaApertura(EntityLayer.clsVarGlobal.dFecSystem, EntityLayer.clsVarGlobal.nIdAgencia);
            //if (Convert.ToBoolean(dtEstadoCajaAgencia.Rows[0]["nApertura"]))
            //{
            //    script.Mensaje("La agencia ya esta cerrada,no es posible Inciar Operaciones");
            //    //MessageBox.Show("La agencia ya esta cerrada," + "\n" + "no es posible Inciar Operaciones", "Inicio de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    this.pnlCierre.Visible = false;
            //    return;
            //}

            DatosUsuario();
            if (this.ValidarInicioOpe() != "A")
            {
                this.pnlCierre.Visible = false;
                return;
            }

            //--Validar si ya Realizó su corte Fraccionario
            if (ValidarCorteFracc() == "ERROR")
            {
                this.pnlCierre.Visible = false;
                return;
            }

            if (ValidaRespBoveda() != "0")
            {
                if (VerificaCierreOpe() != "OK")
                {
                    this.pnlCierre.Visible = false;
                    return;
                }
            }

            if (CuadreOpe() != "OK")
            {
                script.Mensaje(CuadreOpe());
                //MessageBox.Show(CuadreOpe(), "Error al Extraer Datos de las Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //FormatoGrid();

            if (SalIniOpe() != "OK")
            {
                script.Mensaje(SalIniOpe());
                //MessageBox.Show(SalIniOpe(), "Error al Extraer Datos de las Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaldoFinal();
            //--Saldo de Corte Fraccionario
            if (SaldoCorteFraccionario() != "OK")
            {
                script.Mensaje(SaldoCorteFraccionario());
                //MessageBox.Show(SaldoCorteFraccionario(), "Error al Extraer Datos de las Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //tbcCieOpe.SelectedIndex = 1;
            //FormatoGridDol();
            //tbcCieOpe.SelectedIndex = 0;
            this.btnGrabar.Enabled = true;


        }


        private void DatosUsuario()
        {
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
            this.dtpFechaSis.SeleccionarFecha = objUsuario.dFecSystem;
            this.txtCodUsu.Text = objUsuario.idUsuario.ToString();
            txtUsuario.Text = objUsuario.cWinuser.ToString();
            int nidCli = Convert.ToInt32(objUsuario.idUsuario);
            CLI.CapaNegocio.clsCNRetDatosCliente RetDatCli = new CLI.CapaNegocio.clsCNRetDatosCliente();
            DataTable DatosCli = RetDatCli.ListarDatosCli(nidCli, "D");
            if (DatosCli.Rows.Count > 0)
            {
                txtNomUsu.Text = DatosCli.Rows[0]["cNombre"].ToString();
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
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(hUsuario.Value);
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

        private string ValidarCorteFracc()
        {
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
            string cRpta = "OK";
            string msge = "";
            clsCNControlOpe ValCorFra = new clsCNControlOpe();
            string cCorFra = ValCorFra.ValidaCorteFracc(this.dtpFechaSis.SeleccionarFecha, objUsuario.idUsuario, objUsuario.nIdAgencia, ref msge);
            if (msge == "OK")
            {
                if (cCorFra == "0")
                {
                    script.Mensaje("Primero debe Realizar su Corte Fraccionario... por Favor..");
                    //MessageBox.Show("Primero debe Realizar su Corte Fraccionario... por Favor..", "Validar Corte Fraccionario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cRpta = "ERROR";
                }
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Validar Corte Fraccionario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cRpta = "ERROR";
            }
            return cRpta;
        }


        private string ValidaRespBoveda()
        {
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
            if (string.IsNullOrEmpty(this.txtCodUsu.Text.Trim()))
            {
                script.Mensaje("No Existe Usuario");
                //MessageBox.Show("No Existe Usuario", "Validar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "ERROR";
            }
            clsCNControlOpe ValidaResBov = new clsCNControlOpe();
            string cValUsu = ValidaResBov.RetRespBoveda(Convert.ToInt32(this.txtCodUsu.Text.Trim().ToString()), objUsuario.nIdAgencia);
            // Si valor es: 0--> usuario no Es Responsable de Boveda, 1 u otro Valor--> Es responsable de Boveda
            return cValUsu;
        }

        private string VerificaCierreOpe()
        {
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
            string cRpta = "";
            clsCNControlOpe ValCieOpe = new clsCNControlOpe();
            string msg = "";
            DataTable tbvalcierre = ValCieOpe.ValidarCierreOpe(this.dtpFechaSis.SeleccionarFecha, objUsuario.nIdAgencia, ref msg);
            if (msg == "OK")
            {
                if (tbvalcierre.Rows.Count > 0)
                {
                    for (int i = 0; i < tbvalcierre.Rows.Count; i++)
                    {
                        cRpta = cRpta + tbvalcierre.Rows[i]["cNombre"].ToString() + " ;";
                    }
                    cRpta = "FALTA QUE CIERREN CAJA: " + cRpta;
                    script.Mensaje(cRpta);
                    //MessageBox.Show(cRpta, "Validar Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cRpta = "OK";
                }
            }
            return cRpta;
        }

        private string CuadreOpe()
        {
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
            string msge = "";
            int idUsu = Convert.ToInt32(this.txtCodUsu.Text);
            clsCNControlOpe CuadreOpe = new clsCNControlOpe();
            //=====================================================================
            //---Ingresos en Soles
            //=====================================================================
            tbIngSol = CuadreOpe.CuadreOperaciones(this.dtpFechaSis.SeleccionarFecha, idUsu, 1, objUsuario.nIdAgencia, 1, ref msge);
            if (msge == "OK")
            {
                this.dtgIngSoles.DataSource = tbIngSol;
                this.dtgIngSoles.DataBind();
                if (tbIngSol.Rows.Count > 0)
                {
                    this.txtMonIngSol.Text = tbIngSol.Rows[0]["nTotal"].ToString();
                }
            }
            else
            {
                return msge;
            }

            //=====================================================================
            //---Egresos en Soles
            //=====================================================================
            tbEgrSol = CuadreOpe.CuadreOperaciones(this.dtpFechaSis.SeleccionarFecha, idUsu, 1, objUsuario.nIdAgencia, 2, ref msge);
            if (msge == "OK")
            {
                this.dtgEgrSoles.DataSource = tbEgrSol;
                this.dtgEgrSoles.DataBind();
                if (tbEgrSol.Rows.Count > 0)
                {
                    this.txtMonEgrSol.Text = tbEgrSol.Rows[0]["nTotal"].ToString();
                }
            }
            else
            {
                //MessageBox.Show(msge, "Error al Extraer Datos de las Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // return false;
                return msge;
            }           
            return "OK";
        }


        private string SalIniOpe()
        {
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
            string msge = "";
            int idUsu = Convert.ToInt32(this.txtCodUsu.Text);
            clsCNControlOpe saldoIniOpe = new clsCNControlOpe();
            //=====================================================================
            //---Ingresos en Soles
            //=====================================================================
            DataTable tbSalIniOpe = saldoIniOpe.SaldoinicialOpe(this.dtpFechaSis.SeleccionarFecha, idUsu, objUsuario.nIdAgencia, ref msge);
            if (msge == "OK")
            {
                if (tbSalIniOpe.Rows.Count > 0)
                {
                    this.txtSalIniSol.Text = tbSalIniOpe.Rows[0]["nSalIniSol"].ToString();
                    
                }
            }
            else
            {
                return msge;
                // MessageBox.Show(msge, "Error al Extraer El Saldo Inicial...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "OK";
        }

        private void SaldoFinal()
        {
            //====================
            //--SALDO FINA SOLES
            //====================
            this.txtSalFinSol.Text = Convert.ToString(Math.Round((Math.Round(Convert.ToDouble(this.txtSalIniSol.Text), 2) + Math.Round(Convert.ToDouble(this.txtMonIngSol.Text), 2) - Math.Round(Convert.ToDouble(this.txtMonEgrSol.Text), 2)), 2));
        }

        private string SaldoCorteFraccionario()
        {
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
            double nMonSoles = 0.00, nMonDolar = 0.00;
            clsCNOpeControl SalCorteFrac = new clsCNOpeControl();
            string cRpta = SalCorteFrac.RetMontoCorFracc(this.dtpFechaSis.SeleccionarFecha, objUsuario.idUsuario, objUsuario.nIdAgencia, ref nMonSoles, ref nMonDolar);
            if (cRpta == "OK")
            {
                this.txtCortSoles.Text = nMonSoles.ToString();
                this.txtDifSoles.Text = Convert.ToString(Math.Round((Math.Round(nMonSoles, 2) - Math.Round(Convert.ToDouble(this.txtSalFinSol.Text), 2)), 2));
                //=========================================================================
                //-----Validar Cierre de Operaciones en Soles
                //=========================================================================
                double nDifSol = Math.Round(Convert.ToDouble(this.txtDifSoles.Text), 2);
                if (nDifSol == 0)
                {
                    this.lblSoles.Text = "CIERRE EN SOLES OK...";
                    this.lblSoles.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    if (nDifSol > 0)
                    {
                        this.lblSoles.Text = "EXISTE DIFERENCIAS, VERIFICAR!!! (Emitir Recibo de INGRESO por SOBRANTE)";
                        this.lblSoles.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        this.lblSoles.Text = "EXISTE DIFERENCIAS, VERIFICAR!!! (Emitir Recibo de EGRESO por FALTANTE)";
                        this.lblSoles.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            else
            {
                return cRpta;
                //MessageBox.Show(cRpta, "Error al Extraer El Saldo Inicial...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "OK";
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
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
            //===================================================================
            //--Validar Datos del Cuadre
            //===================================================================          
            if (Convert.ToDouble(this.txtSalFinSol.Text) < 0)
            {
                script.Mensaje("El Saldo Final en SOLES NO Puede ser NEGATIVO: VERIFICAR...");
                //MessageBox.Show("El Saldo Final en SOLES NO Puede ser NEGATIVO: VERIFICAR...", "Validar Cierre de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Convert.ToDouble(this.txtDifSoles.Text) != 0)
            {
                script.Mensaje("Existe Diferencia en SOLES entre el CORTE FRACCIONARIO y CUADRE CAJA.. VERIFICAR...");
                //MessageBox.Show("Existe Diferencia en SOLES entre el CORTE FRACCIONARIO y CUADRE CAJA.. VERIFICAR...", "Validar Cierre de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //================================================
            //--Valida si Tiene habilitaciones Pendientes
            //================================================
            clsCNControlOpe ValHabPen = new clsCNControlOpe();
            string cRptahabpen = ValHabPen.ValidaHabPendientes(this.dtpFechaSis.SeleccionarFecha, Convert.ToInt32(this.txtCodUsu.Text), objUsuario.nIdAgencia, 1);
            if (Convert.ToInt32(cRptahabpen) > 0)
            {
                script.Mensaje("No puede Cerrar porque Tiene Habilitaciones Pendientes por APROBAR o RECHAZAR...");
                //MessageBox.Show("No puede Cerrar porque Tiene Habilitaciones Pendientes por APROBAR o RECHAZAR...", "Validar Cierre de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //================================================
            //--Valida si Tiene Habilitaciones Generadas
            //================================================
            clsCNControlOpe ValHabGen = new clsCNControlOpe();
            string cRptaHabGen = ValHabGen.ValidaHabPendientes(this.dtpFechaSis.SeleccionarFecha, Convert.ToInt32(this.txtCodUsu.Text), objUsuario.nIdAgencia, 2);
            if (Convert.ToInt32(cRptaHabGen) > 0)
            {
                script.Mensaje("No puede Cerrar porque Tiene Habilitaciones GENERADAS...");
                //MessageBox.Show("No puede Cerrar porque Tiene Habilitaciones GENERADAS...", "Validar Cierre de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //var Msg = MessageBox.Show("Esta Seguro de Cerrar su Caja?...", "Registrar Corte Fraccionario", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            //if (Msg == DialogResult.No)
            //{
            //    return;
            //}

            //===================================================================
            //Guardar Datos de Ingreso en Soles XML
            //===================================================================
            string msge = "";
            int idUsu = Convert.ToInt32(this.txtCodUsu.Text);
            clsCNControlOpe CuadreOpe = new clsCNControlOpe();
            tbIngSol = CuadreOpe.CuadreOperaciones(this.dtpFechaSis.SeleccionarFecha, idUsu, 1, objUsuario.nIdAgencia, 1, ref msge);
            DataSet dsIngSol = new DataSet("dsIngSol");
            dsIngSol.Tables.Add(tbIngSol);
            string xmlIngSol = dsIngSol.GetXml();
            xmlIngSol = clsCNFormatoXML.EncodingXML(xmlIngSol);
            dsIngSol.Tables.Clear();

            //===================================================================
            //Guardar Datos de Egresos en Soles XML
            //===================================================================  
            tbEgrSol = CuadreOpe.CuadreOperaciones(this.dtpFechaSis.SeleccionarFecha, idUsu, 1, objUsuario.nIdAgencia, 2, ref msge);
            DataSet dsEgrSol = new DataSet("dsEgrSol");
            dsEgrSol.Tables.Add(tbEgrSol);
            string xmlEgrSol = dsEgrSol.GetXml();
            xmlEgrSol = clsCNFormatoXML.EncodingXML(xmlEgrSol);
            dsEgrSol.Tables.Clear();

            //===================================================================
            //Guardar Datos de ingreso en Dólares XML
            //===================================================================          
            DataSet dsIngDol = new DataSet("dsIngDol");
            dsIngDol.Tables.Add("");
            string xmlIngDol = dsIngDol.GetXml();
            xmlIngDol = clsCNFormatoXML.EncodingXML(xmlIngDol);
            dsIngDol.Tables.Clear();

            //===================================================================
            //Guardar Datos de Egresos en Dolares XML
            //===================================================================          
            DataSet dsEgrDol = new DataSet("dsEgrDol");
            dsEgrDol.Tables.Add("");
            string xmlEgrDol = dsEgrDol.GetXml();
            xmlEgrDol = clsCNFormatoXML.EncodingXML(xmlEgrDol);
            dsEgrDol.Tables.Clear();

            //==================================================
            //--Grabar Cuadre Operaciones
            //==================================================
            double nSalIniSol = Convert.ToDouble(this.txtSalIniSol.Text);
            double nSalFinSol = Convert.ToDouble(this.txtSalFinSol.Text);
            clsCNControlOpe RegCieOpe = new clsCNControlOpe();
            string cRpta = RegCieOpe.RegCierreOperaciones(this.dtpFechaSis.SeleccionarFecha, Convert.ToInt32(this.txtCodUsu.Text), objUsuario.nIdAgencia,
                                            nSalIniSol, 0, nSalFinSol, 0, xmlIngSol, xmlEgrSol, xmlIngDol, xmlEgrDol);
            if (cRpta == "OK")
            {
                this.btnGrabar.Enabled = false;
                this.btnImprimir.Enabled = true;
                this.btnImpDetall.Enabled = true;
                script.Mensaje("El Cierre de Operaciones se Realizó Correctamente...");
                //MessageBox.Show("El Cierre de Operaciones se Realizó Correctamente...", "Cierre de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //==================================================
                //--Actualizar Cierre
                //==================================================
                ActualizarCierre();
            }
            else
            {
                this.btnGrabar.Enabled = false;
                this.btnImprimir.Enabled = false;
                this.btnImpDetall.Enabled = false;
                script.Mensaje(cRpta);
                //MessageBox.Show(cRpta, "Cierre de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void ActualizarCierre()
        {

            if (CuadreOpe() != "OK")
            {
                script.Mensaje(CuadreOpe());
                //MessageBox.Show(CuadreOpe(), "Error al Extraer Datos de las Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SalIniOpe() != "OK")
            {
                script.Mensaje(SalIniOpe());
                //MessageBox.Show(SalIniOpe(), "Error al Extraer Datos de las Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaldoFinal();
            //--Saldo de Corte Fraccionario
            if (SaldoCorteFraccionario() != "OK")
            {
                script.Mensaje(SaldoCorteFraccionario());
                //MessageBox.Show(SaldoCorteFraccionario(), "Error al Extraer Datos de las Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        protected void btnImpDetall_Click(object sender, EventArgs e)
        {
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
            DateTime dFecha = this.dtpFechaSis.SeleccionarFecha;
            int idUsu = objUsuario.idUsuario;
            int idAge = objUsuario.nIdAgencia;

            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("dsKardex", new clsRPTCNCaja().CNDetallOperaciones(dFecha, idUsu, idAge)));
            ListaParametros.Add(new ReportParameter("dFecOpe", dFecha.ToString(), false));

            Session["ListaDataSource"] = ListaDataSource;
            Session["ListaParametros"] = ListaParametros;
            Session["lModal"] = true;
            var cReporte = "rptDetalleOpe.rdlc";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
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
            DateTime dFecha = this.dtpFechaSis.SeleccionarFecha;
            int idUsu = objUsuario.idUsuario;
            int idAge = objUsuario.nIdAgencia;

            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("rptResumenOpe", new clsRPTCNCaja().CNResumenOpeSol(dFecha, idUsu, idAge)));
            ListaDataSource.Add(new ReportDataSource("rptResumenDol", new clsRPTCNCaja().CNResumenOpeDol(dFecha, idUsu, idAge)));

            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;
            var cReporte = "rptResumenOpe.rdlc";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);


        }

    }
}