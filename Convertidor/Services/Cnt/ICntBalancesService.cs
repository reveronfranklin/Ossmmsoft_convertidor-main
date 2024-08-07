﻿using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntBalancesService
    {
        Task<ResultDto<List<CntBalancesResponseDto>>> GetAll();
        Task<ResultDto<CntBalancesResponseDto>> GetByCodigo(int codigoBalance);
        Task<ResultDto<CntBalancesResponseDto>> Create(CntBalancesUpdateDto dto);
        Task<ResultDto<CntBalancesResponseDto>> Update(CntBalancesUpdateDto dto);
        Task<ResultDto<CntBalancesDeleteDto>> Delete(CntBalancesDeleteDto dto);
    }
}
