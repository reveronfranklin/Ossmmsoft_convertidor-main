﻿using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntTmpSaldosService
    {
        Task<ResultDto<List<CntTmpSaldosResponseDto>>> GetAll();
        Task<ResultDto<CntTmpSaldosResponseDto>> GetByCodigo(int codigoTmpSaldo);
        Task<ResultDto<CntTmpSaldosResponseDto>> Create(CntTmpSaldosUpdateDto dto);
        Task<ResultDto<CntTmpSaldosResponseDto>> Update(CntTmpSaldosUpdateDto dto);
        Task<ResultDto<CntTmpSaldosDeleteDto>> Delete(CntTmpSaldosDeleteDto dto);
    }
}
