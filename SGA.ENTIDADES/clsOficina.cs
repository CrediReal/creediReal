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
    public class clsOficina
    {
        public int idOficina { get; set; }
        public string cNomOficina { get; set; }
        public string cDireccion { get; set; }
        public string cTelef { get; set; }
        public int idUsuario { get; set; }
        public bool lVigente { get; set; }

        public clsOficina()
        {
            idOficina = 0;
            cNomOficina = string.Empty;
            cDireccion = string.Empty;
            cTelef = string.Empty;
            idUsuario = 0;
            lVigente = true;
        }

        public clsOficina(DataRow row)
        {
            idOficina = Convert.ToInt32(row["idOficina"]);
            cNomOficina = Convert.ToString(row["cNomOficina"]);
            cDireccion = Convert.ToString(row["cDireccion"]);
            cTelef = Convert.ToString(row["cTelef"]);
            lVigente = Convert.ToBoolean(row["lVigente"]);
        }
    }
}
