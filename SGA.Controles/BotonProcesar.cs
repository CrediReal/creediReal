using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    [ToolboxData("<{0}:BotonProcesar runat=server></{0}:BotonProcesar>")]
    public class BotonProcesar : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Procesar";
            this.CssClass = "btnBase_Procesar";

            base.Render(writer);
        }
    }
}
