using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntAuxiliaresService : ICntAuxiliaresService
    {
        private readonly ICntAuxiliaresRepository _repository;
        private readonly ICntMayoresService _cntMayoresService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;

        public CntAuxiliaresService(ICntAuxiliaresRepository repository,
                                     ICntMayoresService cntMayoresService,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmProveedoresRepository admProveedoresRepository)
        {
            _repository = repository;
            _cntMayoresService = cntMayoresService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admProveedoresRepository = admProveedoresRepository;
        }

        public async Task<CntAuxiliaresResponseDto> MapAuxiliares(CNT_AUXILIARES dtos)
        {
            CntAuxiliaresResponseDto itemResult = new CntAuxiliaresResponseDto();
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
            itemResult.FechaFinVigencia = dtos.FECHA_FIN_VIGENCIA;
            itemResult.FechaFinVigencistring = dtos.FECHA_FIN_VIGENCIA.ToString("u");
            FechaDto fechaFinVigenciaObj =FechaObj.GetFechaDto(dtos.FECHA_FIN_VIGENCIA);
            itemResult.FechaFinVigenciaObj = (FechaDto)fechaFinVigenciaObj;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;

            return itemResult;

        }

        public async Task<List<CntAuxiliaresResponseDto>> MapListAuxiliares(List<CNT_AUXILIARES> dtos)
        {
            List<CntAuxiliaresResponseDto> result = new List<CntAuxiliaresResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapAuxiliares(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntAuxiliaresResponseDto>>> GetAll()
        {

            ResultDto<List<CntAuxiliaresResponseDto>> result = new ResultDto<List<CntAuxiliaresResponseDto>>(null);
            try
            {
                var auxiliares = await _repository.GetAll();
                var cant = auxiliares.Count();
                if (auxiliares != null && auxiliares.Count() > 0)
                {
                    var listDto = await MapListAuxiliares(auxiliares);

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

        public async Task<ResultDto<List<CntAuxiliaresResponseDto>>> GetAllByCodigoMayor(int codigoMayor)
        {

            ResultDto<List<CntAuxiliaresResponseDto>> result = new ResultDto<List<CntAuxiliaresResponseDto>>(null);
            try
            {

                var mayores = await _repository.GetByCodigoMayor(codigoMayor);



                if (mayores.Count() > 0)
                {
                    List<CntAuxiliaresResponseDto> listDto = new List<CntAuxiliaresResponseDto>();

                    foreach (var item in mayores)
                    {
                        var dto = await MapAuxiliares(item);
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

        public async Task<ResultDto<CntAuxiliaresResponseDto>> Create(CntAuxiliaresUpdateDto dto)
        {
            ResultDto<CntAuxiliaresResponseDto> result = new ResultDto<CntAuxiliaresResponseDto>(null);
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

                if (dto.CodigoProveedor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Proveedor Invalido";
                    return result;

                }

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (codigoProveedor == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Proveedor Invalido";
                    return result;

                }





                CNT_AUXILIARES entity = new CNT_AUXILIARES();
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
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;





                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapAuxiliares(created.Data);
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


        public async Task<ResultDto<CntAuxiliaresResponseDto>> Update(CntAuxiliaresUpdateDto dto)
        {
            ResultDto<CntAuxiliaresResponseDto> result = new ResultDto<CntAuxiliaresResponseDto>(null);
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

                if (dto.CodigoProveedor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Proveedor Invalido";
                    return result;

                }

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (codigoProveedor == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Proveedor Invalido";
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
                codigoAuxiliar.CODIGO_PROVEEDOR = dto.CodigoProveedor;




                codigoAuxiliar.CODIGO_EMPRESA = conectado.Empresa;
                codigoAuxiliar.USUARIO_UPD = conectado.Usuario;
                codigoAuxiliar.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoAuxiliar);

                var resultDto = await MapAuxiliares(codigoAuxiliar);
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


        public async Task<ResultDto<CntAuxiliaresDeleteDto>> Delete(CntAuxiliaresDeleteDto dto)
        {
            ResultDto<CntAuxiliaresDeleteDto> result = new ResultDto<CntAuxiliaresDeleteDto>(null);
            try
            {

                var codigoAuxiliar = await _repository.GetByCodigo(dto.CodigoAuxiliar);
                if (codigoAuxiliar == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoAuxiliar);

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
