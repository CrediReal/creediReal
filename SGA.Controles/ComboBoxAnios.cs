using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.LogicaNegocio;

namespace SGA.Controles
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxAnios runat=server></{0}:ComboBoxAnios>")]
    public class ComboBoxAnios : ComboBoxBase
    {
        public int nIni { get; set; }
        public int nFin { get; set; }

        public void ListarAnios()
        {
            DataTable dt = new clsCNAniosMeses().GetAnios(nIni, nFin);
            DataValueField = "nAnio";
            DataTextField = "cAnio";
            DataSource = dt;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }

    }
}
