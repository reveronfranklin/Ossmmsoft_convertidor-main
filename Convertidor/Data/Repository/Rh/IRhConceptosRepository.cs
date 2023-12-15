using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Repository.Rh
{
	public interface IRhConceptosRepository
	{

        Task<List<RH_CONCEPTOS>> GetAll();
        Task<List<RH_CONCEPTOS>> GeTByTipoNomina(int codTipoNomina);
        Task<List<RH_CONCEPTOS>> GetConceptosByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta);
        Task<RH_CONCEPTOS> GetByCodigo(int codigoConcepto);
        Task<RH_CONCEPTOS> GetCodigoString(string codigo);
        Task<RH_CONCEPTOS> GetByCodigoTipoNomina(int codigoConcepto, int codigoTipoNomina);
        Task<RH_CONCEPTOS> GetByExtra1(string extra1);
        Task<ResultDto<RH_CONCEPTOS>> Add(RH_CONCEPTOS entity);
        Task<ResultDto<RH_CONCEPTOS>> Update(RH_CONCEPTOS entity);
        //Task<string> Delete(int CodigoConcepto);
        Task<int> GetNextKey();
    }
}

