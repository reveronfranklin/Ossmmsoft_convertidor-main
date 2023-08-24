using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhPersonasRepository
	{
        Task<List<RH_PERSONAS>> GetAll();
        Task<RH_PERSONAS> GetCedula(int cedula);
        Task<RH_PERSONAS> GetCodigoPersona(int codigoPersona);
        Task<ResultDto<RH_PERSONAS>> Add(RH_PERSONAS entity);
        Task<ResultDto<RH_PERSONAS>> Update(RH_PERSONAS entity);
        Task<string> Delete(int codigoRelacionCargo);

    }
}

