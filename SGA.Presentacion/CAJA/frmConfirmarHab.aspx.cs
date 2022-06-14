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
namespace SGA.Presentacion.CAJA
{
    public partial class frmConfirmarHab : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNControlOpe ControlOpe = new clsCNControlOpe();
        DataTable tbHabPen;

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
            ListaHabPed(1);
            //FormatoGridCli();
            //ListaHabPed(1);
            this.CheckBoxBase1.Checked = true;
            
            if (this.ValidarInicioOpe() != "A")
            {
                this.Dispose();
                return;
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
            int nidCli = objUsuario.idUsuario;
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

        private void ListaHabPed(int nOpc)
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
            int nidUsu = Convert.ToInt32(this.txtCodUsu.Text);
            clsCNControlOpe LisHabPen = new clsCNControlOpe();
            tbHabPen = LisHabPen.ListarHabPen(dtpFechaSis.SeleccionarFecha, objUsuario.nIdAgencia, nidUsu, nOpc, ref msge);
            if (msge == "OK")
            {
                ViewState["tbHabPen"] = tbHabPen;
                this.dtgHabPen.DataSource = tbHabPen;
                this.dtgHabPen.DataBind();
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Extraer Datos de Habilitaciones Pendientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void BotonAceptar1_Click(object sender, EventArgs e)
        {
            if (this.dtgHabPen.Rows.Count <= 0)
            {
                script.Mensaje("No Existe Habilitaciones por Confirmar...");
                //MessageBox.Show("No Existe Habilitaciones por Confirmar...", "Confirmar Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //var Msg = MessageBox.Show("Esta seguro de Confirmar la habilitación?...", "Confirmar Habilitaciones", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //if (Msg == DialogResult.No)
            //{
            //    return;
            //}

            if (dtgHabPen.Rows.Count > 0) 
            {
                clsCNControlOpe ConfirHabPen = new clsCNControlOpe();
                ListaHabPed(1);
                //clsCNControlOpe LisHabPen = new clsCNControlOpe();
                //DataTable tbHabPen = LisHabPen.ListarHabPen(dtpFechaSis.SeleccionarFecha, clsVarGlobal.nIdAgencia, nidUsu, nOpc, ref msge);
                int idHabi = Convert.ToInt32(tbHabPen.Rows[0]["idhabilita"].ToString());
                string cRpta = ConfirHabPen.ConfirmarHab(idHabi, dtpFechaSis.SeleccionarFecha);
                if (cRpta == "OK")
                {
                    script.Mensaje("La Confirmación de la Habilitación se Realizó Correctamente");
                    //MessageBox.Show("La Confirmación de la Habilitación se Realizó Correctamente", "Confirmar Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    script.Mensaje(cRpta);
                    //MessageBox.Show(cRpta, "Error al Confirmar la Habilitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.CheckBoxBase1.Checked = true;
            ListaHabPed(1);
            //FormatoGridCli();
            this.dtgHabPen.Focus();
            //this.dtgHabPen.Select();
        }

        protected void CheckBoxBase1_CheckedChanged(object sender, EventArgs e)
        {
            ListaHabPed(1);
            this.BotonAceptar1.Enabled = true;
            this.btnRechazar.Enabled = true;
            this.CheckBoxBase2.Checked = false;
        }

        protected void CheckBoxBase2_CheckedChanged(object sender, EventArgs e)
        {
            ListaHabPed(2);
            this.BotonAceptar1.Enabled = false;
            this.btnRechazar.Enabled = true;
            this.CheckBoxBase1.Checked = false;
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            var tbHabPen= (DataTable)ViewState["tbHabPen"];
            if (this.dtgHabPen.Rows.Count <= 0)
            {
                script.Mensaje("No Existe Habilitaciones por Rechazar...");
                //MessageBox.Show("No Existe Habilitaciones por Rechazar...", "Rechazar Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dtgHabPen.Rows.Count > 0)
            {
                clsCNControlOpe RechazHabPen = new clsCNControlOpe();
                int idHabi = Convert.ToInt32(dtgHabPen.Rows[0].Cells[0].Text);
                string cRpta = RechazHabPen.RechazarHab(idHabi);
                if (cRpta == "OK")
                {
                    script.Mensaje("El Rechazo de la Habilitación se Realizó Correctamente");
                    //MessageBox.Show("El Rechazo de la Habilitación se Realizó Correctamente", "Rechazar Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    script.Mensaje(cRpta);
                    //MessageBox.Show(cRpta, "Error al Rechazar la Habilitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (this.CheckBoxBase1.Checked)
                {
                    this.CheckBoxBase1.Checked = true;
                    ListaHabPed(1);
                }
                else
                {
                    this.CheckBoxBase2.Checked = true;
                    ListaHabPed(2);
                }
                this.dtgHabPen.Focus();
            }
        }
    }
}