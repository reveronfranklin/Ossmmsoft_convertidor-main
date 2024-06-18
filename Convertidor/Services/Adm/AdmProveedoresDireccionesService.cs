using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Services.Sis;


namespace Convertidor.Services.Adm
{
	public class AdmProveedoresDireccionesService: IAdmProveedoresDireccionesService
    {

      
        private readonly IAdmDireccionProveedorRepository _repository;
        private readonly IAdmDescriptivaRepository _repositoryPreDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        private readonly IAdmProveedoresRepository _proveedorRepository;
        private readonly ISisUbicacionService _sisUbicacionService;
       

        public AdmProveedoresDireccionesService(IAdmDireccionProveedorRepository repository,
                                      IAdmDescriptivaRepository repositoryPreDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                     
                                      IAdmProveedoresRepository proveedorRepository,
                                      ISisUbicacionService sisUbicacionService)
		{
            _repository = repository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;

            _proveedorRepository = proveedorRepository;
            _sisUbicacionService = sisUbicacionService;
        }



       
       
        public  async Task<AdmProveedorDireccionResponseDto> MapProveedorDireccionDto(ADM_DIR_PROVEEDOR dtos)
        {
            AdmProveedorDireccionResponseDto itemResult = new AdmProveedorDireccionResponseDto();
          
            
            itemResult.CodigoDirProveedor = dtos.CODIGO_DIR_PROVEEDOR;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;

            itemResult.TipoDireccionId = dtos.TIPO_DIRECCION_ID;
            itemResult.PaisId = dtos.PAIS_ID;
            itemResult.EstadoId =dtos.ESTADO_ID;
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
            itemResult.Vivienda = dtos.VIVIENDA;
            itemResult.TipoNivelId= dtos.TIPO_NIVEL_ID;
            itemResult.Nivel = dtos.NIVEL;
            itemResult.TipoUnidadId = dtos.TIPO_UNIDAD_ID;
            itemResult.NumeroUnidad = dtos.NUMERO_UNIDAD;
            itemResult.ComplementoDir = dtos.COMPLEMENTO_DIR;
            itemResult.TenenciaId = dtos.TENENCIA_ID;
            itemResult.CodigoPostal = dtos.CODIGO_POSTAL;
            itemResult.Principal = dtos.PRINCIPAL;
           
          
            return itemResult;
        }

