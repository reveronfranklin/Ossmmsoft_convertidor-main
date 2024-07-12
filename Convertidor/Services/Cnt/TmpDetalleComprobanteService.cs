using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class TmpDetalleComprobanteService : ITmpDetalleComprobanteService
    {
        private readonly ITmpDetalleComprobanteRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntComprobantesService _cntComprobantesService;
        private readonly ICntMayoresService _cntMayoresService;
        private readonly ICntAuxiliaresService _cntAuxiliaresService;
        public TmpDetalleComprobanteService(ITmpDetalleComprobanteRepository repository,
                                            ISisUsuarioRepository sisUsuarioRepository,
                                            ICntComprobantesService cntComprobantesService,
                                            ICntMayoresService cntMayoresService,
                                            ICntAuxiliaresService cntAuxiliaresService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntComprobantesService = cntComprobantesService;
            _cntMayoresService = cntMayoresService;
            _cntAuxiliaresService = cntAuxiliaresService;
        }

        public async Task<TmpDetalleComprobanteResponseDto> MapTmpDetalleComprobante(TMP_DETALLE_COMPROBANTE dtos)
        {
            TmpDetalleComprobanteResponseDto itemResult = new TmpDetalleComprobanteResponseDto();
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

        public async Task<List<TmpDetalleComprobanteResponseDto>> MapListTmpDetalleComprobante(List<TMP_DETALLE_COMPROBANTE> dtos)
        {
            List<TmpDetalleComprobanteResponseDto> result = new List<TmpDetalleComprobanteResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpDetalleComprobante(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<TmpDetalleComprobanteResponseDto>>> GetAll()
        {

            ResultDto<List<TmpDetalleComprobanteResponseDto>> result = new ResultDto<List<TmpDetalleComprobanteResponseDto>>(null);
            try
            {
                var tmpDetalleComprobante = await _repository.GetAll();
                var cant = tmpDetalleComprobante.Count();
                if (tmpDetalleComprobante != null && tmpDetalleComprobante.Count() > 0)
                {
                    var listDto = await MapListTmpDetalleComprobante(tmpDetalleComprobante);

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

        public async Task<ResultDto<TmpDetalleComprobanteResponseDto>> Create(TmpDetalleComprobanteUpdateDto dto)
        {
            ResultDto<TmpDetalleComprobanteResponseDto> result = new ResultDto<TmpDetalleComprobanteResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoComprobante <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Comprobrante invalido";
                    return result;

                }

                var codigoComprobante = await _cntComprobantesService.GetByCodigo(dto.CodigoComprobante);
                if (codigoComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Comprobante invalido";
                    return result;


                }


                if (dto.CodigoMayor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mayor invalido";
                    return result;
                }

                var codigoMayor = await _cntMayoresService.GetByCodigo(dto.CodigoMayor);
                if (codigoMayor == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mayor invalido";
                    return result;

                }

                if (dto.CodigoAuxiliar <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar Invalido";
                    return result;

                }

                var codigoAuxiliar = await _cntAuxiliaresService.GetByCodigo(dto.CodigoAuxiliar);
                if (codigoAuxiliar == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar Invalido";
                    return result;
                }

                if (dto.Referencia1.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Referencia1  Invalida";
                    return result;

                }

                if (dto.Referencia2.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Referencia2 Invalida";
                    return result;


                }

                if (dto.Descripcion != string.Empty && dto.Descripcion.Length > 200)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;

                }

                if (dto.Monto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
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






                TMP_DETALLE_COMPROBANTE entity = new TMP_DETALLE_COMPROBANTE();
                entity.CODIGO_DETALLE_COMPROBANTE = await _repository.GetNextKey();
                entity.CODIGO_COMPROBANTE = dto.CodigoComprobante;
                entity.CODIGO_MAYOR = dto.CodigoMayor;
                entity.CODIGO_AUXILIAR = dto.CodigoAuxiliar;
                entity.REFERENCIA1 = dto.Referencia1;
                entity.REFERENCIA2 = dto.Referencia2;
                entity.REFERENCIA3 = dto.Referencia3;
                entity.DESCRIPCION = dto.Descripcion;
                entity.MONTO = dto.Monto;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapTmpDetalleComprobante(created.Data);
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

        public async Task<ResultDto<TmpDetalleComprobanteResponseDto>> Update(TmpDetalleComprobanteUpdateDto dto)
        {
            ResultDto<TmpDetalleComprobanteResponseDto> result = new ResultDto<TmpDetalleComprobanteResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoDetalleComprobante = await _repository.GetByCodigo(dto.CodigoDetalleComprobante);
                if (codigoDetalleComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Comprobante no Existe";
                    return result;

                }


                if (dto.CodigoComprobante <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Comprobrante invalido";
                    return result;

                }

                var codigoComprobante = await _cntComprobantesService.GetByCodigo(dto.CodigoComprobante);
                if (codigoComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Comprobante invalido";
                    return result;


                }


                if (dto.CodigoMayor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mayor invalido";
                    return result;
                }

                var codigoMayor = await _cntMayoresService.GetByCodigo(dto.CodigoMayor);
                if (codigoMayor == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mayor invalido";
                    return result;

                }

                if (dto.CodigoAuxiliar <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar Invalido";
                    return result;

                }

                var codigoAuxiliar = await _cntAuxiliaresService.GetByCodigo(dto.CodigoAuxiliar);
                if (codigoAuxiliar == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar Invalido";
                    return result;
                }

                if (dto.Referencia1.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Referencia1  Invalida";
                    return result;

                }

                if (dto.Referencia2.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Referencia2 Invalida";
                    return result;


                }

                if (dto.Descripcion != string.Empty && dto.Descripcion.Length > 200)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;

                }

                if (dto.Monto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
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





                codigoDetalleComprobante.CODIGO_DETALLE_COMPROBANTE = dto.CodigoDetalleComprobante;
                codigoDetalleComprobante.CODIGO_COMPROBANTE = dto.CodigoComprobante;
                codigoDetalleComprobante.CODIGO_MAYOR = dto.CodigoMayor;
                codigoDetalleComprobante.CODIGO_AUXILIAR = dto.CodigoAuxiliar;
                codigoDetalleComprobante.REFERENCIA1 = dto.Referencia1;
                codigoDetalleComprobante.REFERENCIA2 = dto.Referencia2;
                codigoDetalleComprobante.REFERENCIA3 = dto.Referencia3;
                codigoDetalleComprobante.DESCRIPCION = dto.Descripcion;
                codigoDetalleComprobante.MONTO = dto.Monto;
                codigoDetalleComprobante.EXTRA1 = dto.Extra1;
                codigoDetalleComprobante.EXTRA2 = dto.Extra2;
                codigoDetalleComprobante.EXTRA3 = dto.Extra3;




                codigoDetalleComprobante.CODIGO_EMPRESA = conectado.Empresa;
                codigoDetalleComprobante.USUARIO_UPD = conectado.Usuario;
                codigoDetalleComprobante.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoDetalleComprobante);

                var resultDto = await MapTmpDetalleComprobante(codigoDetalleComprobante);
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

        public async Task<ResultDto<TmpDetalleComprobanteDeleteDto>> Delete(TmpDetalleComprobanteDeleteDto dto)
        {
            ResultDto<TmpDetalleComprobanteDeleteDto> result = new ResultDto<TmpDetalleComprobanteDeleteDto>(null);
            try
            {

                var codigoDetalleComprobante = await _repository.GetByCodigo(dto.CodigoDetalleComprobante);
                if (codigoDetalleComprobante == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Comprobante no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDetalleComprobante);

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
