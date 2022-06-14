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
    public partial class ConBusCli : System.Web.UI.UserControl
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNCliente cncliente = new clsCNCliente();
        public event EventHandler ClienteChanged;

        public bool OcultarBusqueda { get; set; }

        public int IdCli
        {
            get
            {
                int? idCli = (int?)ViewState[string.Format("{0}_idCli", this.UniqueID)];
                return idCli ?? 0;
            }
            set
            {
                ViewState[string.Format("{0}_idCli",this.UniqueID)] = value;
            }
        }

        public DataTable dtClientes
        {
            get
            {
                return ViewState[string.Format("{0}_dtClientes", this.UniqueID)] as DataTable;
            }
            set
            {
                ViewState[string.Format("{0}_dtClientes", this.UniqueID)] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) 
                return;
            
            pnlBusqueda.Visible = !OcultarBusqueda;
            pnlClientes.Visible = false;
            pnlDatos.Visible = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int idtipoBuqueda = Convert.ToInt32(rblTipoBusqueda.SelectedValue);
            string cValBusqueda = txtValBusqueda.Text.Trim();

            var dtDatosCli = cncliente.BuscarCliente(idtipoBuqueda, cValBusqueda);
            if (dtDatosCli.Rows.Count > 0)
            {
                dtClientes = dtDatosCli;
                if (dtDatosCli.Rows.Count == 1)
                {
                    AsignaValores(dtClientes.Rows[0]);
                    pnlBusqueda.Visible = false;
                    pnlClientes.Visible = false;
                    pnlDatos.Visible = true;
                    Habilitar(false);
                }
                else
                {
                    grvClientes.DataSource = dtDatosCli;
                    grvClientes.DataBind();

                    pnlBusqueda.Visible = true;
                    pnlClientes.Visible = true;
                    pnlDatos.Visible = false;
                }
            }
            else
            {
                script.Mensaje("No se encontraron datos para la búsqueda");
                ActivarBusqueda(true);
            }
        }

        protected void rblTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValBusqueda.Text = "";
            txtValBusqueda.Focus();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int idCliente = Convert.ToInt32(((Button)sender).CommandArgument);

            if (dtClientes == null)
                return;

            DataRow dr = dtClientes.AsEnumerable().FirstOrDefault(x => x.Field<int>("idCliente") == idCliente);
            if (dr == null)
                return;

            AsignaValores(dr);

            pnlBusqueda.Visible = false;
            pnlClientes.Visible = false;
            pnlDatos.Visible = true;
            Habilitar(false);
        }

        public void ActivarBusqueda(bool lEstado)
        {
            Habilitar(true);
            pnlBusqueda.Visible = lEstado;
            pnlClientes.Visible = !lEstado;
            pnlDatos.Visible = !lEstado;
            LimpiarControl();
            txtValBusqueda.Focus();
        }

        public void LimpiarControl()
        {
            IdCli = 0;
            txtNombres.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtValBusqueda.Text = string.Empty;
            grvClientes.DataSource = null;
            grvClientes.DataBind();
        }

        public void Habilitar(bool lEstado)
        {
            pnlBusqueda.Enabled = lEstado;
            pnlClientes.Enabled = lEstado;
            pnlDatos.Enabled = lEstado;
        }

        private void AsignaValores(DataRow row)
        {
            IdCli = Convert.ToInt32(row["idCliente"]);
            txtNombres.Text = row["cNombres"].ToString().Trim();
            txtDocumento.Text = row["cDocumento"].ToString().Trim();
            txtDireccion.Text = row["cDireccion"].ToString().Trim();

            if (ClienteChanged != null)
                ClienteChanged(this, EventArgs.Empty);
        }

        public void BuscarCliente(int idCli)
        {
            var dtDatosCli = cncliente.BuscarCliente(1, idCli.ToString());
            if (dtDatosCli.Rows.Count > 0)
            {
                dtClientes = dtDatosCli;
                if (dtDatosCli.Rows.Count == 1)
                {
                    AsignaValores(dtDatosCli.Rows[0]);

                    pnlBusqueda.Visible = false;
                    pnlClientes.Visible = false;
                    pnlDatos.Visible = true;
                    Habilitar(false);
                }
            }
            else
            {
                ActivarBusqueda(true);
            }
        }

    }
}