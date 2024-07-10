﻿using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ITmpDetalleComprobanteService
    {
        Task<ResultDto<List<TmpDetalleComprobanteResponseDto>>> GetAll();
        Task<ResultDto<TmpDetalleComprobanteResponseDto>> Create(TmpDetalleComprobanteUpdateDto dto);
    }
}
