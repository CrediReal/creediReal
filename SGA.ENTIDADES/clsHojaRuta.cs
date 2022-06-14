using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsHojaRuta
    {
        public int idHojaRuta { get; set; }
        public int idAsesor { get; set; }
        public int idOficina { get; set; }
        public string cNomOficina { get; set; }
        public DateTime dFecIni { get; set; }
        public DateTime dFecFin { get; set; }
        public List<clsDetHojaRuta> DetalleHojaRuta { get; set; }

        public clsHojaRuta()
        {
            idHojaRuta = 0;
            idAsesor = 0;
            idOficina = 0;
            cNomOficina = string.Empty;
            dFecIni = DateTime.Now.Date;
            dFecFin = DateTime.Now.Date;
            DetalleHojaRuta = new List<clsDetHojaRuta>();
        }

        public clsHojaRuta(DataRow row)
        {
            idHojaRuta = Convert.ToInt32(row["idHojaRuta"]);
            idAsesor = Convert.ToInt32(row["idAsesor"]);
            idOficina = Convert.ToInt32(row["idOficina"]);
            cNomOficina = Convert.ToString(row["cNomOficina"]);
            dFecIni = Convert.ToDateTime(row["dFecIni"]);
            dFecFin = Convert.ToDateTime(row["dFecFin"]);
        }
    }
}
