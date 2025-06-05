using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm.Pagos;

namespace Convertidor.Services.Adm.Pagos.AdmPagosService;

public partial class AdmPagosService
{
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
                
                if (lote.STATUS != "PE" )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Lote de pago no muede ser Modificado esta en estatus: {lote.STATUS}";
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
                await _admLotePagoRepository.UpdateSearchText((int)entity.CODIGO_LOTE_PAGO);
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