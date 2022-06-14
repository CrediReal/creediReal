using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SGA.AccesoDatos
{
    public class clsADCredito
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();
        public DataTable ADAsignaMoraManual(int nNumCredito, decimal nMora)
        {
            try
            {
                return objEjeSp.EjecSP("CRE_AsignaMoraManual_SP", nNumCredito, nMora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BuscarSolicitud(string cNombre)
        {
            try
            {
                return objEjeSp.EjecSP("BuscarSolicitud", cNombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarSolicitud(int idSolicitud, decimal nCapitalSolicitado, int nPlazoCuota, int nCuotas, decimal nTasaCompensatoria, int idEstado)
        {
            try
            {
                return objEjeSp.EjecSP("ActualizarSolicitud", idSolicitud, nCapitalSolicitado, nPlazoCuota, nCuotas, nTasaCompensatoria, idEstado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable rptConsolidado(DateTime dFecha)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ReporteConsolidaddo_SP", dFecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable rptConsolidado2(int nDiaAtr, int idUsuario)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ReporteConsolidado2_SP", nDiaAtr, idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable rptResumenCart()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ResumenMora_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RegPerdonMora(int idCuenta, decimal nMonto)
        {
            try
            {
                return objEjeSp.EjecSP("FAST_RegPerdonMora_sp", idCuenta, nMonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
