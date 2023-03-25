using System;
using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis
{
	public interface ISisUsuarioServices
	{

      
        Task<ResultLoginDto> Login(LoginDto dto);

    }
}

