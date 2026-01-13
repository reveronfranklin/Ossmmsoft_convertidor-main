using Convertidor.Data.Entities.Adm;

namespace Convertidor.Profiles.Adm.Replica;

public class AdmDocumentosOpProfile:Profile
{
    public AdmDocumentosOpProfile()
    {
        CreateMap<ADM_DOCUMENTOS_OP, Data.EntitiesDestino.ADM.ADM_DOCUMENTOS_OP>();
    }
}