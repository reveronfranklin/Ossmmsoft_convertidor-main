using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntTmpAnaliticoService : ICntTmpAnaliticoService
    {
        private readonly ICntTmpAnaliticoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntDetalleComprobanteService _cntDetalleComprobanteService;
        private readonly ICntTmpSaldosService _cntTmpSaldosService;

        public CntTmpAnaliticoService(ICntTmpAnaliticoRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      ICntDetalleComprobanteService cntDetalleComprobanteService,
                                      ICntTmpSaldosService cntTmpSaldosService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntDetalleComprobanteService = cntDetalleComprobanteService;
            _cntTmpSaldosService = cntTmpSaldosService;
        }

        public async Task<CntTmpAnaliticoResponseDto> MapTmpAnalitico(CNT_TMP_ANALITICO dtos)
        {
            CntTmpAnaliticoResponseDto itemResult = new CntTmpAnaliticoResponseDto();
            itemResult.CodigoTmpAnalitico = dtos.CODIGO_TMP_ANALITICO;
            itemResult.CodigoTmpSaldo = dtos.CODIGO_TMP_SALDO;
            itemResult.CodigoDetalleComprobante = dtos.CODIGO_DETALLE_COMPROBANTE;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntTmpAnaliticoResponseDto>> MapListTmpAnalitico(List<CNT_TMP_ANALITICO> dtos)
        {
            List<CntTmpAnaliticoResponseDto> result = new List<CntTmpAnaliticoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpAnalitico(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntTmpAnaliticoResponseDto>>> GetAll()
        {

            ResultDto<List<CntTmpAnaliticoResponseDto>> result = new ResultDto<List<CntTmpAnaliticoResponseDto>>(null);
            try
            {
                var tmpAnalitico = await _repository.GetAll();
                var cant = tmpAnalitico.Count();
                if (tmpAnalitico != null && tmpAnalitico.Count() > 0)
                {
                    var listDto = await MapListTmpAnalitico(tmpAnalitico);

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

        public async Task<ResultDto<CntTmpAnaliticoResponseDto>> Create(CntTmpAnaliticoUpdateDto dto)
        {
            ResultDto<CntTmpAnaliticoResponseDto> result = new ResultDto<CntTmpAnaliticoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoTmpSaldo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tmp saldo invalido";
                    return result;

                }

                var codigoTmpSaldo = await _cntTmpSaldosService.GetByCodigo(dto.CodigoTmpSaldo);
                if (codigoTmpSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tmp saldo invalido";
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






                CNT_TMP_ANALITICO entity = new CNT_TMP_ANALITICO();
                entity.CODIGO_TMP_ANALITICO = await _repository.GetNextKey();
                entity.CODIGO_TMP_SALDO = dto.CodigoTmpSaldo;
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
                    var resultDto = await MapTmpAnalitico(created.Data);
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

        public async Task<ResultDto<CntTmpAnaliticoResponseDto>> Update(CntTmpAnaliticoUpdateDto dto)
        {
            ResultDto<CntTmpAnaliticoResponseDto> result = new ResultDto<CntTmpAnaliticoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoTmpAnalitico = await _repository.GetByCodigo(dto.CodigoTmpAnalitico);
                if (codigoTmpAnalitico == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tmp Analitico no Existe";
                    return result;

                }


                if (dto.CodigoTmpSaldo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tmp saldo invalido";
                    return result;

                }

                var codigoTmpSaldo = await _cntTmpSaldosService.GetByCodigo(dto.CodigoTmpSaldo);
                if (codigoTmpSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tmp saldo invalido";
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





                codigoTmpAnalitico.CODIGO_TMP_ANALITICO = dto.CodigoTmpAnalitico;
                codigoTmpAnalitico.CODIGO_TMP_SALDO = dto.CodigoTmpSaldo;
                codigoTmpAnalitico.CODIGO_DETALLE_COMPROBANTE = dto.CodigoDetalleComprobante;
                codigoTmpAnalitico.EXTRA1 = dto.Extra1;
                codigoTmpAnalitico.EXTRA2 = dto.Extra2;
                codigoTmpAnalitico.EXTRA3 = dto.Extra3;




                codigoTmpAnalitico.CODIGO_EMPRESA = conectado.Empresa;
                codigoTmpAnalitico.USUARIO_UPD = conectado.Usuario;
                codigoTmpAnalitico.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoTmpAnalitico);

                var resultDto = await MapTmpAnalitico(codigoTmpAnalitico);
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

        public async Task<ResultDto<CntTmpAnaliticoDeleteDto>> Delete(CntTmpAnaliticoDeleteDto dto)
        {
            ResultDto<CntTmpAnaliticoDeleteDto> result = new ResultDto<CntTmpAnaliticoDeleteDto>(null);
            try
            {

                var codigoTmpAnalitico = await _repository.GetByCodigo(dto.CodigoTmpAnalitico);
                if (codigoTmpAnalitico == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Tmp Analitico no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoTmpAnalitico);

                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }


    }
}
