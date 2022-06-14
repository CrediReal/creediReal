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
    public class clsTipo
    {
        public int idTipo { get; set; }
        public string cTipo { get; set; }
        public int idUsuario { get; set; }
        public bool lVigente { get; set; }

        public clsTipo()
        {
            idTipo = 0;
            cTipo = String.Empty;
            idUsuario = 0;
            lVigente = true;
        }

        public clsTipo(DataRow row)
        {
            idTipo = Convert.ToInt32(row["idTipo"]);
            cTipo = Convert.ToString(row["cTipo"]);
            lVigente = Convert.ToBoolean(row["lVigente"]);
        }
    }
}
