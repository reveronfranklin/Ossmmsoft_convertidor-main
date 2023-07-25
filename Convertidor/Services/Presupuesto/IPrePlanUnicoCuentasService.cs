using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPrePlanUnicoCuentasService
	{

        Task<ResultDto<List<PrePlanUnicoCuentasGetDto>>> GetAllByCodigoPresupuesto(int codigoPresupuesto);
        Task<ResultDto<List<TreePUC>>> GetTreeByPresupuesto(int codigoPresupuesto);

        Task<ResultDto<List<PrePlanUnicoCuentasGetDto>>> UpdatePucPadre(int codigoPresupuesto);
        Task<PRE_PLAN_UNICO_CUENTAS> GetPucPadre(FilterPrePUCPresupuestoCodigos dto);
        Task<ResultDto<PrePlanUnicoCuentasGetDto>> Update(PrePlanUnicoCuentaUpdateDto dto);
        Task<ResultDto<PrePlanUnicoCuentasGetDto>> Create(PrePlanUnicoCuentaUpdateDto dto);
        Task DeleteByCodigoPresupuesto(int codigoPresupuesto);
        Task<ResultDto<DeletePrePucDto>> Delete(DeletePrePucDto dto);
        Task<List<int>> ListarHijos(List<PRE_PLAN_UNICO_CUENTAS> listDto, int codPuc);
        Task<ResultDto<PrePlanUnicoCuentasGetDto>> ValidateDto(PrePlanUnicoCuentaUpdateDto dto);
        Task<ResultDto<List<PreCodigosPuc>>> ListCodigosHistoricoPuc();
    }
}

