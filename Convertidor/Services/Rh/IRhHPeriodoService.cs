using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhHPeriodoService
	{


        Task<List<RH_H_PERIODOS>> GetAll(PeriodoFilterDto filter);
        Task<List<RH_H_PERIODOS>> GetByTipoNomina(int tipoNomina);
        Task<List<RhHPeriodosResponseDto>> GetByYear(int ano);
        Task<ResultDto<RhHPeriodosResponseDto>> Create(RhHPeriodosUpdate dto);
        Task<ResultDto<RhHPeriodosResponseDto>> Update(RhHPeriodosUpdate dto);
        Task<ResultDto<RhHPeriodosDeleteDto>> Delete(RhHPeriodosDeleteDto dto);

    }
}

