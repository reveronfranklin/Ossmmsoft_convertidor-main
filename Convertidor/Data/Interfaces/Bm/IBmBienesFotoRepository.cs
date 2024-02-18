using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBmBienesFotoRepository
{
    Task<BM_BIENES_FOTO> GetByCodigo(int codigoBienFoto);
    Task<List<BM_BIENES_FOTO>> GetByCodigoBien(int codigoBien);
    Task<List<BM_BIENES_FOTO>> GetByNumeroPlaca(string numeroPlaca);
    Task<ResultDto<BM_BIENES_FOTO>> Add(BM_BIENES_FOTO entity);
    Task<ResultDto<BM_BIENES_FOTO>> Update(BM_BIENES_FOTO entity);
    Task<string> Delete(int codigoBien);
    Task<int> GetNextKey();
    Task<BM_BIENES_FOTO> GetByNumeroPlacaFoto(string numeroPlaca, string foto);
    Task<int> CantidadFotosPorPlaca(string numeroPlaca);

}