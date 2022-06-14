using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    [AspNetHostingPermission(SecurityAction.Demand,
         Level = AspNetHostingPermissionLevel.Minimal),
     AspNetHostingPermission(SecurityAction.InheritanceDemand,
         Level = AspNetHostingPermissionLevel.Minimal),
     DefaultProperty("Email"),
     ParseChildren(true, "Text"),
     ToolboxData("<{0}:MailLink runat=\"server\"> </{0}:MailLink>")]
    public class MailLink : WebControl
    {
        [Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("Direccion de correo")]
        public virtual string Email
        {
            get
            {
                string s = (string)ViewState["Email"];
                return (s == null) ? String.Empty : s;
            }
            set
            {
                ViewState["Email"] = value;
            }
        }

        [Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("texto qe muestra el link."),
        Localizable(true),
        PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public virtual string Text
        {
            get
            {
                string s = (string)ViewState["Text"];
                return (s == null) ? String.Empty : s;
            }
            set
            {
                ViewState["Text"] = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.A;
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "mailto:" + Email);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (Text == String.Empty)
            {
                Text = Email;
            }
            writer.WriteEncodedText(Text);
        }



    }
}
