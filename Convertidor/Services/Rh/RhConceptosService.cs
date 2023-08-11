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
	public class RhConceptosService: IRhConceptosService
    {
		
        private readonly DataContext _context;



   
        private readonly IRhConceptosRepository _repository;

        private readonly IMapper _mapper;

        public RhConceptosService(IRhConceptosRepository repository)
        {
            _repository = repository;
          
        }
       
        public async Task<List<ListConceptosDto>> GetAll()
        {
            try
            {
                var conceptos = await _repository.GetAll();

                var result = MapListConceptosDto(conceptos);


                return (List<ListConceptosDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
      
        public async Task<List<ListConceptosDto>> GetConceptosByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta)
        {
            try
            {
                var conceptos = await _repository.GetConceptosByCodigoPersona(codigoPersona,desde,hasta);

                var result = MapListConceptosDto(conceptos);


                return (List<ListConceptosDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }




        public List<ListConceptosDto> MapListConceptosDto(List<RH_CONCEPTOS> dtos)
        {
            List<ListConceptosDto> result = new List<ListConceptosDto>();

            foreach (var item in dtos)
            {

                ListConceptosDto itemResult = new ListConceptosDto();

                itemResult.CodigoConcepto = item.CODIGO_CONCEPTO;
                itemResult.Codigo = item.CODIGO;
                itemResult.CodigoTipoNomina =item.CODIGO_TIPO_NOMINA;
                itemResult.Denominacion = item.DENOMINACION;

     
             
                result.Add(itemResult);


            }
            return result;



        }

    }
}

