using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsModelo
    {
        public int idModelo { get; set; }
        public string cModelo { get; set; }
        public clsMarca Marca { get; set; }
        public int idUsuario { get; set; }
        public bool lVigente { get; set; }

        public clsModelo()
        {
            idModelo = 0;
            cModelo = string.Empty;
            Marca = new clsMarca();
            idUsuario = 0;
            lVigente = true;
        }

        public clsModelo(DataRow row)
        {
            idModelo = Convert.ToInt32(row["idModelo"]);
            cModelo = row["cModelo"].ToString();
            Marca = new clsMarca()
            {
                idMarca = Convert.ToInt32(row["idMarca"]),
                cMarca = row["cMarca"].ToString()
            };
            idUsuario = Convert.ToInt32(row["idModelo"]);
            lVigente = Convert.ToBoolean(row["lVigente"]);
        }
    }
}
