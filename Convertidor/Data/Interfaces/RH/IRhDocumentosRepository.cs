using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhDocumentosRepository
	{
        Task<List<RH_DOCUMENTOS>> GetByCodigoPersona(int codigoPersona);
        Task<RH_DOCUMENTOS> GetByCodigo(int codigoDocumento);

        Task<ResultDto<RH_DOCUMENTOS>> Add(RH_DOCUMENTOS entity);
        Task<ResultDto<RH_DOCUMENTOS>> Update(RH_DOCUMENTOS entity);
        Task<string> Delete(int codigoDocumento);
        Task<int> GetNextKey();

    }
}

