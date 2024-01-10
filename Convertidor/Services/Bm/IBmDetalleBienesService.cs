using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmDetalleBienesService
    {
        Task<ResultDto<List<BmDetalleBienesGetDto>>> GetAll();
        
        Task<ResultDto<BmDetalleBienesGetDto>> Update(BmDetalleBienesUpdateDto dto);
        Task<ResultDto<BmDetalleBienesGetDto>> Create(BmDetalleBienesUpdateDto dto);
        Task<ResultDto<BmDetalleBienesDeleteDto>> Delete(BmDetalleBienesDeleteDto dto);

    }
}

