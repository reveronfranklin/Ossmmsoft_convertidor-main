using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntDetalleComprobanteService : ICntDetalleComprobanteService
    {
        private readonly ICntDetalleComprobanteRepository _repository;

        public CntDetalleComprobanteService(ICntDetalleComprobanteRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntDetalleComprobanteResponseDto> MapDetalleComprobante(CNT_DETALLE_COMPROBANTE dtos)
        {
            CntDetalleComprobanteResponseDto itemResult = new CntDetalleComprobanteResponseDto();
            itemResult.CodigoDetalleComprobante = dtos.CODIGO_DETALLE_COMPROBANTE;
            itemResult.CodigoComprobante = dtos.CODIGO_COMPROBANTE;
            itemResult.CodigoMayor = dtos.CODIGO_MAYOR;
            itemResult.CodigoAuxiliar = dtos.CODIGO_AUXILIAR;
            itemResult.Referencia1 = dtos.REFERENCIA1;
            itemResult.Referencia2 = dtos.REFERENCIA2;
            itemResult.Referencia3 = dtos.REFERENCIA3;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Monto = dtos.MONTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntDetalleComprobanteResponseDto>> MapListDetalleComprobante(List<CNT_DETALLE_COMPROBANTE> dtos)
        {
            List<CntDetalleComprobanteResponseDto> result = new List<CntDetalleComprobanteResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDetalleComprobante(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntDetalleComprobanteResponseDto>>> GetAll()
        {

            ResultDto<List<CntDetalleComprobanteResponseDto>> result = new ResultDto<List<CntDetalleComprobanteResponseDto>>(null);
            try
            {
                var detalleComprobante = await _repository.GetAll();
                var cant = detalleComprobante.Count();
                if (detalleComprobante != null && detalleComprobante.Count() > 0)
                {
                    var listDto = await MapListDetalleComprobante(detalleComprobante);

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

        public async Task<ResultDto<List<CntDetalleComprobanteResponseDto>>> GetAllByCodigoComprobante(int codigoComprobante)
        {

            ResultDto<List<CntDetalleComprobanteResponseDto>> result = new ResultDto<List<CntDetalleComprobanteResponseDto>>(null);
            try
            {

                var comprobantes = await _repository.GetByCodigoComprobante(codigoComprobante);



                if (comprobantes.Count() > 0)
                {
                    List<CntDetalleComprobanteResponseDto> listDto = new List<CntDetalleComprobanteResponseDto>();

                    foreach (var item in comprobantes)
                    {
                        var dto = await MapDetalleComprobante(item);
                        listDto.Add(dto);
                    }


                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = "No existen Datos";

                }
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
