using System.DirectoryServices;
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
        private readonly IAdmBeneficiariosOpRepository _admBeneficiariosOpRepository;
        private readonly IAdmOrdenPagoRepository _ordenPagoRepository;

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
                                      IOssConfigRepository ossConfigRepository,
                                      IAdmBeneficiariosOpRepository admBeneficiariosOpRepository,
                                      IAdmOrdenPagoRepository ordenPagoRepository
                                    )
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
            _admBeneficiariosOpRepository = admBeneficiariosOpRepository;
            _ordenPagoRepository = ordenPagoRepository;
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

            itemResult.NumeroCuentaProveedor = dtos.NUMERO_CUENTA;
            itemResult.CodigoPresupuesto = (int)dtos.CODIGO_PRESUPUESTO;
           
            //Data Beneficiario Pagos
            var beneficiario= await _beneficiariosPagosRepository.GetByPago(dtos.CODIGO_CHEQUE);
            if (beneficiario != null)
            {
                itemResult.CodigoBeneficiarioPago = beneficiario.CODIGO_BENEFICIARIO_CH;
                itemResult.CodigoBeneficiarioOP = (int)beneficiario.CODIGO_BENEFICIARIO_OP;
                if(beneficiario.CODIGO_ORDEN_PAGO==null) beneficiario.CODIGO_ORDEN_PAGO = 0;
                if(beneficiario.NUMERO_ORDEN_PAGO==null) beneficiario.NUMERO_ORDEN_PAGO = "";
                itemResult.CodigoOrdenPago=(int)beneficiario.CODIGO_ORDEN_PAGO;
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

        public async Task<string> ValidarMontoPago(int codigoBeneficiarioOp, decimal montoPagado)
        {

           var result = "";
            var beneficiario = await _admBeneficiariosOpRepository.GetCodigoBeneficiarioOp(codigoBeneficiarioOp);

            if (beneficiario != null)
            {
                var pendiente = beneficiario.MONTO - beneficiario.MONTO_PAGADO;
                if (pendiente < montoPagado)
                {
                    result = $"{montoPagado} es Mayor al monto Pendiente de la Orden de pago:{pendiente}";
                }
                
            }
            return result;
        }
        public async Task<ResultDto<bool>> UpdateMonto(PagoUpdateMontoDto dto)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            try
            {
                var beneficiario = await _beneficiariosPagosRepository.GetCodigoBeneficiarioPago(dto.CodigoBeneficiarioPago);
                if (beneficiario == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "No existen beneficiario op";
                    return result;
                }
                var pago = await _repository.GetByCodigoCheque(beneficiario.CODIGO_CHEQUE);
                if (pago == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Codigo de Pago No Existe";
                    return result;
                }

                
                if (dto.Monto<=0)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = $"Monto Invalido";
                    return result;
                }
                
                var esModificable=await PagoEsModificable((int)pago.CODIGO_LOTE_PAGO);
                if (esModificable.Length > 0)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message =esModificable;
                    return result;
                }
                
                var beneficiarioOp =
                    await _admBeneficiariosOpRepository.GetCodigoBeneficiarioOp((int)beneficiario.CODIGO_BENEFICIARIO_OP);
                if (beneficiarioOp != null)
                {
                    
                    var validarMontoPago = await ValidarMontoPago((int)beneficiario.CODIGO_BENEFICIARIO_OP, dto.Monto);

                    if (validarMontoPago.Length>0)
                    {
                        result.Data = false;
                        result.IsValid = false;
                        result.Message = validarMontoPago;
                        return result;
                    }
                }

           
                beneficiario.MONTO = dto.Monto;
                var conectado = await _sisUsuarioRepository.GetConectado();
                beneficiario.USUARIO_UPD=conectado.Usuario;
                beneficiario.FECHA_UPD = DateTime.Now;
                var updated = await _beneficiariosPagosRepository.Update(beneficiario);
                if (updated.IsValid && updated.Data != null)
                {
                    var totalPagado =
                        await _beneficiariosPagosRepository.GetTotalPagadoCodigoBeneficiarioOp(
                            (int)beneficiario.CODIGO_BENEFICIARIO_OP);
                    await _admBeneficiariosOpRepository.UpdateMontoPagado((int)beneficiario.CODIGO_BENEFICIARIO_OP, totalPagado); 

                    result.Data = true;
                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = false;
                    result.IsValid = updated.IsValid;
                    result.Message = updated.Message;
                }

                return result;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = e.Message;
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

        public async Task<string> PagoEsModificable(int codigoLote)
        {
            var result = "";
            var lote = await _admLotePagoRepository.GetByCodigo(codigoLote);
            if (lote == null)
            {
               
                result = "Lote no encontrado";
                return result;
            }

            if (lote.STATUS == "AP")
            {
                result = "Lote Con Estatus APROBADO, do puede ser Modificado";
                return result;
            }
            
            return result;
        }
        
        public async Task<bool> CreateBeneficiarioPago( PagoCreateDto dto,int codigoPresupuesto,int codigoPago)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            try
            {
                ADM_BENEFICIARIOS_CH entity = new ADM_BENEFICIARIOS_CH();
                entity.CODIGO_BENEFICIARIO_CH = await _beneficiariosPagosRepository.GetNextKey();
                entity.CODIGO_CHEQUE = codigoPago;
                entity.CODIGO_BENEFICIARIO_OP = dto.CodigoBeneficiarioOP;
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.NUMERO_ORDEN_PAGO = dto.NumeroOrdenPago;
                entity.MONTO = dto.Monto;
                entity.MONTO_ANULADO = 0;
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.CODIGO_PRESUPUESTO = codigoPresupuesto;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;
                await _beneficiariosPagosRepository.Add(entity);
                var totalPagado =
                    await _beneficiariosPagosRepository.GetTotalPagadoCodigoBeneficiarioOp(
                        (int)dto.CodigoBeneficiarioOP);
                await _admBeneficiariosOpRepository.UpdateMontoPagado((int)dto.CodigoBeneficiarioOP, totalPagado); 

                return true;
            }
            catch (Exception e)
            {
               return false;
            }
          


        }

        public async Task<ResultDto<PagoResponseDto>> Update(PagoUpdateDto dto)
        {
            ResultDto<PagoResponseDto> result = new ResultDto<PagoResponseDto>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            try
            {
                var pago = await _repository.GetByCodigoCheque(dto.CodigoPago);
                if (pago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo de Pago No Existe";
                    return result;
                }

                var esModificable=await PagoEsModificable((int)pago.CODIGO_LOTE_PAGO);
                if (esModificable.Length > 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message =esModificable;
                    return result;
                }
                
                var beneficiario = await _beneficiariosPagosRepository.GetCodigoBeneficiarioPago(dto.CodigoBeneficiarioPago);
                if (beneficiario == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existen beneficiario op";
                    return result;
                }

                if (string.IsNullOrEmpty(dto.Motivo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }


                if (dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;

                }
                var validarMontoPago = await ValidarMontoPago((int)beneficiario.CODIGO_BENEFICIARIO_OP, dto.Monto);

                if (validarMontoPago.Length>0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = validarMontoPago;
                    return result;
                }
                
                
                PagoUpdateMontoDto pagoUpdateMontoDto = new PagoUpdateMontoDto();
                pagoUpdateMontoDto.CodigoBeneficiarioPago = dto.CodigoBeneficiarioPago;
                pagoUpdateMontoDto.Monto = dto.Monto;
                var resultUpdateMonto =await  UpdateMonto(pagoUpdateMontoDto);
                if (resultUpdateMonto.IsValid == false)
                {
                    result.Data = null;
                    result.IsValid = resultUpdateMonto.IsValid;
                    result.Message = resultUpdateMonto.Message;
                    return result;
                }
                
                pago.MOTIVO=dto.Motivo;
                pago.FECHA_UPD = DateTime.Now;
                pago.USUARIO_UPD = conectado.Usuario;
              
                await _repository.Update(pago);
             
                var resultDto = await MapChequesDto(pago);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
                return result;

            }
            catch (Exception e)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = e.Message;
                return result;
            }
            
        }

        public async Task<ResultDto<bool>> Delete(PagoDeleteDto dto)
        {
            int codigoOrdenPago = 0;
            ResultDto<bool> result = new ResultDto<bool>(false);
            try
            {
                var pago = await _repository.GetByCodigoCheque(dto.CodigoPago);
                if (pago == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Codigo de Pago No Existe";
                    return result;
                }
                var beneficiario = await _beneficiariosPagosRepository.GetByPago(dto.CodigoPago);
                if (beneficiario is not null)
                {
                    codigoOrdenPago = (int)beneficiario.CODIGO_ORDEN_PAGO;
                }
                 var esModificable=await PagoEsModificable((int)pago.CODIGO_LOTE_PAGO);
                if (esModificable.Length > 0)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message =esModificable;
                    return result;
                }
                
                var deleted = await _repository.Delete(dto.CodigoPago);
                if (deleted.Length > 0)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message =deleted;
                    return result;
                }


                var totalPagado =
                    await _beneficiariosPagosRepository.GetTotalPagadoCodigoBeneficiarioOp(
                        (int)beneficiario.CODIGO_BENEFICIARIO_OP);
                    await _admBeneficiariosOpRepository.UpdateMontoPagado((int)beneficiario.CODIGO_BENEFICIARIO_OP, totalPagado); 

            
               
                result.Data = true;
                result.IsValid = true;
                result.Message ="";
                return result;
                
                
                
            }
            catch (Exception e)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = e.Message;
                return result;
            }
        }
        public async Task<ResultDto<PagoResponseDto>> Create(PagoCreateDto dto)
        {
            ResultDto<PagoResponseDto> result = new ResultDto<PagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var lote = await _admLotePagoRepository.GetByCodigo(dto.CodigoLote);
                if (lote == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote No Existe";
                    return result;
                }
              
                var beneficiarioOp= await _admBeneficiariosOpRepository.GetCodigoBeneficiarioOp(dto.CodigoBeneficiarioOP);
                if (beneficiarioOp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existen beneficiario op";
                    return result;
                }
                
                var validarMontoPago = await ValidarMontoPago(dto.CodigoBeneficiarioOP, dto.Monto);

                if (validarMontoPago.Length>0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = validarMontoPago;
                    return result;
                }
                
                
                var proveedor = await _proveedoresRepository.GetByCodigo(beneficiarioOp.CODIGO_PROVEEDOR);
                if (proveedor==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }
                /*var cuenta=await _sisCuentaBancoRepository.GetByCodigoCuenta(proveedor.NUMERO_CUENTA);
                if (cuenta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta Banco invalido";
                    return result;
                }*/
                
                var ordenPago= await _ordenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (ordenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe Orden Pago";
                    return result;
                }

                dto.NumeroOrdenPago = ordenPago.NUMERO_ORDEN_PAGO;



                if (string.IsNullOrEmpty(dto.Motivo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }


                if (dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;

                }
                
                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, (int)lote.CODIGO_PRESUPUESTO);
                if (presupuesto==null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

            
            

                var tipoChequeID = await _admDescriptivaRepository.GetByCodigo(lote.TIPO_PAGO_ID);
                if (tipoChequeID==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Pago Invalido";
                    return result;
                }

                int numeroChequera = 0;
                int numeroCheque = 0;
                var ossConfigChequera = await _ossConfigRepository.GetByClave(lote.TIPO_PAGO_ID.ToString());
                if (ossConfigChequera != null)
                {
                    numeroChequera = int.Parse(ossConfigChequera.VALOR);
                }
                numeroCheque= await _repository.GetNextCheque(numeroChequera,(int)lote.CODIGO_PRESUPUESTO);
                
                ADM_CHEQUES entity = new ADM_CHEQUES();
                entity.CODIGO_LOTE_PAGO = dto.CodigoLote;
                entity.CODIGO_CHEQUE = await _repository.GetNextKey();
                entity.ANO = presupuesto.ANO;
                entity.CODIGO_CUENTA_BANCO = lote.CODIGO_CUENTA_BANCO;
                entity.NUMERO_CHEQUERA =numeroChequera;
                entity.NUMERO_CHEQUE = numeroCheque;
                entity.FECHA_CHEQUE = lote.FECHA_PAGO;
                entity.CODIGO_PROVEEDOR = beneficiarioOp.CODIGO_PROVEEDOR;
                entity.PRINT_COUNT = 0;
                entity.MOTIVO = dto.Motivo;
                entity.STATUS = "PE";
                entity.ENDOSO = "S";
                entity.CODIGO_PRESUPUESTO = lote.CODIGO_PRESUPUESTO;
                entity.TIPO_CHEQUE_ID =lote.TIPO_PAGO_ID;
                entity.NUMERO_CUENTA = proveedor.NUMERO_CUENTA;
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                await CreateBeneficiarioPago(dto,(int)lote.CODIGO_PRESUPUESTO, entity.CODIGO_CHEQUE);
                var resultDto = await MapChequesDto(created.Data);
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

        
        
    }
 }

