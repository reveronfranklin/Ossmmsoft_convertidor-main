using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_V_DOC_PAGADOServices : IPRE_V_DOC_PAGADOServices
    {
       

        private readonly IPRE_V_DOC_PAGADORepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;

       

        public PRE_V_DOC_PAGADOServices(IPRE_V_DOC_PAGADORepository repository,
                                    IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository)
        {
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;

        }
        
        public async Task<ResultDto<List<PreDetalleDocumentoGetDto>>> GetAllByCodigoSaldo(FilterDocumentosPreVSaldo filter)
        {
           
            ResultDto<List<PreDetalleDocumentoGetDto>> result = new ResultDto<List<PreDetalleDocumentoGetDto>>(null);
            try
            {
                var documentosCompromisos = await _repository.GetByCodicoSaldo(filter);
                if (documentosCompromisos.Count() > 0)
                {
                    var id = 0;
                    List<PreDetalleDocumentoGetDto> resultList = new List<PreDetalleDocumentoGetDto>();
                    foreach (var item in documentosCompromisos)
                    {
                        id++;

                        resultList.Add(MapPreDetalleDocumentoGetDto(item,id));
                    }


                 

                    result.Data = resultList;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;


        }

       

        public  PreDetalleDocumentoGetDto MapPreDetalleDocumentoGetDto(PRE_V_DOC_PAGADO entity,int id)
        {
            PreDetalleDocumentoGetDto dto = new PreDetalleDocumentoGetDto();
            dto.Id = id;
            dto.CodigoSaldo=entity.CODIGO_SALDO;
            dto.CodigoPresupuesto=entity.CODIGO_PRESUPUESTO;
            dto.Fecha = entity.FECHA;
            dto.Numero = entity.NUMERO;

            dto.Motivo = entity.MOTIVO;
            dto.Monto = entity.MONTO;

            return dto;


        }

    }
}

