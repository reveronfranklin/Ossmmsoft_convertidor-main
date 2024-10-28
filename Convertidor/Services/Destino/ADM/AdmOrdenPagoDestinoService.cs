
using Convertidor.Data.DestinoInterfaces.ADM;
using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Utility;


namespace Convertidor.Services.Destino.ADM
{
    public class AdmOrdenPagoDestinoService : IAdmOrdenPagoDestinoService
    {
        private readonly IAdmOrdenPagoRepository _repository;
        private readonly IAdmOrdenPagoDestinoRepository _destinoRepository;


        public AdmOrdenPagoDestinoService(IAdmOrdenPagoRepository repository,IAdmOrdenPagoDestinoRepository destinoRepository )
        {
            _repository = repository;
            _destinoRepository = destinoRepository;
        }

        public ADM_ORDEN_PAGO Map(Data.Entities.Adm.ADM_ORDEN_PAGO ordenPagoOrigen)
        {
           
            ADM_ORDEN_PAGO newOrden = new ADM_ORDEN_PAGO();
            newOrden.CODIGO_ORDEN_PAGO = ordenPagoOrigen.CODIGO_ORDEN_PAGO;
            newOrden.ANO  = ordenPagoOrigen.ANO;
            newOrden.CODIGO_COMPROMISO = ordenPagoOrigen.CODIGO_COMPROMISO;
            newOrden.CODIGO_ORDEN_COMPRA= ordenPagoOrigen.CODIGO_ORDEN_COMPRA;
            newOrden.CODIGO_CONTRATO = ordenPagoOrigen.CODIGO_CONTRATO;
            newOrden.CODIGO_PROVEEDOR= ordenPagoOrigen.CODIGO_PROVEEDOR;
            newOrden.NUMERO_ORDEN_PAGO = ordenPagoOrigen.NUMERO_ORDEN_PAGO;
            newOrden.REFERENCIA_ORDEN_PAGO = ordenPagoOrigen.REFERENCIA_ORDEN_PAGO;
            newOrden.FECHA_ORDEN_PAGO = ordenPagoOrigen.FECHA_ORDEN_PAGO;
            newOrden.TIPO_ORDEN_PAGO_ID = ordenPagoOrigen.TIPO_ORDEN_PAGO_ID;
            newOrden.FECHA_PLAZO_DESDE = ordenPagoOrigen.FECHA_PLAZO_DESDE;
            newOrden.FECHA_PLAZO_HASTA = ordenPagoOrigen.FECHA_PLAZO_HASTA;
            newOrden.CANTIDAD_PAGO = ordenPagoOrigen.CANTIDAD_PAGO;
            newOrden.NUMERO_PAGO= ordenPagoOrigen.NUMERO_PAGO;
            newOrden.FRECUENCIA_PAGO_ID= ordenPagoOrigen.FRECUENCIA_PAGO_ID;
            newOrden.TIPO_PAGO_ID = ordenPagoOrigen.TIPO_PAGO_ID;
            newOrden.NUMERO_VALUACION= ordenPagoOrigen.NUMERO_VALUACION;
            newOrden.STATUS = ordenPagoOrigen.STATUS;
            newOrden.MOTIVO = ordenPagoOrigen.MOTIVO;
            newOrden.EXTRA1 = ordenPagoOrigen.EXTRA1;
            newOrden.EXTRA2 = ordenPagoOrigen.EXTRA2;
            newOrden.EXTRA3 = ordenPagoOrigen.EXTRA3;
            newOrden.USUARIO_INS = ordenPagoOrigen.USUARIO_INS;
            newOrden.FECHA_INS = ordenPagoOrigen.FECHA_INS;
            newOrden.USUARIO_UPD = ordenPagoOrigen.USUARIO_UPD;
            newOrden.FECHA_UPD = ordenPagoOrigen.FECHA_UPD;
            newOrden.CODIGO_EMPRESA = ordenPagoOrigen.CODIGO_EMPRESA;
            newOrden.CODIGO_PRESUPUESTO = ordenPagoOrigen.CODIGO_PRESUPUESTO;
            newOrden.EXTRA4 = ordenPagoOrigen.EXTRA4;
            newOrden.EXTRA5 = ordenPagoOrigen.EXTRA5;
            newOrden.EXTRA6 = ordenPagoOrigen.EXTRA6;
            newOrden.EXTRA7 = ordenPagoOrigen.EXTRA7;
            newOrden.EXTRA8 = ordenPagoOrigen.EXTRA8;
            newOrden.EXTRA9 = ordenPagoOrigen.EXTRA9;
            newOrden.EXTRA10 = ordenPagoOrigen.EXTRA10;
            newOrden.EXTRA11  = ordenPagoOrigen.EXTRA11;
            newOrden.EXTRA12  = ordenPagoOrigen.EXTRA12;
            newOrden.EXTRA13  = ordenPagoOrigen.EXTRA13;
            newOrden.EXTRA14  = ordenPagoOrigen.EXTRA14;
            newOrden.EXTRA15  = ordenPagoOrigen.EXTRA15;
            newOrden.NUMERO_COMPROBANTE = ordenPagoOrigen.NUMERO_COMPROBANTE;
            newOrden.FECHA_COMPROBANTE = ordenPagoOrigen.FECHA_COMPROBANTE;
            newOrden.NUMERO_COMPROBANTE2 = ordenPagoOrigen.NUMERO_COMPROBANTE2;
            newOrden.NUMERO_COMPROBANTE3 = ordenPagoOrigen.NUMERO_COMPROBANTE3;
            newOrden.NUMERO_COMPROBANTE4 = ordenPagoOrigen.NUMERO_COMPROBANTE4;
            newOrden.SEARCH_TEXT = ordenPagoOrigen.SEARCH_TEXT;
            return newOrden;
        }
        
        public async  Task<ResultDto<bool>> CopiarOrdenPago(int codigoOrdenPago)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            var ordenPagoOrigen = await _repository.GetCodigoOrdenPago(codigoOrdenPago);
            if (ordenPagoOrigen == null)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = "Orden de Pago No existe";
                return result;
            }


            var ordenDestino = await _destinoRepository.GetCodigoOrdenPago(codigoOrdenPago);
            if (ordenDestino != null)
            {
                await _destinoRepository.Delete(codigoOrdenPago);
                
            }


            var newOrden = Map(ordenPagoOrigen);
            
            var orderCreated = await _destinoRepository.Add(newOrden);
            if (orderCreated.IsValid == true)
            {
                
            }
            
            
            
            return result;
        }
    
   
        
    }
 }

