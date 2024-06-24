﻿using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntEstadoCuentasService
    {
        Task<ResultDto<List<CntEstadoCuentasResponseDto>>> GetAll();
    }
}
