using System.Globalization;
using System.Text;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Microsoft.Extensions.Caching.Distributed;

namespace Convertidor.Data.Repository.Rh
{
	public class RhPersonaService: IRhPersonaService
    {
        

   
        private readonly IRhPersonasRepository _repository;
        private readonly IRhHistoricoMovimientoService _historicoMovimientoService;
        private readonly IRhEducacionService _rhEducacionService;
        private readonly IRhDireccionesService _rhDireccionesService;
        private readonly IRhConceptosService _rhConceptosService;
        private readonly ISisUbicacionNacionalRepository _sisUbicacionNacionalRepository;
        private readonly IRhComunicacionesRepository _rhComunicacionessRepository;
        private readonly IRH_HISTORICO_PERSONAL_CARGORepository _rhHistoricoPersonalCargorepository;
        private readonly IRhDescriptivasService _rhDescriptivasServices;
        private readonly IRhAdministrativosService _rhAdministrativosServices;
        private readonly IRhRelacionCargosRepository _rhRelacionCargosRepository;
        private readonly IPreCargosRepository _preCargosRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _preIndiceCatPrg;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;

        public RhPersonaService(IRhPersonasRepository repository,
                                IRhHistoricoMovimientoService historicoMovimientoService,
                                IRhEducacionService rhEducacionService,
                                IRhDireccionesService rhDireccionesService,
                                IRhConceptosService rhConceptosService,
                                ISisUbicacionNacionalRepository sisUbicacionNacionalRepository,
                                IRhComunicacionesRepository rhComunicacionessRepository,
                                IRH_HISTORICO_PERSONAL_CARGORepository rhHistoricoPersonalCargorepository,
                                IRhDescriptivasService rhDescriptivasServices,
                                 IRhAdministrativosService rhAdministrativosServices,
                                IRhRelacionCargosRepository rhRelacionCargosRepository, 
                                IPreCargosRepository preCargosRepository,
                                IPRE_INDICE_CAT_PRGRepository preIndiceCatPrg, 
                                ISisUsuarioRepository sisUsuarioRepository ,
                                    IDistributedCache distributedCache,
                                IConfiguration configuration)
        {
            
            _repository = repository;
            _historicoMovimientoService = historicoMovimientoService;
            _rhEducacionService= rhEducacionService;
            _rhDireccionesService = rhDireccionesService;
            _rhConceptosService = rhConceptosService;
            _sisUbicacionNacionalRepository = sisUbicacionNacionalRepository;
            _rhComunicacionessRepository = rhComunicacionessRepository;
            _rhHistoricoPersonalCargorepository = rhHistoricoPersonalCargorepository;
            _rhDescriptivasServices = rhDescriptivasServices;
            _rhAdministrativosServices = rhAdministrativosServices;
            _rhRelacionCargosRepository = rhRelacionCargosRepository;
            _preCargosRepository = preCargosRepository;
            _preIndiceCatPrg = preIndiceCatPrg;
            _sisUsuarioRepository = sisUsuarioRepository;
            _distributedCache = distributedCache;
            _configuration = configuration;
        }
       
        
        /*public async Task AddRedis(string key, string value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, value,TimeSpan.FromHours(2));
        }
        public void DeleteRedis(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            db.KeyDelete(key);
        }
        public async Task<string> GetRedis(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            //db.KeyDelete("ListProducts");
            return await db.StringGetAsync(key);
        }*/
        public async  Task<ResultDto<List<ListSimplePersonaDto>>> GetAll()
        {
            ResultDto<List<ListSimplePersonaDto>> result = new ResultDto<List<ListSimplePersonaDto>>(null);
            try
            {
                var cacheKey = "GetAllListSimplePersonaDto";
                List<RH_PERSONAS> personas = new List<RH_PERSONAS>();
                List<ListSimplePersonaDto> resultData = new List<ListSimplePersonaDto>();
                //personas = await _repository.GetAll();

                // =await  MapListSimplePersonasDto(personas);
                var listPersonas= await _distributedCache.GetAsync(cacheKey);
                if (listPersonas != null)
                {
                    resultData = System.Text.Json.JsonSerializer.Deserialize<List<ListSimplePersonaDto>> (listPersonas);
                    result.Data =resultData;

                    result.IsValid = true;
                    result.Message = "";
                    return result;
                }
                else
                {
                    personas = await _repository.GetAll();

                    resultData =await  MapListSimplePersonasDto(personas);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddDays(20))
                        .SetSlidingExpiration(TimeSpan.FromDays(1));
                   var serializedList = System.Text.Json.JsonSerializer.Serialize(resultData);
                   var redisListBytes = Encoding.UTF8.GetBytes(serializedList);
                    await _distributedCache.SetAsync(cacheKey,redisListBytes,options);
                    result.Data =resultData;

                    result.IsValid = true;
                    result.Message = "";
                    return result;
                }

               
            }
            catch (Exception ex)
            {
                result.Data =null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }


