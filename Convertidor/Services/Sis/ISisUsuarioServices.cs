using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis
{
	public interface ISisUsuarioServices
	{

      
        Task<ResultLoginDto> Login(LoginDto dto);
        string GetMyName();
        Task<SIS_USUARIOS> GetByLogin(string login);
        string GetToken(SIS_USUARIOS usuario);
        string GetMenuPre();
        string GetMenuDeveloper();
        string GetMenuRh();
        Task<List<RoleMenuDto>> GetMenu(string usuario);
        Task<ResultDto<List<SisUsuariosResponseDto>>> GetAll();
        Task<ResultDto<SisUsuariosResponseDto>> Update(SisUsuariosUpdateDto dto);
        Task<ResultDto<SisUsuariosResponseDto>> Create(SisUsuariosUpdateDto dto);

	}
}

