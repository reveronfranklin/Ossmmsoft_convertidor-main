using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmValContratoRepository
    {
        Task<ADM_VAL_CONTRATO> GetCodigoValContrato(int codigoValContrato);
        Task<List<ADM_VAL_CONTRATO>> GetAll();
        Task<ResultDto<ADM_VAL_CONTRATO>> Add(ADM_VAL_CONTRATO entity);
        Task<ResultDto<ADM_VAL_CONTRATO>> Update(ADM_VAL_CONTRATO entity);
        Task<string> Delete(int codigoValContrato);
        Task<int> GetNextKey();
    }
}
