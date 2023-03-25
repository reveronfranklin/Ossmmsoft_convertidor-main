using System;
using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;

namespace Convertidor.Data.Interfaces.Sis
{
	public interface ISisUsuarioRepository
	{

        Task<List<SIS_USUARIOS>> GetALL();
        Task<ResultLoginDto> Login(LoginDto dto);

    }
}

