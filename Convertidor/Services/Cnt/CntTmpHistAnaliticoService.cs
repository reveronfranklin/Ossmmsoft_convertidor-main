using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntTmpHistAnaliticoService : ICntTmpHistAnaliticoService
    {
        private readonly ICntTmpHistAnaliticoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntSaldosService _cntSaldosService;
        private readonly ICntDetalleComprobanteService _cntDetalleComprobanteService;

        public CntTmpHistAnaliticoService(ICntTmpHistAnaliticoRepository repository,
                                          ISisUsuarioRepository sisUsuarioRepository,
                                          ICntSaldosService cntSaldosService,
                                          ICntDetalleComprobanteService cntDetalleComprobanteService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntSaldosService = cntSaldosService;
            _cntDetalleComprobanteService = cntDetalleComprobanteService;
        }

        public async Task<CntTmpHistAnaliticoResponseDto> MapTmpHistAnalitico(CNT_TMP_HIST_ANALITICO dtos)
        {
            CntTmpHistAnaliticoResponseDto itemResult = new CntTmpHistAnaliticoResponseDto();
            itemResult.CodigoHistAnalitico = dtos.CODIGO_HIST_ANALITICO;
            itemResult.CodigoSaldo = dtos.CODIGO_SALDO;
            itemResult.CodigoDetalleComprobante = dtos.CODIGO_DETALLE_COMPROBANTE;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntTmpHistAnaliticoResponseDto>> MapListTmpHistAnalitico(List<CNT_TMP_HIST_ANALITICO> dtos)
        {
            List<CntTmpHistAnaliticoResponseDto> result = new List<CntTmpHistAnaliticoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpHistAnalitico(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntTmpHistAnaliticoResponseDto>>> GetAll()
        {

            ResultDto<List<CntTmpHistAnaliticoResponseDto>> result = new ResultDto<List<CntTmpHistAnaliticoResponseDto>>(null);
            try
            {
                var tmpHistAnalitico = await _repository.GetAll();
                var cant = tmpHistAnalitico.Count();
                if (tmpHistAnalitico != null && tmpHistAnalitico.Count() > 0)
                {
                    var listDto = await MapListTmpHistAnalitico(tmpHistAnalitico);

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

        public async Task<ResultDto<CntTmpHistAnaliticoResponseDto>> Create(CntTmpHistAnaliticoUpdateDto dto)
        {
            ResultDto<CntTmpHistAnaliticoResponseDto> result = new ResultDto<CntTmpHistAnaliticoResponseDto>(null);
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






                CNT_TMP_HIST_ANALITICO entity = new CNT_TMP_HIST_ANALITICO();
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
                    var resultDto = await MapTmpHistAnalitico(created.Data);
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

        public async Task<ResultDto<CntTmpHistAnaliticoResponseDto>> Update(CntTmpHistAnaliticoUpdateDto dto)
        {
            ResultDto<CntTmpHistAnaliticoResponseDto> result = new ResultDto<CntTmpHistAnaliticoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoHistAnalitico = await _repository.GetByCodigo(dto.CodigoHistAnalitico);
                if (codigoHistAnalitico == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Historico Analitico no Existe";
                    return result;

                }


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





                codigoHistAnalitico.CODIGO_HIST_ANALITICO = dto.CodigoHistAnalitico;
                codigoHistAnalitico.CODIGO_SALDO = dto.CodigoSaldo;
                codigoHistAnalitico.CODIGO_DETALLE_COMPROBANTE = dto.CodigoDetalleComprobante;
                codigoHistAnalitico.EXTRA1 = dto.Extra1;
                codigoHistAnalitico.EXTRA2 = dto.Extra2;
                codigoHistAnalitico.EXTRA3 = dto.Extra3;




                codigoHistAnalitico.CODIGO_EMPRESA = conectado.Empresa;
                codigoHistAnalitico.USUARIO_UPD = conectado.Usuario;
                codigoHistAnalitico.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoHistAnalitico);

                var resultDto = await MapTmpHistAnalitico(codigoHistAnalitico);
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


    }
}
