using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPRE_PRESUPUESTOSService
	{


        Task<ResultDto<GetPRE_PRESUPUESTOSDto>> GetByCodigo(FilterPRE_PRESUPUESTOSDto filter);

        Task<ResultDto<List<GetPRE_PRESUPUESTOSDto>>> GetAll(FilterPRE_PRESUPUESTOSDto filter);

        Task<ResultDto<GetPRE_PRESUPUESTOSDto>> Create(CreatePRE_PRESUPUESTOSDto dto);
        Task<ResultDto<GetPRE_PRESUPUESTOSDto>> Update(UpdatePRE_PRESUPUESTOSDto dto);
        Task<ResultDto<DeletePrePresupuestoDto>> Delete(DeletePrePresupuestoDto dto);

        Task<ResultDto<List<ListPresupuestoDto>>> GetListPresupuesto();
        Task<ResultDto<List<GetPRE_PRESUPUESTOSDto>>> GetList(FilterPRE_PRESUPUESTOSDto filter);
        Task<PRE_PRESUPUESTOS> GetUltimo();
	}
}

