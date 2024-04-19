using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreMetasService : IPreMetasService
    {
        private readonly IPreMetasRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _preSUPUESTOSRepository;
        private readonly IPreDescriptivaRepository _preDescriptivaRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreMetasService(IPreMetasRepository repository,
                                      IPRE_PRESUPUESTOSRepository preSUPUESTOSRepository,
                                      IPreDescriptivaRepository preDescriptivaRepository,
                                      ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _preSUPUESTOSRepository = preSUPUESTOSRepository;
            _preDescriptivaRepository = preDescriptivaRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PreMetasResponseDto>>> GetAll()
        {

            ResultDto<List<PreMetasResponseDto>> result = new ResultDto<List<PreMetasResponseDto>>(null);
            try
            {

                var Metas = await _repository.GetAll();



                if (Metas.Count() > 0)
                {
                    List<PreMetasResponseDto> listDto = new List<PreMetasResponseDto>();

                    foreach (var item in Metas)
                    {
                        var dto = await MapPreMetas(item);
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

       
        public async Task<PreMetasResponseDto> MapPreMetas(PRE_METAS dto)
        {
            PreMetasResponseDto itemResult = new PreMetasResponseDto();
            itemResult.CodigoMeta = dto.CODIGO_META;
            itemResult.CodigoProyecto = dto.CODIGO_PROYECTO;
            itemResult.NumeroMeta = dto.NUMERO_META; 
            itemResult.DenominacionMeta = dto.DENOMINACION_META;
            itemResult.UnidadMedidaId = dto.UNIDAD_MEDIDA_ID;
            itemResult.CantidadMeta = dto.CANTIDAD_META;
            itemResult.CostoMeta = dto.COSTO_META;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
            itemResult.CantidadPrimerTrimestre = dto.CANTIDAD_PRIMER_TRIMESTRE;
            itemResult.CantidadSegundoTrimestre = dto.CANTIDAD_SEGUNDO_TRIMESTRE;
            itemResult.CantidadTercerTrimestre = dto.CANTIDAD_TERCER_TRIMESTRE;
            itemResult.CantidadCuartoTrimestre = dto.CANTIDAD_CUARTO_TRIMESTRE;
           
            return itemResult;

        }

        public async Task<List<PreMetasResponseDto>> MapListPreMetasDto(List<PRE_METAS> dtos)
        {
            List<PreMetasResponseDto> result = new List<PreMetasResponseDto>();


            foreach (var item in dtos)
            {

                PreMetasResponseDto itemResult = new PreMetasResponseDto();

                itemResult = await MapPreMetas(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PreMetasResponseDto>> Update(PreMetasUpdateDto dto)
        {

            ResultDto<PreMetasResponseDto> result = new ResultDto<PreMetasResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoMeta = await _repository.GetByCodigo(dto.CodigoMeta);
                if (codigoMeta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Meta no existe";
                    return result;
                }
                
                if (dto.CodigoProyecto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proyecto Invalido";
                    return result;
                }

                
                if (dto.NumeroMeta < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Meta Invalido";
                    return result;
                }
                if (dto.DenominacionMeta.Length > 300)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Meta Invalida";
                    return result;
                }
                var unidadMedidaId = await _preDescriptivaRepository.GetByIdAndTitulo(5, dto.UnidadMedidaId);
                if (dto.UnidadMedidaId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad de medida Invalida";
                    return result;
                }

              
                if (dto.CantidadMeta < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "cantidad meta Invalida";
                    return result;
                }

                if (dto.CostoMeta < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Costo Meta Invalido";
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

                var codigoPresupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                if(dto.CantidadPrimerTrimestre < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Primer Trimestre Invalido";
                    return result;

                }

                if (dto.CantidadSegundoTrimestre < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Segundo Trimestre Invalido";
                    return result;

                }
                if (dto.CantidadTercerTrimestre < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Tercer Trimestre Invalido";
                    return result;

                }

                if (dto.CantidadCuartoTrimestre < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Cuarto Trimestre Invalido";
                    return result;

                }


                codigoMeta.CODIGO_META = dto.CodigoMeta;
                codigoMeta.CODIGO_PROYECTO = dto.CodigoProyecto;
                codigoMeta.NUMERO_META = dto.NumeroMeta;
                codigoMeta.DENOMINACION_META = dto.DenominacionMeta;
                codigoMeta.UNIDAD_MEDIDA_ID = dto.UnidadMedidaId;
                codigoMeta.CANTIDAD_META = dto.CantidadMeta;
                codigoMeta.COSTO_META = dto.CostoMeta;
                codigoMeta.EXTRA1 = dto.Extra1;
                codigoMeta.EXTRA2 = dto.Extra2;
                codigoMeta.EXTRA3 = dto.Extra3;
                codigoMeta.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoMeta.CANTIDAD_PRIMER_TRIMESTRE = dto.CantidadPrimerTrimestre;
                codigoMeta.CANTIDAD_SEGUNDO_TRIMESTRE = dto.CantidadSegundoTrimestre;
                codigoMeta.CANTIDAD_TERCER_TRIMESTRE = dto.CantidadTercerTrimestre;
                codigoMeta.CANTIDAD_CUARTO_TRIMESTRE = dto.CantidadCuartoTrimestre;



                codigoMeta.CODIGO_EMPRESA = conectado.Empresa;
                codigoMeta.USUARIO_UPD = conectado.Usuario;
                codigoMeta.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoMeta);

                var resultDto = await MapPreMetas(codigoMeta);
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

        public async Task<ResultDto<PreMetasResponseDto>> Create(PreMetasUpdateDto dto)
        {

            ResultDto<PreMetasResponseDto> result = new ResultDto<PreMetasResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoMeta = await _repository.GetByCodigo(dto.CodigoMeta);
                if (codigoMeta != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Meta no existe";
                    return result;
                }

                if (dto.CodigoProyecto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proyecto Invalido";
                    return result;
                }


                if (dto.NumeroMeta < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Meta Invalido";
                    return result;
                }
                if (dto.DenominacionMeta.Length > 300)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Meta Invalida";
                    return result;
                }
                var unidadMedidaId = await _preDescriptivaRepository.GetByIdAndTitulo(5, dto.UnidadMedidaId);
                if (dto.UnidadMedidaId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad de medida Invalida";
                    return result;
                }


                if (dto.CantidadMeta < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "cantidad meta Invalida";
                    return result;
                }

                if (dto.CostoMeta < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Costo Meta Invalido";
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

                var codigoPresupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                if (dto.CantidadPrimerTrimestre < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Primer Trimestre Invalido";
                    return result;

                }

                if (dto.CantidadSegundoTrimestre < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Segundo Trimestre Invalido";
                    return result;

                }
                if (dto.CantidadTercerTrimestre < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Tercer Trimestre Invalido";
                    return result;

                }

                if (dto.CantidadCuartoTrimestre < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Cuarto Trimestre Invalido";
                    return result;

                }



                PRE_METAS entity = new PRE_METAS();
                entity.CODIGO_META = await _repository.GetNextKey();
                entity.CODIGO_PROYECTO = dto.CodigoProyecto;
                entity.NUMERO_META = dto.NumeroMeta;
                entity.DENOMINACION_META = dto.DenominacionMeta;
                entity.UNIDAD_MEDIDA_ID = dto.UnidadMedidaId;
                entity.CANTIDAD_META = dto.CantidadMeta;
                entity.COSTO_META = dto.CostoMeta;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.CANTIDAD_PRIMER_TRIMESTRE = dto.CantidadPrimerTrimestre;
                entity.CANTIDAD_SEGUNDO_TRIMESTRE = dto.CantidadSegundoTrimestre;
                entity.CANTIDAD_TERCER_TRIMESTRE = dto.CantidadTercerTrimestre;
                entity.CANTIDAD_CUARTO_TRIMESTRE = dto.CantidadCuartoTrimestre;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreMetas(created.Data);
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

        public async Task<ResultDto<PreMetasDeleteDto>> Delete(PreMetasDeleteDto dto)
        {

            ResultDto<PreMetasDeleteDto> result = new ResultDto<PreMetasDeleteDto>(null);
            try
            {

                var codigoMeta = await _repository.GetByCodigo(dto.CodigoMeta);
                if (codigoMeta == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Meta no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoMeta);

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
