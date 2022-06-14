using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace SGA.Presentacion
{
    public partial class frmGenReporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == true) return;

                var lModal = (bool)Session["lModal"];
                BotonAtras1.Visible = !lModal;
                BotonSalir1.Visible = lModal;

                List<ReportParameter> ListaParametros = (List<ReportParameter>)Session["ListaParametros"];
                List<ReportDataSource> ListaDataSource = (List<ReportDataSource>)Session["ListaDataSource"];

                this.rptViewerLocal.ProcessingMode = ProcessingMode.Local;
                this.rptViewerLocal.LocalReport.EnableExternalImages = true;
                String cNombreReporte = Request.QueryString["cNomReporte"].ToString();
                this.rptViewerLocal.LocalReport.ReportPath = @"REPORTES/" + cNombreReporte;

                if (ListaParametros != null)
                {
                    this.rptViewerLocal.LocalReport.SetParameters(ListaParametros);
                }

                ReportDataSource dtsunico = new ReportDataSource();
                for (int i = 0; i < ListaDataSource.Count(); i++)
                {
                    dtsunico.Name = ListaDataSource[i].Name;
                    dtsunico.Value = ListaDataSource[i].Value;
                    this.rptViewerLocal.LocalReport.DataSources.Add(new ReportDataSource(dtsunico.Name, dtsunico.Value));
                }
                this.rptViewerLocal.DataBind();
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }

        protected void BotonAtras1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["cForm"].ToString(),true);
        }

        protected void BotonSalir1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Close", "window.close();", true);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = rptViewerLocal.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);

            var cFecTie = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToLongTimeString().Replace(":", "_").Replace(".", "").Replace(" ", "_");
            var cNomFile = "pdf/output" + cFecTie + ".pdf";

            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(cNomFile), FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }

            //Open existing PDF
            
            Document document = new Document(PageSize.A4);

            PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath(cNomFile));
            //Getting a instance of new PDF writer
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(
            HttpContext.Current.Server.MapPath("Print.pdf"), FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;

            int i = 0;
            int p = 0;
            int n = reader.NumberOfPages;
            Rectangle psize = reader.GetPageSize(1);

            float width = psize.Width;
            float height = psize.Height;

            if (rbnOrientacion.SelectedValue=="1")
            {
                document.SetPageSize(PageSize.A4.Rotate());
            }
            else
            {
                document.SetPageSize(PageSize.A4);
            }
            //Add Page to new document
            while (i < n)
            {
                document.NewPage();
                p++;
                i++;

                PdfImportedPage page1 = writer.GetImportedPage(reader, i);
                cb.AddTemplate(page1, 0, 0);
            }

            //Attach javascript to the document
            PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer);
            writer.AddJavaScript(jAction);
            document.Close();

            //Attach pdf to the iframe
            frmPrint.Attributes["src"] = "Print.pdf";
        }
    }
}