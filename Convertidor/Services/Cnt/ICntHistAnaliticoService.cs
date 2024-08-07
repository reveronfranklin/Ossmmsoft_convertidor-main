﻿using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntHistAnaliticoService
    {
        Task<ResultDto<List<CntHistAnaliticoResponseDto>>> GetAll();
        Task<ResultDto<CntHistAnaliticoResponseDto>> Create(CntHistAnaliticoUpdateDto dto);
        Task<ResultDto<CntHistAnaliticoResponseDto>> Update(CntHistAnaliticoUpdateDto dto);
        Task<ResultDto<CntHistAnaliticoDeleteDto>> Delete(CntHistAnaliticoDeleteDto dto);
    }
}
