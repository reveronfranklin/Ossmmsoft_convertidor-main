using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhDescriptivaRepository
	{

        Task<RH_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId);

    }
}

