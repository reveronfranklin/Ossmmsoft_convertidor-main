using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm.Pagos;
using Convertidor.Utility;

namespace Convertidor.Services.Adm.Pagos.AdmPagosService;

public partial class AdmPagosService
{
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

}