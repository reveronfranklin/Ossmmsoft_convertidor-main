using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm.AdmLotePagoService;

public partial class AdmLotePagoService
{
          public  async Task<AdmLotePagoResponseDto> MapAdmLotePagoDto(ADM_LOTE_PAGO dtos)
        {
            AdmLotePagoResponseDto itemResult = new AdmLotePagoResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                
                itemResult.CodigoLotePago = dtos.CODIGO_LOTE_PAGO;
                itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
             
                var cuenta=await _sisCuentaBancoRepository.GetByCodigo(dtos.CODIGO_CUENTA_BANCO);
                if (cuenta != null)
                {
                    itemResult.NumeroCuenta = cuenta.NO_CUENTA;
                    var banco = await _bancoRepository.GetById(cuenta.CODIGO_BANCO);
                    if (banco != null)
                    {
                        itemResult.CodigoBanco = cuenta.CODIGO_BANCO;
                        itemResult.NombreBanco = banco.NOMBRE;
                    }
                }
                itemResult.TipoPagoId = dtos.TIPO_PAGO_ID;
                itemResult.DescripcionTipoPago = "";
                var descriptivaTipoCuenta = await _admDescriptivaRepository.GetByCodigo(itemResult.TipoPagoId);
                if (descriptivaTipoCuenta!=null)
                {
                    itemResult.DescripcionTipoPago = descriptivaTipoCuenta.DESCRIPCION;
                }
                itemResult.FechaPago = dtos.FECHA_PAGO;
                itemResult.FechaPagoString=Fecha.GetFechaString(dtos.FECHA_PAGO);
                itemResult.FechaPagoDto=Fecha.GetFechaDto(dtos.FECHA_PAGO);
                itemResult.CodigoPresupuesto = (int)dtos.CODIGO_PRESUPUESTO;
                itemResult.SearchText=dtos.SEARCH_TEXT;
                itemResult.Status = dtos.STATUS;
                itemResult.Titulo = dtos.TITULO;
                itemResult.FileName=$"{dtos.FILE_NAME}";
                

      
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<AdmLotePagoResponseDto>> MapListLotePagoDto(List<ADM_LOTE_PAGO> dtos)
        {
            List<AdmLotePagoResponseDto> result = new List<AdmLotePagoResponseDto>();
            if (dtos.Count > 0)
            {
                foreach (var item in dtos)
                {
                    if (item == null)
                    {
                        var detener = "";
                    }
                    else
                    {
                        var itemResult =  await MapAdmLotePagoDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }

}