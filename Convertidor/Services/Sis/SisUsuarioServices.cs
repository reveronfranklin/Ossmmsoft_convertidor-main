using System;
using AutoMapper;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos.Sis;
using NuGet.Protocol.Core.Types;

namespace Convertidor.Services.Sis
{
	public class SisUsuarioServices: ISisUsuarioServices
    {
		
        private readonly ISisUsuarioRepository _repository;
      

        private readonly IMapper _mapper;

        public SisUsuarioServices(ISisUsuarioRepository repository,
                                 

                                      IMapper mapper)
        {
            _repository = repository;
           

            _mapper = mapper;
        }

        public async Task<ResultLoginDto> Login(LoginDto dto)
        {
            var result = await _repository.Login(dto);
            return result;
        }


    }
}

