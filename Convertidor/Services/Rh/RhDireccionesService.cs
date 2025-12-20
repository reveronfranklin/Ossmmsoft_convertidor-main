using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Sis;

namespace Convertidor.Data.Repository.Rh
{
	public class RhDireccionesService: IRhDireccionesService
    {
		




   
        private readonly IRhDireccionesRepository _repository;
 
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ISisUbicacionService _sisUbicacionService;
        private readonly IRhPersonasRepository _personasRepository;
        private readonly IRhDescriptivaRepository _descriptivaRepository;

        public RhDireccionesService(IRhDireccionesRepository repository,

                                    ISisUsuarioRepository sisUsuarioRepository,
                                    ISisUbicacionService sisUbicacionService,
                                    IRhPersonasRepository personasRepository,
                                    IRhDescriptivaRepository descriptivaRepository
                                   )
        {
            _repository = repository;

            _sisUsuarioRepository = sisUsuarioRepository;
            _sisUbicacionService = sisUbicacionService;
            _personasRepository = personasRepository;
            _descriptivaRepository = descriptivaRepository;
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
            itemResult.Direccion=string.Empty;
            var direccion = await _descriptivaRepository.GetByCodigoDescriptiva(dtos.DIRECCION_ID);
            if(direccion != null)
            {
                itemResult.Direccion = direccion.DESCRIPCION;
            }

            itemResult.PaisId = dtos.PAIS_ID;
            itemResult.Pais="";
            var pais = await _sisUbicacionService.GetPais(dtos.PAIS_ID);
            if (pais != null)
            {
                itemResult.Pais = pais.Descripcion;
            }
            itemResult.Estado="";
            itemResult.EstadoId = dtos.ESTADO_ID;
            var estado= await _sisUbicacionService.GetEstado(dtos.PAIS_ID, dtos.ESTADO_ID);
            if (estado != null)
            {
                itemResult.Estado = estado.Descripcion;
            }

            itemResult.Municipio = "";
            itemResult.MunicipioId = dtos.MUNICIPIO_ID;
            var municipio = await _sisUbicacionService.GetMunicipio(dtos.PAIS_ID, dtos.ESTADO_ID, dtos.MUNICIPIO_ID);
            if (municipio != null)
            {
                itemResult.Municipio = municipio.Descripcion;
            }
            itemResult.Ciudad = "";
            itemResult.CiudadId = dtos.CIUDAD_ID;
            var ciudad = await _sisUbicacionService.GetCiudad(dtos.PAIS_ID, dtos.ESTADO_ID, dtos.MUNICIPIO_ID, dtos.CIUDAD_ID);
            if (ciudad != null)
            {
                itemResult.Ciudad = ciudad.Descripcion;
            }
            itemResult.Parroquia = "";
            itemResult.ParroquiaId = dtos.PARROQUIA_ID;
            var parroquia = await _sisUbicacionService.GetParroquia(dtos.PAIS_ID, dtos.ESTADO_ID, dtos.MUNICIPIO_ID, dtos.CIUDAD_ID, dtos.PARROQUIA_ID);
            if(parroquia != null)
            {
                itemResult.Parroquia = parroquia.Descripcion;
            }
            itemResult.Sector="";
            itemResult.SectorId = dtos.SECTOR_ID;
            var sector = await _sisUbicacionService.GetSector(dtos.PAIS_ID, dtos.ESTADO_ID, dtos.MUNICIPIO_ID, dtos.CIUDAD_ID, dtos.PARROQUIA_ID, dtos.SECTOR_ID);
            if (sector != null)
            {
                itemResult.Sector = sector.Descripcion;
            }
            itemResult.Urbanizacion = "";
            var urbanizacion = await _sisUbicacionService.GetUrbanizacion(dtos.PAIS_ID, dtos.ESTADO_ID, dtos.MUNICIPIO_ID, dtos.CIUDAD_ID, dtos.PARROQUIA_ID, dtos.SECTOR_ID, dtos.URBANIZACION_ID);
            if (urbanizacion != null)
            {
                itemResult.Urbanizacion = urbanizacion.Descripcion;
            }
            itemResult.UrbanizacionId = dtos.URBANIZACION_ID;
         
            itemResult.TipoViviendaId = dtos.TIPO_VIVIENDA_ID;
            itemResult.TipoVivienda="";
            var tipoVivienda = await _descriptivaRepository.GetByCodigoDescriptiva(dtos.TIPO_VIVIENDA_ID);
            if(tipoVivienda != null)
            {
                itemResult.TipoVivienda = tipoVivienda.DESCRIPCION;
            }
            dtos.VIVIENDA ??= string.Empty;
            itemResult.Vivienda = dtos.VIVIENDA;
            
            itemResult.TipoNivelId = dtos.TIPO_NIVEL_ID;
            itemResult.TipoNivel = "";
            var tipoNivel   = await _descriptivaRepository.GetByCodigoDescriptiva(dtos.TIPO_NIVEL_ID);
            if (tipoNivel != null)
            {
                   itemResult.TipoNivel = tipoNivel.DESCRIPCION;
            }
            dtos.NIVEL ??= string.Empty;
            itemResult.Nivel = dtos.NIVEL;
            itemResult.NroVivienda = dtos.NRO_VIVIENDA;
            itemResult.ComplementoDir = dtos.COMPLEMENTO_DIR;
            itemResult.TenenciaId = dtos.TENENCIA_ID;
            itemResult.Tenencia = "";
            var tenencia = await _descriptivaRepository.GetByCodigoDescriptiva(dtos.TENENCIA_ID);
            if(tenencia != null)
            {
                itemResult.Tenencia = tenencia.DESCRIPCION;
            }
            itemResult.CodigoPostal = dtos.CODIGO_POSTAL;
            itemResult.Principal = false;
            if(dtos.PRINCIPAL == 1)
            {
                itemResult.Principal = true;
            }
      
           
            

            return itemResult;

        }



