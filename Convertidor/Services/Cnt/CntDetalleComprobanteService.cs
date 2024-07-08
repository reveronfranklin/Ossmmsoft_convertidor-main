using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntDetalleComprobanteService : ICntDetalleComprobanteService
    {
        private readonly ICntDetalleComprobanteRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntComprobantesService _cntComprobantesService;
        private readonly ICntMayoresService _cntMayoresService;
        private readonly ICntAuxiliaresService _cntAuxiliaresService;

        public CntDetalleComprobanteService(ICntDetalleComprobanteRepository repository,
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

        public async Task<ResultDto<CntDetalleComprobanteResponseDto>> Create(CntDetalleComprobanteUpdateDto dto)
        {
            ResultDto<CntDetalleComprobanteResponseDto> result = new ResultDto<CntDetalleComprobanteResponseDto>(null);
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

                if(dto.Descripcion != string.Empty && dto.Descripcion.Length > 200) 
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






                CNT_DETALLE_COMPROBANTE entity = new CNT_DETALLE_COMPROBANTE();
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
                    var resultDto = await MapDetalleComprobante(created.Data);
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
