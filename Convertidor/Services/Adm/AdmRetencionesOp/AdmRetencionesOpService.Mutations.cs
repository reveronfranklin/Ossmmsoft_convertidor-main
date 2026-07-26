using System.Globalization;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Adm;
using Microsoft.Extensions.Logging;


namespace Convertidor.Services.Adm.AdmRetencionesOp
{
    // Usa 'partial' para indicar que la clase se define en múltiples archivos
    public partial class AdmRetencionesOpService
    {



        public async Task<decimal> GetBaseImponibleByCodigoOrdenPago(int codigoOrdenPago, string tipoRetencion)
        {
            decimal result = 0;
            var documentosOp = await _admDocumentosOpRepository.GetByCodigoOrdenPago(codigoOrdenPago);
            if (documentosOp != null && documentosOp.Count() > 0)
            {
                // Calcular el total del BaseImponible
                decimal totalBaseImponible = documentosOp.Sum(t => t.BASE_IMPONIBLE);
                // Calcular el total del Impuesto exento
                decimal totalMontoImpuestoExento = documentosOp.Sum(t => t.MONTO_IMPUESTO_EXENTO);

                /*if (tipoRetencion == "IVA")
                {
                     totalBaseImponible = documentosOp.Sum(t => t.MONTO_IMPUESTO);
                     totalMontoImpuestoExento = 0;
                }*/

                result = totalBaseImponible + totalMontoImpuestoExento;
            }
            return result;
        }
    



        public async Task<ResultDto<AdmRetencionesOpResponseDto>> Update(AdmRetencionesOpUpdateDto dto)
        {
            ResultDto<AdmRetencionesOpResponseDto> result = new ResultDto<AdmRetencionesOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoRetencionOp = await _repository.GetCodigoRetencionOp(dto.CodigoRetencionOp);
                if (codigoRetencionOp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retencion op no existe";
                    return result;
                }
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }
                
                if (codigoOrdenPago != null && codigoOrdenPago.STATUS != "PE")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No puede Modificar, Orden de Pago";
                    return result;
                }
                var tipoRetencionId = await _admDescriptivaRepository.GetByCodigo(dto.TipoRetencionId);
                if (tipoRetencionId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo retencion Id invalido";
                    return result;
                }

                

                var retenciones = await _admRetencionesRepository.GetAll();
                var retencionesPorTipo =
                    retenciones.Where(x => x.TIPO_RETENCION_ID == dto.TipoRetencionId).FirstOrDefault();
                if (retencionesPorTipo != null)
                {
                    var admRetencion = await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
                    if (admRetencion == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Codigo retencion invalido";
                        return result;
                    }
                }
                if (dto.PorRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por retencion invalido";
                    return result;
                }
                if (dto.MontoRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retencion invalido";
                    return result;
                }

                if (dto.NumeroComprobante != null && dto.NumeroComprobante.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "La longitud Maxima del Comprobante es de 20 digitos";
                    return result;
                }


                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                if (dto.BaseImponible < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible Invalida";
                    return result;
                }
                var baseImponible = await GetBaseImponibleByCodigoOrdenPago(dto.CodigoOrdenPago,tipoRetencionId.CODIGO);
                
                 if (dto.CodigoRetencion==0) {
                    baseImponible=0;
                    dto.BaseImponible=0;
                };

                if (baseImponible>0 && dto.PorRetencion>0)
                {
                    dto.BaseImponible = baseImponible;
                    if(tipoRetencionId.CODIGO != "IVA")
                    {
                         dto.MontoRetencion = baseImponible * dto.PorRetencion / 100;
                    }
                   

                }

                codigoRetencionOp.CODIGO_RETENCION_OP = dto.CodigoRetencionOp;
                codigoRetencionOp.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                codigoRetencionOp.TIPO_RETENCION_ID = dto.TipoRetencionId;
                codigoRetencionOp.CODIGO_RETENCION = dto.CodigoRetencion;
                codigoRetencionOp.POR_RETENCION = dto.PorRetencion;
                codigoRetencionOp.MONTO_RETENCION = dto.MontoRetencion;

                codigoRetencionOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoRetencionOp.BASE_IMPONIBLE = dto.BaseImponible;

                codigoRetencionOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoRetencionOp.USUARIO_UPD = conectado.Usuario;
                codigoRetencionOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoRetencionOp);

                await ReplicaRetencionesEnAdmBeneficiariosOp(dto.CodigoOrdenPago);
                await ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(dto.CodigoOrdenPago);

                var resultDto = await MapRetencionesOpDto(codigoRetencionOp);
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


        public async Task<ResultDto<AsignacionComprobanteOpDto>> AsignarComprobanteIvaOrdenPago(int codigoOrdenPago)
        {
            ResultDto<AsignacionComprobanteOpDto> result = new ResultDto<AsignacionComprobanteOpDto>(null);
            var data = new AsignacionComprobanteOpDto { CodigoOrdenPago = codigoOrdenPago };

            try
            {
                var documentosOrdenPago = await _admDocumentosOpRepository.GetByCodigoOrdenPago(codigoOrdenPago);
                if (documentosOrdenPago == null)
                {
                    _logger.LogWarning("AsignarComprobanteIvaOrdenPago: fallo al consultar documentos de la orden {CodigoOrdenPago}", codigoOrdenPago);
                    data.Estado = EstadoAsignacionComprobante.Error;
                    result.Data = data;
                    result.IsValid = false;
                    result.Message = "No se pudieron consultar los documentos de la orden de pago";
                    return result;
                }

                var retencionesOp = await _repository.GetByOrdenPago(codigoOrdenPago);
                if (retencionesOp == null)
                {
                    _logger.LogWarning("AsignarComprobanteIvaOrdenPago: fallo al consultar retenciones de la orden {CodigoOrdenPago}", codigoOrdenPago);
                    data.Estado = EstadoAsignacionComprobante.Error;
                    result.Data = data;
                    result.IsValid = false;
                    result.Message = "No se pudieron consultar las retenciones de la orden de pago";
                    return result;
                }

                var tipoRetencionCache = new Dictionary<int, string>();
                var retencionesIva = new List<ADM_RETENCIONES_OP>();

                foreach (var item in retencionesOp)
                {
                    if (!tipoRetencionCache.TryGetValue(item.TIPO_RETENCION_ID, out var codigoTipoRetencion))
                    {
                        var tipoRetencion = await _admDescriptivaRepository.GetByCodigo(item.TIPO_RETENCION_ID);
                        if (tipoRetencion == null)
                        {
                            _logger.LogWarning("AsignarComprobanteIvaOrdenPago: retencion {CodigoRetencionOp} con TipoRetencionId {TipoRetencionId} sin descriptiva configurada", item.CODIGO_RETENCION_OP, item.TIPO_RETENCION_ID);
                            data.Estado = EstadoAsignacionComprobante.Error;
                            result.Data = data;
                            result.IsValid = false;
                            result.Message = $"La retención {item.CODIGO_RETENCION_OP} tiene un Tipo de Retención sin descriptiva configurada";
                            return result;
                        }

                        codigoTipoRetencion = tipoRetencion.CODIGO ?? string.Empty;
                        tipoRetencionCache[item.TIPO_RETENCION_ID] = codigoTipoRetencion;
                    }

                    if (codigoTipoRetencion == "IVA")
                    {
                        retencionesIva.Add(item);
                    }
                }

                data.CantidadRetencionesIva = retencionesIva.Count;

                if (retencionesIva.Count == 0)
                {
                    _logger.LogInformation("AsignarComprobanteIvaOrdenPago: orden {CodigoOrdenPago} sin retenciones IVA, no aplica", codigoOrdenPago);
                    data.Estado = EstadoAsignacionComprobante.NoAplica;
                    result.Data = data;
                    result.IsValid = true;
                    result.Message = "";
                    return result;
                }

                var ordenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(codigoOrdenPago);
                if (ordenPago == null)
                {
                    data.Estado = EstadoAsignacionComprobante.Error;
                    result.Data = data;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }

                // Idempotencia: reutilizar un numero ya persistido, en la orden o en alguna de sus retenciones IVA,
                // antes de reservar una nueva serie. Esto evita consumir una segunda serie en un reintento.
                decimal? numeroExistente = null;
                if (ordenPago.NUMERO_COMPROBANTE.HasValue && ordenPago.NUMERO_COMPROBANTE.Value > 0)
                {
                    numeroExistente = ordenPago.NUMERO_COMPROBANTE.Value;
                }
                else
                {
                    foreach (var retencion in retencionesIva)
                    {
                        if (!string.IsNullOrWhiteSpace(retencion.NUMERO_COMPROBANTE) &&
                            decimal.TryParse(retencion.NUMERO_COMPROBANTE, NumberStyles.Number, CultureInfo.InvariantCulture, out var numeroRetencion) &&
                            numeroRetencion > 0)
                        {
                            numeroExistente = numeroRetencion;
                            break;
                        }
                    }
                }

                decimal numeroComprobante;
                if (numeroExistente.HasValue)
                {
                    numeroComprobante = numeroExistente.Value;
                    data.Estado = EstadoAsignacionComprobante.Reutilizado;
                    _logger.LogInformation("AsignarComprobanteIvaOrdenPago: orden {CodigoOrdenPago} reutiliza comprobante {NumeroComprobante}", codigoOrdenPago, numeroComprobante);
                }
                else
                {
                    var descriptivasIva = (await _sisDescriptivaRepository.GetALL())
                        ?.Where(x => x.EXTRA1 == "IVA").ToList() ?? new List<SIS_DESCRIPTIVAS>();

                    if (descriptivasIva.Count == 0)
                    {
                        data.Estado = EstadoAsignacionComprobante.Error;
                        result.Data = data;
                        result.IsValid = false;
                        result.Message = "No existe descriptiva de series configurada para IVA";
                        return result;
                    }

                    if (descriptivasIva.Count > 1)
                    {
                        _logger.LogWarning("AsignarComprobanteIvaOrdenPago: {Cantidad} descriptivas SIS configuradas con EXTRA1='IVA'", descriptivasIva.Count);
                        data.Estado = EstadoAsignacionComprobante.Error;
                        result.Data = data;
                        result.IsValid = false;
                        result.Message = "Existe más de una descriptiva SIS configurada con EXTRA1='IVA'; la configuración es ambigua";
                        return result;
                    }

                    var sisDescriptiva = descriptivasIva[0];

                    var reserva = await _serieDocumentosRepository.ReservarSerieAtomica(sisDescriptiva.DESCRIPCION_ID);
                    if (!reserva.IsValid)
                    {
                        _logger.LogWarning("AsignarComprobanteIvaOrdenPago: fallo al reservar serie para orden {CodigoOrdenPago}: {Mensaje}", codigoOrdenPago, reserva.Message);
                        data.Estado = EstadoAsignacionComprobante.Error;
                        result.Data = data;
                        result.IsValid = false;
                        result.Message = reserva.Message;
                        return result;
                    }

                    if (!decimal.TryParse(reserva.Data, NumberStyles.Number, CultureInfo.InvariantCulture, out numeroComprobante))
                    {
                        data.Estado = EstadoAsignacionComprobante.Error;
                        result.Data = data;
                        result.IsValid = false;
                        result.Message = $"El número de serie generado '{reserva.Data}' no tiene un formato numérico válido para NUMERO_COMPROBANTE";
                        return result;
                    }

                    data.Estado = EstadoAsignacionComprobante.Generado;
                    _logger.LogInformation("AsignarComprobanteIvaOrdenPago: orden {CodigoOrdenPago} genera comprobante {NumeroComprobante}", codigoOrdenPago, numeroComprobante);
                }

                data.NumeroComprobante = numeroComprobante;
                data.NumeroComprobanteTexto = numeroComprobante.ToString(CultureInfo.InvariantCulture);

                if (!ordenPago.NUMERO_COMPROBANTE.HasValue || ordenPago.NUMERO_COMPROBANTE.Value <= 0)
                {
                    var actualizaOrden = await _admOrdenPagoRepository.UpdateNumeroComprobante(codigoOrdenPago, numeroComprobante);
                    if (!actualizaOrden.IsValid)
                    {
                        _logger.LogWarning("AsignarComprobanteIvaOrdenPago: fallo al persistir comprobante {NumeroComprobante} en ADM_ORDEN_PAGO de la orden {CodigoOrdenPago}: {Mensaje}", numeroComprobante, codigoOrdenPago, actualizaOrden.Message);
                        data.Estado = EstadoAsignacionComprobante.Error;
                        result.Data = data;
                        result.IsValid = false;
                        result.Message = $"No se pudo persistir el comprobante en ADM_ORDEN_PAGO. Comprobante {data.NumeroComprobanteTexto} reservado; reintente la aprobación para completar la asignación.";
                        return result;
                    }
                }

                foreach (var retencion in retencionesIva)
                {
                    if (!string.IsNullOrWhiteSpace(retencion.NUMERO_COMPROBANTE))
                    {
                        continue;
                    }

                    var actualizaRetencion = await _repository.UpdaNumeroComprobante(retencion.CODIGO_RETENCION_OP, data.NumeroComprobanteTexto);
                    if (!actualizaRetencion.IsValid)
                    {
                        _logger.LogWarning("AsignarComprobanteIvaOrdenPago: fallo al persistir comprobante {NumeroComprobante} en ADM_RETENCIONES_OP (Codigo Retencion Op: {CodigoRetencionOp}): {Mensaje}", numeroComprobante, retencion.CODIGO_RETENCION_OP, actualizaRetencion.Message);
                        data.Estado = EstadoAsignacionComprobante.Error;
                        result.Data = data;
                        result.IsValid = false;
                        result.Message = $"Comprobante {data.NumeroComprobanteTexto} no se pudo persistir en ADM_RETENCIONES_OP (Codigo Retencion Op: {retencion.CODIGO_RETENCION_OP}). La orden permanece pendiente; reintente la aprobación para completar la asignación.";
                        return result;
                    }

                    data.CantidadRetencionesActualizadas++;
                }

                result.Data = data;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AsignarComprobanteIvaOrdenPago: error tecnico procesando la orden {CodigoOrdenPago}", codigoOrdenPago);
                data.Estado = EstadoAsignacionComprobante.Error;
                result.Data = data;
                result.IsValid = false;
                result.Message = $"Error tecnico: {ex.Message}";
                return result;
            }
        }


        public async Task<string> GetNumeroComprobanteNoIva(string codigoTipoRetencion,int codigoPesupuesto,int codigoOrdenPago)
        {
            string result = "";
         
            
            var sisDescriptiva = await _sisDescriptivaRepository.GetByExtra1(codigoTipoRetencion);
            if (sisDescriptiva != null)
            {
                var numeroSolicitud = await _serieDocumentosRepository.GenerateNextSerie(codigoPesupuesto, sisDescriptiva.DESCRIPCION_ID, sisDescriptiva.CODIGO_DESCRIPCION);
                result = numeroSolicitud.Data;
                
            }

            
           
            return result;

        }


        public async Task<bool> IsValidTotalDocumentosVsTotalCompromisoCreate(int codigoOrdenPago, decimal montoRetencion)
        {
            bool result = true;
            decimal totalMontoDocumentos = 0;
            decimal totalPucOrdenPago = 0;
            decimal totalRetenciones = 0;
            var pucOrdenPago = await _admPucOrdenPagoRepository.GetByOrdenPago(codigoOrdenPago);
            if (pucOrdenPago != null && pucOrdenPago.Count() > 0)
            {
                totalPucOrdenPago = pucOrdenPago.Sum(t => t.MONTO);
            }
            var documentosOp = await _admDocumentosOpRepository.GetByCodigoOrdenPago(codigoOrdenPago);

            if (documentosOp != null && documentosOp.Count() > 0)
            {
                totalMontoDocumentos = documentosOp.Sum(t => t.MONTO_DOCUMENTO);

            }

            var retenciones = await _repository.GetByOrdenPago(codigoOrdenPago);
            if (retenciones != null && retenciones.Count() > 0)
            {
                //  var descriptivaIva = await _admDescriptivaRepository.GetByCodigoDescriptivaTexto("IVA");

                totalRetenciones = (decimal)retenciones.Sum(t => t.MONTO_RETENCION);
                //.Where(X=>X.TIPO_RETENCION_ID!=descriptivaIva.DESCRIPCION_ID)


            }


            var totalRetencion = totalRetenciones + montoRetencion;
            if (totalMontoDocumentos - totalRetencion < 0)
            {
                result = false;
            }
            else
            {
                if (totalMontoDocumentos - (totalRetencion) > totalPucOrdenPago)
                {
                    result = false;
                }
            }





            return result;
        }
        
        
        
        public async Task<ResultDto<AdmRetencionesOpResponseDto>> Create(AdmRetencionesOpUpdateDto dto)
        {
            ResultDto<AdmRetencionesOpResponseDto> result = new ResultDto<AdmRetencionesOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoRetencionOp = await _repository.GetCodigoRetencionOp(dto.CodigoRetencionOp);
                if (codigoRetencionOp != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retención orden pago ya existe";
                    return result;
                }
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }
                if (codigoOrdenPago != null && codigoOrdenPago.STATUS != "PE")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No puede Modificar, Orden de Pago";
                    return result;
                }
                
                var isValidMonto= await IsValidTotalDocumentosVsTotalCompromisoCreate(dto.CodigoOrdenPago, dto.MontoRetencion);
                if (isValidMonto == false && codigoOrdenPago.CON_FACTURA==1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "La suma de los documentos + las retenciones superan el compromiso";
                    return result;
                }
                
                var tipoRetencion = await _admDescriptivaRepository.GetByCodigo(dto.TipoRetencionId);
                if (tipoRetencion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo retención Id invalido";
                    return result;
                }

                var retenciones = await _admRetencionesRepository.GetAll();
                var retencionesPorTipo =
                    retenciones.Where(x => x.TIPO_RETENCION_ID == dto.TipoRetencionId).FirstOrDefault();
                if (retencionesPorTipo != null)
                {
                    var admRetencion = await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
                    if (admRetencion == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Codigo retención invalido";
                        return result;
                    }
                }

                if (dto.PorRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por retención invalido";
                    return result;
                }
                if (dto.MontoRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retención invalido";
                    return result;
                }


                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                if (dto.BaseImponible < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible Invalida";
                    return result;
                }

                var retencionOp =
                    await _repository.GetByOrdenPagoCodigoRetencionTipoRetencionPorcentaje(
                        dto.CodigoOrdenPago, dto.CodigoRetencion, dto.TipoRetencionId, dto.PorRetencion);
                if (retencionOp != null)
                {
                    var admRetencion = await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
                    var conceptoPago = "";
                    if (admRetencion != null)
                    {
                        conceptoPago = admRetencion.CONCEPTO_PAGO;
                    }
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Ya existe esta retención para la Orden de Pago:{dto.CodigoOrdenPago}- {tipoRetencion.DESCRIPCION}-{conceptoPago}-{dto.PorRetencion}%";
                    return result;
                }

                var baseImponible = await GetBaseImponibleByCodigoOrdenPago(dto.CodigoOrdenPago,tipoRetencion.CODIGO );
                
                if (dto.CodigoRetencion==0) {
                    baseImponible=0;
                    dto.BaseImponible=0;
                };
                if (baseImponible > 0  && dto.PorRetencion > 0)
                {
                    dto.BaseImponible = baseImponible;
                      if(tipoRetencion.CODIGO != "IVA")
                      {
                           dto.MontoRetencion = baseImponible * dto.PorRetencion / 100;
                      }
                   
                }
            

                ADM_RETENCIONES_OP entity = new ADM_RETENCIONES_OP();
                entity.CODIGO_RETENCION_OP = await _repository.GetNextKey();
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.TIPO_RETENCION_ID = dto.TipoRetencionId;
                entity.CODIGO_RETENCION = dto.CodigoRetencion;
                entity.POR_RETENCION = dto.PorRetencion;
                entity.MONTO_RETENCION = dto.MontoRetencion;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.BASE_IMPONIBLE = dto.BaseImponible;
                //Se asigna el numero de comprobante
                if (tipoRetencion.CODIGO == "ISLR")
                {
                    string paddedNumber = dto.CodigoOrdenPago.ToString().PadLeft(8, '0');

                    var mes = DateTime.Now.Month.ToString().PadLeft(2, '0');
                    var serieLetras = $"{DateTime.Now.Year}{mes} ";
                    entity.NUMERO_COMPROBANTE = $"{serieLetras.Trim()}{paddedNumber.Trim()}";
                }
                else
                {
                   /* if (tipoRetencion.CODIGO == "IVA")
                    {
                        entity.NUMERO_COMPROBANTE = await GetNumeroComprobanteIva(tipoRetencion.CODIGO,(int)entity.CODIGO_PRESUPUESTO,dto.CodigoOrdenPago);

                    }else
                    {
                        entity.NUMERO_COMPROBANTE = await GetNumeroComprobanteNoIva(tipoRetencion.CODIGO,(int)entity.CODIGO_PRESUPUESTO,dto.CodigoOrdenPago);

                    }*/

                    if (tipoRetencion.CODIGO != "IVA")
                    {
                         entity.NUMERO_COMPROBANTE = await GetNumeroComprobanteNoIva(tipoRetencion.CODIGO,(int)entity.CODIGO_PRESUPUESTO,dto.CodigoOrdenPago);

                    }
                  
                }


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    
                    await ReplicaRetencionesEnAdmBeneficiariosOp(dto.CodigoOrdenPago);
                    await ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(dto.CodigoOrdenPago);
                    //Se actualiza el numero de comprobante
                    //await _admDocumentosOpRepository.UpdateNroComprobante(dto.CodigoDocumento,  entity.NUMERO_COMPROBANTE);
                    var resultDto = await MapRetencionesOpDto(created.Data);
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
