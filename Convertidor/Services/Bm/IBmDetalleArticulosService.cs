using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmDetalleArticulosService
    {
        Task<ResultDto<List<BmDetalleArticulosResponseDto>>> GetAll();
        
        Task<ResultDto<BmDetalleArticulosResponseDto>> Update(BmDetalleArticulosUpdateDto dto);
        Task<ResultDto<BmDetalleArticulosResponseDto>> Create(BmDetalleArticulosUpdateDto dto);
        Task<ResultDto<BmDetalleArticulosDeleteDto>> Delete(BmDetalleArticulosDeleteDto dto);

    }
}

