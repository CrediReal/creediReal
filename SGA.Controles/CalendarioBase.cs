using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//personalizando los tags para este control
[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    //añadir icono al control
    [System.Drawing.ToolboxBitmap(typeof(Calendar))]

    [ToolboxData("<{0}:CalendarioBase runat=server></{0}:CalendarioBase>")]
    public class CalendarioBase : CompositeControl
    {
        public TextBox textBox;
        Button boton;
        Calendar calendar;
        RegularExpressionValidator validaFecha;

        [Category("Appearance")]
        [Description("Setear la imagen para el calendario")]
        public string ImagenBotonCss//para poder asignar una imagen de fondo al boton
        {
            get
            {
                EnsureChildControls();
                return boton.CssClass != null ? boton.CssClass : string.Empty;
            }
            set
            {
                EnsureChildControls();
                boton.CssClass = value;
            }
        }

        protected override void RecreateChildControls()
        {
            //permite que no se desaqparezca el contenido del valor de la propiedad
            //que se a creado
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            textBox = new TextBox();
            textBox.ID = "fechaTextBox";
            textBox.Width = Unit.Pixel(80);
            textBox.MaxLength = 10;//Cantidad maxima de caracteres
            //textBox.TextMode = TextBoxMode.DateTime;
            textBox.CssClass = "txtBase";


            boton = new Button();
            boton.ID = "BotonCalendario";
            boton.CssClass = "IconoCalendario";
            boton.Click += new EventHandler(boton_Click);

            calendar = new Calendar();
            calendar.ID = "ControlCalendario";
            calendar.Visible = false;
            calendar.CssClass = "txtBaseCalendario";


            calendar.DayStyle.CssClass = "DiaCalendarioBase";
            calendar.OtherMonthDayStyle.CssClass = "OtrosDiaCalendarioBase";
            calendar.SelectorStyle.CssClass = "SelectorDiaCalendarioBase";
            calendar.TitleStyle.CssClass = "TituloCalendarioBase";
            calendar.NextPrevStyle.CssClass = "NextPrevCalendarioBase";
            calendar.SelectionChanged += new EventHandler(calendar_SelectionChanged);


            validaFecha = new RegularExpressionValidator();
            validaFecha.ID = "validaFechaID";
            validaFecha.ControlToValidate = textBox.ID;
            validaFecha.ErrorMessage = "*";
            validaFecha.ValidationExpression = @"^(([0-9])|([0-2][0-9])|(3[0-1]))\/(([1-9])|(0[1-9])|(1[0-2]))\/(([0-9][0-9])|([1-2][0,9][0-9][0-9]))$";//Formato Ingles dd/mm/yyyy
            validaFecha.Display = ValidatorDisplay.Dynamic;
            //validaFecha.CssClass    = "validator";
            validaFecha.ForeColor = System.Drawing.Color.Red;

            //añadir controles hijos para calendario personalizado
            this.Controls.Add(textBox);
            //this.Controls.Add(boton);
            this.Controls.Add(calendar);
            this.Controls.Add(validaFecha);
        }

        [Category("Appearance")]
        [Description("Obtiene o asigna la fecha del calendario personalizado")]
        public DateTime SeleccionarFecha
        {
            get
            {
                EnsureChildControls();
                return string.IsNullOrEmpty(textBox.Text) ? DateTime.MinValue : Convert.ToDateTime(textBox.Text);
            }
            set
            {
                if (value != null)
                {
                    EnsureChildControls();
                    textBox.Text = value.ToString("dd/MM/yyyy");//value.ToShortDateString();
                    calendar.SelectedDate = value;
                }
                else
                {
                    EnsureChildControls();
                    textBox.Text = "";
                    calendar.VisibleDate = DateTime.Today;
                }
            }
        }

        void calendar_SelectionChanged(object sender, EventArgs e)
        {
            textBox.Text = calendar.SelectedDate.ToShortDateString();
            ObtenerValorFechaCalendarioEventArgs eventoFecha = new ObtenerValorFechaCalendarioEventArgs(calendar.SelectedDate);
            //llamamos al método que hemos creado
            OnDateSelection(eventoFecha);
            calendar.Visible = false;
        }

        void boton_Click(object sender, EventArgs e)
        {
            if (calendar.Visible)
            {
                calendar.Visible = false;
            }
            else
            {
                calendar.Visible = true;
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    calendar.VisibleDate = DateTime.Today;
                }
                else
                {
                    DateTime output = DateTime.Today;
                    bool isConvertidoConExitoDateTime = DateTime.TryParse(textBox.Text, out output);
                    calendar.VisibleDate = output;
                }
            }
        }

        override protected void OnPreRender(EventArgs e)
        {

            if (this.Page.Request.Browser.JavaScript == true)
            {
                // Build JavaScript		
                StringBuilder s = new StringBuilder();
                s.Append("\n<script type='text/javascript' language='JavaScript'>\n");
                s.Append("function CalendarioKeyPress(keyCode) {\n");
                s.Append("  if ((keyCode < 47) || (keyCode > 57)) {\n");
                s.Append("      return false;\n");
                s.Append("  }\n");
                s.Append("}\n");

                /*s.Append("function CalendarioClick() {\n");
                s.Append("  alert('hola'); ");
                s.Append("      ");
                s.Append("}\n");*/


                // Traducción al español del control datePicker
                s.Append("$(function($){\n");
                s.Append("    $.datepicker.regional['es'] = { \n");
                s.Append("        closeText: 'Cerrar', \n");
                s.Append("        prevText: '<Ant', \n");
                s.Append("        nextText: 'Sig>', \n");
                s.Append("        currentText: 'Hoy', \n");
                s.Append("        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'], \n");
                s.Append("        monthNamesShort: ['Ene','Feb','Mar','Abr', 'May','Jun','Jul','Ago','Sep', 'Oct','Nov','Dic'], \n");
                s.Append("        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'], \n");
                s.Append("        dayNamesShort: ['Dom','Lun','Mar','Mié','Juv','Vie','Sáb'], \n");
                s.Append("        dayNamesMin: ['Do','Lu','Ma','Mi','Ju','Vi','Sá'], \n");
                s.Append("        weekHeader: 'Sm', \n");
                s.Append("        dateFormat: 'dd/mm/yy', \n");
                s.Append("        firstDay: 1, \n");
                s.Append("        isRTL: false, \n");
                s.Append("        showMonthAfterYear: false, \n");
                s.Append("        yearSuffix: '' \n");
                s.Append("    }; \n");
                s.Append("    $.datepicker.setDefaults($.datepicker.regional['es']); \n");
                s.Append("});\n");

                //Añadir el  control
                s.Append("$(function() {\n");
                //s.Append("$( '#" + textBox.ClientID + "' ).datepicker();");
                s.Append("$( '#" + textBox.ClientID + "' ).datepicker({ \n");
                s.Append("changeMonth: true, \n");
                s.Append("changeYear: true,\n");
                s.Append("showOn: 'button',\n");
                s.Append("buttonImage: 'Styles/calendar3.png', \n");
                s.Append("buttonImageOnly: true \n");
                s.Append("});\n");
                s.Append("});\n");

                s.Append("</script>\n");

                // Add the Script to the Page
                this.Page.RegisterClientScriptBlock("CalendarioKeyPress" + "_" + textBox.ClientID, s.ToString());

                // Add KeyPress Event
                try
                {
                    //this.Attributes.Remove("onKeyPress");//Eliminar el evento onKeyPress
                    //this.Attributes.Remove("onClick");//Eliminar el evento onClick
                }
                finally
                {
                    this.Attributes.Add("onKeyPress", "return CalendarioKeyPress(event.keyCode)");//añadir el evento onKeyPress
                    //this.Attributes.Add("onClick", "return CalendarioClick(event.onClick)");//añadir el evento onClick
                }
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            AddAttributesToRender(output);
            output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "");

            //  +--------------------------------------+
            //  |   textBox |   boton   |   validaFecha |
            //  +--------------------------------------+  

            output.RenderBeginTag(HtmlTextWriterTag.Table);
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            textBox.RenderControl(output);
            output.RenderEndTag();
            /*output.RenderBeginTag(HtmlTextWriterTag.Td);
                boton.RenderControl(output);
            output.RenderEndTag();*/
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            validaFecha.RenderControl(output);
            output.RenderEndTag();
            output.RenderEndTag();//cierre tag tr
            output.RenderEndTag();//cierre tag table

            calendar.RenderControl(output);
        }

        public event ObtenerValorFechaCalendarioHandler FechaSeleccionada;

        protected virtual void OnDateSelection(ObtenerValorFechaCalendarioEventArgs e)
        {
            if (FechaSeleccionada != null)
            {
                FechaSeleccionada(this, e);
            }
        }
    }

    public class ObtenerValorFechaCalendarioEventArgs : EventArgs
    {
        private DateTime _fechaSeleccionada;

        public ObtenerValorFechaCalendarioEventArgs(DateTime fechaSeleccionada)
        {
            this._fechaSeleccionada = fechaSeleccionada;
        }
        public DateTime FechaSeleccionada
        {
            get
            {
                return this._fechaSeleccionada;
            }
        }
    }

    public delegate void ObtenerValorFechaCalendarioHandler(object sender, ObtenerValorFechaCalendarioEventArgs e);
}
