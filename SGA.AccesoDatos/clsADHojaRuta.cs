using SGA.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Conector;
using System.Data;
using SGA.Utilitarios;

namespace SGA.AccesoDatos
{
    public class clsADHojaRuta
    {
        public List<clsHojaRuta> GetHojaRutas(int idAsesor)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarHojaRutaUsu_Sp", idAsesor);
                return (from DataRow row in dtResult.Rows select new clsHojaRuta(row)).ToList();
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
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarDetHojaRuta_Sp", idHojaRuta);
                return (from DataRow row in dtResult.Rows select new clsDetHojaRuta(row)).ToList();
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
                string xmlHojaRuta = ObjHojaRuta.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveHojaRuta_Sp", xmlHojaRuta, idUsuReg);
                return new clsDBResp(dtResult);
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
                return new clsGENEjeSP().EjecSP("SGA_RptVisitasOficina_Sp", idOficina);
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
                return new clsGENEjeSP().EjecSP("SGA_RptVisitasUsuario_Sp", idUsuario);
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
                return new clsGENEjeSP().EjecSP("SGA_RptVisitasUbigeo_Sp", cUbigeo);
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
                return new clsGENEjeSP().EjecSP("SGA_RptVisitasFecha_Sp", dFecIni,dFecFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
