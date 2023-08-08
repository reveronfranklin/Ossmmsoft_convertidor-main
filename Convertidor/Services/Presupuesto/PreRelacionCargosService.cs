using System;
using System.Collections.Generic;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Rh;
using Ganss.Excel;
using NuGet.Packaging;

namespace Convertidor.Services.Presupuesto
{
	public class PreRelacionCargosService: IPreRelacionCargosService
    {

      
        private readonly IPRE_RELACION_CARGOSRepository _repository;
        private readonly IPreCargosRepository _preCargoRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestoRepository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IPRE_RELACION_CARGOSRepository _preRelacionCargoRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _preICPRepository;
        private readonly IConfiguration _configuration;
        public PreRelacionCargosService(IPRE_RELACION_CARGOSRepository repository,
                                        IPreCargosRepository preCargoRepository,
                                        IPRE_PRESUPUESTOSRepository prePresupuestoRepository,
                                      IConfiguration configuration,
                                      IPreDescriptivaRepository repositoryPreDescriptiva,
                                      IPRE_INDICE_CAT_PRGRepository preICPRepository,
                                      IPRE_RELACION_CARGOSRepository preRelacionCargoRepository)
		{
            _repository = repository;
            _preCargoRepository = preCargoRepository;
            _configuration = configuration;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _preRelacionCargoRepository = preRelacionCargoRepository;
            _preICPRepository = preICPRepository;
            _prePresupuestoRepository = prePresupuestoRepository;


        }


