using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmvNotaRepository:IAdmvNotaRepository
    {
        private readonly DataContextAdm _context;

        public AdmvNotaRepository(DataContextAdm context)
        {
            _context = context;
        }


        public async Task<ResultDto<List<ADM_V_NOTAS>>> GetNotaDebitoPagoElectronicoByLote(int codigoLote)
        {
            ResultDto<List<ADM_V_NOTAS>> result = new ResultDto<List<ADM_V_NOTAS>>(null);

            try
            {
                var tiposCheque = new List<int> { 573, 818, 834 }; // Lista de valores permitidos
                var data = await _context.ADM_V_NOTAS
                    .Where(x => x.CODIGO_LOTE_PAGO == codigoLote && tiposCheque.Contains((int)x.TIPO_PAGO_ID))
                    .ToListAsync();

                result.CantidadRegistros = data.Count;
                result.TotalPage = 1;
                result.Page = 1;
                result.IsValid = true;
                result.Message = "";
                result.Data = data;
                return result;
            }
            catch (Exception ex)
            {
                result.CantidadRegistros = 0;
                result.IsValid = false;
                result.Message = ex.Message;
                result.Data = null;
                return result;
            }
        }
    }
}