using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatCalcXTriangulacionRepository
    {
        Task<List<CAT_CALC_X_TRIANGULACION>> GetAll();
    }
}
