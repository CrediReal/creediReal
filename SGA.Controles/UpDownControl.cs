using System;
using System.Diagnostics;
using System.Text;				
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;			
using System.Collections;		
using System.Collections.Specialized;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    /// <summary>
    /// Posición de los botones
    /// </summary>
    public enum PosButton
    {
        /// <summary>
        /// Los dos a la izquierda.
        /// </summary>
        LeftLeft,
        /// <summary>
        /// Uno a la izquierda otro a la derecha.
        /// </summary>
        LeftRigth,
        /// <summary>
        /// Los dos a la derecha.
        /// </summary>
        RigthRigth,
        /// <summary>
        /// Uno Arriba y el otro abajo.
        /// </summary>
        UpDown,
        /// <summary>
        /// Los dos arriba.
        /// </summary>
        UpUp,
        /// <summary>
        /// Los dos abajo.
        /// </summary>
        DownDown
    }

    /// <summary>
    /// TextBox con botones de más menos que se ejecuta en el cliente.
    /// </summary>
    [DefaultProperty("Text"),
        ToolboxData("<{0}:numericUpDown runat=server></{0}:numericUpDown>")]
    public class numericUpDown : System.Web.UI.WebControls.TextBox
    {
        #region Propiededes
        /// <summary>
        /// Posición de los votones.
        /// </summary>
        public PosButton PositionButton
        {
            get
            {
                object o = ViewState["PositionButton"];
                return (o == null) ? PosButton.LeftLeft : (PosButton)o;
            }

            set
            {
                base.ViewState["PositionButton"] = value;
            }
        }

        /// <summary>
        /// Indica si los botones tienen que aparecer en orden inverso.
        /// </summary>
        public bool PositionInverso
        {
            get
            {
                object o = ViewState["PositionInverso"];
                return (o == null) ? false : (bool)o;
            }

            set
            {
                base.ViewState["PositionInverso"] = value;
            }
        }

        /// <summary>
        /// Imagen del boton incrementar.
        /// </summary>
        public string ImageUrlPlus
        {
            get
            {
                object o = base.ViewState["ImageUrlPlus"];
                return (o == null) ? String.Empty : (string)o;
            }

            set
            {
                base.ViewState["ImageUrlPlus"] = value;
            }
        }

        /// <summary>
        /// Imagen del boton decrementar.
        /// </summary>
        public string ImageUrlMinus
        {
            get
            {
                object o = ViewState["ImageUrlMinus"];
                return (o == null) ? String.Empty : (string)o;
            }

            set
            {
                base.ViewState["ImageUrlMinus"] = value;
            }
        }

        public decimal Maximun
        {
            get
            {
                object o = ViewState["Maximun"];
                return (o == null) ? 100 : (decimal)o;
            }

            set
            {
                base.ViewState["Maximun"] = value;
            }
        }

        public decimal Minimun
        {
            get
            {
                object o = ViewState["Minimun"];
                return (o == null) ? 0 : (decimal)o;
            }

            set
            {
                base.ViewState["Minimun"] = value;
            }
        }

        /// <summary>
        /// Valor por defecto.
        /// </summary>
        public decimal ValueDefault
        {
            get
            {
                object o = ViewState["ValueDefault"];
                return (o == null) ? 0 : (decimal)o;
            }

            set
            {
                base.ViewState["ValueDefault"] = value;
            }
        }

        public override string Text
        {
            get
            {
                object o = ViewState["Text"];
                string sText = (o == null) ? String.Empty : (string)o;
                try
                {
                    decimal valor = System.Convert.ToDecimal(sText);
                    if (valor > Maximun)
                        sText = System.Convert.ToString(Maximun);
                    else if (valor < Minimun)
                        sText = System.Convert.ToString(Minimun);
                }
                catch (System.Exception)
                {
                    sText = System.Convert.ToString(ValueDefault);
                }
                return sText;
            }

            set
            {
                base.ViewState["Text"] = value;
            }
        }
        #endregion
        #region Render
        /// <summary> 
        /// Procesar este control en el parámetro de salida especificado.
        /// </summary>
        /// <param name="output">Programa de escritura HTML para escribir</param>
        protected override void Render(HtmlTextWriter output)
        {
            if (ImageUrlMinus == "" && ImageUrlPlus == "")
            {
                base.Render(output);
                return;
            }
            output.Write(@"<table border=""0"" cellspacing=""0"" cellpadding=""0""");

            output.Write(@" style=""");
            foreach (string key in base.Style.Keys)
            {
                output.Write(key);
                output.Write(":");
                output.Write(base.Style[key]);
                output.Write(";");
            }
            output.Write(@"""");

            base.Style.Remove("TOP");
            base.Style.Remove("LEFT");
            base.Style.Remove("POSITION");

            output.Write(">");
            switch (PositionButton)
            {
                case PosButton.LeftLeft:
                    RenderLeftLeft(output);
                    break;
                case PosButton.RigthRigth:
                    RenderRigthRigth(output);
                    break;
                case PosButton.LeftRigth:
                    RenderLeftRigth(output);
                    break;
                case PosButton.UpUp:
                    RenderUpUp(output);
                    break;
                case PosButton.DownDown:
                    RenderDownDown(output);
                    break;
                case PosButton.UpDown:
                    RenderUpDown(output);
                    break;

            }
            //output.WriteEndTag(HtmlTextWriterTag.Table);
            output.WriteEndTag("table");
        }
        /// <summary> 
        /// Pinta los dos botones a la izquierda.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected void RenderLeftLeft(HtmlTextWriter output)
        {
            output.Write(@"<tr><td valign = ""bottom"" align= ""right"">");
            RenderBoton2(output);
            output.Write(@"</td><td rowspan=""2"" valign =""middle"" align= ""left"">");
            base.Render(output);
            output.Write(@"</tr><tr><td valign = ""top"" align= ""right"">");
            RenderBoton1(output);
            output.Write(@"</td></tr>");
        }

        /// <summary> 
        /// Pinta los dos botones a la izquierda.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected void RenderRigthRigth(HtmlTextWriter output)
        {
            output.Write(@"<tr><td rowspan=""2"" valign =""middle"" align= ""right"">");
            base.Render(output);
            output.Write(@"</td><td valign = ""bottom"" align= ""left"">");
            RenderBoton2(output);
            output.Write(@"</tr><tr><td valign = ""top"" align= ""left"">");
            RenderBoton1(output);
            output.Write(@"</td></tr>");
        }

        /// <summary> 
        /// Pinta los dos botones a derecha e izquierda.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected void RenderLeftRigth(HtmlTextWriter output)
        {
            output.Write(@"<tr valign = ""middle"" align = ""right"">");
            if (bBootenes1)
            {
                output.Write("<td>");
                RenderBoton1(output);
                output.Write(@"</td>");
            }
            output.Write(@"<td valign = ""middle"" align= ""center"">");
            base.Render(output);
            output.Write(@"</td>");
            if (bBootenes2)
            {
                output.Write(@"<td valign = ""middle"" align= ""left"">");
                RenderBoton2(output);
                output.Write(@"</td>");
            }
            output.Write(@"</tr>");
        }

        /// <summary> 
        /// Pinta los dos botones a la derecha.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected void RenderUpUp(HtmlTextWriter output)
        {
            output.Write(@"<tr><td valign = ""bottom"" align= ""right"">");
            RenderBoton1(output);
            output.Write(@"</td><td valign = ""bottom"" align= ""left"">");
            RenderBoton2(output);
            output.Write(@"</tr><tr><td colspan = ""2"" valign = ""top"" align= ""center"">");
            base.Render(output);
            output.Write(@"</td></tr>");
        }

        /// <summary> 
        /// Pinta los dos botones a la derecha.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected void RenderDownDown(HtmlTextWriter output)
        {
            output.Write(@"<tr><td colspan = ""2"" valign = ""bottom"" align= ""center"">");
            base.Render(output);
            output.Write(@"</td></tr><tr><td valign = ""middle"" align= ""right"">");
            RenderBoton1(output);
            output.Write(@"</td><td valign = ""top"" align= ""left"">");
            RenderBoton2(output);
            output.Write(@"</td></tr>");
        }

        /// <summary> 
        /// Pinta los dos botones a la derecha.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected void RenderUpDown(HtmlTextWriter output)
        {
            if (bBootenes2)
            {
                output.Write(@"<tr><td valign = ""bottom"" align= ""center"">");
                RenderBoton2(output);
                output.Write(@"</td></tr>");
            }
            output.Write(@"<tr><td valign = ""middle"" align= ""center"">");
            base.Render(output);
            output.Write(@"</td></tr>");
            if (bBootenes1)
            {
                output.Write(@"<tr><td valign = ""top"" align= ""center"">");
                RenderBoton1(output);
                output.Write(@"</td></tr>");
            }
        }

        /// <summary> 
        /// Pinta el primer botón.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected void RenderBoton1(HtmlTextWriter output)
        {
            if (this.PositionInverso)
                RenderBotonesPlus(output);
            else
                RenderBotonesMinus(output);
        }

        /// <summary> 
        /// Pinta el segundo botón.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected void RenderBoton2(HtmlTextWriter output)
        {
            if (this.PositionInverso)
                RenderBotonesMinus(output);
            else
                RenderBotonesPlus(output);
        }

        /// <summary>
        /// Indica si se ha definido el bonton1.
        /// </summary>
        protected bool bBootenes1
        {
            get
            {
                if (this.PositionInverso)
                    return ImageUrlPlus == "" ? false : true;
                else
                    return ImageUrlMinus == "" ? false : true;
            }
        }

        /// <summary>
        /// Indica si se ha definido el bonton1.
        /// </summary>
        protected bool bBootenes2
        {
            get
            {
                if (this.PositionInverso)
                    return ImageUrlMinus == "" ? false : true;
                else
                    return ImageUrlPlus == "" ? false : true;
            }
        }

        /// <summary> 
        /// Pinta el boton más.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected void RenderBotonesPlus(HtmlTextWriter output)
        {
            if (ImageUrlPlus == "")
            {
                output.Write("&nbsp;");
            }
            else
            {
                output.WriteBeginTag("img");
                output.WriteAttribute("src", ImageUrlPlus);
                output.WriteAttribute("border", "0");
                output.WriteAttribute("onclick", "javascript:IncrementaValor(" + this.ClientID + ",1," + Maximun + "," + Minimun + ",'" + ValueDefault + "');");
                output.Write(">");
            }
        }

        /// <summary> 
        /// Pinta el boton menos.
        /// </summary>
        /// <param name="output"> Programa de escritura HTML para escribir </param>
        protected void RenderBotonesMinus(HtmlTextWriter output)
        {
            if (ImageUrlMinus == "")
            {
                output.Write("&nbsp;");
            }
            else
            {
                output.WriteBeginTag("img");
                output.WriteAttribute("src", ImageUrlMinus);
                output.WriteAttribute("border", "0");
                output.WriteAttribute("onclick", "javascript:IncrementaValor(" + this.ClientID + ",-1," + Maximun + "," + Minimun + ",'" + ValueDefault + "');");
                output.Write(">");
            }
        }

        /// <summary>
        /// Inserta codigo javascript necesario para el control.
        /// </summary>
        protected override void OnPreRender(System.EventArgs e)
        {
            if (!this.Page.IsClientScriptBlockRegistered("numericUpDown_Valor"))
            {
                string sScript = @"
				<script language=""javascript"">				
					function IncrementaValor(texto, Cantidad, maximun, minimun, valuedefault)
					{
						if(!isFinite(texto.value) || texto.value == """")
						{
							texto.value = valuedefault;
						}
						indice = eval(texto.value);
						indice = eval(indice+Cantidad);
						if(indice > maximun)
							indice = maximun;
						if(indice < minimun)
							indice = minimun;
						texto.value = indice;
					}
				</script>";
                this.Page.RegisterClientScriptBlock("numericUpDown_Valor", sScript);
            }
        }
        #endregion
    }
}