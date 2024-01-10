using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmDetalleArticulosService
    {
        Task<ResultDto<List<BmDetalleArticulosGetDto>>> GetAll();
        
        Task<ResultDto<BmDetalleArticulosGetDto>> Update(BmDetalleArticulosUpdateDto dto);
        Task<ResultDto<BmDetalleArticulosGetDto>> Create(BmDetalleArticulosUpdateDto dto);
        Task<ResultDto<BmDetalleArticulosDeleteDto>> Delete(BmDetalleArticulosDeleteDto dto);

    }
}

