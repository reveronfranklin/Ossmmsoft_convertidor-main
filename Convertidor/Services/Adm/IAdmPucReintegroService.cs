﻿using Convertidor.Dtos.Adm;
using Convertidor.Dtos;

namespace Convertidor.Services.Adm
{
    public interface IAdmPucReintegroService
    {
        Task<ResultDto<AdmPucReintegroResponseDto>> Update(AdmPucReintegroUpdateDto dto);
        Task<ResultDto<AdmPucReintegroResponseDto>> Create(AdmPucReintegroUpdateDto dto);
        Task<ResultDto<AdmPucReintegroDeleteDto>> Delete(AdmPucReintegroDeleteDto dto);
    }
}
