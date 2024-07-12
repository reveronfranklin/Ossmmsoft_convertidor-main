using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Repository.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmDetalleSolCompromisoService : IAdmDetalleSolCompromisoService
    {
        private readonly IAdmDetalleSolCompromisoRepository _repository;

        public AdmDetalleSolCompromisoService(IAdmDetalleSolCompromisoRepository repository)
        {
            _repository = repository;
        }


        public async Task<AdmDetalleSolCompromisoResponseDto> MapDetalleSolCompromisodDto(ADM_DETALLE_SOL_COMPROMISO dtos)
        {
            AdmDetalleSolCompromisoResponseDto itemResult = new AdmDetalleSolCompromisoResponseDto();
            itemResult.CodigoDetalleSolicitud = dtos.CODIGO_DETALLE_SOLICITUD;
            itemResult.CodigoPucSolicitud = dtos.CODIGO_PUC_SOLICITUD;
            itemResult.Cantidad = dtos.CANTIDAD;
            itemResult.UdmId = dtos.UDM_ID;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.PrecioUnitario = dtos.PRECIO_UNITARIO;
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.PorImpuesto = dtos.POR_IMPUESTO;
            itemResult.CantidadAprobada = dtos.CANTIDAD_APROBADA;
            itemResult.CantidadAnulada = dtos.CANTIDAD_ANULADA;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            
            return itemResult;
        }

        public async Task<List<AdmDetalleSolCompromisoResponseDto>> MapListDetalleSolicitudDto(List<ADM_DETALLE_SOL_COMPROMISO> dtos)
        {
            List<AdmDetalleSolCompromisoResponseDto> result = new List<AdmDetalleSolCompromisoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapDetalleSolCompromisodDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }


        public async Task<ResultDto<List<AdmDetalleSolCompromisoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmDetalleSolCompromisoResponseDto>> result = new ResultDto<List<AdmDetalleSolCompromisoResponseDto>>(null);
            try
            {
                var detalleSolCompromiso = await _repository.GetAll();

                if (detalleSolCompromiso != null && detalleSolCompromiso.Count() > 0)
                {
                    var listDto = await MapListDetalleSolicitudDto(detalleSolCompromiso);

                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No data";

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }

    }
}
