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
    public partial class conBuscarCliente : System.Web.UI.UserControl
    {
        #region Variables Globales

        clsCNAlmacen cnalmacen = new clsCNAlmacen();
        clsWebJScript script = new clsWebJScript();
        clsCNUsuario cnusuario = new clsCNUsuario();
        clsCNCliente cncliente = new clsCNCliente();

        public int idCliente { get; set; }

        #endregion
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;
            pnlClientes.Visible = false;
            pnlDatos.Visible = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int idtipoBuqueda=Convert.ToInt32(rblTipoBusqueda.SelectedValue);
            string cValBusqueda=txtValBusqueda.Text.Trim();

            GEN.CapaNegocio.clsCNBuscarCli listarCli = new GEN.CapaNegocio.clsCNBuscarCli();
            DataTable dtDatosCli = listarCli.ListarClientes(idtipoBuqueda.ToString(), cValBusqueda);

           // var dtDatosCli = cncliente.BuscarCliente(idtipoBuqueda, cValBusqueda);
            pnlClientes.Visible = false;
            if (dtDatosCli.Rows.Count > 0)
            {
                if (dtDatosCli.Rows.Count == 1)
                {
                    pnlDatos.Visible = true;
                    this.txtNombres.Text = dtDatosCli.Rows[0]["cNombre"].ToString().Trim();
                    this.txtDocumento.Text = dtDatosCli.Rows[0]["cDocumentoID"].ToString().Trim();
                    //this.txtDireccion.Text = dtDatosCli.Rows[0]["cDireccion"].ToString().Trim();
                    Session["idCliente"] = Convert.ToInt32(dtDatosCli.Rows[0]["idCli"]);
                }
                else
                {
                    pnlClientes.Visible = true;
                    lisclientes.DataSource = dtDatosCli;
                    lisclientes.DataTextField = "cNombre";
                    lisclientes.DataValueField = "idCli";
                    lisclientes.DataBind();
                }
            }
            else
            {
                idCliente = 0;
                script.Mensaje("No se encontraron datos para la búsqueda");
            }
        }

        protected void lisclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["idCliente"] = Convert.ToInt32(lisclientes.SelectedValue);
            GEN.CapaNegocio.clsCNBuscarCli listarCli = new GEN.CapaNegocio.clsCNBuscarCli();

            var dtDatosCli = listarCli.ListarclixIdCli(Convert.ToInt32(lisclientes.SelectedValue));
            if (dtDatosCli.Rows.Count == 1)
            {
                pnlClientes.Visible = false;
                pnlDatos.Visible = true;   
                this.txtNombres.Text = dtDatosCli.Rows[0]["cNombre"].ToString().Trim();
                this.txtDocumento.Text = dtDatosCli.Rows[0]["cDocumentoID"].ToString().Trim();
                //this.txtDireccion.Text = dtDatosCli.Rows[0]["cDireccion"].ToString().Trim();
            }
        }

        public void Ocultar(bool lEstado)
        {
            pnlBusqueda.Visible = !lEstado;
            pnlClientes.Visible = !lEstado;
            pnlDatos.Visible = !lEstado;
            LimpiarControl();
        }

        public void ActivarBusqueda(bool lEstado)
        {
            pnlBusqueda.Visible = lEstado;
            pnlClientes.Visible = !lEstado;
            pnlDatos.Visible = !lEstado;
            LimpiarControl();
        }

        public void LimpiarControl()
        {
            this.txtNombres.Text = "";
            this.txtDocumento.Text = "";
            //this.txtDireccion.Text = "";
            this.txtValBusqueda.Text = "";
            lisclientes.DataSource = null;
            lisclientes.DataBind();
        }

        protected void rblTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValBusqueda.Text = "";
            txtValBusqueda.Focus();
        }

        public void Habilitar(bool lEstado)
        {
            pnlBusqueda.Enabled = lEstado;
            pnlClientes.Enabled = lEstado;
            pnlDatos.Enabled = lEstado;
        }
    }
}