using System;
using System.Collections.Generic;
using System.Text;
using SGA.AccesoDatos;
using System.Data;
using SGA.ENTIDADES;

namespace SGA.LogicaNegocio
{
    public class clsCNUsuario
    {
        clsADUsuario objUsuario = new clsADUsuario();

        public DataTable InsUpdUsuario(string xmlUsuario)
        {
            try
            {
                return objUsuario.InsUpdUsuario(xmlUsuario);
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
                return objUsuario.ExisteDNI(DNI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public string crearLoginUsuario(string cNombre, string cApellidoPaterno, string cDNI)
        {
            string cLogin = "";
            cLogin = cNombre.Trim() +"."+
                     cApellidoPaterno.Trim().Replace(" ", "").Replace("  ", "");// +
                     //cDNI.Trim().Substring(cDNI.Trim().Length - 4, 4);

            return cLogin.ToLower().Replace("ñ","n");
        }

        public DataTable ListarDatosUsuario(string cUsuario)
        {
            try
            {
                return objUsuario.ListarDatosUsuario(cUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// CAMBIO DE PASSWORD
        /// </summary>
        /// <param name="cUsuario"></param>
        /// <param name="cClaveAnterior"></param>
        /// <param name="cNuevoPassord"></param>
        /// <returns></returns>
        public DataTable ActualizarNuevoPassword(string cUsuario,  string cNuevoPassord)
        {
            try
            {
                return objUsuario.ActualizarNuevoPassword(cUsuario, cNuevoPassord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Buscar un usuario de Acuerdo a su Apellidos
        /// </summary>
        /// <param name="cApellidos"></param>
        /// <returns></returns>
        public DataTable BuscarUsuarioPorApellido(string cApellidos)
        {
            try
            {
                return objUsuario.BuscarUsuarioPorApellido(cApellidos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Reinicia la clave del Usuario por defecto
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="cPassXDefecto"></param>
        /// <returns></returns>
        public DataTable ReiniciarClaveXDefecto(int idUsuario, string cPassXDefecto, int idUsuarioReg, string cNombrePc, string cMacPc)
        {
            try
            {
                return objUsuario.ReiniciarClaveXDefecto(idUsuario, cPassXDefecto, idUsuarioReg, cNombrePc, cMacPc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene los perfiles de un Usuario descuerdo a su ID
        /// </summary>
        /// <param name="idUsuario">Identificador de Usuario</param>
        /// <returns></returns>
        public DataTable ObtenerPerfilesUsuarioPorId(int idUsuario)
        {
            try
            {
                return objUsuario.ObtenerPerfilesUsuarioPorId(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Lista todos los usuario que tengan un perfil determinado
        public DataTable ListarUsuariosXPerfil(int idPerfil)
        {
            try
            {
                return objUsuario.ListarUsuariosXPerfil(idPerfil);
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
                return objUsuario.BuscarUsuarioPorDNI(DNI);
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
                return objUsuario.ListarUsuarios();
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
                return objUsuario.ListaDatosUsuarioXId(idUsuario);
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
                return objUsuario.ListarUsuarioxPerfil(idPerfil);
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
                return objUsuario.ListarUsuariosOficinaPerfil(idOficina, cPerfiles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsUsuario GetDatosUsuario(string cUsuario)
        {
            try
            {
                var dtDatUsuario= objUsuario.ListarDatosUsuario(cUsuario);

                clsUsuario ObjetoUsuario = new clsUsuario();
                ObjetoUsuario.idCli = Convert.ToInt32(dtDatUsuario.Rows[0]["idCli"]);
                ObjetoUsuario.idUsuario = Convert.ToInt32(dtDatUsuario.Rows[0]["idUsuario"]);
                ObjetoUsuario.cUsuario = Convert.ToString(dtDatUsuario.Rows[0]["cWinUser"]);
                ObjetoUsuario.cNombres = Convert.ToString(dtDatUsuario.Rows[0]["cNombre"]) + " " + Convert.ToString(dtDatUsuario.Rows[0]["cApellidoPaterno"]) + " " + Convert.ToString(dtDatUsuario.Rows[0]["cApellidoMaterno"]);
                ObjetoUsuario.cApellidoPaterno = Convert.ToString(dtDatUsuario.Rows[0]["cApellidoPaterno"]);
                ObjetoUsuario.cApellidoMaterno = Convert.ToString(dtDatUsuario.Rows[0]["cApellidoMaterno"]);
                ObjetoUsuario.cNombre = Convert.ToString(dtDatUsuario.Rows[0]["cNombre"]);
                ObjetoUsuario.cWinuser = Convert.ToString(dtDatUsuario.Rows[0]["cWinUser"]);
                ObjetoUsuario.cDNI = Convert.ToString(dtDatUsuario.Rows[0]["cDocumentoID"]);
                ObjetoUsuario.idSexo = Convert.ToInt32(dtDatUsuario.Rows[0]["idSexo"]);
                ObjetoUsuario.dFecSystem = Convert.ToDateTime(dtDatUsuario.Rows[0]["dFecSystem"]);
                ObjetoUsuario.nIdAgencia = Convert.ToInt32(dtDatUsuario.Rows[0]["idAgencia"]);
                ObjetoUsuario.lCambioClave = Convert.ToBoolean(dtDatUsuario.Rows[0]["lCambioClave"]);
                ObjetoUsuario.idEstado = Convert.ToInt32(dtDatUsuario.Rows[0]["idEstado"]);
                ObjetoUsuario.idCargo = Convert.ToInt32(dtDatUsuario.Rows[0]["idCargo"]);
                ObjetoUsuario.dFechaIngreso = Convert.ToDateTime(dtDatUsuario.Rows[0]["dFechaIngreso"]);
                ObjetoUsuario.dFechaCese = Convert.ToDateTime(dtDatUsuario.Rows[0]["dFechaCese"]);
                ObjetoUsuario.cNombreAge = Convert.ToString(dtDatUsuario.Rows[0]["cNombreAge"]);

                return ObjetoUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
