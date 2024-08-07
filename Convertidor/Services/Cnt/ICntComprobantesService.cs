﻿using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntComprobantesService
    {
        Task<ResultDto<List<CntComprobantesResponseDto>>> GetAll();
        Task<ResultDto<CntComprobantesResponseDto>> GetByCodigo(int codigoComprobante);
        Task<ResultDto<CntComprobantesResponseDto>> Create(CntComprobantesUpdateDto dto);
        Task<ResultDto<CntComprobantesResponseDto>> Update(CntComprobantesUpdateDto dto);
        Task<ResultDto<CntComprobantesDeleteDto>> Delete(CntComprobantesDeleteDto dto);
    }
}
