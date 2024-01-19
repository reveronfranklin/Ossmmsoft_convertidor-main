using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmMovBienesService
    {
        Task<BmMovBienesResponseDto> GetByCodigoMovBien(int codigoMovBien);
        Task<ResultDto<List<BmMovBienesResponseDto>>> GetAll();
        Task<ResultDto<BmMovBienesResponseDto>> Update(BmMovBienesUpdateDto dto);
        Task<ResultDto<BmMovBienesResponseDto>> Create(BmMovBienesUpdateDto dto);
        Task<ResultDto<BmMovBienesDeleteDto>> Delete(BmMovBienesDeleteDto dto);
    }
}

