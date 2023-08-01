using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_RELACION_CARGOSRepository
	{
        Task<List<PRE_RELACION_CARGOS>> GetAll();
        Task<PRE_RELACION_CARGOS> GetByCodigo(int codigoRelacionCargo);
        Task<ResultDto<PRE_RELACION_CARGOS>> GetByIcp(int codigoIcp);
        Task<ResultDto<PRE_RELACION_CARGOS>> Add(PRE_RELACION_CARGOS entity);
        Task<ResultDto<PRE_RELACION_CARGOS>> Update(PRE_RELACION_CARGOS entity);
        Task<string> Delete(int codigoRelacionCargo);
        Task<int> GetNextKey();
        Task<List<PRE_RELACION_CARGOS>> GetByCodigoCargo(int codigoCargo);

    }
}

