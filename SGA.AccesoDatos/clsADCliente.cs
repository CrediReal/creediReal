using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.AccesoDatos
{
    public class clsADCliente 
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarCliente()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarCliente_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarClienteid(int idCliente)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarClienteidCliente_SP", idCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarCliente(string cNombres, int idTipoPersona, int idTipoDocumento, string cDocumento, string cDocumentoAdicional, string cNombre,
            string cApellidoPaterno, string cApellidoMaterno, DateTime dFechaNac, string cTelefono, string cCorreo, string cDireccion, int idUsuReg, DateTime dFechaReg, bool lVigente,
            string cCorreoAdi, string cDireccionAdi, string cUbigeo, string cUbigeoAdi, string cTelefonoAdi, int idAsesor, string cContacto, int idOficina, string cCargoContacto, int idTipo)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsertarCliente_SP", cNombres, idTipoPersona, idTipoDocumento, cDocumento, cDocumentoAdicional, cNombre,
            cApellidoPaterno, cApellidoMaterno, dFechaNac, cTelefono, cCorreo, cDireccion, idUsuReg, dFechaReg, lVigente,
            cCorreoAdi, cDireccionAdi, cUbigeo, cUbigeoAdi, cTelefonoAdi, idAsesor, cContacto, idOficina, cCargoContacto, idTipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarCliente(int idCliente,string cNombres, int idTipoPersona, int idTipoDocumento, string cDocumento, string cDocumentoAdicional, string cNombre,
            string cApellidoPaterno, string cApellidoMaterno, DateTime dFechaNac, string cTelefono, string cCorreo, string cDireccion, int idUsuMod, DateTime dFechaMod, bool lVigente,
            string cCorreoAdi, string cDireccionAdi, string cUbigeo, string cUbigeoAdi, string cTelefonoAdi, int idAsesor, string cContacto, int idOficina, string cCargoContacto, int idTipo)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ActualizarCliente_SP",idCliente, cNombres, idTipoPersona, idTipoDocumento, cDocumento, cDocumentoAdicional, cNombre,
            cApellidoPaterno, cApellidoMaterno, dFechaNac, cTelefono, cCorreo, cDireccion, idUsuMod, dFechaMod, lVigente,
            cCorreoAdi, cDireccionAdi, cUbigeo, cUbigeoAdi, cTelefonoAdi, idAsesor, cContacto, idOficina, cCargoContacto, idTipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EliminarCliente(int idCliente)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_EliminarCliente_SP", idCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BuscarCliente(int idTipoBusqueda, string cValBusqueda)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_BuscarCliente_SP", idTipoBusqueda, cValBusqueda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetClientesUbigeo(string cCodUbigeo)
        {
            try
            {
                return new clsGENEjeSP().EjecSP("SGA_ListarClientesUbigeo_Sp", cCodUbigeo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

    public class clsADPersona
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarTipoPersona()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarTipoPersona_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
