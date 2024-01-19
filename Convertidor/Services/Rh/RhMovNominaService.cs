using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Convertidor.Utility;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using NPOI.SS.Formula.Functions;


namespace Convertidor.Services.Adm
{
    
	public class RhMovNominaService: IRhMovNominaService
    {

      
        private readonly IRhMovNominaRepository _repository;
        private readonly IRhDescriptivaRepository _repositoryRhDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;
        private readonly IRhTipoNominaRepository _rhTipoNominaRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;


        public RhMovNominaService(IRhMovNominaRepository repository,
                                        IRhDescriptivaRepository repositoryRhDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                        IRhConceptosRepository rhConceptosRepository,
                                        IRhTipoNominaRepository rhTipoNominaRepository,
                                        IRhPersonasRepository rhPersonasRepository) 
		{
            _repository = repository;
            _repositoryRhDescriptiva = repositoryRhDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhConceptosRepository = rhConceptosRepository;
            _rhTipoNominaRepository = rhTipoNominaRepository;
            _rhPersonasRepository = rhPersonasRepository;
        }


        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            try
            {
                FechaDesdeObj.Year = fecha.Year.ToString();
                string month = "00" + fecha.Month.ToString();
                string day = "00" + fecha.Day.ToString();
                FechaDesdeObj.Month = month.Substring(month.Length - 2);
                FechaDesdeObj.Day = day.Substring(day.Length - 2);
                return FechaDesdeObj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return FechaDesdeObj;
            }
           
         
    
          
        }
       
