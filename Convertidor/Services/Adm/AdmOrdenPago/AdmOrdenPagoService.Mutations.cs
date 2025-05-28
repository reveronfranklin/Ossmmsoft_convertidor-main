using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmOrdenPago;

public partial class AdmOrdenPagoService 
{
            public async Task<ResultDto<bool>> CrearPucOrdenPagoDesdeCompromiso(int codigoCompromiso,int codigoOrdenPago)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);

            try
            {
                var detalleCompromiso = await _preDetalleCompromisosRepository.GetByCodigoCompromiso(codigoCompromiso);
                if (detalleCompromiso.Count > 0)
                {
                    foreach (var itemDetalle in detalleCompromiso)
                    {
                        var pucCompromiso =
                            await _prePucCompromisosRepository.GetListByCodigoDetalleCompromiso(itemDetalle
                                .CODIGO_DETALLE_COMPROMISO);
                        if (pucCompromiso.Count > 0)
                        {
                            foreach (var itemPucCompromiso in pucCompromiso)
                            {

                                if (itemPucCompromiso.MONTO - itemPucCompromiso.MONTO_CAUSADO > 0)
                                {
                                    AdmPucOrdenPagoUpdateDto newPuc = new AdmPucOrdenPagoUpdateDto();
                                    newPuc.CodigoOrdenPago = codigoOrdenPago;
                                    newPuc.CodigoPucOrdenPago = 0;
                                    newPuc.CodigoPucCompromiso = itemPucCompromiso.CODIGO_PUC_COMPROMISO;  
                                    newPuc.CodigoIcp = itemPucCompromiso.CODIGO_ICP;  
                                    newPuc.CodigoPuc = itemPucCompromiso.CODIGO_PUC;  
                                    newPuc.FinanciadoId = itemPucCompromiso.FINANCIADO_ID;  
                                    newPuc.CodigoFinanciado = itemPucCompromiso.CODIGO_FINANCIADO;  
                                    newPuc.CodigoSaldo = itemPucCompromiso.CODIGO_SALDO;  
                                    newPuc.CodigoSaldo = itemPucCompromiso.CODIGO_SALDO;  
                                    newPuc.Monto = itemPucCompromiso.MONTO-itemPucCompromiso.MONTO_CAUSADO;  
                                    newPuc.MontoCompromiso= itemPucCompromiso.MONTO; 
                                    newPuc.MontoPagado = 0;  
                                    newPuc.MontoAnulado =0;  
                                    newPuc.Extra1 ="";  
                                    newPuc.Extra2 ="";  
                                    newPuc.Extra3 ="";  
                                    newPuc.CodigoCompromisoOp =codigoCompromiso;  
                                    newPuc.CodigoPresupuesto =itemPucCompromiso.CODIGO_PRESUPUESTO;
                                    await _admPucOrdenPagoService.Create(newPuc);
                                
                              
                                    var total = await _admPucOrdenPagoRepository.GetTotalByCodigoCompromiso(itemPucCompromiso.CODIGO_PUC_COMPROMISO);

                                    await _prePucCompromisosRepository.UpdateMontoCausadoById(
                                        itemPucCompromiso.CODIGO_PUC_COMPROMISO, total);
                                }
 
                            }
                        }
                        
                    }
                    
                }

                result.Message = "";
                result.IsValid = true;
                result.Data = true;
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.IsValid = false;
                result.Data = false;
                return result;
            }
            
         
        }
        
}