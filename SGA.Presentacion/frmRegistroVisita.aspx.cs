using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion
{
    public partial class frmRegistroVisita : System.Web.UI.Page
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsDetHojaRuta ObjDetHojaRuta
        {
            get
            {
                clsDetHojaRuta objDetHojaRuta = ViewState["ObjDetHojaRuta"] as clsDetHojaRuta;
                return objDetHojaRuta ?? new clsDetHojaRuta();
            }
            set
            {
                ViewState["ObjDetHojaRuta"] = value;
            }
        }

        public clsHojaRuta ObjHojaRuta
        {
            get
            {
                clsHojaRuta objHojaRuta = ViewState["ObjHojaRuta"] as clsHojaRuta;
                return objHojaRuta ?? new clsHojaRuta();
            }
            set
            {
                ViewState["ObjHojaRuta"] = value;
            }
        }

        public List<clsHojaRuta> LstHojaRutas
        {
            get
            {
                List<clsHojaRuta> lstHojaRutas = ViewState["lstHojaRutas"] as List<clsHojaRuta>;
                return lstHojaRutas ?? new List<clsHojaRuta>();
            }
            set
            {
                ViewState["lstHojaRutas"] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

                cboTipoContacto.ListarTipoContactos();

                BuscarHojaRutas(0);
                pnlResultados.Visible = true;
                pnlBotones.Visible = false;
                pnlDetalle.Visible = false;
                pnlForm.Visible = false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idHojaRuta = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjHojaRuta = LstHojaRutas.FirstOrDefault(x => x.idHojaRuta == idHojaRuta);
            if (ObjHojaRuta == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlResultados.Visible = true;
            pnlBotones.Visible = true;
            pnlDetalle.Visible = true;
            pnlForm.Visible = false;

            List<clsDetHojaRuta> DetHojaRuta = new clsCNHojaRuta().GetDetalleHojaRuta(ObjHojaRuta.idHojaRuta);
            ObjHojaRuta.DetalleHojaRuta = DetHojaRuta;
            grvDetalleHojaRuta.DataSource = ObjHojaRuta.DetalleHojaRuta;
            grvDetalleHojaRuta.DataBind();
        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjDetHojaRuta = new clsDetHojaRuta()
            {
                idTipoVisita = 2,
                idInterno = 1
            };

            FillControls(ObjDetHojaRuta);
            HabilitarControles(true);

            pnlResultados.Visible = true;
            pnlBotones.Visible = false;
            pnlDetalle.Visible = true;
            pnlForm.Visible = true;
        }

        protected void BotonGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
                if (CamposSonValidos() == false) return;

                ObjDetHojaRuta.dFecVisita = Convert.ToDateTime(txtFecVisita.Text.Trim());
                ObjDetHojaRuta.idTipoContacto = Convert.ToInt32(cboTipoContacto.SelectedValue);
                ObjDetHojaRuta.cComentario = txtComentario.Text.Trim();
                if (chcProxVisita.Checked)
                {
                    ObjDetHojaRuta.dFecHoraProxVisita = Convert.ToDateTime(txtFecHoraProxVisita.Text.Trim());
                }

                clsDetHojaRuta obj = ObjHojaRuta.DetalleHojaRuta.FirstOrDefault(x => x.idDetHojaRuta == ObjDetHojaRuta.idDetHojaRuta);

                if (obj == null)
                {
                    ObjDetHojaRuta.idCli = ConBusCli.IdCli;
                    ObjHojaRuta.DetalleHojaRuta.Add(ObjDetHojaRuta);
                }
                else
                {
                    int index = ObjHojaRuta.DetalleHojaRuta.IndexOf(obj);
                    ObjHojaRuta.DetalleHojaRuta[index] = ObjDetHojaRuta;
                }

                int idUsuReg = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNHojaRuta().SaveHojaRutas(ObjHojaRuta, idUsuReg);
                if (objResp.nMsje == 0)
                {
                    pnlResultados.Visible = true;
                    pnlBotones.Visible = true;
                    pnlDetalle.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    BuscarHojaRutas(0);

                    List<clsDetHojaRuta> DetHojaRuta = new clsCNHojaRuta().GetDetalleHojaRuta(ObjHojaRuta.idHojaRuta);
                    ObjHojaRuta.DetalleHojaRuta = DetHojaRuta;
                    grvDetalleHojaRuta.DataSource = ObjHojaRuta.DetalleHojaRuta;
                    grvDetalleHojaRuta.DataBind();

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

            pnlResultados.Visible = true;
            pnlBotones.Visible = true;
            pnlDetalle.Visible = true;
            pnlForm.Visible = false;
        }

        protected void btnEditarDetalle_Click(object sender, EventArgs e)
        {
            int idDetHojaRuta = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjDetHojaRuta = ObjHojaRuta.DetalleHojaRuta.FirstOrDefault(x => x.idDetHojaRuta == idDetHojaRuta);
            if (ObjDetHojaRuta == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlResultados.Visible = true;
            pnlBotones.Visible = false;
            pnlDetalle.Visible = true;
            pnlForm.Visible = true;

            FillControls(ObjDetHojaRuta);
        }

        protected void chcProxVisita_CheckedChanged(object sender, EventArgs e)
        {
            txtFecHoraProxVisita.Text = string.Empty;
            txtFecHoraProxVisita.Enabled = chcProxVisita.Checked;
        }

        #endregion

        #region Metodos

        private void LimpiarControles()
        {
            txtCodigo.Text = string.Empty;
            txtFecVisita.Text = string.Empty;
            txtComentario.Text = string.Empty;
        }

        private bool CamposSonValidos()
        {
            DateTime dFecVisita;
            if(!DateTime.TryParse(txtFecVisita.Text.Trim(), out dFecVisita ))
            {
                Script.Mensaje("La fecha de visita no es válida.");
                return false;
            }
            if (string.IsNullOrEmpty(cboTipoContacto.SelectedValue.ToString()))
            {
                Script.Mensaje("Seleccione el tipo de contacto.");
                return false;
            }
            if (string.IsNullOrEmpty(txtComentario.Text.Trim()))
            {
                Script.Mensaje("Ingrese el comentario de la visita.");
                return false;
            }
            DateTime dFecHoraProxVisita;
            if (chcProxVisita.Checked && !DateTime.TryParse(txtFecHoraProxVisita.Text.Trim(), out dFecHoraProxVisita))
            {
                Script.Mensaje("La fecha y hora de proxima visita no es válida.");
                return false;
            }
            return true;
        }

        private void FillControls(clsDetHojaRuta objDetHojaRuta)
        {
            txtCodigo.Text = objDetHojaRuta.idDetHojaRuta == 0 ? string.Empty : objDetHojaRuta.idDetHojaRuta.ToString();
            ConBusCli.BuscarCliente(objDetHojaRuta.idCli);
            txtFecVisita.Text = objDetHojaRuta.dFecVisita == null ? string.Empty : objDetHojaRuta.dFecVisita.Value.ToString("dd-MM-yyyy");
            if (cboTipoContacto.Items.FindByValue(objDetHojaRuta.idTipoContacto.ToString()) != null)
                cboTipoContacto.SelectedValue = objDetHojaRuta.idTipoContacto.ToString();
            txtComentario.Text = objDetHojaRuta.cComentario;
            chcProxVisita.Checked = objDetHojaRuta.dFecHoraProxVisita != null;
            txtFecHoraProxVisita.Text = objDetHojaRuta.dFecHoraProxVisita == null ? string.Empty : objDetHojaRuta.dFecHoraProxVisita.Value.ToString("dd-MM-yyyy");
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            txtFecVisita.Enabled = lHabil;
            cboTipoContacto.Enabled = lHabil;
            txtComentario.Enabled = lHabil;
        }

        private void BuscarHojaRutas(int idUsuario)
        {
            LstHojaRutas = new clsCNHojaRuta().GetHojaRutas(idUsuario);
            grvHojaRutas.DataSource = LstHojaRutas;
            grvHojaRutas.DataBind();

            pnlResultados.Visible = true;
        }

        #endregion

    }
}