using SGA.AccesoDatos;
using SGA.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNTipoContacto
    {
        public List<clsTipoContacto> GetTipoContacto(int id, bool lSoloVig = true)
        {
            try
            {
                return new clsADTipoContacto().GetTipoContacto(id, lSoloVig);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
