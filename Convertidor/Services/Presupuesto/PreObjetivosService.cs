using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreObjetivosService : IPreObjetivosService
    {
        private readonly IPreObjetivosRepository _repository;
        private readonly IPRE_INDICE_CAT_PRGRepository _pRE_INDICE_CAT_PRGRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreObjetivosService(IPreObjetivosRepository repository,
                                   IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository,
                                   IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                         ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _pRE_INDICE_CAT_PRGRepository = pRE_INDICE_CAT_PRGRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PreObjetivosResponseDto>>> GetAll()
        {

            ResultDto<List<PreObjetivosResponseDto>> result = new ResultDto<List<PreObjetivosResponseDto>>(null);
            try
            {

                var preObjetivos = await _repository.GetAll();



                if (preObjetivos.Count() > 0)
                {
                    List<PreObjetivosResponseDto> listDto = new List<PreObjetivosResponseDto>();

                    foreach (var item in preObjetivos)
                    {
                        var dto = await MapPreObjetivos(item);
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

       
        public async Task<PreObjetivosResponseDto> MapPreObjetivos(PRE_OBJETIVOS dto)
        {
            PreObjetivosResponseDto itemResult = new PreObjetivosResponseDto();
            itemResult.CodigoObjetivo = dto.CODIGO_OBJETIVO;
            itemResult.CodigoIcp = dto.CODIGO_ICP;
            itemResult.NumeroObjetivo = dto.NUMERO_OBJETIVO;
            itemResult.DenominacionObjetivo = dto.DENOMINACION_OBJETIVO;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
           

            return itemResult;

        }

        public async Task<List<PreObjetivosResponseDto>> MapListPreObjetivos(List<PRE_OBJETIVOS> dtos)
        {
            List<PreObjetivosResponseDto> result = new List<PreObjetivosResponseDto>();


            foreach (var item in dtos)
            {

                PreObjetivosResponseDto itemResult = new PreObjetivosResponseDto();

                itemResult = await MapPreObjetivos(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PreObjetivosResponseDto>> Update(PreObjetivosUpdateDto dto)
        {

            ResultDto<PreObjetivosResponseDto> result = new ResultDto<PreObjetivosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();



                if (dto.CodigoObjetivo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Objetivo no existe";
                    return result;

                }

                var codigoObjetivo = await _repository.GetByCodigo(dto.CodigoObjetivo);
                if (codigoObjetivo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Objetivo no existe";
                    return result;

                }
                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                var codigoIcp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                if (dto.NumeroObjetivo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Objetivo Invalido";
                    return result;
                }

                if (dto.DenominacionObjetivo.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Objetivo Invalido";
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

                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;

                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }


            


                codigoObjetivo.CODIGO_OBJETIVO = dto.CodigoObjetivo;
                codigoObjetivo.CODIGO_ICP = dto.CodigoIcp;
                codigoObjetivo.NUMERO_OBJETIVO = dto.NumeroObjetivo;
                codigoObjetivo.DENOMINACION_OBJETIVO = dto.DenominacionObjetivo;
                codigoObjetivo.EXTRA1 = dto.Extra1;
                codigoObjetivo.EXTRA2 = dto.Extra2;
                codigoObjetivo.EXTRA3 = dto.Extra3;
                codigoObjetivo.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;




                codigoObjetivo.CODIGO_EMPRESA = conectado.Empresa;
                codigoObjetivo.USUARIO_UPD = conectado.Usuario;
                codigoObjetivo.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoObjetivo);

                var resultDto = await MapPreObjetivos(codigoObjetivo);
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

        public async Task<ResultDto<PreObjetivosResponseDto>> Create(PreObjetivosUpdateDto dto)
        {

            ResultDto<PreObjetivosResponseDto> result = new ResultDto<PreObjetivosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                var codigoIcp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                if (dto.NumeroObjetivo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Objetivo Invalido";
                    return result;
                }

                if (dto.DenominacionObjetivo.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Objetivo Invalido";
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

                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;

                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }





                PRE_OBJETIVOS entity = new PRE_OBJETIVOS();
                entity.CODIGO_OBJETIVO = await _repository.GetNextKey();
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.NUMERO_OBJETIVO = dto.NumeroObjetivo;
                entity.DENOMINACION_OBJETIVO = dto.DenominacionObjetivo;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreObjetivos(created.Data);
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

        public async Task<ResultDto<PreObjetivosDeleteDto>> Delete(PreObjetivosDeleteDto dto)
        {

            ResultDto<PreObjetivosDeleteDto> result = new ResultDto<PreObjetivosDeleteDto>(null);
            try
            {

                var codigoObjetivo = await _repository.GetByCodigo(dto.CodigoObjetivo);
                if (codigoObjetivo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Objetivo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoObjetivo);

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
