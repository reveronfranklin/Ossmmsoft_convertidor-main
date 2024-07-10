﻿using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ITmpAuxiliaresRepository
    {
        Task<List<TMP_AUXILIARES>> GetAll();
        Task<ResultDto<TMP_AUXILIARES>> Add(TMP_AUXILIARES entity);
        Task<int> GetNextKey();
    }
}
