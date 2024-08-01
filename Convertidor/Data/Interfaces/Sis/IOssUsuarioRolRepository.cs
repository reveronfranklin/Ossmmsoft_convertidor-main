using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssUsuarioRolRepository
{
    Task<List<OSS_USUARIO_ROL>> GetALL();
    Task<OSS_USUARIO_ROL> GetByCodigo(int codigoUsuarioRol);
    Task<List<OSS_USUARIO_ROL>> GetByCodigoUsuario(int codigoUsuario);
    Task<List<OSS_USUARIO_ROL>> GetByUsuario(string usuario);

    Task<ResultDto<OSS_USUARIO_ROL>> Add(OSS_USUARIO_ROL entity);
    Task<ResultDto<OSS_USUARIO_ROL>> Update(OSS_USUARIO_ROL entity);


}