        public async  Task<List<RhDireccionesResponseDto>> MapListDireccionesDto(List<RH_DIRECCIONES> dtos)
        {
            List<RhDireccionesResponseDto> result = new List<RhDireccionesResponseDto>();

            foreach (var item in dtos)
            {

                RhDireccionesResponseDto itemResult = new RhDireccionesResponseDto();

                itemResult = await MapDireccionesDto(item);
               
               
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
                if(!string.IsNullOrEmpty(dto.ComplementoDir) && dto.ComplementoDir.Trim().Length > 200){
                    
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Complemento de Dir es muy largo, Maximo 200 caracteres";
                    return result;
                }
                if(dto.SectorId > 0){
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
                }

                if (dto.UrbanizacionId > 0)
                {
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
                //direccion.MANZANA_ID = dto.ManzanaId;
                //direccion.PARCELA_ID = dto.ParcelaId;
                //direccion.VIALIDAD_ID = dto.VialidadId;
                //direccion.VIALIDAD = dto.Vialidad;
                direccion.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                direccion.VIVIENDA_ID = dto.ViviendaId;
                direccion.TIPO_NIVEL_ID = dto.TipoNivelId;
                direccion.NIVEL = dto.Nivel;
                direccion.NRO_VIVIENDA = dto.NroVivienda;
                direccion.COMPLEMENTO_DIR = dto.ComplementoDir;
                direccion.TENENCIA_ID = dto.TenenciaId;
                direccion.CODIGO_POSTAL = dto.CodigoPostal;
               if (dto.Principal == true)
                {
                    direccion.PRINCIPAL=1;
                }
                else
                {
                    direccion.PRINCIPAL=0;
                }
            


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
                if(!string.IsNullOrEmpty(dto.ComplementoDir) && dto.ComplementoDir.Trim().Length > 200){
                    
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Complemento de Dir es muy largo, Maximo 200 caracteres";
                    return result;
                }
                if(dto.SectorId > 0){
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
                }

                if (dto.UrbanizacionId > 0)
                {
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
                //entity.MANZANA_ID = dto.ManzanaId;
                //entity.PARCELA_ID = dto.ParcelaId;
                //entity.VIALIDAD_ID = dto.VialidadId;
                //entity.VIALIDAD = dto.Vialidad;
                entity.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                entity.VIVIENDA_ID = dto.ViviendaId;
                entity.TIPO_NIVEL_ID = dto.TipoNivelId;
                entity.NIVEL = dto.Nivel;
                entity.NRO_VIVIENDA = dto.NroVivienda;
                entity.COMPLEMENTO_DIR = dto.ComplementoDir;
                entity.TENENCIA_ID = dto.TenenciaId;
                entity.CODIGO_POSTAL = dto.CodigoPostal;
                if (dto.Principal == true)
                {
                    entity.PRINCIPAL=1;
                }
                else
                {
                    entity.PRINCIPAL=0;
                }
            

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

