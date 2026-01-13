using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Profiles.Pre.Replica;

public class PreVSaldoProfile:Profile
{
    public PreVSaldoProfile()
    {
        CreateMap<PRE_V_SALDOS, Data.EntitiesDestino.PRE.PRE_V_SALDOS>();
    }
}