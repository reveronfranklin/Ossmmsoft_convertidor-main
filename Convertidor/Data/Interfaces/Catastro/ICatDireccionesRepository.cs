using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDireccionesRepository
    {
        Task<List<CAT_DIRECCIONES>> GetAll();
        Task<CAT_DIRECCIONES> GetByCodigo(int codigoDireccion);
        Task<ResultDto<CAT_DIRECCIONES>> Add(CAT_DIRECCIONES entity);
        Task<ResultDto<CAT_DIRECCIONES>> Update(CAT_DIRECCIONES entity);
        Task<int> GetNextKey();
    }
}
