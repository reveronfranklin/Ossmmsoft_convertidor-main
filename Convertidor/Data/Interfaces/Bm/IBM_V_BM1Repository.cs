using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Bm.Mobil;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBM_V_BM1Repository
{
    Task<List<BM_V_BM1>> GetAll();
    Task<List<BM_V_BM1>> GetByPlaca(int codigoBien);
    Task<BM_V_BM1> GetByNroPlaca(string nroPlaca);
    Task<List<BM_V_BM1>> GetAllByCodigoIcp(int codigoIcp);
    List<ICPGetDto> GetICP();

    Task<List<ProductResponse>> GetProductMobil(ProductFilterDto filter);
    Task<ProductResponse> GetProductMobilById(ProductFilterDto filter);
    Task<List<BmPlacas>> GetPlacas();

}