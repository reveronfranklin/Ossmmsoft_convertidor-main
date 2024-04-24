namespace Convertidor.Data.Repository.Rh
{
	public class RhEducacionService: IRhEducacionService
    {
        
        private readonly IRhEducacionRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _personasRepository;

        public RhEducacionService(IRhEducacionRepository repository,
                           IRhDescriptivasService descriptivaService,
                           ISisUsuarioRepository sisUsuarioRepository,
                           IRhPersonasRepository personasRepository)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _personasRepository = personasRepository;
           
        }
       
        public async Task<List<RhEducacionResponseDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var educacion = await _repository.GetByCodigoPersona(codigoPersona);

                var result = await MapListEducacionDto(educacion);


                return (List<RhEducacionResponseDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_EDUCACION> GetByCodigo(int codigoEducacion)
        {
            try
            {
               
                var result =  await _repository.GetByCodigo(codigoEducacion);

                return (RH_EDUCACION)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);
    
            return FechaDesdeObj;
        }
        public async Task<RhEducacionResponseDto> MapEducacionDto(RH_EDUCACION dtos)
        {


            RhEducacionResponseDto itemResult = new RhEducacionResponseDto();
            itemResult.CodigoEducacion = dtos.CODIGO_EDUCACION;
            itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
            itemResult.NivelId = dtos.NIVEL_ID;
            itemResult.DescripcionNivel = await _descriptivaService.GetDescripcionByCodigoDescriptiva(dtos.NIVEL_ID);
            itemResult.NombreInstituto = dtos.NOMBRE_INSTITUTO;
            itemResult.LocalidadInstituto =dtos.LOCALIDAD_INSTITUTO;
            itemResult.ProfesionID = dtos.PROFESION_ID;
            itemResult.FechaIni = dtos.FECHA_INI;
            itemResult.FechaIniString = dtos.FECHA_INI.ToString("u"); 
            itemResult.FechaFin = dtos.FECHA_FIN;
            itemResult.FechaFinString = dtos.FECHA_FIN.ToString("u"); 
            FechaDto FechaIniObj = GetFechaDto(dtos.FECHA_INI);
            itemResult.FechaIniObj = (FechaDto)FechaIniObj;
            FechaDto FechaFinObj = GetFechaDto(dtos.FECHA_FIN);
            itemResult.FechaFinObj = (FechaDto)FechaFinObj;
            itemResult.UltimoAñoAprobado = dtos.ULTIMO_ANO_APROBADO;
            itemResult.Graduado = dtos.GRADUADO;
            itemResult.TituloId = dtos.TITULO_ID;
            itemResult.MencionEspecialidadId = dtos.MENCION_ESPECIALIDAD_ID;
        


            return itemResult;

        }


        public async  Task<List<RhEducacionResponseDto>> MapListEducacionDto(List<RH_EDUCACION> dtos)
        {
            List<RhEducacionResponseDto> result = new List<RhEducacionResponseDto>();

            foreach (var item in dtos)
            {

              
                var itemResult=await MapEducacionDto(item);
              
                
                result.Add(itemResult);


            }
            return result;

        }
        public async Task<ResultDto<RhEducacionResponseDto>> Update(RhEducacionUpdate dto)
        {

            ResultDto<RhEducacionResponseDto> result = new ResultDto<RhEducacionResponseDto>(null);
            try
            {

                var codigoEducacion = await _repository.GetByCodigo(dto.CodigoEducacion);
                if (codigoEducacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo educacion invalido";
                    return result;
                }

                var niveles = await _descriptivaService.GetByTitulo(5);

                {
                    var nivel = niveles.Where(x => x.Id == dto.NivelId);
                    if (niveles is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Nivel id Invalido";
                        return result;
                    }
                }
                var profesiones = await _descriptivaService.GetByTitulo(8);

                {
                    var profesion = profesiones.Where(x => x.Id == dto.ProfesionId);
                    if (profesiones is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Profesion Id Invalido";
                        return result;
                    }
                }

                

                if (  string.IsNullOrEmpty(dto.LocalidadInstituto)  )
                {
                    result.Message = "Localidad invalida!!";
                    result.IsValid = false;
                    return result;
                }


                if (string.IsNullOrEmpty(dto.NombreInstituto) )
                {
                    result.Message = "Instituto invalido";
                    result.IsValid = false;
                    return result;
                }
            
                if (dto.Graduado is not null && dto.Graduado.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Graduado invalido";
                }

                var titulos = await _descriptivaService.GetByTitulo(16);

                {
                    var titulo = titulos.Where(x => x.Id == dto.TituloId);
                    if (titulo is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Titulo Id invalido";
                        return result;
                    }
                }


                var mencionEspecialdades = await _descriptivaService.GetByTitulo(25);

                {
                    var mencionEspecialdad = mencionEspecialdades.Where(x => x.Id == dto.MencionEspecialidadId);
                    if (mencionEspecialdad is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Mencion especialidad Id Invalida";
                        return result;
                    }
                }



                codigoEducacion.CODIGO_EDUCACION = dto.CodigoEducacion;
                codigoEducacion.CODIGO_PERSONA = dto.CodigoPersona;
                codigoEducacion.NIVEL_ID = dto.NivelId;
                codigoEducacion.NOMBRE_INSTITUTO = dto.NombreInstituto;
                codigoEducacion.LOCALIDAD_INSTITUTO = dto.LocalidadInstituto;
                codigoEducacion.PROFESION_ID = dto.ProfesionId;
                codigoEducacion.FECHA_INI = dto.FechaIni;
                codigoEducacion.FECHA_FIN = dto.FechaFin;
                codigoEducacion.ULTIMO_ANO_APROBADO = dto.UltimoAñoAprobado;
                codigoEducacion.GRADUADO = dto.Graduado;
                codigoEducacion.TITULO_ID = dto.TituloId;
                codigoEducacion.MENCION_ESPECIALIDAD_ID = dto.MencionEspecialidadId;
               
                

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoEducacion.CODIGO_EMPRESA = conectado.Empresa;
                codigoEducacion.USUARIO_UPD = conectado.Usuario;
                codigoEducacion.FECHA_UPD=DateTime.Now;


                await _repository.Update(codigoEducacion);



                var resultDto = await MapEducacionDto(codigoEducacion);
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

        public async Task<ResultDto<RhEducacionResponseDto>> Create (RhEducacionUpdate dto)
        {

            ResultDto<RhEducacionResponseDto> result = new ResultDto<RhEducacionResponseDto>(null);
            try
            {
                var codigoEducacion = await _repository.GetByCodigo(dto.CodigoEducacion);
                if (codigoEducacion != null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo educacion invalido";
                    return result;
                }

                var niveles = await _descriptivaService.GetByTitulo(5);

                {
                    var nivel = niveles.Where(x => x.Id == dto.NivelId);
                    if (niveles is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Nivel id Invalido";
                        return result;
                    }
                }
                var profesiones = await _descriptivaService.GetByTitulo(8);

                {
                    var profesion = profesiones.Where(x => x.Id == dto.ProfesionId);
                    if (profesiones is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Profesion Id Invalido";
                        return result;
                    }
                }

               

                if(string.IsNullOrEmpty(dto.LocalidadInstituto) ) 
                {
                    result.Message = "Localidad invalida!!";
                    result.IsValid = false;
                    return result;
                }
                
                
                if(string.IsNullOrEmpty(dto.NombreInstituto)) 
                {
                    result.Message = "Instituto invalido";
                    result.IsValid = false;
                    return result;
                }

              

                if (dto.Graduado is not null && dto.Graduado.Length>1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Graduado invalido";
                }

                var titulos = await _descriptivaService.GetByTitulo(16);

                {
                    var titulo = titulos.Where(x => x.Id == dto.TituloId);
                    if (titulo is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Titulo Id invalido";
                        return result;
                    }
                }


                var mencionEspecialdades = await _descriptivaService.GetByTitulo(25);

                {
                    var mencionEspecialdad = mencionEspecialdades.Where(x => x.Id == dto.MencionEspecialidadId);
                    if (mencionEspecialdad is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Mencion especialidad Id Invalida";
                        return result;
                    }
                }


                RH_EDUCACION entity = new RH_EDUCACION();
                entity.CODIGO_EDUCACION = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.NIVEL_ID = dto.NivelId;
                entity.NOMBRE_INSTITUTO = dto.NombreInstituto;
                entity.LOCALIDAD_INSTITUTO = dto.LocalidadInstituto;
                entity.PROFESION_ID = dto.ProfesionId;
                entity.FECHA_INI = dto.FechaIni;
                entity.FECHA_FIN = dto.FechaFin;
                entity.ULTIMO_ANO_APROBADO = dto.UltimoAñoAprobado;
                entity.GRADUADO = dto.Graduado;
                entity.TITULO_ID = dto.TituloId;
                entity.MENCION_ESPECIALIDAD_ID = dto.MencionEspecialidadId;
           





                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS=DateTime.Now;

                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapEducacionDto(created.Data);
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

        public async Task<ResultDto<RhEducacionDeleteDto>> Delete(RhEducacionDeleteDto dto)
        {

            ResultDto<RhEducacionDeleteDto> result = new ResultDto<RhEducacionDeleteDto>(null);
            try
            {

                var educacion = await _repository.GetByCodigo(dto.CodigoEducacion);
                if (educacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Educacion no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoEducacion);

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


