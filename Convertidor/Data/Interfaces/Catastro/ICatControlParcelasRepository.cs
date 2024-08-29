using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatControlParcelasRepository
    {
        Task<List<CAT_CONTROL_PARCELAS>> GetAll();
        Task<CAT_CONTROL_PARCELAS> GetByCodigo(int codigoControlParcela);
        Task<ResultDto<CAT_CONTROL_PARCELAS>> Add(CAT_CONTROL_PARCELAS entity);
        Task<ResultDto<CAT_CONTROL_PARCELAS>> Update(CAT_CONTROL_PARCELAS entity);
        Task<int> GetNextKey();
    }
}
