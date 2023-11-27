﻿using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
    public interface IRhHRetencionesIncesService
    {
        Task<List<RhTmpRetencionesIncesDto>> GetRetencionesHInces(FilterRetencionesDto filter);
        Task<ResultDto<string>> Create(List<RH_H_RETENCIONES_INCES> entities);
    }
}
