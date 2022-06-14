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
    [ToolboxData("<{0}:BotonProcesarCierre runat=server></{0}:BotonProcesarCierre>")]
    public class BotonProcesarCierre : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.CssClass = "btnBase_ProcesarCierre";

            base.Render(writer);
        }
    }
}
