using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [System.Drawing.ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxBaseMarca runat=server></{0}:ComboBoxBaseMarca>")]
    public class ComboBoxBaseMarca : DropDownList
    {
        public void Llenar(bool lTodos = false)
        {
            CssClass = "cmbBase";

            List<clsMarca> lstMarcas = new clsCNMarca().ListarMarcas(0, string.Empty, true);
            if (lTodos)
            {
                clsMarca objTodos = new clsMarca() {cMarca = "TODOS"};
                lstMarcas.Insert(0, objTodos);
            }
            DataValueField = "idMarca";
            DataTextField = "cMarca";
            DataSource = lstMarcas;
            DataBind();
        }

        
    }
}
