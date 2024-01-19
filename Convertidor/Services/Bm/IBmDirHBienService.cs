using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmDirHBienService
    {

        Task<BmDirHBienResponseDto> GetByCodigoHDirBien(int codigoHDirBien);
        Task<ResultDto<BmDirHBienResponseDto>> Update(BmDirHBienUpdateDto dto);
        Task<ResultDto<BmDirHBienResponseDto>> Create(BmDirHBienUpdateDto dto);
        Task<ResultDto<BmDirHBienDeleteDto>> Delete(BmDirHBienDeleteDto dto);

    }
}

