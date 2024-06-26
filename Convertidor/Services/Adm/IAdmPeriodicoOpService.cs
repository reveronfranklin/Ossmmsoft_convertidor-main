﻿using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmPeriodicoOpService
    {
        Task<ResultDto<List<AdmPeriodicoOpResponseDto>>> GetAll();
        Task<ResultDto<AdmPeriodicoOpResponseDto>> Update(AdmPeriodicoOpUpdateDto dto);
        Task<ResultDto<AdmPeriodicoOpResponseDto>> Create(AdmPeriodicoOpUpdateDto dto);
        Task<ResultDto<AdmPeriodicoOpDeleteDto>> Delete(AdmPeriodicoOpDeleteDto dto);
    }
}
