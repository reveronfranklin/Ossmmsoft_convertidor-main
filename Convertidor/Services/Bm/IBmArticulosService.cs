using System;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmArticulosService
    {
        
        Task<ResultDto<List<BmArticulosGetDto>>> GetAll();
        Task<ResultDto<List<BmArticulosGetDto>>> GetByCodigoArticulo(int codigoArticulo);
        Task<ResultDto<List<BmArticulosGetDto>>> GetByCodigo(string codigo);
        Task<ResultDto<BmArticulosGetDto>> Update(BmArticulosUpdateDto dto);
        Task<ResultDto<BmArticulosGetDto>> Create(BmArticulosUpdateDto dto);
        Task<ResultDto<BmArticulosDeleteDto>> Delete(BmArticulosDeleteDto dto);

    }
}

