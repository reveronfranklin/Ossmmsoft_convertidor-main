using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Repository.Rh
{
	public interface IRhConceptosRepository
	{

        Task<List<RH_CONCEPTOS>> GetAll();
        Task<List<RH_CONCEPTOS>> GeTByTipoNomina(int codTipoNomina);
    }
}

