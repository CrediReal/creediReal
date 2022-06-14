using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion
{
    public partial class frmModelo : System.Web.UI.Page
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsModelo ObjModelo
        {
            get
            {
                clsModelo objModelo = ViewState["ObjModelo"] as clsModelo;
                return objModelo ?? new clsModelo();
            }
            set
            {
                ViewState["ObjModelo"] = value;
            }
        }

        public List<clsModelo> LstModelos
        {
            get
            {
                List<clsModelo> lstModelos = ViewState["lstModelos"] as List<clsModelo>;
                return lstModelos ?? new List<clsModelo>();
            }
            set
            {
                ViewState["lstModelos"] = value;
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

                cboMarcaBus.Llenar(true);
                cboMarca.Llenar();

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                pnlBotones.Visible = true;
                pnlBusqueda.Visible = true;
                pnlForm.Visible = false;

                //Buscar al usuario por nombre
                LstModelos = new clsCNModelo().GetModelos(0);

                if (!LstModelos.Any())
                {
                    Script.Mensaje("No se encontraron resultados.");
                }

                grvModelos.DataSource = LstModelos;
                grvModelos.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonConsultar_Click(object sender, EventArgs e)
        {
            if (cboMarcaBus.SelectedValue == null)
            {
                Script.Mensaje("Seleccione la marca para la busqueda.");
                return;
            }
            int idMarca = Convert.ToInt32(cboMarcaBus.SelectedValue);
            LstModelos = new clsCNModelo().GetModelos(idMarca);
            grvModelos.DataSource = LstModelos;
            grvModelos.DataBind();
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idModelo = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjModelo = LstModelos.FirstOrDefault(x => x.idModelo == idModelo);
            if (ObjModelo == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;

            FillControls(ObjModelo);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjModelo = new clsModelo();
            FillControls(ObjModelo);
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


                ObjModelo.cModelo = txtModelo.Text.Trim().ToUpper();
                ObjModelo.Marca.idMarca = Convert.ToInt32(cboMarca.SelectedValue);
                ObjModelo.lVigente = chcVigente.Checked;
                ObjModelo.idUsuario = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNModelo().SaveModelo(ObjModelo);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlBusqueda.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    LstModelos = new clsCNModelo().GetModelos(0);
                    grvModelos.DataSource = LstModelos;
                    grvModelos.DataBind();

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
            txtModelo.Text = string.Empty;
            cboMarca.SelectedIndex = -1;
            chcVigente.Checked = false;
        }

        private bool CamposSonValidos()
        {
            if (string.IsNullOrEmpty(txtModelo.Text.Trim()))
            {
                Script.Mensaje("Ingrese la descripción del modelo.");
                return false;
            }
            if (cboMarca.SelectedValue == null)
            {
                Script.Mensaje("Seleccione la marca del modelo.");
                return false;
            }
            return true;
        }

        private void FillControls(clsModelo objModelo)
        {
            txtCodigo.Text = objModelo.idModelo == 0 ? string.Empty : objModelo.idModelo.ToString();
            txtModelo.Text = objModelo.cModelo;
            if (objModelo.Marca.idMarca == 0)
            {
                cboMarca.SelectedIndex = 0;
            }
            else
            {
                cboMarca.SelectedValue = objModelo.Marca.idMarca.ToString();
            }
            chcVigente.Checked = objModelo.lVigente;
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            txtModelo.Enabled = lHabil;
            cboMarca.Enabled = lHabil;
            chcVigente.Enabled = lHabil;
        }

        #endregion

        
    }
}