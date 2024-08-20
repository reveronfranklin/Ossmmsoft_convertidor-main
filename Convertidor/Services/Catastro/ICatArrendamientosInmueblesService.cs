﻿using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatArrendamientosInmueblesService
    {
        Task<ResultDto<List<CatArrendamientosInmueblesResponseDto>>> GetAll();
    }
}
