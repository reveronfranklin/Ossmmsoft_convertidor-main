﻿using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntAuxiliaresPucService
    {
        Task<ResultDto<List<CntAuxiliaresPucResponseDto>>> GetAll();
    }
}
