using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhAriRepository
    {
        
        Task<List<RH_ARI>> GetByCodigoPersona(int codigoPersona);
        Task<RH_ARI> GetByCodigo(int codigoAri);
        Task<ResultDto<RH_ARI>> Add(RH_ARI entity);
        Task<ResultDto<RH_ARI>> Update(RH_ARI entity);
        Task<string> Delete(int codigoAri);
        Task<int> GetNextKey();
	}
}

