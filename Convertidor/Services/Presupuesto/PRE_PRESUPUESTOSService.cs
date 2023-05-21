using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Catastro;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_PRESUPUESTOSService: IPRE_PRESUPUESTOSService
    {
		

        private readonly IPRE_PRESUPUESTOSRepository _repository;
        private readonly IPRE_V_DENOMINACION_PUCRepository _preDenominacionPucRepository;

        private readonly IMapper _mapper;

        public PRE_PRESUPUESTOSService(IPRE_PRESUPUESTOSRepository repository,
                                        IPRE_V_DENOMINACION_PUCRepository preDenominacionPucRepository,
                                      
                                      IMapper mapper)
        {
            _repository = repository;
            _preDenominacionPucRepository = preDenominacionPucRepository;

            _mapper = mapper;
        }





        public async Task<ResultDto<GetPRE_PRESUPUESTOSDto>> GetByCodigo(FilterPRE_PRESUPUESTOSDto filter)
        {

            ResultDto<GetPRE_PRESUPUESTOSDto> result = new ResultDto<GetPRE_PRESUPUESTOSDto>(null);
            try
            {
                var presupuesto = await _repository.GetByCodigo(filter.CodigoEmpresa, filter.CodigoPresupuesto);
                if (presupuesto != null)
                {
                    var dto = await MapPrePresupuestoToGetPrePresupuestoDto(presupuesto);
                    result.Data = dto;
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

        public async Task<ResultDto<List<ListPresupuestoDto>>> GetListPresupuesto()
        {

            ResultDto<List<ListPresupuestoDto>> result = new ResultDto<List<ListPresupuestoDto>>(null);
            try
            {
                var presupuesto = await _repository.GetAll();
                if (presupuesto.Count() > 0)
                {
                    List<ListPresupuestoDto> listDto = new List<ListPresupuestoDto>();

                    foreach (var item in presupuesto.OrderByDescending(x => x.FECHA_HASTA).ToList())
                    {
                        ListPresupuestoDto dto = new ListPresupuestoDto();
                        dto.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
                        dto.Descripcion = item.DENOMINACION;
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


        public async Task<ResultDto<List<GetPRE_PRESUPUESTOSDto>>> GetAll(FilterPRE_PRESUPUESTOSDto filter)
        {

            ResultDto<List<GetPRE_PRESUPUESTOSDto>> result = new ResultDto<List<GetPRE_PRESUPUESTOSDto>>(null);
            try
            {
                var presupuesto = await _repository.GetAll();
                if (presupuesto.Count() >0)
                {
                    List< GetPRE_PRESUPUESTOSDto> listDto = new List<GetPRE_PRESUPUESTOSDto>();

                    foreach (var item in presupuesto)
                    {
                        var dto = await MapPrePresupuestoToGetPrePresupuestoDto(item);
                        listDto.Add(dto);
                    }

                   
                    result.Data = listDto.OrderByDescending(x=>x.FechaHasta).ToList();
                    
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


        public async Task<ResultDto<GetPRE_PRESUPUESTOSDto>> Create(CreatePRE_PRESUPUESTOSDto dto)
        {

            ResultDto<GetPRE_PRESUPUESTOSDto> result = new ResultDto<GetPRE_PRESUPUESTOSDto>(null);
            try
            {

                if (dto.Denominacion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }
                if (dto.Ano <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo Presupuesto  Invalido";
                    return result;
                }
              
                var existePeriodo = await _repository.ExisteEnPeriodo(dto.CodigoEmpresa,dto.FechaDesde,dto.FechaHasta);
                if (existePeriodo)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo existe en el presupuesto";
                    return result;
                }

                var entity = MapCreatePrePresupuestoDtoToPrePresupuesto(dto);
                entity.CODIGO_PRESUPUESTO = await _repository.GetNextKey();  
                var created = await _repository.Add(entity);
                var resultDto = await MapPrePresupuestoToGetPrePresupuestoDto(created.Data);
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

        public async Task<ResultDto<GetPRE_PRESUPUESTOSDto>> Update(UpdatePRE_PRESUPUESTOSDto dto)
        {

            ResultDto<GetPRE_PRESUPUESTOSDto> result = new ResultDto<GetPRE_PRESUPUESTOSDto>(null);
            try
            {

                var presupuesto = await _repository.GetByCodigo(dto.CodigoEmpresa, dto.CodigoPresupuesto);
                if (presupuesto==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }
                if (dto.Denominacion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }
                if (dto.Ano <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo Presupuesto  Invalido";
                    return result;
                }
                /*if (dto.MontoPresupuesto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Presupuesto  Invalido";
                    return result;
                }*/


               
                presupuesto.DENOMINACION = dto.Denominacion;
                presupuesto.DESCRIPCION = dto.Descripcion;
                presupuesto.ANO = dto.Ano;
                presupuesto.MONTO_PRESUPUESTO = dto.MontoPresupuesto;
                presupuesto.FECHA_DESDE = dto.FechaDesde;
                presupuesto.FECHA_HASTA = dto.FechaHasta;
                presupuesto.FECHA_APROBACION = dto.FechaAprobacion;
                presupuesto.NUMERO_ORDENANZA = dto.NumeroOrdenanza;
                presupuesto.FECHA_ORDENANZA = dto.FechaOrdenanza;
                presupuesto.EXTRA1 = dto.Extra1;
                presupuesto.EXTRA2 = dto.Extra2;
                presupuesto.EXTRA3 = dto.Extra3;
                presupuesto.USUARIO_UPD = dto.UsuarioUpd;
                presupuesto.FECHA_UPD = DateTime.Now;
               
                await _repository.Update(presupuesto);
                var resultDto = await MapPrePresupuestoToGetPrePresupuestoDto(presupuesto);
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
        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(month.Length - 2);
    
            return FechaDesdeObj;
        }

        public async Task<GetPRE_PRESUPUESTOSDto> MapPrePresupuestoToGetPrePresupuestoDto(PRE_PRESUPUESTOS entity)
        {
            GetPRE_PRESUPUESTOSDto dto = new GetPRE_PRESUPUESTOSDto();
            dto.CodigoPresupuesto = entity.CODIGO_PRESUPUESTO;
            dto.Denominacion = entity.DENOMINACION;
            dto.Descripcion = entity.DESCRIPCION ?? "";
            dto.Ano = entity.ANO;
            dto.MontoPresupuesto = entity.MONTO_PRESUPUESTO;
            dto.FechaDesde = entity.FECHA_DESDE.ToString("u");          
            dto.FechaHasta = entity.FECHA_HASTA.ToString("u");  
            dto.FechaAprobacion= entity.FECHA_APROBACION.ToString("u");
            dto.NumeroOrdenanza = entity.NUMERO_ORDENANZA;

            dto.FechaOrdenanza = entity.FECHA_ORDENANZA.ToString("u");

            FechaDto FechaDesdeObj = GetFechaDto(entity.FECHA_DESDE);
            dto.FechaDesdeObj = (FechaDto)FechaDesdeObj;

            FechaDto FechaHastaObj = GetFechaDto(entity.FECHA_HASTA);
            dto.FechaHastaObj = (FechaDto)FechaHastaObj;

            FechaDto FechaAprobacionObj = GetFechaDto(entity.FECHA_APROBACION);
            dto.FechaAprobacionObj = (FechaDto)FechaAprobacionObj;

            FechaDto FechaOrdenanzaObj = GetFechaDto(entity.FECHA_ORDENANZA);
            dto.FechaOrdenanzaObj = (FechaDto)FechaOrdenanzaObj;
       

            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra2 = entity.EXTRA3;
            dto.TotalPresupuesto = 0;
            dto.TotalDisponible = 0;
            dto.TotalPresupuestoString = "";
            dto.TotalDisponibleString = "";
            var preDenominacionPuc = await _preDenominacionPucRepository.GetByCodigoPresupuesto(dto.CodigoPresupuesto);
            List<GetPRE_V_DENOMINACION_PUCDto> listpreDenominacionPuc = new List<GetPRE_V_DENOMINACION_PUCDto>();
            if (preDenominacionPuc.Count() > 0)
            {
                foreach (var item in preDenominacionPuc)
                {
                    GetPRE_V_DENOMINACION_PUCDto itemPreDenominacionPuc = new GetPRE_V_DENOMINACION_PUCDto();
                    itemPreDenominacionPuc.AnoSaldo = item.ANO_SALDO;
                    itemPreDenominacionPuc.MesSaldo = item.MES_SALDO;
                    itemPreDenominacionPuc.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
                    itemPreDenominacionPuc.CodigoPartida = item.CODIGO_PARTIDA;
                    itemPreDenominacionPuc.CodigoGenerica = item.CODIGO_GENERICA;
                    itemPreDenominacionPuc.CodigoEspecifica = item.CODIGO_ESPECIFICA;
                    itemPreDenominacionPuc.CodigoSubEspecifica = item.CODIGO_SUBESPECIFICA;
                    itemPreDenominacionPuc.CodigoNivel5 = item.CODIGO_NIVEL5;
                    itemPreDenominacionPuc.DenominacionPuc = item.DENOMINACION_PUC;
                    itemPreDenominacionPuc.Presupuestado = item.PRESUPUESTADO;
                    itemPreDenominacionPuc.Modificado = item.MODIFICADO;
                    itemPreDenominacionPuc.Comprometido = item.COMPROMETIDO;
                    itemPreDenominacionPuc.Causado = item.CAUSADO;
                    itemPreDenominacionPuc.Modificado = item.MODIFICADO;
                    itemPreDenominacionPuc.Pagado = item.PAGADO;
                    itemPreDenominacionPuc.Deuda = item.DEUDA;
                    itemPreDenominacionPuc.Disponibilidad = item.DISPONIBILIDAD;
                    itemPreDenominacionPuc.DisponibilidadFinan = item.DISPONIBILIDAD_FINAN;
                   
                    listpreDenominacionPuc.Add(itemPreDenominacionPuc);
                }

                // if (dto.TotalPresupuesto > 1000) dto.TotalPresupuesto = dto.TotalPresupuesto / 1000;
                //if (dto.TotalDisponible > 1000) dto.TotalDisponible = dto.TotalDisponible / 1000;
             

                dto.PreDenominacionPuc = listpreDenominacionPuc;

                var preDenominacionPucresumen = ResumenePreDenominacionPuc(listpreDenominacionPuc);
                if (preDenominacionPucresumen.Count > 0) { 
                    dto.PreDenominacionPucResumen = preDenominacionPucresumen;
                    var resumen = from s in preDenominacionPucresumen.OrderBy(x => x.CodigoPresupuesto).ToList()
                                  group s by new { CodigoPresupuesto = s.CodigoPresupuesto} into g
                                  select new
                                  {
                                      g.Key.CodigoPresupuesto,
                                      Presupuestado = g.Sum(s => s.Presupuestado),
                                      Modificado = g.Sum(s => s.Modificado),
                                      Comprometido = g.Sum(s => s.Comprometido),
                                      Causado = g.Sum(s => s.Causado),
                                      Pagado = g.Sum(s => s.Pagado),
                                      Deuda = g.Sum(s => s.Deuda),
                                      Disponibilidad = g.Sum(s => s.Disponibilidad),
                                      DisponibilidadFinan = g.Sum(s => s.DisponibilidadFinan)
                                  };
                    if (resumen!=null)
                    {
                        var itemResumen = resumen.FirstOrDefault();
                        dto.TotalPresupuesto = itemResumen.Presupuestado;
                        dto.TotalDisponible = itemResumen.Disponibilidad;
                    }
                    
                    if (dto.TotalPresupuesto > 0)
                    {
                        dto.TotalPresupuestoString = dto.TotalPresupuesto.ToString("#,#", CultureInfo.InvariantCulture);
                    }
                    if (dto.TotalDisponible > 0)
                    {
                        dto.TotalDisponibleString = dto.TotalDisponible.ToString("#,#", CultureInfo.InvariantCulture);
                    }


                }


            }
            

            return dto;

        }
        public List<GetPreDenominacionPucResumenAnoDto> ResumenePreDenominacionPuc(List<GetPRE_V_DENOMINACION_PUCDto> dto)
        {
            List<GetPreDenominacionPucResumenAnoDto> result = new List<GetPreDenominacionPucResumenAnoDto>();
            if (dto.Count > 0)
            {


                var resumen = from s in dto.OrderBy(x => x.CodigoPresupuesto).ToList()
                        group s by new { CodigoPresupuesto = s.CodigoPresupuesto, DenominacionPuc = s.DenominacionPuc} into g
                        select new
                        {
                            g.Key.CodigoPresupuesto,
                            g.Key.DenominacionPuc,
                          
                            Presupuestado = g.Sum(s => s.Presupuestado),
                            Modificado = g.Sum(s => s.Modificado),
                            Comprometido = g.Sum(s => s.Comprometido),
                            Causado = g.Sum(s => s.Causado),
                            Pagado = g.Sum(s => s.Pagado),
                            Deuda = g.Sum(s => s.Deuda),
                            Disponibilidad = g.Sum(s => s.Disponibilidad),
                            DisponibilidadFinan = g.Sum(s => s.DisponibilidadFinan)
                        };


                foreach (var item in dto)
                {
                    var getPreDenominacionPucResumenAnoDto = result
                               .Where(x => x.CodigoPresupuesto == item.CodigoPresupuesto && x.DenominacionPuc.Trim()==item.DenominacionPuc.Trim()).FirstOrDefault();

                    if (getPreDenominacionPucResumenAnoDto == null)
                    {
                        GetPreDenominacionPucResumenAnoDto itemResult = new GetPreDenominacionPucResumenAnoDto();
                        itemResult.AnoSaldo = item.AnoSaldo;
                        itemResult.CodigoPresupuesto = item.CodigoPresupuesto;
                        itemResult.CodigoPartida = item.CodigoPartida;
                        itemResult.CodigoGenerica = item.CodigoGenerica;
                        itemResult.CodigoEspecifica = item.CodigoEspecifica;
                        itemResult.CodigoSubEspecifica = item.CodigoSubEspecifica;
                        itemResult.CodigoNivel5 = item.CodigoNivel5;
                        itemResult.DenominacionPuc = item.DenominacionPuc;

                        itemResult.Presupuestado = 0;
                        itemResult.Modificado = 0;
                        itemResult.Comprometido = 0;
                        itemResult.Causado = 0;
                        itemResult.Pagado =0;
                        itemResult.Deuda = 0;
                        itemResult.Disponibilidad = 0;
                        itemResult.DisponibilidadFinan = 0;

                        result.Add(itemResult);

                    }             

                }

                foreach (var item in result)
                {
                    var getItem = resumen
                              .Where(x => x.CodigoPresupuesto == item.CodigoPresupuesto && x.DenominacionPuc.Trim() == item.DenominacionPuc.Trim()).FirstOrDefault();

                    if(getItem != null)
                    {
                        item.Presupuestado += getItem.Presupuestado;
                        item.Modificado += getItem.Modificado;
                        item.Comprometido += getItem.Comprometido;
                        item.Causado += getItem.Causado;
                        item.Pagado += getItem.Pagado;
                        item.Deuda += getItem.Deuda;
                        item.Disponibilidad += getItem.Disponibilidad;
                        item.DisponibilidadFinan += getItem.DisponibilidadFinan;
                    }
                }
             
                

            }



            return result;



        }

        public PRE_PRESUPUESTOS MapCreatePrePresupuestoDtoToPrePresupuesto(CreatePRE_PRESUPUESTOSDto dto)
        {
            PRE_PRESUPUESTOS entity = new PRE_PRESUPUESTOS();

           
            entity.DENOMINACION = dto.Denominacion;
            entity.DESCRIPCION = dto.Descripcion;
            entity.ANO = dto.Ano;
            entity.MONTO_PRESUPUESTO = dto.MontoPresupuesto;
            entity.FECHA_DESDE = dto.FechaDesde;
            entity.FECHA_HASTA = dto.FechaHasta;
            entity.FECHA_APROBACION = dto.FechaAprobacion;
            entity.NUMERO_ORDENANZA = dto.NumeroOrdenanza;
            entity.FECHA_ORDENANZA = dto.FechaOrdenanza;
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.USUARIO_INS = dto.UsuarioIns;
            entity.FECHA_INS = DateTime.Now;
            entity.CODIGO_EMPRESA = dto.CodigoEmpresa;

        

            return entity;




        }

        public PRE_PRESUPUESTOS MapUpdatePrePresupuestoDtoToPrePresupuesto(UpdatePRE_PRESUPUESTOSDto dto)
        {
            PRE_PRESUPUESTOS entity = new PRE_PRESUPUESTOS();


            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            entity.DENOMINACION = dto.Denominacion;
            entity.DESCRIPCION = dto.Descripcion;
            entity.ANO = dto.Ano;
            entity.MONTO_PRESUPUESTO = dto.MontoPresupuesto;
            entity.FECHA_DESDE = dto.FechaDesde;
            entity.FECHA_HASTA = dto.FechaHasta;
            entity.FECHA_APROBACION = dto.FechaAprobacion;
            entity.NUMERO_ORDENANZA = dto.NumeroOrdenanza;
            entity.FECHA_ORDENANZA = dto.FechaOrdenanza;
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.USUARIO_UPD = dto.UsuarioUpd;
            entity.CODIGO_EMPRESA = dto.CodigoEmpresa;

            return entity;




        }





    }
}

