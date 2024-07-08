using Convertidor.Data.Entities;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntComprobantesRepository : ICntComprobantesRepository
    {
        private readonly DataContextCnt _context;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CntComprobantesRepository(DataContextCnt context,
                                         ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<List<CNT_COMPROBANTES>> GetAll()
        {
            try
            {
                var result = await _context.CNT_COMPROBANTES.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_COMPROBANTES> GetByNumeroComprobante(string numeroComprobante)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.CNT_COMPROBANTES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_EMPRESA == conectado.Empresa && e.NUMERO_COMPROBANTE == numeroComprobante)
                    .FirstOrDefaultAsync();

                return (CNT_COMPROBANTES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ResultDto<CNT_COMPROBANTES>> Add(CNT_COMPROBANTES entity)
        {

            ResultDto<CNT_COMPROBANTES> result = new ResultDto<CNT_COMPROBANTES>(null);
            try
            {
                await _context.CNT_COMPROBANTES.AddAsync(entity);
                await _context.SaveChangesAsync();


                result.Data = entity;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.CNT_COMPROBANTES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_COMPROBANTE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_COMPROBANTE + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

       
    }
}
