using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    /// <summary>
    /// Descripción breve de wccBotonEnviar. Al hacer clic solo se envia una vez
    /// </summary>
    [DefaultProperty("Text"),
        ToolboxData("<{0}:BotonEnviar runat=server></{0}:BotonEnviar>")]
    public class BotonEnviar : System.Web.UI.WebControls.Button
    {
        private string _TextoEnviando = "Enviando...";

        [Bindable(true),
            Category("Appearance"),
            DefaultValue("Procesando..."),
            Description("Espere un momento por favor")]
        public string TextoEnviando
        {
            get
            {
                return _TextoEnviando;
            }

            set
            {
                _TextoEnviando = value;
            }
        }

        public BotonEnviar()
        {
            base.Text = "Confirmar";
            base.CausesValidation = false;
        }

        protected override void OnPreRender(EventArgs e)
        {
            //Registramos la función de envío del botón
            Page.ClientScript.RegisterClientScriptBlock("".GetType(), "fEnviar_" + ID, FuncionEnviarBoton());
            //Page.RegisterClientScriptBlock("fEnviar_" + ID,FuncionEnviarBoton());
            base.OnPreRender(e);
        }

        /// <summary> 
        /// Procesar este control en el parámetro de salida especificado.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected override void Render(HtmlTextWriter output)
        {
            this.Text = "Grabar";
            this.CssClass = "btnBase_Grabar";

            //Creamos el panel donde va el botón principal
            output.Write("<div id='div1_" + ID + "' style='display: inline'>");

            output.AddAttribute("onclick", "Enviar_" + ID + "('" + ID + "');");
            base.Render(output);

            output.Write("</div>");

            //Creamos el panel y el botón secundario de envío
            output.Write("<div id='div2_" + ID + "' style='display: none'>");
            output.Write("<input disabled type=submit class='btnBase_Enviar' value='" + TextoEnviando + "' />");
            output.Write("</div>");

        }



        private string FuncionEnviarBoton()
        {
            string txt = "<script language='javascript'>";

            txt += @"function Enviar_" + ID + @"(id){
														  if (typeof(Page_ClientValidate) == 'function') 
														  {
															  if (Page_ClientValidate() == true ) 
															  { 
																  document.getElementById('div1_' + id).style.display = 'none';
																  document.getElementById('div2_' + id).style.display = 
																	  'inline';							return true;
															  }
															  else 
															  {
																  return false;
															  }
														  }
														  else 
														  {	
															  document.getElementById('div1_' + id).style.display = 'none';
															  document.getElementById('div2_' + id).style.display = 'inline';
															  return true;
														  }

													  }</script>;";

            return txt;
        }
    }
}
