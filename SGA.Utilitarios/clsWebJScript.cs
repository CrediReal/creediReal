using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Utilitarios
{
    public class clsWebJScript
    {
        public void Mensaje(string message)
        {
            // Cleans the message to allow single quotation marks 
            string cleanMessage = message.Replace("'", "\'");
            string script = "<script type='text/javascript'>alert('" + cleanMessage + "');</script>";

            // Gets the executing web page 
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page 
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(string), "alert", script);
            }
        }

        public void SeleccionarTextBox(TextBox textBox)
        {
            if (ScriptManager.GetCurrent(textBox.Page) != null && ScriptManager.GetCurrent(textBox.Page).IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(textBox.Page,
                                           textBox.Page.GetType(),
                                           "SetFocusInUpdatePanel-" + textBox.ClientID,
                                           String.Format("ctrlToSelect='{0}';", textBox.ClientID),
                                           true);
            }
            else
            {
                textBox.Page.ClientScript.RegisterStartupScript(textBox.Page.GetType(),
                                                 "Select-" + textBox.ClientID,
                                                 String.Format("document.getElementById('{0}').select();", textBox.ClientID),
                                                 true);
            }
        }


    }
}
