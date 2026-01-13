using Convertidor.Data.Entities.Adm;

namespace Convertidor.Profiles.Adm.Replica;

public class AdmretencionesOpProfile:Profile
{
    public AdmretencionesOpProfile()
    {
        CreateMap<ADM_RETENCIONES_OP, Data.EntitiesDestino.ADM.ADM_RETENCIONES_OP>();
    }
}