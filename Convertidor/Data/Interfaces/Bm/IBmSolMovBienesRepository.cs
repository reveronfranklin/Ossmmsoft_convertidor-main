﻿using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm
{
    public interface IBmSolMovBienesRepository
    {

        Task<BM_SOL_MOV_BIENES> GetByCodigoMovBien(int codigoMovBien);
        Task<List<BM_SOL_MOV_BIENES>> GetAll();
        Task<BM_SOL_MOV_BIENES> GetByCodigoBien(int codigoBien);
        Task<ResultDto<BM_SOL_MOV_BIENES>> Add(BM_SOL_MOV_BIENES entity);
        Task<ResultDto<BM_SOL_MOV_BIENES>> Update(BM_SOL_MOV_BIENES entity);
        Task<string> Delete(int codigoMovBien);
        Task<int> GetNextKey();
 
    }
}

