using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmOrdenPago;

public partial class AdmOrdenPagoService 
{
            public async Task<ResultDto<AdmOrdenPagoResponseDto>> Create(AdmOrdenPagoUpdateDto dto)
        {
            ResultDto<AdmOrdenPagoResponseDto> result = new ResultDto<AdmOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago((int)dto.CodigoOrdenPago);
                if (codigoOrdenPago != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo  existe";
                    return result;
                }
              
                var compromiso = await _preCompromisosService.GetByCompromiso(dto.CodigoCompromiso);
                if (compromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Compromiso Invalido";
                    return result;
                }

                
                if (dto.FechaOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Orden Pago Invalida";
                    return result;
                }
                var tipoOrdenPagoId = await _admDescriptivaRepository.GetByCodigo(dto.TipoOrdenPagoId);
                if (tipoOrdenPagoId==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Orden Pago Invalido";
                    return result;
                }
                

                if (dto.CantidadPago <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Pago Invalida";
                    return result;
                }

             
                var frecuenciaPagoId = await _admDescriptivaRepository.GetByCodigo(dto.FrecuenciaPagoId);
                if (frecuenciaPagoId==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia Pago Invalido";
                    return result;
                }

                var tipoPagoId = await _admDescriptivaRepository.GetByCodigo(dto.TipoPagoId);
                if (tipoPagoId==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Pago Id Invalido";
                    return result;
                }

             

                if (dto.Motivo == null || dto.Motivo.Length<2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
             

                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto==null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
                
               
           

                if (dto.NumeroComprobante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante Invalido";
                    return result;
                }
                if (dto.NumeroComprobante > 0 && dto.FechaComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha comprobante Invalida";
                    return result;
                }

                if (dto.NumeroComprobante2 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante2 Invalido";
                    return result;
                }
                
             

               var compromisoPendiente= await  _admCompromisosPendientesRepository.GetCompromisosPendientesPorCodigoCompromiso(compromiso
                    .CodigoCompromiso);
               if (compromisoPendiente==null)
               {
                   result.Data = null;
                   result.IsValid = false;
                   result.Message = "COMPROMISO NO ESTA PENDIENTE";
                   return result;
               } 
               

            ADM_ORDEN_PAGO entity = new ADM_ORDEN_PAGO();
            entity.CODIGO_ORDEN_PAGO = await _repository.GetNextKey();
            entity.ANO = presupuesto.ANO;
            entity.CODIGO_PROVEEDOR = compromiso.CodigoProveedor;
            entity.NUMERO_ORDEN_PAGO = await _repository.GetNextOrdenPago(dto.CodigoPresupuesto);
            entity.FECHA_ORDEN_PAGO = DateTime.Now;
            entity.TIPO_ORDEN_PAGO_ID = dto.TipoOrdenPagoId;
            entity.CANTIDAD_PAGO = dto.CantidadPago;
            entity.FRECUENCIA_PAGO_ID = dto.FrecuenciaPagoId;
            entity.TIPO_PAGO_ID = dto.TipoPagoId;
            entity.STATUS = "PE";
            entity.MOTIVO = dto.Motivo;
            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            entity.NUMERO_COMPROBANTE = dto.NumeroComprobante;
            if (dto.FechaComprobante != null)
            {
                entity.FECHA_COMPROBANTE = (DateTime)dto.FechaComprobante;
            }
          
            entity.NUMERO_COMPROBANTE2 = dto.NumeroComprobante2;
            entity.NUMERO_COMPROBANTE3 = dto.NumeroComprobante3;
            entity.NUMERO_COMPROBANTE4 = dto.NumeroComprobante4;
            entity.FECHA_PLAZO_DESDE = dto.FechaPlazoDesde;
            entity.FECHA_PLAZO_HASTA = dto.FechaPlazoHasta;
            entity.CON_FACTURA = 0;
            if (dto.ConFactura)
            {
                entity.CON_FACTURA = 1;
            }
            
            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                
                //CREAR LOS PUC a parir del compromiso
                await CrearPucOrdenPagoDesdeCompromiso(dto.CodigoCompromiso,created.Data.CODIGO_ORDEN_PAGO);
                
                //CREAMOS EL COMPROMISO DE LA ORDEN DE PAGO
                AdmCompromisoOpUpdateDto compromisoOp = new AdmCompromisoOpUpdateDto();
                compromisoOp.CodigoCompromisoOp = 0;
                compromisoOp.CodigoProveedor = compromiso.CodigoProveedor;
                compromisoOp.CodigoPresupuesto = compromiso.CodigoPresupuesto;
                compromisoOp.CodigoOrdenPago = created.Data.CODIGO_ORDEN_PAGO;
                compromisoOp.CodigoValContrato = 0;
                compromisoOp.OrigenCompromisoId = compromisoPendiente.ORIGEN_COMPROMISO_ID; 
                compromisoOp.CodigoIdentificador = compromiso.CodigoCompromiso; 
                var compromisOpCreated = await _admCompromisoOpService.Create(compromisoOp);
                
               
                await _prePresupuestosRepository.RecalcularSaldo(created.Data.CODIGO_PRESUPUESTO);
             
                
                var descriptivas = await _admDescriptivaRepository.GetAll();
                var proveedores = await _admProveedoresRepository.GetByCodigo(compromisoOp.CodigoProveedor );
                var resultDto = await MapOrdenPagoDto(created.Data,descriptivas,proveedores);

                result.Data = resultDto; 
                //resultDto;
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