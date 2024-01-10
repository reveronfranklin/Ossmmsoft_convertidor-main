using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmDirBienService
    {
        
        
        Task<ResultDto<BmDirBienGetDto>> Update(BmDirBienUpdateDto dto);
        Task<ResultDto<BmDirBienGetDto>> Create(BmDirBienUpdateDto dto);
        Task<ResultDto<BmDirBienDeleteDto>> Delete(BmDirBienDeleteDto dto);

    }
}

