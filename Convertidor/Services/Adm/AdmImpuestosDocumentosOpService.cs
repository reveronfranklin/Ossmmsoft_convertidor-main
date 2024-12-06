using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmImpuestosDocumentosOpService : IAdmImpuestosDocumentosOpService
    {
        private readonly IAdmImpuestosDocumentosOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDocumentosOpRepository _admDocumentosOpRepository;
        private readonly IAdmRetencionesOpRepository _admRetencionesOpRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmRetencionesRepository _admRetencionesRepository;

        public AdmImpuestosDocumentosOpService(IAdmImpuestosDocumentosOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDocumentosOpRepository admDocumentosOpRepository,
                                     IAdmRetencionesOpRepository admRetencionesOpRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IAdmRetencionesRepository admRetencionesRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDocumentosOpRepository = admDocumentosOpRepository;
            _admRetencionesOpRepository = admRetencionesOpRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _admRetencionesRepository = admRetencionesRepository;
        }

        public async Task<AdmImpuestosDocumentosOpResponseDto> MapImpuestosDocumentosOpDto(ADM_IMPUESTOS_DOCUMENTOS_OP dtos)
        {
            AdmImpuestosDocumentosOpResponseDto itemResult = new AdmImpuestosDocumentosOpResponseDto();
            itemResult.CodigoImpuestoDocumentoOp = dtos.CODIGO_IMPUESTO_DOCUMENTO_OP;
            itemResult.CodigoDocumentoOp = dtos.CODIGO_DOCUMENTO_OP;
            itemResult.CodigoRetencion = dtos.CODIGO_RETENCION;
            itemResult.DescripcionCodigoRetencion = "";
            var retencion = await _admRetencionesRepository.GetCodigoRetencion(itemResult.CodigoRetencion);
            if (retencion != null)
            {
                itemResult.DescripcionCodigoRetencion = retencion.CONCEPTO_PAGO;
            }
            
            
            itemResult.TipoRetencionId=dtos.TIPO_RETENCION_ID;
            itemResult.DescripcionTipoRetencion = "";
            
        
            itemResult.DescripcionTipoRetencion =
                await _admDescriptivaRepository.GetDescripcion(itemResult.TipoRetencionId);
            itemResult.PeriodoImpositivo = dtos.PERIODO_IMPOSITIVO;
            itemResult.BaseImponible = dtos.BASE_IMPONIBLE;
            itemResult.MontoImpuesto = dtos.MONTO_IMPUESTO;
            itemResult.MontoImpuestoExento = dtos.MONTO_IMPUESTO_EXENTO;
            itemResult.MontoRetenido = dtos.MONTO_RETENIDO;
           
           
            
            return itemResult;
        }

        public async Task<List<AdmImpuestosDocumentosOpResponseDto>> MapListImpuestosDocumentosOpDto(List<ADM_IMPUESTOS_DOCUMENTOS_OP> dtos)
        {
            List<AdmImpuestosDocumentosOpResponseDto> result = new List<AdmImpuestosDocumentosOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapImpuestosDocumentosOpDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }
        
        public async Task<ResultDto<List<AdmImpuestosDocumentosOpResponseDto>>> GetByDocumento(AdmImpuestosDocumentosOpFilterDto dto)
        {

            ResultDto<List<AdmImpuestosDocumentosOpResponseDto>> result = new ResultDto<List<AdmImpuestosDocumentosOpResponseDto>>(null);
            try
            {
                var documentosOp = await _repository.GetByDocumento(dto.CodigoDocumentoOp);
                var cant = documentosOp.Count();
                if (documentosOp != null && documentosOp.Count() > 0)
                {
                    var listDto = await MapListImpuestosDocumentosOpDto(documentosOp);

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


        public async Task<ResultDto<AdmImpuestosDocumentosOpResponseDto>> Update(AdmImpuestosDocumentosOpUpdateDto dto)
        {
            ResultDto<AdmImpuestosDocumentosOpResponseDto> result = new ResultDto<AdmImpuestosDocumentosOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoImpuestoDocumentoOp = await _repository.GetCodigoImpuestoDocumentoOp(dto.CodigoImpuestoDocumentoOp);
                if (codigoImpuestoDocumentoOp == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo impuesto documento op no existe";
                    return result;
                }
          var documentoOp = await _admDocumentosOpRepository.GetCodigoDocumentoOp(dto.CodigoDocumentoOp);
                if (documentoOp==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo documento op no existe";
                    return result;
                }
                var retencion = await _admRetencionesOpRepository.GetCodigoRetencionOp(dto.CodigoRetencion);
                if (retencion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retencion invalido";
                    return result;
                }

                var tipoRetencion = await _admDescriptivaRepository.GetByCodigo( dto.TipoRetencionId);

                if (tipoRetencion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo retencion Id invalido";
                    return result;
                }
             
             

                if (dto.BaseImponible < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible invalida";
                    return result;
                }

                if (dto.MontoImpuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto impuesto invalido";
                    return result;
                }

                if (dto.MontoImpuestoExento < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto impuesto exento invalido";
                    return result;
                }

                if (dto.MontoRetenido != null && dto.MontoRetenido < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retenido invalido";
                    return result;
                }



                codigoImpuestoDocumentoOp.CODIGO_IMPUESTO_DOCUMENTO_OP = dto.CodigoImpuestoDocumentoOp;
                codigoImpuestoDocumentoOp.CODIGO_DOCUMENTO_OP=dto.CodigoDocumentoOp;
                codigoImpuestoDocumentoOp.CODIGO_RETENCION = dto.CodigoRetencion;
                codigoImpuestoDocumentoOp.TIPO_RETENCION_ID = dto.TipoRetencionId;
                codigoImpuestoDocumentoOp.TIPO_IMPUESTO_ID = 0;
                codigoImpuestoDocumentoOp.BASE_IMPONIBLE=dto.BaseImponible;
                codigoImpuestoDocumentoOp.MONTO_IMPUESTO=dto.MontoImpuesto;
                codigoImpuestoDocumentoOp.MONTO_IMPUESTO_EXENTO = dto.MontoImpuestoExento;
                codigoImpuestoDocumentoOp.MONTO_RETENIDO=dto.MontoRetenido;
                codigoImpuestoDocumentoOp.EXTRA1 = "";
                codigoImpuestoDocumentoOp.EXTRA2 ="";
                codigoImpuestoDocumentoOp.EXTRA3 = "";



                codigoImpuestoDocumentoOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoImpuestoDocumentoOp.USUARIO_UPD = conectado.Usuario;
                codigoImpuestoDocumentoOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoImpuestoDocumentoOp);

                var resultDto = await MapImpuestosDocumentosOpDto(codigoImpuestoDocumentoOp);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ResultDto<AdmImpuestosDocumentosOpResponseDto>> Create(AdmImpuestosDocumentosOpUpdateDto dto)
        {
            ResultDto<AdmImpuestosDocumentosOpResponseDto> result = new ResultDto<AdmImpuestosDocumentosOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoImpuestoDocumentoOp = await _repository.GetCodigoImpuestoDocumentoOp(dto.CodigoImpuestoDocumentoOp);
                if (codigoImpuestoDocumentoOp != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo impuesto documento op ya existe";
                    return result;
                }

                var documentoOp = await _admDocumentosOpRepository.GetCodigoDocumentoOp(dto.CodigoDocumentoOp);
                if (documentoOp==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo documento op no existe";
                    return result;
                }
                var retencion = await _admRetencionesOpRepository.GetCodigoRetencionOp(dto.CodigoRetencion);
                if (retencion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retencion invalido";
                    return result;
                }

                var tipoRetencion = await _admDescriptivaRepository.GetByCodigo( dto.TipoRetencionId);

                if (tipoRetencion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo retencion Id invalido";
                    return result;
                }
             
             

                if (dto.BaseImponible < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible invalida";
                    return result;
                }

                if (dto.MontoImpuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto impuesto invalido";
                    return result;
                }

                if (dto.MontoImpuestoExento < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto impuesto exento invalido";
                    return result;
                }

                if (dto.MontoRetenido != null && dto.MontoRetenido < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retenido invalido";
                    return result;
                }

            


                ADM_IMPUESTOS_DOCUMENTOS_OP entity = new ADM_IMPUESTOS_DOCUMENTOS_OP();
                entity.CODIGO_IMPUESTO_DOCUMENTO_OP = await _repository.GetNextKey();
                entity.CODIGO_DOCUMENTO_OP = dto.CodigoDocumentoOp;
                entity.CODIGO_RETENCION = dto.CodigoRetencion;
                entity.TIPO_RETENCION_ID = dto.TipoRetencionId;
                entity.TIPO_IMPUESTO_ID = 0;
                
                var mes = DateTime.Now.Month.ToString().PadLeft(2, '0');
                var serieLetras = $"{DateTime.Now.Year}{mes} ";
                entity.PERIODO_IMPOSITIVO =$"{serieLetras.Trim()}";
                entity.BASE_IMPONIBLE = dto.BaseImponible;
                entity.MONTO_IMPUESTO = dto.MontoImpuesto;
                entity.MONTO_IMPUESTO_EXENTO = dto.MontoImpuestoExento;
                entity.MONTO_RETENIDO = dto.MontoRetenido;
                entity.EXTRA1 = "";
                entity.EXTRA2 = "";
                entity.EXTRA3 = "";



                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapImpuestosDocumentosOpDto(created.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {

                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                }

                return result;


            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<AdmImpuestosDocumentosOpDeleteDto>> Delete(AdmImpuestosDocumentosOpDeleteDto dto) 
        {
            ResultDto<AdmImpuestosDocumentosOpDeleteDto> result = new ResultDto<AdmImpuestosDocumentosOpDeleteDto>(null);
            try
            {

                var codigoImpuestoDocumentoOp = await _repository.GetCodigoImpuestoDocumentoOp(dto.CodigoImpuestoDocumentoOp);
                if (codigoImpuestoDocumentoOp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo comprobante doc op no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoImpuestoDocumentoOp);

                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }
    }
 }

