using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsMeta
    {
        public int idMeta { get; set; }
        public int nAnio { get; set; }
        public int nMes { get; set; }
        public string cMes { get; set; }
        public int idUsuario { get; set; }
        public string cUsuario { get; set; }
        public int idOficina { get; set; }
        public string cNomOficina { get; set; }
        public int idTipoMeta { get; set; }
        public string  cTipoMeta { get; set; }
        public decimal nValor { get; set; }

        public clsMeta()
        {
            idMeta = 0;
            nAnio = 0;
            nMes = 0;
            idUsuario = 0;
            idOficina = 0;
            idTipoMeta = 0;
            nValor = 0M;
        }

        public clsMeta(DataRow row)
        {
            idMeta = Convert.ToInt32(row["idMeta"]);
            nAnio = Convert.ToInt32(row["nAnio"]);
            nMes = Convert.ToInt32(row["nMes"]);
            cMes = Convert.ToString(row["cMes"]);
            idUsuario = Convert.ToInt16(row["idUsuario"]);
            cUsuario = Convert.ToString(row["cUsuario"]);
            idOficina = Convert.ToInt16(row["idOficina"]);
            cNomOficina = Convert.ToString(row["cNomOficina"]);
            idTipoMeta = Convert.ToInt16(row["idTipoMeta"]);
            cTipoMeta = Convert.ToString(row["cTipoMeta"]);
            nValor = Convert.ToDecimal(row["nValor"]);
        }
    }
}
