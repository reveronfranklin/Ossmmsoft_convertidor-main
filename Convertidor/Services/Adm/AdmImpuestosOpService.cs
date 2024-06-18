using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmImpuestosOpService : IAdmImpuestosOpService
    {
        private readonly IAdmImpuestosOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
        private readonly IPRE_PRESUPUESTOSRepository _preSUPUESTOSRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmImpuestosOpService(IAdmImpuestosOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     IPRE_PRESUPUESTOSRepository preSUPUESTOSRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _preSUPUESTOSRepository = preSUPUESTOSRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

        public async Task<AdmImpuestosOpResponseDto> MapImpuestosOpDto(ADM_IMPUESTOS_OP dtos)
        {
            AdmImpuestosOpResponseDto itemResult = new AdmImpuestosOpResponseDto();
            itemResult.CodigoImpuestoOp = dtos.CODIGO_IMPUESTO_OP;
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.TipoImpuestoId=dtos.TIPO_IMPUESTO_ID;
            itemResult.PorImpuesto = dtos.POR_IMPUESTO;
            itemResult.MontoImpuesto = dtos.MONTO_IMPUESTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
           
            
            return itemResult;
        }

        public async Task<List<AdmImpuestosOpResponseDto>> MapListImpuestosOpDto(List<ADM_IMPUESTOS_OP> dtos)
        {
            List<AdmImpuestosOpResponseDto> result = new List<AdmImpuestosOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapImpuestosOpDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmImpuestosOpResponseDto>>> GetAll()
        {

            ResultDto<List<AdmImpuestosOpResponseDto>> result = new ResultDto<List<AdmImpuestosOpResponseDto>>(null);
            try
            {
                var impuestosOp = await _repository.GetAll();
                var cant = impuestosOp.Count();
                if (impuestosOp != null && impuestosOp.Count() > 0)
                {
                    var listDto = await MapListImpuestosOpDto(impuestosOp);

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

        public async Task<ResultDto<AdmImpuestosOpResponseDto>> Update(AdmImpuestosOpUpdateDto dto)
        {
            ResultDto<AdmImpuestosOpResponseDto> result = new ResultDto<AdmImpuestosOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoImpuestoOp = await _repository.GetCodigoImpuestoOp(dto.CodigoImpuestoOp);
                if (codigoImpuestoOp == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo impuesto op no existe";
                    return result;
                }

                var codigoOrdenPagoTipoImpuestoId = await _repository.GetByOrdenDePagoTipoImpuesto(dto.CodigoOrdenPago, dto.TipoImpuestoId);
                if (dto.CodigoOrdenPago < 0 && dto.TipoImpuestoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago no existe";
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

                if (dto.PorImpuesto == null) 
                {
                    dto.PorImpuesto = 0;
                }

                if (dto.MontoImpuesto == null)
                {
                    dto.MontoImpuesto = 0;
                }

                if (dto.PorImpuesto > 0 && dto.MontoImpuesto > 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Si tiene Porcentaje de impuesto el monto impuesto debe ser 0";
                    return result;
                }

                if (dto.MontoImpuesto > 0 && dto.PorImpuesto > 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Si tiene Monto de impuesto el porcentaje impuesto debe ser 0";
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

                var codigoPresupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if(dto.CodigoPresupuesto < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo presupuesto invalido";
                    return result;
                }




                codigoImpuestoOp.CODIGO_IMPUESTO_OP = dto.CodigoImpuestoOp;
                codigoImpuestoOp.CODIGO_ORDEN_PAGO=dto.CodigoOrdenPago;
                codigoImpuestoOp.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                if(dto.PorImpuesto < 0) 
                {
                   codigoImpuestoOp.POR_IMPUESTO = dto.PorImpuesto;
                   codigoImpuestoOp.MONTO_IMPUESTO=null;
                }
                if(dto.MontoImpuesto < 0) 
                {
                   codigoImpuestoOp.MONTO_IMPUESTO = dto.MontoImpuesto;
                   codigoImpuestoOp.POR_IMPUESTO = null;
                }
                codigoImpuestoOp.EXTRA1 = dto.Extra1;
                codigoImpuestoOp.EXTRA2 = dto.Extra2;
                codigoImpuestoOp.EXTRA3 = dto.Extra3;
                codigoImpuestoOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


                codigoImpuestoOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoImpuestoOp.USUARIO_UPD = conectado.Usuario;
                codigoImpuestoOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoImpuestoOp);

                var resultDto = await MapImpuestosOpDto(codigoImpuestoOp);
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

        public async Task<ResultDto<AdmImpuestosOpResponseDto>> Create(AdmImpuestosOpUpdateDto dto)
        {
            ResultDto<AdmImpuestosOpResponseDto> result = new ResultDto<AdmImpuestosOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoImpuestoOp = await _repository.GetCodigoImpuestoOp(dto.CodigoImpuestoOp);
                if (codigoImpuestoOp != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo impuesto op ya existe";
                    return result;
                }

                
                var codigoOrdenPagoTipoImpuestoId = await _repository.GetByOrdenDePagoTipoImpuesto(dto.CodigoOrdenPago,dto.TipoImpuestoId);
                if (dto.CodigoOrdenPago < 0 && dto.TipoImpuestoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago no existe";
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
                if (dto.PorImpuesto == null)
                {
                    dto.PorImpuesto = 0;

                }
               
                if (dto.MontoImpuesto == null)
                {
                    dto.MontoImpuesto = 0;
                }

                if (dto.PorImpuesto > 0 && dto.MontoImpuesto > 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Si tiene Porcentaje de impuesto el monto impuesto debe ser 0";
                    return result;
                }

                if (dto.MontoImpuesto > 0 && dto.PorImpuesto > 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Si tiene Monto de impuesto el porcentaje impuesto debe ser 0";
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

                var codigoPresupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo presupuesto invalido";
                    return result;
                }


                ADM_IMPUESTOS_OP entity = new ADM_IMPUESTOS_OP();
                entity.CODIGO_IMPUESTO_OP = await _repository.GetNextKey();
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                if(dto.PorImpuesto > 0) 
                {
                    entity.POR_IMPUESTO = dto.PorImpuesto;
                    entity.MONTO_IMPUESTO = null;
                }
                if(dto.MontoImpuesto > 0) 
                {
                    entity.MONTO_IMPUESTO = dto.MontoImpuesto;
                    entity.POR_IMPUESTO = null;
                }
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
                    var resultDto = await MapImpuestosOpDto(created.Data);
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

        public async Task<ResultDto<AdmImpuestosOpDeleteDto>> Delete(AdmImpuestosOpDeleteDto dto) 
        {
            ResultDto<AdmImpuestosOpDeleteDto> result = new ResultDto<AdmImpuestosOpDeleteDto>(null);
            try
            {

                var codigoImpuestoOp = await _repository.GetCodigoImpuestoOp(dto.CodigoImpuestoOp);
                if (codigoImpuestoOp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Impuesto op no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoImpuestoOp);

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

