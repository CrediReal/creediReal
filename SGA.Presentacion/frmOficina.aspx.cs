using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.Controles;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion
{
    public partial class frmOficina : System.Web.UI.Page
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsOficina ObjOficina
        {
            get
            {
                clsOficina objOficina = ViewState["objOficina"] as clsOficina;
                return objOficina ?? new clsOficina();
            }
            set
            {
                ViewState["objOficina"] = value;
            }
        }

        public List<clsOficina> LstOficinas
        {
            get
            {
                List<clsOficina> lstOficinas = ViewState["lstOficinas"] as List<clsOficina>;
                return lstOficinas ?? new List<clsOficina>();
            }
            set
            {
                ViewState["lstOficinas"] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }

                if (IsPostBack) return;
                
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                pnlBotones.Visible = true;
                pnlBusqueda.Visible = true;
                pnlForm.Visible = false;

                //Buscar al usuario por nombre
                LstOficinas = new clsCNOficina().ListarOficinas(0);

                if (!LstOficinas.Any())
                {
                    Script.Mensaje("No se encontraron resultados.");
                }

                grvOficinas.DataSource = LstOficinas;
                grvOficinas.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idOficina = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjOficina = LstOficinas.FirstOrDefault(x => x.idOficina == idOficina);
            if (ObjOficina == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;

            FillControls(ObjOficina);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjOficina = new clsOficina();
            FillControls(ObjOficina);
            HabilitarControles(true);

            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;
            
        }

        protected void BotonGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------Valida Session Vigente -------------------------------------->
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                //------------------------------------------------------------------------------>

                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
                if (CamposSonValidos() == false) return;


                ObjOficina.cNomOficina = txtNomOficina.Text.Trim().ToUpper();
                ObjOficina.cDireccion = txtDireccion.Text.Trim().ToUpper();
                ObjOficina.cTelef = txtTelefono.Text.Trim().ToUpper();
                ObjOficina.lVigente = chcVigente.Checked;
                ObjOficina.idUsuario = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNOficina().SaveOficina(ObjOficina);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlBusqueda.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    LstOficinas = new clsCNOficina().ListarOficinas(0);
                    grvOficinas.DataSource = LstOficinas;
                    grvOficinas.DataBind();   
    
                    Script.Mensaje(objResp.cMsje);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            pnlBotones.Visible = true;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = false;
        }

        #endregion

        #region Metodos

        private void LimpiarControles()
        {
            txtCodigo.Text = string.Empty;
            txtNomOficina.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            chcVigente.Checked = false;
        }

        private bool CamposSonValidos()
        {
            if (string.IsNullOrEmpty(txtNomOficina.Text.Trim()))
            {
                Script.Mensaje("Ingrese la descripción de la oficina.");
                return false;
            }
            if (string.IsNullOrEmpty(txtDireccion.Text.Trim()))
            {
                Script.Mensaje("Ingrese la dirección de la oficina.");
                return false;
            }
            if (string.IsNullOrEmpty(txtTelefono.Text.Trim()))
            {
                Script.Mensaje("Ingrese el teléfono de la oficina.");
                return false;
            }
            return true;
        }

        private void FillControls(clsOficina objOficina)
        {
            txtCodigo.Text = objOficina.idOficina == 0 ? string.Empty : objOficina.idOficina.ToString();
            txtNomOficina.Text = objOficina.cNomOficina;
            txtDireccion.Text = objOficina.cDireccion;
            txtTelefono.Text = objOficina.cTelef;
            chcVigente.Checked = objOficina.lVigente;
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            txtNomOficina.Enabled = lHabil;
            txtDireccion.Enabled = lHabil;
            txtTelefono.Enabled = lHabil;
            chcVigente.Enabled = lHabil;
        }

        #endregion
    }
}