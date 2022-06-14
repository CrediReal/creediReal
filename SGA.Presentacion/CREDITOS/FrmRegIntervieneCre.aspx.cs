using SGA.ENTIDADES;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.LogicaNegocio;

namespace SGA.Presentacion.CREDITOS
{
    public partial class FrmRegIntervieneCre : System.Web.UI.Page
    {
        clsWebJScript script = new clsWebJScript();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["usuario"] != null)
                {
                    Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                if (Session["DatosUsuarioSession"] == null) {
                    throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); 
                }

                if (IsPostBack) return;

                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                cargarTipoInterv();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            string cIdNumSol = this.hdSolicitud.Value;
            string cCodUsu = objUsuario.cWinuser; ;
            DateTime dFechReg = objUsuario.dFecSystem;

            DataTable dtInterviniente = (DataTable)ViewState["dtLisIntervSol"];
            DataSet dsIntervCre = new DataSet("dsIntervCred");
            dsIntervCre.Tables.Add(dtInterviniente);
            string xmlIntervCre = dsIntervCre.GetXml();
            xmlIntervCre = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(xmlIntervCre);
            dsIntervCre.Tables.Clear();
            Boolean lgarantia = chcGarantia.Checked;

            CRE.CapaNegocio.clsCNIntervCre GuardaIntervCre = new CRE.CapaNegocio.clsCNIntervCre();
            GuardaIntervCre.GuardarIntervCre(Convert.ToInt32(cIdNumSol), cCodUsu, dFechReg, xmlIntervCre, lgarantia);            
            this.BotonGrabar1.Enabled = false;
            this.BotonAgregarItem1.Enabled = false;
            this.cboTipoInterv.Enabled = false;
            this.script.Mensaje("Los Datos se Guardaron Correctamente");
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            this.conBuscarInterviniente.Visible = false;
            pnlIntervinientes.Visible = false;
            this.conBuscarCliente1.Habilitar(true);
            lblSolicitud.Text = "Nro. de Solicitud:";
            BotonGrabar1.Visible = false;
            BotonConsultar1.Enabled = true;
            hdSolicitud.Value = "";
            conBuscarCliente1.LimpiarControl();
        }

        private void cargarTipoInterv()
        {
            GEN.CapaNegocio.clsCNInterviniente TipoInterv = new GEN.CapaNegocio.clsCNInterviniente();
            DataTable dtTipoInterv = TipoInterv.CNListaTipoInterv();
            this.cboTipoInterv.DataSource = dtTipoInterv;
            this.cboTipoInterv.DataValueField="idtipointerv";
            this.cboTipoInterv.DataTextField = "CTIPOINTERVENCION";
            this.cboTipoInterv.DataBind();
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            if (idCliente == 0)
            {
                return;
            }
            GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
            DataTable dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "S", "[1]");
            if (dtDatosCuentaSolCliente.Rows.Count == 1)
            {
                this.conBuscarInterviniente.Visible = true;
                pnlIntervinientes.Visible = true;
                var nIdSolCta = Convert.ToInt32(dtDatosCuentaSolCliente.Rows[0][0]);
                this.conBuscarCliente1.Habilitar(false);
                this.dtgIntervinientes.DataSource = retonarTabla();
                this.dtgIntervinientes.DataBind();
                lblSolicitud.Text = "Nro. de Solicitud:" + nIdSolCta.ToString();
                hdSolicitud.Value = nIdSolCta.ToString();
                BotonGrabar1.Visible = true;
                BotonConsultar1.Enabled = false;
                CargaDatos();
            }
            else
            {
                this.conBuscarInterviniente.Visible = false;
                pnlIntervinientes.Visible = false;
                this.conBuscarCliente1.Habilitar(true);
                lblSolicitud.Text = "Nro. de Solicitud:";
                BotonGrabar1.Visible = false;
                hdSolicitud.Value = "";
                BotonConsultar1.Enabled = true;
                script.Mensaje("No existe solicitud de crédito aprobada para desembolso");
            }
        }

        private void CargaDatos()
        {
            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCli = Convert.ToInt32(Session["idCliente"]);
            if (idCli == 0)
            {
                return;
            }
            int nIdSol = Convert.ToInt32(this.hdSolicitud.Value);

            if (nIdSol > 0)
            {
                CRE.CapaNegocio.clsCNIntervCre IntervCre = new CRE.CapaNegocio.clsCNIntervCre();
                DataTable ListIntervCre = IntervCre.CNdtIntervCre(nIdSol);
                GEN.CapaNegocio.clsCNBuscarCli objBusCli = new GEN.CapaNegocio.clsCNBuscarCli();
                DataTable nidCli = objBusCli.DatosClientexNumSol(nIdSol);
                chcGarantia.Checked = Convert.ToBoolean(nidCli.Rows[0]["lRequiereGarantia"]);
                int nBaseNegativa = Convert.ToInt32(nidCli.Rows[0]["lBaseNegativa"]);
                int nPrendario = Convert.ToInt32(nidCli.Rows[0]["lPrenda"]);
                if (nBaseNegativa == 1 || nPrendario == 1)
                {
                    chcGarantia.Enabled = false;
                }
                else
                {
                    chcGarantia.Enabled = true;
                }

                DataTable dtInterviniente = (DataTable)ViewState["dtLisIntervSol"];
               

                if (ListIntervCre.Rows.Count == 0 && nIdSol > 0)
                {
                    dtInterviniente.Rows.Add(dtInterviniente.NewRow());
                    dtInterviniente.Rows[0]["cTipoModif"] = "I";
                    dtInterviniente.Rows[0]["idCli"] = idCli;
                    dtInterviniente.Rows[0]["cNombre"] = this.conBuscarCliente1.txtNombres.Text.ToString().Trim();
                    dtInterviniente.Rows[0]["idTipoInterv"] = 1;
                    dtInterviniente.Rows[0]["cTipoIntervencion"] = "TITULAR";

                }
                if (ListIntervCre.Rows.Count > 0)
                {
                    for (int i = 0; i < ListIntervCre.Rows.Count; i++)
                    {
                        if (ListIntervCre.Rows[i]["cTipoModif"].ToString() != "D")
                        {
                            dtInterviniente.Rows.Add(dtInterviniente.NewRow());

                            dtInterviniente.Rows[i]["cTipoModif"] = ListIntervCre.Rows[i]["cTipoModif"];
                            dtInterviniente.Rows[i]["idCli"] = ListIntervCre.Rows[i]["idCli"];
                            dtInterviniente.Rows[i]["cNombre"] = ListIntervCre.Rows[i]["cNombre"];
                            dtInterviniente.Rows[i]["idTipoInterv"] = ListIntervCre.Rows[i]["idTipoInterv"];
                            dtInterviniente.Rows[i]["cTipoIntervencion"] = ListIntervCre.Rows[i]["cTipoIntervencion"];
                        }
                    }
                }
                ViewState["dtLisIntervSol"] = dtInterviniente;
                this.dtgIntervinientes.DataSource = dtInterviniente;
                this.dtgIntervinientes.DataBind();
            }
        }

        private DataTable retonarTabla()
        {
            DataTable dtLisIntervSol = new DataTable("dtLisIntervSol");
            dtLisIntervSol.Columns.Add("cTipoModif", typeof(string));
            dtLisIntervSol.Columns.Add("idCli", typeof(Int32));
            dtLisIntervSol.Columns.Add("cNombre", typeof(string));
            dtLisIntervSol.Columns.Add("idTipoInterv", typeof(Int32));
            dtLisIntervSol.Columns.Add("cTipoIntervencion", typeof(string));
            ViewState["dtLisIntervSol"] = dtLisIntervSol;
            return dtLisIntervSol;
        }

        protected void BotonAgregarItem1_Click(object sender, EventArgs e)
        {
            if (Session["idCliInterv"] == null)
            {
                return;
            }
            var idCliInterv = Convert.ToInt32(Session["idCliInterv"]);
            if (idCliInterv == 0)
            {
                return;
            }

            if (validarDatos() == "ERROR")
            {
                return;
            }

            DataTable dtInterviniente = (DataTable)ViewState["dtLisIntervSol"];
            DataRow drCurrentRow = null;
            drCurrentRow = dtInterviniente.NewRow();
            drCurrentRow["cTipoModif"] = "I";
            drCurrentRow["idCli"] = idCliInterv;
            drCurrentRow["cNombre"] = conBuscarInterviniente.txtNombres.Text;
            drCurrentRow["idTipoInterv"] = Convert.ToInt32(this.cboTipoInterv.SelectedValue);
            drCurrentRow["cTipoIntervencion"] = cboTipoInterv.SelectedItem.Text;

            dtInterviniente.Rows.Add(drCurrentRow);
            ViewState["dtLisIntervSol"] = dtInterviniente;
            dtgIntervinientes.DataSource = dtInterviniente;
            dtgIntervinientes.DataBind();
        }

        protected void dtgIntervinientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (Session["DatosUsuarioSession"] == null)
                {
                    Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                }

                int nIndex = e.RowIndex;

                DataTable dtInterviniente = (DataTable)ViewState["dtLisIntervSol"];
                DataRow drProducto = dtInterviniente.Rows[nIndex];
                dtInterviniente.Rows.Remove(drProducto);
                dtInterviniente.AcceptChanges();

                ViewState["dtLisIntervSol"] = dtInterviniente;
                dtgIntervinientes.DataSource = dtInterviniente;
                dtgIntervinientes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string validarDatos()
        {
            Int32 nContIdIntTit = 0;
            Int32 nContIdIntCon = 0;
            DataTable dtInterviniente = (DataTable)ViewState["dtLisIntervSol"];

            if (dtInterviniente.Rows.Count >= 1)
            {
                for (int i = 0; i < dtInterviniente.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtInterviniente.Rows[i]["idTipoInterv"]) == 1)
                    {
                        nContIdIntTit++;
                    }
                    if (Convert.ToInt32(dtInterviniente.Rows[i]["idTipoInterv"]) == 2)
                    {
                        nContIdIntCon++;
                    }
                    var idCliInterv = Convert.ToInt32(Session["idCliInterv"]);

                    if (dtInterviniente.Rows[i]["idCli"].ToString().Trim() == idCliInterv.ToString())
                    {
                        this.script.Mensaje("Cliente ya se encuentra en la Lista de Intervinientes");
                        return "ERROR";
                    }
                }
                if (this.cboTipoInterv.SelectedValue.ToString() == "1" && nContIdIntTit == 1)
                {
                    script.Mensaje("Ya Existe el Titular del Crédito");
                    return "ERROR";
                }
                if (cboTipoInterv.SelectedValue.ToString() == "2" && nContIdIntCon == 1)
                {
                    script.Mensaje("Ya Existe el(la) Cónyuge como Interviniente");
                    return "ERROR";
                }
            }
            return "OK";
        }
      
    }
}