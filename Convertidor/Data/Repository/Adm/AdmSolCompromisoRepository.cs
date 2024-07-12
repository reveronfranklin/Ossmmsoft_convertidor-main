using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmSolCompromisoRepository : IAdmSolCompromisoRepository
    {
        private readonly DataContextAdm _contex;

        public AdmSolCompromisoRepository(DataContextAdm contex)
        {
            _contex = contex;
        }

        public async Task<List<ADM_SOL_COMPROMISO>> GetAll()
        {
            try
            {
                var result = await _contex.ADM_SOL_COMPROMISO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }


    }
}
