using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

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
        private readonly IPreCompromisosRepository _preCompromisosRepository;
        private readonly IAdmPucOrdenPagoRepository _admPucOrdenPagoRepository;
        private readonly IAdmBeneficariosOpService _admBeneficariosOpService;
        private readonly IAdmRetencionesOpRepository _admRetencionesOpRepository;

        public AdmCompromisoOpService(IAdmCompromisoOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmProveedoresRepository admProveedoresRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IPreCompromisosRepository preCompromisosRepository,
                                     IAdmPucOrdenPagoRepository admPucOrdenPagoRepository,
                                     IAdmBeneficariosOpService admBeneficariosOpService,
                                     IAdmRetencionesOpRepository admRetencionesOpRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admProveedoresRepository = admProveedoresRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _preCompromisosRepository = preCompromisosRepository;
            _admPucOrdenPagoRepository = admPucOrdenPagoRepository;
            _admBeneficariosOpService = admBeneficariosOpService;
            _admRetencionesOpRepository = admRetencionesOpRepository;
        }


        public async Task<string> GetCompromisosByOrdenPago(int codigoOrdenPago,int codigoPresupuesto)
        {
            string result = "";
            var compromisoOp = await _repository.GetCodigoOrdenPago(codigoOrdenPago,codigoPresupuesto);
            if (compromisoOp != null && compromisoOp.Count > 0)
            {

                foreach (var item in compromisoOp)
                {
                    result = result + "-" + item.CODIGO_IDENTIFICADOR;
                }
                
            }
            
            return result;

        }
        
        public async Task<ResultDto<List<AdmCompromisoOpResponseDto>>> GetByOrdenPago(int codigoOrdenPago,int codigoPresupuesto)
        {

            ResultDto<List<AdmCompromisoOpResponseDto>> result = new ResultDto<List<AdmCompromisoOpResponseDto>>(null);
            try
            {
                var compromisoOp = await _repository.GetCodigoOrdenPago(codigoOrdenPago,codigoPresupuesto);
              
                if (compromisoOp != null && compromisoOp.Count() > 0)
                {
                    var listDto =  MapListCompromisoOpDto(compromisoOp);

                    result.Data = await listDto;
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
        public async  Task<AdmCompromisoOpResponseDto> MapCompromisoOpDto(ADM_COMPROMISO_OP dtos)
        {
            AdmCompromisoOpResponseDto itemResult = new AdmCompromisoOpResponseDto();
            itemResult.CodigoCompromisoOp = dtos.CODIGO_COMPROMISO_OP;
            itemResult.OrigenCompromisoId = dtos.ORIGEN_COMPROMISO_ID;
            itemResult.OrigenDescripcion = "";
            var descriptivaOrigen = await _admDescriptivaRepository.GetByCodigo(itemResult.OrigenCompromisoId);
            if (descriptivaOrigen != null)
            {
                itemResult.OrigenDescripcion = descriptivaOrigen.DESCRIPCION;
            }
            itemResult.CodigoIdentificador = dtos.CODIGO_IDENTIFICADOR;
            itemResult.Numero = "";
            var compromiso = await _preCompromisosRepository.GetByCodigo(itemResult.CodigoIdentificador);
            if (compromiso != null)
            {
                itemResult.Numero = compromiso.NUMERO_COMPROMISO;
               
                itemResult.Fecha =  Fecha.GetFechaString(compromiso.FECHA_COMPROMISO.Date);
            }

            itemResult.Monto = 0;
           var pucOrdenPago=  await _admPucOrdenPagoRepository.GetByOrdenPago(dtos.CODIGO_ORDEN_PAGO);
           if (pucOrdenPago.Count > 0)
           {
               itemResult.Monto = pucOrdenPago.Sum(x => x.MONTO);
           }
            
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.CodigoPresupuesto = (int)dtos.CODIGO_PRESUPUESTO;

            if (dtos.CODIGO_VAL_CONTRATO == null) dtos.CODIGO_VAL_CONTRATO = 0;
            itemResult.CodigoValContrato = (int)dtos.CODIGO_VAL_CONTRATO;
            itemResult.Descripcion = "";
            return itemResult;
        }

        public  async Task<List<AdmCompromisoOpResponseDto>> MapListCompromisoOpDto(List<ADM_COMPROMISO_OP> dtos)
        {
            List<AdmCompromisoOpResponseDto> result = new List<AdmCompromisoOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await  MapCompromisoOpDto(item);

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
                    var listDto =  MapListCompromisoOpDto(compromisoOp);

                    result.Data = await listDto;
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
                codigoCompromisoOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoCompromisoOp.CODIGO_VAL_CONTRATO = dto.CodigoValContrato;
                codigoCompromisoOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoCompromisoOp.USUARIO_UPD = conectado.Usuario;
                codigoCompromisoOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoCompromisoOp);

                await ReplicaCompromisosEnAdmBeneficiariosOp(dto.CodigoOrdenPago);
                
                var resultDto =  MapCompromisoOpDto(codigoCompromisoOp);
                result.Data = await resultDto;
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
                if (dto.OrigenCompromisoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "origen compromiso Id invalido";
                    return result;
                }
                var origenCompromiso = await _admDescriptivaRepository.GetByCodigo(dto.OrigenCompromisoId);
                if (origenCompromiso==null)
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

            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            entity.CODIGO_VAL_CONTRATO = dto.CodigoValContrato;

            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                await ReplicaCompromisosEnAdmBeneficiariosOp(dto.CodigoOrdenPago);
                var resultDto =  MapCompromisoOpDto(created.Data);
                result.Data = await resultDto;
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
        public async Task ReplicaCompromisosEnAdmBeneficiariosOp(int codigoOrdenPago)
        {

            var ordenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(codigoOrdenPago);
            decimal totalMontoRetencion = 0;
            var pucOrdenPago = await _admPucOrdenPagoRepository.GetByOrdenPago(codigoOrdenPago);
            if (pucOrdenPago.Count > 0)
            {
                // Calcular el total del Impuesto
                   var totalMontoPuc=pucOrdenPago.Sum(t => t.MONTO);
                   var retencionesOp = await _admRetencionesOpRepository.GetByOrdenPago(codigoOrdenPago);
                   if (retencionesOp.Count > 0)
                   {
                       // Calcular el total del Impuesto
                       totalMontoRetencion = (decimal)retencionesOp.Sum(t => t.MONTO_RETENCION);
                   }

                   var proveedor = ordenPago.CODIGO_PROVEEDOR;
                    
                    var admBeneficiario = await _admBeneficariosOpService.GetByOrdenPagoProveedor(codigoOrdenPago,proveedor);
                    if (admBeneficiario != null)
                    {
                     
                        
                        AdmBeneficiariosOpUpdateDto dto = new AdmBeneficiariosOpUpdateDto();
                        dto.CodigoBeneficiarioOp = admBeneficiario.CODIGO_BENEFICIARIO_OP;
                        dto.CodigoPresupuesto = ordenPago.CODIGO_PRESUPUESTO;
                        dto.CodigoOrdenPago=codigoOrdenPago;
                        dto.CodigoProveedor = admBeneficiario.CODIGO_PROVEEDOR;
                        dto.Monto= (decimal)totalMontoPuc-totalMontoRetencion;
                        dto.MontoAnulado = admBeneficiario.MONTO_ANULADO;
                        dto.MontoPagado = admBeneficiario.MONTO_PAGADO;
                        await _admBeneficariosOpService.Update(dto);
                        
                    }
                    else
                    {
                        AdmBeneficiariosOpUpdateDto dto = new AdmBeneficiariosOpUpdateDto();
                        dto.CodigoBeneficiarioOp = 0;
                        dto.CodigoPresupuesto = ordenPago.CODIGO_PRESUPUESTO;
                        dto.CodigoOrdenPago=codigoOrdenPago;
                        dto.CodigoProveedor = proveedor;
                        dto.Monto= (decimal)totalMontoPuc-totalMontoRetencion;
                        dto.MontoAnulado = 0;
                        dto.MontoPagado = 0;
                        await _admBeneficariosOpService.Create(dto);

                    }
                
            }
 
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
                
                await ReplicaCompromisosEnAdmBeneficiariosOp(codigoCompromisoOp.CODIGO_ORDEN_PAGO);
                
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

