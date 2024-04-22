using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm
{
    public interface IBmClasificacionBienesRepository
	{

        Task<BM_CLASIFICACION_BIENES> GetByCodigoClasificacionBien(int codigoClasificacionBien);
        Task<BM_CLASIFICACION_BIENES> GetByCodigoGrupo(string codigoGrupo);
        Task<List<BM_CLASIFICACION_BIENES>> GetAll();
        Task<BM_CLASIFICACION_BIENES> GetByCodigoNivel1(string codigo);
        Task<BM_CLASIFICACION_BIENES> GetByCodigoNivel2(string codigo);
        Task<ResultDto<BM_CLASIFICACION_BIENES>> Add(BM_CLASIFICACION_BIENES entity);
        Task<ResultDto<BM_CLASIFICACION_BIENES>> Update(BM_CLASIFICACION_BIENES entity);
        Task<string> Delete(int codigoClasificacionBien);
        Task<int> GetNextKey();
     
    }
}

