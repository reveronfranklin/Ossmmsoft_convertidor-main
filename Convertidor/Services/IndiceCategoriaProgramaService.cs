using AutoMapper;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Convertidor.Dtos;

namespace Convertidor.Services
{
    public class IndiceCategoriaProgramaService: IIndiceCategoriaProgramaService
    {
        
        private readonly IPRE_INDICE_CAT_PRGRepository _repository;
        private readonly IIndiceCategoriaProgramaRepository _destinoRepository;
        private readonly IMapper _mapper;

        public IndiceCategoriaProgramaService(IPRE_INDICE_CAT_PRGRepository repository,
                                      IIndiceCategoriaProgramaRepository destinoRepository,
                                      IMapper mapper)
        {
            _repository = repository;
            _destinoRepository = destinoRepository;
            _mapper = mapper;
        }


        public async Task<ResultDto<IndiceCategoriaPrograma>> TransferirIndiceCategoriaProgramaPorCantidadDeDias(int dias)
        {

            ResultDto<IndiceCategoriaPrograma> result = new ResultDto<IndiceCategoriaPrograma>(null);

            List<IndiceCategoriaPrograma> destinoList = new List<IndiceCategoriaPrograma>();

            try
            {


                var historico = await _repository.GetAll();
                if (historico.ToList().Count > 0)
                {
                    await _destinoRepository.DeleteAll();

                    foreach (var item in historico)
                    {

                        var destinoNew = _mapper.Map<IndiceCategoriaPrograma>(item);

                        
                        //destinoNew.EXTRA1 = item.EXTRA1 ?? "";
                       // destinoNew.EXTRA2 = item.EXTRA2 ?? "";
                       // destinoNew.EXTRA3 = item.EXTRA3 ?? "";
                        destinoList.Add(destinoNew);

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