        public async Task<ResultDto<List<PreRelacionCargoGetDto>>> GetAll()
        {

            ResultDto<List<PreRelacionCargoGetDto>> result = new ResultDto<List<PreRelacionCargoGetDto>>(null);
            try
            {

                var relacionCargo = await _repository.GetAll();

               

                if (relacionCargo.Count() > 0)
                {
                    List<PreRelacionCargoGetDto> listDto = new List<PreRelacionCargoGetDto>();

                    foreach (var item in relacionCargo)
                    {
                        var dto = await MapPreRelacionCargo(item);
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



        public async Task<ResultDto<List<PreRelacionCargoGetDto>>> GetAllByPresupuesto(FilterByPresupuestoDto filter)
        {

            ResultDto<List<PreRelacionCargoGetDto>> result = new ResultDto<List<PreRelacionCargoGetDto>>(null);
            try
            {

                if (filter.CodigoPresupuesto == 0)
                {
                    var lastPresupuesto = await _prePresupuestoRepository.GetLast();
                    if (lastPresupuesto != null) filter.CodigoPresupuesto = lastPresupuesto.CODIGO_PRESUPUESTO;
                }

                var cargos = await _repository.GetAllByCodigoPresupuesto(filter.CodigoPresupuesto);

                if (filter.CodigoIcp != null && filter.CodigoIcp > 0)
                {
                    cargos = cargos.Where(x => x.CODIGO_ICP == filter.CodigoIcp).ToList();
                }


                if (cargos.Count() > 0)
                {
                    List<PreRelacionCargoGetDto> listDto = new List<PreRelacionCargoGetDto>();

                    foreach (var item in cargos)
                    {
                        var dto = await MapPreRelacionCargo(item);
                        listDto.Add(dto);
                    }

                    listDto = listDto.OrderBy(x => x.IcpConcat).ToList();
                    ExcelMapper mapper = new ExcelMapper();


                    var settings = _configuration.GetSection("Settings").Get<Settings>();


                    var ruta = @settings.ExcelFiles;  //@"/Users/freveron/Documents/MM/App/full-version/public/ExcelFiles";
                    var fileName = $"RelacionCargos {filter.CodigoPresupuesto.ToString()}-{filter.CodigoIcp.ToString()}-{DateTime.Now.ToLongDateString()}.xlsx";
                    string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);


                    mapper.Save(newFile, listDto, $"RelacionCargos", true);
                 
                    result.LinkData = $"/ExcelFiles/{fileName}";

                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";
                    result.LinkData = "";

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

        public async Task<ResultDto<PreRelacionCargoGetDto>> Update(PreRelacionCargoUpdateDto dto)
        {

            ResultDto<PreRelacionCargoGetDto> result = new ResultDto<PreRelacionCargoGetDto>(null);
            try
            {



                var relacionCargoUpdate = await _repository.GetByCodigo(dto.CodigoRelacionCargo);
                if (relacionCargoUpdate == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Relacion Cargo no existe";
                    return result;
                }
                var presupuesto = await _prePresupuestoRepository.GetByCodigo(13,dto.CodigoPresupuesto);
                if (presupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }

                var cargo = await _preCargoRepository.GetByCodigo(dto.CodigoCargo);
                if (cargo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo no existe";
                    return result;
                }

                var icp = await _preICPRepository.GetByCodigo(dto.CodigoIcp);
                if (icp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo ICP no existe";
                    return result;
                }

                if (dto.Cantidad <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad debe ser Mayor a Cero";
                    return result;
                }
                if (dto.Sueldo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sueldo debe ser Mayor a Cero";
                    return result;
                }


                relacionCargoUpdate.ANO = presupuesto.ANO;
                relacionCargoUpdate.ESCENARIO = dto.Escenario;
                relacionCargoUpdate.CODIGO_CARGO = dto.CodigoCargo;
                relacionCargoUpdate.CODIGO_ICP = dto.CodigoIcp;
                relacionCargoUpdate.SUELDO = dto.Sueldo;
                relacionCargoUpdate.COMPENSACION = dto.Compensacion;
                relacionCargoUpdate.PRIMA = dto.Prima;
                relacionCargoUpdate.OTRO = dto.Otro;
                relacionCargoUpdate.EXTRA1 = dto.Extra1;
                relacionCargoUpdate.EXTRA2 = dto.Extra2;
                relacionCargoUpdate.EXTRA3 = dto.Extra3;
                relacionCargoUpdate.FECHA_UPD = DateTime.Now;


                await _repository.Update(relacionCargoUpdate);

                var resultDto =await  MapPreRelacionCargo(relacionCargoUpdate);
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

        public async Task<ResultDto<PreRelacionCargoGetDto>> Create(PreRelacionCargoUpdateDto dto)
        {

            ResultDto<PreRelacionCargoGetDto> result = new ResultDto<PreRelacionCargoGetDto>(null);
            try
            {

                var presupuesto = await _prePresupuestoRepository.GetByCodigo(13, dto.CodigoPresupuesto);
                if (presupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }

                var cargo = await _preCargoRepository.GetByCodigo(dto.CodigoCargo);
                if (cargo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo no existe";
                    return result;
                }

                var icp = await _preICPRepository.GetByCodigo(dto.CodigoIcp);
                if (icp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo ICP no existe";
                    return result;
                }

                if (dto.Cantidad <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad debe ser Mayor a Cero";
                    return result;
                }
                if (dto.Sueldo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sueldo debe ser Mayor a Cero";
                    return result;
                }




                PRE_RELACION_CARGOS entity = new PRE_RELACION_CARGOS();
                entity.CODIGO_RELACION_CARGO = await _repository.GetNextKey();
                entity.ANO = presupuesto.ANO;
                entity.ESCENARIO = dto.Escenario;
                entity.CODIGO_CARGO = dto.CodigoCargo;
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.SUELDO = dto.Sueldo;
                entity.COMPENSACION = dto.Compensacion;
                entity.PRIMA = dto.Prima;
                entity.OTRO = dto.Otro;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.FECHA_UPD = DateTime.Now;
                entity.FECHA_INS = DateTime.Now;
                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreRelacionCargo(created.Data);
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
        public async Task<PreRelacionCargoGetDto> MapPreRelacionCargo(PRE_RELACION_CARGOS item)
        {
            PreRelacionCargoGetDto dto = new PreRelacionCargoGetDto();
            dto.CodigoRelacionCargo = item.CODIGO_RELACION_CARGO;
            dto.Ano = item.ANO;
            dto.Escenario = item.ESCENARIO;
            dto.CodigoIcp = item.CODIGO_ICP;
            dto.DenominacionIcp = "";
            dto.CodigoCargo = item.CODIGO_CARGO;
            dto.DenominacionCargo = "";
            dto.Cantidad = item.CANTIDAD;
            dto.Sueldo = item.SUELDO;
            dto.Compensacion = item.COMPENSACION;
            dto.Prima = item.PRIMA;
            dto.Otro = item.OTRO;
            if (item.EXTRA1 == null) item.EXTRA1 = "";
            if (item.EXTRA2 == null) item.EXTRA2 = "";
            if (item.EXTRA3 == null) item.EXTRA3 = "";
            dto.Extra1 = item.EXTRA1;
            dto.Extra2 = item.EXTRA2;
            dto.Extra3 = item.EXTRA3;
            dto.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
      
            var cargo = await _preCargoRepository.GetByCodigo(dto.CodigoCargo);
            if (cargo != null)
            {
                dto.DenominacionCargo = cargo.DENOMINACION;
               
                dto.DescripcionTipoCargo = "";
                var tipoCargo = await _repositoryPreDescriptiva.GetByCodigo(cargo.TIPO_CARGO_ID);
                if (tipoCargo != null)
                {
                    dto.DescripcionTipoCargo = tipoCargo.DESCRIPCION;
                }
                dto.DescripcionTipoPersonal="";
                var tipoPersonal = await _repositoryPreDescriptiva.GetByCodigo(cargo.TIPO_PERSONAL_ID);
                if (tipoPersonal != null)
                {
                    dto.DescripcionTipoPersonal = tipoPersonal.DESCRIPCION;
                }

            }

            var icp = await _preICPRepository.GetByCodigo(item.CODIGO_ICP);
            if (icp != null)
            {
                dto.DenominacionIcp = icp.DENOMINACION;

                dto.IcpConcat = $"{icp.CODIGO_SECTOR}-{icp.CODIGO_PROGRAMA}-{icp.CODIGO_SUBPROGRAMA}-{icp.CODIGO_PROYECTO}-{icp.CODIGO_ACTIVIDAD}-{icp.CODIGO_OFICINA}";
           
            }

            return dto;

        }


        public async Task<ResultDto<PreRelacionCargoDeleteDto>> Delete(PreRelacionCargoDeleteDto dto)
        {

            ResultDto<PreRelacionCargoDeleteDto> result = new ResultDto<PreRelacionCargoDeleteDto>(null);
            try
            {

                var relacionCargo = await _repository.GetByCodigo(dto.CodigoRelacionCargo);
                if (relacionCargo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Relacion Cargo no existe";
                    return result;
                }

               //TODO VALIDAR CONTRA RH_RELACION_CARGO

                var deleted = await _repository.Delete(dto.CodigoRelacionCargo);

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

