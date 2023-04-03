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

        private readonly IMapper _mapper;

        public RhPersonaService(IRhPersonasRepository repository)
        {
            _repository = repository;
          
        }
       
        public async Task<List<ListPersonasDto>> GetAll()
        {
            try
            {
                var personas = await _repository.GetAll();

                var result = MapListPersonasto(personas);


                return (List<ListPersonasDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

      
      

        public List<ListPersonasDto> MapListPersonasto(List<RH_PERSONAS> dtos)
        {
            List<ListPersonasDto> result = new List<ListPersonasDto>();

            foreach (var item in dtos)
            {

                ListPersonasDto itemResult = new ListPersonasDto();

                

                itemResult.CodigoPersona = item.CODIGO_PERSONA;
                itemResult.Cedula = item.CEDULA;
                itemResult.Nombre = item.NOMBRE;
                itemResult.Apellido = item.APELLIDO;
                itemResult.Nacionalidad = item.NACIONALIDAD;
                itemResult.Sexo = item.SEXO;
                itemResult.FechaNacimiento = item.FECHA_NACIMIENTO;
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

