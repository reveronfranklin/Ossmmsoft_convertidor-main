using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Repository.Rh
{
	public interface IRhConceptosRepository
	{

        Task<List<RH_CONCEPTOS>> GetAll();
        Task<List<RH_CONCEPTOS>> GeTByTipoNomina(int codTipoNomina);

        Task<List<RH_CONCEPTOS>> GetConceptosByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta);
        Task<RH_CONCEPTOS> GetByCodigo(int codigoConcepto);
        Task<RH_CONCEPTOS> GetByCodigoTipoNomina(int codigoConcepto, int codigoTipoNomina);
    }
}

