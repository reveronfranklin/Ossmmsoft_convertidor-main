using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Bm
{
    public interface IBmDetalleBienesRepository
    {
        Task<BM_DETALLE_BIENES> GetByCodigoBien(int codigoBien);
        Task<BM_DETALLE_BIENES> GetByCodigoDetalleBien(int codigoDetalleBien);
  
        Task<List<BM_DETALLE_BIENES>> GetAll();
        Task<ResultDto<BM_DETALLE_BIENES>> Add(BM_DETALLE_BIENES entity);
        Task<ResultDto<BM_DETALLE_BIENES>> Update(BM_DETALLE_BIENES entity);
        Task<string> Delete(int codigoBien);
        Task<int> GetNextKey();
        
    }
}

