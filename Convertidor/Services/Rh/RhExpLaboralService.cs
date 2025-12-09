using Convertidor.Utility;

namespace Convertidor.Data.Repository.Rh
{
	public class RhExpLaboralService : IRhExpLaboralService
    {
        
   
        private readonly IRhExpLaboralRepository _repository;

        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
  

        public RhExpLaboralService(IRhExpLaboralRepository repository, 
                           
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IRhPersonasRepository rhPersonasRepository
                                        )
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
        }
       
       
        
       

        public async Task<List<RhExpLaboralResponseDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {

                var ExpLaboral = await _repository.GetByCodigoPersona(codigoPersona);

                var result = await MapListRhExpLaboralDto(ExpLaboral);


                return (List<RhExpLaboralResponseDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        public async  Task<RhExpLaboralResponseDto> MapRhExpLaboralDto(RH_EXP_LABORAL dtos)
        {


                RhExpLaboralResponseDto itemResult = new RhExpLaboralResponseDto();
                itemResult.CodigoExpLaboral = dtos.CODIGO_EXP_LABORAL;
                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.NombreEmpresa = dtos.NOMBRE_EMPRESA;
                itemResult.TipoEmpresa = dtos.TIPO_EMPRESA;
            
                itemResult.Cargo = dtos.CARGO;
                itemResult.FechaDesde = dtos.FECHA_DESDE;
                itemResult.FechaHasta = dtos.FECHA_HASTA;
                itemResult.FechaDesdeString = Fecha.GetFechaString( dtos.FECHA_DESDE); 
                itemResult.FechaHastaString = Fecha.GetFechaString( dtos.FECHA_HASTA); 
                FechaDto FechaDesdeObj = Fecha.GetFechaDto(dtos.FECHA_DESDE);
                itemResult.FechaDesdeObj = (FechaDto)FechaDesdeObj;
                FechaDto FechaHastaObj =Fecha.GetFechaDto(dtos.FECHA_HASTA);
                itemResult.FechaHastaObj = FechaHastaObj;
                itemResult.UltimoSueldo = dtos.ULTIMO_SUELDO;
                if(dtos.SUPERVISOR == null)  dtos.SUPERVISOR = "";
                itemResult.Supervisor = dtos.SUPERVISOR;
                if (dtos.CARGO_SUPERVISOR == null) dtos.CARGO_SUPERVISOR = "";
                itemResult.CargoSupervisor = dtos.CARGO_SUPERVISOR;
                if(dtos.TELEFONO==null) dtos.TELEFONO="";
                itemResult.Telefono = dtos.TELEFONO;
                if (dtos.SUPERVISOR == null) dtos.SUPERVISOR = "";
                itemResult.Supervisor = dtos.SUPERVISOR;
                if (dtos.DESCRIPCION == null) dtos.DESCRIPCION = "";
                itemResult.Descripcion = dtos.DESCRIPCION;
             


            return itemResult;

        }

        public async Task<List<RhExpLaboralResponseDto>> MapListRhExpLaboralDto(List<RH_EXP_LABORAL> dtos)
        {
            List<RhExpLaboralResponseDto> result = new List<RhExpLaboralResponseDto>();


            foreach (var item in dtos)
            {

                RhExpLaboralResponseDto itemResult = new RhExpLaboralResponseDto();

                itemResult = await MapRhExpLaboralDto(item);

                result.Add(itemResult);
            }
            return result;



        }
        public static bool ValidarUnSoloGyP(string texto)
        {
            return texto?.Length == 1 && (texto[0] == 'G' || texto[0] == 'P');
        }

        public async Task<ResultDto<RhExpLaboralResponseDto>> Validate(RhExpLaboralUpdateDto dto)
        {

              ResultDto<RhExpLaboralResponseDto> result = new ResultDto<RhExpLaboralResponseDto>(null);
              var codigoPersona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (codigoPersona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo persona no existe";
                    return result;
                }
                if (dto.NombreEmpresa is not null && dto.NombreEmpresa.Length>50)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Empresa Invalido";
                    return result;
                }

                var valido=ValidarUnSoloGyP(dto.TipoEmpresa);
                if (!valido)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Empresa Invalido, debe ser G o P";
                    return result;
                }
               
                if (string.IsNullOrEmpty(dto.Cargo) || dto.Cargo.Length>50)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo Invalido";
                    return result;
                }

