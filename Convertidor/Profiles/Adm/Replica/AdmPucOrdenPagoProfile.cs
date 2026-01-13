using Convertidor.Data.Entities.Adm;

namespace Convertidor.Profiles.Adm.Replica;

public class AdmPucOrdenPagoProfile:Profile
{
    public AdmPucOrdenPagoProfile()
    {
        CreateMap<ADM_PUC_ORDEN_PAGO, Data.EntitiesDestino.ADM.ADM_PUC_ORDEN_PAGO>();
    }
}