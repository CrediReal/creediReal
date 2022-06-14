using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.AccesoDatos
{
    public class clsADUsuario
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable InsUpdUsuario(string xmlUsuario)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsUpdUsuario_SP", xmlUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean ExisteDNI(string DNI)
        {
            try
            {
                DataTable dtRespuesta = objEjeSp.EjecSP("SGA_BuscarDNI_SP", DNI);
                if (dtRespuesta.Rows.Count > 0)//Existe el DNI
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       

        public DataTable ListarDatosUsuario(string cUsuario)
        {
            try
            {
                //return objEjeSp.EjecSP("SGA_ListaDatosUsuario_SP", cUsuario);
                return objEjeSp.EjecSP("Gen_BuscaPersonalUser_sp", cUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public DataTable ActualizarNuevoPassword(string cUsuario, string cNuevoPassord)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ActualizarNuevoPassword_SP", cUsuario,  cNuevoPassord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BuscarUsuarioPorApellido(string cApellidos)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_BuscarUsuarioPorApellido_SP", cApellidos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ReiniciarClaveXDefecto(int idUsuario, string cPassXDefecto, int idUsuarioReg, string cNombrePc, string cMacPc)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ReiniciarClaveXDefecto_SP", idUsuario, cPassXDefecto, idUsuarioReg, cNombrePc, cMacPc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerPerfilesUsuarioPorId(int idUsuario)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ObtenerPerfilesUsuarioPorId_SP", idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarUsuariosXPerfil(int idPerfil)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarUsuariosXPerfil_SP", idPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public DataTable ListarUsuariosRelacionadosOnoAProyecto(int idProyecto)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarUsuariosRelacionadosOnoAProyecto_SP", idProyecto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListarObservadoresXProyecto(int idProyecto)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarObservadoresXProyecto_SP", idProyecto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable InsUpdVincularUsuarioProyecto(string xmlNuevosUsuariosVinculados, string xmlUsuariosDesvinculados, int idProyecto, int idCoordinador)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsUpdVincularUsuarioProyecto_SP", xmlNuevosUsuariosVinculados, xmlUsuariosDesvinculados, idProyecto, idCoordinador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable IdentificarProyectoAlQueEstaVinculado(int idUsuario)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_IdentificarProyectoAlQueEstaVinculadoAdministrador_SP", idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarObservadoresRelacionadosOnoACoordinador(int idUsuarioCoordinador, int idProyecto)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarObservadoresRelacionadosOnoACoordinador_SP", idUsuarioCoordinador, idProyecto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BuscarUsuarioPorDNI(string DNI)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_BuscarUsuarioPorDNI_SP", DNI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarUsuarios()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarUsuarios_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListaDatosUsuarioXId(int idUsuario)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListaDatosUsuarioXId_SP", idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarUsuarioxPerfil(int idPerfil)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarUsuarioxPerfil_SP", idPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarUsuariosOficinaPerfil(int idOficina, string cPerfiles)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarUsuariosByOficina_Sp", idOficina, cPerfiles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
