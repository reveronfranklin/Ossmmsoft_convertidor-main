using System.Text;
using AutoMapper;
using Convertidor.Data.Entities;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Convertidor.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace Convertidor.Services
{
    public class HistoricoNominaService : IHistoricoNominaService
    {



        private readonly IRH_HISTORICO_NOMINARepository _repository;
        private readonly IHistoricoNominaRepository _destinoRepository;
        private readonly IHistoricoPersonalCargoRepository _historicoPersonalCargoRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public HistoricoNominaService(IRH_HISTORICO_NOMINARepository repository,
                                      IHistoricoNominaRepository destinoRepository,
                                      IHistoricoPersonalCargoRepository historicoPersonalCargoRepository,
                                      IMapper mapper,
                                      IDistributedCache distributedCache)
        {
            _repository = repository;
            _destinoRepository = destinoRepository;
            _historicoPersonalCargoRepository = historicoPersonalCargoRepository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }


        public async Task<ResultDto<HistoricoNomina>> TransferirHistoricoNominaPorCantidadDeDias(int dias)
        {

            ResultDto<HistoricoNomina> result = new ResultDto<HistoricoNomina>(null);

            List<HistoricoNomina> destinoList = new List<HistoricoNomina>();

            try
            {


                var historico = await _repository.GetByLastDay(dias);
                if (historico.ToList().Count > 0)
                {
                    await _destinoRepository.DeletePorDias(dias);

                    foreach (var item in historico)
                    {

                        var destinoNew = _mapper.Map<HistoricoNomina>(item);

                        destinoNew.COMPLEMENTO_CONCEPTO = item.COMPLEMENTO_CONCEPTO ?? "";
                        destinoNew.EXTRA1 = item.EXTRA1 ?? "";
                        destinoNew.EXTRA2 = item.EXTRA2 ?? "";
                        destinoNew.EXTRA3 = item.EXTRA3 ?? "";
                        var historicoPersonalCargo =
                            await _historicoPersonalCargoRepository.GetByKey(item.CODIGO_EMPRESA, item.CODIGO_PERSONA, item.CODIGO_TIPO_NOMINA, item.CODIGO_PERIODO);
                        if (historicoPersonalCargo!= null)
                        {
                            destinoList.Add(destinoNew);
                            //var adicionado = await _destinoRepository.Add(destinoNew);
                        }
                        else
                        {

                            var aqui = 0;
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

        public async Task<List<RH_HISTORICO_NOMINA>> GetByPeriodo(int codigoPeriodo,int tipoNomina)
        {
            try
            {
                var listHistorico = new List<RH_HISTORICO_NOMINA>();

                var cacheKey = "listNominaPeriodo_" + codigoPeriodo.ToString() + "_" + tipoNomina.ToString();

                var serializedListNominaPeriodo = string.Empty;

                var redisListNominaPeriodo = await _distributedCache.GetAsync(cacheKey);
                if (redisListNominaPeriodo != null)
                {
                    serializedListNominaPeriodo = System.Text.Encoding.UTF8.GetString(redisListNominaPeriodo);
                    listHistorico = JsonConvert.DeserializeObject<List<RH_HISTORICO_NOMINA>>(serializedListNominaPeriodo);
                 
                }
                else
                {
                    listHistorico = await _repository.GetByPeriodo(codigoPeriodo, tipoNomina);
                    serializedListNominaPeriodo = JsonConvert.SerializeObject(listHistorico);
                    redisListNominaPeriodo = Encoding.UTF8.GetBytes(serializedListNominaPeriodo);

                    var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddDays(2))
                            .SetSlidingExpiration(TimeSpan.FromDays(1));
                    await _distributedCache.SetAsync(cacheKey, redisListNominaPeriodo, options);

                }

                return listHistorico;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<IEnumerable<HistoricoNomina>> GetByLastDay(int days)
        {
            try
            {

                var result = await _destinoRepository.GetByLastDay(days);
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<IEnumerable<HistoricoNomina>> GetByLastDayWithRelation(int days)
        {
            try
            {

                var result = await _destinoRepository.GetByLastDayWithRelation(days);
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }

        }

    }
}
