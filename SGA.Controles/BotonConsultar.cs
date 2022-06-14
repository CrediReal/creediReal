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
    [ToolboxData("<{0}:BotonConsultar runat=server></{0}:BotonConsultar>")]
    public class BotonConsultar : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Consultar";
            this.CssClass = "btnBase_Consultar";
            base.Render(writer);
        }
    }
}
