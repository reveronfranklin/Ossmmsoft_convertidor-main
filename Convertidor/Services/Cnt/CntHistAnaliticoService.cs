using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntHistAnaliticoService : ICntHistAnaliticoService
    {
        private readonly ICntHistAnaliticoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntSaldosService _cntSaldosService;
        private readonly ICntDetalleComprobanteService _cntDetalleComprobanteService;

        public CntHistAnaliticoService(ICntHistAnaliticoRepository repository,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        ICntSaldosService cntSaldosService,
                                        ICntDetalleComprobanteService cntDetalleComprobanteService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntSaldosService = cntSaldosService;
            _cntDetalleComprobanteService = cntDetalleComprobanteService;
        }

        public async Task<CntHistAnaliticoResponseDto> MapHistAnalitico(CNT_HIST_ANALITICO dtos)
        {
            CntHistAnaliticoResponseDto itemResult = new CntHistAnaliticoResponseDto();
            itemResult.CodigoHistAnalitico = dtos.CODIGO_HIST_ANALITICO;
            itemResult.CodigoSaldo = dtos.CODIGO_SALDO;
            itemResult.CodigoDetalleComprobante = dtos.CODIGO_DETALLE_COMPROBANTE;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntHistAnaliticoResponseDto>> MapListHistAnalitico(List<CNT_HIST_ANALITICO> dtos)
        {
            List<CntHistAnaliticoResponseDto> result = new List<CntHistAnaliticoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapHistAnalitico(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntHistAnaliticoResponseDto>>> GetAll()
        {

            ResultDto<List<CntHistAnaliticoResponseDto>> result = new ResultDto<List<CntHistAnaliticoResponseDto>>(null);
            try
            {
                var histAnalitico = await _repository.GetAll();
                var cant = histAnalitico.Count();
                if (histAnalitico != null && histAnalitico.Count() > 0)
                {
                    var listDto = await MapListHistAnalitico(histAnalitico);

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

        public async Task<ResultDto<CntHistAnaliticoResponseDto>> Create(CntHistAnaliticoUpdateDto dto)
        {
            ResultDto<CntHistAnaliticoResponseDto> result = new ResultDto<CntHistAnaliticoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoSaldo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo saldo invalido";
                    return result;

                }

                var codigoSaldo = await _cntSaldosService.GetByCodigo(dto.CodigoSaldo);
                if (codigoSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo saldo invalido";
                    return result;


                }


                if (dto.CodigoDetalleComprobante <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Comprobante invalido";
                    return result;
                }

                var codigoDetalleComprobante = await _cntDetalleComprobanteService.GetByCodigo(dto.CodigoDetalleComprobante);
                if (codigoDetalleComprobante == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Comprobante invalido";
                    return result;

                }

             

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }






                CNT_HIST_ANALITICO entity = new CNT_HIST_ANALITICO();
                entity.CODIGO_HIST_ANALITICO = await _repository.GetNextKey();
                entity.CODIGO_SALDO = dto.CodigoSaldo;
                entity.CODIGO_DETALLE_COMPROBANTE = dto.CodigoDetalleComprobante;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapHistAnalitico(created.Data);
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
