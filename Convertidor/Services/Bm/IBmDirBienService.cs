using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmDirBienService
    {

        Task<BmDirBienResponseDto> GetByCodigoDirBien(int codigoDirBien);
        Task<ResultDto<BmDirBienResponseDto>> Update(BmDirBienUpdateDto dto);
        Task<ResultDto<BmDirBienResponseDto>> Create(BmDirBienUpdateDto dto);
        Task<ResultDto<BmDirBienDeleteDto>> Delete(BmDirBienDeleteDto dto);

    }
}

