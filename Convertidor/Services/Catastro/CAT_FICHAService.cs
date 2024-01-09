using Convertidor.Dtos;
using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
	public class CAT_FICHAService: ICAT_FICHAService
    {
		

        private readonly ICAT_FICHARepository _repository;


        public CAT_FICHAService(ICAT_FICHARepository repository)

        {
            _repository = repository;

        }

      

        public async Task<ResultDto<GetCatFichaDto>> GetByKey(int codigoCatFicha)
        {
            GetCatFichaDto resultDto = new GetCatFichaDto();
           
            ResultDto<GetCatFichaDto> response = new ResultDto<GetCatFichaDto>(resultDto);
            try
            {
                var result = await _repository.GetByKey(codigoCatFicha);

                resultDto = MapCatFichaToGetCatFichaDto(result);
                response.Data = resultDto;
                response.IsValid = true;
                response.Message = "";
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsValid = true;
                response.Message = ex.InnerException.Message;
                return response;
            }
           
        }

        public async Task<ResultDto<GetCatFichaDto>> Create(CreateCatFichaDto dto)
        {


            GetCatFichaDto resultDto = new GetCatFichaDto();

            ResultDto<GetCatFichaDto> response = new ResultDto<GetCatFichaDto>(resultDto);
            try
            {
                var next = await _repository.GetNext();

                var catFicha = MapCreateCatFichaDtoToCatFichaDto(dto);
               
                var catFichaCreted =await _repository.Add(catFicha);
                response = await GetByKey(next);
              
                response.IsValid = true;
                response.Message = "";
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsValid = true;
                response.Message = ex.InnerException.Message;
                return response;
            }

        }

        public async Task<ResultDto<GetCatFichaDto>> Update(UpdateCatFichaDto dto)
        {


            GetCatFichaDto resultDto = new GetCatFichaDto();

            ResultDto<GetCatFichaDto> response = new ResultDto<GetCatFichaDto>(resultDto);
            try
            {

                var validate = await ValidateUpdateCatFicha(dto);
          
                if (validate.IsValid) {
                    var catFicha = MapUpdateCatFichaDtoToCatFicha(dto);
                    await _repository.Update(catFicha);
                    response = await GetByKey(dto.CODIGO_FICHA);
                }
                else
                {
                    response.Data = null;
                    response.IsValid = validate.IsValid;
                    response.Message = validate.Message;
                }

             
          
             

               
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsValid = true;
                response.Message = ex.InnerException.Message;
                return response;
            }

        }


        public async Task<ResultDto<bool>> ValidateUpdateCatFicha(UpdateCatFichaDto dto)
        {
            ResultDto<bool> response = new ResultDto<bool>(true);

            var ficha = await GetByKey(dto.CODIGO_FICHA);
            if (ficha.Data != null)
            {
                response.Data = false;
                response.IsValid = false;
                response.Message = "Ficha no existe";
            }


            return response;
        }

        public GetCatFichaDto MapCatFichaToGetCatFichaDto(CAT_FICHA entity)
        {
            GetCatFichaDto dto = new GetCatFichaDto();
            dto.CODIGO_FICHA = entity.CODIGO_FICHA;

            dto.CODIGO_VIEJO_CAT = entity.CODIGO_VIEJO_CAT;

            dto.CODIGO_PARCELA = entity.CODIGO_PARCELA;

            dto.CODIGO_INMUEBLE = entity.CODIGO_INMUEBLE;

            dto.ESTATUS_FICHA_ID = entity.ESTATUS_FICHA_ID;


            dto.AMBITO_ID = entity.AMBITO_ID;

            dto.MANZANA_ID = entity.MANZANA_ID;

            dto.SUB_SECTOR_ID = entity.SUB_SECTOR_ID;

            dto.PARCELA_ID = entity.PARCELA_ID;

            dto.SUB_PARCELA_ID = entity.SUB_PARCELA_ID;

            dto.NIVEL_ID = entity.NIVEL_ID;

            dto.UNIDAD_ID = entity.UNIDAD_ID;

            dto.FICHA_CATASTRAL_NUM = entity.FICHA_CATASTRAL_NUM;

            dto.FECHA_PRIMERA_VISITA = entity.FECHA_PRIMERA_VISITA;

            dto.FECHA_LEVANTAMIENTO = entity.FECHA_LEVANTAMIENTO;

            dto.CONTROL_ARCHIVO = entity.CONTROL_ARCHIVO;

            dto.INSCRIPCION_CATASTRAL_NUM =entity.INSCRIPCION_CATASTRAL_NUM;

            dto.CODIGO_ZONIFICACION = entity.CODIGO_ZONIFICACION;

            dto.AREA_PARCELA = entity.AREA_PARCELA;

            dto.FRENTE_PARCELA = entity.FRENTE_PARCELA;

            dto.AREA_INMUEBLE = entity.AREA_INMUEBLE;

            dto.AREA_TIPO_PARCELA = entity.AREA_TIPO_PARCELA;

            dto.FRENTE_TIPO_PARCELA = entity.FRENTE_TIPO_PARCELA;


            dto.EXTRA1 = entity.EXTRA1;

            dto.EXTRA2 = entity.EXTRA2;

            dto.EXTRA3 = entity.EXTRA3;

            dto.USUARIO_INS = entity.USUARIO_INS;

            dto.FECHA_INS = entity.FECHA_INS;

            dto.CODIGO_EMPRESA = entity.CODIGO_EMPRESA;

            dto.EXTRA4 = entity.EXTRA4;

            dto.EXTRA5 = entity.EXTRA5;

            dto.EXTRA6 = entity.EXTRA6;

            dto.EXTRA7 = entity.EXTRA7;

            dto.EXTRA8 = entity.EXTRA8;

            dto.EXTRA9 = entity.EXTRA9;

            dto.EXTRA10 = entity.EXTRA10;

            dto.EXTRA11 = entity.EXTRA11;


            dto.EXTRA13 = entity.EXTRA13;

            dto.EXTRA14 = entity.EXTRA14;

            dto.EXTRA15 = entity.EXTRA15;

            dto.NUMERO_CIVICO = entity.NUMERO_CIVICO;

            dto.FECHA_INS_HIST = entity.FECHA_INS_HIST;

            dto.CODIGO_UBICACION_NAC = entity.CODIGO_UBICACION_NAC;

            dto.CODIGO_FICHA_FK = entity.CODIGO_FICHA_FK;

            dto.CODIGO_CATASTRO = entity.CODIGO_CATASTRO;

            return dto;




        }


        public CAT_FICHA MapCreateCatFichaDtoToCatFichaDto(CreateCatFichaDto dto)
        {
            CAT_FICHA entity = new CAT_FICHA();

          

            entity.CODIGO_VIEJO_CAT = entity.CODIGO_VIEJO_CAT;

            entity.CODIGO_PARCELA = entity.CODIGO_PARCELA;

            entity.CODIGO_INMUEBLE = entity.CODIGO_INMUEBLE;

            entity.ESTATUS_FICHA_ID = entity.ESTATUS_FICHA_ID;


            entity.AMBITO_ID = entity.AMBITO_ID;

            entity.MANZANA_ID = entity.MANZANA_ID;

            entity.SUB_SECTOR_ID = entity.SUB_SECTOR_ID;

            entity.PARCELA_ID = entity.PARCELA_ID;

            entity.SUB_PARCELA_ID = entity.SUB_PARCELA_ID;

            entity.NIVEL_ID = entity.NIVEL_ID;

            entity.UNIDAD_ID = entity.UNIDAD_ID;

            entity.FICHA_CATASTRAL_NUM = entity.FICHA_CATASTRAL_NUM;

            entity.FECHA_PRIMERA_VISITA = entity.FECHA_PRIMERA_VISITA;

            entity.FECHA_LEVANTAMIENTO = entity.FECHA_LEVANTAMIENTO;

            entity.CONTROL_ARCHIVO = entity.CONTROL_ARCHIVO;

            entity.INSCRIPCION_CATASTRAL_NUM = entity.INSCRIPCION_CATASTRAL_NUM;

            entity.CODIGO_ZONIFICACION = entity.CODIGO_ZONIFICACION;

            entity.AREA_PARCELA = entity.AREA_PARCELA;

            entity.FRENTE_PARCELA = entity.FRENTE_PARCELA;

            entity.AREA_INMUEBLE = entity.AREA_INMUEBLE;

            entity.AREA_TIPO_PARCELA = entity.AREA_TIPO_PARCELA;

            entity.FRENTE_TIPO_PARCELA = entity.FRENTE_TIPO_PARCELA;


            entity.EXTRA1 = entity.EXTRA1;

            entity.EXTRA2 = entity.EXTRA2;

            entity.EXTRA3 = entity.EXTRA3;

            entity.USUARIO_INS = entity.USUARIO_INS;

            entity.FECHA_INS = entity.FECHA_INS;

            entity.CODIGO_EMPRESA = entity.CODIGO_EMPRESA;

            entity.EXTRA4 = entity.EXTRA4;

            entity.EXTRA5 = entity.EXTRA5;

            entity.EXTRA6 = entity.EXTRA6;

            entity.EXTRA7 = entity.EXTRA7;

            entity.EXTRA8 = entity.EXTRA8;

            entity.EXTRA9 = entity.EXTRA9;

            entity.EXTRA10 = entity.EXTRA10;

            entity.EXTRA11 = entity.EXTRA11;


            entity.EXTRA13 = entity.EXTRA13;

            entity.EXTRA14 = entity.EXTRA14;

            entity.EXTRA15 = entity.EXTRA15;

            entity.NUMERO_CIVICO = entity.NUMERO_CIVICO;

            entity.FECHA_INS_HIST = entity.FECHA_INS_HIST;

            entity.CODIGO_UBICACION_NAC = entity.CODIGO_UBICACION_NAC;

            entity.CODIGO_FICHA_FK = entity.CODIGO_FICHA_FK;

            entity.CODIGO_CATASTRO = entity.CODIGO_CATASTRO;

            return entity;




        }

        public CAT_FICHA MapUpdateCatFichaDtoToCatFicha(UpdateCatFichaDto dto)
        {
            CAT_FICHA entity = new CAT_FICHA();

            entity.CODIGO_FICHA = entity.CODIGO_FICHA;

            entity.CODIGO_VIEJO_CAT = entity.CODIGO_VIEJO_CAT;

            entity.CODIGO_PARCELA = entity.CODIGO_PARCELA;

            entity.CODIGO_INMUEBLE = entity.CODIGO_INMUEBLE;

            entity.ESTATUS_FICHA_ID = entity.ESTATUS_FICHA_ID;


            entity.AMBITO_ID = entity.AMBITO_ID;

            entity.MANZANA_ID = entity.MANZANA_ID;

            entity.SUB_SECTOR_ID = entity.SUB_SECTOR_ID;

            entity.PARCELA_ID = entity.PARCELA_ID;

            entity.SUB_PARCELA_ID = entity.SUB_PARCELA_ID;

            entity.NIVEL_ID = entity.NIVEL_ID;

            entity.UNIDAD_ID = entity.UNIDAD_ID;

            entity.FICHA_CATASTRAL_NUM = entity.FICHA_CATASTRAL_NUM;

            entity.FECHA_PRIMERA_VISITA = entity.FECHA_PRIMERA_VISITA;

            entity.FECHA_LEVANTAMIENTO = entity.FECHA_LEVANTAMIENTO;

            entity.CONTROL_ARCHIVO = entity.CONTROL_ARCHIVO;

            entity.INSCRIPCION_CATASTRAL_NUM = entity.INSCRIPCION_CATASTRAL_NUM;

            entity.CODIGO_ZONIFICACION = entity.CODIGO_ZONIFICACION;

            entity.AREA_PARCELA = entity.AREA_PARCELA;

            entity.FRENTE_PARCELA = entity.FRENTE_PARCELA;

            entity.AREA_INMUEBLE = entity.AREA_INMUEBLE;

            entity.AREA_TIPO_PARCELA = entity.AREA_TIPO_PARCELA;

            entity.FRENTE_TIPO_PARCELA = entity.FRENTE_TIPO_PARCELA;

            entity.EXTRA1 = entity.EXTRA1;

            entity.EXTRA2 = entity.EXTRA2;

            entity.EXTRA3 = entity.EXTRA3;

            entity.USUARIO_INS = entity.USUARIO_INS;

            entity.FECHA_INS = entity.FECHA_INS;

            entity.CODIGO_EMPRESA = entity.CODIGO_EMPRESA;

            entity.EXTRA4 = entity.EXTRA4;

            entity.EXTRA5 = entity.EXTRA5;

            entity.EXTRA6 = entity.EXTRA6;

            entity.EXTRA7 = entity.EXTRA7;

            entity.EXTRA8 = entity.EXTRA8;

            entity.EXTRA9 = entity.EXTRA9;

            entity.EXTRA10 = entity.EXTRA10;

            entity.EXTRA11 = entity.EXTRA11;


            entity.EXTRA13 = entity.EXTRA13;

            entity.EXTRA14 = entity.EXTRA14;

            entity.EXTRA15 = entity.EXTRA15;

            entity.NUMERO_CIVICO = entity.NUMERO_CIVICO;

            entity.FECHA_INS_HIST = entity.FECHA_INS_HIST;

            entity.CODIGO_UBICACION_NAC = entity.CODIGO_UBICACION_NAC;

            entity.CODIGO_FICHA_FK = entity.CODIGO_FICHA_FK;

            entity.CODIGO_CATASTRO = entity.CODIGO_CATASTRO;

            return entity;




        }

    }
}

