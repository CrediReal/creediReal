using SGA.ENTIDADES;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.ADMINISTRACION
{
    public partial class frmAprobacionSolicitud : System.Web.UI.Page
    {
        GEN.CapaNegocio.clsCNAprobacion objAprobacion = new GEN.CapaNegocio.clsCNAprobacion();
        clsWebJScript script = new clsWebJScript();

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
                    hPerfil.Value = Request.QueryString["perfil"].ToString();
                }
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }

                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                
                txtOpinion.Enabled = false;
                btnAprobar.Enabled = false;
                btnRechazar.Enabled = false;

                cargarLisSoliciAproba();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarLisSoliciAproba()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            var dtSoliciAproba = objAprobacion.CNLisSoliciAprobaPendiente(objUsuario.idUsuario, Convert.ToInt32(hPerfil.Value), objUsuario.dFecSystem);
            dtgLisSoliciAproba.DataSource = dtSoliciAproba;
            ViewState["dtSoliciAproba"] = dtSoliciAproba;
            dtgLisSoliciAproba.DataBind();

            txtOpinion.Text = "";
            if (dtSoliciAproba.Rows.Count > 0)
            {
                txtOpinion.Enabled = true;
                btnAprobar.Enabled = true;
                btnRechazar.Enabled = true;
            }
            else
            {
                txtOpinion.Enabled = false;
                btnAprobar.Enabled = false;
                btnRechazar.Enabled = false;
            }
        }


        protected void Onselected_Click(object sender, EventArgs e)
        {
            int idSolAproba = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            HidSolAproba.Value = idSolAproba.ToString();
            var dtSoliciAproba=(DataTable)ViewState["dtSoliciAproba"];
            var query = dtSoliciAproba.AsEnumerable().Where(x => (int)x["idSolAproba"] == idSolAproba).ToList()[0];

            HidNivelAprRanOpe.Value = query["idNivelAprRanOpe"].ToString();
            txtIdSolAproba.Text = query["idSolAproba"].ToString();
            txtNomCliente.Text = query["cNomCliente"].ToString();
            txtProducto.Text = query["cProducto"].ToString();
            txtTipoOperacion.Text = query["cTipoOperacion"].ToString();
            txtDocument.Text = query["idDocument"].ToString();
            txtMoneda.Text = query["cMoneda"].ToString();
            txtValAproba.Text = query["nValAproba"].ToString();
            txtMotivo.Text = query["cMotivo"].ToString();
            txtSustento.Text = query["cSustento"].ToString();
            txtFecSolici.Text = Convert.ToDateTime(query["dFecSolici"]).ToShortDateString();
        }

        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            if (txtOpinion.Text == "")
            {
                script.Mensaje("Debe de registrar la opinión");
                txtOpinion.Focus();
                return;
            }
            
            int idSolAproba = Convert.ToInt32(HidSolAproba.Value);
            int idNivelAprRanOpe = Convert.ToInt32(HidNivelAprRanOpe.Value);
            int idEstado = 2;
            string cOpinion = txtOpinion.Text;

            RegAprobaSolicitud(idSolAproba, idNivelAprRanOpe, objUsuario.idUsuario, idEstado, cOpinion, objUsuario.dFecSystem, Convert.ToInt32(hPerfil.Value));
            cargarLisSoliciAproba();

        }


        private void RegAprobaSolicitud(int idSolAproba, int idNivelAprRanOpe, int idUsuario, int idEstado, string cOpinion, DateTime dFecSis, int idPerfil)
        {
            DataTable dtAprobaSolicitud;
            dtAprobaSolicitud = objAprobacion.CNRegAprobaSolicitud(idSolAproba, idNivelAprRanOpe, idUsuario, idEstado, cOpinion, dFecSis, idPerfil);

            int idRpta = Convert.ToInt32(dtAprobaSolicitud.Rows[0]["idRpta"]);
            script.Mensaje(dtAprobaSolicitud.Rows[0]["cMensage"].ToString());
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            if (txtOpinion.Text == "")
            {
                script.Mensaje("Debe de registrar la opinión");
                txtOpinion.Focus();
                return;
            }

            int idSolAproba = Convert.ToInt32(HidSolAproba.Value);
            int idNivelAprRanOpe = Convert.ToInt32(HidNivelAprRanOpe.Value);
            int idEstado = 4;
            string cOpinion = txtOpinion.Text;

            RegAprobaSolicitud(idSolAproba, idNivelAprRanOpe, objUsuario.idUsuario, idEstado, cOpinion, objUsuario.dFecSystem, Convert.ToInt32(hPerfil.Value));
            cargarLisSoliciAproba();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            cargarLisSoliciAproba();
        }
    }
}