namespace Convertidor.Data.Interfaces.RH;

public interface IRhVTitularesBeneficiariosRepository
{
    Task<List<RH_V_TITULAR_BENEFICIARIOS>> GetAll();
    Task<List<RH_V_TITULAR_BENEFICIARIOS>> GetByTipoNomina(int codigoTipoNomina);
}