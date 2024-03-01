using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Catastro;

public interface IBM_V_BM1Service
{
    Task<ResultDto<List<Bm1GetDto>>> GetAll();
    Task<ResultDto<List<Bm1GetDto>>> GetAllByIcp(int codigoIcp);
    Task<ResultDto<List<ICPGetDto>>> GetICP();
    Task<List<Bm1GetDto>> GetByPlaca(int codigoBien);
}