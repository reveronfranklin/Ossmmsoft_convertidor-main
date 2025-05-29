using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm.AdmOrdenPago;

public partial class AdmOrdenPagoService 
{
          public async Task<AdmOrdenPagoResponseDto> MapOrdenPagoDto(ADM_ORDEN_PAGO dtos,List<ADM_DESCRIPTIVAS> descriptivas,ADM_PROVEEDORES proveedor)
        {

       
            
            
            AdmOrdenPagoResponseDto itemResult = new AdmOrdenPagoResponseDto();
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.ANO = dtos.ANO;

            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.NombreProveedor = "";
            itemResult.NombreProveedor = proveedor.NOMBRE_PROVEEDOR;
          
            itemResult.NumeroOrdenPago = dtos.NUMERO_ORDEN_PAGO;
            itemResult.FechaOrdenPago = dtos.FECHA_ORDEN_PAGO;
            itemResult.FechaOrdenPagoString =Fecha.GetFechaString(dtos.FECHA_ORDEN_PAGO);
            ;            FechaDto fechaOrdenPagoObj = Fecha.GetFechaDto(dtos.FECHA_ORDEN_PAGO);
            itemResult.FechaOrdenPagoObj = (FechaDto)fechaOrdenPagoObj;
            
            itemResult.CantidadPago = dtos.CANTIDAD_PAGO;
            itemResult.TipoOrdenPagoId = dtos.TIPO_ORDEN_PAGO_ID;
            itemResult.DescripcionTipoOrdenPago = GetDenominacionDescriptiva(descriptivas , dtos.TIPO_ORDEN_PAGO_ID);
            itemResult.FrecuenciaPagoId = dtos.FRECUENCIA_PAGO_ID;
            itemResult.DescripcionFrecuencia = GetDenominacionDescriptiva(descriptivas ,(int)dtos.FRECUENCIA_PAGO_ID);
            itemResult.TipoPagoId = dtos.TIPO_PAGO_ID;
            itemResult.DescripcionTipoPago = GetDenominacionDescriptiva(descriptivas ,(int)dtos.TIPO_PAGO_ID);
            itemResult.Status = dtos.STATUS;
            itemResult.DescripcionStatus= Estatus.GetStatus( itemResult.Status );
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.NumeroComprobante = dtos.NUMERO_COMPROBANTE;
            if (dtos.FECHA_COMPROBANTE != null)
            {
                itemResult.FechaComprobante = (DateTime)dtos.FECHA_COMPROBANTE;
                itemResult.FechaComprobanteString = Fecha.GetFechaString((DateTime)dtos.FECHA_COMPROBANTE);
                FechaDto fechaComprobanteObj = Fecha.GetFechaDto((DateTime)dtos.FECHA_COMPROBANTE);
                itemResult.FechaComprobanteObj = (FechaDto)fechaComprobanteObj;
            }
          
        
            itemResult.NumeroComprobante2 = dtos.NUMERO_COMPROBANTE2;
            itemResult.numeroComprobante3 = dtos.NUMERO_COMPROBANTE3;
            itemResult.NumeroComprobante4 = dtos.NUMERO_COMPROBANTE4;
            itemResult.ConFactura = false;
            if (dtos.CON_FACTURA == 1)
            {
                itemResult.ConFactura = true;
            }

            if (dtos.ESTATUS_TEXT is null) dtos.ESTATUS_TEXT = "";
            itemResult.EstatusText=dtos.ESTATUS_TEXT;
            return itemResult;
        }

        public async Task<List<AdmOrdenPagoResponseDto>> MapListOrdenPagoDto(List<ADM_ORDEN_PAGO> dtos)
        {
            
            var descriptivas = await _admDescriptivaRepository.GetAll();
           
            List<AdmOrdenPagoResponseDto> result = new List<AdmOrdenPagoResponseDto>();
            {
                foreach (var item in dtos)
                {
                    var proveedores = await _admProveedoresRepository.GetByCodigo(item.CODIGO_PROVEEDOR);
                    var itemResult = await MapOrdenPagoDto(item,descriptivas,proveedores);

                    result.Add(itemResult);
                }
                return result;
            }
        }
  
}