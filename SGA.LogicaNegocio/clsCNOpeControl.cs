using System;
using System.Collections.Generic;
using System.Text;
//using SGA.AccesoDatos;
using System.Data;
using CAJ.AccesoDatos;

namespace SGA.LogicaNegocio
{
    public class clsCNOpeControl
    {

        clsADControlOpe objSaldos = new clsADControlOpe();

        public string RetMontoCorFracc(DateTime dFecSis, int nidUsuario, int cCodAge, ref double nMonCorSoles, ref double nMonCorDolar)
        {
            string msg;
            try
            {
                DataTable tbMonCorFra = objSaldos.RetMontoCorteFrac(dFecSis, nidUsuario, cCodAge);
                if (tbMonCorFra.Rows.Count > 0)
                {
                    nMonCorSoles = Convert.ToDouble(tbMonCorFra.Rows[0]["nTotal"].ToString());
                    //nMonCorDolar = Convert.ToDouble(tbMonCorFra.Rows[1]["nTotal"].ToString());
                }
                else
                {
                    nMonCorSoles = 0.00;
                    //nMonCorDolar = 0.00;
                }
                msg = "OK";
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }

    }
}
