using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Data.Repository.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatDValorTierraService : ICatDValorTierraService
    {
        private readonly ICatDValorTierraRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICatDescriptivasRepository _catDescriptivasRepository;
        private readonly ICAT_UBICACION_NACService _cAT_UBICACION_NACService;

        public CatDValorTierraService(ICatDValorTierraRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      ICatDescriptivasRepository catDescriptivasRepository,
                                      ICAT_UBICACION_NACService cAT_UBICACION_NACService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _catDescriptivasRepository = catDescriptivasRepository;
            _cAT_UBICACION_NACService = cAT_UBICACION_NACService;
        }

        public async Task<CatDValorTierraResponseDto> MapDValorTierra(CAT_D_VALOR_TIERRA entity)
        {
            CatDValorTierraResponseDto dto = new CatDValorTierraResponseDto();

            dto.CodigoValorTierra = entity.CODIGO_VALOR_TIERRA;
            dto.CodigoDValorTierraUrbFk = entity.CODIGO_D_VALOR_TIERRA_URB_FK;
            dto.PaisId = entity.PAIS_ID;
            dto.EstadoId = entity.ESTADO_ID;
            dto.MunicipioId = entity.MUNICIPIO_ID;
            dto.ParroquiaId = entity.PARROQUIA_ID;
            dto.SectorId = entity.SECTOR_ID;
            dto.FechaIniVigValor = entity.FECHA_INI_VIG_VALOR;
            dto.FechaIniVigValorString = entity.FECHA_INI_VIG_VALOR.ToString("u");
            FechaDto FechaIniVigValorObj = FechaObj.GetFechaDto(entity.FECHA_INI_VIG_VALOR);
            dto.FechaIniVigValorObj = (FechaDto)FechaIniVigValorObj;
            dto.FechaFinVigValor = entity.FECHA_FIN_VIG_VALOR;
            dto.FechaFinVigValorString = entity.FECHA_FIN_VIG_VALOR.ToString("u");
            FechaDto FechaFinVigValorObj = FechaObj.GetFechaDto(entity.FECHA_FIN_VIG_VALOR);
            dto.FechaFinVigValorObj = (FechaDto)FechaFinVigValorObj;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.CodigoZonificacionId = entity.CODIGO_ZONIFICACION_ID;
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

        public async Task<List<CatDValorTierraResponseDto>> MapListDValorTierra(List<CAT_D_VALOR_TIERRA> dtos)
        {
            List<CatDValorTierraResponseDto> result = new List<CatDValorTierraResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDValorTierra(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDValorTierraResponseDto>>> GetAll()
        {

            ResultDto<List<CatDValorTierraResponseDto>> result = new ResultDto<List<CatDValorTierraResponseDto>>(null);
            try
            {
                var dValorTierra = await _repository.GetAll();
                var cant = dValorTierra.Count();
                if (dValorTierra != null && dValorTierra.Count() > 0)
                {
                    var listDto = await MapListDValorTierra(dValorTierra);

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

        public async Task<ResultDto<CatDValorTierraResponseDto>> Create(CatDValorTierraUpdateDto dto)
        {

            ResultDto<CatDValorTierraResponseDto> result = new ResultDto<CatDValorTierraResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoDValorTierraUrbFk <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo D Valor Tierra Fk Invalido";
                    return result;

                }

                var pais = await _cAT_UBICACION_NACService.GetPais(dto.PaisId);

                if (pais is null)
                {
                    pais.Id = dto.PaisId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais  Invalido";
                    return result;
                }

                var estado = await _cAT_UBICACION_NACService.GetEstado(dto.PaisId, dto.EstadoId);
                if (estado is null)
                {
                    estado.Id = dto.EstadoId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Invalido";
                    return result;
                }

                var municipio = await _cAT_UBICACION_NACService.GetMunicipio(dto.PaisId, dto.EstadoId, dto.MunicipioId);
                if (municipio is null)
                {
                    municipio.Id = dto.MunicipioId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Municipio Invalido";
                    return result;
                }

                if(dto.ParroquiaId <= 0) 
                {
                    municipio.Id = dto.MunicipioId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Parroquia Id Invalido";
                    return result;


                }

                if (dto.SectorId <= 0)
                {
                    municipio.Id = dto.MunicipioId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sector Id Invalido";
                    return result;


                }



                if (dto.FechaIniVigValor == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Ini Vig Valor Invalido";
                    return result;
                }

                if (dto.FechaFinVigValor == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Fin Vig Valor Invalido";
                    return result;
                }

                if (dto.VialidadPrincipalId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad Principal Id Invalida";
                    return result;
                }

                var vialidadPrincipalId = await _catDescriptivasRepository.GetByIdAndTitulo(3, dto.VialidadPrincipalId);
                if(vialidadPrincipalId == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad Principal Id Invalida";
                    return result;

                }

                if (dto.VialidadDesdeId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad Desde Id Invalida";
                    return result;
                }

                var vialidadDesdeId = await _catDescriptivasRepository.GetByIdAndTitulo(3, dto.VialidadDesdeId);
                if (vialidadDesdeId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad Desde Id Invalida";
                    return result;


                }

                if (dto.VialidadHastaId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad Hasta Id Invalida";
                    return result;
                }


                var vialidadHastaId = await _catDescriptivasRepository.GetByIdAndTitulo(3, dto.VialidadHastaId);
                if (vialidadHastaId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad Hasta Id Invalida";
                    return result;


                }

                if (dto.ValorTierra <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Tierra Invalido";
                    return result;

                }

                if(dto.Observaciones.Length > 100)
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

                var codigoZonificacionId = await _catDescriptivasRepository.GetByIdAndTitulo(40, dto.CodigoZonificacionId);
                if (codigoZonificacionId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Zonificacion Id Invalido";
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

               




                CAT_D_VALOR_TIERRA entity = new CAT_D_VALOR_TIERRA();
                entity.CODIGO_VALOR_TIERRA = await _repository.GetNextKey();
                entity.CODIGO_D_VALOR_TIERRA_URB_FK = dto.CodigoDValorTierraUrbFk;
                entity.PAIS_ID = dto.PaisId;
                entity.ESTADO_ID = dto.EstadoId;
                entity.MUNICIPIO_ID = dto.MunicipioId;
                entity.PARROQUIA_ID = dto.ParroquiaId;
                entity.SECTOR_ID = dto.SectorId;
                entity.FECHA_INI_VIG_VALOR = dto.FechaIniVigValor;
                entity.FECHA_FIN_VIG_VALOR = dto.FechaFinVigValor;
                entity.VIALIDAD_PRINCIPAL_ID = dto.VialidadPrincipalId;
                entity.VIALIDAD_DESDE_ID = dto.VialidadDesdeId;
                entity.VIALIDAD_HASTA_ID = dto.VialidadHastaId;
                entity.VALOR_TIERRA = dto.ValorTierra;
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
                    var resultDto = await MapDValorTierra(created.Data);
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