                if (!DateValidate.IsDate(dto.FechaDesde.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial Invalida";
                    result.LinkData = "";
                    return result;
                }
                if (!DateValidate.IsDate(dto.FechaHasta.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Final Invalida";
                    result.LinkData = "";
                    return result;
                }

                if (dto.UltimoSueldo == null)
                {
                    dto.UltimoSueldo = 0;
                }
                if (dto.Supervisor is not null && dto.Supervisor.Length > 50)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Supervisor Invalido";
                    return result;
                }
                if (dto.CargoSupervisor is not null && dto.CargoSupervisor.Length>50)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo Supervisor Invalido";
                    return result;
                }
                if (dto.Telefono is not null && dto.Telefono.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Telefono Invalido";
                    return result;
                }
                if (dto.Descripcion is not null && dto.Descripcion.Length>1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }
                 result.Data = null;
                 result.IsValid = true;
                 result.Message = "";
                return result;

        }

        public async Task<ResultDto<RhExpLaboralResponseDto>> Update(RhExpLaboralUpdateDto dto)
        {

            ResultDto<RhExpLaboralResponseDto> result = new ResultDto<RhExpLaboralResponseDto>(null);
            try
            {
                var codigoExpLaboral = await _repository.GetByCodigo(dto.CodigoExpLaboral);
                if (codigoExpLaboral == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo exp laboral no existe";
                    return result;
                }

                var validateDto =await Validate(dto);
                if (validateDto.IsValid == false)
                {
                    return validateDto;
                }
                codigoExpLaboral.CODIGO_EXP_LABORAL = dto.CodigoExpLaboral;
                codigoExpLaboral.NOMBRE_EMPRESA = dto.NombreEmpresa;
                codigoExpLaboral.TIPO_EMPRESA = dto.TipoEmpresa;
                codigoExpLaboral.RAMO_ID = dto.RamoId;
                codigoExpLaboral.CARGO = dto.Cargo;
                codigoExpLaboral.FECHA_DESDE = dto.FechaDesde;
                codigoExpLaboral.FECHA_HASTA = dto.FechaHasta;
                codigoExpLaboral.ULTIMO_SUELDO = dto.UltimoSueldo;
                codigoExpLaboral.SUPERVISOR = dto.Supervisor;
                codigoExpLaboral.CARGO_SUPERVISOR = dto.CargoSupervisor;
                codigoExpLaboral.TELEFONO = dto.Telefono;
                codigoExpLaboral.DESCRIPCION = dto.Descripcion;



                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoExpLaboral.CODIGO_EMPRESA = conectado.Empresa;
                codigoExpLaboral.USUARIO_UPD = conectado.Usuario.ToString();
                codigoExpLaboral.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoExpLaboral);



                var resultDto = await MapRhExpLaboralDto(codigoExpLaboral);
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

        public async Task<ResultDto<RhExpLaboralResponseDto>> Create(RhExpLaboralUpdateDto dto)
        {

            ResultDto<RhExpLaboralResponseDto> result = new ResultDto<RhExpLaboralResponseDto>(null);
            try
            {
                var codigoExpLaboral = await _repository.GetByCodigo(dto.CodigoExpLaboral);
                if(codigoExpLaboral != null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe el Codigo ExpLaboral";
                    return result;
                }
                var validateDto =await Validate(dto);
                if (validateDto.IsValid == false)
                {
                    return validateDto;
                }


                RH_EXP_LABORAL entity = new RH_EXP_LABORAL();
                entity.CODIGO_EXP_LABORAL = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.NOMBRE_EMPRESA = dto.NombreEmpresa;
                entity.TIPO_EMPRESA = dto.TipoEmpresa;
                entity.RAMO_ID = dto.RamoId;
                entity.CARGO = dto.Cargo;
                entity.FECHA_DESDE = dto.FechaDesde;
                entity.FECHA_HASTA = dto.FechaHasta;
                entity.ULTIMO_SUELDO = dto.UltimoSueldo;
                entity.SUPERVISOR = dto.Supervisor;
                entity.CARGO_SUPERVISOR = dto.CargoSupervisor;
                entity.TELEFONO = dto.Telefono;
                entity.DESCRIPCION = dto.Descripcion;
            



                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario.ToString();
                entity.FECHA_INS = DateTime.Now;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRhExpLaboralDto(created.Data);
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
 
        public async Task<ResultDto<RhExpLaboralDeleteDto>> Delete(RhExpLaboralDeleteDto dto)
        {

            ResultDto<RhExpLaboralDeleteDto> result = new ResultDto<RhExpLaboralDeleteDto>(null);
            try
            {

                var codigoExpLaboral = await _repository.GetByCodigo(dto.CodigoExpLaboral);
                if (codigoExpLaboral == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Explaboral no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoExpLaboral);

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

