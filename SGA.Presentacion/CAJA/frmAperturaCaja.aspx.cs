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
    public partial class frmAperturaCaja : System.Web.UI.Page
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

            if (this.cboAgencias.SelectedIndex > 0)
            {
                this.cboAgencias.SelectedValue = "1";
                //this.cboAgencias.Select();
            }
        }

            private void ListarColAgencia(int idAge)
        {
            clsCNControlOpe LisColAge = new clsCNControlOpe();
            DataTable tbColAge = LisColAge.ListarColabAgencias(idAge);
            this.cboColaborador.DataSource = tbColAge;
            cboColaborador.DataValueField = tbColAge.Columns[0].ToString();
            cboColaborador.DataTextField = tbColAge.Columns[1].ToString();
            cboColaborador.DataBind();

            if (tbColAge.Rows.Count>0)
            {
                this.cboColaborador.Enabled = true;
            }
            else
            {
                this.cboColaborador.Enabled = false;
            }
        }


        private void chcCorteFracc_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chcCorteFracc.Checked)
            {
                this.btnAceptar.Enabled = true;
                this.chcApeCaja.Checked = false;
            }
            else
            {
                this.btnAceptar.Enabled = false;
                this.chcApeCaja.Checked = false;
            }
        }

        private string ValidarCorte()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            string msge = "";
            int nAgeRes = Convert.ToInt32(this.cboAgencias.SelectedValue.ToString());
            int nColRes = Convert.ToInt32(this.cboColaborador.SelectedValue.ToString());
            clsCNControlOpe ValCorteFra = new clsCNControlOpe();
            string cRpta = ValCorteFra.ValAutCorteFracc(objUsuario.dFecSystem, nColRes, nAgeRes, 1, ref msge);
            if (msge == "OK")
            {
                if (cRpta == "0")
                {
                    script.Mensaje("No tiene Corte Fraccionario Registrado");
                    //MessageBox.Show("No tiene Corte Fraccionario Registrado", "Validar Corte Fraccionario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return cRpta;
                }
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Validar Corte Fraccionario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return cRpta;
            }
            return cRpta;
        }

        private string ValidarCierreOpe()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            string msge = "";
            int nAgeRes = Convert.ToInt32(this.cboAgencias.SelectedValue.ToString());
            int nColRes = Convert.ToInt32(this.cboColaborador.SelectedValue.ToString());
            clsCNControlOpe ValCorteFra = new clsCNControlOpe();
            string cRpta = ValCorteFra.ValAutCorteFracc(objUsuario.dFecSystem, nColRes, nAgeRes, 2, ref msge);
            if (msge == "OK")
            {
                switch (cRpta)
                {
                    case "0":
                        script.Mensaje("No ha Registrado su Caja");
                        //MessageBox.Show("No ha Registrado su Caja", "Validar Caja por Operador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "ERROR";
                    case "A":
                        script.Mensaje("Su Caja no se Encuentra Cerrado");
                        //MessageBox.Show("Su Caja no se Encuentra Cerrado", "Validar Caja por Operador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "ERROR";
                    default:
                        return "OK";
                }
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Validar Caja por Operador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "ERROR";
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

        protected void btnAceptar_Click1(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            if (string.IsNullOrEmpty(this.cboAgencias.SelectedValue.ToString()))
            {
                script.Mensaje("Debe Seleccionar la Agencia");
                //MessageBox.Show("Debe Seleccionar la Agencia", "Validar Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboAgencias.Focus();
                //this.cboAgencias.Select();
                return;
            }

            if (this.cboAgencias.SelectedValue.ToString() == "0")
            {
                script.Mensaje("Debe Seleccionar la Agencia");
                //MessageBox.Show("Debe Seleccionar la Agencia", "Validar Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboAgencias.Focus();
                //this.cboAgencias.Select();
                return;
            }

            if (string.IsNullOrEmpty(this.cboColaborador.SelectedValue.ToString()))
            {
                script.Mensaje("Debe Seleccionar un Colaborador");
                //MessageBox.Show("Debe Seleccionar un Colaborador", "Validar Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboColaborador.Focus();
                //this.cboColaborador.Select();
                return;
            }

            if (string.IsNullOrEmpty(this.txtSustento.Text.Trim()))
            {
                script.Mensaje("Debe Registrar el Sustento Respectivo");
                //MessageBox.Show("Debe Registrar el Sustento Respectivo", "Validar Apertura de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtSustento.Focus();
                //this.txtSustento.Select();
                return;
            }

            //=========================================================
            //--Validar que el mismo usuario no pueda habilitarse
            //=========================================================
            if (objUsuario.idUsuario.ToString() == this.cboColaborador.SelectedValue.ToString())
            {
                script.Mensaje("El Mismo Usuario no Puede Habilitarse");
                //MessageBox.Show("El Mismo Usuario no Puede Habilitarse", "Validar Habilitación de Caja y Corte Fraccionario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboColaborador.Focus();
                //this.cboColaborador.Select();
                return;
            }

            //=========================================================
            //--Asignar datos para Registrar
            //=========================================================
            int nAgeRes = Convert.ToInt32(this.cboAgencias.SelectedValue.ToString());
            int nColRes = Convert.ToInt32(this.cboColaborador.SelectedValue.ToString());
            string cSust = this.txtSustento.Text.Trim();
            //=========================================================
            //--Registrar Habilitación de caja cerrada
            //=========================================================
            if (this.chcApeCaja.Checked)
            {
                if (ValidarCierreOpe() == "ERROR")
                {
                    return;
                }

                clsCNControlOpe RegApeCajCerra = new clsCNControlOpe();
                string cRpta = RegApeCajCerra.RegApeCajaCerrada(objUsuario.dFecSystem, objUsuario.idUsuario, objUsuario.nIdAgencia,
                                                                nColRes, nAgeRes, cSust, 1);
                if (cRpta == "OK")
                {
                    script.Mensaje("La Apertura de la Caja, se Realizó Correctamente...");
                    //MessageBox.Show("La Apertura de la Caja, se Realizó Correctamente...", "Aperturar Caja Cerrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    script.Mensaje(cRpta);
                    //MessageBox.Show(cRpta, "Aperturar Caja Cerrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //=========================================================
            //--Registrar Habilitación de Corte Fraccionario
            //=========================================================
            if (this.chcCorteFracc.Checked)
            {
                if (ValidarCorte() == "0")
                {
                    return;
                }


                clsCNControlOpe RegHabCor = new clsCNControlOpe();
                string cRpta = RegHabCor.RegApeCajaCerrada(objUsuario.dFecSystem, objUsuario.idUsuario, objUsuario.nIdAgencia,
                                                                nColRes, nAgeRes, cSust, 2);
                if (cRpta == "OK")
                {
                    script.Mensaje("La Habilitación del Corte Fraccionario, se Realizó Correctamente...");
                    //MessageBox.Show("La Habilitación del Corte Fraccionario, se Realizó Correctamente...", "Aperturar Caja Cerrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    script.Mensaje(cRpta);
                    //MessageBox.Show(cRpta, "Habilitar Corte Fraccionario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.btnAceptar.Enabled = false;

        }


        protected void cboAgencias_SelectedIndexChanged1(object sender, EventArgs e)
        {
            int nItem;
            nItem = Convert.ToInt32(this.cboAgencias.SelectedValue.ToString());
            ListarColAgencia(nItem);
        }

        protected void chcApeCaja_CheckedChanged1(object sender, EventArgs e)
        {
            if (this.chcApeCaja.Checked)
            {
                this.btnAceptar.Enabled = true;
                this.chcCorteFracc.Checked = false;
            }
            else
            {
                this.btnAceptar.Enabled = false;
                this.chcCorteFracc.Checked = false;
            }

        }

        protected void chcCorteFracc_CheckedChanged1(object sender, EventArgs e)
        {
            if (this.chcCorteFracc.Checked)
            {
                this.btnAceptar.Enabled = true;
                this.chcApeCaja.Checked = false;
            }
           

        }
        
    }
}