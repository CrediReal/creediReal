using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGA.ENTIDADES
{
    public class clsDBResp
    {
        public int nMsje { set; get; }
        public string cMsje { set; get; }
        public int idGenerado { set; get; }

        public clsDBResp(int nMsje, string cMsje, int idGenerado)
        {
            this.nMsje = nMsje;
            this.cMsje = cMsje;
            this.idGenerado = idGenerado;
        }

        public clsDBResp(int nMsje, string cMsje) : this(nMsje, cMsje, 0) { }

        public clsDBResp() : this(0, string.Empty, 0) { }

        public clsDBResp(DataTable dtResult) : this()
        {
            foreach (DataRow row in dtResult.Rows)
            {
                if (dtResult.Columns.Contains("nMsje"))
                {
                    this.nMsje = Convert.ToInt16(row["nMsje"]);
                }
                if (dtResult.Columns.Contains("cMsje"))
                {
                    this.cMsje = Convert.ToString(row["cMsje"]);
                }
                else
                {
                    this.cMsje = "No se retorno ningun mensaje del procedimiento. " + 
                                "Revise el retorno del procedimiento y los alias de los mensajes";
                }
                if (dtResult.Columns.Contains("idGenerado"))
                {
                    this.idGenerado = Convert.ToInt32(row["idGenerado"]);
                }
            }

        }

    }
}