        public async Task<ResultDto<List<ListSimplePersonaDto>>> GetAllSimple()
        {
            ResultDto<List<ListSimplePersonaDto>> result = new ResultDto<List<ListSimplePersonaDto>>(null);

            try
            {
                
                var cacheKey = "GetAllSimpleListSimplePersonaDto";
                List<RH_PERSONAS> personas = new List<RH_PERSONAS>();
                List<ListSimplePersonaDto> resultData = new List<ListSimplePersonaDto>();
                
                //personas = await _repository.GetAll();

                //result =await MapListSimplePersonasDto(personas);
                
               
                var listPersonas= await _distributedCache.GetAsync(cacheKey);
                
                if (listPersonas != null)
                {
                    resultData = System.Text.Json.JsonSerializer.Deserialize<List<ListSimplePersonaDto>> (listPersonas);
                    result.Data =resultData;

                    result.IsValid = true;
                    result.Message = "";
                    return result;
                }
                else
                {
                    personas = await _repository.GetAll();

                    resultData =await MapListSimplePersonasDto(personas);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddDays(20))
                        .SetSlidingExpiration(TimeSpan.FromDays(19));
                    var serializedList = System.Text.Json.JsonSerializer.Serialize(resultData);
                    var redisListBytes = Encoding.UTF8.GetBytes(serializedList);
                    await _distributedCache.SetAsync(cacheKey,redisListBytes,options);
                    result.Data =resultData;
                    result.IsValid = true;
                    result.Message = "";
                    return result;
                }

              
            }
            catch (Exception ex)
            {
                result.Data =null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
              
            }

        }
        public async Task<PersonasDto> GetPersona(int codigoPersona)
        {
            try
            {
                var personas = await _repository.GetCodigoPersona(codigoPersona);

                var result = await MapObjPersonasDto(personas);


                return (PersonasDto)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ListPersonasDto> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var personas = await _repository.GetCodigoPersona(codigoPersona);

                var result = await MapPersonasDto(personas);


                return (ListPersonasDto)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ListPersonasDto> MapPersonasDto(RH_PERSONAS dtos)
        {
           

                ListPersonasDto itemResult = new ListPersonasDto();



                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.Cedula = dtos.CEDULA;
                itemResult.Nombre = dtos.NOMBRE;
                itemResult.Apellido = dtos.APELLIDO;
                itemResult.Nacionalidad = dtos.NACIONALIDAD;
                itemResult.Sexo = dtos.SEXO;
                itemResult.FechaNacimiento = dtos.FECHA_NACIMIENTO.ToShortDateString();
                itemResult.PaisNacimientoId = dtos.PAIS_NACIMIENTO_ID;
                itemResult.EstadoNacimientoId = dtos.ESTADO_NACIMIENTO_ID;
                itemResult.NumeroGacetaNacional = dtos.NUMERO_GACETA_NACIONAL;
                itemResult.EstadoCivilId = dtos.ESTADO_CIVIL_ID;
                itemResult.Estatura = dtos.ESTATURA;
                itemResult.Peso = dtos.PESO;
                if (string.IsNullOrEmpty(dtos.MANO_HABIL))
                {
                    dtos.MANO_HABIL = "D";
                }
                itemResult.ManoHabil = dtos.MANO_HABIL;
                itemResult.Extra1 = dtos.EXTRA1;
                itemResult.Extra2 = dtos.EXTRA2;
                itemResult.Extra3 = dtos.EXTRA3;
                itemResult.Status = dtos.STATUS;
                itemResult.IdentificacionId = dtos.IDENTIFICACION_ID;
                itemResult.NumeroIdentificacion = dtos.NUMERO_IDENTIFICACION;
                var educacion = await _rhEducacionService.GetByCodigoPersona(dtos.CODIGO_PERSONA);
                if (educacion != null)
                {
                    itemResult.EducacionDto = educacion;
                }

                var direcciones = await _rhDireccionesService.GetByCodigoPersona(dtos.CODIGO_PERSONA);
                if (direcciones != null)
                {
                    itemResult.DireccionesDto = direcciones;
                }
                var historico = await _historicoMovimientoService.GetByCodigoPersona(dtos.CODIGO_PERSONA);
                if(historico!= null)
                {
                    itemResult.HistoricoMovimientoDto = historico;
                }
                var desde = DateTime.Now;
                
                desde = await FechaIngresoTrabajador(dtos.CODIGO_PERSONA);
                var hasta =DateTime.Now;
                var tiempoServicio = TiempoServicio(desde, hasta);
                itemResult.TiempoServicio = tiempoServicio;

            return itemResult;


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
        public async Task<PersonasDto> MapObjPersonasDto(RH_PERSONAS dtos)
        {
               
               PersonasDto itemResult = new PersonasDto();
               if (dtos == null) return itemResult;
               try
               {
                 
                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.Cedula = dtos.CEDULA;
                itemResult.Nombre = dtos.NOMBRE;
                itemResult.Apellido = dtos.APELLIDO;
                itemResult.Nacionalidad = dtos.NACIONALIDAD;
                itemResult.Sexo = dtos.SEXO;
                itemResult.FechaNacimiento = dtos.FECHA_NACIMIENTO; 
                itemResult.FechaNacimientoString = dtos.FECHA_NACIMIENTO.ToString("u");
                FechaDto FechaNacimientoObj = GetFechaDto(dtos.FECHA_NACIMIENTO);
                itemResult.FechaNacimientoObj = (FechaDto)FechaNacimientoObj;
                var desdeEdad = dtos.FECHA_NACIMIENTO;
          
                var hastaEdad = DateTime.Now;
                var edad = TiempoServicio(desdeEdad, hastaEdad);
                itemResult.Edad = edad.CantidadAños;
                itemResult.PaisNacimientoId = dtos.PAIS_NACIMIENTO_ID;
                itemResult.EstadoNacimientoId = dtos.ESTADO_NACIMIENTO_ID;
                itemResult.NumeroGacetaNacional = dtos.NUMERO_GACETA_NACIONAL;
                itemResult.EstadoCivilId = dtos.ESTADO_CIVIL_ID;
                itemResult.DescripcionEstadoCivil = await _rhDescriptivasServices.GetDescripcionByCodigoDescriptiva(dtos.ESTADO_CIVIL_ID);
                itemResult.Estatura = dtos.ESTATURA;
                itemResult.Peso = dtos.PESO;
                if (string.IsNullOrEmpty(dtos.MANO_HABIL))
                {
                    dtos.MANO_HABIL = "D";
                }
                
                itemResult.ManoHabil = dtos.MANO_HABIL;
                itemResult.Extra1 = dtos.EXTRA1;
                itemResult.Extra2 = dtos.EXTRA2;
                itemResult.Extra3 = dtos.EXTRA3;
                itemResult.Status = dtos.STATUS;
                itemResult.IdentificacionId = dtos.IDENTIFICACION_ID;
                itemResult.NumeroIdentificacion = dtos.NUMERO_IDENTIFICACION;
                itemResult.Email = "";

                var pais = await _sisUbicacionNacionalRepository.GetPais(dtos.PAIS_NACIMIENTO_ID);
                if (pais is not null)
                {
                    itemResult.PaisNacimiento = pais.EXTRA1!;
                }
                var comunicaciones = await _rhComunicacionessRepository.GetByCodigoPersona(dtos.CODIGO_PERSONA);
                if (comunicaciones.Count > 0)
                {
                    var email = comunicaciones.Where(x => x.LINEA_COMUNICACION.Contains("@")).FirstOrDefault();
                    if (email is not null) itemResult.Email = email.LINEA_COMUNICACION;


                }
                if (itemResult.Sexo == "F")
                {
                    itemResult.Avatar = "/images/avatars/4.png";
                }
                else
                {
                    itemResult.Avatar = "/images/avatars/1.png";
                }

                itemResult.Avatar = $"/images/avatars/{dtos.CEDULA.ToString()}.jpg";
                var desde = DateTime.Now;
                //var primerMovimiento = await _rhHistoricoPersonalCargorepository.GetPrimerMovimientoByCodigoPersona(dtos.CODIGO_PERSONA);
                //desde = primerMovimiento.FECHA_NOMINA;

                desde = await FechaIngresoTrabajador(dtos.CODIGO_PERSONA);
                var hasta = DateTime.Now;
                var tiempoServicio = TiempoServicio(desde, hasta);
                itemResult.TiempoServicio = tiempoServicio;
                var relacionCargo = await CargoActual(dtos.CODIGO_PERSONA);
                if(relacionCargo is not null)
                {
                    itemResult.Sueldo = relacionCargo.SUELDO;
                    itemResult.CodigoCargo = relacionCargo.CODIGO_CARGO;
                    var cargo = await _preCargosRepository.GetByCodigo(relacionCargo.CODIGO_CARGO);
                    itemResult.DescripcionCargo = cargo.DENOMINACION;
                    var icp = await _preIndiceCatPrg.GetByCodigo(relacionCargo.CODIGO_ICP);
                    if(icp is not null)
                    {
                        itemResult.CodigoIcp = relacionCargo.CODIGO_ICP;
                        itemResult.DescripcionIcp = icp.DENOMINACION;
                    }
                    

                }
      
                return itemResult;
               }
               catch (Exception e)
               {
                   Console.WriteLine(e);
                   var result = dtos;
                   throw;
               }
               
              



        }
        public async Task<DateTime>  FechaIngresoTrabajador(int codigoPersona)
        {
            DateTime result= new DateTime();

            List<DateTime> listFechas = new List<DateTime>();

            var primerMovimiento = await _rhHistoricoPersonalCargorepository.GetPrimerMovimientoByCodigoPersona(codigoPersona);
            if (primerMovimiento is not null)
            {

                listFechas.Add(primerMovimiento.FECHA_NOMINA);
            }

            var administrativos = await _rhAdministrativosServices.GetPrimerMovimientoByCodigoPersona(codigoPersona);
            if (administrativos is not null)
            {
                listFechas.Add(administrativos.FECHA_INGRESO);
            }
          

            if (listFechas.Count > 0)
            {
                result = listFechas.OrderBy(x => x).FirstOrDefault();
                return result;
            }
            else
            {
                result=DateTime.Now;
                return result;
            }

           
         

        }

        public async  Task<List<PersonasDto>> MapListPersonasDto(List<RH_PERSONAS> dtos)
        {
            List<PersonasDto> result = new List<PersonasDto>();

            foreach (var item in dtos)
            {

                PersonasDto itemResult = new PersonasDto();
                itemResult = await MapObjPersonasDto(item);


                result.Add(itemResult);

                
            }
            return result;



        }


        public async Task<List<ListSimplePersonaDto>> MapListSimplePersonasDto(List<RH_PERSONAS> dtos)
        {
            List<ListSimplePersonaDto> result = new List<ListSimplePersonaDto>();

            foreach (var item in dtos)
            {

                ListSimplePersonaDto itemResult = new ListSimplePersonaDto();



                itemResult.CodigoPersona = item.CODIGO_PERSONA;
                itemResult.Cedula = item.CEDULA;
                itemResult.Nombre = item.NOMBRE;
                itemResult.Apellido = item.APELLIDO;
                itemResult.Status = item.STATUS;
                itemResult.Nacionalidad = item.NACIONALIDAD;
                itemResult.Sexo = item.SEXO;
                itemResult.FechaNacimiento = item.FECHA_NACIMIENTO.ToShortDateString();
                itemResult.Email = "";
                
           
                var pais = await _sisUbicacionNacionalRepository.GetPais(item.PAIS_NACIMIENTO_ID);
                if(pais is not null)
                {
                    itemResult.PaisNacimiento = pais.EXTRA1!;
                }
                var comunicaciones = await _rhComunicacionessRepository.GetByCodigoPersona(item.CODIGO_PERSONA);
                if (comunicaciones.Count > 0)
                {
                    var email = comunicaciones.Where(x => x.LINEA_COMUNICACION.Contains("@")).FirstOrDefault();
                    if (email is not null) itemResult.Email = email.LINEA_COMUNICACION;
                

                }


                if (item.SEXO == "F")
                {
                    itemResult.Avatar = "/images/avatars/4.png";    
                }
                else
                {
                    itemResult.Avatar = "/images/avatars/1.png";
                }
                itemResult.Avatar = $"/images/avatars/{item.CEDULA.ToString()}.jpg";

                result.Add(itemResult);


            }
                return result.OrderBy(p=>p.NombreCompleto).ToList();



        }


        public  TiempoServicioResponseDto TiempoServicio(DateTime desde, DateTime hasta)
        {
            TiempoServicioResponseDto result = new TiempoServicioResponseDto();

            var añoDesde = desde.Year;
            var mesDesde = desde.Month;
            var diaDesde = desde.Day;

            var añoHasta = hasta.Year;
            var mesHasta = hasta.Month;
            var diaHasta = hasta.Day;

            var cantidadAños = 0;
            var cantidadMeses = 0;
            var cantidadDias = 0;

            cantidadAños = añoHasta - añoDesde;
            cantidadMeses = mesHasta - mesDesde;
            cantidadDias = diaHasta - diaDesde;


            if (cantidadDias > 28)
            {
                cantidadDias = 0;
                cantidadMeses++;
            }
            else if (cantidadDias < 0)
            {
                cantidadDias = 30 + cantidadDias;
                cantidadMeses--;

            }
            if (cantidadMeses == 12)
            {
                cantidadMeses = 0;
                cantidadAños++;
            }
            else if (cantidadMeses < 0)
            {
                cantidadMeses = 12 + cantidadMeses;
                cantidadAños--;
            }
            
            result.CantidadAños = cantidadAños;
            result.CantidadMeses = cantidadMeses;
            result.CantidadDias = cantidadDias;

            result.FechaDesde = desde;
            result.FechaHasta = hasta;
            result.FechaDesdeString = desde.ToShortDateString();
            result.FechaHastaString = hasta.ToShortDateString();



            return result;


        }

        public async Task<RH_RELACION_CARGOS> CargoActual(int codigoPersona)
        {
            RH_RELACION_CARGOS result;
            result = await _rhRelacionCargosRepository.GetUltimoCargoPorPersona(codigoPersona);
            return result;
        }

        public List<string> GetListNacionalidad()
        {
            List<string> result = new List<string>();
            result.Add("V");
            result.Add("E");
            return result;
        }
        public List<string> GetListSexo()
        {
            List<string> result = new List<string>();
            result.Add("M");
            result.Add("F");
            return result;
        }
        public List<string> GetListStatus()
        {
            List<string> result = new List<string>();
            result.Add("A");
            result.Add("E");
            result.Add("S");
            return result;
        }
        public List<string> GetListManoHabil()
        {
            List<string> result = new List<string>();
            result.Add("D");
            result.Add("I");
          
            return result;
        }
        
        

        public async Task<ResultDto<PersonasDto>> Update(RhPersonaUpdateDto dto)
        {

            ResultDto<PersonasDto> result = new ResultDto<PersonasDto>(null);
            try
            {

                var persona = await _repository.GetCodigoPersona(dto.CodigoPersona);
                if (persona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona No Existe";
                    return result;
                }
                if (dto.Cedula <= 0)
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

                var nacionalidad = GetListNacionalidad().Where(x => x== dto.Nacionalidad).FirstOrDefault();
                if (String.IsNullOrEmpty(nacionalidad))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nacionalidad Invalida";
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
                
                if (dto.Estatura <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estatura Invalida";
                    return result;
                }
                if (dto.Peso <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Peso Invalido";
                    return result;
                }
                var status = GetListStatus().Where(x => x== dto.Status).FirstOrDefault();
                if (String.IsNullOrEmpty(status))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                    
                }
                var manoHabil = GetListManoHabil().Where(x => x == dto.ManoHabil).FirstOrDefault();
                if (String.IsNullOrEmpty(manoHabil))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Mano Habil Invalida";
                    return result;
                    
                }
                var estadoCivil = await _rhDescriptivasServices.GetDescripcionByCodigoDescriptiva(dto.EstadoCivilId);
                if (String.IsNullOrEmpty(estadoCivil))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Civil Invalido";
                    return result;
                    
                }
                var pais = await _sisUbicacionNacionalRepository.GetPais(dto.PaisNacimientoId);
                if (pais is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais Invalido";
                    return result;
                }
                var estado = await _sisUbicacionNacionalRepository.GetEstado(dto.PaisNacimientoId,dto.EstadoNacimientoId);
                if (estado is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Invalido";
                    return result;
                }
                
                persona.CEDULA = dto.Cedula;
                persona.NOMBRE = dto.Nombre;
                persona.APELLIDO = dto.Apellido;
                persona.NACIONALIDAD = dto.Nacionalidad;
                persona.SEXO = dto.Sexo;
                persona.PAIS_NACIMIENTO_ID = dto.PaisNacimientoId;
                persona.ESTADO_NACIMIENTO_ID = dto.EstadoNacimientoId;
                persona.ESTADO_CIVIL_ID = dto.EstadoCivilId;
                persona.ESTATURA = dto.Estatura;
                persona.PESO = dto.Peso;
                persona.MANO_HABIL = dto.ManoHabil;
                //persona.STATUS = dto.Status;
                persona.IDENTIFICACION_ID = dto.IdentificacionId;
                persona.NUMERO_IDENTIFICACION = dto.NumeroIdentificacion;
           
                
                var fechaNacimiento = Convert.ToDateTime(dto.FechaNacimiento, CultureInfo.InvariantCulture);
                persona.FECHA_NACIMIENTO = fechaNacimiento;
                persona.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                persona.CODIGO_EMPRESA = conectado.Empresa;
                persona.USUARIO_UPD = conectado.Usuario;


                await _repository.Update(persona);
                if (dto.Data == "/images/avatars/1.png") dto.Data = "";
                if (dto.Data.Length > 0)
                {
                    dto.Extension = ".JPG";
                
                    dto.NombreArchivo = $@"{persona.CEDULA}" + dto.Extension;
                    var settings = _configuration.GetSection("Settings").Get<Settings>();
                    var ruta = @settings.Images;  
                    dto.Ruta = ruta;
                    //

                    //CREA EL ARCHIVO DE IMAGEN

                    //Convert Base64 Encoded string to Byte Array.
                    var dataArray = dto.Data.Split("/");
                    //string base64 = dto.Data;
                    byte[] imageBytes = Convert.FromBase64String(dto.Data);

                    //Ruta y nombre de la imagen
                    var imageFullName = dto.Ruta + dto.NombreArchivo;
                    //creo el fichero
                    await System.IO.File.WriteAllBytesAsync(imageFullName, imageBytes);
                }
               
 
                var resultDto = await GetPersona(dto.CodigoPersona);
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

        public async Task<ResultDto<PersonasDto>> Create(RhPersonaUpdateDto dto)
        {

            ResultDto<PersonasDto> result = new ResultDto<PersonasDto>(null);
            try
            {
                if (dto.Cedula <= 0)
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

                var nacionalidad = GetListNacionalidad().Where(x => x== dto.Nacionalidad).FirstOrDefault();
                if (String.IsNullOrEmpty(nacionalidad))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nacionalidad Invalida";
                    return result;
                    
                }
                var sexo = GetListSexo().Where(x=> x== dto.Sexo).FirstOrDefault();
                if (String.IsNullOrEmpty(sexo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sexo Invalido";
                    return result;
                    
                }
                
                if (dto.Estatura <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estatura Invalida";
                    return result;
                }
                if (dto.Peso <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Peso Invalido";
                    return result;
                }
                var status = GetListStatus().Where(x => x== dto.Status).FirstOrDefault();
                if (String.IsNullOrEmpty(status))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                    
                }
                var manoHabil = GetListManoHabil().Where(x => x== dto.ManoHabil).FirstOrDefault();
                if (String.IsNullOrEmpty(manoHabil))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Mano Habil Invalida";
                    return result;
                    
                }
                var estadoCivil = await _rhDescriptivasServices.GetDescripcionByCodigoDescriptiva(dto.EstadoCivilId);
                if (String.IsNullOrEmpty(estadoCivil))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Civil Invalido";
                    return result;
                    
                }
                var pais = await _sisUbicacionNacionalRepository.GetPais(dto.PaisNacimientoId);
                if (pais is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais Invalido";
                    return result;
                }
                var estado = await _sisUbicacionNacionalRepository.GetEstado(dto.PaisNacimientoId,dto.EstadoNacimientoId);
                if (estado is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Invalido";
                    return result;
                }
                RH_PERSONAS persona = new RH_PERSONAS();
                persona.CODIGO_PERSONA = await _repository.GetNextKey();
                persona.CEDULA = dto.Cedula;
                persona.NOMBRE = dto.Nombre;
                persona.APELLIDO = dto.Apellido;
                persona.NACIONALIDAD = dto.Nacionalidad;
                persona.SEXO = dto.Sexo;
                persona.PAIS_NACIMIENTO_ID = dto.PaisNacimientoId;
                persona.ESTADO_NACIMIENTO_ID = dto.EstadoNacimientoId;
                persona.ESTADO_CIVIL_ID = dto.EstadoCivilId;
                persona.ESTATURA = dto.Estatura;
                persona.PESO = dto.Peso;
                persona.MANO_HABIL = dto.ManoHabil;
                persona.STATUS = dto.Status;
                persona.IDENTIFICACION_ID = dto.IdentificacionId;
                persona.NUMERO_IDENTIFICACION = dto.NumeroIdentificacion;
           
             
                var fechaNacimiento = Convert.ToDateTime(dto.FechaNacimiento, CultureInfo.InvariantCulture);
                persona.FECHA_NACIMIENTO = fechaNacimiento;
                persona.FECHA_INS = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                persona.CODIGO_EMPRESA = conectado.Empresa;
                persona.USUARIO_INS = conectado.Usuario;


                var created=await _repository.Add(persona);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await GetPersona(dto.CodigoPersona);
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
 
        public async Task<ResultDto<RhPersonaDeleteDto>> Delete(RhPersonaDeleteDto dto)
        {

            ResultDto<RhPersonaDeleteDto> result = new ResultDto<RhPersonaDeleteDto>(null);
            try
            {

                var persona = await _repository.GetCodigoPersona(dto.CodigoPersona);
                if (persona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPersona);

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

