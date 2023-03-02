using AutoMapper;
using Convertidor.Data.Entities;
using Convertidor.Data.EntitiesDestino;

namespace Convertidor.Profiles
{
    public class HistoricoNominaProfile:Profile
    {

        public HistoricoNominaProfile()
        {

            //Source------> Destination
            CreateMap<RH_HISTORICO_NOMINA, HistoricoNomina>();
            CreateMap<RH_HISTORICO_PERSONAL_CARGO, HistoricoPersonalCargo>();
            CreateMap<PRE_INDICE_CAT_PRG, IndiceCategoriaPrograma>();

            
        }
    }
}
