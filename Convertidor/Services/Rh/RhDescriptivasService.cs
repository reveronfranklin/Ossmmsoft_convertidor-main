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
	public class RhDescriptivasService: IRhDescriptivasService
    {
		
        private readonly DataContext _context;



   
        private readonly IRhDescriptivaRepository _repository;

        private readonly IMapper _mapper;

        public RhDescriptivasService(IRhDescriptivaRepository repository)
        {
            _repository = repository;
          
        }
       
        public async Task<string> GetDescripcionByCodigoDescriptiva(int descripcionId)
        {
            try
            {
                var descriptiva = await _repository.GetByCodigoDescriptiva(descripcionId);
                var result = string.Empty;
                if (descriptiva != null)
                {
                    result = descriptiva.DESCRIPCION;
                }


                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_DESCRIPTIVAS>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll();
             


                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

    }
}

