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
    [ToolboxData("<{0}:BotonEditar runat=server></{0}:BotonEditar>")]
    public class BotonEditar : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Editar";
            this.CssClass = "btnBase_Editar";
            base.Render(writer);
        }

    }
}