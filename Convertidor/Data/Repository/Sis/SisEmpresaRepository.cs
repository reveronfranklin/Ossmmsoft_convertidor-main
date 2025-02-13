using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class SisEmpresaRepository: ISisEmpresaRepository
    {
		
        private readonly DataContextSis _context;

        public SisEmpresaRepository(DataContextSis context)
        {
            _context = context;
        }
      
        public async Task<SIS_EMPRESAS> GetByCodigo(int codigoEmpresa)
        {
            try
            {
                var result = await _context.SIS_EMPRESAS.DefaultIfEmpty().Where(e => e.CODIGO_EMPRESA == codigoEmpresa).FirstOrDefaultAsync();

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

