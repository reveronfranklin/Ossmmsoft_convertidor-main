using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Pagos;
using Convertidor.Utility;

namespace Convertidor.Services.Adm.Pagos
{
    public class AdmPagosService : IAdmPagosService
    {
        private readonly IAdmChequesRepository _repository;
        private readonly IAdmProveedoresRepository _proveedoresRepository;
        private readonly IAdmContactosProveedorRepository _contactosProveedorRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly ISisCuentaBancoRepository _sisCuentaBancoRepository;
        private readonly ISisBancoRepository _sisBancoRepository;
        private readonly IAdmLotePagoRepository _admLotePagoRepository;
        private readonly IAdmBeneficiariosPagosRepository _beneficiariosPagosRepository;
        private readonly IOssConfigRepository _ossConfigRepository;

        public AdmPagosService( IAdmChequesRepository repository,
                                      IAdmProveedoresRepository proveedoresRepository,
                                      IAdmContactosProveedorRepository contactosProveedorRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                      IAdmDescriptivaRepository admDescriptivaRepository,
                                      ISisCuentaBancoRepository sisCuentaBancoRepository,
                                      ISisBancoRepository sisBancoRepository,
                                      IAdmLotePagoRepository admLotePagoRepository,
                                        IAdmBeneficiariosPagosRepository beneficiariosPagosRepository,
                                      IOssConfigRepository ossConfigRepository)
        {
            _repository = repository;
            _proveedoresRepository = proveedoresRepository;
            _contactosProveedorRepository = contactosProveedorRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _sisCuentaBancoRepository = sisCuentaBancoRepository;
            _sisBancoRepository = sisBancoRepository;
            _admLotePagoRepository = admLotePagoRepository;
            _beneficiariosPagosRepository = beneficiariosPagosRepository;
            _ossConfigRepository = ossConfigRepository;
        }

       

        public async Task<PagoResponseDto> MapChequesDto(ADM_CHEQUES dtos)
        {
            PagoResponseDto itemResult = new PagoResponseDto();

            itemResult.CodigoLote = (int)dtos.CODIGO_LOTE_PAGO;
            itemResult.CodigoPago = dtos.CODIGO_CHEQUE;
            itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
            var cuenta = await _sisCuentaBancoRepository.GetByCodigo(dtos.CODIGO_CUENTA_BANCO);
            if (cuenta!=null)
            {
                itemResult.NroCuenta = cuenta.NO_CUENTA;
                itemResult.CodigoBanco = cuenta.CODIGO_BANCO;
                itemResult.NombreBanco = "";
                var banco = await _sisBancoRepository.GetByCodigo(cuenta.CODIGO_BANCO);
                if (banco != null)
                {
                    itemResult.NombreBanco = banco.NOMBRE;
                }
            }


          
            itemResult.FechaPago = dtos.FECHA_CHEQUE;
            itemResult.FechaPagoString = Fecha.GetFechaString(dtos.FECHA_CHEQUE);
            FechaDto fechaChequeObj = Fecha.GetFechaDto(dtos.FECHA_CHEQUE);
            itemResult.FechaPagoObj =(FechaDto) fechaChequeObj;
       
         
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.NombreProveedor = "";
            var proveedor = await _proveedoresRepository.GetByCodigo(dtos.CODIGO_PROVEEDOR);
            if(proveedor!=null)
            {
                itemResult.NombreProveedor = proveedor.NOMBRE_PROVEEDOR;
            }
            
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.Status = dtos.STATUS;
        

            itemResult.TipoChequeID = (int)dtos.TIPO_CHEQUE_ID;
            itemResult.DescripcionTipoCheque = "";
            var descripcionTipoCheque = await _admDescriptivaRepository.GetByCodigo(itemResult.TipoChequeID);
            if (descripcionTipoCheque != null)
            {
                itemResult.DescripcionTipoCheque = descripcionTipoCheque.DESCRIPCION;
            }
            
            if (dtos.FECHA_ENTREGA != null)
            {
                itemResult.FechaEntrega =dtos.FECHA_ENTREGA;
                itemResult.FechaEntregaString =Fecha.GetFechaString((DateTime)dtos.FECHA_ENTREGA);
                FechaDto fechaEntregaObj = Fecha.GetFechaDto((DateTime)dtos.FECHA_ENTREGA);
                itemResult.FechaEntregaObj = (FechaDto) fechaEntregaObj;
            }
            itemResult.CodigoPresupuesto = (int)dtos.CODIGO_PRESUPUESTO;
           
            //Data Beneficiario Pagos
            var beneficiario= await _beneficiariosPagosRepository.GetByPago(dtos.CODIGO_CHEQUE);
            if (beneficiario != null)
            {
                itemResult.CodigoBeneficiarioPago = beneficiario.CODIGO_BENEFICIARIO_CH;
                itemResult.CodigoBeneficiarioOP = beneficiario.CODIGO_BENEFICIARIO_OP;
                if(beneficiario.CODIGO_ORDEN_PAGO==null) beneficiario.CODIGO_ORDEN_PAGO = 0;
                if(beneficiario.NUMERO_ORDEN_PAGO==null) beneficiario.NUMERO_ORDEN_PAGO = "";
                itemResult.CodigoOrdenPago=beneficiario.CODIGO_ORDEN_PAGO;
                itemResult.NumeroOrdenPago = beneficiario.NUMERO_ORDEN_PAGO;
                itemResult.Monto = beneficiario.MONTO;
                itemResult.MontoAnulado = beneficiario.MONTO_ANULADO;
                
            }
           


            return itemResult;
        }

        public async Task<List<PagoResponseDto>> MapListChequesDto(List<ADM_CHEQUES> dtos)
        {
            List<PagoResponseDto> result = new List<PagoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapChequesDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<PagoResponseDto>>> GetByLote(AdmChequeFilterDto dto)
        {

            ResultDto<List<PagoResponseDto>> result = new ResultDto<List<PagoResponseDto>>(null);
            try
            {
                await _repository.UpdateSearchText(dto.CodigoLote);
                var pagos = await _repository.GetByLote(dto);
             
                if (pagos.Data != null && pagos.Data.Count() > 0)
                {
                    var listDto = await MapListChequesDto(pagos.Data);

                    result.Data = listDto;
                    result.IsValid = true;
                    result.CantidadRegistros = pagos.CantidadRegistros;
                    result.Page = pagos.Page;
                    result.TotalPage= pagos.TotalPage;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.CantidadRegistros = 0;
                    result.Page = 0;
                    result.TotalPage= 0;
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

