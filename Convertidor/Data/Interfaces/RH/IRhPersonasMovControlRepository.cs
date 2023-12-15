using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Data.Repository.Rh
{
	public interface IRhPersonasMovControlRepository
	{

        Task<List<RH_PERSONAS_MOV_CONTROL>> GetAll();


        Task<RH_PERSONAS_MOV_CONTROL> GetByCodigo(int codigoPersonaMoV);

        Task<List<RH_PERSONAS_MOV_CONTROL>> GetCodigoPersona(int codigoPersona);

        Task<ResultDto<RH_PERSONAS_MOV_CONTROL>> Add(RH_PERSONAS_MOV_CONTROL entity);
        Task<ResultDto<RH_PERSONAS_MOV_CONTROL>> Update(RH_PERSONAS_MOV_CONTROL entity);
        Task<string> Delete(int CodigoPersonaMovCtrl);
        Task<int> GetNextKey();
    }
}

