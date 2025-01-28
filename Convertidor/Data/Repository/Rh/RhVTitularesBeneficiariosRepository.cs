using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class RhVTitularesBeneficiariosRepository: IRhVTitularesBeneficiariosRepository
    {
		
        private readonly DataContext _context;

        public RhVTitularesBeneficiariosRepository(DataContext context)
        {
            _context = context;
        }
      
   
      
        public async Task<List<RH_V_TITULAR_BENEFICIARIOS>> GetAll()
        {
            try
            {
                var result = await _context.RH_V_TITULAR_BENEFICIARIOS.DefaultIfEmpty()
                    .OrderBy(x=>x.CODIGO_TIPO_NOMINA)
                    .ThenBy(x=>x.CEDULA_TITULAR)
                    .ThenBy(x=>x.ORDER_BY)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RH_V_TITULAR_BENEFICIARIOS>> GetByTipoNomina(int codigoTipoNomina)
        {
            try
            {
                var result = await _context.RH_V_TITULAR_BENEFICIARIOS.DefaultIfEmpty()
                    .Where(x=>x.CODIGO_TIPO_NOMINA==codigoTipoNomina)
                    .OrderBy(x=>x.CEDULA_TITULAR)
                    .ThenBy(x=>x.ORDER_BY)
                    .ToListAsync();

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

