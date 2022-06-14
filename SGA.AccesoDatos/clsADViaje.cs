using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.AccesoDatos
{
    public class clsADViaje
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarViaje()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarViaje_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarViajeid(int idViaje)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarViajeidViaje_SP", idViaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarViajeNroViaje(int nNumViaje)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarViajeNroViaje_SP", nNumViaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarViaje(int nNumViaje, int idEstado, int idProveedor, int idCliente, string cDestino, int idUsuReg, DateTime dFechaReg, bool lVigente)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsertarViaje_SP",nNumViaje, idEstado, idProveedor, idCliente, cDestino, idUsuReg, dFechaReg, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarViaje(int idViaje, int nNumViaje, int idEstado, int idProveedor, int idCliente, string cDestino, int idUsuMod, DateTime dFechaMod, bool lVigente)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ActualizarViaje_SP", idViaje, nNumViaje, idEstado, idProveedor, idCliente, cDestino, idUsuMod, dFechaMod, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EliminarViaje(int idViaje)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_EliminarViaje_SP", idViaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarEstadoViaje()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarEstadoViaje_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptViajes(DateTime dFecIni, DateTime dFecfin)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_RptViajes_SP", dFecIni,  dFecfin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
