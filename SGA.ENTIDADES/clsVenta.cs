
using System;
using System.Collections.Generic;
using System.Data;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsVenta
    {
        public int idVenta { get; set; }
        public int idCli { get; set; }
        public string cNombres { get; set; }
        public int idOficina { get; set; }
        public string cNomOficina { get; set; }
        public decimal nTotalVenta { get; set; }
        public int idEstado { get; set; }
        public List<clsDetVenta> DetalleVenta { get; set; } 
        public clsVenta()
        {
            idVenta = 0;
            idCli = 0;
            cNombres = string.Empty;
            idOficina = 0;
            cNomOficina = string.Empty;
            nTotalVenta = 0M;
            idEstado = 1;
            DetalleVenta = new List<clsDetVenta>();
        }

        public clsVenta(DataRow row)
        {
            idVenta = Convert.ToInt32(row["idVenta"]);
            idCli = Convert.ToInt32(row["idCli"]);
            cNombres = Convert.ToString(row["cNombres"]);
            idOficina = Convert.ToInt32(row["idOficina"]);
            cNomOficina = Convert.ToString(row["cNomOficina"]);
            nTotalVenta = Convert.ToDecimal(row["nTotalVenta"]);
            idEstado = Convert.ToInt32(row["idEstado"]);
        }
    }
}
