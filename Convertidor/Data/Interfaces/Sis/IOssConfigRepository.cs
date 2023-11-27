using System;
using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Sis
{
	public interface IOssConfigRepository
	{
        Task<List<OSS_CONFIG>> GetALL();
        Task<List<OSS_CONFIG>> GetListByClave(string clave);
        Task<OSS_CONFIG> GetByClave(string clave);
        Task<ResultDto<OSS_CONFIG>> Add(OSS_CONFIG entity);
        Task<ResultDto<OSS_CONFIG>> Update(OSS_CONFIG entity);

    }
}

