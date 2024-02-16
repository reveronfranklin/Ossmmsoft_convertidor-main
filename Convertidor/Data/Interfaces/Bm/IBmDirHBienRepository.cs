using System;
using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm
{
    public interface IBmDirHBienRepository
    {
        Task<BM_DIR_H_BIEN> GetByCodigoDirBien(int codigoDirBien);
        Task<BM_DIR_H_BIEN> GetByCodigoHDirBien(int codigoHDirBien);
        Task<BM_DIR_H_BIEN> GetByCodigoIcp(int codigoIcp);
        Task<List<BM_DIR_H_BIEN>> GetAll();
        Task<ResultDto<BM_DIR_H_BIEN>> Add(BM_DIR_H_BIEN entity);
        Task<ResultDto<BM_DIR_H_BIEN>> Update(BM_DIR_H_BIEN entity);
        Task<string> Delete(int codigoHDirBien);
        Task<int> GetNextKey();
        
    }
}

