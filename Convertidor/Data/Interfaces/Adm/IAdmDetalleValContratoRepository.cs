using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmDetalleValContratoRepository
    {
        Task<ADM_DETALLE_VAL_CONTRATO> GetCodigoDetalleValContrato(int codigoDetalleValContrato);
        Task<List<ADM_DETALLE_VAL_CONTRATO>> GetAll();
        Task<ResultDto<ADM_DETALLE_VAL_CONTRATO>> Add(ADM_DETALLE_VAL_CONTRATO entity);
        Task<ResultDto<ADM_DETALLE_VAL_CONTRATO>> Update(ADM_DETALLE_VAL_CONTRATO entity);
        Task<string> Delete(int codigoDetalleValContrato);
        Task<int> GetNextKey();
    }
}
