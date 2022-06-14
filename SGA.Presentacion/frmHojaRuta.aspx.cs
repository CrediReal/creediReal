using SGA.Controles;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion
{
    public partial class frmHojaRuta : System.Web.UI.Page
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

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

                cboDepartamento.CargarDepartamentos();

                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
                BuscarHojaRutas(usuarioSession.idUsuario);
                pnlBotones.Visible = true;
                pnlResultados.Visible = true;
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

            pnlBotones.Visible = false;
            pnlResultados.Visible = true;
            pnlForm.Visible = true;

            List<clsDetHojaRuta> DetHojaRuta = new clsCNHojaRuta().GetDetalleHojaRuta(ObjHojaRuta.idHojaRuta);
            ObjHojaRuta.DetalleHojaRuta = DetHojaRuta;
            FillControls(ObjHojaRuta);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjHojaRuta = new clsHojaRuta()
            {
                idHojaRuta = 0,
                idOficina = 1,
                idAsesor = 0,
                dFecIni = DateTime.Now.Date,
                dFecFin = DateTime.Now.Date,
                DetalleHojaRuta = new List<clsDetHojaRuta>()
            };

            FillControls(ObjHojaRuta);
            HabilitarControles(true);

            pnlBotones.Visible = false;
            pnlResultados.Visible = true;
            pnlForm.Visible = true;
        }

        protected void BotonGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
                if (CamposSonValidos() == false) return;

                ObjHojaRuta.idAsesor = usuarioSession.idUsuario;
                ObjHojaRuta.dFecIni = Convert.ToDateTime(txtFecIni.Text);
                ObjHojaRuta.dFecFin = Convert.ToDateTime(txtFecFin.Text);

                int idUsuReg = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNHojaRuta().SaveHojaRutas(ObjHojaRuta, idUsuReg);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlResultados.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    BuscarHojaRutas(usuarioSession.idUsuario);

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
            pnlResultados.Visible = true;
            pnlForm.Visible = false;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            List<int> LstIdClientes = new List<int>();
            List<Cliente> LstClientes = new List<Cliente>();
            foreach (GridViewRow row in grvClientes.Rows)
            {
                CheckBoxBase chcSeleccion = row.FindControl("chcSeleccion") as CheckBoxBase;
                LabelBase lblIdCliBus = row.FindControl("lblIdCliBus") as LabelBase;
                LabelBase lblNombres = row.FindControl("lblNombres") as LabelBase;
                LabelBase lblDocumento = row.FindControl("lblDocumento") as LabelBase;
                LabelBase lblDireccion = row.FindControl("lblDireccion") as LabelBase;
                if (chcSeleccion != null && lblIdCliBus != null)
                {
                    if (chcSeleccion.Checked)
                    {
                        LstIdClientes.Add(Convert.ToInt32(lblIdCliBus.Text));
                        LstClientes.Add(new Cliente()
                        {
                            idCliente = Convert.ToInt32(lblIdCliBus.Text),
                            cNombres = lblNombres.Text.Trim(),
                            cDocumento = lblDocumento.Text.Trim(),
                            cDireccion = lblDireccion.Text.Trim()
                        });
                    }
                }
            }

            if (!LstIdClientes.Any())
            {
                Script.Mensaje("Seleccione al menos un item para agregar al listado de visitas.");
                return;
            }

            if (ObjHojaRuta.DetalleHojaRuta.Select(x => x.idCli).Intersect(LstIdClientes).Any())
            {
                Script.Mensaje("Se seleccionó clientes repetidos.");
                return;
            }

            foreach (Cliente cliente in LstClientes)
            {
                ObjHojaRuta.DetalleHojaRuta.Add(
                    new clsDetHojaRuta()
                    {
                        idCli = cliente.idCliente,
                        cNombres = cliente.cNombres,
                        cDocumento = cliente.cDocumento,
                        cDireccion = cliente.cDireccion,
                        idInterno = ObjHojaRuta.DetalleHojaRuta.Any() ? ObjHojaRuta.DetalleHojaRuta.Max(x => x.idInterno) + 1 : 1
                    });
            }

            string cCodDep = cboDepartamento.SelectedValue.ToString();
            string cCodProv = string.IsNullOrEmpty(cboProvincia.SelectedValue.ToString()) ? "%" :
                                                    cboProvincia.SelectedValue.ToString();
            string cCodDis = string.IsNullOrEmpty(cboDistrito.SelectedValue.ToString()) ? "%" :
                                                    cboDistrito.SelectedValue.ToString();

            DataTable dtClientes = new clsCNCliente().GetClientesUbigeo(string.Format("{0}{1}{2}", cCodDep, cCodProv, cCodDis));
            grvClientes.DataSource = dtClientes;
            grvClientes.DataBind();
            grvDetalleHojaRuta.DataSource = ObjHojaRuta.DetalleHojaRuta;
            grvDetalleHojaRuta.DataBind();
        }

        protected void btnQuitarDetalle_OnClick(object sender, EventArgs e)
        {
            int idInterno = Convert.ToInt32(((Button)sender).CommandArgument);

            var objDetHojaRuta = ObjHojaRuta.DetalleHojaRuta.FirstOrDefault(x => x.idInterno == idInterno);
            if (objDetHojaRuta == null)
            {
                return;
            }

            ObjHojaRuta.DetalleHojaRuta.Remove(objDetHojaRuta);
            grvDetalleHojaRuta.DataSource = ObjHojaRuta.DetalleHojaRuta;
            DataBind();
        }

        protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboDepartamento.SelectedValue.ToString()))
            {
                cboProvincia.ListarProvincias(cboDepartamento.SelectedValue.ToString());
            }
        }

        protected void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboProvincia.SelectedValue.ToString()))
            {
                cboDistrito.ListarDistritos(cboDepartamento.SelectedValue.ToString(),
                                                cboProvincia.SelectedValue.ToString());
            }
        }

        protected void btnBuscarCli_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDepartamento.SelectedValue.ToString()))
            {
                Script.Mensaje("Seleccione el departamento.");
                return;
            }

            string cCodDep = cboDepartamento.SelectedValue.ToString();
            string cCodProv = string.IsNullOrEmpty(cboProvincia.SelectedValue.ToString()) ? "%" :
                                                    cboProvincia.SelectedValue.ToString();
            string cCodDis = string.IsNullOrEmpty(cboDistrito.SelectedValue.ToString()) ? "%" :
                                                    cboDistrito.SelectedValue.ToString();

            DataTable dtClientes = new clsCNCliente().GetClientesUbigeo(string.Format("{0}{1}{2}", cCodDep, cCodProv, cCodDis));
            grvClientes.DataSource = dtClientes;
            grvClientes.DataBind();
        }

        #endregion

        #region Metodos

        private void LimpiarControles()
        {
            txtCodigo.Text = string.Empty;
            grvDetalleHojaRuta.DataSource = null;
        }

        private bool CamposSonValidos()
        {
            DateTime dFecIni;
            DateTime dFecFin;
            if (!DateTime.TryParse(txtFecIni.Text.Trim(), out dFecIni))
            {
                Script.Mensaje("Fecha de inicio no válida.");
                return false;
            }
            if (!DateTime.TryParse(txtFecFin.Text.Trim(), out dFecFin))
            {
                Script.Mensaje("Fecha final no válida.");
                return false;
            }
            if (grvDetalleHojaRuta.Rows.Count == 0)
            {
                Script.Mensaje("Ingrese el detalle de la HojaRuta");
                return false;
            }

            return true;
        }

        private void FillControls(clsHojaRuta objHojaRuta)
        {
            txtCodigo.Text = objHojaRuta.idHojaRuta == 0 ? string.Empty : objHojaRuta.idHojaRuta.ToString();
            txtFecIni.Text = objHojaRuta.dFecIni.ToString("dd-MM-yyyy");
            txtFecFin.Text = objHojaRuta.dFecFin.ToString("dd-MM-yyyy");

            grvDetalleHojaRuta.DataSource = objHojaRuta.DetalleHojaRuta;
            grvDetalleHojaRuta.DataBind();
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            txtFecIni.Enabled = lHabil;
            txtFecFin.Enabled = lHabil;
            grvDetalleHojaRuta.Enabled = lHabil;
        }

        private void BuscarHojaRutas(int idAsesor)
        {
            LstHojaRutas = new clsCNHojaRuta().GetHojaRutas(idAsesor);
            grvHojaRutas.DataSource = LstHojaRutas;
            grvHojaRutas.DataBind();

            pnlResultados.Visible = true;
        }

        #endregion

   }

    struct Cliente
    {
        public int idCliente;
        public string cNombres;
        public string cDocumento;
        public string cDireccion;
    }
}