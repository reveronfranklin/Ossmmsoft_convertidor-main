using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisSerieDocumentosRepository
{

    Task<List<SIS_SERIE_DOCUMENTOS>> GetALL();
    Task<SIS_SERIE_DOCUMENTOS> GetById(int id);
    Task<ResultDto<SIS_SERIE_DOCUMENTOS>> Add(SIS_SERIE_DOCUMENTOS entity);
    Task<ResultDto<SIS_SERIE_DOCUMENTOS>> Update(SIS_SERIE_DOCUMENTOS entity);
    Task<int> GetNextKey();
    Task<ResultDto<string>> GenerateNextSerie(int codigoPresupuesto, int tipoDocumentoId, string codigo);


}