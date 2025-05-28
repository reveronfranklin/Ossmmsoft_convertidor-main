namespace Convertidor.Services.Adm.AdmDocumentosOp;

public partial class AdmDocumentosOpService
{
            public async Task<bool> IsValidTotalDocumentosVsTotalCompromisoCreate(int codigoOrdenPago,decimal montoDocumento)
        {
            bool result = true;
            decimal totalMontoDocumentos = 0;
            decimal totalPucOrdenPago = 0;
            var pucOrdenPago = await _admPucOrdenPagoRepository.GetByOrdenPago(codigoOrdenPago);
            if (pucOrdenPago != null && pucOrdenPago.Count() > 0)
            {
                totalPucOrdenPago = pucOrdenPago.Sum(t => t.MONTO);
            }
            var documentosOp = await _repository.GetByCodigoOrdenPago(codigoOrdenPago);
          
            if (documentosOp != null && documentosOp.Count() > 0)
            {
                totalMontoDocumentos = documentosOp.Sum(t => t.MONTO_DOCUMENTO);
              


            }

            if (totalMontoDocumentos + montoDocumento > totalPucOrdenPago)
            {
                result = false;
            }
            
           
            
            return result;
        }
        
        
        public async Task<bool> IsValidTotalDocumentosVsTotalCompromisoUpdate(int codigoDocumento,int codigoOrdenPago,decimal montoDocumento)
        {
            bool result = true;
            decimal totalMontoDocumentos = 0;
            decimal totalPucOrdenPago = 0;
            var documentosOp = await _repository.GetByCodigoOrdenPago(codigoOrdenPago);
            var cant = documentosOp.Count();
            if (documentosOp != null && documentosOp.Count() > 0)
            {
                totalMontoDocumentos = documentosOp
                    .Where(t => t.CODIGO_DOCUMENTO_OP != codigoDocumento)
                    .Sum(t => t.MONTO_DOCUMENTO);
                var pucOrdenPago = await _admPucOrdenPagoRepository.GetByOrdenPago(codigoOrdenPago);
                if (pucOrdenPago != null && pucOrdenPago.Count() > 0)
                {
                    totalPucOrdenPago = pucOrdenPago.Sum(t => t.MONTO);
                }


            }

            if (totalMontoDocumentos + montoDocumento > totalPucOrdenPago)
            {
                result = false;
            }
            
           
            
            return result;
        }

        
}