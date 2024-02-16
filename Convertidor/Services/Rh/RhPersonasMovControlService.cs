namespace Convertidor.Data.Repository.Rh
{
	public class RhPersonasMovControlService : IRhPersonasMovControlService
    {
		
        private readonly DataContext _context;



   
        private readonly IRhPersonasMovControlRepository _repository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;
        private readonly IMapper _mapper;

        public RhPersonasMovControlService(IRhPersonasMovControlRepository repository,
                          IRhPersonasRepository rhPersonasRepository,
                          ISisUsuarioRepository sisUsuarioRepository,
                          IRhConceptosRepository rhConceptosRepository)
        {
            _repository = repository;
            _rhPersonasRepository = rhPersonasRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhConceptosRepository = rhConceptosRepository;
        }
       
    

        public async Task<List<RhPersonasMovControlResponseDto>> GetCodigoPersona(int codigoPersona)
        {
            try
            {

                var MovimientoControl = await _repository.GetCodigoPersona(codigoPersona);

                var result = await MapListPersonasMovControlDto(MovimientoControl);


                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<RhPersonasMovControlResponseDto> MapPersonasMovControlDto(RH_PERSONAS_MOV_CONTROL dtos)
        {


            RhPersonasMovControlResponseDto itemResult = new RhPersonasMovControlResponseDto();
            itemResult.CodigoPersonaMovCtrl = dtos.CODIGO_PERSONA_MOV_CTRL;
            itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
            itemResult.CodigoConcepto = dtos.CODIGO_CONCEPTO;
            itemResult.ControlAplica = dtos.CONTROL_APLICA;
            if (itemResult.ControlAplica == 1)
            {
                itemResult.DescripcionControlAplica = "SI";

            }
            else
            {
                itemResult.DescripcionControlAplica = "NO";
            }
               
                
            var concepto = await _rhConceptosRepository.GetByCodigo(itemResult.CodigoConcepto);
            if (concepto != null)
            {
                itemResult.DescripcionConcepto = concepto.DESCRIPCION;
            }


            return itemResult;

        }


        public async Task<List<RhPersonasMovControlResponseDto>> MapListPersonasMovControlDto(List<RH_PERSONAS_MOV_CONTROL> dtos)
        {
            List<RhPersonasMovControlResponseDto> result = new List<RhPersonasMovControlResponseDto>();

            foreach (var item in dtos)
            {

                RhPersonasMovControlResponseDto itemResult = new RhPersonasMovControlResponseDto();
                itemResult = await MapPersonasMovControlDto(item);
                
                result.Add(itemResult);

            }
            return result;



        }

        public async Task<ResultDto<RhPersonasMovControlResponseDto>> Create(RhPersonasMovControlUpdateDto dto)
        {

            ResultDto<RhPersonasMovControlResponseDto> result = new ResultDto<RhPersonasMovControlResponseDto>(null);
            try
            {

                var persona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (persona is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo persona Invalida";
                    return result;
                }

                var codigo = await _rhConceptosRepository.GetByCodigo(dto.CodigoConcepto);
                if (codigo is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo concepto  Invalido";
                    return result;
                }

                if (dto.ControlAplica < 0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Control Aplica Invalido";
                    return result;
                }

                

                RH_PERSONAS_MOV_CONTROL entity = new RH_PERSONAS_MOV_CONTROL();
                entity.CODIGO_PERSONA_MOV_CTRL = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.CODIGO_CONCEPTO = dto.CodigoConcepto;
                entity.CONTROL_APLICA = dto.ControlAplica;
             
                

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPersonasMovControlDto(created.Data);
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

        public async Task<ResultDto<RhPersonasMovControlResponseDto>> Update(RhPersonasMovControlUpdateDto dto)
        {

            ResultDto<RhPersonasMovControlResponseDto> result = new ResultDto<RhPersonasMovControlResponseDto>(null);
            try
            {
                var codigoPersonaMoV = await _repository.GetByCodigo(dto.CodigoPersonaMovCtrl);
                if (codigoPersonaMoV==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo movimiento persona  Invalido";
                    return result;
                }
                var codigo = await _rhConceptosRepository.GetByCodigo(dto.CodigoConcepto);
                if (codigo is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message ="Codigo Concepto  Invalido";
                    return result;
                }

                var codigoPersona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (codigoPersona is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message ="Persona  Invalida";
                    return result;
                }

                if (dto.ControlAplica < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Control Aplica Invalido";
                    return result;
                }


                RH_PERSONAS_MOV_CONTROL entity = new RH_PERSONAS_MOV_CONTROL();
                codigoPersonaMoV.CODIGO_PERSONA_MOV_CTRL= dto.CodigoPersonaMovCtrl;
                codigoPersonaMoV.CODIGO_PERSONA = dto.CodigoPersona;
                codigoPersonaMoV.CODIGO_CONCEPTO = dto.CodigoConcepto;
                codigoPersonaMoV.CONTROL_APLICA = dto.ControlAplica;
          

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoPersonaMoV.CODIGO_EMPRESA = conectado.Empresa;
                codigoPersonaMoV.USUARIO_UPD = conectado.Usuario;
                codigoPersonaMoV.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoPersonaMoV);



                var resultDto = await MapPersonasMovControlDto(codigoPersonaMoV);
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

        public async Task<ResultDto<RhPersonasMovControlDeleteDto>> Delete(RhPersonasMovControlDeleteDto dto)
        {

            ResultDto<RhPersonasMovControlDeleteDto> result = new ResultDto<RhPersonasMovControlDeleteDto>(null);
            try
            {

                var personasMovControl = await _repository.GetByCodigo(dto.CodigoPersonaMovCtrl);
                if (personasMovControl == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Concepto no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPersonaMovCtrl);

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

