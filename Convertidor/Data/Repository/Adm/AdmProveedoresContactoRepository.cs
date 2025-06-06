using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmAdmProveedoresContactoRepository:IAdmAdmProveedoresContactoRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmAdmProveedoresContactoRepository(DataContextAdm context)
        {
            _context = context;
        }
      
        public async Task<List<ADM_V_PAGAR_A_LA_OP_TERCEROS>> GetAll()
        {
            try
            {
                var result = await _context.ADM_V_PAGAR_A_LA_OP_TERCEROS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        


    }
}

