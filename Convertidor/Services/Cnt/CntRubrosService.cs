using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntRubrosService : ICntRubrosService
    {
        private readonly ICntRubrosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CntRubrosService(ICntRubrosRepository repository,
                                 ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CntRubrosResponseDto> MapRubros(CNT_RUBROS dtos)
        {
            CntRubrosResponseDto itemResult = new CntRubrosResponseDto();
            itemResult.CodigoRubro = dtos.CODIGO_RUBRO;
            itemResult.NumeroRubro = dtos.NUMERO_RUBRO;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.Extra4 = dtos.EXTRA4;
            itemResult.Extra5 = dtos.EXTRA5;
            itemResult.Extra6 = dtos.EXTRA6;
            itemResult.Extra7 = dtos.EXTRA7;
            itemResult.Extra8 = dtos.EXTRA8;
            itemResult.Extra9 = dtos.EXTRA9;
            itemResult.Extra10 = dtos.EXTRA10;
            itemResult.Extra11 = dtos.EXTRA11;
            itemResult.Extra12 = dtos.EXTRA12;
            itemResult.Extra13 = dtos.EXTRA13;
            itemResult.Extra14 = dtos.EXTRA14;
            itemResult.Extra15 = dtos.EXTRA15;

            return itemResult;

        }

        public async Task<List<CntRubrosResponseDto>> MapListRubros(List<CNT_RUBROS> dtos)
        {
            List<CntRubrosResponseDto> result = new List<CntRubrosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapRubros(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntRubrosResponseDto>>> GetAll()
        {

            ResultDto<List<CntRubrosResponseDto>> result = new ResultDto<List<CntRubrosResponseDto>>(null);
            try
            {
                var rubros = await _repository.GetAll();
                var cant = rubros.Count();
                if (rubros != null && rubros.Count() > 0)
                {
                    var listDto = await MapListRubros(rubros);

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

        public async Task<ResultDto<CntRubrosResponseDto>> Create(CntRubrosUpdateDto dto)
        {
            ResultDto<CntRubrosResponseDto> result = new ResultDto<CntRubrosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var numeroRubro = Convert.ToInt32(dto.NumeroRubro);
                if(numeroRubro <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Rubro invalido";
                    return result;


                }


                if (dto.NumeroRubro.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Rubro invalido";
                    return result;
                }

                if (dto.Denominacion.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
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






                CNT_RUBROS entity = new CNT_RUBROS();
                entity.CODIGO_RUBRO = await _repository.GetNextKey();
                entity.NUMERO_RUBRO = dto.NumeroRubro;
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
                    var resultDto = await MapRubros(created.Data);
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
