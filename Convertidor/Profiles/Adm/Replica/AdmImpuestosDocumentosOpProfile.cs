using Convertidor.Data.Entities.Adm;

namespace Convertidor.Profiles.Adm.Replica;

public class AdmImpuestosDocumentosOpProfile:Profile
{
    public AdmImpuestosDocumentosOpProfile()
    {
        CreateMap<ADM_IMPUESTOS_DOCUMENTOS_OP, Data.EntitiesDestino.ADM.ADM_IMPUESTOS_DOCUMENTOS_OP>();
    }
}