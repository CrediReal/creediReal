using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SGA.LogicaNegocio;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    [System.Drawing.ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxBaseSexo runat=server></{0}:ComboBoxBaseSexo>")]
    public class ComboBoxBaseSexo : DropDownList
    {
        private void Llenar()
        {
            this.CssClass = "cmbBase";

            clsCNSexo ListaSexo = new clsCNSexo();

            DataTable tbSexo = ListaSexo.ListarSexo();            
            this.DataValueField = tbSexo.Columns[0].ToString();
            this.DataTextField = tbSexo.Columns[1].ToString();
            this.DataSource = tbSexo;
            this.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Llenar();
        }
    }
}
