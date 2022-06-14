using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsTipoMeta
    {
        public int idTipoMeta { get; set; }
        public string cTipoMeta { get; set; }
        public int idUsuario { get; set; }
        public bool lVigente { get; set; }

        public clsTipoMeta()
        {
            idTipoMeta = 0;
            cTipoMeta = string.Empty;
            idUsuario = 0;
            lVigente = true;
        }

        public clsTipoMeta(DataRow row)
        {
            idTipoMeta = Convert.ToInt16(row["idTipoMeta"]);
            cTipoMeta = Convert.ToString(row["cTipoMeta"]);
            lVigente = Convert.ToBoolean(row["lVigente"]);
        }
    }
}
