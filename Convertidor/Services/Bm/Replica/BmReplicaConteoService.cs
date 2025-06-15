using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Services.Bm.Replica;

namespace Convertidor.Services.Bm
{
    public class BmReplicaConteoService: IBmReplicaConteoService
    {

      
        private readonly IBmReplicaRepository _repository;
      
        public BmReplicaConteoService(IBmReplicaRepository repository
                                      )
		{
            _repository = repository;
          
           

        }

    

        public async Task<ResultDto<Boolean>> ReplicarConteo()
        {

            ResultDto<Boolean> result = new ResultDto<Boolean>(false);
            try
            {
              
                await _repository.ReplicarArticulos();
                await _repository.ReplicarBienes();
                await _repository.ReplicarMovimientosBienes();
                await _repository.ReplicarDireccionesBienes();
                await _repository.ReplicarClasificacionesBienes();
                await _repository.ReplicarPersonas();
                result.Data = true;
                result.IsValid = true;
                result.Message = "Conteo Replicado";

            }
            catch (Exception ex)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }


    }
}

