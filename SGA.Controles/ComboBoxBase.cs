using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Reflection;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    [System.Drawing.ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxBase runat=server></{0}:ComboBoxBase>")]
    public class ComboBoxBase : DropDownList
    {
        private bool _addTodos = false;
        public bool AddTodos
        {
            get { return _addTodos; }
            set { this._addTodos = value; }
        }

        protected void AgregarTodos()
        {
            if (_addTodos && !string.IsNullOrEmpty(DataValueField) && !string.IsNullOrEmpty(DataTextField)
                   && DataSource != null)
            {
                if (DataSource is IList &&
                        DataSource.GetType().IsGenericType &&
                        DataSource.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)))
                {
                    Type typeObject = DataSource.GetType().GetGenericArguments().Single();
                    object objTodos = Activator.CreateInstance(typeObject);
                    PropertyInfo propValue = typeObject.GetProperty(DataValueField, BindingFlags.Public | BindingFlags.Instance);
                    if (propValue != null && propValue.CanWrite)
                    {
                        propValue.SetValue(objTodos, 0, null);
                    }
                    PropertyInfo propText = typeObject.GetProperty(DataTextField, BindingFlags.Public | BindingFlags.Instance);
                    if (propText != null && propText.CanWrite)
                    {
                        propText.SetValue(objTodos, "TODOS", null);
                    }
                    DataSource.GetType().GetMethod("Insert").Invoke(DataSource, new[] { 0, objTodos });
                    DataBind();
                }
                if (DataSource is DataTable)
                {
                    DataTable dt = DataSource as DataTable;
                    DataRow dr = dt.NewRow();
                    dr[DataValueField] = 0;
                    dr[DataTextField] = "TODOS";
                    dt.Rows.InsertAt(dr, 0);
                    DataSource = dt;
                    DataBind();
                }

            }
        }

        private void Llenardropdown()
        {
            this.CssClass = "cmbBase";
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Llenardropdown();
        }

    }
}
