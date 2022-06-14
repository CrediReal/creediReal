using SGA.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.AccesoDatos;

namespace SGA.LogicaNegocio
{
    public class clsCNHojaRuta
    {
        public List<clsHojaRuta> GetHojaRutas(int idAsesor)
        {
            try
            {
                return new clsADHojaRuta().GetHojaRutas(idAsesor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<clsDetHojaRuta> GetDetalleHojaRuta(int idHojaRuta)
        {
            try
            {
                return new clsADHojaRuta().GetDetalleHojaRuta(idHojaRuta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsDBResp SaveHojaRutas(clsHojaRuta ObjHojaRuta, int idUsuReg)
        {
            try
            {
                return new clsADHojaRuta().SaveHojaRutas(ObjHojaRuta,idUsuReg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptVisitasOficina(int idOficina)
        {
            try
            {
                return new clsADHojaRuta().RptVisitasOficina(idOficina);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptVisitasUsuario(int idUsuario)
        {
            try
            {
                return new clsADHojaRuta().RptVisitasUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptVisitasUbigeo(string cUbigeo)
        {
            try
            {
                return new clsADHojaRuta().RptVisitasUbigeo(cUbigeo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptVisitasFecha(DateTime dFecIni, DateTime dFecFin)
        {
            try
            {
                return new clsADHojaRuta().RptVisitasFecha(dFecIni, dFecFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
