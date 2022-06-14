using SGA.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SGA.Presentacion
{
    public partial class changeinfo : System.Web.UI.Page
    {
        clsCNCredito cncredito = new clsCNCredito();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        protected void btnactualizar_Click(object sender, EventArgs e)
        {
            if (ViewState["idSolicitud"] != null)
            {
                var dtSolicitudes = (DataTable)ViewState["dtSolicitudes"];
                int idSolicitud = Convert.ToInt32(ViewState["idSolicitud"]);
                var dr = dtSolicitudes.AsEnumerable().Where(x => (int)x["idSolicitud"] == idSolicitud).ToList()[0];
                decimal nCapitalSolicitado = Convert.ToDecimal(dr["nCapitalSolicitado"]);
                int nPlazoCuota = Convert.ToInt32(dr["nPlazoCuota"]);
                int nCuotas = Convert.ToInt32(dr["nCuotas"]);
                decimal nTasaCompensatoria = Convert.ToDecimal(dr["nTasaCompensatoria"]);
                int idEstado = Convert.ToInt32(dr["idEstado"]);
                cncredito.ActualizarSolicitud(idSolicitud, nCapitalSolicitado, nPlazoCuota, nCuotas, nTasaCompensatoria, idEstado);
                Response.Write("Actualización correcta");
            }

        }

        protected void dtgSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dtgSolicitud.Rows.Count > 0)
            {
                var idSolicitud = Convert.ToInt32(dtgSolicitud.SelectedDataKey.Values[0]);
                ViewState["idSolicitud"] = idSolicitud;
                var dtSolicitudes = (DataTable)ViewState["dtSolicitudes"];
                var dr = dtSolicitudes.AsEnumerable().Where(x => (int)x["idSolicitud"] == idSolicitud).ToList()[0];
                txtCuotas.Value = dr["nCuotas"].ToString();
                txtMonto.Value = dr["nCapitalSolicitado"].ToString();
                txtPlazo.Value = dr["nPlazoCuota"].ToString();
                txtTasa.Value = dr["nTasaCompensatoria"].ToString();
                txtEstado.Value = dr["idEstado"].ToString();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            var dtSolicitudes = cncredito.BuscarSolicitud(txtNombre.Value.Trim());
            if (dtSolicitudes.Rows.Count > 0)
            {
                ViewState["dtSolicitudes"] = dtSolicitudes;
                dtgSolicitud.DataSource = dtSolicitudes;
                dtgSolicitud.DataBind();
            }
        }
    }
}