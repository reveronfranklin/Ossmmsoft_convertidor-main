using Convertidor.Data.Entities.Adm;

namespace Convertidor.Profiles.Adm.Replica;

public class AdmproveedorProfile:Profile
{
    public AdmproveedorProfile()
    {
        CreateMap<ADM_PROVEEDORES, Data.EntitiesDestino.ADM.ADM_PROVEEDORES>();
    }
}