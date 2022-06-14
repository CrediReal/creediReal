using System;
using System.IO;
using System.Web.UI;

namespace SGA.Presentacion
{
    public class PageBase : Page
    {
        public PageBase()
        {
            Init += PageBase_Init;
        }

        protected void PageBase_Init(object sender, EventArgs e)
        {
            string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);
            if (Session["DatosUsuarioSession"] == null && pageName != "frmInicio")
            {
                Session.Clear();
                throw new Exception("La Sesión a terminado vuelva ingresar porfavor");
            }
        }
    }
}