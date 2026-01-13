using Convertidor.Data.Entities.Adm;

namespace Convertidor.Services.Adm;

public interface IADM_V_PAGAR_A_LA_OP_TERCEROSServices
{
    Task<ResultDto<List<ADM_V_PAGAR_A_LA_OP_TERCEROS>>> GetAll();
}