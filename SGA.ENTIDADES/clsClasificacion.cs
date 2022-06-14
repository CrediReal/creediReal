using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsClasificacion
    {
        public int idClasificacion { get; set; }
        public string cClasificacion { get; set; }
        public int idUsuario { get; set; }
        public bool lVigente { get; set; }

        public clsClasificacion()
        {
            idClasificacion = 0;
            cClasificacion = string.Empty;
            idUsuario = 0;
            lVigente = true;
        }
        public clsClasificacion(DataRow row)
        {
            idClasificacion = Convert.ToInt32(row["idClasificacion"]);
            cClasificacion = Convert.ToString(row["cClasificacion"]);
            idUsuario = 0;
            lVigente = Convert.ToBoolean(row["lVigente"]);
        }
    }
}
