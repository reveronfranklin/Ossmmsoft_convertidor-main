﻿using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatArrendamientosInmueblesService
    {
        Task<ResultDto<List<CatArrendamientosInmueblesResponseDto>>> GetAll();
        Task<ResultDto<CatArrendamientosInmueblesResponseDto>> Create(CatArrendamientosInmueblesUpdateDto dto);
        Task<ResultDto<CatArrendamientosInmueblesResponseDto>> Update(CatArrendamientosInmueblesUpdateDto dto);
        Task<ResultDto<CatArrendamientosInmueblesDeleteDto>> Delete(CatArrendamientosInmueblesDeleteDto dto);
    }
}
