using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    [System.Drawing.ToolboxBitmap(typeof(TextBox))]
    [ToolboxData("<{0}:NumberBox runat=server></{0}:NumberBox>"), DefaultProperty("DecimalPlaces")]
    public class NumberBox : TextBox
    {
        private int mDecimalPlaces = 0;
        private char mDecimalSymbol = '.';
        private bool mAllowNegatives = true;

        /// <summary>
        /// Obtiene o establece el número de decimales para el cuadro de número.
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(0), Description("Indica el número de decimales a mostrar.")]
        public virtual int DecimalPlaces
        {
            get { return mDecimalPlaces; }
            set { mDecimalPlaces = value; }
        }

        /// <summary>
        /// Obtiene o establece el símbolo de agrupamiento de dígitos para el cuadro de número.
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("."), Description("El símbolo de agrupación de dígitos.")]
        public virtual char DecimalSymbol
        {
            get { return mDecimalSymbol; }
            set { mDecimalSymbol = value; }
        }

        /// <summary>
        /// Obtiene o establecesi están permitidos los números negativos en el cuadro de número.
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(true), Description("True cuando los valores negativos se permiten")]
        public virtual bool AllowNegatives
        {
            get { return mAllowNegatives; }
            set { mAllowNegatives = value; }
        }

        /// <summary>
        /// Obtiene o establece el valor del cuadro de número.
        /// </summary>
        public virtual double Value
        {
            get
            {
                try
                {
                    return ParseStringToDouble(this.Text == "" ? "0" : this.Text);
                }
                catch (FormatException e)
                {
                    throw new
                    InvalidOperationException(e.Message);
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            set
            {
                if ((value < 0) & !AllowNegatives)
                    throw new
                        ArgumentOutOfRangeException("Sólo se permiten valores positivos para este NumberBox");

                //base.Text = value.ToString(this.Format);
                base.Text = value.ToString(GetFormat()).Replace(".", DecimalSymbol.ToString());
            }
        }

        /// <summary>
        /// Obtiene o establece el contenido de texto del cuadro de número.
        /// </summary>
        override public string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                try
                {
                    if (value == "")
                    {
                        value = "0";
                    }
                    this.Value = ParseStringToDouble(value);
                }
                catch (FormatException e)
                {
                    base.Text = value;
                    throw e;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// agrega un JavaScript en la página y llama al evento onKeyPress del form
        /// </summary>
        /// <param name="e"></param>
        override protected void OnPreRender(EventArgs e)
        {

            if (this.Page.Request.Browser.JavaScript == true)
            {
                this.CssClass = "txtBase";
                // Build JavaScript		
                StringBuilder s = new StringBuilder();
                s.Append("\n<script type='text/javascript' language='JavaScript'>\n");
                s.Append("<!--\n");
                s.Append("    function NumberBoxKeyPress(event, dp, dc, n) {\n");
                s.Append("	     var myString = new String(event.srcElement.value);\n");
                s.Append("	     var pntPos = myString.indexOf(String.fromCharCode(dc));\n");
                s.Append("	     var keyChar = window.event.keyCode;\n");
                s.Append("       if ((keyChar < 48) || (keyChar > 57)) {\n");
                s.Append("          if (keyChar == dc) {\n");
                s.Append("              if ((pntPos != -1) || (dp < 1)) {\n");
                s.Append("                  return false;\n");
                s.Append("              }\n");
                s.Append("          } else \n");
                s.Append("if (((keyChar == 45) && (!n || myString.length != 0)) || (keyChar != 45)) \n");
                s.Append("		     return false;\n");
                s.Append("       }\n");
                s.Append("       return true;\n");
                s.Append("    }\n");
                s.Append("// -->\n");
                s.Append("</script>\n");

                // Add the Script to the Page
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(string), "NumberBoxKeyPress", s.ToString());
                // this.Page.RegisterClientScriptBlock("NumberBoxKeyPress", s.ToString());

                // Add KeyPress Event
                try
                {
                    this.Attributes.Remove("onKeyPress");
                }
                finally
                {
                    this.Attributes.Add("onKeyPress", "return NumberBoxKeyPress(event, "
                        + DecimalPlaces.ToString() + ", "
                        + ((int)DecimalSymbol).ToString() + ", "
                        + AllowNegatives.ToString().ToLower() + ")");
                }
            }
        }

        /// <summary>
        /// Retorna  RegularExpression string que se puede utilizar para validar
        /// usando un RegularExpressionValidator.
        /// </summary>
        virtual public string ValidationRegularExpression
        {
            get
            {
                StringBuilder regexp = new StringBuilder();

                if (AllowNegatives)
                    regexp.Append("([-]|[0-9])");

                regexp.Append("[0-9]*");

                if (DecimalPlaces > 0)
                {
                    regexp.Append("([");
                    regexp.Append(DecimalSymbol);
                    regexp.Append("]|[0-9]){0,1}[0-9]{0,");
                    regexp.Append(DecimalPlaces.ToString());
                    regexp.Append("}$");
                }

                return regexp.ToString();
            }
        }

        /// <summary>
        /// Convierte un String a un Double
        /// </summary>
        /// <param name="s">string a ser convertido a un double</param>
        /// <returns>double value</returns>
        virtual protected double ParseStringToDouble(string s)
        {
            s = s.Replace(DecimalSymbol.ToString(), ".");
            return double.Parse(s);
        }

        /// <summary>
        /// Retorna el FormatString usado para mostrar el valor en el cuadro de número.
        /// </summary>
        /// <returns>Format string</returns>
        virtual protected string GetFormat()
        {
            StringBuilder f = new StringBuilder();
            f.Append("0");
            if (DecimalPlaces > 0)
            {
                f.Append(".");
                f.Append('0', DecimalPlaces);
            }

            return f.ToString();
        }

    }
}
