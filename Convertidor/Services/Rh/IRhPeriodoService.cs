﻿namespace Convertidor.Services.Rh
{
	public interface IRhPeriodoService
	{


		Task<List<RhPeriodosResponseDto>> GetAll(PeriodoFilterDto filter);
        Task<List<RhPeriodosResponseDto>> GetByTipoNomina(int tipoNomina);
        Task<RhPeriodosResponseDto> GetByPeriodo(int codigoPeriodo);
        Task<List<RhPeriodosResponseDto>> GetByYear(int ano);
        Task<ResultDto<RhPeriodosResponseDto>> Create(RhPeriodosUpdate dto);
        Task<ResultDto<RhPeriodosResponseDto>> Update(RhPeriodosUpdate dto);
        Task<ResultDto<RhPeriodosDeleteDto>> Delete(RhPeriodosDeleteDto dto);
        Task<ResultDto<RhPeriodosResponseDto>> UpdateDatosCierre(RhPeriodosUsuarioFechaUpdate dto);
        Task<ResultDto<RhPeriodosResponseDto>> UpdateDatosPreCierre(RhPeriodosUsuarioFechaUpdate dto);

        Task<ResultDto<RhPeriodosResponseDto>> UpdateDatosPrenomina(RhPeriodosUsuarioFechaUpdate dto);
        Task<RhPeriodosResponseDto> GetPeriodoAbierto(RhTiposNominaFilterDto dto);

    }
}

