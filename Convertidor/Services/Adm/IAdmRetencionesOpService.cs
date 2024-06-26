﻿using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmRetencionesOpService
    {
        Task<ResultDto<List<AdmRetencionesOpResponseDto>>> GetAll();
        Task<ResultDto<AdmRetencionesOpResponseDto>> Update(AdmRetencionesOpUpdateDto dto);
        Task<ResultDto<AdmRetencionesOpResponseDto>> Create(AdmRetencionesOpUpdateDto dto);
        Task<ResultDto<AdmRetencionesOpDeleteDto>> Delete(AdmRetencionesOpDeleteDto dto);
    }
}
