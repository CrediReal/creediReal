using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    [Serializable]
    public class clsDetHojaRuta
    {
        public int idDetHojaRuta { get; set; }
        public int idInterno { get; set; }
        public int idCli { get; set; }
        public string cNombres { get; set; }
        public string cDocumento { get; set; }
        public string cDireccion { get; set; }
        public int idTipoVisita { get; set; }
        public DateTime? dFecVisita { get; set; }
        public int? idTipoContacto { get; set; }
        public string cComentario { get; set; }
        public DateTime? dFecHoraProxVisita { get; set; }

        public bool ShouldSerializedFecVisita()
        {
            return dFecVisita != null;
        }

        public bool ShouldSerializeidTipoContacto()
        {
            return idTipoContacto != null;
        }

        public bool ShouldSerializedFecHoraProxVisita()
        {
            return dFecHoraProxVisita != null;
        }


        public clsDetHojaRuta()
        {
            idDetHojaRuta = 0;
            idInterno = 0;
            idCli = 0;
            idTipoVisita = 1;
            dFecVisita = null;
            idTipoContacto = null;
            cComentario = string.Empty;
        }

        public clsDetHojaRuta(DataRow row)
        {
            idDetHojaRuta = Convert.ToInt32(row["idDetHojaRuta"]);
            idCli = Convert.ToInt32(row["idCli"]);
            cNombres = Convert.ToString(row["cNombres"]);
            cDocumento = Convert.ToString(row["cDocumento"]);
            cDireccion = Convert.ToString(row["cDireccion"]);
            idTipoVisita = Convert.ToInt32(row["idTipoVisita"]);
            dFecVisita = row["dFecVisita"] == DBNull.Value ? null : (DateTime?)row["dFecVisita"];
            idTipoContacto = row["idTipoContacto"] == DBNull.Value ? null : (int?)row["idTipoContacto"];
            cComentario = Convert.ToString(row["cComentario"]);
            dFecHoraProxVisita = row["dFecHoraProxVisita"] == DBNull.Value ? null : (DateTime?)row["dFecHoraProxVisita"];
        }
    }
}
