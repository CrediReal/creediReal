using SGA.LogicaNegocio;
using SGA.Utilitarios;
using CAJ.CapaNegocio;
using SGA.ENTIDADES;
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
    public partial class frmCorteFraccionario : System.Web.UI.Page
    {

        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNControlOpe ControlOpe = new clsCNControlOpe();
        public DataTable tbMonSol;
        public DataTable tbBillSol;
        //public DataTable tbBillDol;

        public clsCorteFraccionario ObjCorteFraccionario
        {
            get
            {
                clsCorteFraccionario ObjCorteFraccionario = ViewState["ObjCorteFraccionario"] as clsCorteFraccionario;
                return ObjCorteFraccionario ?? new clsCorteFraccionario();
            }
            set
            {
                ViewState["ObjCorteFraccionario"] = value;
            }
        }

        public List<clsCorteFraccionario> LstCorteFraccionario
        {
            get
            {
                List<clsCorteFraccionario> ObjCorteFraccionario = ViewState["ObjCorteFraccionario"] as List<clsCorteFraccionario>;
                return ObjCorteFraccionario ?? new List<clsCorteFraccionario>();
            }
            set
            {
                ViewState["LstCorteFraccionario"] = value;
            }
        }
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
            DatosUsuario();
            if (ValidarCorteFracc() == "ERROR")
            {
                //---Llenar Grid
                ListarMonedaSoles(2);
                ListarBilleteSoles(2);
                dtgMonedas.Enabled = false;
                dtgBilletes.Enabled = false;
                this.btnGrabar.Enabled = false;
                this.btnCancelar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnImprimir.Enabled = true;
                calcularTotalMoneda();
                calcularTotalBillete();
            }
            else
            {
                //---Llenar Grid
                ListarMonedaSoles(1);
                ListarBilleteSoles(1);
                dtgMonedas.Enabled = true;
                dtgBilletes.Enabled = true;
                this.btnGrabar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = false;
            }
            //===========================================================================================
            //--Validar Inicio de Operaciones
            //===========================================================================================
            if (this.ValidarInicioOpe() != "A")
            {
                this.Dispose();
                return;
            }

            //===========================================================================================
            //--Validar Corte Fraccionario
            //=============================

        }

        protected void dtgMonedas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dtgMonedas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dtgMonedas.EditIndex = e.NewEditIndex;
            //dtgMonedas.DataBind();
        }

        //protected void GridViewBase1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    //GridViewBase1.EditIndex = -1;
        //    //GridViewBase1.DataBind();
        //}

        //protected void GridViewBase1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{

        //}

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
            int nidCli = objUsuario.idUsuario;
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
            clsCNControlOpe ValCorFra = new clsCNControlOpe();
            string cCorFra = ValCorFra.ValidaCorteFracc(this.dtpFechaSis.SeleccionarFecha, objUsuario.idUsuario, objUsuario.nIdAgencia, ref msge);
            if (msge == "OK")
            {
                if (cCorFra != "0")
                {
                    script.Mensaje("Ya Realizó su Corte Fraccionario");
                    //MessageBox.Show("Ya Realizó su Corte Fraccionario", "Validar Corte Fraccionario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //dtgMonedas.Enabled = false;
                   //dtgBilletes.Enabled = false;
                    //dtgBillDolares.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnGrabar.Enabled = false;
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

        private void ListarMonedaSoles(int nOpc)
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
            string msge = "";
            clsCNControlOpe LisMonSol = new clsCNControlOpe();
            if (nOpc == 1)
            {
                tbMonSol = LisMonSol.ListarBillMon(1, 1, ref msge);
                if (msge == "OK")
                {
                    tbMonSol.AcceptChanges();
                    tbMonSol.Columns["nCantidad"].ReadOnly = false;
                    tbMonSol.Columns["nTotal"].ReadOnly = false;
                    this.dtgMonedas.DataSource = tbMonSol;
                    this.dtgMonedas.DataBind();
                }
                else
                {
                    script.Mensaje(msge);
                    //MessageBox.Show(msge, "Error al Extraer Datos de Monedas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                tbMonSol = LisMonSol.ListarCorteFrac(this.dtpFechaSis.SeleccionarFecha, objUsuario.idUsuario, objUsuario.nIdAgencia, 1, 1, ref msge);
                if (msge == "OK")
                {
                    tbMonSol.AcceptChanges();
                    tbMonSol.Columns["nCantidad"].ReadOnly = false;
                    tbMonSol.Columns["nTotal"].ReadOnly = false;
                    this.dtgMonedas.DataSource = tbMonSol;
                    this.dtgMonedas.DataBind();

                }
                else
                {
                    script.Mensaje(msge);
                    //MessageBox.Show(msge, "Error al Extraer Datos de Monedas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void ListarBilleteSoles(int nOpc)
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
            string msge = "";
            clsCNControlOpe LisBillSol = new clsCNControlOpe();
            if (nOpc == 1)
            {
                tbBillSol = LisBillSol.ListarBillMon(1, 2, ref msge);
                if (msge == "OK")
                {
                    tbBillSol.AcceptChanges();
                    tbBillSol.Columns["nCantidad"].ReadOnly = false;
                    tbBillSol.Columns["nTotal"].ReadOnly = false;
                    this.dtgBilletes.DataSource = tbBillSol;
                    this.dtgBilletes.DataBind();
                }
                else
                {
                    script.Mensaje(msge);
                    //MessageBox.Show(msge, "Error al Extraer Datos de Billetes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                tbBillSol = LisBillSol.ListarCorteFrac(this.dtpFechaSis.SeleccionarFecha, objUsuario.idUsuario, objUsuario.nIdAgencia, 1, 2, ref msge);
                if (msge == "OK")
                {
                    tbBillSol.AcceptChanges();
                    tbBillSol.Columns["nCantidad"].ReadOnly = false;
                    tbBillSol.Columns["nTotal"].ReadOnly = false;
                    this.dtgBilletes.DataSource = tbBillSol;
                    this.dtgBilletes.DataBind();
                }
                else
                {
                    script.Mensaje(msge);
                    //MessageBox.Show(msge, "Error al Extraer Datos de Billetes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        protected void BotonEditar2_Click(object sender, EventArgs e)
        {
            int nCantidad = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjCorteFraccionario = LstCorteFraccionario.FirstOrDefault(x => x.nCantidad == nCantidad);
            if (ObjCorteFraccionario == null)
            {
                script.Mensaje("No se puede editar el registro.");
                return;
            }

        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            TextBox txtTotal = (TextBox)row.FindControl("txtTotal");

            var nCantidad = Convert.ToDouble(txtCantidad.Text);
            var nValor = Convert.ToDouble(row.Cells[0].Text);
            var nTotal = string.Format("{0:0.00}",nCantidad * nValor);
            txtTotal.Text = nTotal;
            calcularTotalMoneda();
        }

        protected void txtCantidad2_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad2");
            TextBox txtTotal = (TextBox)row.FindControl("txtTotal2");

            var nCantidad = Convert.ToDouble(txtCantidad.Text);
            var nValor = Convert.ToDouble(row.Cells[0].Text);
            var nTotal = string.Format("{0:0.00}", nCantidad * nValor);
            txtTotal.Text = nTotal;
            calcularTotalBillete();
        }

        private void calcularTotalMoneda()
        {
            var nTotal=0.0;
            for (int i = 0; i < dtgMonedas.Rows.Count; i++)
            {
                nTotal = nTotal + Convert.ToDouble(((TextBox)dtgMonedas.Rows[i].FindControl("txtTotal")).Text);
            }
            txtMonedas.Text = string.Format("{0:0.00}", nTotal);
            txtTotal.Text = string.Format("{0:0.00}", txtMonedas.Value + txtBilletes.Value);
        }

        private void calcularTotalBillete()
        {
            var nTotal = 0.0;
            for (int i = 0; i < this.dtgBilletes.Rows.Count; i++)
            {
                nTotal = nTotal + Convert.ToDouble(((TextBox)dtgBilletes.Rows[i].FindControl("txtTotal2")).Text);
            }
            this.txtBilletes.Text = string.Format("{0:0.00}", nTotal);
            txtTotal.Text = string.Format("{0:0.00}", txtMonedas.Value + txtBilletes.Value);
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
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
            //===================================================================
            //Guardar Datos de Monedas Mediante XML
            //===================================================================          
            DataSet dsMonSol = new DataSet("dsMonSol");
            dsMonSol.Tables.Add(dtMonedas());
            string xmlMonSol = dsMonSol.GetXml();
            xmlMonSol = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(xmlMonSol);
            dsMonSol.Tables.Clear();

            //===================================================================
            //Guardar Datos de Billetes Soles Mediante XML
            //===================================================================          
            DataSet dsBillSol = new DataSet("dsBillSol");
            dsBillSol.Tables.Add(dtBilletes());
            string xmlBillSol = dsBillSol.GetXml();
            xmlBillSol = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(xmlBillSol);
            dsBillSol.Tables.Clear();

            //===================================================================
            //Guardar Datos de Billetes Dol Mediante XML
            //===================================================================          
            DataSet dsBillDol = new DataSet("dsBillDol");
            //dsBillDol.Tables.Add(tbBillDol);
            string xmlBillDol = dsBillDol.GetXml();
            xmlBillDol = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(xmlBillDol);
            dsBillDol.Tables.Clear();

            //==================================================
            //--Grabar Corte Fraccionario
            //==================================================
            clsCNControlOpe RegCorFra = new clsCNControlOpe();
            string cRpta = RegCorFra.registroCorFrac(Convert.ToInt32(this.txtCodUsu.Text), this.dtpFechaSis.SeleccionarFecha, objUsuario.nIdAgencia, xmlMonSol, xmlBillSol, xmlBillDol);
            if (cRpta == "OK")
            {
                script.Mensaje("El billetaje se Registro Correctamente.");
            }
            else
            {
                script.Mensaje(cRpta);
            }
            this.btnGrabar.Enabled = false;
            this.dtgMonedas.Enabled = false;
            this.dtgBilletes.Enabled = false;
           // this.dtgBillDolares.Enabled = false;
            this.btnImprimir.Enabled = true;
        }

        private DataTable dtMonedas()
        {
            DataTable dt= new DataTable();
             string msge = "";
            clsCNControlOpe LisMonSol = new clsCNControlOpe();

            dt = LisMonSol.ListarBillMon(1, 1, ref msge);
            dt.Columns["nCantidad"].ReadOnly = false;
            dt.Columns["nTotal"].ReadOnly = false;
            for (int i = 0; i < dtgMonedas.Rows.Count; i++)
            {
                dt.Rows[i]["nCantidad"]= Convert.ToDouble(((TextBox)dtgMonedas.Rows[i].FindControl("txtCantidad")).Text);
                dt.Rows[i]["nTotal"] = Convert.ToDouble(((TextBox)dtgMonedas.Rows[i].FindControl("txtTotal")).Text);
            }

            return dt;
        }

        private DataTable dtBilletes()
        {
            DataTable dt = new DataTable();
            string msge = "";
            clsCNControlOpe LisBillSol = new clsCNControlOpe();

            dt = LisBillSol.ListarBillMon(1, 2, ref msge);
            dt.Columns["nCantidad"].ReadOnly = false;
            dt.Columns["nTotal"].ReadOnly = false;
            for (int i = 0; i < dtgBilletes.Rows.Count; i++)
            {
                dt.Rows[i]["nCantidad"] = Convert.ToDouble(((TextBox)this.dtgBilletes.Rows[i].FindControl("txtCantidad2")).Text);
                dt.Rows[i]["nTotal"] = Convert.ToDouble(((TextBox)dtgBilletes.Rows[i].FindControl("txtTotal2")).Text);
            }

            return dt;
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
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
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();
          
            DateTime dFecha = this.dtpFechaSis.SeleccionarFecha;
            int idUsu = objUsuario.idUsuario;
            int idAge = objUsuario.nIdAgencia;
            ListaDataSource.Add(new ReportDataSource("dsCorteFracc", new clsRPTCNCaja().CNCorteFracc(dFecha, idUsu, idAge)));

            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;
            var cReporte = "rptCorteFracc.rdlc";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void btnEditar_Click(object sender, EventArgs e)
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

            if (ValidaRespBoveda() == "0")
            {
                string msge = "";
                clsCNControlOpe ValCorteFra = new clsCNControlOpe();
                string cRpta = ValCorteFra.ValAutCorteFracc(this.dtpFechaSis.SeleccionarFecha, Convert.ToInt32(this.txtCodUsu.Text), objUsuario.nIdAgencia, 1, ref msge);
                if (msge == "OK")
                {
                    if (cRpta == "N")
                    {
                        script.Mensaje("Debe Solicitar Autorización Para Modificar el Corte Fraccionario");
                        return;
                    }
                }
                else
                {
                    script.Mensaje(msge);
                    return;
                }
            }

            //--Desabilitar botones
            this.dtgMonedas.Enabled = true;
            this.dtgBilletes.Enabled = true;
            //this.dtgBillDolares.Enabled = true;
            this.btnGrabar.Enabled = true;
            this.btnEditar.Enabled = false;
            this.btnCancelar.Enabled = true;
            this.btnImprimir.Enabled = false;
        }

        private string ValidaRespBoveda()
        {
            if (string.IsNullOrEmpty(this.txtCodUsu.Text.Trim()))
            {
                script.Mensaje("No Existe Usuario");
                return "ERROR";
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
            clsCNControlOpe ValidaResBov = new clsCNControlOpe();
            string cValUsu = ValidaResBov.RetRespBoveda(Convert.ToInt32(this.txtCodUsu.Text.Trim().ToString()), objUsuario.nIdAgencia);
            // Si valor es: 0--> usuario no Es Responsable de Boveda, 1 u otro Valor--> Es responsable de Boveda
            return cValUsu;
        }
    }
}