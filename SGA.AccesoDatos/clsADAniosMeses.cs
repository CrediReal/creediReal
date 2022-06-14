using System.Data;
using Helper.Conector;

namespace SGA.AccesoDatos
{
    public class clsADAniosMeses
    {
        public DataTable GetMeses()
        {
            return new clsGENEjeSP().EjecSP("SGA_ListarMeses_Sp");
        }

        public DataTable GetAnios(int nIni, int nFin)
        {
            return new clsGENEjeSP().EjecSP("SGA_ListarAnios_Sp", nIni, nFin);
        }
    }
}
