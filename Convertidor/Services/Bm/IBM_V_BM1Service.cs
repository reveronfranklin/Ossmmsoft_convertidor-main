using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Catastro;

public interface IBM_V_BM1Service
{
    Task<ResultDto<List<Bm1GetDto>>> GetAll();
    Task<List<Bm1GetDto>> GetByPlaca(int codigoBien);
}