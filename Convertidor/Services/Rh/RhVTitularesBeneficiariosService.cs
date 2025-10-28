using System.Globalization;
using Convertidor.Data.Repository.Rh;
using Convertidor.Utility;
using Microsoft.AspNetCore.Components.Web.Virtualization;


namespace Convertidor.Services.Adm
{
    
	public class RhVTitularesBeneficiariosService:IRhVTitularesBeneficiariosService
    {

      
        private readonly IRhVTitularesBeneficiariosRepository _repository;
        private readonly IRhPersonaService _personaService;


        public RhVTitularesBeneficiariosService(IRhVTitularesBeneficiariosRepository repository,
                                         IRhPersonaService personaService,
                                        IRhDescriptivaRepository repositoryRhDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                        IRhConceptosRepository rhConceptosRepository)
        {
            _repository = repository;
            _personaService = personaService;
        }


        
       
        public  async Task<RhVTitularBeneficiariosResponseDto> MapRhTitularBeneficiarioDto(RH_V_TITULAR_BENEFICIARIOS dtos)
        {
            RhVTitularBeneficiariosResponseDto itemResult = new RhVTitularBeneficiariosResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }

                if (dtos.CEDULA_TITULAR == "V-26411483")
                {
                    var detener = "";
                }
                itemResult.CedulaTitular = dtos.CEDULA_TITULAR;
                itemResult.CedulaBeneficiario = dtos.CEDULA_BENEFICIARIO;
                itemResult.NombreTituBene = dtos.NOMBRES_TITU_BENE;
                itemResult.ApellidosTituBene = dtos.APELLIDOS_TITU_BENE;
               
                var hasta = DateTime.Now;
                if (dtos.FECHA_NACIMIENTO_FAMILIAR == null)
                {
                    itemResult.FechaNacimientoFamiliarString =""; ;
                    itemResult.FechaNacimientoFamiliarObj = null;
                    itemResult.Edad = "";
                }
                else
                {
                    itemResult.FechaNacimientoFamiliar = (DateTime)dtos.FECHA_NACIMIENTO_FAMILIAR;
                    itemResult.FechaNacimientoFamiliarString =Fecha.GetFechaString((DateTime)dtos.FECHA_NACIMIENTO_FAMILIAR); 
                    FechaDto FechaNacimientoFamiliarObj = Fecha.GetFechaDto((DateTime)dtos.FECHA_NACIMIENTO_FAMILIAR);
                    itemResult.FechaNacimientoFamiliarObj = FechaNacimientoFamiliarObj;
              
                    var edad =_personaService.TiempoServicio((DateTime)dtos.FECHA_NACIMIENTO_FAMILIAR, hasta);
                    itemResult.Edad = $"{edad.CantidadAños} Años {edad.CantidadMeses} meses {edad.CantidadDias} dias";

                }
            
           
                itemResult.Sexo = dtos.SEXO;
                itemResult.EstadoCivil = dtos.ESTADO_CIVIL;
        
                itemResult.CdLocalidad = dtos.CD_LOCALIDAD;
        
                itemResult.CdGrupo = dtos.CD_GRUPO;
        
                itemResult.CdBanco = dtos.CD_BANCO;
        
                itemResult.EstadoCivil = dtos.ESTADO_CIVIL;

                itemResult.NuCuenta = "";
                if (dtos.NU_CUENTA != null)
                {
                    itemResult.NuCuenta = dtos.NU_CUENTA.Replace("'", string.Empty);;
                }
                itemResult.TpCuenta = dtos.TP_CUENTA;
        
                itemResult.DeEmail = dtos.DE_EMAIL;
                itemResult.NroArea = dtos.NRO_AREA;
                itemResult.NroTelefono = dtos.NRO_TELEFONO;
                itemResult.FechaEgreso = dtos.FECHA_EGRESO;
                itemResult.CodigoIcp = dtos.CODIGO_ICP;
                itemResult.Parentesco =dtos.PARENTESCO;
                itemResult.Vinculo = "";
                if (dtos.VINCULO != null)
                {
                    itemResult.Vinculo = dtos.VINCULO;
                }
                itemResult.TipoNomina = dtos.TIPO_NOMINA;
             
