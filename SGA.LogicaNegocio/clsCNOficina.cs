using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.AccesoDatos;
using SGA.ENTIDADES;

namespace SGA.LogicaNegocio
{
    public class clsCNOficina
    {
        public List<clsOficina> ListarOficinas(int id, bool lSoloVig = false)
        {
            try
            {
                return new clsADOficina().ListarOficinas(id, lSoloVig);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsOficina GetOficinaById(int id)
        {
            try
            {
                return ListarOficinas(id).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsDBResp SaveOficina(clsOficina objOficina)
        {
            try
            {
                return new clsADOficina().SaveOficina(objOficina);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
