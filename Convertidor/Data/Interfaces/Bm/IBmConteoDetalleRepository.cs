using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBmConteoDetalleRepository
{
    Task<BM_CONTEO_DETALLE> GetByCodigo(int conteoDetalleId);
    Task<List<BM_CONTEO_DETALLE>> GetAllByConteo(int codigoConteo);
    Task<List<BM_CONTEO_DETALLE>> GetAll();
    Task<ResultDto<BM_CONTEO_DETALLE>> Add(BM_CONTEO_DETALLE entity);
    Task<ResultDto<BM_CONTEO_DETALLE>> Update(BM_CONTEO_DETALLE entity);
    Task<string> Delete(int tituloId);
    Task<int> GetNextKey();
    Task<bool> ExisteConteo(int codigoConteo);
    Task<ResultDto<List<BM_CONTEO_DETALLE>>> AddRange(List<BM_CONTEO_DETALLE> entity);
    Task<bool> ConteoIniciado(int codigoConteo);
    Task<bool> DeleteRangeConteo(int codigoConteo);
    Task<bool> ConteoIniciadoConDiferenciaSinComentario(int codigoConteo);

    ResultDto<bool> CrearDesdeBm1(string unidadTrabajo, int codigoEmpresa, int usuario, int codigoConteo,
        int cantidadConteos);
}