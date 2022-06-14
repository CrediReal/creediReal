using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SGA.AccesoDatos
{
    public class clsADCronograma
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();
        public DataTable ADdtFeriado()
        {
            try
            {
                return objEjeSp.EjecSP("GEN_ListaFeriados_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable RegistrarCronogramaCanasta(String xCronograma, DateTime dFecdesemb, int idUsuario, decimal nMonto, int nCuotas, decimal nTasa)
        {
            return objEjeSp.EjecSP("CRE_RegistrarCanasta_SP", xCronograma, dFecdesemb, idUsuario, nMonto, nCuotas, nTasa);
        }

        public DataTable BuscarCanasta(int idCli, int nPeriodo)
        {
            return objEjeSp.EjecSP("CRE_BuscarCanasta_SP", idCli, nPeriodo);
        }

        public DataTable CronogramaCanasta(int idCli, int nPeriodo)
        {
            return objEjeSp.EjecSP("CRE_CronogramaCanasta_SP", idCli, nPeriodo);
        }
    }
}