        public async Task< List<AdmProveedorDireccionResponseDto>> MapListProveedorDireccionDto(List<ADM_DIR_PROVEEDOR> dtos)
        {
            List<AdmProveedorDireccionResponseDto> result = new List<AdmProveedorDireccionResponseDto>();
           
            
            foreach (var item in dtos)
            {
                

                var itemResult =  await MapProveedorDireccionDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
        public async Task<ResultDto<AdmProveedorDireccionResponseDto?>> Update(AdmProveedorDireccionUpdateDto dto)
        {

            ResultDto<AdmProveedorDireccionResponseDto?> result = new ResultDto<AdmProveedorDireccionResponseDto?>(null);
            try
            {

                var proveedorContacto = await _repository.GetByCodigo(dto.CodigoDirProveedor);
                if (proveedorContacto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Direccion no existe";
                    return result;
                }

                var proveedor = await _proveedorRepository.GetByCodigo(dto.CodigoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor no existe";
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

               
                
                
                var tiposDireccion = await _repositoryPreDescriptiva.GetByIdAndTitulo(4,dto.TipoDireccionId);
                if (tiposDireccion==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Direccion  Invalido";
                    return result;
                }
                var tipoVivienda = await _repositoryPreDescriptiva.GetByIdAndTitulo(6,dto.TipoViviendaId);
                if (tipoVivienda==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Vivienda  Invalido";
                    return result;
                }

                if (String.IsNullOrEmpty(dto.Vialidad))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad Invalido";
                    return result;
                    
                }
                if (String.IsNullOrEmpty(dto.Vivienda))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vivienda Invalido";
                    return result;
                    
                }
                if (String.IsNullOrEmpty(dto.ComplementoDir))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Complemento Direccion Invalido";
                    return result;
                    
                }
                if (dto.CodigoPostal<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Postal Invalido";
                    return result;
                    
                }
                
                proveedorContacto.PAIS_ID = dto.PaisId;
                proveedorContacto.ESTADO_ID = dto.EstadoId;
                proveedorContacto.MUNICIPIO_ID = dto.MunicipioId;
                proveedorContacto.CIUDAD_ID = dto.CiudadId;
                proveedorContacto.PARROQUIA_ID = dto.ParroquiaId;
                proveedorContacto.SECTOR_ID = dto.SectorId;
                proveedorContacto.URBANIZACION_ID = dto.UrbanizacionId;
                proveedorContacto.TIPO_DIRECCION_ID = dto.TipoDireccionId;
                proveedorContacto.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                proveedorContacto.VIALIDAD_ID = dto.VialidadId;
                proveedorContacto.VIALIDAD = dto.Vialidad;
                proveedorContacto.VIVIENDA = dto.Vivienda;
                proveedorContacto.PRINCIPAL = dto.Principal;
                proveedorContacto.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                proveedorContacto.CODIGO_EMPRESA = conectado.Empresa;
                proveedorContacto.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(proveedorContacto);
                
                var resultDto = await  MapProveedorDireccionDto(proveedorContacto);
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
        public List<string> GetListSexo()
        {
            List<string> result = new List<string>();
            result.Add("M");
            result.Add("F");
            return result;
        }
        public async Task<ResultDto<AdmProveedorDireccionResponseDto>> Create(AdmProveedorDireccionUpdateDto dto)
        {

            ResultDto<AdmProveedorDireccionResponseDto> result = new ResultDto<AdmProveedorDireccionResponseDto>(null);
            try
            {
               
             


                var proveedor = await _proveedorRepository.GetByCodigo(dto.CodigoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor no existe";
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

               
                
                
                var tiposDireccion = await _repositoryPreDescriptiva.GetByIdAndTitulo(4,dto.TipoDireccionId);
                if (tiposDireccion==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Direccion  Invalido";
                    return result;
                }
                var tipoVivienda = await _repositoryPreDescriptiva.GetByIdAndTitulo(6,dto.TipoViviendaId);
                if (tipoVivienda==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Vivienda  Invalido";
                    return result;
                }

                if (String.IsNullOrEmpty(dto.Vialidad))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad Invalido";
                    return result;
                    
                }
                if (String.IsNullOrEmpty(dto.Vivienda))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vivienda Invalido";
                    return result;
                    
                }
                if (String.IsNullOrEmpty(dto.ComplementoDir))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Complemento Direccion Invalido";
                    return result;
                    
                }
                if (dto.CodigoPostal<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Postal Invalido";
                    return result;
                    
                }
                ADM_DIR_PROVEEDOR entity = new ADM_DIR_PROVEEDOR();
                entity.CODIGO_DIR_PROVEEDOR = await _repository.GetNextKey();
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.PAIS_ID = dto.PaisId;
                entity.ESTADO_ID = dto.EstadoId;
                entity.MUNICIPIO_ID = dto.MunicipioId;
                entity.CIUDAD_ID = dto.CiudadId;
                entity.PARROQUIA_ID = dto.ParroquiaId;
                entity.SECTOR_ID = dto.SectorId;
                entity.URBANIZACION_ID = dto.UrbanizacionId;
                entity.TIPO_DIRECCION_ID = dto.TipoDireccionId;
                entity.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                entity.VIALIDAD_ID = dto.VialidadId;
                entity.VIALIDAD = dto.Vialidad;
                entity.VIVIENDA = dto.Vivienda;
                entity.PRINCIPAL = dto.Principal;
              
               
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                proveedor.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapProveedorDireccionDto(created.Data);
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
 
        public async Task<ResultDto<AdmProveedorDireccionDeleteDto>> Delete(AdmProveedorDireccionDeleteDto dto)
        {

            ResultDto<AdmProveedorDireccionDeleteDto> result = new ResultDto<AdmProveedorDireccionDeleteDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoDirProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Direccion no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDirProveedor);

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

        public async Task<ResultDto<AdmProveedorDireccionResponseDto>> GetByCodigo(AdmProveedorDireccionFilterDto dto)
        { 
            ResultDto<AdmProveedorDireccionResponseDto> result = new ResultDto<AdmProveedorDireccionResponseDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoDirProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Contacto existe";
                    return result;
                }
                
                var resultDto = await  MapProveedorDireccionDto(proveedor);
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

        public async Task<ResultDto<List<AdmProveedorDireccionResponseDto>>> GetAll(AdmProveedorDireccionFilterDto dto)
        {
            ResultDto<List<AdmProveedorDireccionResponseDto>> result = new ResultDto<List<AdmProveedorDireccionResponseDto>>(null);
            try
            {

                var proveedor = await _repository.GetByProveedor(dto.CodigoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor no existe";
                    return result;
                }
                
                var resultDto = await  MapListProveedorDireccionDto(proveedor);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
           
        }
    }
}

