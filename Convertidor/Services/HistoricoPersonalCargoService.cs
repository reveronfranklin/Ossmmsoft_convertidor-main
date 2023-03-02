using AutoMapper;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Convertidor.Dtos;

namespace Convertidor.Services
{
    public class HistoricoPersonalCargoService: IHistoricoPersonalCargoService
    {

        private readonly IRH_HISTORICO_PERSONAL_CARGORepository _repository;
        private readonly IHistoricoPersonalCargoRepository _destinoRepository;
        private readonly IIndiceCategoriaProgramaRepository _indiceCategoriaProgramaRepository;
        private readonly IMapper _mapper;

        public HistoricoPersonalCargoService(IRH_HISTORICO_PERSONAL_CARGORepository repository,
                                      IHistoricoPersonalCargoRepository destinoRepository,
                                      IIndiceCategoriaProgramaRepository indiceCategoriaProgramaRepository,
                                      IMapper mapper)
        {
            _repository = repository;
            _destinoRepository = destinoRepository;
            _indiceCategoriaProgramaRepository = indiceCategoriaProgramaRepository;
            _mapper = mapper;
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



    }
}
