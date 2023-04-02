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
	public class RhPeriodoService: IRhPeriodoService
    {
		
        private readonly DataContext _context;



   
        private readonly IRhPeriodoRepository _repository;

        private readonly IMapper _mapper;

        public RhPeriodoService(IRhPeriodoRepository repository)
        {
            _repository = repository;
          
        }
       
        public async Task<List<RH_PERIODOS>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll();
                return (List<RH_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_PERIODOS>> GetByTipoNomina(int tipoNomina)
        {
            try
            {

                var result = await _repository.GetByTipoNomina(tipoNomina);
                return (List<RH_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<ListPeriodoDto>> GetByYear(int ano)
        {
            try
            {

                var result = await _repository.GetByYear(ano);
                var resultDto = MapListPeriodoDto(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public List<ListPeriodoDto> MapListPeriodoDto(List<RH_PERIODOS> dtos)
        {
            List<ListPeriodoDto> result = new List<ListPeriodoDto>();

            foreach (var item in dtos)
            {

                ListPeriodoDto itemResult = new ListPeriodoDto();
                itemResult.CodigoPeriodo = item.CODIGO_PERIODO;
                itemResult.CodigoTipoNomina = item.CODIGO_TIPO_NOMINA;
                itemResult.FechaNomina = item.FECHA_NOMINA;
                itemResult.Periodo = item.PERIODO;
                itemResult.TipoNomina = item.TIPO_NOMINA;
                result.Add(itemResult);


            }
            return result;



        }

    }
}

