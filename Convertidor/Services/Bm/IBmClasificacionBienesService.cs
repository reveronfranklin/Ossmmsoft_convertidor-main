using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmClasificacionBienesService
    {
        Task<ResultDto<List<BmClasificacionBienesGetDto>>> GetAll();
        
        Task<ResultDto<BmClasificacionBienesGetDto>> Update(BmClasificacionBienesUpdateDto dto);
        Task<ResultDto<BmClasificacionBienesGetDto>> Create(BmClasificacionBienesUpdateDto dto);
        Task<ResultDto<BmClasificacionBienesDeleteDto>> Delete(BmClasificacionBienesDeleteDto dto);

    }
}

