using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Sis;
using MathNet.Numerics.RootFinding;

namespace Convertidor.Services.Adm
{
    public class AdmCompromisoOpService : IAdmCompromisoOpService
    {
        private readonly IAdmCompromisoOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmCompromisoOpService(IAdmCompromisoOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmProveedoresRepository admProveedoresRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admProveedoresRepository = admProveedoresRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

        
        public async Task<AdmCompromisoOpResponseDto> MapCompromisoOpDto(ADM_COMPROMISO_OP dtos)
        {
            AdmCompromisoOpResponseDto itemResult = new AdmCompromisoOpResponseDto();
            itemResult.CodigoCompromisoOp = dtos.CODIGO_COMPROMISO_OP;
            itemResult.OrigenCompromisoId = dtos.ORIGEN_COMPROMISO_ID;
            itemResult.CodigoIdentificador = dtos.CODIGO_IDENTIFICADOR;
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.CodigoValContrato = dtos.CODIGO_VAL_CONTRATO;

            return itemResult;
        }

        public async Task<List<AdmCompromisoOpResponseDto>> MapListCompromisoOpDto(List<ADM_COMPROMISO_OP> dtos)
        {
            List<AdmCompromisoOpResponseDto> result = new List<AdmCompromisoOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapCompromisoOpDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmCompromisoOpResponseDto>>> GetAll()
        {

            ResultDto<List<AdmCompromisoOpResponseDto>> result = new ResultDto<List<AdmCompromisoOpResponseDto>>(null);
            try
            {
                var compromisoOp = await _repository.GetAll();
                var cant = compromisoOp.Count();
                if (compromisoOp != null && compromisoOp.Count() > 0)
                {
                    var listDto = await MapListCompromisoOpDto(compromisoOp);

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
        public async Task<ResultDto<AdmCompromisoOpResponseDto>> Update(AdmCompromisoOpUpdateDto dto)
        {
            ResultDto<AdmCompromisoOpResponseDto> result = new ResultDto<AdmCompromisoOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoCompromisoOp = await _repository.GetCodigoCompromisoOp(dto.CodigoCompromisoOp);
                if (codigoCompromisoOp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo compromiso op no existe";
                    return result;
                }
                var origenCompromisoId = await _admDescriptivaRepository.GetByIdAndTitulo(3,dto.OrigenCompromisoId);
                if (dto.OrigenCompromisoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "origen compromiso Id invalido";
                    return result;
                }

                if (dto.CodigoIdentificador < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Identificador invalido";
                    return result;
                }

                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if(dto.CodigoOrdenPago < 0) 
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
                    result.Message = "Codigo Proveedor Invalido";
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

                if(dto.CodigoValContrato < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo val contrato Invalido";
                    return result;
                }
               

                codigoCompromisoOp.CODIGO_COMPROMISO_OP = dto.CodigoCompromisoOp;
                codigoCompromisoOp.ORIGEN_COMPROMISO_ID = dto.OrigenCompromisoId;
                codigoCompromisoOp.CODIGO_IDENTIFICADOR = dto.CodigoIdentificador;
                codigoCompromisoOp.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                codigoCompromisoOp.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                codigoCompromisoOp.EXTRA1 = dto.Extra1;
                codigoCompromisoOp.EXTRA2 = dto.Extra2;
                codigoCompromisoOp.EXTRA3 = dto.Extra3;
                codigoCompromisoOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoCompromisoOp.CODIGO_VAL_CONTRATO = dto.CodigoValContrato;




                codigoCompromisoOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoCompromisoOp.USUARIO_UPD = conectado.Usuario;
                codigoCompromisoOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoCompromisoOp);

                var resultDto = await MapCompromisoOpDto(codigoCompromisoOp);
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

        public async Task<ResultDto<AdmCompromisoOpResponseDto>> Create(AdmCompromisoOpUpdateDto dto)
        {
            ResultDto<AdmCompromisoOpResponseDto> result = new ResultDto<AdmCompromisoOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoCompromisoOp = await _repository.GetCodigoCompromisoOp(dto.CodigoCompromisoOp);
                if (codigoCompromisoOp != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo compromiso op ya existe";
                    return result;
                }
                var origenCompromisoId = await _admDescriptivaRepository.GetByIdAndTitulo(3, dto.OrigenCompromisoId);
                if (dto.OrigenCompromisoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "origen compromiso Id invalido";
                    return result;
                }

                if (dto.CodigoIdentificador < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Identificador invalido";
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
                    result.Message = "Codigo Proveedor Invalido";
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

                if (dto.CodigoValContrato < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo val contrato Invalido";
                    return result;
                }


            ADM_COMPROMISO_OP entity = new ADM_COMPROMISO_OP();
            entity.CODIGO_COMPROMISO_OP = await _repository.GetNextKey();
            entity.ORIGEN_COMPROMISO_ID = dto.OrigenCompromisoId;
            entity.CODIGO_IDENTIFICADOR = dto.CodigoIdentificador;
            entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
            entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            entity.CODIGO_VAL_CONTRATO = dto.CodigoValContrato;
           


            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapCompromisoOpDto(created.Data);
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

        public async Task<ResultDto<AdmCompromisoOpDeleteDto>> Delete(AdmCompromisoOpDeleteDto dto) 
        {
            ResultDto<AdmCompromisoOpDeleteDto> result = new ResultDto<AdmCompromisoOpDeleteDto>(null);
            try
            {

                var codigoCompromisoOp = await _repository.GetCodigoCompromisoOp(dto.CodigoCompromisoOp);
                if (codigoCompromisoOp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoCompromisoOp);

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

