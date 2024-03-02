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
    public class AdmComprobantesDocumentosOpService : IAdmComprobantesDocumentosOpService
    {
        private readonly IAdmComprobantesDocumentosOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IAdmDocumentosOpRepository _admDocumentosOpRepository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
       

        public AdmComprobantesDocumentosOpService(IAdmComprobantesDocumentosOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmProveedoresRepository admProveedoresRepository,
                                     IAdmDocumentosOpRepository admDocumentosOpRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admProveedoresRepository = admProveedoresRepository;
            _admDocumentosOpRepository = admDocumentosOpRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
           
        }

        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);

            return FechaDesdeObj;
        }

        public async Task<AdmComprobantesDocumentosOpResponseDto> MapComprobantesDocumentosOpDto(ADM_COMPROBANTES_DOCUMENTOS_OP dtos)
        {
            AdmComprobantesDocumentosOpResponseDto itemResult = new AdmComprobantesDocumentosOpResponseDto();
            itemResult.CodigoComprobanteDocOp = dtos.CODIGO_COMPROBANTE_DOC_OP;
            itemResult.CodigoDocumentoOp = dtos.CODIGO_DOCUMENTO_OP;
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.CodigoProveedor=dtos.CODIGO_PROVEEDOR;
            itemResult.NumeroComprobante=dtos.NUMERO_COMPROBANTE;
            itemResult.FechaComprobante = dtos.FECHA_COMPROBANTE;
            itemResult.FechaComprobanteString = dtos.FECHA_COMPROBANTE.ToString("u");
            FechaDto fechaComprobanteObj = GetFechaDto( dtos.FECHA_COMPROBANTE);
            itemResult.FechaComprobanteObj = (FechaDto) fechaComprobanteObj;    
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            
            

            return itemResult;
        }

        public async Task<List<AdmComprobantesDocumentosOpResponseDto>> MapListComprobantesDocumentosOpDto(List<ADM_COMPROBANTES_DOCUMENTOS_OP> dtos)
        {
            List<AdmComprobantesDocumentosOpResponseDto> result = new List<AdmComprobantesDocumentosOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapComprobantesDocumentosOpDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmComprobantesDocumentosOpResponseDto>>> GetAll()
        {

            ResultDto<List<AdmComprobantesDocumentosOpResponseDto>> result = new ResultDto<List<AdmComprobantesDocumentosOpResponseDto>>(null);
            try
            {
                var comprobanteDocumentoOp = await _repository.GetAll();
                var cant = comprobanteDocumentoOp.Count();
                if (comprobanteDocumentoOp != null && comprobanteDocumentoOp.Count() > 0)
                {
                    var listDto = await MapListComprobantesDocumentosOpDto(comprobanteDocumentoOp);

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

        public async Task<ResultDto<AdmComprobantesDocumentosOpResponseDto>> Update(AdmComprobantesDocumentosOpUpdateDto dto)
        {
            ResultDto<AdmComprobantesDocumentosOpResponseDto> result = new ResultDto<AdmComprobantesDocumentosOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoComprobanteDocOp = await _repository.GetCodigoComprobanteDocOp(dto.CodigoComprobanteDocOp);
                if (codigoComprobanteDocOp == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo comprobante Doc op no existe";
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
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (dto.CodigoOrdenPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if(dto.CodigoProveedor < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo proveedor invalido";
                    return result;
                }

                if (dto.NumeroComprobante < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante invalido";
                    return result;
                }

                if (dto.FechaComprobante ==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha comprobante invalido";
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


                var codigopresupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                
                codigoComprobanteDocOp.CODIGO_COMPROBANTE_DOC_OP = dto.CodigoComprobanteDocOp;
                codigoComprobanteDocOp.CODIGO_DOCUMENTO_OP=dto.CodigoDocumentoOp;
                codigoComprobanteDocOp.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                codigoComprobanteDocOp.CODIGO_PROVEEDOR = dto.CodigoOrdenPago;
                codigoComprobanteDocOp.NUMERO_COMPROBANTE = dto.NumeroComprobante;
                codigoComprobanteDocOp.FECHA_COMPROBANTE = dto.FechaComprobante;
                codigoComprobanteDocOp.EXTRA1 = dto.Extra1;
                codigoComprobanteDocOp.EXTRA2 = dto.Extra2;
                codigoComprobanteDocOp.EXTRA3 = dto.Extra3;
                codigoComprobanteDocOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
              

                codigoComprobanteDocOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoComprobanteDocOp.USUARIO_UPD = conectado.Usuario;
                codigoComprobanteDocOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoComprobanteDocOp);

                var resultDto = await MapComprobantesDocumentosOpDto(codigoComprobanteDocOp);
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

        public async Task<ResultDto<AdmComprobantesDocumentosOpResponseDto>> Create(AdmComprobantesDocumentosOpUpdateDto dto)
        {
            ResultDto<AdmComprobantesDocumentosOpResponseDto> result = new ResultDto<AdmComprobantesDocumentosOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoComprobanteDocOp = await _repository.GetCodigoComprobanteDocOp(dto.CodigoComprobanteDocOp);
                if (codigoComprobanteDocOp != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo comprobante Doc op no existe";
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
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (dto.CodigoOrdenPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo proveedor invalido";
                    return result;
                }

                if (dto.NumeroComprobante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante invalido";
                    return result;
                }

                if (dto.FechaComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha comprobante invalido";
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


                var codigopresupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }


                ADM_COMPROBANTES_DOCUMENTOS_OP entity = new ADM_COMPROBANTES_DOCUMENTOS_OP();
                entity.CODIGO_COMPROBANTE_DOC_OP = await _repository.GetNextKey();
                entity.CODIGO_DOCUMENTO_OP = dto.CodigoDocumentoOp;
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.NUMERO_COMPROBANTE = dto.NumeroComprobante;
                entity.FECHA_COMPROBANTE = dto.FechaComprobante;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
           


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapComprobantesDocumentosOpDto(created.Data);
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

        public async Task<ResultDto<AdmComprobantesDocumentosOpDeleteDto>> Delete(AdmComprobantesDocumentosOpDeleteDto dto) 
        {
            ResultDto<AdmComprobantesDocumentosOpDeleteDto> result = new ResultDto<AdmComprobantesDocumentosOpDeleteDto>(null);
            try
            {

                var codigoComprobanteDocOp = await _repository.GetCodigoComprobanteDocOp(dto.CodigoComprobanteDocOp);
                if (codigoComprobanteDocOp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo comprobante doc op no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoComprobanteDocOp);

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

