using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmSolMovBienesService
    {
        Task<BmSolMovBienesResponseDto> GetByCodigoMovBien(int codigoMovBien);
        Task<ResultDto<List<BmSolMovBienesResponseDto>>> GetAll();
        Task<ResultDto<BmSolMovBienesResponseDto>> Update(BmSolMovBienesUpdateDto dto);
        Task<ResultDto<BmSolMovBienesResponseDto>> Create(BmSolMovBienesUpdateDto dto);
        Task<ResultDto<BmSolMovBienesDeleteDto>> Delete(BmSolMovBienesDeleteDto dto);
    }
}

