using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntRelacionDocumentosRepository : ICntRelacionDocumentosRepository
    {
        private readonly DataContextCnt _context;

        public CntRelacionDocumentosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_RELACION_DOCUMENTOS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_RELACION_DOCUMENTOS.DefaultIfEmpty().ToListAsync();
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
