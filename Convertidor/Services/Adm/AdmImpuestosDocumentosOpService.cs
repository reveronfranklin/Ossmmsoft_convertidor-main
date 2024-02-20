using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;
using Convertidor.Dtos.Sis;
using MathNet.Numerics.RootFinding;

namespace Convertidor.Services.Adm
{
    public class AdmImpuestosDocumentosOpService : IAdmImpuestosDocumentosOpService
    {
        private readonly IAdmImpuestosDocumentosOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDocumentosOpRepository _admDocumentosOpRepository;
        private readonly IAdmRetencionesOpRepository _admRetencionesOpRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmImpuestosDocumentosOpService(IAdmImpuestosDocumentosOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDocumentosOpRepository admDocumentosOpRepository,
                                     IAdmRetencionesOpRepository admRetencionesOpRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDocumentosOpRepository = admDocumentosOpRepository;
            _admRetencionesOpRepository = admRetencionesOpRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

        public async Task<AdmImpuestosDocumentosOpResponseDto> MapImpuestosDocumentosOpDto(ADM_IMPUESTOS_DOCUMENTOS_OP dtos)
        {
            AdmImpuestosDocumentosOpResponseDto itemResult = new AdmImpuestosDocumentosOpResponseDto();
            itemResult.CodigoImpuestoDocumentoOp = dtos.CODIGO_IMPUESTO_DOCUMENTO_OP;
            itemResult.CodigoDocumentoOp = dtos.CODIGO_DOCUMENTO_OP;
            itemResult.CodigoRetencion = dtos.CODIGO_RETENCION;
            itemResult.TipoRetencionId=dtos.TIPO_RETENCION_ID;
            itemResult.TipoImpuestoId=dtos.TIPO_IMPUESTO_ID;
            itemResult.PeriodoImpositivo = dtos.PERIODO_IMPOSITIVO;
            itemResult.BaseImponible = dtos.BASE_IMPONIBLE;
            itemResult.MontoImpuesto = dtos.MONTO_IMPUESTO;
            itemResult.MontoImpuestoExento = dtos.MONTO_IMPUESTO_EXENTO;
            itemResult.MontoRetenido = dtos.MONTO_RETENIDO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
           
            
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

                var codigoDocumentoOp = await _admDocumentosOpRepository.GetCodigoDocumentoOp(dto.CodigoDocumentoOp);
                if (dto.CodigoDocumentoOp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo documento op no existe";
                    return result;
                }
                var codigoRetencion = await _admRetencionesOpRepository.GetCodigoRetencionOp(dto.CodigoRetencion);
                if (dto.CodigoRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retencion invalido";
                    return result;
                }

                 var tipoRetencionId = await _admDescriptivaRepository.GetByIdAndTitulo(19, dto.TipoRetencionId);
              
                if(dto.TipoRetencionId < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo retencion Id invalido";
                    return result;
                }

                var tipoImpuestoId = await _admDescriptivaRepository.GetByIdAndTitulo(18, dto.TipoImpuestoId);
                if (dto.TipoImpuestoId > 0 && tipoImpuestoId==false )
                {
                    
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo impuesto id invalido";
                    return result;
                }

                if (dto.PeriodoImpositivo == string.Empty && dto.PeriodoImpositivo.Length > 6)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo Impositivo invalido";
                    return result;
                }

                if (dto.BaseImponible < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible invalida";
                    return result;
                }

                if(dto.MontoImpuesto < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto impuesto invalido";
                    return result;
                }

                if(dto.MontoImpuestoExento < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto impuesto exento invalido";
                    return result;
                }

                if(dto.MontoRetenido != null && dto.MontoRetenido < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retenido invalido";
                    return result;
                }
                
                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }




                codigoImpuestoDocumentoOp.CODIGO_IMPUESTO_DOCUMENTO_OP = dto.CodigoImpuestoDocumentoOp;
                codigoImpuestoDocumentoOp.CODIGO_DOCUMENTO_OP=dto.CodigoDocumentoOp;
                codigoImpuestoDocumentoOp.CODIGO_RETENCION = dto.CodigoRetencion;
                codigoImpuestoDocumentoOp.TIPO_RETENCION_ID = dto.TipoRetencionId;
                codigoImpuestoDocumentoOp.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                codigoImpuestoDocumentoOp.PERIODO_IMPOSITIVO = dto.PeriodoImpositivo;
                codigoImpuestoDocumentoOp.BASE_IMPONIBLE=dto.BaseImponible;
                codigoImpuestoDocumentoOp.MONTO_IMPUESTO=dto.MontoImpuesto;
                codigoImpuestoDocumentoOp.MONTO_IMPUESTO_EXENTO = dto.MontoImpuestoExento;
                codigoImpuestoDocumentoOp.MONTO_RETENIDO=dto.MontoRetenido;
                codigoImpuestoDocumentoOp.EXTRA1 = dto.Extra1;
                codigoImpuestoDocumentoOp.EXTRA2 = dto.Extra2;
                codigoImpuestoDocumentoOp.EXTRA3 = dto.Extra3;



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

                var codigoDocumentoOp = await _admDocumentosOpRepository.GetCodigoDocumentoOp(dto.CodigoDocumentoOp);
                if (dto.CodigoDocumentoOp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo documento op no existe";
                    return result;
                }
                var codigoRetencion = await _admRetencionesOpRepository.GetCodigoRetencionOp(dto.CodigoRetencion);
                if (dto.CodigoRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retencion invalido";
                    return result;
                }

                var tipoRetencionId = await _admDescriptivaRepository.GetByIdAndTitulo(19, dto.TipoRetencionId);

                if (dto.TipoRetencionId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo retencion Id invalido";
                    return result;
                }
                var tipoImpuestoId = await _admDescriptivaRepository.GetByIdAndTitulo(18, dto.TipoImpuestoId);
                if (dto.TipoImpuestoId > 0 && tipoImpuestoId == false)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo impuesto id invalido";
                    return result;
                }

                if (dto.PeriodoImpositivo == string.Empty && dto.PeriodoImpositivo.Length > 6)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo Impositivo invalido";
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

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }


                ADM_IMPUESTOS_DOCUMENTOS_OP entity = new ADM_IMPUESTOS_DOCUMENTOS_OP();
                entity.CODIGO_IMPUESTO_DOCUMENTO_OP = await _repository.GetNextKey();
                entity.CODIGO_DOCUMENTO_OP = dto.CodigoDocumentoOp;
                entity.CODIGO_RETENCION = dto.CodigoRetencion;
                entity.TIPO_RETENCION_ID = dto.TipoRetencionId;
                entity.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                entity.PERIODO_IMPOSITIVO = dto.PeriodoImpositivo;
                entity.BASE_IMPONIBLE = dto.BaseImponible;
                entity.MONTO_IMPUESTO = dto.MontoImpuesto;
                entity.MONTO_IMPUESTO_EXENTO = dto.MontoImpuestoExento;
                entity.MONTO_RETENIDO = dto.MontoRetenido;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;



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

