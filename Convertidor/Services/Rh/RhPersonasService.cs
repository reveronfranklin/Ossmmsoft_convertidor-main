using System;
using System.Collections.Generic;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhPersonaService: IRhPersonaService
    {
		
        private readonly DataContext _context;



   
        private readonly IRhPersonasRepository _repository;
        private readonly IRhHistoricoMovimientoService _historicoMovimientoService;
        private readonly IRhEducacionService _rhEducacionService;
        private readonly IRhDireccionesService _rhDireccionesService;
        private readonly IMapper _mapper;

        public RhPersonaService(IRhPersonasRepository repository,
                                IRhHistoricoMovimientoService historicoMovimientoService,
                                IRhEducacionService rhEducacionService,
                                IRhDireccionesService rhDireccionesService)
        {
            _repository = repository;
            _historicoMovimientoService = historicoMovimientoService;
            _rhEducacionService= rhEducacionService;
            _rhDireccionesService = rhDireccionesService;


        }
       
        public async Task<List<PersonasDto>> GetAll()
        {
            try
            {
                var personas = await _repository.GetAll();

                var result =await  MapListPersonasDto(personas);


                return (List<PersonasDto>)result;
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
                var personas = await _repository.GetCodogoPersona(codigoPersona);

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
           

            return itemResult;







        }


        public async Task<List<PersonasDto>> MapListPersonasDto(List<RH_PERSONAS> dtos)
        {
            List<PersonasDto> result = new List<PersonasDto>();

            foreach (var item in dtos)
            {

                PersonasDto itemResult = new PersonasDto();

                

                itemResult.CodigoPersona = item.CODIGO_PERSONA;
                itemResult.Cedula = item.CEDULA;
                itemResult.Nombre = item.NOMBRE;
                itemResult.Apellido = item.APELLIDO;
                itemResult.Nacionalidad = item.NACIONALIDAD;
                itemResult.Sexo = item.SEXO;
                itemResult.FechaNacimiento = item.FECHA_NACIMIENTO.ToShortDateString();
                itemResult.PaisNacimientoId = item.PAIS_NACIMIENTO_ID;
                itemResult.EstadoNacimientoId = item.ESTADO_NACIMIENTO_ID;
                itemResult.NumeroGacetaNacional = item.NUMERO_GACETA_NACIONAL;
                itemResult.EstadoCivilId = item.ESTADO_CIVIL_ID;
                itemResult.Estatura = item.ESTATURA;
                itemResult.Peso = item.PESO;
                itemResult.ManoHabil = item.MANO_HABIL;
                itemResult.Extra1 = item.EXTRA1;
                itemResult.Extra2 = item.EXTRA2;
                itemResult.Extra3 = item.EXTRA3;
                itemResult.Status = item.STATUS;
                itemResult.IdentificacionId = item.IDENTIFICACION_ID;
                itemResult.NumeroIdentificacion = item.NUMERO_IDENTIFICACION;
               

                result.Add(itemResult);


            }
            return result;



        }

    }
}

