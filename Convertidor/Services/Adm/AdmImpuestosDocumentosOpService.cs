using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm.AdmRetencionesOp;

namespace Convertidor.Services.Adm
{
    public  class AdmImpuestosDocumentosOpService : IAdmImpuestosDocumentosOpService
    {
        private readonly IAdmImpuestosDocumentosOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDocumentosOpRepository _admDocumentosOpRepository;
        private readonly IAdmRetencionesOpRepository _admRetencionesOpRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmRetencionesRepository _admRetencionesRepository;
        private readonly IAdmRetencionesOpService _admRetencionesOpService;

        public AdmImpuestosDocumentosOpService(IAdmImpuestosDocumentosOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDocumentosOpRepository admDocumentosOpRepository,
                                     IAdmRetencionesOpRepository admRetencionesOpRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IAdmRetencionesRepository admRetencionesRepository,
                                     IAdmRetencionesOpService admRetencionesOpService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDocumentosOpRepository = admDocumentosOpRepository;
            _admRetencionesOpRepository = admRetencionesOpRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _admRetencionesRepository = admRetencionesRepository;
            _admRetencionesOpService = admRetencionesOpService;
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
            if(dtos.MONTO_RETENIDO == null){ dtos.MONTO_RETENIDO = 0;}
            itemResult.MontoRetenido = (decimal)dtos.MONTO_RETENIDO;
           
           
            
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
            List<AdmImpuestosDocumentosOpResponseDto> resultDefault = new List<AdmImpuestosDocumentosOpResponseDto>();

            try
            {
                var documentosOp = await _repository.GetByDocumento(dto.CodigoDocumentoOp);
                var cant = documentosOp.Count();
                if (documentosOp != null && documentosOp.Count() > 0)
                {
                    var listDto = await MapListImpuestosDocumentosOpDto(documentosOp);

                    
                    // Calcular el total del BaseImponible
                    decimal totalBaseImponible = listDto.Sum(t => t.BaseImponible);

                    // Calcular el total del Impuesto
                    decimal totalMontoImpuesto = listDto.Sum(t => t.MontoImpuesto);
                    
                    // Calcular el total del Impuesto exento
                    decimal totalMontoImpuestoExento = listDto.Sum(t => t.MontoImpuestoExento);

                    // Calcular el total del Impuesto exento
                    decimal totalMontoRetenido = listDto.Sum(t => t.MontoRetenido);

                    result.Total1 = totalBaseImponible;
                    result.Total2 = totalMontoImpuesto;
                    result.Total3 = totalMontoImpuestoExento;
                    result.Total4 = totalMontoRetenido;
                    result.Page = 1;
                    result.TotalPage = 1;
                    result.CantidadRegistros = listDto.Count();
                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Total1 = 0;
                    result.Total2 = 0;
                    result.Total3 = 0;
                    result.Total4 = 0;
                    result.Page = 1;
                    result.TotalPage = 1;
                    result.CantidadRegistros = 0;
                    result.Data = resultDefault;
                    result.IsValid = true;
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

        public async Task ReplicarMotoRetenidoDocumento(int codigoDocumentoOp,decimal montoRetencion)
        {
            await _admDocumentosOpRepository.UpdateMontoRetenido(codigoDocumentoOp,montoRetencion);
            
          
        }

        
        public async Task ReplicaImpuestoAdmRetencionesOp(AdmImpuestosDocumentosOpUpdateDto dto)
        {
            
            var documentoOp = await _admDocumentosOpRepository.GetCodigoDocumentoOp(dto.CodigoDocumentoOp);
            if (documentoOp!=null)
            {
              
                AdmRetencionesOpUpdateDto admRetencionesOpDto = new AdmRetencionesOpUpdateDto();
                
                
                var admRetencionesOp = await _admRetencionesOpRepository.GetByOrdenPagoCodigoRetencionTipoRetencion(documentoOp.CODIGO_ORDEN_PAGO,dto.CodigoRetencion,dto.TipoRetencionId);
                if (admRetencionesOp != null )
                {
                    admRetencionesOpDto.CodigoRetencionOp = admRetencionesOp.CODIGO_RETENCION_OP;
                    admRetencionesOpDto.CodigoOrdenPago = documentoOp.CODIGO_ORDEN_PAGO;
                    admRetencionesOpDto.CodigoRetencion = (int)admRetencionesOp.CODIGO_RETENCION;
                    admRetencionesOpDto.TipoRetencionId = (int)admRetencionesOp.TIPO_RETENCION_ID;
                    admRetencionesOpDto.PorRetencion = (int)admRetencionesOp.POR_RETENCION;
                    admRetencionesOpDto.BaseImponible = dto.BaseImponible;
                    admRetencionesOpDto.MontoRetencion = dto.MontoRetenido;
                    admRetencionesOpDto.CodigoPresupuesto = (int)admRetencionesOp.CODIGO_PRESUPUESTO;
                    admRetencionesOpDto.NumeroComprobante = admRetencionesOp.NUMERO_COMPROBANTE;
                    await _admRetencionesOpService.Update(admRetencionesOpDto);
                }
                else
                {
                    admRetencionesOpDto.CodigoRetencionOp = 0;
                    admRetencionesOpDto.CodigoOrdenPago = documentoOp.CODIGO_ORDEN_PAGO;
                    admRetencionesOpDto.CodigoRetencion = dto.CodigoRetencion;
                    admRetencionesOpDto.TipoRetencionId = dto.TipoRetencionId;
                    var retencion= await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
                    if (retencion != null)
                    {
                        admRetencionesOpDto.PorRetencion = (decimal)retencion.POR_RETENCION;
                    }
              
                    admRetencionesOpDto.MontoRetencion = dto.MontoRetenido;
                    admRetencionesOpDto.BaseImponible = dto.BaseImponible;
                    admRetencionesOpDto.MontoRetencion = dto.MontoRetenido;
                    admRetencionesOpDto.CodigoPresupuesto = documentoOp.CODIGO_PRESUPUESTO;
                    admRetencionesOpDto.NumeroComprobante ="";
                    await _admRetencionesOpService.Create(admRetencionesOpDto);
                
                }
                
            
               /* var impuestosDocumentosOp = await _repository.GetByDocumento(dto.CodigoDocumentoOp);
    
                // Calcular el total Monto Retenido
                var totalMontoRetenido = impuestosDocumentosOp.Sum(t => t.MONTO_RETENIDO);
                await ReplicarMotoRetenidoDocumento(dto.CodigoDocumentoOp,(decimal)totalMontoRetenido);*/
                
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
                var retencion = await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
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
                
                

                var montoImpuesto=(dto.BaseImponible * (decimal)retencion.POR_RETENCION)/100;

                if (dto.MontoImpuestoExento > montoImpuesto)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Excento no puede ser mayor a el monto del impuesto";
                    return result;
                }
                
                var esValidoTotalBaseImpuestoVsBaseDocumento =
                    await EsValidoTotalBaseImpuestoVsBaseDocumentoUpdate(dto.CodigoDocumentoOp, dto.CodigoImpuestoDocumentoOp,dto.BaseImponible);
                if (esValidoTotalBaseImpuestoVsBaseDocumento ==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Base no puede ser mayor a la base del Documento";
                    return result;
                }
                
                codigoImpuestoDocumentoOp.CODIGO_IMPUESTO_DOCUMENTO_OP = dto.CodigoImpuestoDocumentoOp;
                codigoImpuestoDocumentoOp.CODIGO_DOCUMENTO_OP=dto.CodigoDocumentoOp;
               // codigoImpuestoDocumentoOp.CODIGO_RETENCION = dto.CodigoRetencion;
                //codigoImpuestoDocumentoOp.TIPO_RETENCION_ID = dto.TipoRetencionId;
                codigoImpuestoDocumentoOp.TIPO_IMPUESTO_ID = 0;
                codigoImpuestoDocumentoOp.BASE_IMPONIBLE=dto.BaseImponible;
                codigoImpuestoDocumentoOp.MONTO_IMPUESTO = montoImpuesto;
                codigoImpuestoDocumentoOp.MONTO_IMPUESTO_EXENTO = dto.MontoImpuestoExento;
                codigoImpuestoDocumentoOp.MONTO_RETENIDO=montoImpuesto-dto.MontoImpuestoExento;
                codigoImpuestoDocumentoOp.EXTRA1 = "";
                codigoImpuestoDocumentoOp.EXTRA2 ="";
                codigoImpuestoDocumentoOp.EXTRA3 = "";



                codigoImpuestoDocumentoOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoImpuestoDocumentoOp.USUARIO_UPD = conectado.Usuario;
                codigoImpuestoDocumentoOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoImpuestoDocumentoOp);
                dto.MontoRetenido = (decimal)codigoImpuestoDocumentoOp.MONTO_RETENIDO;
                dto.MontoImpuesto = (decimal)codigoImpuestoDocumentoOp.MONTO_IMPUESTO;
                dto.MontoRetenido=(decimal)codigoImpuestoDocumentoOp.MONTO_RETENIDO;
                await ReplicaImpuestoAdmRetencionesOp(dto);
             
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


        public async Task<bool> EsValidoTotalBaseImpuestoVsBaseDocumentoCreate(int codigoDocumentoOp,decimal nuevoMontoImpuesto)
        {
            bool result = true;
            decimal totalImpuesto = 0;
           
            decimal totalBaseImpuesto = 0;
            var documentoOp = await _admDocumentosOpRepository.GetCodigoDocumentoOp(codigoDocumentoOp);
            if (documentoOp != null)
            {
              
                var impuestosDocumentosOp = await _repository.GetByDocumento(codigoDocumentoOp);
                if (impuestosDocumentosOp.Count() > 0)
                {
                    totalImpuesto = impuestosDocumentosOp.Sum(t => t.MONTO_IMPUESTO);
                   
                }
            }
    
            totalBaseImpuesto = totalImpuesto + nuevoMontoImpuesto;
            if (documentoOp != null && totalBaseImpuesto > documentoOp.BASE_IMPONIBLE)
            {
                result = false;
            }
            

          
            return result;

        }
        
        
        public async Task<bool> EsValidoTotalBaseImpuestoVsBaseDocumentoUpdate(int codigoDocumentoOp,int codigoImpuestoDocumeto,decimal nuevaBaseImponible)
        {
            bool result = true;
            decimal totalBaseImponibleImpuesto = 0;
            decimal totalBaseDocumento = 0;
            decimal totalBaseImpuesto = 0;
            var documentoOp = await _admDocumentosOpRepository.GetCodigoDocumentoOp(codigoDocumentoOp);
            if (documentoOp != null)
            {
                totalBaseDocumento = documentoOp.BASE_IMPONIBLE;
                var impuestosDocumentosOp = await _repository.GetByDocumento(codigoDocumentoOp);
                if (impuestosDocumentosOp.Count() > 0)
                {
                    totalBaseImponibleImpuesto = impuestosDocumentosOp.Where(x=>x.CODIGO_IMPUESTO_DOCUMENTO_OP!=codigoImpuestoDocumeto).Sum(t => t.BASE_IMPONIBLE);
                   
                }
            }
    
            totalBaseImpuesto = nuevaBaseImponible + totalBaseImponibleImpuesto;
            if (totalBaseImpuesto > totalBaseDocumento)
            {
                result = false;
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
                var retencion = await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
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
                
                var impuestoExiste =await _repository.GetByDocumentoCodigoRetencionTipoRetencion(dto.CodigoDocumentoOp,dto.CodigoRetencion,dto.TipoRetencionId);
                if (impuestoExiste!=null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe este impuesto en el documento";
                    return result;
                }

                if (dto.BaseImponible < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible invalida";
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

                var montoImpuesto=(dto.BaseImponible * (decimal)retencion.POR_RETENCION)/100;

                if (dto.MontoImpuestoExento > montoImpuesto)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Excento no puede ser mayor a el monto del impuesto";
                    return result;
                }

                var esValidoTotalBaseImpuestoVsBaseDocumento =
                   await EsValidoTotalBaseImpuestoVsBaseDocumentoCreate(dto.CodigoDocumentoOp, dto.BaseImponible);
                if (esValidoTotalBaseImpuestoVsBaseDocumento ==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Base no puede ser mayor a la base del Documento";
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
                entity.MONTO_IMPUESTO = montoImpuesto;
                entity.MONTO_IMPUESTO_EXENTO = dto.MontoImpuestoExento;
          
                entity.MONTO_RETENIDO=montoImpuesto-dto.MontoImpuestoExento;
                
                entity.EXTRA1 = "";
                entity.EXTRA2 = "";
                entity.EXTRA3 = "";



                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    dto.MontoRetenido = (decimal)entity.MONTO_RETENIDO;
                    dto.MontoImpuesto = (decimal)entity.MONTO_IMPUESTO;
                    dto.MontoRetenido=(decimal)entity.MONTO_RETENIDO;
                    await ReplicaImpuestoAdmRetencionesOp(dto);
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
                
                
                var documentoOp = await _admDocumentosOpRepository.GetCodigoDocumentoOp(codigoImpuestoDocumentoOp.CODIGO_DOCUMENTO_OP);
                
                var admRetencionesOp = await _admRetencionesOpRepository.GetByOrdenPagoCodigoRetencionTipoRetencion(documentoOp.CODIGO_ORDEN_PAGO,codigoImpuestoDocumentoOp.CODIGO_RETENCION,codigoImpuestoDocumentoOp.TIPO_RETENCION_ID);
                if (admRetencionesOp != null)
                {
                    await _admRetencionesOpRepository.Delete(admRetencionesOp.CODIGO_RETENCION_OP);
                }

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

