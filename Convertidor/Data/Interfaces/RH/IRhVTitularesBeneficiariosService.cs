namespace Convertidor.Data.Interfaces.RH;

public interface IRhVTitularesBeneficiariosService
{
    Task<ResultDto<List<RhVTitularBeneficiariosResponseDto>>> GetAll();
    Task<ResultDto<List<RhVTitularBeneficiariosResponseDto>>> GetByTipoNomina(FilterTipoNomina filter);
}