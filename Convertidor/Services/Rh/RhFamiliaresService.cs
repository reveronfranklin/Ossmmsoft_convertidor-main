using System.Globalization;
using Convertidor.Services.Sis;
using Convertidor.Utility;

namespace Convertidor.Data.Repository.Rh
{
	public class RhFamiliaresService: IRhFamiliaresService
    {
        
   
        private readonly IRhFamiliaresRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IUtilityService _utilityService;


        public RhFamiliaresService(IRhFamiliaresRepository repository, 
                                        IRhDescriptivasService descriptivaService, 
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IUtilityService utilityService)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _utilityService = utilityService;
        }
       
        public async Task<ResultDto<List<RhFamiliarResponseDto>>> GetByCodigoPersona(int codigoPersona)
        {
            
            ResultDto<List<RhFamiliarResponseDto>> result = new ResultDto<List<RhFamiliarResponseDto>>(null);
            try
            {
                
                var familiares = await _repository.GetByCodigoPersona(codigoPersona);

                var familiaresResult = await MapListFamiliaresDto(familiares);
                result.Data = familiaresResult;
                result.IsValid = true;
                result.Message = "";

                return ( ResultDto<List<RhFamiliarResponseDto>>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }
      


      
       
        public async  Task<RhFamiliarResponseDto> MapFamiliaresDto(RH_FAMILIARES dtos)
        {

            RhFamiliarResponseDto itemResult = new RhFamiliarResponseDto();
                itemResult.CodigoFamiliar = dtos.CODIGO_FAMILIAR;
                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.CedulaFamiliar = dtos.CEDULA_FAMILIAR;
                itemResult.Nacionalidad = dtos.NACIONALIDAD;
                itemResult.Nombre = dtos.NOMBRE;
                itemResult.Apellido = dtos.APELLIDO;
                itemResult.FechaNacimiento = dtos.FECHA_NACIMIENTO;
                itemResult.FechaNacimientoString = Fecha.GetFechaString( dtos.FECHA_NACIMIENTO); 
            
                FechaDto FechaNacimientoObj = Fecha.GetFechaDto(dtos.FECHA_NACIMIENTO);
                itemResult.FechaNacimientoObj = (FechaDto)FechaNacimientoObj;
                
                DateTime fechaActual = DateTime.Now;
                TimeSpan diferecia = fechaActual -   itemResult.FechaNacimiento;
                double dias = diferecia.TotalDays;
                double meses =  Math.Floor(dias / 365);
                double años = Math.Floor(dias / 365);
                string edad = "";
                if (años > 1)
                {
                    edad = $"{años} Años";
                }
                else
                {
                    edad = $"{meses} Meses";
                }

                itemResult.Edad = edad;
                itemResult.ParienteId = dtos.PARIENTE_ID;
                var pariente = await _descriptivaService.GetDescripcionByCodigoDescriptiva(dtos.PARIENTE_ID);
                
                itemResult.ParienteDescripcion =pariente;
                itemResult.Sexo = dtos.SEXO;
                itemResult.NivelEducativo = dtos.NIVEL_EDUCATIVO_ID;
                itemResult.Grado = dtos.GRADO;
               
                return itemResult;
        }

        public async  Task<List<RhFamiliarResponseDto>> MapListFamiliaresDto(List<RH_FAMILIARES> dtos)
        {
            List<RhFamiliarResponseDto> result = new List<RhFamiliarResponseDto>();
            
            foreach (var item in dtos)
            {

                RhFamiliarResponseDto itemResult = new RhFamiliarResponseDto();

                itemResult = await MapFamiliaresDto(item);
               
                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<RhFamiliarResponseDto>> Update(RhFamiliarUpdateDto dto)
        {

            ResultDto<RhFamiliarResponseDto> result = new ResultDto<RhFamiliarResponseDto>(null);
            try
            {

                var familiar = await _repository.GetByCodigo(dto.CodigoFamiliar);
                if (familiar == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Familiar no existe";
                    return result;
                }
                if (dto.CedulaFamiliar <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cedula Invalida";
                    return result;
                }
                if (dto.Nombre.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Invalido";
                    return result;
                }
                if (dto.Apellido.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Apellido Invalido";
                    return result;
                }
                var nacionalidad = _utilityService.GetListNacionalidad().Where(x => x== dto.Nacionalidad).FirstOrDefault();
                if (String.IsNullOrEmpty(nacionalidad))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nacionalidad Invalida";
                    return result;
                    
                }
                var sexo = _utilityService.GetListSexo().Where(x => x== dto.Sexo).FirstOrDefault();
                if (String.IsNullOrEmpty(sexo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sexo Invalido";
                    return result;
                    
                }
                var pariente = await _descriptivaService.GetDescripcionByCodigoDescriptiva(dto.ParienteId);
                if (String.IsNullOrEmpty(pariente))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Pariente Invalido";
                    return result;
                    
                }
               /* var nivelEducativo = await _descriptivaService.GetDescripcionByCodigoDescriptiva(dto.NivelEducativo);
                if (String.IsNullOrEmpty(nivelEducativo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nivel Educativo Invalido";
                    return result;
                    
                }*/

                familiar.CEDULA_FAMILIAR = dto.CedulaFamiliar;
                familiar.NACIONALIDAD = dto.Nacionalidad;
                familiar.NOMBRE = dto.Nombre;
                familiar.APELLIDO = dto.Apellido;
                var fechaNacimiento = Convert.ToDateTime(dto.FechaNacimientoString, CultureInfo.InvariantCulture);
                familiar.FECHA_NACIMIENTO = fechaNacimiento;
                familiar.PARIENTE_ID = dto.ParienteId;
                familiar.SEXO = dto.Sexo;
                familiar.NIVEL_EDUCATIVO_ID = dto.NivelEducativo;
                familiar.GRADO = dto.Grado;
                familiar.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                familiar.CODIGO_EMPRESA = conectado.Empresa;
                familiar.USUARIO_UPD = conectado.Usuario;
              
            

                await _repository.Update(familiar);

                var resultDto = await MapFamiliaresDto(familiar);
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

        public async Task<ResultDto<RhFamiliarResponseDto>> Create(RhFamiliarUpdateDto dto)
        {

            ResultDto<RhFamiliarResponseDto> result = new ResultDto<RhFamiliarResponseDto>(null);
            try
            {
              
                if (dto.CedulaFamiliar <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cedula Invalida";
                    return result;
                }
                if (dto.Nombre.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Invalido";
                    return result;
                }
                if (dto.Apellido.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Apellido Invalido";
                    return result;
                }
                var nacionalidad = _utilityService.GetListNacionalidad().Where(x => x== dto.Nacionalidad).FirstOrDefault();
                if (String.IsNullOrEmpty(nacionalidad))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nacionalidad Invalida";
                    return result;
                    
                }
                var sexo = _utilityService.GetListSexo().Where(x => x== dto.Sexo).FirstOrDefault();
                if (String.IsNullOrEmpty(sexo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sexo Invalido";
                    return result;
                    
                }
                var pariente = await _descriptivaService.GetDescripcionByCodigoDescriptiva(dto.ParienteId);
                if (String.IsNullOrEmpty(pariente))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Pariente Invalido";
                    return result;
                    
                }
              

                RH_FAMILIARES entity = new RH_FAMILIARES();
                entity.CODIGO_FAMILIAR = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.CEDULA_FAMILIAR = dto.CedulaFamiliar;
                entity.NACIONALIDAD = dto.Nacionalidad;
                entity.NOMBRE = dto.Nombre;
                entity.APELLIDO = dto.Apellido;
                var fechaNacimiento = Convert.ToDateTime(dto.FechaNacimientoString, CultureInfo.InvariantCulture);
                entity.FECHA_NACIMIENTO = fechaNacimiento;
                entity.PARIENTE_ID = dto.ParienteId;
                entity.SEXO = dto.Sexo;
                entity.NIVEL_EDUCATIVO_ID = dto.NivelEducativo;
                entity.GRADO = dto.Grado;
                
                
                entity.FECHA_INS = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;


                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapFamiliaresDto(created.Data);
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
 
        public async Task<ResultDto<RhFamiliarDeleteDto>> Delete(RhFamiliarDeleteDto dto)
        {

            ResultDto<RhFamiliarDeleteDto> result = new ResultDto<RhFamiliarDeleteDto>(null);
            try
            {

                var familiar = await _repository.GetByCodigo(dto.CodigoFamiliar);
                if (familiar == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Familiar no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoFamiliar);

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

