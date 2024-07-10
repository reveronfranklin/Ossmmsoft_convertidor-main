using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class TmpAuxiliaresService : ITmpAuxiliaresService
    {
        private readonly ITmpAuxiliaresRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntMayoresService _cntMayoresService;

        public TmpAuxiliaresService(ITmpAuxiliaresRepository repository,
                                    ISisUsuarioRepository sisUsuarioRepository,
                                    ICntMayoresService cntMayoresService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntMayoresService = cntMayoresService;
        }

        public async Task<TmpAuxiliaresResponseDto> MapTmpAuxiliares(TMP_AUXILIARES dtos)
        {
            TmpAuxiliaresResponseDto itemResult = new TmpAuxiliaresResponseDto();
            itemResult.CodigoAuxiliar = dtos.CODIGO_AUXILIAR;
            itemResult.CodigoMayor = dtos.CODIGO_MAYOR;
            itemResult.Segmento1 = dtos.SEGMENTO1;
            itemResult.Segmento2 = dtos.SEGMENTO2;
            itemResult.Segmento3 = dtos.SEGMENTO3;
            itemResult.Segmento4 = dtos.SEGMENTO4;
            itemResult.Segmento5 = dtos.SEGMENTO5;
            itemResult.Segmento6 = dtos.SEGMENTO6;
            itemResult.Segmento7 = dtos.SEGMENTO7;
            itemResult.Segmento8 = dtos.SEGMENTO8;
            itemResult.Segmento10 = dtos.SEGMENTO10;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
           

            return itemResult;

        }

        public async Task<List<TmpAuxiliaresResponseDto>> MapListTmpAuxiliares(List<TMP_AUXILIARES> dtos)
        {
            List<TmpAuxiliaresResponseDto> result = new List<TmpAuxiliaresResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpAuxiliares(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<TmpAuxiliaresResponseDto>>> GetAll()
        {

            ResultDto<List<TmpAuxiliaresResponseDto>> result = new ResultDto<List<TmpAuxiliaresResponseDto>>(null);
            try
            {
                var tmpAuxiliares = await _repository.GetAll();
                var cant = tmpAuxiliares.Count();
                if (tmpAuxiliares != null && tmpAuxiliares.Count() > 0)
                {
                    var listDto = await MapListTmpAuxiliares(tmpAuxiliares);

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

        public async Task<ResultDto<TmpAuxiliaresResponseDto>> Create(TmpAuxiliaresUpdateDto dto)
        {
            ResultDto<TmpAuxiliaresResponseDto> result = new ResultDto<TmpAuxiliaresResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoMayor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Mayor invalido";
                    return result;
                }

                var codigoMayor = await _cntMayoresService.GetByCodigo(dto.CodigoMayor);
                if (codigoMayor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Mayor invalido";
                    return result;

                }

                if (dto.Segmento1 is not null && dto.Segmento1.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Segmento1 Invalido";
                    return result;
                }
                if (dto.Segmento2 is not null && dto.Segmento2.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Segmento2 Invalido";
                    return result;
                }
                if (dto.Segmento3 is not null && dto.Segmento3.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Segmento3 Invalido";
                    return result;
                }


                if (dto.Denominacion.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }

                if (dto.Descripcion is not null && dto.Descripcion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
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



            




                TMP_AUXILIARES entity = new TMP_AUXILIARES();
                entity.CODIGO_AUXILIAR = await _repository.GetNextKey();
                entity.CODIGO_MAYOR = dto.CodigoMayor;
                entity.SEGMENTO1 = dto.Segmento1;
                entity.SEGMENTO2 = dto.Segmento2;
                entity.SEGMENTO3 = dto.Segmento3;
                entity.SEGMENTO4 = dto.Segmento4;
                entity.SEGMENTO5 = dto.Segmento5;
                entity.SEGMENTO6 = dto.Segmento6;
                entity.SEGMENTO7 = dto.Segmento7;
                entity.SEGMENTO8 = dto.Segmento8;
                entity.SEGMENTO9 = dto.Segmento9;
                entity.SEGMENTO10 = dto.Segmento10;
                entity.DENOMINACION = dto.Denominacion;
                entity.DESCRIPCION = dto.Descripcion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
             





                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapTmpAuxiliares(created.Data);
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


        public async Task<ResultDto<TmpAuxiliaresResponseDto>> Update(TmpAuxiliaresUpdateDto dto)
        {
            ResultDto<TmpAuxiliaresResponseDto> result = new ResultDto<TmpAuxiliaresResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoAuxiliar = await _repository.GetByCodigo(dto.CodigoAuxiliar);
                if (codigoAuxiliar == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar Invalido";
                    return result;

                }


                if (dto.CodigoMayor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Mayor invalido";
                    return result;
                }

                var codigoMayor = await _cntMayoresService.GetByCodigo(dto.CodigoMayor);
                if (codigoMayor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Mayor invalido";
                    return result;

                }

                if (dto.Segmento1 is not null && dto.Segmento1.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Segmento1 Invalido";
                    return result;
                }
                if (dto.Segmento2 is not null && dto.Segmento2.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Segmento2 Invalido";
                    return result;
                }
                if (dto.Segmento3 is not null && dto.Segmento3.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Segmento3 Invalido";
                    return result;
                }


                if (dto.Denominacion.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }

                if (dto.Descripcion is not null && dto.Descripcion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
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

             





                codigoAuxiliar.CODIGO_AUXILIAR = dto.CodigoAuxiliar;
                codigoAuxiliar.CODIGO_MAYOR = dto.CodigoMayor;
                codigoAuxiliar.SEGMENTO1 = dto.Segmento1;
                codigoAuxiliar.SEGMENTO2 = dto.Segmento2;
                codigoAuxiliar.SEGMENTO3 = dto.Segmento3;
                codigoAuxiliar.SEGMENTO4 = dto.Segmento4;
                codigoAuxiliar.SEGMENTO5 = dto.Segmento5;
                codigoAuxiliar.SEGMENTO6 = dto.Segmento6;
                codigoAuxiliar.SEGMENTO7 = dto.Segmento7;
                codigoAuxiliar.SEGMENTO8 = dto.Segmento8;
                codigoAuxiliar.SEGMENTO9 = dto.Segmento9;
                codigoAuxiliar.SEGMENTO10 = dto.Segmento10;
                codigoAuxiliar.DENOMINACION = dto.Denominacion;
                codigoAuxiliar.DESCRIPCION = dto.Descripcion;
                codigoAuxiliar.EXTRA1 = dto.Extra1;
                codigoAuxiliar.EXTRA2 = dto.Extra2;
                codigoAuxiliar.EXTRA3 = dto.Extra3;





                codigoAuxiliar.CODIGO_EMPRESA = conectado.Empresa;
                codigoAuxiliar.USUARIO_UPD = conectado.Usuario;
                codigoAuxiliar.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoAuxiliar);

                var resultDto = await MapTmpAuxiliares(codigoAuxiliar);
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
