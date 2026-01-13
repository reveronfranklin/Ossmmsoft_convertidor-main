using Convertidor.Data.Entities.Adm;

namespace Convertidor.Profiles.Adm.Replica;

public class AdmDescriptivaProfile:Profile
{
    public AdmDescriptivaProfile()
    {
        CreateMap<ADM_DESCRIPTIVAS, Data.EntitiesDestino.ADM.ADM_DESCRIPTIVAS>();
    }
}