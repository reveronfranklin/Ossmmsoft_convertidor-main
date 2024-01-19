using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmDetalleBienesService
    {
        Task<ResultDto<List<BmDetalleBienesResponseDto>>> GetAll();
        
        Task<ResultDto<BmDetalleBienesResponseDto>> Update(BmDetalleBienesUpdateDto dto);
        Task<ResultDto<BmDetalleBienesResponseDto>> Create(BmDetalleBienesUpdateDto dto);
        Task<ResultDto<BmDetalleBienesDeleteDto>> Delete(BmDetalleBienesDeleteDto dto);

    }
}

