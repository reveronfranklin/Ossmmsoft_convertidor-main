using System;
using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis
{
	public interface ISisUsuarioServices
	{

      
        Task<ResultLoginDto> Login(LoginDto dto);
        string GetMyName();
        Task<SIS_USUARIOS> GetByLogin(string login);
        Task<ResultDto<SIS_USUARIOS>> Update(SIS_USUARIOS entity);
        string GetToken(SIS_USUARIOS usuario);
    }
}

