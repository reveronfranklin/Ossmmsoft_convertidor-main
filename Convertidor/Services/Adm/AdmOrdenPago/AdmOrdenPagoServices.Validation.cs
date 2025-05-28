using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmOrdenPago;

public partial class AdmOrdenPagoService 
{
    
         public async Task<bool> OrdenDePagoEsModificable(int codigoOrdenPago)
        {
            bool result = false;
            
            var ordenPago = await _repository.GetCodigoOrdenPago(codigoOrdenPago);
            if (ordenPago != null)
            {
                if (ordenPago.STATUS == "PE")
                {
                    result = true;
                }
                if (ordenPago.STATUS == "AP" || ordenPago.STATUS == "AN")
                {
                    result = false;
                }
                var admPucOrdenPago = await _admPucOrdenPagoRepository.GetByOrdenPago(codigoOrdenPago);
                if(admPucOrdenPago != null && admPucOrdenPago.Count>0)
                {
                    var montoPagado = admPucOrdenPago.Sum(x => x.MONTO_PAGADO);
                    if (ordenPago.STATUS == "AP" && montoPagado == 0)
                    {
                        result = true;
                    }
                }
            }
        
            return result;

        }

        public async Task<string> ValidaAprobarOrdenPago(ADM_ORDEN_PAGO ordenPago)
        {
            
            string result = "";
            decimal totalMontoBeneficiariosOp=0;
            decimal totalMontoPucOrdenPago = 0;
            if (ordenPago.STATUS != "PE")
            {
               
                result = "Orden de pago no esta pendiente";
                return result;
               
            }

            if (ordenPago.CON_FACTURA == 1)
            {
               var documentos = await _admDocumentosOpRepository.GetByCodigoOrdenPago(ordenPago.CODIGO_ORDEN_PAGO);
               if (documentos == null || !documentos.Any())
               {
                   result = "Orden de pago es con Factura y no tiene cargados los documentos";
                   return result;
               }
            }
            
            AdmOrdenPagoBeneficiarioFlterDto filter = new AdmOrdenPagoBeneficiarioFlterDto();
            filter.CodigoPresupuesto = ordenPago.CODIGO_PRESUPUESTO;
            filter.CodigoOrdenPago = ordenPago.CODIGO_ORDEN_PAGO;
            var admBeneficiarios = await _admBeneficariosOpService.GetByOrdenPago(filter);
            if (admBeneficiarios.Data.Count > 0)
            {
                totalMontoBeneficiariosOp = admBeneficiarios.Data.Sum(t => t.Monto);
            }
            
            var admPucOrdenPago =await  _admPucOrdenPagoService.GetByOrdenPago(ordenPago.CODIGO_ORDEN_PAGO);
            if (admPucOrdenPago.Data.Count > 0)
            {
                 
                totalMontoPucOrdenPago = admPucOrdenPago.Data.Sum(t => t.Monto);
            }

            if (totalMontoBeneficiariosOp != totalMontoPucOrdenPago)
            {
                result = $"Monto en Beneficiarios es diferente a sus PUC. Total Beneficiarios OP: {totalMontoBeneficiariosOp} - Total PUC OP: {totalMontoPucOrdenPago}";
            }
            
            
            return result;

        }
        
        
        public async Task<string> ValidaAnularOrdenPago(ADM_ORDEN_PAGO ordenPago)
        {
            
            string result = "";
            decimal totalPagado=0;
       
            if (ordenPago.STATUS != "AP")
            {
               
                result = "Orden de pago no esta pendiente";
                return result;
               
            }
            AdmOrdenPagoBeneficiarioFlterDto filter = new AdmOrdenPagoBeneficiarioFlterDto();
           
            
            var admPucOrdenPago =await  _admPucOrdenPagoService.GetByOrdenPago(ordenPago.CODIGO_ORDEN_PAGO);
            if (admPucOrdenPago.Data.Count > 0)
            {
                 
                totalPagado = admPucOrdenPago.Data.Sum(t => t.MontoPagado);
            }

            if (totalPagado!=0)
            {
                result = $"Orden de Pago ya tiene Pagos: {totalPagado} ";
            }
            
            
            return result;

        }


}