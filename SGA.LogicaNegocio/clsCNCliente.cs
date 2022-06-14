using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNCliente
    {
        clsADCliente adcliente = new clsADCliente();

        public DataTable ListarCliente()
        {
            try
            {
                return adcliente.ListarCliente();
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
                return adcliente.ListarClienteid(idCliente);
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
                return adcliente.InsertarCliente(cNombres, idTipoPersona, idTipoDocumento, cDocumento, cDocumentoAdicional, cNombre,
            cApellidoPaterno, cApellidoMaterno, dFechaNac, cTelefono, cCorreo, cDireccion, idUsuReg, dFechaReg, lVigente,
            cCorreoAdi, cDireccionAdi, cUbigeo, cUbigeoAdi, cTelefonoAdi, idAsesor, cContacto, idOficina, cCargoContacto, idTipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarCliente(int idCliente, string cNombres, int idTipoPersona, int idTipoDocumento, string cDocumento, string cDocumentoAdicional, string cNombre,
            string cApellidoPaterno, string cApellidoMaterno, DateTime dFechaNac, string cTelefono, string cCorreo, string cDireccion, int idUsuMod, DateTime dFechaMod, bool lVigente,
            string cCorreoAdi, string cDireccionAdi, string cUbigeo, string cUbigeoAdi, string cTelefonoAdi, int idAsesor, string cContacto, int idOficina, string cCargoContacto, int idTipo)
        {
            try
            {
                return adcliente.ActualizarCliente(idCliente, cNombres, idTipoPersona, idTipoDocumento, cDocumento, cDocumentoAdicional, cNombre,
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
                return adcliente.EliminarCliente(idCliente);
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
                return adcliente.BuscarCliente(idTipoBusqueda, cValBusqueda);
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
                return new clsADCliente().GetClientesUbigeo(cCodUbigeo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class clsCNPersona
    {
        clsADPersona adpersona = new clsADPersona();

        public DataTable ListarTipoPersona()
        {
            try
            {
                return adpersona.ListarTipoPersona();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
