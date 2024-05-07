using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreProyectosInversionService : IPreProyectosInversionService
    {
        private readonly IPreProyectosInversionRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPreDescriptivaRepository _pRE_PREDESCriptivaRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _pRE_INDICE_CAT_PRGRepository;

        public PreProyectosInversionService(IPreProyectosInversionRepository repository,
                                   ISisUsuarioRepository sisUsuarioRepository,
                                   IPreDescriptivaRepository pRE_PREDESCriptivaRepository,
                                   IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                   IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _pRE_PREDESCriptivaRepository = pRE_PREDESCriptivaRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _pRE_INDICE_CAT_PRGRepository = pRE_INDICE_CAT_PRGRepository;
        }


        public async Task<ResultDto<List<PreProyectosInversionResponseDto>>> GetAll()
        {

            ResultDto<List<PreProyectosInversionResponseDto>> result = new ResultDto<List<PreProyectosInversionResponseDto>>(null);
            try
            {

                var preProyectosInversion = await _repository.GetAll();



                if (preProyectosInversion.Count() > 0)
                {
                    List<PreProyectosInversionResponseDto> listDto = new List<PreProyectosInversionResponseDto>();

                    foreach (var item in preProyectosInversion)
                    {
                        var dto = await MapPreProyectosInversion(item);
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

        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);

            return FechaDesdeObj;
        }
        public async Task<PreProyectosInversionResponseDto> MapPreProyectosInversion(PRE_PROYECTOS_INVERSION dto)
        {
            PreProyectosInversionResponseDto itemResult = new PreProyectosInversionResponseDto();
            itemResult.CodigoProyectoInv = dto.CODIGO_PROYECTO_INV;
            itemResult.Ano = itemResult.Ano;
            itemResult.Escenario = dto.ESCENARIO;
            itemResult.CodigoIcp = dto.CODIGO_ICP;
            itemResult.FinanciadoId = dto.FINANCIADO_ID;
            itemResult.CodigoObra = dto.CODIGO_OBRA;
            itemResult.Denominacion = dto.DENOMINACION;
            itemResult.CodigoFuncionario = dto.CODIGO_FUNCIONARIO;
            itemResult.FechaIni = dto.FECHA_INI;
            itemResult.FechaIniString = dto.FECHA_INI.ToString();
            FechaDto fechaIniObj = GetFechaDto(dto.FECHA_INI);
            itemResult.FechaIniObj=(FechaDto)fechaIniObj;
            itemResult.FechaFin = dto.FECHA_FIN;
            itemResult.FechaFinString = dto.FECHA_INI.ToString();
            FechaDto fechaFinObj = GetFechaDto(dto.FECHA_FIN);
            itemResult.FechaFinObj = (FechaDto)fechaFinObj;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;

            return itemResult;
        }

        public async Task<List<PreProyectosInversionResponseDto>> MapListPreProyectosInversion(List<PRE_PROYECTOS_INVERSION> dtos)
        {
            List<PreProyectosInversionResponseDto> result = new List<PreProyectosInversionResponseDto>();

            foreach (var item in dtos)
            {
                PreProyectosInversionResponseDto itemResult = new PreProyectosInversionResponseDto();
                itemResult = await MapPreProyectosInversion(item);
                result.Add(itemResult);
            }

            return result;

        }

        public async Task<ResultDto<PreProyectosInversionResponseDto>> Update(PreProyectosInversionUpdateDto dto)
        {

            ResultDto<PreProyectosInversionResponseDto> result = new ResultDto<PreProyectosInversionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoProyectoInv < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proyecto Inv no existe";
                    return result;

                }
                var codigoProyectoInv = await _repository.GetByCodigo(dto.CodigoProyectoInv);
                if (codigoProyectoInv == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proyecto Inv no existe";
                    return result;
                }

                if(dto.Ano < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano Invalido";
                    return result;
                }

                if(dto.Escenario < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Escenario Invalido";
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

                if(dto.FinanciadoId < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id Invalido";
                    return result;
                }

                var financiadoId = await _pRE_PREDESCriptivaRepository.GetByIdAndTitulo(3, dto.FinanciadoId);
                if(financiadoId == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id Invalido";
                    return result;

                }

                if(dto.CodigoObra.Length > 20) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Obra Invalido";
                    return result;

                }

                if(dto.CodigoFuncionario < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Obra Invalido";
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

                codigoProyectoInv.CODIGO_PROYECTO_INV = dto.CodigoProyectoInv;
                codigoProyectoInv.ANO = dto.Ano;
                codigoProyectoInv.ESCENARIO = dto.Escenario;
                codigoProyectoInv.CODIGO_ICP = dto.CodigoIcp;
                codigoProyectoInv.FINANCIADO_ID = dto.FinanciadoId;
                codigoProyectoInv.CODIGO_OBRA = dto.CodigoObra;
                codigoProyectoInv.DENOMINACION = dto.Denominacion;
                codigoProyectoInv.CODIGO_FUNCIONARIO = dto.CodigoFuncionario;
                codigoProyectoInv.FECHA_INI = dto.FechaIni;
                codigoProyectoInv.FECHA_FIN = dto.FechaFin;
                codigoProyectoInv.SITUACION_ID = dto.SituacionId;
                codigoProyectoInv.COSTO = dto.Costo;
                codigoProyectoInv.COMPROMISO_ANTERIOR = dto.CompromisoAnterior;
                codigoProyectoInv.COMPROMISO_VIGENTE = dto.CompromisoVigente;
                codigoProyectoInv.EJECUTADO_ANTERIOR = dto.EjecutadoAnterior;
                codigoProyectoInv.EJECUTADO_VIGENTE = dto.EjecutadoVigente;
                codigoProyectoInv.ESTIMADA_PRESUPUESTO = dto.EstimadaPresupuesto;
                codigoProyectoInv.ESTIMADA_POSTERIOR = dto.EstimadaPosterior;
                codigoProyectoInv.EXTRA1 = dto.Extra1;
                codigoProyectoInv.EXTRA2 = dto.Extra2;
                codigoProyectoInv.EXTRA3 = dto.Extra3;
                codigoProyectoInv.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoProyectoInv.FUNCIONARIO = dto.Funcionario;



                codigoProyectoInv.CODIGO_EMPRESA = conectado.Empresa;
                codigoProyectoInv.USUARIO_UPD = conectado.Usuario;
                codigoProyectoInv.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoProyectoInv);

                var resultDto = await MapPreProyectosInversion(codigoProyectoInv);
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

        public async Task<ResultDto<PreProyectosInversionResponseDto>> Create (PreProyectosInversionUpdateDto dto)
        {

            ResultDto<PreProyectosInversionResponseDto> result = new ResultDto<PreProyectosInversionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano Invalido";
                    return result;
                }

                if (dto.Escenario < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Escenario Invalido";
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

                if (dto.FinanciadoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id Invalido";
                    return result;
                }

                var financiadoId = await _pRE_PREDESCriptivaRepository.GetByIdAndTitulo(3, dto.FinanciadoId);
                if (financiadoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id Invalido";
                    return result;

                }

                if (dto.CodigoObra.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Obra Invalido";
                    return result;

                }

                if (dto.CodigoFuncionario < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Obra Invalido";
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



                PRE_PROYECTOS_INVERSION entity = new PRE_PROYECTOS_INVERSION();
                entity.CODIGO_PROYECTO_INV = await _repository.GetNextKey();
                entity.ANO = dto.Ano;
                entity.ESCENARIO = dto.Escenario;
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.FINANCIADO_ID = dto.FinanciadoId;
                entity.CODIGO_OBRA = dto.CodigoObra;
                entity.DENOMINACION = dto.Denominacion;
                entity.CODIGO_FUNCIONARIO = dto.CodigoFuncionario;
                entity.FECHA_INI = dto.FechaIni;
                entity.FECHA_FIN = dto.FechaFin;
                entity.SITUACION_ID = dto.SituacionId;
                entity.COSTO = dto.Costo;
                entity.COMPROMISO_ANTERIOR = dto.CompromisoAnterior;
                entity.COMPROMISO_VIGENTE = dto.CompromisoVigente;
                entity.EJECUTADO_ANTERIOR = dto.EjecutadoAnterior;
                entity.EJECUTADO_VIGENTE = dto.EjecutadoVigente;
                entity.ESTIMADA_PRESUPUESTO = dto.EstimadaPresupuesto;
                entity.ESTIMADA_POSTERIOR = dto.EstimadaPosterior;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.FUNCIONARIO = dto.Funcionario;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreProyectosInversion(created.Data);
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

        public async Task<ResultDto<PreProyectosInversionDeleteDto>> Delete(PreProyectosInversionDeleteDto dto)
        {

            ResultDto<PreProyectosInversionDeleteDto> result = new ResultDto<PreProyectosInversionDeleteDto>(null);
            try
            {

                var codigoProyectoInv = await _repository.GetByCodigo(dto.CodigoProyectoInv);
                if (codigoProyectoInv == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Proyecto Inv no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoProyectoInv);

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