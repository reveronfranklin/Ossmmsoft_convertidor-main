using Convertidor.Data.Entities.Adm;

namespace Convertidor.Profiles.Adm.Replica;

public class AdmBeneficiariosOpProfile:Profile
{
    public AdmBeneficiariosOpProfile()
    {
        CreateMap<ADM_BENEFICIARIOS_OP, Data.EntitiesDestino.ADM.ADM_BENEFICIARIOS_OP>();
    }
}