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
    [ToolboxData("<{0}:BotonNuevo runat=server></{0}:BotonNuevo>")]
    public class BotonNuevo : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Nuevo";
            this.CssClass = "btnBase_Nuevo";
            base.Render(writer);
        }
    }
}