﻿using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis
{
	public interface IOssConfigServices
	{

        Task<ResultDto<List<OssConfigGetDto>>> GetListByClave(string clave);
        Task<int> GetNextByClave(string clave);

    }
}