        public List<ListTipoMovimiento> GetListTipo()
        {
            List<ListTipoMovimiento> result = new List<ListTipoMovimiento>();
            ListTipoMovimiento normal = new ListTipoMovimiento();
            normal.Codigo = "F";
            normal.Decripcion = "Fijo";
            ListTipoMovimiento especial = new ListTipoMovimiento();
            especial.Codigo = "V";
            especial.Decripcion = "Variable";
            result.Add(normal);
            result.Add(especial);
          
            return result;
        }
        public  async Task<RhMovNominaResponseDto> MapRhMovNominaDto(RH_MOV_NOMINA dtos)
        {
            RhMovNominaResponseDto itemResult = new RhMovNominaResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                
               itemResult.CodigoMovNomina = dtos.CODIGO_MOV_NOMINA;
               itemResult.CodigoTipoNomina = dtos.CODIGO_TIPO_NOMINA;
               itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
               itemResult.CodigoConcepto = dtos.CODIGO_CONCEPTO;
               itemResult.DescripcionConcepto = "";
               var concepto = await _rhConceptosRepository.GetByCodigo(dtos.CODIGO_CONCEPTO);
               if (concepto != null)
               {
                   itemResult.DescripcionConcepto = concepto.DENOMINACION;
               }
               itemResult.ComplementoConcepto = dtos.COMPLEMENTO_CONCEPTO;
               itemResult.Tipo = dtos.TIPO;
               itemResult.DescripcionTipo = "";
               var tipo = GetListTipo().Where(x => x.Codigo == dtos.TIPO).FirstOrDefault();
               if (tipo != null)
               {
                   itemResult.DescripcionTipo =tipo.Decripcion;
               }
               itemResult.FrecuenciaId = dtos.FRECUENCIA_ID;
               itemResult.DescripcionFrecuencia = "";
               var frecuencia = await _repositoryRhDescriptiva.GetByCodigoDescriptiva(dtos.FRECUENCIA_ID);
               if (frecuencia != null)
               {
                   itemResult.DescripcionFrecuencia = frecuencia.DESCRIPCION;
               }
               itemResult.Monto = dtos.MONTO;
               itemResult.OssMonto = dtos.OSS_MONTO;
              
           
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<RhMovNominaResponseDto>> MapListMovNominaDto(List<RH_MOV_NOMINA> dtos)
        {
            List<RhMovNominaResponseDto> result = new List<RhMovNominaResponseDto>();
            if (dtos.Count > 0)
            {
                foreach (var item in dtos)
                {
                    if (item == null)
                    {
                        var detener = "";
                    }
                    else
                    {
                        var itemResult =  await MapRhMovNominaDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }

        public async Task<ResultDto<RhMovNominaResponseDto?>> UpdateCalculo(int codigoMovNomina,int calculoId,decimal monto)
        {

            ResultDto<RhMovNominaResponseDto?> result = new ResultDto<RhMovNominaResponseDto?>(null);
            try
            {

                var movNomina = await _repository.GetByCodigo(codigoMovNomina);
                if (movNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Movimiento Nomina no existe";
                    return result;
                }
                
           
                movNomina.CALCULO_ID = calculoId;
                movNomina.OSS_MONTO = monto;
                movNomina.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                movNomina.CODIGO_EMPRESA = conectado.Empresa;
                movNomina.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(movNomina);
                var resultDto = await  MapRhMovNominaDto(movNomina);
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
           
        public async Task<ResultDto<RhMovNominaResponseDto?>> Update(RhMovNominaUpdateDto dto)
        {

            ResultDto<RhMovNominaResponseDto?> result = new ResultDto<RhMovNominaResponseDto?>(null);
            try
            {

                var movNomina = await _repository.GetByCodigo(dto.CodigoMovNomina);
                if (movNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Movimiento Nomina no existe";
                    return result;
                }
                
                var concepto = await _rhConceptosRepository.GetByCodigo(dto.CodigoConcepto);
                if (concepto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto no existe";
                    return result;
                }

                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.CodigoTipoNomina);
                if (tipoNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Nomina no existe";
                    return result;
                }

                var persona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (persona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona no existe";
                    return result;
                }
                var tipo = GetListTipo().Where(x => x.Codigo == dto.Tipo).FirstOrDefault();
                if (tipo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Movimiento no existe";
                    return result;
                }
                var frecuencia = await _repositoryRhDescriptiva.GetByCodigoDescriptiva(dto.FrecuenciaId);
                if (frecuencia == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia no existe";
                    return result;
                }
                if (dto.Monto == 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Mono Invalido";
                    return result;
                }

                movNomina.CODIGO_PERSONA = dto.CodigoPersona;
                movNomina.CODIGO_CONCEPTO = dto.CodigoConcepto;
                movNomina.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                movNomina.FRECUENCIA_ID = dto.FrecuenciaId;
                movNomina.COMPLEMENTO_CONCEPTO = dto.ComplementoConcepto;
                movNomina.MONTO = dto.Monto;
                movNomina.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                movNomina.CODIGO_EMPRESA = conectado.Empresa;
                movNomina.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(movNomina);
                var resultDto = await  MapRhMovNominaDto(movNomina);
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

        public async Task<ResultDto<RhMovNominaResponseDto>> Create(RhMovNominaUpdateDto dto)
        {

            ResultDto<RhMovNominaResponseDto> result = new ResultDto<RhMovNominaResponseDto>(null);
            try
            {
               
             

                var concepto = await _rhConceptosRepository.GetByCodigo(dto.CodigoConcepto);
                if (concepto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto no existe";
                    return result;
                }

                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.CodigoTipoNomina);
                if (tipoNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Nomina no existe";
                    return result;
                }

                var persona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (persona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona no existe";
                    return result;
                }
                var tipo = GetListTipo().Where(x => x.Codigo == dto.Tipo).FirstOrDefault();
                if (tipo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Movimiento no existe";
                    return result;
                }
                var frecuencia = await _repositoryRhDescriptiva.GetByCodigoDescriptiva(dto.FrecuenciaId);
                if (frecuencia == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia no existe";
                    return result;
                }
                if (dto.Monto == 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Mono Invalido";
                    return result;
                }
                
                RH_MOV_NOMINA entity = new RH_MOV_NOMINA();
             
                entity.CODIGO_MOV_NOMINA = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.CODIGO_CONCEPTO = dto.CodigoConcepto;
                entity.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                entity.FRECUENCIA_ID = dto.FrecuenciaId;
                entity.COMPLEMENTO_CONCEPTO = dto.ComplementoConcepto;
                entity.MONTO = dto.Monto;
                
                entity.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRhMovNominaDto(created.Data);
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
 
        public async Task<ResultDto<RhMovNominaDeleteDto>> Delete(RhMovNominaDeleteDto dto)
        {

            ResultDto<RhMovNominaDeleteDto> result = new ResultDto<RhMovNominaDeleteDto>(null);
            try
            {

                var movNomina = await _repository.GetByCodigo(dto.CodigoMovNomina);
                if (movNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Movimiento no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoMovNomina);

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

        public async Task<ResultDto<RhMovNominaResponseDto>> GetByCodigo(RhMovNominaFilterDto dto)
        { 
            ResultDto<RhMovNominaResponseDto> result = new ResultDto<RhMovNominaResponseDto>(null);
            try
            {

                var movNomina = await _repository.GetByCodigo(dto.CodigoMovNomina);
                if (movNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Movimiento no existe";
                    return result;
                }
                
                var resultDto =  await MapRhMovNominaDto(movNomina);
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
        public async Task<ResultDto<RhMovNominaResponseDto>>GetByTipoNominaPersonaConcepto(RhMovNominaFilterDto dto)
        {
            ResultDto<RhMovNominaResponseDto> result = new ResultDto<RhMovNominaResponseDto>(null);
            try
            {
                var movNomina = await _repository.GetByTipoNominaPersonaConccepto(dto);
                if (movNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "NO DATA";
                    return result;
                }
                
                var resultDto =  await MapRhMovNominaDto(movNomina);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<List<RhMovNominaResponseDto>>> GetAllByPersona(RhMovNominaFilterDto dto)
        {
            ResultDto<List<RhMovNominaResponseDto>> result = new ResultDto<List<RhMovNominaResponseDto>>(null);
            try
            {

                var movNomina = await _repository.GetByPersona(dto.CodigoPersona);
                if (movNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListMovNominaDto(movNomina);
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
        
        public async Task<ResultDto<List<RhMovNominaResponseDto>>> GetAll()
        {
            ResultDto<List<RhMovNominaResponseDto>> result = new ResultDto<List<RhMovNominaResponseDto>>(null);
            try
            {

                var conceptoAcumula = await _repository.GetAll();
                if (conceptoAcumula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListMovNominaDto(conceptoAcumula);
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

