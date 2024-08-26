using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatCalcXTriangulacionService : ICatCalcXTriangulacionService
    {
        private readonly ICatCalcXTriangulacionRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CatCalcXTriangulacionService(ICatCalcXTriangulacionRepository repository,
                                             ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CatCalcXTriangulacionResponseDto> MapCalcXTriangulacion(CAT_CALC_X_TRIANGULACION entity)
        {
            CatCalcXTriangulacionResponseDto dto = new CatCalcXTriangulacionResponseDto();

            dto.CodigoTriangulacion = entity.CODIGO_TRIANGULACION;
            dto.CodigoFicha = entity.CODIGO_FICHA;
            dto.CodigoAvaluoConstruccion = entity.CODIGO_AVALUO_CONSTRUCCION;
            dto.CatetoA = entity.CATETO_A;
            dto.CatetoB = entity.CATETO_B;
            dto.CatetoC = entity.CATETO_C;
            dto.AreaParcial = entity.AREA_PARCIAL;
            dto.AreaComplementaria = entity.AREA_COMPLEMENTARIA;
            dto.Observaciones = entity.OBSERVACIONES;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.Extra4 = entity.EXTRA4;
            dto.Extra5 = entity.EXTRA5;
            dto.Extra6 = entity.EXTRA6;
            dto.Extra7 = entity.EXTRA7;
            dto.Extra8 = entity.EXTRA8;
            dto.Extra9 = entity.EXTRA9;
            dto.Extra10 = entity.EXTRA10;
            dto.Extra11 = entity.EXTRA11;
            dto.Extra12 = entity.EXTRA12;
            dto.Extra13 = entity.EXTRA13;
            dto.Extra14 = entity.EXTRA14;
            dto.Extra15 = entity.EXTRA15;


            return dto;

        }

        public async Task<List<CatCalcXTriangulacionResponseDto>> MapListCalcXTriangulacion(List<CAT_CALC_X_TRIANGULACION> dtos)
        {
            List<CatCalcXTriangulacionResponseDto> result = new List<CatCalcXTriangulacionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCalcXTriangulacion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatCalcXTriangulacionResponseDto>>> GetAll()
        {

            ResultDto<List<CatCalcXTriangulacionResponseDto>> result = new ResultDto<List<CatCalcXTriangulacionResponseDto>>(null);
            try
            {
                var calcTriangulacion = await _repository.GetAll();
                var cant = calcTriangulacion.Count();
                if (calcTriangulacion != null && calcTriangulacion.Count() > 0)
                {
                    var listDto = await MapListCalcXTriangulacion(calcTriangulacion);

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

        public async Task<ResultDto<CatCalcXTriangulacionResponseDto>> Create(CatCalcXTriangulacionUpdateDto dto)
        {

            ResultDto<CatCalcXTriangulacionResponseDto> result = new ResultDto<CatCalcXTriangulacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoFicha <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Ficha Invalido ";
                    return result;
                }

                if (dto.CodigoAvaluoConstruccion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Avaluo Contruccion Invalido";
                    return result;

                }

                if (dto.CatetoA <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cateto A Invalido";
                    return result;

                }

                if (dto.CatetoB <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cateto b Invalido";
                    return result;

                }

                if (dto.CatetoB <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cateto b Invalido";
                    return result;

                }

                if (dto.AreaParcial <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Parcial Invalida";
                    return result;

                }

                if (dto.AreaComplementaria <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Complementaria Invalida";
                    return result;

                }

                if (dto.Observaciones.Length > 200)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observaciones Invalidas";
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

                if (dto.Extra4 is not null && dto.Extra4.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra4 Invalido";
                    return result;
                }
                if (dto.Extra5 is not null && dto.Extra5.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra5 Invalido";
                    return result;
                }

                if (dto.Extra6 is not null && dto.Extra6.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra6 Invalido";
                    return result;
                }

                if (dto.Extra7 is not null && dto.Extra7.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra7 Invalido";
                    return result;
                }
                if (dto.Extra8 is not null && dto.Extra8.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra8 Invalido";
                    return result;
                }

                if (dto.Extra9 is not null && dto.Extra9.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra9 Invalido";
                    return result;
                }

                if (dto.Extra10 is not null && dto.Extra10.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra10 Invalido";
                    return result;
                }
                if (dto.Extra11 is not null && dto.Extra11.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra11 Invalido";
                    return result;
                }

                if (dto.Extra12 is not null && dto.Extra12.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra12 Invalido";
                    return result;
                }

                if (dto.Extra13 is not null && dto.Extra13.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra13 Invalido";
                    return result;
                }
                if (dto.Extra14 is not null && dto.Extra14.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra14 Invalido";
                    return result;
                }

                if (dto.Extra15 is not null && dto.Extra15.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra15 Invalido";
                    return result;
                }



                CAT_CALC_X_TRIANGULACION entity = new CAT_CALC_X_TRIANGULACION();
                entity.CODIGO_TRIANGULACION = await _repository.GetNextKey();
                entity.CODIGO_FICHA = dto.CodigoFicha;
                entity.CODIGO_AVALUO_CONSTRUCCION = dto.CodigoAvaluoConstruccion;
                entity.CATETO_A = dto.CatetoA;
                entity.CATETO_B = dto.CatetoB;
                entity.CATETO_C = dto.CatetoC;
                entity.AREA_PARCIAL = dto.AreaParcial;
                entity.AREA_COMPLEMENTARIA = dto.AreaComplementaria;
                entity.OBSERVACIONES = dto.Observaciones;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.EXTRA4 = dto.Extra4;
                entity.EXTRA5 = dto.Extra5;
                entity.EXTRA6 = dto.Extra6;
                entity.EXTRA7 = dto.Extra7;
                entity.EXTRA8 = dto.Extra8;
                entity.EXTRA9 = dto.Extra9;
                entity.EXTRA10 = dto.Extra10;
                entity.EXTRA11 = dto.Extra11;
                entity.EXTRA12 = dto.Extra12;
                entity.EXTRA13 = dto.Extra13;
                entity.EXTRA14 = dto.Extra14;
                entity.EXTRA15 = dto.Extra15;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCalcXTriangulacion(created.Data);
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
