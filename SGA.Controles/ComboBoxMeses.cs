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
using SGA.ENTIDADES;
using SGA.LogicaNegocio;

namespace SGA.Controles
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxMeses runat=server></{0}:ComboBoxMeses>")]
    public class ComboBoxMeses : ComboBoxBase
    {
        public void CargarMeses()
        {
            if (!Page.IsPostBack)
            {
                DataTable dt = new clsCNAniosMeses().GetMeses();
                DataValueField = "nMes";
                DataTextField = "cMes";
                DataSource = dt;
                DataBind();
                AgregarTodos();
            }
        }
    }
}
