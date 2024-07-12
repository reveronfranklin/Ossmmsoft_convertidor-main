using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Repository.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmSolCompromisoService : IAdmSolCompromisoService
    {
        private readonly IAdmSolCompromisoRepository _repository;

        public AdmSolCompromisoService(IAdmSolCompromisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<AdmSolCompromisoResponseDto> MapSolCompromisoDto(ADM_SOL_COMPROMISO dtos)
        {
            AdmSolCompromisoResponseDto itemResult = new AdmSolCompromisoResponseDto();
            
                itemResult.CodigoSolCompromiso = dtos.CODIGO_SOL_COMPROMISO;
                itemResult.TipoSolCompromisoId = dtos.TIPO_SOL_COMPROMISO_ID;
                itemResult.NumeroSolicitud = dtos.NUMERO_SOLICITUD;
                itemResult.FechaSolicitud = dtos.FECHA_SOLICITUD;
                itemResult.FechaSolicitudString = Fecha.GetFechaString(dtos.FECHA_SOLICITUD);
                FechaDto fechaSolicitudObj = Fecha.GetFechaDto(dtos.FECHA_SOLICITUD);
                itemResult.FechaSolicitudObj = (FechaDto)fechaSolicitudObj;
                itemResult.CodigoSolicitante = dtos.CODIGO_SOLICITANTE;      
                itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
                itemResult.Motivo = dtos.MOTIVO ;
                itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
                itemResult.Ano = dtos.ANO;
     


            return itemResult;
        }

        public async Task<List<AdmSolCompromisoResponseDto>> MapListSolCompromisoDto(List<ADM_SOL_COMPROMISO> dtos)
        {
            List<AdmSolCompromisoResponseDto> result = new List<AdmSolCompromisoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapSolCompromisoDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmSolCompromisoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmSolCompromisoResponseDto>> result = new ResultDto<List<AdmSolCompromisoResponseDto>>(null);
            try
            {
                var solCompromiso = await _repository.GetAll();
                var cant = solCompromiso.Count();
                if (solCompromiso != null && solCompromiso.Count() > 0)
                {
                    var listDto = await MapListSolCompromisoDto(solCompromiso);

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
