﻿using System.Globalization;
using System.Text;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Utility;
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

        public async Task<ResultDto<List<ListSimplePersonaDto>>> AddPersonaCache(int codigoPersona)
        {
            List<ListSimplePersonaDto> resultData = new List<ListSimplePersonaDto>();
            try
            {
               
                var persona = await GetPersona(codigoPersona);
                if (persona != null)
                {
                    var cacheKey = "GetAllListSimplePersonaDto";
                    var listPersonas= await _distributedCache.GetAsync(cacheKey);
                    if (listPersonas != null)
                    {
                        resultData = System.Text.Json.JsonSerializer.Deserialize<List<ListSimplePersonaDto>>(listPersonas);
                        var personaFind = resultData.Where(x => x.CodigoPersona == codigoPersona).FirstOrDefault();
                        if (personaFind == null)
                        {
                            resultData.Add(personaFind);
                            var options = new DistributedCacheEntryOptions()
                                .SetAbsoluteExpiration(DateTime.Now.AddDays(20))
                                .SetSlidingExpiration(TimeSpan.FromDays(10));
                            var serializedList = System.Text.Json.JsonSerializer.Serialize(resultData);
                            var redisListBytes = Encoding.UTF8.GetBytes(serializedList);
                            await _distributedCache.SetAsync(cacheKey,redisListBytes,options);
                        }
                    }
                
                }

                var result = await GetAll();
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
         
        }
      
         public async  Task<ResultDto<List<ListSimplePersonaDto>>> GetAll()
        {
            ResultDto<List<ListSimplePersonaDto>> result = new ResultDto<List<ListSimplePersonaDto>>(null);
            try
            {
                
                var personas = await _repository.GetAll();
                var resultData =await MapListSimplePersonasDto(personas);
                result.Data =resultData;
                result.IsValid = true;
                result.Message = "";
                return result;
               
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
                
                var personas = await _repository.GetAll();
                var resultData =await MapListSimplePersonasDto(personas);
                result.Data =resultData;
                result.IsValid = true;
                result.Message = "";
                return result;
              
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
     
        public async Task<PersonasDto> MapObjPersonasDto(RH_PERSONAS dtos)
        {
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.IMagesFront;
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
                itemResult.FechaNacimiento =dtos.FECHA_NACIMIENTO; 
                itemResult.FechaNacimientoString = Fecha.GetFechaString(dtos.FECHA_NACIMIENTO);
                FechaDto FechaNacimientoObj = Fecha.GetFechaDto(dtos.FECHA_NACIMIENTO);
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

                if (dtos.FILE_NAME == null)
                {
                    dtos.FILE_NAME = "";
                    if (itemResult.Sexo == "F")
                    {
                        itemResult.Avatar = $"{destino}4.png";
                    }
                    else
                    {
                        itemResult.Avatar = $"{destino}1.png";
                    }
                }
                else
                {
                    itemResult.Avatar = $"{destino}{dtos.FILE_NAME.ToString()}";
                }
                
           
                
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
                    itemResult.DescripcionCargo = "";
                    var cargo = await _preCargosRepository.GetByCodigo(relacionCargo.CODIGO_CARGO);
                    if (cargo != null)
                    {
                        itemResult.DescripcionCargo = cargo.DENOMINACION;
                    }
                    
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

        public string GetPais(List<SIS_UBICACION_NACIONAL> list,int paisId)
        {
            var result = "";
            var pais = list.Where(x => x.PAIS == paisId).FirstOrDefault();
            if (pais != null)
            {
                result = pais.EXTRA1!;
            }
            return result;


        }
        public string GetEmail(List<RH_COMUNICACIONES> list,int id)
        {
            var result = "";
            var comunicacion = list.Where(x => x.CODIGO_PERSONA==id && x.LINEA_COMUNICACION.Contains("@")).FirstOrDefault();;
            if (comunicacion != null)
            {
                result = comunicacion.LINEA_COMUNICACION!;
            }
            return result;


        }
        public string GetFileName(string filename,string sexo,string destino)
        {
            var result = "";
            if (String.IsNullOrEmpty(filename))
            {
                if (sexo == "F")
                {
                    result = $"{destino}4.png";    
                }
                else
                {
                    result = $"{destino}1.png";
                }
            }
            else
            {
                result = $"{destino}{filename.ToString()}";
            }

            return result;


        }
        public async Task<List<ListSimplePersonaDto>> MapListSimplePersonasDto(List<RH_PERSONAS> dtos)
        {
            
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.IMagesFront; 
            List<ListSimplePersonaDto> result = new List<ListSimplePersonaDto>();

            var paises = await _sisUbicacionNacionalRepository.GetPaises();
            var comunicaciones = await _rhComunicacionessRepository.GetAll();
            foreach (var item in dtos)
            {

                ListSimplePersonaDto itemResult = new ListSimplePersonaDto();

                if (item.CODIGO_PERSONA == null)
                {
                    var detener = 1;
                }

                itemResult.CodigoPersona = item.CODIGO_PERSONA;
                itemResult.Cedula = item.CEDULA;
                itemResult.Nombre = item.NOMBRE;
                itemResult.Apellido = item.APELLIDO;
                itemResult.Status = item.STATUS;
                itemResult.Nacionalidad = item.NACIONALIDAD;
                itemResult.Sexo = item.SEXO;
                itemResult.FechaNacimiento = FechaObj.GetFechaString(item.FECHA_NACIMIENTO);
                itemResult.Email = "";
                itemResult.PaisNacimiento = GetPais(paises, item.PAIS_NACIMIENTO_ID);
                itemResult.Email = GetEmail(comunicaciones, item.CODIGO_PERSONA);
                itemResult.Avatar = GetFileName(item.FILE_NAME, item.SEXO, destino);
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
            result.FechaDesdeString =FechaObj.GetFechaString(desde) ;
            result.FechaHastaString = FechaObj.GetFechaString(hasta);



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
        
       
        
    public async Task<ResultDto<PersonasDto>> AddImage(int codigoPersona,List<IFormFile> files)
        {
        
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.Images; 

            ResultDto<PersonasDto> result = new ResultDto<PersonasDto>(null);
           
            var persona = await GetPersona(codigoPersona);
            if (persona == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Persona No existe";
                return result;
            }
          
       
            try
            {
             
              
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        string fileName = file.FileName;
                        fileName =file.FileName.Replace(" ", "_");
                        var arrFileName = fileName.Split(".");
                        Guid uuid = Guid.NewGuid();
                        var filePatch = $"{destino}{persona.Cedula}_{uuid.ToString()}.{arrFileName[1]}";
                        var personaUpdate = await _repository.GetCodigoPersona(persona.CodigoPersona);
                        if (personaUpdate != null)
                        {
                            personaUpdate.FILE_NAME= $"{persona.Cedula}_{uuid.ToString()}.{arrFileName[1]}";
                            await _repository.Update(personaUpdate);
                        }
                        
                        using (var stream =System.IO.File.Create(filePatch) )
                        {
                            await  file.CopyToAsync(stream);
                        }
                      
                       
                    }
                }

                result.Data = persona;
                result.IsValid = true;
                result.Message = "";
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
                /*var status = GetListStatus().Where(x => x== dto.Status).FirstOrDefault();
                if (String.IsNullOrEmpty(status))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                    
                }*/
                var manoHabil = GetListManoHabil().Where(x => x == dto.ManoHabil).FirstOrDefault();
                if (String.IsNullOrEmpty(manoHabil))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Mano Habil Invalida";
                    return result;

                }
                var identificacion = await _rhDescriptivasServices.GetByCodigoDescriptiva(dto.IdentificacionId);
                if (identificacion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Identificacion Invalido";
                    return result;
                    
                }

                if (dto.NumeroIdentificacion<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero de Identificacion Invalido";
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
           
              
                string formato = "dd/MM/yyyy"; // Ajusta este formato a tus necesidades
             
             
                DateTime fechaConvertida = DateTime.ParseExact( dto.FechaNacimiento, formato, CultureInfo.InvariantCulture);
                persona.FECHA_NACIMIENTO = fechaConvertida;
                persona.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                persona.CODIGO_EMPRESA = conectado.Empresa;
                persona.USUARIO_UPD = conectado.Usuario;


                await _repository.Update(persona);
             
               
 
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
                
                var personaFind = await _repository.GetCedula(dto.Cedula);
                if (personaFind != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cedula de Identidad ya existe";
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
                /*var status = GetListStatus().Where(x => x== dto.Status).FirstOrDefault();
                if (String.IsNullOrEmpty(status))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                    
                }*/
                var identificacion = await _rhDescriptivasServices.GetByCodigoDescriptiva(dto.IdentificacionId);
                if (identificacion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Identificacion Invalido";
                    return result;
                    
                }

                if (dto.NumeroIdentificacion<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero de Identificacion Invalido";
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
                persona.STATUS = "A";//dto.Status;
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
                    var resultDto = await GetPersona(persona.CODIGO_PERSONA );
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
        
        public string[] GetFicheros(string ruta)
        {

            string[] ficheros = Directory.GetFiles(ruta);
            string[] sorted = ficheros.OrderByDescending(o => o).ToArray();
            return sorted;
        }
        
        public  int ConvertStringToInt(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            else
            {
                return 0; // Retorna null si la conversión falla
            }
        }

        public async Task<string> CopiarArchivos()
        {

            string text1 = "";
            try
            {

                string outFileName = @"";

                var _env = "development";
                var settings = _configuration.GetSection("Settings").Get<Settings>();
                var destino = @settings.Images;
                var origen = @settings.RhFilesProceso;
                var separadorPatch = @settings.SeparatorPatch;

                var ficheros = GetFicheros(origen);
                foreach (string file in ficheros)
                {
                    var srcFileArr = file.Split(separadorPatch);
                    var fileName = srcFileArr[srcFileArr.Length - 1];
                    var controlArray = fileName.Split("_");
                    var controlFinalArray = controlArray[controlArray.Length - 1].Split(".");
           
                    var cedula = ConvertStringToInt(controlFinalArray[0]);
                

                    var persona = await _repository.GetCedula(cedula);
                    if (persona != null)
                    {
                     
                        File.Delete($"{destino}{separadorPatch}{fileName}");
                       
                        if (!File.Exists($"{destino}{separadorPatch}{fileName}"))
                        {

                            File.Copy(file, $"{destino}{separadorPatch}{fileName}");
                            if (File.Exists($"{destino}{separadorPatch}{fileName}"))
                            {
                                persona.FILE_NAME = fileName;

                                await _repository.Update(persona);
                                File.Delete($"{origen}{separadorPatch}{fileName}");
                            }
                        }

                    }else{
                        File.Delete($"{origen}{separadorPatch}{fileName}");
                    }


                }
                
                



                return text1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return text1;
            }


        }


        
    }
}

