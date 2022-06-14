using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.AccesoDatos;
using SGA.ENTIDADES;

namespace SGA.LogicaNegocio
{
    public class clsCNVenta
    {
        public List<clsVenta> GetVentas(int idOficina)
        {
            try
            {
                return new clsADVenta().GetVentas(idOficina);
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
                return new clsADVenta().GetDetalleVentas(idVenta);
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
                return new clsADVenta().SaveVentas(objVenta, idUsuReg);
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
                return new clsADVenta().RptVentasCliente(idCli);
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
                return new clsADVenta().RptVentasClienteIndicador(idCli);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptVentasAsesor(string cXmlAsesor,string cXmlOficina,int nAnio)
        {
            try
            {
                return new clsADVenta().RptVentasAsesor(cXmlAsesor, cXmlOficina, nAnio);
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
                return new clsADVenta().RptVentasSucursal(cxmlmeses,cxmlcategoria, nAnio);
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
                return new clsADVenta().RptVentasPorcliente(idCli);
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
                return new clsADVenta().ListaTrabajadoresData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
