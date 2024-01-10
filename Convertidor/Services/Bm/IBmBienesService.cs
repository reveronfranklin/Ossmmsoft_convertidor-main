using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmBienesService
    {
        Task<ResultDto<List<BmBienesGetDto>>> GetAll();
        
        Task<ResultDto<BmBienesGetDto>> Update(BmBienesUpdateDto dto);
        Task<ResultDto<BmBienesGetDto>> Create(BmBienesUpdateDto dto);
        Task<ResultDto<BmBienesDeleteDto>> Delete(BmBienesDeleteDto dto);

    }
}

