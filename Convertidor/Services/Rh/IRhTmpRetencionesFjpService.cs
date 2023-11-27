﻿using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
    public interface IRhTmpRetencionesFjpService
    {
        Task<ResultDto<List<RhTmpRetencionesFjpDto>>> GetRetencionesFjp(FilterRetencionesDto filter);
    }
}
