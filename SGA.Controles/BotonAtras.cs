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
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BotonAtras runat=server></{0}:BotonAtras>")]
    public class BotonAtras : Button
    {


        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Atrás";
            this.CssClass = "btnBase_Atras";

            base.Render(writer);
        }

    }
}
