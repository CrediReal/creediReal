using Microsoft.Reporting.WebForms;
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
    public partial class frmReporteVentasData : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNVenta cnventa = new clsCNVenta();
        clsCNOficina cnoficina = new clsCNOficina();
        clsCNAniosMeses cnmeses = new clsCNAniosMeses();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            //----------------- TITULO ------------------------>
            lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
            cargarTrabajadores();
            cargarOficinas();
            cargarMeses();
            cargarCategorias();
        }

        private void cargarTrabajadores()
        {
            var dtTrabajador = cnventa.ListaTrabajadoresData();
            chblTrabajadores.DataSource = dtTrabajador;
            chblTrabajadores.DataTextField = "cColaborador";
            chblTrabajadores.DataValueField = "idColaborador";
            chblTrabajadores.DataBind();

            foreach (ListItem li in chblTrabajadores.Items)
            {
                li.Selected = true;
            }
        }

        private void cargarMeses()
        {
            chblMes.DataSource = cnmeses.GetMeses();
            chblMes.DataTextField = "cMes";
            chblMes.DataValueField = "nMes";
            chblMes.DataBind();

            foreach (ListItem li in this.chblMes.Items)
            {
                li.Selected = true;
            }
        }

        private void cargarOficinas()
        {
            this.chblOficinas.DataSource = cnoficina.ListarOficinas(0, true);
            chblOficinas.DataTextField = "cNomOficina";
            chblOficinas.DataValueField = "idOficina";
            chblOficinas.DataBind();
            foreach (ListItem li in chblOficinas.Items)
            {
                li.Selected = true;
            }
        }

        private void cargarCategorias()
        {
            DataTable dtCategoria = new DataTable();
            dtCategoria.Columns.Add("idCategoria", typeof(int));
            dtCategoria.Columns.Add("cCategoria", typeof(string));
            DataRow dr ;
            dr = dtCategoria.NewRow();
            dr["idCategoria"] = 1;
            dr["cCategoria"] ="VOLVO   .";
            dtCategoria.Rows.Add(dr);

            dr = dtCategoria.NewRow();
            dr["idCategoria"] = 2;
            dr["cCategoria"] = "MAK   .";
            dtCategoria.Rows.Add(dr);

            dr = dtCategoria.NewRow();
            dr["idCategoria"] = 3;
            dr["cCategoria"] = "SIV   .";
            dtCategoria.Rows.Add(dr);

            dr = dtCategoria.NewRow();
            dr["idCategoria"] = 4;
            dr["cCategoria"] = "BATERIA   .";
            dtCategoria.Rows.Add(dr);

            dr = dtCategoria.NewRow();
            dr["idCategoria"] = 5;
            dr["cCategoria"] = "LUBRICANTE   .";
            dtCategoria.Rows.Add(dr);
            dtCategoria.AcceptChanges();

            this.chblCategorias.DataSource = dtCategoria;
            chblCategorias.DataTextField = "cCategoria";
            chblCategorias.DataValueField = "idCategoria";
            chblCategorias.DataBind();
            foreach (ListItem li in this.chblCategorias.Items)
            {
                li.Selected = true;
            }
        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            try
            {
                List<ReportParameter> ListaParametros = new List<ReportParameter>();
                List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

                var cXmlAsesor = xmlColaboradores();
                var cXmlOficina = xmlOficina();
                var nAnio = Convert.ToInt32(cboAnios.SelectedValue);
                ListaDataSource.Add(new ReportDataSource("dsVentaAsesor", cnventa.RptVentasAsesor(cXmlAsesor, cXmlOficina, nAnio)));

                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;

                var cReporte = "RptVentasXAsesor.rdlc";

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void BotonImprimir2_Click(object sender, EventArgs e)
        {
            try
            {
                List<ReportParameter> ListaParametros = new List<ReportParameter>();
                List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();
                var cxmlmeses = xmlMeses();
                var cxmlcategoria = xmlCategoria();
                var nAnio = Convert.ToInt32(cboAnios.SelectedValue);
                ListaDataSource.Add(new ReportDataSource("dsVentaSucursal", cnventa.RptVentasSucursal(cxmlmeses,cxmlcategoria, nAnio)));

                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;

                var cReporte = "RptVentaSucursales.rdlc";

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void BotonImprimir3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validar()) return;

                List<ReportParameter> ListaParametros = new List<ReportParameter>();
                List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

                int idCli = ConBusCli.IdCli;
                ListaDataSource.Add(new ReportDataSource("dsVentaCliente", cnventa.RptVentasPorcliente(idCli)));

                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;

                var cReporte = "RptVentaXCliente.rdlc";

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string xmlColaboradores()
        {
            DataSet dsAsesor = new DataSet("dsAsesor");
            DataTable dtAsesor = new DataTable("dtAsesor");

            dtAsesor.Columns.Add("cAsesor", typeof(string));

            foreach (ListItem li in chblTrabajadores.Items)
            {
                if (li.Selected == true)
                {
                    var dr = dtAsesor.NewRow();
                    dr["cAsesor"] = li.Value;
                    dtAsesor.Rows.Add(dr);
                }
            }
            dtAsesor.AcceptChanges();

            dsAsesor.Tables.Add(dtAsesor);

            return dsAsesor.GetXml();
        }

        private string xmlOficina()
        {
            DataSet dsOficina = new DataSet("dsOficina");
            DataTable dtOficina = new DataTable("dtOficina");

            dtOficina.Columns.Add("idOficina", typeof(int));

            foreach (ListItem li in this.chblOficinas.Items)
            {
                if (li.Selected == true)
                {
                    var dr = dtOficina.NewRow();
                    dr["idOficina"] = li.Value;
                    dtOficina.Rows.Add(dr);
                }
            }
            dtOficina.AcceptChanges();

            dsOficina.Tables.Add(dtOficina);

            return dsOficina.GetXml();
        }

        private string xmlMeses()
        {
            DataSet dsMes = new DataSet("dsMes");
            DataTable dtMes = new DataTable("dtMes");

            dtMes.Columns.Add("nMes", typeof(int));

            foreach (ListItem li in this.chblMes.Items)
            {
                if (li.Selected == true)
                {
                    var dr = dtMes.NewRow();
                    dr["nMes"] = li.Value;
                    dtMes.Rows.Add(dr);
                }
            }
            dtMes.AcceptChanges();

            dsMes.Tables.Add(dtMes);

            return dsMes.GetXml();
        }

        private string xmlCategoria()
        {
            DataSet dsCategoria = new DataSet("dsCategoria");
            DataTable dtCategoria = new DataTable("dtCategoria");

            dtCategoria.Columns.Add("idCategoria", typeof(int));
            dtCategoria.Columns.Add("lVisible", typeof(bool));

            foreach (ListItem li in this.chblCategorias.Items)
            {
                var dr = dtCategoria.NewRow();
                dr["idCategoria"] = li.Value;
                dr["lVisible"] = li.Selected;
                dtCategoria.Rows.Add(dr);
            }
            dtCategoria.AcceptChanges();

            dsCategoria.Tables.Add(dtCategoria);

            return dsCategoria.GetXml();
        }

        protected void chbTodoCol_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem li in chblTrabajadores.Items)
            {
                li.Selected = chbTodoCol.Checked;
            }
        }

        protected void chbTodoOfi_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem li in this.chblOficinas.Items)
            {
                li.Selected = chbTodoOfi.Checked;
            }
        }

        protected void chbMesTodos_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem li in this.chblMes.Items)
            {
                li.Selected = chbMesTodos.Checked;
            }
        }

        protected void chbTodosCategoria_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem li in this.chblCategorias.Items)
            {
                li.Selected = chbTodosCategoria.Checked;
            }
        }

        private bool validar()
        {
            if (ConBusCli.IdCli == 0)
            {
                script.Mensaje("Seleccione el cliente para la busqueda.");
                return false;
            }

            return true;
        }
    }
}