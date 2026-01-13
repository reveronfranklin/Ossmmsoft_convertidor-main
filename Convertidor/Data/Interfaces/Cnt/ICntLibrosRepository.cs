using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntLibrosRepository
    {
        Task<List<CNT_LIBROS>> GetAll();
        Task<CNT_LIBROS> GetByCodigo(int codigoLibro);
        Task<ResultDto<CNT_LIBROS>> Add(CNT_LIBROS entity);
        Task<ResultDto<CNT_LIBROS>> Update(CNT_LIBROS entity);
        Task<string> Delete(int codigoLibro);
        Task<int> GetNextKey();
    }
}
