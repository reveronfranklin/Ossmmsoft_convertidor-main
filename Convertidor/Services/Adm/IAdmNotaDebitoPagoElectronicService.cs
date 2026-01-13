using Convertidor.Data.Entities.Adm;

namespace Convertidor.Services.Adm;

public interface IAdmNotaDebitoPagoElectronicService
{
    Task<ResultDto<List<ADM_V_NOTAS>>> GetNotaDebitoPagoElectronicoByLote(int codigoLote);
}