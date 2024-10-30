using Convertidor.Data.Entities.Adm;

namespace Convertidor.Profiles.Adm.Replica;

public class AdmOrdenPagoProfile:Profile
{
    public AdmOrdenPagoProfile()
    {
        CreateMap<ADM_ORDEN_PAGO, Data.EntitiesDestino.ADM.ADM_ORDEN_PAGO>();
    }
}