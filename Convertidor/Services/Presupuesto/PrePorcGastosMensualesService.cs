using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PrePorcGastosMensualesService : IPrePorcGastosMensualesService
    {
        private readonly IPrePorcGastosMensualesRepository _repository;
        private readonly IPRE_INDICE_CAT_PRGRepository _inDICE_CAT_PRGRepository;
        private readonly IPRE_PLAN_UNICO_CUENTASRepository _pLAN_UNICO_CUENTASRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PrePorcGastosMensualesService(IPrePorcGastosMensualesRepository repository,
                                               IPRE_INDICE_CAT_PRGRepository inDICE_CAT_PRGRepository,
                                               IPRE_PLAN_UNICO_CUENTASRepository pLAN_UNICO_CUENTASRepository,
                                               IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                               ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _inDICE_CAT_PRGRepository = inDICE_CAT_PRGRepository;
            _pLAN_UNICO_CUENTASRepository = pLAN_UNICO_CUENTASRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PrePorcGastosMensualesResponseDto>>> GetAll()
        {

            ResultDto<List<PrePorcGastosMensualesResponseDto>> result = new ResultDto<List<PrePorcGastosMensualesResponseDto>>(null);
            try
            {

                var PrePorGastosMes = await _repository.GetAll();



                if (PrePorGastosMes.Count() > 0)
                {
                    List<PrePorcGastosMensualesResponseDto> listDto = new List<PrePorcGastosMensualesResponseDto>();

                    foreach (var item in PrePorGastosMes)
                    {
                        var dto = await MapPrePorcGastosMensuales(item);
                        listDto.Add(dto);
                    }


                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }


        public async Task<PrePorcGastosMensualesResponseDto> MapPrePorcGastosMensuales(PRE_PORC_GASTOS_MENSUALES dto)
        {
            PrePorcGastosMensualesResponseDto itemResult = new PrePorcGastosMensualesResponseDto();
            itemResult.CodigoPorGastosMes = dto.CODIGO_POR_GASTOS_MES;
            itemResult.CodigoIcp = dto.CODIGO_ICP;
            itemResult.CodigoPuc = dto.CODIGO_PUC;
            itemResult.CodigoIcp = dto.CODIGO_ICP;
            itemResult.Por1Mes = dto.POR_1_MES;
            itemResult.Por2Mes = dto.POR_2_MES;
            itemResult.Por3Mes = dto.POR_3_MES;
            itemResult.Por4Mes = dto.POR_4_MES;
            itemResult.Por5Mes = dto.POR_5_MES;
            itemResult.Por6Mes = dto.POR_6_MES;
            itemResult.Por7Mes = dto.POR_7_MES;
            itemResult.Por8MES = dto.POR_8_MES;
            itemResult.Por9Mes = dto.POR_9_MES;
            itemResult.Por10Mes = dto.POR_10_MES;
            itemResult.Por11Mes = dto.POR_11_MES;
            itemResult.Por12Mes = dto.POR_12_MES;
            itemResult.MesInicial = dto.MES_INICIAL;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
       

            return itemResult;

        }

        public async Task<List<PrePorcGastosMensualesResponseDto>> MapListPrePorcGastosMensuales(List<PRE_PORC_GASTOS_MENSUALES> dtos)
        {
            List<PrePorcGastosMensualesResponseDto> result = new List<PrePorcGastosMensualesResponseDto>();


            foreach (var item in dtos)
            {

                PrePorcGastosMensualesResponseDto itemResult = new PrePorcGastosMensualesResponseDto();

                itemResult = await MapPrePorcGastosMensuales(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PrePorcGastosMensualesResponseDto>> Update(PrePorcGastosMensualesUpdateDto dto)
        {

            ResultDto<PrePorcGastosMensualesResponseDto> result = new ResultDto<PrePorcGastosMensualesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if(dto.CodigoPorGastosMes < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Participa Financiera Org invalido";
                    return result;

                }

                var codigoPorGastosMes = await _repository.GetByCodigo(dto.CodigoPorGastosMes);
                if(codigoPorGastosMes == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo por Gastos Mes invalido";
                    return result;
                }

                if (dto.CodigoIcp < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp invalido";
                    return result;

                }

                var codigoIcp = await _inDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if(codigoIcp == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp invalido";
                    return result;

                }

                if (dto.CodigoPuc < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc invalido";
                    return result;

                }

                var codigoPuc = await _pLAN_UNICO_CUENTASRepository.GetByCodigo(dto.CodigoPuc);
                if (codigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc invalido";
                    return result;

                }

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

              



                codigoPorGastosMes.CODIGO_POR_GASTOS_MES = dto.CodigoPorGastosMes;
                codigoPorGastosMes.CODIGO_ICP = dto.CodigoIcp;
                codigoPorGastosMes.CODIGO_PUC = dto.CodigoPuc;
                codigoPorGastosMes.POR_1_MES = dto.Por1Mes;
                codigoPorGastosMes.POR_2_MES = dto.Por2Mes;
                codigoPorGastosMes.POR_3_MES = dto.Por3Mes;
                codigoPorGastosMes.POR_4_MES = dto.Por4Mes;
                codigoPorGastosMes.POR_5_MES = dto.Por5Mes;
                codigoPorGastosMes.POR_6_MES = dto.Por6Mes;
                codigoPorGastosMes.POR_7_MES = dto.Por7Mes;
                codigoPorGastosMes.POR_8_MES = dto.Por8MES;
                codigoPorGastosMes.POR_9_MES = dto.Por9Mes;
                codigoPorGastosMes.POR_10_MES = dto.Por10Mes;
                codigoPorGastosMes.POR_11_MES = dto.Por11Mes;
                codigoPorGastosMes.POR_12_MES = dto.Por12Mes;
                codigoPorGastosMes.MES_INICIAL = dto.MesInicial;
                codigoPorGastosMes.EXTRA1 = dto.Extra1;
                codigoPorGastosMes.EXTRA2 = dto.Extra2;
                codigoPorGastosMes.EXTRA3 = dto.Extra3;


                codigoPorGastosMes.CODIGO_EMPRESA = conectado.Empresa;
                codigoPorGastosMes.USUARIO_UPD = conectado.Usuario;
                codigoPorGastosMes.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoPorGastosMes);

                var resultDto = await MapPrePorcGastosMensuales(codigoPorGastosMes);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<PrePorcGastosMensualesResponseDto>> Create(PrePorcGastosMensualesUpdateDto dto)
        {

            ResultDto<PrePorcGastosMensualesResponseDto> result = new ResultDto<PrePorcGastosMensualesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp invalido";
                    return result;

                }

                var codigoIcp = await _inDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp invalido";
                    return result;

                }

                if (dto.CodigoPuc < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc invalido";
                    return result;

                }

                var codigoPuc = await _pLAN_UNICO_CUENTASRepository.GetByCodigo(dto.CodigoPuc);
                if (codigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc invalido";
                    return result;

                }

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }





                PRE_PORC_GASTOS_MENSUALES entity = new PRE_PORC_GASTOS_MENSUALES();
                entity.CODIGO_POR_GASTOS_MES = await _repository.GetNextKey();
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.CODIGO_PUC = dto.CodigoPuc;
                entity.POR_1_MES = dto.Por1Mes;
                entity.POR_2_MES = dto.Por2Mes;
                entity.POR_3_MES = dto.Por3Mes;
                entity.POR_4_MES = dto.Por4Mes;
                entity.POR_5_MES = dto.Por5Mes;
                entity.POR_7_MES = dto.Por7Mes;
                entity.POR_8_MES = dto.Por8MES;
                entity.POR_9_MES = dto.Por9Mes;
                entity.POR_10_MES = dto.Por10Mes;
                entity.POR_11_MES = dto.Por11Mes;
                entity.POR_12_MES = dto.Por12Mes;
                entity.MES_INICIAL = dto.MesInicial;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPrePorcGastosMensuales(created.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";


                }
                else
                {

                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                }

                return result;



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<PrePorcGastosMensualesDeleteDto>> Delete(PrePorcGastosMensualesDeleteDto dto)
        {

            ResultDto<PrePorcGastosMensualesDeleteDto> result = new ResultDto<PrePorcGastosMensualesDeleteDto>(null);
            try
            {

                var codigoPorGastosMes = await _repository.GetByCodigo(dto.CodigoPorGastosMes);
                if (codigoPorGastosMes == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Por Gastos Mes no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPorGastosMes);

                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }
    }
}
