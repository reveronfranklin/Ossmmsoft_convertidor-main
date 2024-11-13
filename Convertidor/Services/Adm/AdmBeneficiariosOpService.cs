using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmBeneficiariosOpService : IAdmBeneficariosOpService
    {
        private readonly IAdmBeneficiariosOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
        private readonly IPRE_PRESUPUESTOSRepository _preSUPUESTOSRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
     

        public AdmBeneficiariosOpService(IAdmBeneficiariosOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     IPRE_PRESUPUESTOSRepository preSUPUESTOSRepository,
                                     IAdmProveedoresRepository admProveedoresRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _preSUPUESTOSRepository = preSUPUESTOSRepository;
            _admProveedoresRepository = admProveedoresRepository;
            
        }

        public async Task<AdmBeneficiariosOpResponseDto> MapBeneficiariosOpDto(ADM_BENEFICIARIOS_OP dtos)
        {
            AdmBeneficiariosOpResponseDto itemResult = new AdmBeneficiariosOpResponseDto();
            itemResult.CodigoBeneficiarioOp = dtos.CODIGO_BENEFICIARIO_OP;
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.CodigoProveedor=dtos.CODIGO_PROVEEDOR;
            itemResult.NombreProveedor = "";
            var proveedor = await _admProveedoresRepository.GetByCodigo(dtos.CODIGO_PROVEEDOR);
            if (proveedor != null)
            {
                itemResult.NombreProveedor = proveedor.NOMBRE_PROVEEDOR;
            }
          
            itemResult.Monto = dtos.MONTO;
            itemResult.MontoAnulado = dtos.MONTO_ANULADO;
            itemResult.MontoPagado = dtos.MONTO_PAGADO;
            itemResult.CodigoPresupuesto = (int)dtos.CODIGO_PRESUPUESTO;
           
            
            return itemResult;
        }

        public async Task<List<AdmBeneficiariosOpResponseDto>> MapListBeneficiariosOpDto(List<ADM_BENEFICIARIOS_OP> dtos)
        {
            List<AdmBeneficiariosOpResponseDto> result = new List<AdmBeneficiariosOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapBeneficiariosOpDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmBeneficiariosOpResponseDto>>> GetByOrdenPago(AdmOrdenPagoBeneficiarioFlterDto filter)
        {

            ResultDto<List<AdmBeneficiariosOpResponseDto>> result = new ResultDto<List<AdmBeneficiariosOpResponseDto>>(null);
            try
            {
                var beneficiariosOp = await _repository.GetByOrdenPago(filter.CodigoOrdenPago);
                var cant = beneficiariosOp.Count();
                if (beneficiariosOp != null && beneficiariosOp.Count() > 0)
                {
                    var listDto = await MapListBeneficiariosOpDto(beneficiariosOp);

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

        public async Task<ResultDto<AdmBeneficiariosOpResponseDto>> Update(AdmBeneficiariosOpUpdateDto dto)
        {
            ResultDto<AdmBeneficiariosOpResponseDto> result = new ResultDto<AdmBeneficiariosOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoBeneficiarioOp = await _repository.GetCodigoBeneficiarioOp(dto.CodigoBeneficiarioOp);
                if (codigoBeneficiarioOp == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Beneficiario no existe";
                    return result;
                }

                var ordenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (ordenPago==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago no existe";
                    return result;
                }


                var proveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (proveedor==null)
                {
                    
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo proveedor invalido";
                    return result;
                }

                if (dto.Monto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto invalido";
                    return result;
                }

                if (dto.MontoPagado < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Pagado invalido";
                    return result;
                }

                if (dto.MontoAnulado < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Anulado invalido";
                    return result;
                }

              
                var presupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if(presupuesto==null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo presupuesto invalido";
                    return result;
                }




                codigoBeneficiarioOp.CODIGO_BENEFICIARIO_OP = dto.CodigoBeneficiarioOp;
                codigoBeneficiarioOp.CODIGO_ORDEN_PAGO=dto.CodigoOrdenPago;
                codigoBeneficiarioOp.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                codigoBeneficiarioOp.MONTO=dto.Monto;
                codigoBeneficiarioOp.MONTO_PAGADO=dto.MontoPagado;
                codigoBeneficiarioOp.MONTO_ANULADO = dto.MontoAnulado;
           
                codigoBeneficiarioOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


                codigoBeneficiarioOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoBeneficiarioOp.USUARIO_UPD = conectado.Usuario;
                codigoBeneficiarioOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoBeneficiarioOp);

                var resultDto = await MapBeneficiariosOpDto(codigoBeneficiarioOp);
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

        public async Task<ResultDto<AdmBeneficiariosOpResponseDto>> Create(AdmBeneficiariosOpUpdateDto dto)
        {
            ResultDto<AdmBeneficiariosOpResponseDto> result = new ResultDto<AdmBeneficiariosOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoImpuestoOp = await _repository.GetCodigoBeneficiarioOp(dto.CodigoBeneficiarioOp);
                if (codigoImpuestoOp != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo beneficiario op ya existe";
                    return result;
                }

                
                var ordenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (ordenPago==null )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago no existe";
                    return result;
                }

                var proveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (proveedor==null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo proveedor invalido";
                    return result;
                }
                var proOrdenPagoProveedor = await _repository.GetByOrdenPagoProveedor(dto.CodigoOrdenPago,dto.CodigoProveedor);
                if (proOrdenPagoProveedor!=null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe este Proveedor para la orden de Pago";
                    return result;
                }
                

                if (dto.Monto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto invalido";
                    return result;
                }

                if (dto.MontoPagado < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Pagado invalido";
                    return result;
                }

                if (dto.MontoAnulado < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Anulado invalido";
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


                ADM_BENEFICIARIOS_OP entity = new ADM_BENEFICIARIOS_OP();
                entity.CODIGO_BENEFICIARIO_OP = await _repository.GetNextKey();
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.MONTO = dto.Monto;
                entity.MONTO_PAGADO = dto.MontoPagado;
                entity.MONTO_ANULADO = dto.MontoAnulado;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;



                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapBeneficiariosOpDto(created.Data);
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

        public async Task<ResultDto<AdmBeneficiariosOpDeleteDto>> Delete(AdmBeneficiariosOpDeleteDto dto) 
        {
            ResultDto<AdmBeneficiariosOpDeleteDto> result = new ResultDto<AdmBeneficiariosOpDeleteDto>(null);
            try
            {

                var codigoBeneficiarioOp = await _repository.GetCodigoBeneficiarioOp(dto.CodigoBeneficiarioOp);
                if (codigoBeneficiarioOp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Beneficiario op no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoBeneficiarioOp);

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