                if (dtos.FECHA_INGRESO == null)
                {
                 
                    itemResult.FechaIngresoString =""; 
                    itemResult.FechaIngresoObj=null;
                    itemResult.TiempoServicio = "";

                }
                else
                {
                    itemResult.FechaIngreso = (DateTime)dtos.FECHA_INGRESO;
                    itemResult.FechaIngresoString =Fecha.GetFechaString((DateTime)dtos.FECHA_INGRESO); 
                    itemResult.FechaIngresoObj=Fecha.GetFechaDto((DateTime)dtos.FECHA_INGRESO);
                    var tiempoServicio =_personaService.TiempoServicio((DateTime)dtos.FECHA_INGRESO, hasta);

                    itemResult.TiempoServicio= $"{tiempoServicio.CantidadAños} Años {tiempoServicio.CantidadMeses} meses {tiempoServicio.CantidadDias} dias";

                }
              
                itemResult.UnidadDescripcion = dtos.UNIDAD_ADSCRIPCION;
                itemResult.CargoNominal = dtos.CARGO_NOMINAL;
                itemResult.AntiguedadCmc = dtos.ANTIGUEDAD_CMC;
                itemResult.AntiguedadOtros = dtos.ANTIGUEDAD_OTROS;
                itemResult.SueldoBasico = (decimal)dtos.SUELDO_BASICO;
                itemResult.AnosAntiguedadVaca = (decimal)dtos.ANOS_ANTIGUEDAD_VACA;
                itemResult.DiasAntiguedadVaca = (decimal)dtos.DIAS_ANTIGUEDAD_VACA;
                   itemResult.PeriodosVaca = (string)dtos.PERIODOS_VACA;
                if (dtos.PERIODOS_VACA == null)
                {
                    itemResult.PeriodosVaca = "";
                }
                 itemResult.PeriodosVacappDisfru = (string)dtos.PERIODOS_VACA_PP_DISFRU;
                if (dtos.PERIODOS_VACA_PP_DISFRU == null)
                {
                    itemResult.PeriodosVacappDisfru = "";
                }
                  
             
               

   

         
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<RhVTitularBeneficiariosResponseDto>> MapListRhTitularBeneficiarioDto(List<RH_V_TITULAR_BENEFICIARIOS> dtos)
        {
            List<RhVTitularBeneficiariosResponseDto> result = new List<RhVTitularBeneficiariosResponseDto>();
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
                        var itemResult =  await MapRhTitularBeneficiarioDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
        
        
        public async Task<ResultDto<List<RhVTitularBeneficiariosResponseDto>>> GetAll()
        {
            ResultDto<List<RhVTitularBeneficiariosResponseDto>> result = new ResultDto<List<RhVTitularBeneficiariosResponseDto>>(null);
            try
            {

                var beneficiarios = await _repository.GetAll();
                if (beneficiarios == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }

                var resultDto = await  MapListRhTitularBeneficiarioDto(beneficiarios);
                result.Data = resultDto;
                result.CantidadRegistros = resultDto.Count;
                result.IsValid = true;
                result.Message = "";
                result.Page = 1;
                result.TotalPage=1;
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

        
        public async Task<ResultDto<List<RhVTitularBeneficiariosResponseDto>>> GetByTipoNomina(FilterTipoNomina filter)
        {
            
            ResultDto<List<RhVTitularBeneficiariosResponseDto>> result = new ResultDto<List<RhVTitularBeneficiariosResponseDto>>(null);

            try
            {
           
            
             

                if (filter.CodigoTipoNomina==null || filter.CodigoTipoNomina.Count == 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                List<RhVTitularBeneficiariosResponseDto> resultData = new List<RhVTitularBeneficiariosResponseDto>();
               
                foreach (var item in filter.CodigoTipoNomina)
                {
                    var titularesBeneficiarios = await _repository.GetByTipoNomina(item.CodigoTipoNomina);
                    if (titularesBeneficiarios != null &&  titularesBeneficiarios.Count>0)
                    {
                        
                        var resultDto = await  MapListRhTitularBeneficiarioDto(titularesBeneficiarios);

                        resultData.AddRange(resultDto);
                    }
                   
                }
                int contadorId = 1;
                foreach (var item in resultData)
                {
                    item.Id = contadorId;
                    contadorId++;
                }

              
                result.Data = resultData;
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

