using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Data.Repository.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatDValorConstruccionService : ICatDValorConstruccionService
    {
        private readonly ICatDValorConstruccionRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICatDescriptivasRepository _catDescriptivasRepository;

        public CatDValorConstruccionService(ICatDValorConstruccionRepository repository,
                                            ISisUsuarioRepository sisUsuarioRepository,
                                            ICatDescriptivasRepository catDescriptivasRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _catDescriptivasRepository = catDescriptivasRepository;
        }

        public async Task<CatDValorConstruccionResponseDto> MapDValorConstruccion(CAT_D_VALOR_CONSTRUCCION entity)
        {
            CatDValorConstruccionResponseDto dto = new CatDValorConstruccionResponseDto();

            dto.CodigoParcela = entity.CODIGO_PARCELA;
            dto.CodigoDValorConstruccion = entity.CODIGO_D_VALOR_CONSTRUCCION;
            dto.CodigoValorConstruccion = entity.CODIGO_VALOR_CONSTRUCCION;
            dto.CodigoInmueble = entity.CODIGO_INMUEBLE;
            dto.CodigoCatastro = entity.CODIGO_CATASTRO;
            dto.EstructuraNivel1Id = entity.ESTRUCTURA_NIVEL1_ID;
            dto.EstructuraNivel2Id = entity.ESTRUCTURA_NIVEL2_ID;
            dto.EstructuraNivel3Id = entity.ESTRUCTURA_NIVEL3_ID;
            dto.EstructuraNivel4Id = entity.ESTRUCTURA_NIVEL4_ID;
            dto.EstructuraDescriptiva = entity.ESTRUCTURA_DESCRIPTIVA;
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
            dto.ValorComplementario = entity.VALOR_COMPLEMENTARIO;



            return dto;

        }

        public async Task<List<CatDValorConstruccionResponseDto>> MapListDValorConstruccion(List<CAT_D_VALOR_CONSTRUCCION> dtos)
        {
            List<CatDValorConstruccionResponseDto> result = new List<CatDValorConstruccionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDValorConstruccion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDValorConstruccionResponseDto>>> GetAll()
        {

            ResultDto<List<CatDValorConstruccionResponseDto>> result = new ResultDto<List<CatDValorConstruccionResponseDto>>(null);
            try
            {
                var dValorConstruccion = await _repository.GetAll();
                var cant = dValorConstruccion.Count();
                if (dValorConstruccion != null && dValorConstruccion.Count() > 0)
                {
                    var listDto = await MapListDValorConstruccion(dValorConstruccion);

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


        public async Task<ResultDto<CatDValorConstruccionResponseDto>> Create(CatDValorConstruccionUpdateDto dto)
        {

            ResultDto<CatDValorConstruccionResponseDto> result = new ResultDto<CatDValorConstruccionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoDValorConstruccion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo D Valor Construccion Invalido";
                    return result;

                }

                if (dto.CodigoValorConstruccion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Valor Construccion Invalido ";
                    return result;
                }

                if (dto.CodigoInmueble <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Inmueble Invalido";
                    return result;

                }

                if (dto.CodigoCatastro.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Catastro Invalido";
                    return result;

                }

                if (dto.EstructuraNivel1Id <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 1 Id Invalido";
                    return result;

                }

                var estructuraNivelID1 = await _catDescriptivasRepository.GetByIdAndTitulo(49, dto.EstructuraNivel1Id);
                if (estructuraNivelID1 == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 1 Id Invalida";
                    return result;


                }

                if (dto.EstructuraNivel2Id <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 2 Id Invalido";
                    return result;

                }

                var estructuraNivelID2 = await _catDescriptivasRepository.GetByIdAndTitulo(49, dto.EstructuraNivel2Id);
                if (estructuraNivelID2 == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 2 Id Invalida";
                    return result;


                }

                if (dto.EstructuraNivel3Id <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 3 Id Invalido";
                    return result;
                }

                var estructuraNivelID3 = await _catDescriptivasRepository.GetByIdAndTitulo(49, dto.EstructuraNivel3Id);
                if (estructuraNivelID3 == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 3 Id Invalida";
                    return result;


                }

                if (dto.EstructuraNivel4Id <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 4 Id Invalida";
                    return result;
                }

                var estructuraNivelID4 = await _catDescriptivasRepository.GetByIdAndTitulo(49, dto.EstructuraNivel4Id);
                if (estructuraNivelID4 == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 4 Id Invalida";
                    return result;


                }

                if (dto.EstructuraDescriptiva.Length > 10)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Descriptiva Invalida";
                    return result;

                }

                var estructuraDescriptiva = await _catDescriptivasRepository.GetByCodigo(Convert.ToInt32(dto.EstructuraDescriptiva));
                if(estructuraDescriptiva == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Descriptiva Invalida";
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

                if (dto.ValorComplementario <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Complementario Invalido";
                    return result;

                }




                CAT_D_VALOR_CONSTRUCCION entity = new CAT_D_VALOR_CONSTRUCCION();
                entity.CODIGO_PARCELA = await _repository.GetNextKey();
                entity.CODIGO_D_VALOR_CONSTRUCCION = dto.CodigoDValorConstruccion;
                entity.CODIGO_VALOR_CONSTRUCCION = dto.CodigoValorConstruccion;
                entity.CODIGO_INMUEBLE = dto.CodigoInmueble;
                entity.CODIGO_CATASTRO = dto.CodigoCatastro;
                entity.ESTRUCTURA_NIVEL1_ID = dto.EstructuraNivel1Id;
                entity.ESTRUCTURA_NIVEL2_ID = dto.EstructuraNivel2Id;
                entity.ESTRUCTURA_NIVEL3_ID = dto.EstructuraNivel3Id;
                entity.ESTRUCTURA_NIVEL4_ID = dto.EstructuraNivel4Id;
                entity.ESTRUCTURA_DESCRIPTIVA = estructuraDescriptiva.DESCRIPCION;
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
                entity.VALOR_COMPLEMENTARIO = dto.ValorComplementario;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDValorConstruccion(created.Data);
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

        public async Task<ResultDto<CatDValorConstruccionResponseDto>> Update(CatDValorConstruccionUpdateDto dto)
        {

            ResultDto<CatDValorConstruccionResponseDto> result = new ResultDto<CatDValorConstruccionResponseDto>(null);
            try
            {


                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoParcela = await _repository.GetByCodigo(dto.CodigoParcela);



                if (codigoParcela == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Parcela Invalido";
                    return result;

                }

                if (dto.CodigoDValorConstruccion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo D Valor Construccion Invalido";
                    return result;

                }

                if (dto.CodigoValorConstruccion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Valor Construccion Invalido ";
                    return result;
                }

                if (dto.CodigoInmueble <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Inmueble Invalido";
                    return result;

                }

                if (dto.CodigoCatastro.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Catastro Invalido";
                    return result;

                }

                if (dto.EstructuraNivel1Id <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 1 Id Invalido";
                    return result;

                }

                var estructuraNivelID1 = await _catDescriptivasRepository.GetByIdAndTitulo(49, dto.EstructuraNivel1Id);
                if (estructuraNivelID1 == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 1 Id Invalida";
                    return result;


                }

                if (dto.EstructuraNivel2Id <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 2 Id Invalido";
                    return result;

                }

                var estructuraNivelID2 = await _catDescriptivasRepository.GetByIdAndTitulo(49, dto.EstructuraNivel2Id);
                if (estructuraNivelID2 == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 2 Id Invalida";
                    return result;


                }

                if (dto.EstructuraNivel3Id <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 3 Id Invalido";
                    return result;
                }

                var estructuraNivelID3 = await _catDescriptivasRepository.GetByIdAndTitulo(49, dto.EstructuraNivel3Id);
                if (estructuraNivelID3 == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 3 Id Invalida";
                    return result;


                }

                if (dto.EstructuraNivel4Id <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 4 Id Invalida";
                    return result;
                }

                var estructuraNivelID4 = await _catDescriptivasRepository.GetByIdAndTitulo(49, dto.EstructuraNivel4Id);
                if (estructuraNivelID4 == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Nivel 4 Id Invalida";
                    return result;


                }

                if (dto.EstructuraDescriptiva.Length > 10)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Descriptiva Invalida";
                    return result;

                }

                var estructuraDescriptiva = await _catDescriptivasRepository.GetByCodigo(Convert.ToInt32(dto.EstructuraDescriptiva));
                if (estructuraDescriptiva == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estructura Descriptiva Invalida";
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

                if (dto.ValorComplementario <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Complementario Invalido";
                    return result;

                }





                codigoParcela.CODIGO_PARCELA = dto.CodigoParcela;
                codigoParcela.CODIGO_D_VALOR_CONSTRUCCION = dto.CodigoDValorConstruccion;
                codigoParcela.CODIGO_VALOR_CONSTRUCCION = dto.CodigoValorConstruccion;
                codigoParcela.CODIGO_INMUEBLE = dto.CodigoInmueble;
                codigoParcela.CODIGO_CATASTRO = dto.CodigoCatastro;
                codigoParcela.ESTRUCTURA_NIVEL1_ID = dto.EstructuraNivel1Id;
                codigoParcela.ESTRUCTURA_NIVEL2_ID = dto.EstructuraNivel2Id;
                codigoParcela.ESTRUCTURA_NIVEL3_ID = dto.EstructuraNivel3Id;
                codigoParcela.ESTRUCTURA_NIVEL4_ID = dto.EstructuraNivel4Id;
                codigoParcela.ESTRUCTURA_DESCRIPTIVA = estructuraDescriptiva.DESCRIPCION;
                codigoParcela.EXTRA1 = dto.Extra1;
                codigoParcela.EXTRA2 = dto.Extra2;
                codigoParcela.EXTRA3 = dto.Extra3;
                codigoParcela.EXTRA4 = dto.Extra4;
                codigoParcela.EXTRA5 = dto.Extra5;
                codigoParcela.EXTRA6 = dto.Extra6;
                codigoParcela.EXTRA7 = dto.Extra7;
                codigoParcela.EXTRA8 = dto.Extra8;
                codigoParcela.EXTRA9 = dto.Extra9;
                codigoParcela.EXTRA10 = dto.Extra10;
                codigoParcela.EXTRA11 = dto.Extra11;
                codigoParcela.EXTRA12 = dto.Extra12;
                codigoParcela.EXTRA13 = dto.Extra13;
                codigoParcela.EXTRA14 = dto.Extra14;
                codigoParcela.EXTRA15 = dto.Extra15;
                codigoParcela.VALOR_COMPLEMENTARIO = dto.ValorComplementario;


                codigoParcela.CODIGO_EMPRESA = conectado.Empresa;
                codigoParcela.USUARIO_INS = conectado.Usuario;
                codigoParcela.FECHA_INS = DateTime.Now;

                await _repository.Update(codigoParcela);
                var resultDto = await MapDValorConstruccion(codigoParcela);
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
