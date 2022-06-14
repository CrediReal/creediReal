using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsTipoContacto
    {
        public int idTipoContacto { get; set; }
        public string cTipoContacto { get; set; }

        public clsTipoContacto()
        {
            idTipoContacto = 0;
            cTipoContacto = string.Empty;
        }

        public clsTipoContacto(DataRow dr)
        {
            idTipoContacto = Convert.ToInt32(dr["idTipoContacto"]);
            cTipoContacto = Convert.ToString(dr["cTipoContacto"]);
        }
    }
}
