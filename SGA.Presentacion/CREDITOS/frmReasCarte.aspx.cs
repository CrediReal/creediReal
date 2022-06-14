using SGA.LogicaNegocio;
using SGA.Utilitarios;
using SGA.ENTIDADES;
using CRE.CapaNegocio;
using GEN.CapaNegocio;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmReasCarte : System.Web.UI.Page
    {

        int nIdAseOri = 0;
        int nIdAseDes = 0;
        CRE.CapaNegocio.clsCNCredito Credito = new CRE.CapaNegocio.clsCNCredito();
        DataSet ds = new DataSet();
        DataTable dtUpd = new DataTable();
        DataTable dtCreDes = new DataTable();
        clsWebJScript script = new clsWebJScript();

        string xml;

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
            cboOrigen();
            cboDestino();
            this.cboAseOri.SelectedIndexChanged += new EventHandler(cboAseOri_SelectedIndexChanged1);
            this.cboAseDes.SelectedIndexChanged += new EventHandler(cboAseDes_SelectedIndexChanged1);
            
        }

        protected void dtgCreOri_pageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dtUpd=(DataTable)ViewState["dtUpd"];

            dtgCreOri.DataSource = dtUpd;
            dtgCreOri.PageIndex = e.NewPageIndex;
            dtgCreOri.DataBind();
           
        }

        protected void dtgCreDes_pageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dtCreDes=(DataTable)ViewState["dtCreDes"];
            dtgCreDes.DataSource = dtCreDes;
            dtgCreDes.PageIndex = e.NewPageIndex;
            dtgCreDes.DataBind();

        }

        protected void cboAseOri_SelectedIndexChanged1(object sender, EventArgs e)
        {
            LoadCreOri();
        }

        protected void cboAseDes_SelectedIndexChanged1(object sender, EventArgs e)
        {
            LoadCreDes();
        }

        private void cboOrigen()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }

            int idAgencia = objUsuario.nIdAgencia;
            GEN.CapaNegocio.clsCNPersonalCreditos ListaPersonalCre = new GEN.CapaNegocio.clsCNPersonalCreditos();
            DataTable dt = ListaPersonalCre.ListarPersonalCre(0, 0, 0);
            this.cboAseOri.DataSource = dt;
            this.cboAseOri.DataValueField = dt.Columns[0].ToString();
            this.cboAseOri.DataTextField = dt.Columns[1].ToString();
            this.cboAseOri.DataBind();
            cboAseOri.SelectedValue = "-1";
        }

        private void cboDestino()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }

            int idAgencia = objUsuario.nIdAgencia;
            GEN.CapaNegocio.clsCNPersonalCreditos ListaPersonalCre = new GEN.CapaNegocio.clsCNPersonalCreditos();
            DataTable dt = ListaPersonalCre.ListarPersonalCre(0, 0, 0);
            this.cboAseDes.DataSource = dt;
            this.cboAseDes.DataValueField = dt.Columns[0].ToString();
            this.cboAseDes.DataTextField = dt.Columns[1].ToString();
            this.cboAseDes.DataBind();
            cboAseDes.SelectedValue = "-1";
        }
        
        private void LoadCreOri()
        {
            try
            {
                if (cboAseOri.SelectedItem != null)
                {
                    dtUpd = new DataTable();
                    dtUpd = Credito.LisCreByAna(Convert.ToInt32(cboAseOri.SelectedValue));
                    dtUpd.Columns["lSeleCta"].ReadOnly = false;
                    ViewState["dtUpd"] = dtUpd;

                    dtgCreOri.DataSource = dtUpd;
                    dtgCreOri.DataBind();
                    lblBase1.Text = dtUpd.Rows.Count.ToString() + " Créditos.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LoadCreDes()
        {
            try
            {
                if (cboAseDes.SelectedItem != null)
                {
                    dtCreDes = Credito.LisCreByAna(Convert.ToInt32(cboAseDes.SelectedValue));
                    dtCreDes.Columns["lSeleCta"].ReadOnly = false;
                    ViewState["dtCreDes"] = dtCreDes;
                    dtgCreDes.DataSource = dtCreDes;
                    dtgCreDes.DataBind();
                    lblBase2.Text = dtCreDes.Rows.Count.ToString() + " Créditos.";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

       protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckBox = e.Row.Cells[0].Controls[0] as CheckBox;
                    CheckBox.Enabled = true;

                }

        }

       protected void btnGrabar1_Click(object sender, EventArgs e)
       {
           if (Validacion())
           {
               dtUpd = (DataTable)ViewState["dtUpd"];
               dtUpd.Columns.Remove("cNombre");
               ds.Tables.Add(dtUpd);
               xml = ds.GetXml();
               Credito.UpdCreByAse(xml, Convert.ToInt32(cboAseDes.SelectedValue));
               LoadCreOri();
               LoadCreDes();
               script.Mensaje("Reasignación de Cartera exitosa.");
               ds.Tables.Remove("Table1");
           }
       }

       private bool Validacion()
       {
           bool res = true;
           if (dtgCreOri.Rows.Count <= 0)
           {
               script.Mensaje("El Asesor no tiene créditos asignados");
               //MessageBox.Show("El Asesor no tiene créditos asignados", "Reasignación de Cartera", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               res = false;
               return res;
           }
           if (this.cboAseOri.SelectedValue == this.cboAseDes.SelectedValue)
           {
               script.Mensaje("El Asesor de Orígen y de Destino no pueden ser los mismos.");
               //MessageBox.Show("El Asesor de Orígen y de Destino no pueden ser los mismos.", "Reasignación de Cartera", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               res = false;
               return res;
           }
           bool n = false;
           dtUpd = (DataTable)ViewState["dtUpd"];
           foreach (DataRow item in dtUpd.Rows)
           {
               if ((bool)item["lSeleCta"])
               {
                   n = true;
                   break;
               }
           }
           if (!n)
           {
               script.Mensaje("Debe seleccionar algún crédito.");
               //MessageBox.Show("Debe seleccionar algún credito.", "Reasignación de Cartera", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               res = false;
               return res;
           }
           return res;
       }

       protected void chkSel_CheckedChanged(object sender, EventArgs e)
       {
           var chk = (CheckBox)sender;
           GridViewRow gridRow = (sender as CheckBox).NamingContainer as GridViewRow;
           var idCuenta=gridRow.Cells[2].Text;

           var dtUpd = (DataTable)ViewState["dtUpd"];
           foreach (DataRow item in dtUpd.Rows)
           {
               if (item["idCuenta"].ToString() == idCuenta)
               {
                   item["lSeleCta"] = true;
               }
           }

           ViewState["dtUpd"] = dtUpd;
       }

    }
}