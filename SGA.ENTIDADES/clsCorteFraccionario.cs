using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsCorteFraccionario
    {
        public int idMoneda { get; set; }
        public int idTipBillMon { get; set; }
        public decimal nValor { get; set; }
        public string cDescripcion { get; set; }
        public int nCantidad { get; set; }
        public decimal nTotal { get; set; }

        public clsCorteFraccionario()
        {
            idMoneda = 0;
            idTipBillMon = 0;
            nValor = 0;
            cDescripcion = string.Empty;
            nCantidad = 0;
            nTotal = 0;
        }
        public clsCorteFraccionario(DataRow row)
        {
            idMoneda = Convert.ToInt32(row["idMoneda"]);
            idTipBillMon = Convert.ToInt32(row["idTipBillMon"]);
            nValor = Convert.ToDecimal(row["nValor"]);
            cDescripcion = Convert.ToString(row["cDescripcion"]);
            nCantidad = Convert.ToInt32(row["idClasificacion"]);
            nTotal = Convert.ToDecimal(row["idClasificacion"]);

        }
    }
}
