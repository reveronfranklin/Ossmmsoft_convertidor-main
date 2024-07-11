using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ITmpLibrosRepository
    {
        Task<List<TMP_LIBROS>> GetAll();
        Task<TMP_LIBROS> GetByCodigo(int codigoLibro);
        Task<ResultDto<TMP_LIBROS>> Add(TMP_LIBROS entity);
        Task<ResultDto<TMP_LIBROS>> Update(TMP_LIBROS entity);
        Task<int> GetNextKey();
    }
}
