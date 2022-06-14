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
using System.Text;

namespace SGA.Presentacion
{
    public partial class frmVoucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == true) return;
                if (Session["cVoucher"] == null) return;
                string cVoucher = Session["cVoucher"].ToString();
                imprimir(cVoucher);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BotonAtras1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["cForm"].ToString(), true);
        }

        protected void BotonSalir1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Close", "window.close();", true);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Session["cVoucher"] == null) return;
            string cVoucher = Session["cVoucher"].ToString();
            imprimir(cVoucher);
        }

        private void imprimir(string cVoucher)
        {
            var cFecTie = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToLongTimeString().Replace(":", "_").Replace(".", "").Replace(" ", "_");
            var cNomFile = "pdf/output" + cFecTie + ".pdf";
            var pgSize = new Rectangle(800f, 2000f);
            var pdfDoc = new Document(new Rectangle(295f, 420f), 0f, 0f, 0f, 0f);
            var fuente = FontFactory.GetFont("Calibri", 8, BaseColor.BLACK);

            var htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
            using (var memoryStream = new MemoryStream())
            {
                var writer2 = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                htmlparser.Parse(new StringReader(cVoucher));

                pdfDoc.Close();

                byte[] bytes2 = memoryStream.ToArray();
                File.WriteAllBytes(HttpContext.Current.Server.MapPath(cNomFile), bytes2);
                memoryStream.Close();
            }

            //Document document = new Document(new Rectangle(295f, 420f), 0f, 0f, 0f, 0f);
            //PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath(cNomFile));
            ////Getting a instance of new PDF writer
            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(
            //HttpContext.Current.Server.MapPath("Print.pdf"), FileMode.Create));
            //document.Open();
            //PdfContentByte cb = writer.DirectContent;

            //int i = 0;
            //int p = 0;
            //int n = reader.NumberOfPages;
            //while (i < n)
            //{
            //    document.NewPage();
            //    p++;
            //    i++;

            //    PdfImportedPage page1 = writer.GetImportedPage(reader, i);
            //    cb.AddTemplate(page1, 0, 0);
            //}

            ////Attach javascript to the document
            //PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer);
            //writer.AddJavaScript(jAction);
            //document.Close();
            ////Attach pdf to the iframe
            //frmPrint.Attributes["src"] = "Print.pdf";

            //NUEVA FORMA
            Response.Write("<script>");
            Response.Write("window.open('"+cNomFile+"','_newtab');");
            Response.Write("</script>");

        }
    }
}