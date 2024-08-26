using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatCalcXTriangulacionRepository
    {
        Task<List<CAT_CALC_X_TRIANGULACION>> GetAll();
        Task<CAT_CALC_X_TRIANGULACION> GetByCodigo(int codigoTriangulacion);
        Task<ResultDto<CAT_CALC_X_TRIANGULACION>> Add(CAT_CALC_X_TRIANGULACION entity);
        Task<ResultDto<CAT_CALC_X_TRIANGULACION>> Update(CAT_CALC_X_TRIANGULACION entity);
        Task<int> GetNextKey();
    }
}
