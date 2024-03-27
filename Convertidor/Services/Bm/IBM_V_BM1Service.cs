using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm;

public interface IBM_V_BM1Service
{
    Task<ResultDto<List<Bm1GetDto>>> GetAll(DateTime? desde,DateTime? hasta);
 
    Task<ResultDto<List<ICPGetDto>>> GetICP();
    Task<List<Bm1GetDto>> GetByPlaca(int codigoBien);

    Task<ResultDto<List<Bm1GetDto>>> GetByListIcp(Bm1Filter filter);
    Task<ResultDto<List<Bm1GetDto>>> GetAllByIcp(int codigoIcp, DateTime? desde,DateTime? hasta);

}