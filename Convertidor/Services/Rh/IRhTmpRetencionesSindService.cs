﻿namespace Convertidor.Services.Rh
{
    public interface IRhTmpRetencionesSindService
    {
        Task<ResultDto<List<RhTmpRetencionesSindDto>>> GetRetencionesSind(FilterRetencionesDto filter);
    }
}
