﻿using System;
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
	public class RhTipoNominaService: IRhTipoNominaService
    {
        private readonly IRhTipoNominaRepository _repository;
        
        public RhTipoNominaService(IRhTipoNominaRepository repository)
        {
            _repository = repository;
          
        }
       
        public async Task<List<ListTipoNominaDto>> GetAll()
        {
            try
            {
                var tipoNomina = await _repository.GetAll();

                var result = MapListTipoNominaDto(tipoNomina);


                return (List<ListTipoNominaDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<List<ListTipoNominaDto>> GetTipoNominaByCodigoPersona(int codigoPersona,DateTime desde,DateTime hasta)
        {
            try
            {
                var tipoNomina = await _repository.GetTipoNominaByCodigoPersona(codigoPersona,desde,hasta);

                var result = MapListTipoNominaDto(tipoNomina);


                return (List<ListTipoNominaDto>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }




        public List<ListTipoNominaDto> MapListTipoNominaDto(List<RH_TIPOS_NOMINA> dtos)
        {
            List<ListTipoNominaDto> result = new List<ListTipoNominaDto>();

            foreach (var item in dtos)
            {

                ListTipoNominaDto itemResult = new ListTipoNominaDto();

                

                itemResult.CodigoTipoNomina = item.CODIGO_TIPO_NOMINA;
                itemResult.Descripcion = item.DESCRIPCION;
                result.Add(itemResult);


            }
            return result;



        }

    }
}

