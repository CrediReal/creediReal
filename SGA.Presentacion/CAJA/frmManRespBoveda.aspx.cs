using SGA.LogicaNegocio;
using SGA.Utilitarios;
using CAJ.CapaNegocio;
using SGA.ENTIDADES;
using GEN.CapaNegocio;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.CAJA
{
    public partial class frmManRespBoveda : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        public DataTable tbColAge;


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
            ListarColAgencia(objUsuario.nIdAgencia);
            
        }

        private void ListarColAgencia(int idAge)
        {
            clsCNControlOpe LisColAge = new clsCNControlOpe();
            tbColAge = LisColAge.ListarColPorAgencias(idAge);
            this.cboColaborador.DataSource = tbColAge;
            this.cboColaborador.DataValueField = tbColAge.Columns[0].ToString();
            this.cboColaborador.DataTextField = tbColAge.Columns[1].ToString();
            this.cboColaborador.DataBind();

            this.txtCargo.Text="";
            if (tbColAge.Rows.Count > 0)
            {
                int i = this.cboColaborador.SelectedIndex;
                this.txtCargo.Text = tbColAge.Rows[i]["cCargo"].ToString();
            }

            clsCNControlOpe LisResBovAge = new clsCNControlOpe();
            DataTable tbResBovAge = LisResBovAge.ListarResBovAge(idAge);
            this.dtgResBov.DataSource = tbResBovAge;
            //this.cboColaborador.DataValueField = tbColAge.Columns[0].ToString();
            //this.cboColaborador.DataTextField = tbColAge.Columns[1].ToString();
            this.dtgResBov.DataBind();
            if (tbResBovAge.Rows.Count > 0)
            {
                this.cboColaborador.SelectedValue = tbResBovAge.Rows[0]["idusuario"].ToString(); 
                this.cboColaborador.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
                this.btnGrabar.Enabled = false;
            }
            else
            {
                this.btnEditar.Enabled = false;
                this.btnGrabar.Enabled = true;
                this.btnCancelar.Enabled = false;
                this.cboColaborador.Enabled = true;
                this.cboColaborador.Focus();
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

        protected void cboAgencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAgencias.SelectedIndex > -1)
            {
                int nItem = Convert.ToInt32(cboAgencias.SelectedValue);
                ListarColAgencia(nItem);
            }
            //int nItem;
            //nItem = Convert.ToInt32(this.cboAgencias.SelectedValue.ToString());
            //ListarColAgencia(nItem);
        }

        protected void cboColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsCNControlOpe LisColAge = new clsCNControlOpe();
            tbColAge = LisColAge.ListarColPorAgencias(Convert.ToInt32(cboAgencias.SelectedValue));
            this.cboColaborador.DataSource = tbColAge;
            this.cboColaborador.DataBind();
            if (tbColAge.Rows.Count > 0)
            {
                int i = this.cboColaborador.SelectedIndex;
                this.txtCargo.Text = tbColAge.Rows[i]["cCargo"].ToString();
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            //==================================================================
            //--Validar Datos
            //==================================================================
            if (this.cboAgencias.SelectedIndex < 0)
            {
                script.Mensaje("Debe Seleccionar la Agencia");
                //MessageBox.Show("Debe Seleccionar la Agencia", "Mantenimiento de Responsables", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboAgencias.Focus();
                //this.cboAgencias.Select();
                return;
            }

            if (this.cboColaborador.SelectedIndex < 0)
            {
                script.Mensaje("Debe Seleccionar El Colaborador");
                //MessageBox.Show("Debe Seleccionar El Colaborador", "Mantenimiento de Responsables", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboColaborador.Focus();
                //this.cboColaborador.Select();
                return;
            }
            if (string.IsNullOrEmpty(this.txtCargo.ToString().Trim()))
            {
                script.Mensaje("El Claborador debe Tener un Cargo");
                //MessageBox.Show("El Claborador debe Tener un Cargo", "Mantenimiento de Responsables", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboColaborador.Focus();
                //this.cboColaborador.Select();
                return;
            }
            //==================================================================
            //--Guardar Datos
            //==================================================================
            int idUsu = Convert.ToInt32(this.cboColaborador.SelectedValue.ToString());
            int idAge = Convert.ToInt32(this.cboAgencias.SelectedValue.ToString());
            clsCNControlOpe GuardarresBov = new clsCNControlOpe();
            string msge = GuardarresBov.GuardarResBovAge(idUsu, idAge);
            if (msge == "OK")
            {
                script.Mensaje("Los Datos se Guardaron Correctamente");
                //MessageBox.Show("Los Datos se Guardaron Correctamente", "Mantenimiento de Responsables de Boveda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Registrar el Responsable de Boveda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ListarColAgencia(idAge);
            this.cboAgencias.Enabled = true;
            this.cboColaborador.Enabled = false;
            this.btnGrabar.Enabled = false;
            this.btnCancelar.Enabled = false;
            this.btnEditar.Enabled = false;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            this.cboAgencias.Enabled = false;
            this.cboColaborador.Enabled = true;
            this.btnGrabar.Enabled = true;
            this.btnCancelar.Enabled = true;
            this.btnEditar.Enabled = false;
            this.cboColaborador.Focus();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.cboAgencias.Enabled = true;
            this.cboColaborador.Enabled = false;
            this.btnGrabar.Enabled = false;
            this.btnCancelar.Enabled = false;
            this.btnEditar.Enabled = false;
        }
    }
}