using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsDescuento
    {
        public int idDescuentoCli { get; set; }
        public int idCli { get; set; }
        public string cNombres { get; set; }
        public int idClasificacion { get; set; }
        public string cClasificacion { get; set; }
        public decimal nDescuento { get; set; }
        public decimal nMaxDescuento { get; set; }

        public clsDescuento()
        {
            idDescuentoCli = 0;
            idCli = 0;
            cNombres = string.Empty;
            idClasificacion = 0;
            cClasificacion = string.Empty;
            nDescuento = 0M;
            nMaxDescuento = 0M;
        }

        public clsDescuento(DataRow row)
        {
            idDescuentoCli = Convert.ToInt32(row["idDescuentoCli"]);
            idCli = Convert.ToInt32(row["idCli"]);
            cNombres = Convert.ToString(row["cNombres"]);
            idClasificacion = Convert.ToInt32(row["idClasificacion"]);
            cClasificacion = Convert.ToString(row["cClasificacion"]);
            nDescuento = Convert.ToDecimal(row["nDescuento"]);
            nMaxDescuento = Convert.ToDecimal(row["nMaxDescuento"]);
        }
    }
}
