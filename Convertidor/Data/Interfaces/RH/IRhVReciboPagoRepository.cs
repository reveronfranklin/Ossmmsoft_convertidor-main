namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhVReciboPagoRepository
    {
        Task<List<RH_V_RECIBO_PAGO>> GetAll();
        Task<List<RH_V_RECIBO_PAGO>> GeTByFilter(int codigoPeriodo, int codigoTipoNomina, int codigoPersona);
        Task<RH_V_RECIBO_PAGO> GetByCodigoTipoNomina(int codigoTipoNomina, int codigoPersona);
        
    }
}
