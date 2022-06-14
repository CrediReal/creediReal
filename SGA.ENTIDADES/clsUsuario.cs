using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    public class clsUsuario
    {
        public int idUsuario { get; set; }
        public string cUsuario { get; set; }
        public string cNombres { get; set; }
        public string cApellidoPaterno { get; set; }
        public string cApellidoMaterno { get; set; }
        public string cNombre { get; set; }
        public string cNombreSeg { get; set; }
        public string cDNI { get; set; }
        public int idSexo { get; set; }
        public int idPerfil { get; set; }
        public string cPerfil { get; set; }
        public string cNamePc { get; set; }
        public string cMacPc { get; set; }
        public string cWinuser { get; set; }
        public int nIdAgencia { get; set; }
        public DateTime dFecSystem { get; set; }
        public int idCli { get; set; }
        public bool lCambioClave { get; set; }
        public string cNombreAge { get; set; }
        public int idCargo { get; set; }
        public int idEstado { get; set; }
        public DateTime dFechaIngreso { get; set; }
        public DateTime dFechaCese { get; set; }
    }
}
