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
    [ToolboxData("<{0}:BotonImprimir runat=server></{0}:BotonImprimir>")]
    public class BotonImprimir : Button
    {

        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Imprimir";
            this.CssClass = "btnBase_Imprimir";
            base.Render(writer);
        }

    }
}
