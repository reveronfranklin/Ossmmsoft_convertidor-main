using Convertidor.Services.Sis;

namespace Convertidor.Data.Repository.Rh
{
	public class RhDireccionesService: IRhDireccionesService
    {
		




   
        private readonly IRhDireccionesRepository _repository;
 
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ISisUbicacionService _sisUbicacionService;
        private readonly IRhPersonasRepository _personasRepository;
  

        public RhDireccionesService(IRhDireccionesRepository repository,

                                    ISisUsuarioRepository sisUsuarioRepository,
                                    ISisUbicacionService sisUbicacionService,
                                    IRhPersonasRepository personasRepository
                                   )
        {
            _repository = repository;

            _sisUsuarioRepository = sisUsuarioRepository;
            _sisUbicacionService = sisUbicacionService;
            _personasRepository = personasRepository;
            
        }
       
        public async Task<List<RhDireccionesResponseDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var Direccion = await _repository.GetByCodigoPersona(codigoPersona);
                
                var result = await MapListDireccionesDto(Direccion);


                return (List<RhDireccionesResponseDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

       


        public async Task<RhDireccionesResponseDto> MapDireccionesDto(RH_DIRECCIONES dtos)
        {


            RhDireccionesResponseDto itemResult = new RhDireccionesResponseDto();
            itemResult.CodigoDireccion = dtos.CODIGO_DIRECCION;
            itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
            itemResult.DireccionId = dtos.DIRECCION_ID;
            itemResult.PaisId = dtos.PAIS_ID;
            itemResult.EstadoId = dtos.ESTADO_ID;
            itemResult.MunicipioId = dtos.MUNICIPIO_ID;
            itemResult.CiudadId = dtos.CIUDAD_ID;
            itemResult.ParroquiaId = dtos.PARROQUIA_ID;
            itemResult.SectorId = dtos.SECTOR_ID;
            itemResult.UrbanizacionId = dtos.URBANIZACION_ID;
            itemResult.ManzanaId = dtos.MANZANA_ID;
            itemResult.ParcelaId = dtos.PARCELA_ID;
            itemResult.VialidadId = dtos.VIALIDAD_ID; 
            itemResult.Vialidad = dtos.VIALIDAD;
            itemResult.TipoViviendaId = dtos.TIPO_VIVIENDA_ID;
            itemResult.ViviendaId = dtos.VIVIENDA_ID;
            itemResult.Vivienda = dtos.VIVIENDA;
            itemResult.TipoNivelId = dtos.TIPO_NIVEL_ID;
            itemResult.Nivel = dtos.NIVEL;
            itemResult.NroVivienda = dtos.NRO_VIVIENDA;
            itemResult.ComplementoDir = dtos.COMPLEMENTO_DIR;
            itemResult.TenenciaId = dtos.TENENCIA_ID;
            itemResult.CodigoPostal = dtos.CODIGO_POSTAL;
            itemResult.Principal = dtos.PRINCIPAL;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            

            return itemResult;

        }



        public async  Task<List<RhDireccionesResponseDto>> MapListDireccionesDto(List<RH_DIRECCIONES> dtos)
        {
            List<RhDireccionesResponseDto> result = new List<RhDireccionesResponseDto>();

            foreach (var item in dtos)
            {

                RhDireccionesResponseDto itemResult = new RhDireccionesResponseDto();

                itemResult.CodigoDireccion = item.CODIGO_DIRECCION;
                itemResult.CodigoPersona = item.CODIGO_PERSONA;
                itemResult.DireccionId = item.DIRECCION_ID; 
                itemResult.PaisId = item.PAIS_ID;
                itemResult.EstadoId = item.ESTADO_ID;
                itemResult.MunicipioId = item.MUNICIPIO_ID;
                itemResult.CiudadId = item.CIUDAD_ID;
                itemResult.ParroquiaId = item.PARROQUIA_ID;
                itemResult.SectorId = item.SECTOR_ID;
                itemResult.UrbanizacionId = item.URBANIZACION_ID;
                itemResult.ManzanaId = item.MANZANA_ID;
                itemResult.ParcelaId = item.PARCELA_ID;
                itemResult.VialidadId = item.VIALIDAD_ID;
                itemResult.Vialidad = item.VIALIDAD;
                itemResult.TipoViviendaId = item.TIPO_VIVIENDA_ID;
                itemResult.ViviendaId = item.VIVIENDA_ID;
                itemResult.Vivienda = item.VIVIENDA;
                itemResult.TipoNivelId = item.TIPO_NIVEL_ID;
                itemResult.Nivel = item.NIVEL;
                itemResult.NroVivienda = item.NRO_VIVIENDA;
                itemResult.ComplementoDir = item.COMPLEMENTO_DIR;
                itemResult.TenenciaId = item.TENENCIA_ID;
                itemResult.CodigoPostal = item.CODIGO_POSTAL;
                itemResult.Principal = item.PRINCIPAL;
                itemResult.Extra1 = item.EXTRA1;
                itemResult.Extra2 = item.EXTRA2;
                itemResult.Extra3 = item.EXTRA3;
               
                result.Add(itemResult);


            }
            return result;



        }

        public async Task<ResultDto<RhDireccionesResponseDto>> Update(RhDireccionesUpdate dto)
        {

            ResultDto<RhDireccionesResponseDto> result = new ResultDto<RhDireccionesResponseDto>(null);
            try
            {

                var persona = await _personasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (persona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona no existe";
                    return result;
                }
                var direccion = await _repository.GetByCodigo(dto.CodigoDireccion);
                if (direccion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Direccion no existe";
                    return result;
                }
               


                var pais = await _sisUbicacionService.GetPais(dto.PaisId);
                if (pais == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais invalido";
                    return result;
                }

                var estado = await _sisUbicacionService.GetEstado(dto.PaisId, dto.EstadoId);
                if (estado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado invalido";
                    return result;
                }

                var municipio = await _sisUbicacionService.GetMunicipio(dto.PaisId, dto.EstadoId, dto.MunicipioId);
                if (municipio is null)
                {
                    municipio.Id = dto.MunicipioId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Municipio Invalido";
                    return result;
                }

                var ciudad = await _sisUbicacionService.GetCiudad(dto.PaisId, dto.EstadoId, dto.MunicipioId, dto.CiudadId);
                if (ciudad is null)
                {
                    ciudad.Id = dto.CiudadId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ciudad Invalida";
                    return result;
                }

                var parroquia = await _sisUbicacionService.GetParroquia(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId);
                if (parroquia is null)
                {
                    parroquia.Id = dto.ParroquiaId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Parroquia Invalida";
                    return result;
                }

                var sector = await _sisUbicacionService.GetSector(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId, dto.SectorId);
                if (sector is null)
                {
                    sector.Id = dto.SectorId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sector Invalido";
                    return result;
                }

                var urbanizacion = await _sisUbicacionService.GetUrbanizacion(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId, dto.SectorId, dto.UrbanizacionId);
                if (urbanizacion is null)
                {
                    urbanizacion.Id = dto.UrbanizacionId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Urbanizacion Invalida";
                    return result;
                }

               

                direccion.CODIGO_DIRECCION = dto.CodigoDireccion;
                direccion.CODIGO_PERSONA = dto.CodigoPersona;
                direccion.DIRECCION_ID = dto.DireccionId;
                direccion.PAIS_ID = dto.PaisId;
                direccion.ESTADO_ID = dto.EstadoId;
                direccion.MUNICIPIO_ID = dto.MunicipioId;
                direccion.CIUDAD_ID = dto.CiudadId;
                direccion.PARROQUIA_ID = dto.ParroquiaId;
                direccion.SECTOR_ID = dto.SectorId;
                direccion.URBANIZACION_ID = dto.UrbanizacionId;
                direccion.MANZANA_ID = dto.ManzanaId;
                direccion.PARCELA_ID = dto.ParcelaId;
                direccion.VIALIDAD_ID = dto.VialidadId;
                direccion.VIALIDAD = dto.Vialidad;
                direccion.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                direccion.VIVIENDA_ID = dto.ViviendaId;
                direccion.TIPO_NIVEL_ID = dto.TipoNivelId;
                direccion.NIVEL = dto.Nivel;
                direccion.NRO_VIVIENDA = dto.NroVivienda;
                direccion.COMPLEMENTO_DIR = dto.ComplementoDir;
                direccion.TENENCIA_ID = dto.TenenciaId;
                direccion.CODIGO_POSTAL = dto.CodigoPostal;
                direccion.PRINCIPAL = dto.Principal;


                var conectado = await _sisUsuarioRepository.GetConectado();
                direccion.CODIGO_EMPRESA = conectado.Empresa;
                direccion.USUARIO_UPD = conectado.Usuario;
                direccion.FECHA_UPD = DateTime.Now; 


                await _repository.Update(direccion);



                var resultDto = await MapDireccionesDto(direccion);
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

        public async Task<ResultDto<RhDireccionesResponseDto>> Create(RhDireccionesUpdate dto)
        {

            ResultDto<RhDireccionesResponseDto> result = new ResultDto<RhDireccionesResponseDto>(null);
            try
            {
                
                var pais = await _sisUbicacionService.GetPais(dto.PaisId);
               
                if (pais is null)
                {
                    pais.Id = dto.PaisId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais  Invalido";
                    return result;
                }
               
                var estado = await _sisUbicacionService.GetEstado(dto.PaisId,dto.EstadoId);
                if (estado is null)
                {
                    estado.Id = dto.EstadoId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Invalido";
                    return result;
                }

                var municipio = await _sisUbicacionService.GetMunicipio(dto.PaisId, dto.EstadoId,dto.MunicipioId);
                if (municipio is null)
                {
                    municipio.Id = dto.MunicipioId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Municipio Invalido";
                    return result;
                }

                var ciudad = await _sisUbicacionService.GetCiudad(dto.PaisId, dto.EstadoId,dto.MunicipioId, dto.CiudadId);
                if (ciudad is null)
                {
                    ciudad.Id = dto.CiudadId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ciudad Invalida";
                    return result;
                }

                var parroquia = await _sisUbicacionService.GetParroquia(dto.PaisId, dto.EstadoId,dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId);
                if (parroquia is null)
                {
                    parroquia.Id = dto.ParroquiaId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Parroquia Invalida";
                    return result;
                }

                var sector = await _sisUbicacionService.GetSector(dto.PaisId, dto.EstadoId,dto.MunicipioId,
                    dto.CiudadId,dto.ParroquiaId, dto.SectorId);
                if (sector is null)
                {
                    sector.Id = dto.SectorId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sector Invalido";
                    return result;
                }

                var urbanizacion = await _sisUbicacionService.GetUrbanizacion(dto.PaisId, dto.EstadoId,dto.MunicipioId,
                    dto.CiudadId,dto.ParroquiaId,dto.SectorId, dto.UrbanizacionId);
                if (urbanizacion is null)
                {
                    urbanizacion.Id = dto.UrbanizacionId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Urbanizacion Invalida";
                    return result;
                }

                RH_DIRECCIONES entity = new RH_DIRECCIONES();

                entity.CODIGO_DIRECCION = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.DIRECCION_ID = dto.DireccionId;
                entity.PAIS_ID = dto.PaisId;
                entity.ESTADO_ID = dto.EstadoId;
                entity.MUNICIPIO_ID = dto.MunicipioId;
                entity.CIUDAD_ID = dto.CiudadId;
                entity.PARROQUIA_ID = dto.ParroquiaId;
                entity.SECTOR_ID = dto.SectorId;
                entity.URBANIZACION_ID = dto.UrbanizacionId;
                entity.MANZANA_ID = dto.ManzanaId;
                entity.PARCELA_ID = dto.ParcelaId;
                entity.VIALIDAD_ID = dto.VialidadId;
                entity.VIALIDAD = dto.Vialidad;
                entity.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                entity.VIVIENDA_ID = dto.ViviendaId;
                entity.TIPO_NIVEL_ID = dto.TipoNivelId;
                entity.NIVEL = dto.Nivel;
                entity.NRO_VIVIENDA = dto.NroVivienda;
                entity.COMPLEMENTO_DIR = dto.ComplementoDir;
                entity.TENENCIA_ID = dto.TenenciaId;
                entity.CODIGO_POSTAL = dto.CodigoPostal;
                entity.PRINCIPAL = dto.Principal;

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;    


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDireccionesDto(created.Data);
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

        public async Task<ResultDto<RhDireccionesDeleteDto>> Delete(RhDireccionesDeleteDto dto)
        {

            ResultDto<RhDireccionesDeleteDto> result = new ResultDto<RhDireccionesDeleteDto>(null);
            try
            {

                var Direccion = await _repository.GetByCodigo(dto.CodigoDireccion);
                if (Direccion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Direccion no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDireccion);

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

