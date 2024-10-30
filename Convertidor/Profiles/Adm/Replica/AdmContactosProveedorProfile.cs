using Convertidor.Data.Entities.Adm;

namespace Convertidor.Profiles.Adm.Replica;

public class AdmContactorProveedorProfile:Profile
{
    public AdmContactorProveedorProfile()
    {
        CreateMap<ADM_CONTACTO_PROVEEDOR, Data.EntitiesDestino.ADM.ADM_CONTACTO_PROVEEDOR>();
    }
}