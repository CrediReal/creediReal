using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsMarca
    {
        public int idMarca { get; set; }
        public string cMarca { get; set; }
        public int idUsuario { get; set; }
        public bool lVigente { get; set; }

        public clsMarca()
        {
            idMarca = 0;
            cMarca = string.Empty;
            idUsuario = 0;
            lVigente = true;
        }
        public clsMarca(DataRow row)
        {
            idMarca = Convert.ToInt32(row["idMarca"]);
            cMarca = Convert.ToString(row["cMarca"]);
            idUsuario = 0;
            lVigente = Convert.ToBoolean(row["lVigente"]);
        }
    }
}
