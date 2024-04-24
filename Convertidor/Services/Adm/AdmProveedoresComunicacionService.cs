using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;


namespace Convertidor.Services.Adm
{
    
	public class AdmProveedoresComunicacionService: IAdmProveedoresComunicacionService
    {

      
        private readonly IAdmComunicacionProveedorRepository _repository;
        private readonly IAdmDescriptivaRepository _repositoryPreDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        private readonly IAdmProveedoresRepository _proveedorRepository;


        public AdmProveedoresComunicacionService(IAdmComunicacionProveedorRepository repository,
                                      IAdmDescriptivaRepository repositoryPreDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                         
                                      IAdmProveedoresRepository proveedorRepository)
		{
            _repository = repository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;

            _proveedorRepository = proveedorRepository;
        }



       
       
        public  async Task<AdmProveedorComunicacionResponseDto> MapProveedorComDto(ADM_COM_PROVEEDOR dtos)
        {
            AdmProveedorComunicacionResponseDto itemResult = new AdmProveedorComunicacionResponseDto();
            
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.CodigoComProveedor = dtos.CODIGO_COM_PROVEEDOR;
            
            
            itemResult.TipoComunicacionId = dtos.TIPO_COMUNICACION_ID;
            itemResult.TipoComunicacionDescripcion = "";
            var descriptiva = await _repositoryPreDescriptiva.GetByCodigo( dtos.TIPO_COMUNICACION_ID);
            if (descriptiva != null)
            {
                itemResult.TipoComunicacionDescripcion = descriptiva.DESCRIPCION;
            }
            itemResult.CodigoArea = dtos.CODIGO_AREA;
            itemResult.LineaComunicacion = dtos.LINEA_COMUNICACION;
            itemResult.Principal = dtos.PRINCIPAL;
            itemResult.Extension = dtos.EXTENSION;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;

            return itemResult;
        }

        public async Task< List<AdmProveedorComunicacionResponseDto>> MapListProveedorComDto(List<ADM_COM_PROVEEDOR> dtos)
        {
            List<AdmProveedorComunicacionResponseDto> result = new List<AdmProveedorComunicacionResponseDto>();
           
            
            foreach (var item in dtos)
            {
                

                var itemResult =  await MapProveedorComDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
        public async Task<ResultDto<AdmProveedorComunicacionResponseDto?>> Update(AdmProveedorComunicacionUpdateDto dto)
        {

            ResultDto<AdmProveedorComunicacionResponseDto?> result = new ResultDto<AdmProveedorComunicacionResponseDto?>(null);
            try
            {

                var proveedorComunicacion = await _repository.GetByCodigo(dto.CodigoComProveedor);
                if (proveedorComunicacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Actividad no existe";
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

                
                var tiposComunicacion = await _repositoryPreDescriptiva.GetByIdAndTitulo(5,dto.TipoComunicacionId);
                if (tiposComunicacion==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor Actividad  Invalido";
                    return result;
                }




                proveedorComunicacion.TIPO_COMUNICACION_ID = dto.TipoComunicacionId;
                proveedorComunicacion.CODIGO_AREA = dto.CodigoArea;
                proveedorComunicacion.LINEA_COMUNICACION = dto.LineaComunicacion;
                proveedorComunicacion.EXTENSION = dto.Extension;
                proveedorComunicacion.PRINCIPAL = dto.Principal;
                proveedorComunicacion.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                proveedorComunicacion.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                proveedorComunicacion.CODIGO_EMPRESA = conectado.Empresa;
                proveedorComunicacion.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(proveedorComunicacion);
                var resultDto = await  MapProveedorComDto(proveedorComunicacion);
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

        public async Task<ResultDto<AdmProveedorComunicacionResponseDto>> Create(AdmProveedorComunicacionUpdateDto dto)
        {

            ResultDto<AdmProveedorComunicacionResponseDto> result = new ResultDto<AdmProveedorComunicacionResponseDto>(null);
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

                
                var tiposComunicacion = await _repositoryPreDescriptiva.GetByIdAndTitulo(5,dto.TipoComunicacionId);
                if (tiposComunicacion==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor Actividad  Invalido";
                    return result;
                }


               
               
             
             
                
                ADM_COM_PROVEEDOR entity = new ADM_COM_PROVEEDOR();
                entity.CODIGO_COM_PROVEEDOR = await _repository.GetNextKey();
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.TIPO_COMUNICACION_ID = dto.TipoComunicacionId;
                entity.CODIGO_AREA = dto.CodigoArea;
                entity.LINEA_COMUNICACION = dto.LineaComunicacion;
                entity.EXTENSION = dto.Extension;
                entity.PRINCIPAL = dto.Principal;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                proveedor.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapProveedorComDto(created.Data);
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
 
        public async Task<ResultDto<AdmProveedorComunicacionDeleteDto>> Delete(AdmProveedorComunicacionDeleteDto dto)
        {

            ResultDto<AdmProveedorComunicacionDeleteDto> result = new ResultDto<AdmProveedorComunicacionDeleteDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoComProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Actividad no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoComProveedor);

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

        public async Task<ResultDto<AdmProveedorComunicacionResponseDto>> GetByCodigo(AdmProveedorComunicacionFilterDto dto)
        { 
            ResultDto<AdmProveedorComunicacionResponseDto> result = new ResultDto<AdmProveedorComunicacionResponseDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoComProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Comunicacion no existe";
                    return result;
                }
                
                var resultDto =  await MapProveedorComDto(proveedor);
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

        public async Task<ResultDto<List<AdmProveedorComunicacionResponseDto>>> GetAll(AdmProveedorComunicacionFilterDto dto)
        {
            ResultDto<List<AdmProveedorComunicacionResponseDto>> result = new ResultDto<List<AdmProveedorComunicacionResponseDto>>(null);
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
                 
                var resultDto = await  MapListProveedorComDto(proveedor);
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

