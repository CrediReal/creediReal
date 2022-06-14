using SGA.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Controles
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxDistrito runat=server></{0}:ComboBoxDistrito>")]
    public class ComboBoxDistrito : ComboBoxBase
    {
        public void ListarDistritos(string cCodDepartamento,string cCodProvincia)
        {
            DataTable dt = new clsCNUbigeo().ListarDistrito(cCodDepartamento, cCodProvincia);

            DataValueField = dt.Columns[0].ColumnName;
            DataTextField = dt.Columns[1].ColumnName;
            DataSource = dt;
            DataBind();
            SelectedIndex = -1;
        }
    }
}
