using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Pagos;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmNotaDebitoPagoElectronicService : IAdmNotaDebitoPagoElectronicService
    {
        private readonly IAdmvNotaRepository _repository;
      
        public AdmNotaDebitoPagoElectronicService(  IAdmvNotaRepository repository)
        {
            _repository = repository;
           
        }


        public async Task<ResultDto<List<ADM_V_NOTAS>>> GetNotaDebitoPagoElectronicoByLote(int codigoLote)
        {
          var data = await _repository.GetNotaDebitoPagoElectronicoByLote(codigoLote);
          return data;
        }
        
    }
 }

