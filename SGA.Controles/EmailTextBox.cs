using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:EmailTextBox runat=server></{0}:EmailTextBox>")]
    public class EmailTextBox : CompositeControl
    {

        private TextBox TxtEmail;
        private RegularExpressionValidator ValidEmail;

        public string Email
        {
            get
            {
                EnsureChildControls();
                return TxtEmail.Text;
            }
            set
            {
                EnsureChildControls();
                TxtEmail.Text = value;
            }
        }

        /// <summary>
        /// Crea los controles, textbox y validator
        /// </summary>
        protected override void CreateChildControls()
        {
            //nombre del usuario
            TxtEmail = new TextBox();
            TxtEmail.CssClass = "txtBase";
            TxtEmail.ID = "TxtEmailID";
            this.Controls.Add(TxtEmail);
            //validador del email
            ValidEmail = new RegularExpressionValidator();
            ValidEmail.ID = "ValidEmailID";
            ValidEmail.ControlToValidate = TxtEmail.ID;
            ValidEmail.ErrorMessage = "(Email NO valido, escriba correctamente el email)";
            ValidEmail.ValidationExpression = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            ValidEmail.Display = ValidatorDisplay.Dynamic;
            ValidEmail.CssClass = "validator";
            this.Controls.Add(ValidEmail);
        }

        /// <summary>
        /// Crea la forma de presentacion del contenido, sera como tabla
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            //fila
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            //ubicando en primera celda la etiqueta nombre de usuario
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //clausula for del hml de la etiqueta label
            writer.AddAttribute(HtmlTextWriterAttribute.For, TxtEmail.ID);
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            //writer.Write("Email:");
            writer.RenderEndTag(); //finaliza tag html label
            writer.RenderEndTag(); //finaliza tag html td
            //segunda columna aca se captara el nombre del usuario
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            TxtEmail.RenderControl(writer); //mostrar el control de texto
            writer.RenderEndTag();
            //columna de displiegue de validacion
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            ValidEmail.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag(); //fin de la primera fila
        }

        /// <summary>
        /// El control se dibujara en una tabla
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Table;
            }
        }

    }
}
