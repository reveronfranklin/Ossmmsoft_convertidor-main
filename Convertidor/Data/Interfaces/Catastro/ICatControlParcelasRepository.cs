using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatControlParcelasRepository
    {
        Task<List<CAT_CONTROL_PARCELAS>> GetAll();
    }
}
