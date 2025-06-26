using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Pagos;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmPagosNotasTercerosService : IAdmPagosNotasTercerosService
    {
        private readonly IAdmPagosNotasTercerosRepository _repository;
      
        public AdmPagosNotasTercerosService(  IAdmPagosNotasTercerosRepository repository)
        {
            _repository = repository;
           
        }

   

        public async Task<PagoTercerosReportDto> MapReportDto(ADM_V_NOTAS_TERCEROS dtos)
        {
            PagoTercerosReportDto itemResult = new PagoTercerosReportDto();
            itemResult.CodigoLotePago = dtos.CODIGO_LOTE_PAGO;
            itemResult.CodigoCheque = dtos.CODIGO_CHEQUE;
            itemResult.NumeroCheque = dtos.NUMERO_CHEQUE;
            itemResult.FechaCheque = dtos.FECHA_CHEQUE;
            itemResult.Nombre = dtos.NOMBRE;
            itemResult.NoCuenta = dtos.NO_CUENTA;
            itemResult.PagarALaOrdenDe = dtos.PAGAR_A_LA_ORDEN_DE;
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.Monto = dtos.MONTO;
            itemResult.Endoso = dtos.ENDOSO;
            itemResult.UsuarioIns = dtos.USUARIO_INS;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.DetalleOpIcpPuc = dtos.DETALLE_OP_ICP_PUC;
            itemResult.MontoOpIcpPuc = dtos.MONTO_OP_ICP_PUC;
            itemResult.DetalleImpRet = dtos.DETALLE_IMP_RET;
            itemResult.MontoImpRet = dtos.MONTO_IMP_RET;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
 
      

            return itemResult;
        }

        public async Task<List<PagoTercerosReportDto>> MapListReportDto(List<ADM_V_NOTAS_TERCEROS> dtos)
        {
            List<PagoTercerosReportDto> result = new List<PagoTercerosReportDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapReportDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<PagoTercerosReportDto>>> GetByLotePago(PagoTerceroFilterDto filterDto)
        {

            ResultDto<List<PagoTercerosReportDto>> result = new ResultDto<List<PagoTercerosReportDto>>(null);
            try
            {
                List<ADM_V_NOTAS_TERCEROS> pagosNotas = new List<ADM_V_NOTAS_TERCEROS>();

                if (filterDto.CodigoLotePago > 0 && filterDto.CodigoPago == 0)
                {
                    pagosNotas = await _repository.GetByLote(filterDto.CodigoLotePago);
                }
                if (filterDto.CodigoLotePago > 0 && filterDto.CodigoPago > 0)
                {
                    pagosNotas = await _repository.GetByLoteCodigoPago(filterDto.CodigoLotePago, filterDto.CodigoPago);
                }
              
               
                
                
                var cant = pagosNotas.Count();
                if (pagosNotas != null && pagosNotas.Count() > 0)
                {
                    var listDto = await MapListReportDto(pagosNotas);

                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No data";

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }
      
        
    }
 }

