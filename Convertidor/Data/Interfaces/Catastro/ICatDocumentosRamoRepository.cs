﻿using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDocumentosRamoRepository
    {
        Task<List<CAT_DOCUMENTOS_RAMO>> GetAll();

        Task<ResultDto<CAT_DOCUMENTOS_RAMO>> Add(CAT_DOCUMENTOS_RAMO entity);
        Task<int> GetNextKey();
    }
}
