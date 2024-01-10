using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Sis;

namespace Convertidor.Data.Interfaces.Sis
{
	public interface ISisUsuarioRepository
	{

        Task<List<SIS_USUARIOS>> GetALL();
        Task<ResultLoginDto> Login(LoginDto dto);
        Task<SIS_USUARIOS> GetByLogin(string login);
        Task<ResultDto<SIS_USUARIOS>> Update(SIS_USUARIOS entity);
        string GetToken(SIS_USUARIOS usuario);
        Task<List<UserRole>> GetRolByUserName(string usuario);
        Task<UserConectadoDto> GetConectado();

    }
}

