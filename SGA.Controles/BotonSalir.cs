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
    [ToolboxData("<{0}:BotonSalir runat=server></{0}:BotonSalir>")]
    public class BotonSalir : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Salir";
            this.CssClass = "btnBase_Salir";

            base.Render(writer);
        }
    }
}
