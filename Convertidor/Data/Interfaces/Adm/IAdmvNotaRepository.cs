using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmvNotaRepository
{
    Task<ResultDto<List<ADM_V_NOTAS>>> GetNotaDebitoPagoElectronicoByLote(int codigoLote);
}