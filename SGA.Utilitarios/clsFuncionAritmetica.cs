using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGA.Utilitarios
{
    public class clsFuncionAritmetica
    {
        public double RedxExceso(double nNum, int nNroDec)
        {
            try
            {
                double nAux;
                nAux = nNum * (double)Math.Pow(10, nNroDec);
                nAux = Math.Ceiling(nAux) / (double)Math.Pow(10, nNroDec);
                return nAux;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public double RedxDefecto(double nNum, int nNroDec)
        {
            try
            {
                double nAux;
                nAux = nNum * (double)Math.Pow(10, nNroDec);
                if (nAux < 0)
                    nAux -= 0.5;
                else
                    nAux += 0.5;
                nAux = Math.Truncate(nAux);
                nAux = nAux / (double)Math.Pow(10, nNroDec);
                return nAux;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
