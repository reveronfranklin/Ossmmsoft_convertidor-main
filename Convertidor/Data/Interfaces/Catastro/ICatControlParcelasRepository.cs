using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatControlParcelasRepository
    {
        Task<List<CAT_CONTROL_PARCELAS>> GetAll();
        Task<ResultDto<CAT_CONTROL_PARCELAS>> Add(CAT_CONTROL_PARCELAS entity);
        Task<int> GetNextKey();
    }
}
