using SGA.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("ControlesBase", "SolIntELS")]
namespace SGA.Controles
{
    [System.Drawing.ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxMoneda runat=server></{0}:ComboBoxMoneda>")]
    public class ComboBoxMoneda : DropDownList
    {
        private void Llenar()
        {
            this.CssClass = "cmbBase";

            clsCNMoneda cnmoneda = new clsCNMoneda();

            DataTable dtMoneda = cnmoneda.ListarMoneda();
            this.DataValueField = dtMoneda.Columns[0].ToString();
            this.DataTextField = dtMoneda.Columns[1].ToString();
            this.DataSource = dtMoneda;
            this.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Llenar();
        }
    }
}
