using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntLibrosRepository
    {
        Task<List<CNT_LIBROS>> GetAll();
        Task<ResultDto<CNT_LIBROS>> Add(CNT_LIBROS entity);
        Task<int> GetNextKey();
    }
}
