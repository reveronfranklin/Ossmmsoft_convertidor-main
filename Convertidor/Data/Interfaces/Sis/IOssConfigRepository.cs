using System;
using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis
{
	public interface IOssConfigRepository
	{
        Task<List<OSS_CONFIG>> GetALL();
        Task<List<OSS_CONFIG>> GetListByClave(string clave);

    }
}

