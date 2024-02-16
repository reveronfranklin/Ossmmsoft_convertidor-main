using System;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmArticulosService
    {
        
        Task<ResultDto<List<BmArticulosResponseDto>>> GetAll();
        Task<ResultDto<List<BmArticulosResponseDto>>> GetByCodigoArticulo(int codigoArticulo);
        Task<ResultDto<List<BmArticulosResponseDto>>> GetByCodigo(string codigo);
        Task<ResultDto<BmArticulosResponseDto>> Update(BmArticulosUpdateDto dto);
        Task<ResultDto<BmArticulosResponseDto>> Create(BmArticulosUpdateDto dto);
        Task<ResultDto<BmArticulosDeleteDto>> Delete(BmArticulosDeleteDto dto);

    }
}

