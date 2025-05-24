
using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmRetencionesOpService
    {
        Task<ResultDto<List<AdmRetencionesOpResponseDto>>> GetAll();
        Task<ResultDto<List<AdmRetencionesOpResponseDto>>> GetByOrdenPago(AdmRetencionesFilterDto filter);
        Task<ResultDto<AdmRetencionesOpResponseDto>> Update(AdmRetencionesOpUpdateDto dto);
        Task<ResultDto<AdmRetencionesOpResponseDto>> Create(AdmRetencionesOpUpdateDto dto);
        Task<ResultDto<AdmRetencionesOpDeleteDto>> Delete(AdmRetencionesOpDeleteDto dto);

        Task<ADM_RETENCIONES_OP> GetByOrdenPagoCodigoRetencionTipoRetencion(int codigoOrdenPago, int codigoRetencion,
            int tipoRetencionId);
    }
}
