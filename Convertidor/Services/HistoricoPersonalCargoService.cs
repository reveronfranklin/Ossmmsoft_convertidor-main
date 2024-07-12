using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Convertidor.Utility;

namespace Convertidor.Services
{
    public class HistoricoPersonalCargoService: IHistoricoPersonalCargoService
    {

        private readonly IRH_HISTORICO_PERSONAL_CARGORepository _repository;
        private readonly IHistoricoPersonalCargoRepository _destinoRepository;
        private readonly IIndiceCategoriaProgramaRepository _indiceCategoriaProgramaRepository;
        private readonly IRhPersonaService _personaService;
        private readonly IMapper _mapper;

        public HistoricoPersonalCargoService(IRH_HISTORICO_PERSONAL_CARGORepository repository,
                                      IHistoricoPersonalCargoRepository destinoRepository,
                                      IIndiceCategoriaProgramaRepository indiceCategoriaProgramaRepository,
                                      IMapper mapper, IRhPersonaService personaService)
        {
            _repository = repository;
            _destinoRepository = destinoRepository;
            _indiceCategoriaProgramaRepository = indiceCategoriaProgramaRepository;
            _mapper = mapper;
            _personaService = personaService;
        }

        public async Task<ResultDto<HistoricoPersonalCargo>> TransferirHistoricoPersonalCargoPorCantidadDeDias(int dias)
        {

            ResultDto<HistoricoPersonalCargo> result = new ResultDto<HistoricoPersonalCargo>(null);

            List<HistoricoPersonalCargo> destinoList = new List<HistoricoPersonalCargo>();

            try
            {


                var historico = await _repository.GetByLastDay(dias);
                if (historico.ToList().Count > 0)
                {
                    await _destinoRepository.DeletePorDias(dias);

                    foreach (var item in historico)
                    {

                     

                        var destinoNew = _mapper.Map<HistoricoPersonalCargo>(item);

                     
                        destinoNew.EXTRA1 = item.EXTRA1 ?? "";
                        destinoNew.EXTRA2 = item.EXTRA2 ?? "";
                        destinoNew.EXTRA3 = item.EXTRA3 ?? "";
                        
                        var indiceCategoriaPrograma = await _indiceCategoriaProgramaRepository.GetByKey(item.CODIGO_ICP);
                        if (indiceCategoriaPrograma==null)
                        {
                            var aqui = 0;
                        }
                        var historicoPersonalCargo = destinoList.Where(x => x.CODIGO_EMPRESA == item.CODIGO_EMPRESA && x.CODIGO_PERSONA == item.CODIGO_PERSONA && x.CODIGO_TIPO_NOMINA == item.CODIGO_TIPO_NOMINA && x.CODIGO_PERIODO == item.CODIGO_PERIODO).FirstOrDefault();
                        if (historicoPersonalCargo == null && indiceCategoriaPrograma!=null) {
                            destinoList.Add(destinoNew);
                        }
                        else
                        {
                            var deterner = 0;
                        }
                            

                    }
                    var adicionados = await _destinoRepository.Add(destinoList);
                    result.IsValid = true;
                    result.Message = $" {historico.ToList().Count} Registros Transferidos";
                    return result;
                }
                else
                {

                    result.IsValid = true;
                    result.Message = $" No existen Datos para transferir";
                    return result;

                }



            }
            catch (Exception ex)
            {

                result.IsValid = false;
                result.Message = ex.InnerException.Message;
                return result;

            }
        }


        public async Task<List<RhTipoNominaCargosResponseDto> > GetListCargosPorPersona(int codigoPersona)
        {

            List<RhTipoNominaCargosResponseDto> result = new List<RhTipoNominaCargosResponseDto>();
            var historico = await _repository.GetByCodigoPersona(codigoPersona);
            var cargoActual = await _personaService.CargoActual(codigoPersona);


            var rhHistoricoPersonalCargoes = historico.ToList();
            var q= from s in rhHistoricoPersonalCargoes.OrderByDescending(x => x.FECHA_NOMINA).ToList()
                group s by new { CodigoTipoNomina = s.CODIGO_TIPO_NOMINA, TipoNomina= s.TIPO_NOMINA, CodigoCargo =s.CODIGO_CARGO,DescripcionCargo= s.DESCRIPCION_CARGO} into g
                select new  {
                    g.Key.CodigoTipoNomina,
                    g.Key.TipoNomina,
                    g.Key.CodigoCargo,
                    g.Key.DescripcionCargo
                   
                };

            foreach (var item in q.ToList())
            {
                var existe = result.Where(x => x.DescripcionCargo == item.DescripcionCargo).FirstOrDefault();
                if(existe is null){
                    RhTipoNominaCargosResponseDto itemResult = new RhTipoNominaCargosResponseDto();
                    itemResult.Color = "error";
                    if (cargoActual != null && item.CodigoCargo == cargoActual.CODIGO_CARGO) itemResult.Color  = "success";
                    itemResult.CodigoCargo = item.CodigoCargo;
                    itemResult.DescripcionCargo = item.DescripcionCargo;
                    itemResult.CodigoTipoNomina = item.CodigoTipoNomina;
                    var firstCargo = rhHistoricoPersonalCargoes.
                        Where(x => x.DESCRIPCION_CARGO == item.DescripcionCargo).MinBy(x => x.FECHA_NOMINA);
                    var lastCargo = rhHistoricoPersonalCargoes.Where(x => x.DESCRIPCION_CARGO == item.DescripcionCargo).MaxBy(x => x.FECHA_NOMINA);
                    var desdeObj = Fecha.GetFechaDto(firstCargo.FECHA_NOMINA);
                    itemResult.Desde = $"{desdeObj.Day}/{desdeObj.Month}/{desdeObj.Year}";
                    var hastaObj=Fecha.GetFechaDto(lastCargo.FECHA_NOMINA);
                    itemResult.Hasta = $"{hastaObj.Day}/{hastaObj.Month}/{hastaObj.Year}";
                    itemResult.TipoNomina = item.TipoNomina;
                    result.Add(itemResult);

                }

            }

            return result;



        }


    }
}
