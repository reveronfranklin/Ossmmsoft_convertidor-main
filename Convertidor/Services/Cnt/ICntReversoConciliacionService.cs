﻿using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntReversoConciliacionService
    {
        
        Task<ResultDto<List<CntReversoConciliacionResponseDto>>> GetAllByCodigoConciliacion(int codigoConciliacion);
    }
}
