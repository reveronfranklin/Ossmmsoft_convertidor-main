﻿namespace Convertidor.Services.Rh
{
    public interface IRhTmpRetencionesCahService
    {
        Task<ResultDto<List<RhTmpRetencionesCahDto>>> GetRetencionesCah(FilterRetencionesDto filter);
    }
}
