using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNViaje
    {
        clsADViaje adviaje = new clsADViaje();

        public DataTable ListarViaje()
        {
            try
            {
                return adviaje.ListarViaje();
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
                return adviaje.ListarViajeid(idViaje);
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
                return adviaje.ListarViajeNroViaje(nNumViaje);
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
                return adviaje.InsertarViaje(nNumViaje, idEstado, idProveedor, idCliente, cDestino, idUsuReg, dFechaReg, lVigente);
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
                return adviaje.ActualizarViaje(idViaje, nNumViaje, idEstado, idProveedor, idCliente, cDestino, idUsuMod, dFechaMod, lVigente);
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
                return adviaje.EliminarViaje(idViaje);
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
                return adviaje.ListarEstadoViaje();
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
                return adviaje.RptViajes(dFecIni, dFecfin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
