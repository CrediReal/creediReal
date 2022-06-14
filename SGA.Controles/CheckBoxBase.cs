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
    [System.Drawing.ToolboxBitmap(typeof(CheckBox))]
    [ToolboxData("<{0}:CheckBoxBase runat=server></{0}:CheckBoxBase>")]
    public class CheckBoxBase : CheckBox
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.CssClass = "chkBase";
        }
    }
}
