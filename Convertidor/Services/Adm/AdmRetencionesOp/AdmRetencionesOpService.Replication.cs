using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;
using iText.StyledXmlParser.Jsoup.Select;


namespace Convertidor.Services.Adm.AdmRetencionesOp
{
    // Usa 'partial' para indicar que la clase se define en múltiples archivos
    public partial class AdmRetencionesOpService
    {
        public async Task ReplicarMotoRetenidoDocumento(int codigoDocumentoOp, decimal montoRetencion)
        {
            await _admDocumentosOpRepository.UpdateMontoRetenido(codigoDocumentoOp, montoRetencion);
        }

        public async Task ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(int codigoOrdenPago)
        {

            decimal totalRetenciones = 0;
            decimal totalMontoDocumentos = 0;

            var ordenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(codigoOrdenPago);
            var retencionesOp = await _repository.GetByOrdenPago(codigoOrdenPago);
            if (retencionesOp.Count > 0)
            {
                // Calcular el total del Impuesto
                totalRetenciones = (decimal)retencionesOp.Sum(t => t.MONTO_RETENCION);

            }

            var documentosOp = await _admDocumentosOpRepository.GetByCodigoOrdenPago(codigoOrdenPago);
            if (documentosOp != null && documentosOp.Count() > 0)
            {
                totalMontoDocumentos = documentosOp.Sum(t => t.MONTO_DOCUMENTO);
            }

            var compromisos = await _admCompromisoOpRepository.GetCodigoOrdenPago(codigoOrdenPago, ordenPago.CODIGO_PRESUPUESTO);
            if (compromisos.Count > 0)
            {
                var compromiso = compromisos.FirstOrDefault();
                var beneficiarioCompromiso =
                    await _admBeneficariosOpService.GetByOrdenPagoProveedor(codigoOrdenPago, compromiso.CODIGO_PROVEEDOR);
                if (beneficiarioCompromiso != null)
                {
                    AdmBeneficiariosOpUpdateMontoDto dto = new AdmBeneficiariosOpUpdateMontoDto();
                    if (totalMontoDocumentos > 0)
                    {
                        dto.Monto = totalMontoDocumentos - totalRetenciones;
                    }
                    else
                    {
                        
                        decimal totaCompromiso = 0;
                        var pucOrdenPago=  await _admPucOrdenPagoRepository.GetByOrdenPago(codigoOrdenPago);
                        if (pucOrdenPago.Count > 0)
                        {
                            totaCompromiso = pucOrdenPago.Sum(x => x.MONTO);
                        }
                        dto.Monto = totaCompromiso - totalRetenciones;
                       
                        
                    }
                  
                   
                    dto.CodigoBeneficiarioOp = beneficiarioCompromiso.CODIGO_BENEFICIARIO_OP;
                    await _admBeneficariosOpService.UpdateMonto(dto);
                }   else
                    {
                        AdmBeneficiariosOpUpdateDto dto = new AdmBeneficiariosOpUpdateDto();
                        dto.CodigoBeneficiarioOp = 0;
                        dto.CodigoPresupuesto = ordenPago.CODIGO_PRESUPUESTO;
                        dto.CodigoOrdenPago = codigoOrdenPago;
                        dto.CodigoProveedor = compromiso.CODIGO_PROVEEDOR;
                        if (totalMontoDocumentos > 0)
                        {
                            dto.Monto = totalMontoDocumentos - totalRetenciones;
                        }
                        else
                        {

                            decimal totaCompromiso = 0;
                            var pucOrdenPago=  await _admPucOrdenPagoRepository.GetByOrdenPago(codigoOrdenPago);
                            if (pucOrdenPago.Count > 0)
                            {
                            totaCompromiso = pucOrdenPago.Sum(x => x.MONTO);
                            }
                            dto.Monto = totaCompromiso - totalRetenciones;


                        }

                        dto.MontoAnulado = 0;
                        dto.MontoPagado = 0;
                        await _admBeneficariosOpService.Create(dto);
                    }


            }
        }

        public async Task ReplicaRetencionesEnAdmBeneficiariosOp(int codigoOrdenPago)
        {

            var ordenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(codigoOrdenPago);

            var retencionesOp = await _repository.GetByOrdenPago(codigoOrdenPago);
            if (retencionesOp.Count == 0)
            {
                var prooveedorFiscoConfig = await _ossConfigRepository.GetByClave("PROVEEDOR_FISCO");
                if (prooveedorFiscoConfig != null)
                {
                    var proveedor = int.Parse(prooveedorFiscoConfig.VALOR);
                    var admBeneficiario = await _admBeneficariosOpService.GetByOrdenPagoProveedor(codigoOrdenPago, proveedor);
                    if (admBeneficiario != null)
                    {
                        AdmBeneficiariosOpDeleteDto dto = new AdmBeneficiariosOpDeleteDto();
                        dto.CodigoBeneficiarioOp = admBeneficiario.CODIGO_BENEFICIARIO_OP;
                        await _admBeneficariosOpService.Delete(dto);
                    }
                }
            }
            if (retencionesOp.Count > 0)
            {
                // Calcular el total del Impuesto
                var totalMontoRetencion = retencionesOp.Sum(t => t.MONTO_RETENCION);

                var prooveedorFiscoConfig = await _ossConfigRepository.GetByClave("PROVEEDOR_FISCO");
                if (prooveedorFiscoConfig != null)
                {
                    var proveedor = int.Parse(prooveedorFiscoConfig.VALOR);

                    var admBeneficiario = await _admBeneficariosOpService.GetByOrdenPagoProveedor(codigoOrdenPago, proveedor);
                    if (admBeneficiario != null)
                    {
                        AdmBeneficiariosOpUpdateDto dto = new AdmBeneficiariosOpUpdateDto();
                        dto.CodigoBeneficiarioOp = admBeneficiario.CODIGO_BENEFICIARIO_OP;
                        dto.CodigoPresupuesto = ordenPago.CODIGO_PRESUPUESTO;
                        dto.CodigoOrdenPago = codigoOrdenPago;
                        dto.CodigoProveedor = admBeneficiario.CODIGO_PROVEEDOR;
                        dto.Monto = (decimal)totalMontoRetencion;
                        dto.MontoAnulado = admBeneficiario.MONTO_ANULADO;
                        dto.MontoPagado = admBeneficiario.MONTO_PAGADO;
                        await _admBeneficariosOpService.Update(dto);
                    }
                    else
                    {
                        AdmBeneficiariosOpUpdateDto dto = new AdmBeneficiariosOpUpdateDto();
                        dto.CodigoBeneficiarioOp = 0;
                        dto.CodigoPresupuesto = ordenPago.CODIGO_PRESUPUESTO;
                        dto.CodigoOrdenPago = codigoOrdenPago;
                        dto.CodigoProveedor = proveedor;
                        dto.Monto = (decimal)totalMontoRetencion;
                        dto.MontoAnulado = 0;
                        dto.MontoPagado = 0;
                        await _admBeneficariosOpService.Create(dto);
                    }
                }
            }
        }
    }
}