using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhProcesosService
	{
        Task<RH_PROCESOS> GetByCodigo(int codigoProcesso);
        Task<ResultDto<List<RhprocesosDto>>> GetAll();

    }
}

