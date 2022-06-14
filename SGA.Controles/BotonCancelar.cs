using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    [ToolboxData("<{0}:BotonCancelar runat=server></{0}:BotonCancelar>")]
    public class BotonCancelar : Button
    {

        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Cancelar";
            this.CssClass = "btnBase_Cancelar";

            base.Render(writer);
        }
    }
}
