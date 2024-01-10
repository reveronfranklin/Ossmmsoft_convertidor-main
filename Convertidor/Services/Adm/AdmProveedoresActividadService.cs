using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Services.Rh;
using Convertidor.Utility;
using NPOI.SS.Formula.Functions;


namespace Convertidor.Services.Adm
{
	public class AdmProveedoresActividadService: IAdmProveedoresActividadService
    {

      
        private readonly IAdmActividadProveedorRepository _repository;
        private readonly IAdmDescriptivaRepository _repositoryPreDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonaService _personaServices;
        private readonly IAdmProveedoresRepository _proveedorRepository;
        private IAdmProveedoresActividadService _admProveedoresActividadServiceImplementation;

        public AdmProveedoresActividadService(IAdmActividadProveedorRepository repository,
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



       
       
        public  async Task<AdmProveedorActividadResponseDto> MapProveedorActDto(ADM_ACT_PROVEEDOR dtos)
        {
            AdmProveedorActividadResponseDto itemResult = new AdmProveedorActividadResponseDto();
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.CodigoActProveedor = dtos.CODIGO_ACT_PROVEEDOR;
            itemResult.ActividadId = dtos.ACTIVIDAD_ID;
            itemResult.DescripcionActividad = "";
            var descriptivaActividad = await _repositoryPreDescriptiva.GetByCodigo( dtos.ACTIVIDAD_ID);
            if (descriptivaActividad != null)
            {
                itemResult.DescripcionActividad = descriptivaActividad.DESCRIPCION;
            }
            itemResult.FechaIni = dtos.FECHA_INI;
            itemResult.FechaIniString =dtos.FECHA_INI.ToString("u");   
            FechaDto fechaIniObj = FechaObj.GetFechaDto(dtos.FECHA_INI);
            itemResult.FechaIniObj = fechaIniObj;
            
            
            itemResult.FechaFin = dtos.FECHA_FIN;
            itemResult.FechaFinString =dtos.FECHA_FIN.ToString("u");   
            FechaDto fechaFinObj = FechaObj.GetFechaDto(dtos.FECHA_FIN);
            itemResult.FechaIniObj = fechaFinObj;
          
            return itemResult;
        }

        public async Task< List<AdmProveedorActividadResponseDto>> MapListProveedorActDto(List<ADM_ACT_PROVEEDOR> dtos)
        {
            List<AdmProveedorActividadResponseDto> result = new List<AdmProveedorActividadResponseDto>();
           
            
            foreach (var item in dtos)
            {
                

                var itemResult =  await MapProveedorActDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
        public async Task<ResultDto<AdmProveedorActividadResponseDto?>> Update(AdmProveedorActividadUpdateDto dto)
        {

            ResultDto<AdmProveedorActividadResponseDto?> result = new ResultDto<AdmProveedorActividadResponseDto?>(null);
            try
            {

                var proveedorActividad = await _repository.GetByCodigo(dto.CodigoActProveedor);
                if (proveedorActividad == null)
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

                
                var tiposProveedorActividad = await _repositoryPreDescriptiva.GetByTitulo(13);
                if (tiposProveedorActividad.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor Actividad  Invalido";
                    return result;
                }
                else
                {
                    var tipoProveedor = tiposProveedorActividad.Where(x => x.DESCRIPCION_ID== dto.ActividadId);
                    if (tipoProveedor is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo Proveedor  Invalido";
                        return result;
                    }
                }
               
               
             
             

                if (!DateValidate.IsDate(dto.FechaIniString))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Inicial Invalida";
                    return result;
                }

                if (!DateValidate.IsDate(dto.FechaFinString))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Inicial Invalida";
                    return result;
                }

              

                proveedorActividad.FECHA_INI = dto.FechaIni;
                proveedorActividad.FECHA_FIN = dto.FechaFin;
              
                proveedorActividad.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                proveedorActividad.CODIGO_EMPRESA = conectado.Empresa;
                proveedorActividad.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(proveedorActividad);
                
                var resultDto = await  MapProveedorActDto(proveedorActividad);
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

        public async Task<ResultDto<AdmProveedorActividadResponseDto>> Create(AdmProveedorActividadUpdateDto dto)
        {

            ResultDto<AdmProveedorActividadResponseDto> result = new ResultDto<AdmProveedorActividadResponseDto>(null);
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

                
                var tiposProveedorActividad = await _repositoryPreDescriptiva.GetByTitulo(13);
                if (tiposProveedorActividad.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor Actividad  Invalido";
                    return result;
                }
                else
                {
                    var tipoProveedor = tiposProveedorActividad.Where(x => x.DESCRIPCION_ID== dto.ActividadId);
                    if (tipoProveedor is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo Proveedor  Invalido";
                        return result;
                    }
                }
               
               
             
             

                if (!DateValidate.IsDate(dto.FechaIniString))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Inicial Invalida";
                    return result;
                }

                if (!DateValidate.IsDate(dto.FechaFinString))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Inicial Invalida";
                    return result;
                }
                ADM_ACT_PROVEEDOR entity = new ADM_ACT_PROVEEDOR();
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.ACTIVIDAD_ID = dto.ActividadId;
                entity.FECHA_INI = dto.FechaIni;
                entity.FECHA_FIN = dto.FechaFin;
                entity.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                proveedor.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapProveedorActDto(created.Data);
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
 
        public async Task<ResultDto<AdmProveedorActividadDeleteDto>> Delete(AdmProveedorActividadDeleteDto dto)
        {

            ResultDto<AdmProveedorActividadDeleteDto> result = new ResultDto<AdmProveedorActividadDeleteDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoActProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Actividad no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoActProveedor);

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

        public async Task<ResultDto<AdmProveedorActividadResponseDto>> GetByCodigo(AdmProveedorActividadFilterDto dto)
        { 
            ResultDto<AdmProveedorActividadResponseDto> result = new ResultDto<AdmProveedorActividadResponseDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoActProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Actividadno existe";
                    return result;
                }
                
                var resultDto =  await MapProveedorActDto(proveedor);
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

        public async Task<ResultDto<List<AdmProveedorActividadResponseDto>>> GetAll(AdmProveedorActividadFilterDto dto)
        {
            ResultDto<List<AdmProveedorActividadResponseDto>> result = new ResultDto<List<AdmProveedorActividadResponseDto>>(null);
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
                
                var resultDto = await  MapListProveedorActDto(proveedor);
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

