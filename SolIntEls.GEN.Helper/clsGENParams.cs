using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Conector
{
    public class clsGENParams
    {
        public int nPosicion { get; set; }
        public string Tipodatos { get; set; }
        public SqlParameter Parametro { get; set; }
    }
}
