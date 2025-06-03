using Convertidor.Data.Entities.Adm;

namespace Convertidor.Services.Adm.Pagos.AdmPagoElectronicoService;

public partial class AdmPagoElectronicoService 
{
    public List<string> Map(List<ADM_PAGOS_ELECTRONICOS> list)
    {
        List<string> mapped = new List<string>();
        foreach (var item in list)
        {
            mapped.Add(item.DATA);
        }

        return mapped;
    }

}