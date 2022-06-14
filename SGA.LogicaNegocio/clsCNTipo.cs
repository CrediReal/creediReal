using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.AccesoDatos;
using SGA.ENTIDADES;

namespace SGA.LogicaNegocio
{
    public class clsCNTipo
    {
        public List<clsTipo> ListarTipos(int id, string cTipo, bool lSoloVig = false)
        {
            try
            {
                return new clsADTipo().ListarTipos(id, cTipo, lSoloVig);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsTipo GetTipoById(int id)
        {
            try
            {
                return ListarTipos(id, string.Empty).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<clsTipo> GetTipoByName(string cTipo)
        {
            try
            {
                return ListarTipos(0, cTipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsDBResp SaveTipo(clsTipo objTipo)
        {
            try
            {
                return new clsADTipo().SaveTipo(objTipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
