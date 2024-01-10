using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Services.Rh;
using Convertidor.Utility;
using NPOI.SS.Formula.Functions;
using NPOI.Util;


namespace Convertidor.Services.Adm
{
	public class AdmProveedoresContactosService: IAdmProveedoresContactoService
    {

      
        private readonly IAdmContactosProveedorRepository _repository;
        private readonly IAdmDescriptivaRepository _repositoryPreDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonaService _personaServices;
        private readonly IAdmProveedoresRepository _proveedorRepository;
        private IAdmProveedoresActividadService _admProveedoresActividadServiceImplementation;

        public AdmProveedoresContactosService(IAdmContactosProveedorRepository repository,
                                      IAdmDescriptivaRepository repositoryPreDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IRhPersonaService personaServices,
                                      IAdmProveedoresRepository proveedorRepository)
		{
            _repository = repository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;
            _personaServices = personaServices;
            _proveedorRepository = proveedorRepository;
        }



       
       
        public  async Task<AdmProveedorContactoResponseDto> MapProveedorContactoDto(ADM_CONTACTO_PROVEEDOR dtos)
        {
            AdmProveedorContactoResponseDto itemResult = new AdmProveedorContactoResponseDto();
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            
            itemResult.CodigoContactoProveedor = dtos.CODIGO_CONTACTO_PROVEEDOR;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.TipoContactoDescripcion = "";
            var descriptiva = await _repositoryPreDescriptiva.GetByCodigo( dtos.TIPO_CONTACTO_ID);
            if (descriptiva != null)
            {
                itemResult.TipoContactoDescripcion = descriptiva.DESCRIPCION;
            }
            itemResult.Nombre = dtos.NOMBRE;
            itemResult.Apellido = dtos.APELLIDO;
            itemResult.IdentificacionId = dtos.IDENTIFICACION_ID;
            itemResult.Identificacion = dtos.IDENTIFICACION;
            itemResult.Sexo = dtos.SEXO;
            itemResult.TipoContactoId = dtos.TIPO_CONTACTO_ID;
            itemResult.Principal = dtos.PRINCIPAL;
           
          
            return itemResult;
        }

        public async Task< List<AdmProveedorContactoResponseDto>> MapListProveedorContactoDto(List<ADM_CONTACTO_PROVEEDOR> dtos)
        {
            List<AdmProveedorContactoResponseDto> result = new List<AdmProveedorContactoResponseDto>();
           
            
            foreach (var item in dtos)
            {
                

                var itemResult =  await MapProveedorContactoDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
        public async Task<ResultDto<AdmProveedorContactoResponseDto?>> Update(AdmProveedorContactoUpdateDto dto)
        {

            ResultDto<AdmProveedorContactoResponseDto?> result = new ResultDto<AdmProveedorContactoResponseDto?>(null);
            try
            {

                var proveedorContacto = await _repository.GetByCodigo(dto.CodigoContactoProveedor);
                if (proveedorContacto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Contacto no existe";
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

                
                var tiposContacto = await _repositoryPreDescriptiva.GetByIdAndTitulo(10,dto.TipoContactoId);
                if (tiposContacto==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor Contacto  Invalido";
                    return result;
                }
                var tipoIdentificacio = await _repositoryPreDescriptiva.GetByIdAndTitulo(9,dto.IdentificacionId);
                if (tipoIdentificacio==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor Identificacion  Invalido";
                    return result;
                }

                if (String.IsNullOrEmpty(dto.Nombre))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Invalido";
                    return result;
                    
                }
                if (String.IsNullOrEmpty(dto.Apellido))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Apellido Invalido";
                    return result;
                    
                }
                

                var sexo = GetListSexo().Where(x => x== dto.Sexo).FirstOrDefault();
                if (String.IsNullOrEmpty(sexo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sexo Invalido";
                    return result;
                    
                }
                
                proveedorContacto.NOMBRE = dto.Nombre;
                proveedorContacto.APELLIDO = dto.Apellido;
                proveedorContacto.IDENTIFICACION_ID = dto.IdentificacionId;
                proveedorContacto.IDENTIFICACION = dto.Identificacion;
                proveedorContacto.SEXO = dto.Sexo;
                proveedorContacto.TIPO_CONTACTO_ID = dto.TipoContactoId;
                proveedorContacto.PRINCIPAL = dto.Principal;
                proveedorContacto.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                proveedorContacto.CODIGO_EMPRESA = conectado.Empresa;
                proveedorContacto.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(proveedorContacto);
                
                var resultDto = await  MapProveedorContactoDto(proveedorContacto);
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
        public async Task<ResultDto<AdmProveedorContactoResponseDto>> Create(AdmProveedorContactoUpdateDto dto)
        {

            ResultDto<AdmProveedorContactoResponseDto> result = new ResultDto<AdmProveedorContactoResponseDto>(null);
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

                
                var tiposContacto = await _repositoryPreDescriptiva.GetByIdAndTitulo(10,dto.TipoContactoId);
                if (tiposContacto==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor Contacto  Invalido";
                    return result;
                }
                var tipoIdentificacio = await _repositoryPreDescriptiva.GetByIdAndTitulo(9,dto.IdentificacionId);
                if (tipoIdentificacio==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor Identificacion  Invalido";
                    return result;
                }

                if (String.IsNullOrEmpty(dto.Nombre))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Invalido";
                    return result;
                    
                }
                if (String.IsNullOrEmpty(dto.Apellido))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Apellido Invalido";
                    return result;
                    
                }
                

                var sexo = GetListSexo().Where(x => x== dto.Sexo).FirstOrDefault();
                if (String.IsNullOrEmpty(sexo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sexo Invalido";
                    return result;
                    
                }
                ADM_CONTACTO_PROVEEDOR entity = new ADM_CONTACTO_PROVEEDOR();
                entity.CODIGO_CONTACTO_PROVEEDOR = await _repository.GetNextKey();
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.NOMBRE = dto.Nombre;
                entity.APELLIDO = dto.Apellido;
                entity.IDENTIFICACION_ID = dto.IdentificacionId;
                entity.IDENTIFICACION = dto.Identificacion;
                entity.SEXO = dto.Sexo;
                entity.TIPO_CONTACTO_ID = dto.TipoContactoId;
                entity.PRINCIPAL = dto.Principal;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                proveedor.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapProveedorContactoDto(created.Data);
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
 
        public async Task<ResultDto<AdmProveedorContactoDeleteDto>> Delete(AdmProveedorContactoDeleteDto dto)
        {

            ResultDto<AdmProveedorContactoDeleteDto> result = new ResultDto<AdmProveedorContactoDeleteDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoContactoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Contacto no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoContactoProveedor);

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

        public async Task<ResultDto<AdmProveedorContactoResponseDto>> GetByCodigo(AdmProveedorContactoFilterDto dto)
        { 
            ResultDto<AdmProveedorContactoResponseDto> result = new ResultDto<AdmProveedorContactoResponseDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoContactoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Contacto existe";
                    return result;
                }
                
                var resultDto =  await MapProveedorContactoDto(proveedor);
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

        public async Task<ResultDto<List<AdmProveedorContactoResponseDto>>> GetAll(AdmProveedorContactoFilterDto dto)
        {
            ResultDto<List<AdmProveedorContactoResponseDto>> result = new ResultDto<List<AdmProveedorContactoResponseDto>>(null);
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
                
                var resultDto = await  MapListProveedorContactoDto(proveedor);
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

