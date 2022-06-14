using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SGA.LogicaNegocio
{
    public class clsCNCredito
    {
        clsADCredito adcredito = new clsADCredito();
        public DataTable CNAsignaMoraManual(int nNumCredito, decimal nMora)
        {
            try
            {
                return adcredito.ADAsignaMoraManual(nNumCredito, nMora);
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
                return adcredito.BuscarSolicitud(cNombre);
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
                return adcredito.ActualizarSolicitud(idSolicitud, nCapitalSolicitado, nPlazoCuota, nCuotas, nTasaCompensatoria, idEstado);
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
                return adcredito.rptConsolidado(dFecha);
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
                return adcredito.rptConsolidado2(nDiaAtr, idUsuario);
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
                return adcredito.rptResumenCart();
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
                return adcredito.RegPerdonMora(idCuenta, nMonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
