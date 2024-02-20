using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmBienesService
    {
        Task<ResultDto<List<BmBienesResponseDto>>> GetAll();
        
        Task<ResultDto<BmBienesResponseDto>> Update(BmBienesUpdateDto dto);
        Task<ResultDto<BmBienesResponseDto>> Create(BmBienesUpdateDto dto);
        Task<ResultDto<BmBienesDeleteDto>> Delete(BmBienesDeleteDto dto);

    }
}

