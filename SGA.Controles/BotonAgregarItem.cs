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
    [ToolboxData("<{0}:BotonAgregarItem runat=server></{0}:BotonAgregarItem>")]
    public class BotonAgregarItem : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Agregar";
            this.CssClass = "btnBase_AgregarItem";

            base.Render(writer);
        }
    }
}
