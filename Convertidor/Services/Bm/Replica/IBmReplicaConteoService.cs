using Convertidor.Dtos.Bm;


namespace Convertidor.Services.Bm.Replica
{
	public interface IBmReplicaConteoService
	{

        Task<ResultDto<Boolean>> ReplicarConteo();
      


    }
}
