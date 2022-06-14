using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsConfiguracion
    {
        public int idConfiguracion { get; set; }
        public string cConfiguracion { get; set; }
        public int idUsuario { get; set; }
        public bool lVigente { get; set; }

        public clsConfiguracion()
        {
            idConfiguracion = 0;
            cConfiguracion = string.Empty;
            idUsuario = 0;
            lVigente = true;
        }

        public clsConfiguracion(DataRow row)
        {
            idConfiguracion = Convert.ToInt32(row["idConfiguracion"]);
            cConfiguracion = Convert.ToString(row["cConfiguracion"]);
            lVigente = Convert.ToBoolean(row["lVigente"]);
        }
    }
}
