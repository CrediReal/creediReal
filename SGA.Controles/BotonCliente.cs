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
    [ToolboxData("<{0}:BotonCliente runat=server></{0}:BotonCliente>")]
    public class BotonCliente : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Cliente";
            this.CssClass = "btnBase_Cliente";
            base.Render(writer);
        }
    }
}
