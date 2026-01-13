namespace Convertidor.Services.Adm.Pagos.AdmPagosService;

public partial class AdmPagosService
{
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


}