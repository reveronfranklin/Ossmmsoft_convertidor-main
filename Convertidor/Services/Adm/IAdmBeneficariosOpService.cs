﻿using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmBeneficariosOpService
    {

        Task<ResultDto<List<AdmBeneficiariosOpResponseDto>>> GetByOrdenPago(AdmOrdenPagoBeneficiarioFlterDto filter);
        Task<ResultDto<AdmBeneficiariosOpResponseDto>> Update(AdmBeneficiariosOpUpdateDto dto);
        Task<ResultDto<AdmBeneficiariosOpResponseDto>> Create(AdmBeneficiariosOpUpdateDto dto);
        Task<ResultDto<AdmBeneficiariosOpDeleteDto>> Delete(AdmBeneficiariosOpDeleteDto dto);
        Task<ResultDto<AdmBeneficiariosOpResponseDto>> UpdateMonto(AdmBeneficiariosOpUpdateMontoDto dto);
        Task<ADM_BENEFICIARIOS_OP> GetByOrdenPagoProveedor(int codigoOrdenPago, int codigoProveedor);
        Task ActualizaMontoDesdePucOrdenPago(AdmOrdenPagoBeneficiarioFlterDto filter);
    }
}
