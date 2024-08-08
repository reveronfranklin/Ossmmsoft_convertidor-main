using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreCompromisosRepository
    {


        Task<PRE_COMPROMISOS> GetByCodigo(int codigoCompromiso);
        Task<ResultDto<List<PreCompromisosResponseDto>>> GetByPresupuesto(PreCompromisosFilterDto filter);
        Task<PRE_COMPROMISOS> GetByCodigoSolicitud(int codigoSolicitud);
        Task<PRE_COMPROMISOS> GetByNumeroYFecha(string numeroCompromiso, DateTime fechaCompromiso);
        Task<List<PRE_COMPROMISOS>> GetAll();
        Task<ResultDto<PRE_COMPROMISOS>> Add(PRE_COMPROMISOS entity);
        Task<ResultDto<PRE_COMPROMISOS>> Update(PRE_COMPROMISOS entity);
        Task<string> Delete(int codigoCompromiso);
        Task<int> GetNextKey();
       
    }
}

