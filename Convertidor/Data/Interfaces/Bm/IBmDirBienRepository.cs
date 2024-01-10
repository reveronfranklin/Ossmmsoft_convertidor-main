using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Bm
{
    public interface IBmDirBienRepository
    {
        Task<BM_DIR_BIEN> GetByCodigoDirBien(int codigoDirBien);
        Task<BM_DIR_BIEN> GetByCodigoIcp(int codigoIcp);
  
        Task<List<BM_DIR_BIEN>> GetAll();
        Task<ResultDto<BM_DIR_BIEN>> Add(BM_DIR_BIEN entity);
        Task<ResultDto<BM_DIR_BIEN>> Update(BM_DIR_BIEN entity);
        Task<string> Delete(int codigoDirBien);
        Task<int> GetNextKey();
        
    }
}

