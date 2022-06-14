using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.ENTIDADES;
using SGA.Utilitarios;
using Helper.Conector;

namespace SGA.AccesoDatos
{
    public class clsADVenta
    {
        public List<clsVenta> GetVentas(int idOficina)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarVentas_Sp", idOficina);
                return (from DataRow row in dtResult.Rows select new clsVenta(row)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<clsDetVenta> GetDetalleVentas(int idVenta)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarDetVenta_Sp", idVenta);
                return (from DataRow row in dtResult.Rows select new clsDetVenta(row)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public clsDBResp SaveVentas(clsVenta objVenta, int idUsuReg)
        {
            try
            {
                string xmlVenta = objVenta.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveVenta_Sp", xmlVenta, idUsuReg);
                return new clsDBResp(dtResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptVentasCliente(int idCli)
        {
            try
            {
                return new clsGENEjeSP().EjecSP("SGA_RptVentaCliente_Sp", idCli);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptVentasClienteIndicador(int idCli)
        {
            try
            {
                return new clsGENEjeSP().EjecSP("SGA_RptVentaClienteIndicadores_Sp", idCli);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptVentasAsesor(string cXmlAsesor, string cXmlOficina, int nAnio)
        {
            try
            {
                return new clsGENEjeSP().EjecSP("SGA_RptVentasAsesor_SP",cXmlAsesor,cXmlOficina, nAnio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptVentasSucursal(string cxmlmeses, string cxmlcategoria, int nAnio)
        {
            try
            {
                return new clsGENEjeSP().EjecSP("SGA_RptVentasSucursal_SP", cxmlmeses,cxmlcategoria, nAnio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptVentasPorcliente(int idCli)
        {
            try
            {
                return new clsGENEjeSP().EjecSP("SGA_RptVentasPorcliente_SP", idCli);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListaTrabajadoresData()
        {
            try
            {
                return new clsGENEjeSP().EjecSP("SGA_ListaTrabajadoresData_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
    }
}
