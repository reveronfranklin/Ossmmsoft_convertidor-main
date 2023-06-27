using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhDescriptivasService
	{

        Task<string> GetDescripcionByCodigoDescriptiva(int descripcionId);
        Task<List<RH_DESCRIPTIVAS>> GetAll();
    }
}

