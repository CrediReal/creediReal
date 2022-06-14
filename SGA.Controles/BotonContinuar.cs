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
    [ToolboxData("<{0}:BotonContinuar runat=server></{0}:BotonContinuar>")]
    public class BotonContinuar : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Continuar";
            this.CssClass = "btnBase_Continuar";
            base.Render(writer);
        }
    }
}
