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
    [System.Drawing.ToolboxBitmap(typeof(TextBox))]
    [ToolboxData("<{0}:TextBoxBase runat=server></{0}:TextBoxBase>")]
    public class TextBoxBase : TextBox
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.CssClass = "txtBase";
        }
    }
}
