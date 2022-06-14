using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsDetVenta
    {
        public int idDetVenta { get; set; }
        public int idInterno { get; set; }
        public int idVenta { get; set; }
        public int idMarca { get; set; }
        public string cMarca { get; set; }
        public int idModelo { get; set; }
        public string cModelo { get; set; }
        public decimal nCantidad { get; set; }
        public decimal nPrecio { get; set; }
        public decimal nTotal { get; set; }

        public clsDetVenta()
        {
            idDetVenta = 0;
            idInterno = 0;
            idVenta = 0;
            idMarca = 0;
            idModelo = 0;
            nCantidad = 0M;
            nPrecio = 0M;
            nTotal = 0M;
        }

        public clsDetVenta(DataRow row)
        {
            idDetVenta = Convert.ToInt32(row["idDetVenta"]);
            idMarca = Convert.ToInt32(row["idMarca"]);
            cMarca = Convert.ToString(row["cMarca"]);
            idModelo = Convert.ToInt32(row["idModelo"]);
            cModelo = Convert.ToString(row["cModelo"]);
            nCantidad = Convert.ToDecimal(row["nCantidad"]);
            nPrecio = Convert.ToDecimal(row["nPrecio"]);
            nTotal = Convert.ToDecimal(row["nTotal"]);
        }
    }
}
