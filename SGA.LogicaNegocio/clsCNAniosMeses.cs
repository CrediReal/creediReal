using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.AccesoDatos;

namespace SGA.LogicaNegocio
{
    public class clsCNAniosMeses
    {
        public DataTable GetMeses()
        {
            return new clsADAniosMeses().GetMeses();
        }

        public DataTable GetAnios(int nIni, int nFin)
        {
            return new clsADAniosMeses().GetAnios(nIni, nFin);
        }
    }
}
